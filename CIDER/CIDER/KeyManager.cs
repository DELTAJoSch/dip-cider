using System;
using System.Collections;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Forms;

namespace CIDER
{
    /// <summary>
    /// This class handles the file interaction for writing the path to the api key file
    /// </summary>
    public class KeyManager : IKeyManager
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private DataProvider _data;

        /// <summary>
        /// This event is fired when the api key changes
        /// </summary>
        public static event EventHandler MapKeyChangedEvent;

        private IReader _reader;

        /// <summary>
        /// This is the constructor for the KeyManager class
        /// </summary>
        /// <param name="Data">This expects a DataProvider object to store the api key in</param>
        /// <param name="Reader">Pass a Object that implements the IReader here - inject unit testing mocks and fakes here</param>
        public KeyManager(DataProvider Data, IReader Reader)
        {
            _data = Data;
            _reader = Reader;
        }

        /// <summary>
        /// This function tries to fetch the api key from the key file (if available)
        /// </summary>
        /// <returns>This function returns a bool telling the caller if a key was found</returns>
        public bool Fetch()
        {
            try
            {
                string[] cfg = _reader.ReadAllLines("CIDER.cfg");

                Regex regex = new Regex(@"KEY:.*");

                foreach(string s in cfg)
                {
                    Match match = regex.Match(s);
                    if (_reader.FileExists(s.Remove(0, 4)) & match.Success)
                    {
                        string[] key = _reader.ReadAllLines(s.Remove(0, 4));

                        _data.APIKey = key[0];
                        return true;
                    }
                }

                System.Windows.MessageBox.Show("To use all features correctly, please add a reference to a .key file containing an BingMaps API Key.", "BingMaps API Key", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Info("No API Key found: Maps feature not available");
                return false;
            }
            catch (IndexOutOfRangeException ex)
            {
                System.Windows.MessageBox.Show("To use all features correctly, please add a reference to a .key file containing an BingMaps API Key.", "BingMaps API Key", MessageBoxButton.OK, MessageBoxImage.Error);
                logger.Info(ex ,"No API Key found: Maps feature not available");
                return false;
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Error whilst reading API Keys");
                return false;
            }
        }

        /// <summary>
        /// This function tries to put the path of a key file into the config
        /// </summary>
        /// <returns>returns true if successful</returns>
        public bool Put()
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();

                dialog.Title = "Select API Key File";
                dialog.Filter = "key files(*.key) | *.key";
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;
                dialog.AddExtension = true;
                dialog.DefaultExt = ".key";

                if (_reader.ShowDialog(dialog) == DialogResult.OK)
                {
                    try
                    {
                        string[] cfg = _reader.ReadAllLines("CIDER.cfg");

                        Regex regex = new Regex(@"KEY:.*");

                        ArrayList list = new ArrayList();
                        bool foundKEY = false;

                        foreach (string s in cfg)
                        {
                            Match match = regex.Match(s);

                            string line;

                            if (match.Success)
                            {
                                line = $"KEY:{dialog.FileName}";
                                foundKEY = true;
                            }
                            else
                            {
                                line = s;
                            }

                            list.Add(line);
                        }

                        if (!foundKEY)
                            list.Add($"KEY:{dialog.FileName}");

                        _reader.WriteAllLines((string[])list.ToArray(typeof(string)), "CIDER.cfg");

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex, "Error whilst adding API Key");
                        return false;
                    }

                    RaiseEvent(new EventArgs());
                    return true;
                }
                return false;
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error whilst adding API Key");
                return false;
            }
        }

        private void RaiseEvent(EventArgs e)
        {
            EventHandler handler = MapKeyChangedEvent;
            if (handler != null)
                handler.Invoke(this, e);
        }
    }

    /// <summary>
    /// This interface acts as a way to inject fakes into the Reading and Writing Classes
    /// </summary>
    public interface IReader
    {
        /// <summary>
        /// Reads all lines of a given file
        /// </summary>
        /// <param name="filename">The path of the file to be read</param>
        /// <returns>Returns a string array of the file (line by line)</returns>
        string[] ReadAllLines(string filename);

        /// <summary>
        /// This function shows a openfiledialog
        /// </summary>
        /// <param name="dialog">The dialog to be shown</param>
        /// <returns>Returns the dialogresult</returns>
        DialogResult ShowDialog(OpenFileDialog dialog);

        /// <summary>
        /// Writes the given lines to the specified file
        /// </summary>
        /// <param name="lines">A string array of the lines to be written</param>
        /// <param name="filename">A path to the file</param>
        void WriteAllLines(string[] lines, string filename);

        /// <summary>
        /// This function writes the given string to the specified file
        /// </summary>
        /// <param name="text">The text to be written</param>
        /// <param name="filename">A path to the file</param>
        void WriteAllText(string text, string filename);

        /// <summary>
        /// Checks if a file exists
        /// </summary>
        /// <param name="filename">A path to the file</param>
        /// <returns>Returns true if the file exists</returns>
        bool FileExists(string filename);
    }

    /// <summary>
    /// The production code implementation of the IReader interface
    /// </summary>
    public class FileReader : IReader
    {
        /// <summary>
        /// Checks if a file exists
        /// </summary>
        /// <param name="filename">A path to the file</param>
        /// <returns>Returns true if the file exists</returns>
        public bool FileExists(string filename)
        {
            return File.Exists(filename);
        }

        /// <summary>
        /// Reads all lines of a given file
        /// </summary>
        /// <param name="filename">The path of the file to be read</param>
        /// <returns>Returns a string array of the file (line by line)</returns>
        public string[] ReadAllLines(string filename)
        {
            return File.ReadAllLines(filename);
        }

        /// <summary>
        /// This function shows a openfiledialog
        /// </summary>
        /// <param name="dialog">The dialog to be shown</param>
        /// <returns>Returns the dialogresult</returns>
        public DialogResult ShowDialog(OpenFileDialog dialog)
        {
            return dialog.ShowDialog();
        }

        /// <summary>
        /// Writes the given lines to the specified file
        /// </summary>
        /// <param name="lines">A string array of the lines to be written</param>
        /// <param name="filename">A path to the file</param>
        public void WriteAllLines(string[] lines, string filename)
        {
            File.WriteAllLines(filename, lines);
        }

        /// <summary>
        /// This function writes the given string to the specified file
        /// </summary>
        /// <param name="text">The text to be written</param>
        /// <param name="filename">A path to the file</param>
        public void WriteAllText(string text, string filename)
        {
            File.WriteAllText(filename, text);
        }
    }

    /// <summary>
    /// This interface can used for unit testing
    /// </summary>
    public interface IKeyManager
    {
        /// <summary>
        /// This function should be used to put a new key into a file.
        /// </summary>
        /// <returns>true if successful</returns>
        bool Put();

        /// <summary>
        /// This function should be used to fetch a key.
        /// </summary>
        /// <returns>true if sucessful</returns>
        bool Fetch();
    }
}