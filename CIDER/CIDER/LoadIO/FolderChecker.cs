using System.IO;
using System.Linq;

namespace CIDER.LoadIO
{
    public class FolderChecker : IChecker
    ///Summary
    ///This class implements the IChecker Interface. This class is used to check if a previously selected folder contains the correct files
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public bool IsCorrectFolder(string Path)
        {
            string name = GetFolderName(Path);

            if (File.Exists(Path + "\\" + name + ".csv") && File.Exists(Path + "\\" + name + ".nmea"))
            {
                logger.Info("Folder {0} is correct", Path);
                return true;
            }

            logger.Info("Folder {0} is invalid", Path);
            return false;
        }

        private string GetFolderName(string path)
        {
            string[] splitter = path.Split('\\');
            logger.Debug("Getting folder name: {0} from path: {1}", splitter.Last(), path);
            return splitter.Last();
        }
    }

    public interface IChecker
    {
        bool IsCorrectFolder(string Path);
    }
}