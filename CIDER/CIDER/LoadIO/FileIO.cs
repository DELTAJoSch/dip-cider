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
        public void ReadCSV(DataProvider data, string path)
        {
            string name = GetFolderName(path);

            string[] lines = File.ReadAllLines(path + "\\" + name + ".csv");

            foreach(string line in lines)
            {
                string[] split = line.Split(';');

                if(split[0] == "Inf")
                {
                    try
                    {
                        data.RouteDate = Convert.ToDateTime(split[1]);
                    }
                    catch
                    {
                        throw new NotImplementedException();
                    }
                    data.RouteName = split[2];
                }
                
                if(split[0] == "Dat")
                {
                    try
                    {
                        data.Velocity.Add(new Tuple<float, float, float>(float.Parse(split[1]), float.Parse(split[2]), float.Parse(split[3])));
                    }
                    catch
                    {
                        throw new NotImplementedException();
                    }
                    try
                    {
                        data.Angles.Add(new Tuple<float, float, float>(float.Parse(split[4]), float.Parse(split[5]), float.Parse(split[6])));
                    }
                    catch
                    {
                        throw new NotImplementedException();
                    }
                    try
                    {
                        data.Pressure.Add(float.Parse(split[7]));
                    }
                    catch
                    {
                        throw new NotImplementedException();
                    }
                }
            }
        }

        public void ReadNmea(DataProvider data, string path)
        {
            throw new NotImplementedException();
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
