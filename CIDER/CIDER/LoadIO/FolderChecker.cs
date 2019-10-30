using System.IO;
using System.Linq;

namespace CIDER.LoadIO
{
    /// <summary>
    /// This class implements the IChecker interface. The purpose of this class is to check if a selected folder contains valid data.
    /// </summary>
    public class FolderChecker : IChecker
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// This function checks the integrity of the data in the selected folder.
        /// </summary>
        /// <param name="Path">A path to the folder</param>
        /// <returns>Returns true if the folder contains the correct data</returns>
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

    /// <summary>
    /// The IChecker interface is supposed to be inherited by functions used for checking the data integrity of a folder
    /// </summary>
    public interface IChecker
    {
        /// <summary>
        /// This function should be called to check a particular folder.
        /// </summary>
        /// <param name="Path">A path to the selected folder</param>
        /// <returns>This function should return true if the data in the stored in the folder is ok.</returns>
        bool IsCorrectFolder(string Path);
    }
}