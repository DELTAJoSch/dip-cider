﻿using CIDER.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using NmeaParser;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.LoadIO
{
    public class FileIO : IIO
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private int failedParses;
        public async void ReadCSV(DataProvider data, string path, IRead read, MainWindowViewModel main)
        {
            logger.Debug("Starting CSV ingestion.");

            try
            {
                main.ButtonState(false);
                await Task.Run(() =>
                {
                    string[] lines = read.ReadLinesCsv(path);

                    List<Tuple<float, float>> DataLsb = new List<Tuple<float, float>>();

                    foreach (string line in lines)
                    {
                        string[] split = line.Split(';');

                        if (split[0] == "Inf")
                        {
                            try
                            {
                                data.RouteDate = Convert.ToDateTime(split[1]);
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex, "Couldn't convert to DateTime");
                            }
                            data.RouteName = split[2];
                        }

                        if (split[0] == "Dat")
                        {
                            try
                            {
                                var tp = new Tuple<float, float, float>(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3]));

                                data.XAcceleration.Add(tp.Item1);
                                data.YAcceleration.Add(tp.Item2);
                                data.ZAcceleration.Add(tp.Item3);
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex, "Couldn't convert to touple");
                            }
                            try
                            {
                                var tp = new Tuple<float, float>(float.Parse(split[4]), float.Parse(split[5]));

                                var xGaussData = tp.Item1 * 0.48828125;
                                var yGaussData = tp.Item2 * 0.48828125;

                                double heading;

                                if ((xGaussData == 0) && (yGaussData < 0))
                                {
                                    heading = 90;
                                }
                                else if ((xGaussData == 0) && (yGaussData >= 0))
                                {
                                    heading = 0;
                                }
                                else
                                {
                                    heading = Math.Atan(xGaussData / yGaussData) * (180 / Math.PI);
                                }

                                data.Heading.Add((float)heading);
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex, "Couldn't convert to touple");
                            }
                            try
                            {
                                data.Height.Add(float.Parse(split[6]));
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex, "Couldn't Convert to float");
                            }
                            try
                            {
                                data.Pressure.Add(float.Parse(split[7]));
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex, "Couldn't Convert to float");
                            }
                        }
                    }

                    data.DataPointsAcceleration = Math.Min(data.XAcceleration.Count, data.YAcceleration.Count);
                    data.DataPointsAcceleration = Math.Min(data.DataPointsAcceleration, data.ZAcceleration.Count);
                });

                for (int i = 0; i < data.DataPointsAcceleration; i++)
                {
                    try
                    {
                        var Angle = ExtraMath.CalculateAngle(data.XAcceleration.ElementAt(i), data.YAcceleration.ElementAt(i), data.ZAcceleration.ElementAt(i));

                        data.Roll.Add(Angle.Item1);
                        data.Pitch.Add(Angle.Item2);
                        data.Yaw.Add(Angle.Item3);
                    }catch(Exception ex)
                    {
                        logger.Warn(ex, "Error whilst calculating Angles");
                    }
                }

                data.DataPointsAngle = Math.Min(data.Roll.Count, data.Pitch.Count);
                data.DataPointsAngle = Math.Min(data.DataPointsAngle, data.Yaw.Count);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error reading File");
            }
            logger.Debug("CSV ingestion finished.");
        }

        public async void ReadNmea(DataProvider Data, string path, IRead read, MainWindowViewModel main)
        {
            failedParses = 0;
            bool first = true;
            logger.Debug("Starting NMEA ingestion.");

            await Task.Run(() =>
            {
                try
                {
                    string[] lines = read.ReadLinesNmea(path);

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
                        if(vs[0] == "$GPRMC")
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
            main.ButtonState(true);
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

                if(DateTime.Today != storage.Time)
                    Data.RouteEndTime = storage.Time;

                if (First)
                    Data.RouteStartTime = storage.Time;
            }
            catch(Exception ex)
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

    public class Reader : IRead
    {
        public string[] ReadLinesCsv(string path)
        {
            string name = GetFolderName(path);
            return File.ReadAllLines(path + "\\" + name + ".csv");
        }

        public string[] ReadLinesNmea(string path)
        {
            string name = GetFolderName(path);
            return File.ReadAllLines(path + "\\" + name + ".nmea");
        }

        private string GetFolderName(string path)
        {
            string[] splitter = path.Split('\\');
            return splitter.Last();
        }
    }

    public interface IIO
    {
        void ReadNmea(DataProvider data, string path, IRead read, MainWindowViewModel main);
        void ReadCSV(DataProvider data, string path, IRead read, MainWindowViewModel main);
    }

    public interface IRead
    {
        string[] ReadLinesNmea(string path);
        string[] ReadLinesCsv(string path);
    }
}
