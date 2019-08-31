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
        public void ReadCSV(DataProvider data, string path)
        {
            logger.Debug("Starting CSV ingestion.");
            string name = GetFolderName(path);

            try
            {
                string[] lines = File.ReadAllLines(path + "\\" + name + ".csv");

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
                            data.Velocity.Add(new Tuple<float, float, float>(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
                        }
                        catch (Exception ex)
                        {
                            logger.Error(ex, "Couldn't convert to touple");
                        }
                        try
                        {
                            data.Angles.Add(new Tuple<float, float, float>(float.Parse(split[4]), float.Parse(split[5]), float.Parse(split[6])));
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
                    }
                }
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error reading File");
            }
            logger.Debug("CSV ingestion finished.");
        }

        public void ReadNmea(DataProvider Data, string path)
        {
            failedParses = 0;
            bool first = true;
            logger.Debug("Starting NMEA ingestion.");
            try
            {
                string name = GetFolderName(path);

                string[] lines = File.ReadAllLines(path + "\\" + name + ".nmea");

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

        private string GetFolderName(string path)
        {
            string[] splitter = path.Split('\\');
            return splitter.Last();
        }
    }

    public interface IIO
    {
        void ReadNmea(DataProvider data, string path);
        void ReadCSV(DataProvider data, string path);
    }
}
