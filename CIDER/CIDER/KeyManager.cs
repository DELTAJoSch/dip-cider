using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;

namespace CIDER
{
    public class KeyManager
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        DataProvider _data;
        public static event EventHandler MapKeyChangedEvent;
        private IKeyManagerReader _reader;
        public KeyManager(DataProvider data, IKeyManagerReader reader)
        {
            _data = data;
            _reader = reader;
        }

        public bool Fetch()
        {
            try
            {
                string[] cfg = _reader.ReadAllLines("CIDER.cfg");

                if (_reader.FileExists(cfg[0]))
                {
                    string[] key = _reader.ReadAllLines(cfg[0]);

                    _data.APIKey = key[0];

                    return true;
                }
                else
                {
                    System.Windows.MessageBox.Show("To use all features correctly, please add a reference to a .key file containing an BingMaps API Key.", "BingMaps API Key", MessageBoxButton.OK, MessageBoxImage.Information);
                    logger.Info("No API Key found: Maps feature not available");
                    return false;
                }
            }
            catch(IndexOutOfRangeException ex)
            {
                System.Windows.MessageBox.Show("To use all features correctly, please add a reference to a .key file containing an BingMaps API Key.", "BingMaps API Key", MessageBoxButton.OK, MessageBoxImage.Information);
                logger.Info("No API Key found: Maps feature not available");
                return false;
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Error whilst reading API Keys");
                return false;
            }
        }

        public bool Put()
        {
            OpenFileDialog dialog = new OpenFileDialog();
            try
            {
                string[] cfg = _reader.ReadAllLines("CIDER.cfg");

                dialog.Title = "Select API Key File";
                dialog.Filter = "key files(*.key) | *.key";
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;
                dialog.AddExtension = true;
                dialog.DefaultExt = ".key";

                if(_reader.ShowDialog(dialog) == DialogResult.OK)
                {
                    cfg[0] = dialog.FileName;

                    _reader.WriteAllLines(cfg, "CIDER.cfg");
                    RaiseEvent(new EventArgs());
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (IndexOutOfRangeException ex)
            {
                _reader.WriteAllText(dialog.FileName, "CIDER.cfg");
                RaiseEvent(new EventArgs());
                return true;
            }
            catch (Exception ex)
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

    public interface IKeyManagerReader
    {
        string[] ReadAllLines(string filename);
        DialogResult ShowDialog(OpenFileDialog dialog);
        void WriteAllLines(string[] lines, string filename);
        void WriteAllText(string text, string filename);
        bool FileExists(string filename);
    }

    public class KeyManagerReader : IKeyManagerReader
    {
        public bool FileExists(string filename)
        {
            return File.Exists(filename);
        }

        public string[] ReadAllLines(string filename)
        {
            return File.ReadAllLines(filename);
        }

        public DialogResult ShowDialog(OpenFileDialog dialog)
        {
            return dialog.ShowDialog();
        }

        public void WriteAllLines(string[] lines, string filename)
        {
            File.WriteAllLines(filename, lines);
        }

        public void WriteAllText(string text, string filename)
        {
            File.WriteAllText(filename, text);
        }
    }
}
