/* Copyright (C) 2020  Johannes Schiemer 
	This program is free software: you can redistribute it and/or modify 
	it under the terms of the GNU General Public License as published by 
	the Free Software Foundation, either version 3 of the License, or 
	(at your option) any later version. 
	This program is distributed in the hope that it will be useful, 
	but WITHOUT ANY WARRANTY; without even the implied warranty of 
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
	GNU General Public License for more details. 
	You should have received a copy of the GNU General Public License 
	along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/
using CIDER.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using NmeaParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CIDER.LoadIO
{
    /// <summary>
    /// This class contains all the necessary parsing and file IO used in loading a CIDER data folder
    /// </summary>
    public class FileIO : IIO
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private int failedParses;

        /// <summary>
        /// This function reads the .csv part of the data folder
        /// </summary>
        /// <param name="Data">A DataProvider object to store the ingested data in</param>
        /// <param name="Path">A path to the folder to the .csv file</param>
        /// <param name="Read">An object implementing the IRead interface</param>
        /// <param name="Main">A MainWindowViewModel object to toggle the buttons from</param>
        public async Task ReadCSV(DataProvider Data, string Path, IRead Read, MainWindowViewModel Main)
        {
            logger.Debug("Starting CSV ingestion.");

            try
            {
                Main.ButtonState(false);
                await Task.Run(() =>
                {
                    string[] lines = Read.ReadLinesCsv(Path);

                    List<Tuple<float, float>> DataLsb = new List<Tuple<float, float>>();

                    foreach (string line in lines)
                    {
                        string[] split = line.Split(';');

                        if (split[0] == "Inf")
                        {
                            try
                            {
                                Data.RouteDate = Convert.ToDateTime(split[1]);
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex, "Couldn't convert to DateTime");
                            }
                            Data.RouteName = split[2];
                        }

                        if (split[0] == "Dat")
                        {
                            try
                            {
                                var tp = new Tuple<float, float, float>(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3]));

                                Data.XAcceleration.Add(tp.Item1);
                                Data.YAcceleration.Add(tp.Item2);
                                Data.ZAcceleration.Add(tp.Item3);
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex, "Couldn't convert to touple");
                            }
                            try
                            {
                                var tp = new Tuple<float, float, float>(float.Parse(split[4]), float.Parse(split[5]), float.Parse(split[6]));

                                Data.Roll.Add(tp.Item1);
                                Data.Pitch.Add(tp.Item2);
                                Data.Yaw.Add(tp.Item3);
                                Data.Heading.Add(tp.Item3);
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex, "Couldn't convert to touple");
                            }
                            try
                            {
                                Data.Height.Add(float.Parse(split[7]));
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex, "Couldn't Convert to float");
                            }
                            try
                            {
                                Data.Pressure.Add(float.Parse(split[8]));
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex, "Couldn't Convert to float");
                            }
                        }
                    }

                    Data.DataPointsAcceleration = Math.Min(Data.XAcceleration.Count, Data.YAcceleration.Count);
                    Data.DataPointsAcceleration = Math.Min(Data.DataPointsAcceleration, Data.ZAcceleration.Count);
                });

                Data.DataPointsAngle = Math.Min(Data.Roll.Count, Data.Pitch.Count);
                Data.DataPointsAngle = Math.Min(Data.DataPointsAngle, Data.Yaw.Count);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error reading File");
            }
            logger.Debug("CSV ingestion finished.");
        }

        /// <summary>
        /// This function ingests the .nmea file of a valid CIDER data folder
        /// </summary>
        /// <param name="Data">A DataProvider object to store the ingested data in</param>
        /// <param name="Path">A path to the folder to the .nmea file</param>
        /// <param name="Read">An object implementing the IRead interface</param>
        /// <param name="Main">A MainWindowViewModel object to toggle the buttons from</param>
        public async Task ReadNmea(DataProvider Data, string Path, IRead Read, MainWindowViewModel Main)
        {
            failedParses = 0;
            bool first = true;
            logger.Debug("Starting NMEA ingestion.");

            await Task.Run(() =>
            {
                try
                {
                    string[] lines = Read.ReadLinesNmea(Path);

                    foreach (string line in lines)
                    {
                        string[] vs = line.Split(',');
                        if (vs[0] == "$GPGGA")
                        {
                            GGA(line, Data, first);
                            if (Data.RouteStartTime != DateTime.Today)
                            {
                                first = false;
                            }
                        }
                        if (vs[0] == "$GPRMC")
                        {
                            RMC(line, Data, first);
                        }
                    }
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Error reading nmea file");
                }
            });

            logger.Debug("NMEA ingestion finished.");
            Main.ButtonState(true);
        }

        private void RMC(string Nmea, DataProvider Data, bool First)
        {
            NmeaStorage storage = new NmeaStorage();
            Parser parser = new Parser();

            try
            {
                parser.ParseRMC(Nmea, storage);

                Data.Velocity.Add(storage.SpeedOverGround);
                Data.DataPointsVelocity++;
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "Error whilst parsing");
                failedParses++;
            }
            finally
            {
                logger.Info("{0} parses failed.", failedParses);
            }
        }

        private void GGA(string Nmea, DataProvider Data, bool First)
        {
            NmeaStorage storage = new NmeaStorage();
            Parser parser = new Parser();

            try
            {
                parser.ParseGGA(Nmea, storage);

                if (storage.NorthSouth != '\u0000' && storage.EastWest != '\u0000' && storage.SattelitesInUse > 2)
                {
                    double latitude;
                    double longitude;

                    string lat = storage.Latitude.ToString();
                    string lon = storage.Longitude.ToString();

                    latitude = Convert.ToDouble(lat.Substring(0, 2)) + Convert.ToDouble(lat.Substring(2)) / 60;
                    longitude = Convert.ToDouble(lon.Substring(0, 2)) + Convert.ToDouble(lon.Substring(2)) / 60;

                    if (storage.NorthSouth == 'S')
                    {
                        latitude = -latitude;
                    }
                    if (storage.EastWest == 'W')
                    {
                        longitude = -longitude;
                    }
                    Data.Route.Add(new Location(latitude, longitude));
                }

                Data.AverageSattelitesInUse = storage.SattelitesInUse;

                if (DateTime.Today != storage.Time)
                    Data.RouteEndTime = storage.Time;

                if (First)
                    Data.RouteStartTime = storage.Time;
            }
            catch (Exception ex)
            {
                logger.Debug(ex, "Error whilst parsing");
                failedParses++;
            }
            finally
            {
                logger.Info("{0} parses failed.", failedParses);
            }
        }
    }

    /// <summary>
    /// This class implements the IRead interface. It is used to load the contents of trhe selected files
    /// </summary>
    public class Reader : IRead
    {
        /// <summary>
        /// This reads all the lines in a .csv file
        /// </summary>
        /// <param name="Path">A path to the .csv file</param>
        /// <returns>Returns a string array with the file contents</returns>
        public string[] ReadLinesCsv(string Path)
        {
            string name = GetFolderName(Path);
            return File.ReadAllLines($"{Path}\\{name}.csv");
        }

        /// <summary>
        /// This reads all the lines in a .nmea file
        /// </summary>
        /// <param name="Path">A path to the .nmea file</param>
        /// <returns>Returns a string array with the file contents</returns>
        public string[] ReadLinesNmea(string Path)
        {
            string name = GetFolderName(Path);
            return File.ReadAllLines($"{Path}\\{name}.nmea");
        }

        private string GetFolderName(string Path)
        {
            string[] splitter = Path.Split('\\');
            return splitter.Last();
        }
    }

    /// <summary>
    /// This interface should be implemented by classes being used to parse nmea and csv files
    /// </summary>
    public interface IIO
    {
        /// <summary>
        /// This function should ingest the .nmea file of a valid CIDER data folder
        /// </summary>
        /// <param name="Data">A DataProvider object to store the ingested data in</param>
        /// <param name="Path">A path to the folder to the .nmea file</param>
        /// <param name="Read">An object implementing the IRead interface</param>
        /// <param name="Main">A MainWindowViewModel object to toggle the buttons from</param>
        Task ReadNmea(DataProvider Data, string Path, IRead Read, MainWindowViewModel Main);

        /// <summary>
        /// This function should ingest the .csv file of a valid CIDER data folder
        /// </summary>
        /// <param name="Data">A DataProvider object to store the ingested data in</param>
        /// <param name="Path">A path to the folder to the .nmea file</param>
        /// <param name="Read">An object implementing the IRead interface</param>
        /// <param name="Main">A MainWindowViewModel object to toggle the buttons from</param>
        Task ReadCSV(DataProvider Data, string Path, IRead Read, MainWindowViewModel Main);
    }

    /// <summary>
    /// This interface should be implemented by classes being used to read nmea and csv files
    /// </summary>
    public interface IRead
    {
        /// <summary>
        /// This function should return the file contents of a .nmea file
        /// </summary>
        /// <param name="Path">A path to the file</param>
        /// <returns>The file contents</returns>
        string[] ReadLinesNmea(string Path);


        /// <summary>
        /// This function should return the contents of a .csv file
        /// </summary>
        /// <param name="Path">A path to the file</param>
        /// <returns>The file contents</returns>
        string[] ReadLinesCsv(string Path);
    }
}