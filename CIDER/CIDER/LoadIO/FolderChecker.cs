using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.LoadIO
{
    public class FolderChecker : IChecker
    ///Summary
    ///This class implements the IChecker Interface. This class is used to check if a previously selected folder contains the correct files
    {
        public bool IsCorrectFolder(string Path)
        {
            string name = GetFolderName(Path);

            if(File.Exists(Path + "\\" + name +".csv") && File.Exists(Path+ "\\" + name + ".nmea"))
            {
                return true;
            }

            return false;
        }

        private string GetFolderName(string path)
        {
            string[] splitter = path.Split('\\');
            return splitter.Last();
        }
    }

    public interface IChecker
    {
        bool IsCorrectFolder(string Path);
    }
}
