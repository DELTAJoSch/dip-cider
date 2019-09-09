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
        public void ReadCSV(DataProvider data, string path, IRead read)
        {
            logger.Debug("Starting CSV ingestion.");

            try
            {
                string[] lines = read.ReadLinesCsv(path);

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

                            data.XVelocity.Add(tp.Item1);
                            data.YVelocity.Add(tp.Item2);
                            data.ZVelocity.Add(tp.Item3);
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Couldn't convert to touple");
                        }
                        try
                        {
                            var tp = new Tuple<float, float, float>(float.Parse(split[4]), float.Parse(split[5]), float.Parse(split[6]));

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
                            data.Pressure.Add(float.Parse(split[7]));
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Couldn't Convert to float");
                        }
                        try
                        {
                            data.Height.Add(float.Parse(split[8]));
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Couldn't Convert to float");
                        }
                    }
                }

                data.DataPointsVelocity = Math.Min(data.XVelocity.Count, data.YVelocity.Count);
                data.DataPointsVelocity = Math.Min(data.DataPointsVelocity, data.ZVelocity.Count);
                data.DataPointsAcceleration = Math.Min(data.XAcceleration.Count, data.YAcceleration.Count);
                data.DataPointsAcceleration = Math.Min(data.DataPointsAcceleration, data.ZAcceleration.Count);
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error reading File");
            }
            logger.Debug("CSV ingestion finished.");
        }

        public void ReadNmea(DataProvider Data, string path, IRead read)
        {
            failedParses = 0;
            bool first = true;
            logger.Debug("Starting NMEA ingestion.");
            try
            {
                string[] lines = read.ReadLinesNmea(path);

                foreach (string line in lines)
                {
                    string[] vs = line.Split(',');
                    if (vs[0] == "$GPGGA")
                    {
                        GGA(line, Data, first);
                        if(Data.RouteStartTime != DateTime.Today)
                        {
                            first = false;
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error reading nmea file");
            }
            logger.Debug("NMEA ingestion finished.");
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
        void ReadNmea(DataProvider data, string path, IRead read);
        void ReadCSV(DataProvider data, string path, IRead read);
    }

    public interface IRead
    {
        string[] ReadLinesNmea(string path);
        string[] ReadLinesCsv(string path);
    }
}
