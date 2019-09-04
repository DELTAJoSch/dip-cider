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
        public KeyManager(DataProvider data)
        {
            _data = data;
        }

        public bool Fetch()
        {
            try
            {
                string[] cfg = File.ReadAllLines("CIDER.cfg");

                if (File.Exists(cfg[0]))
                {
                    string[] key = File.ReadAllLines(cfg[0]);

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
                string[] cfg = File.ReadAllLines("CIDER.cfg");

                dialog.Title = "Select API Key File";
                dialog.Filter = "key files(*.key) | *.key";
                dialog.CheckFileExists = true;
                dialog.Multiselect = false;
                dialog.AddExtension = true;
                dialog.DefaultExt = ".key";

                if(dialog.ShowDialog() == DialogResult.OK)
                {
                    cfg[0] = dialog.FileName;

                    File.WriteAllLines("CIDER.cfg", cfg);
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
                File.WriteAllText("CIDER.cfg", dialog.FileName);
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
            handler.Invoke(this, e);
        }
    }
}
