using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CIDER
{
    /// <summary>
    /// This class is used to write the state of the license agreement to the config
    /// </summary>
    public class LicenseWriter
    {
        private IReader keyManagerReader;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// This is the constructor for the LicenseWriter class
        /// </summary>
        /// <param name="Reader">Pass a Object that implements the IReader here - inject unit testing mocks and fakes here</param>
        public LicenseWriter(IReader Reader)
        {
            keyManagerReader = Reader;
        }

        /// <summary>
        /// This function writes the state of the license agreement to the config file
        /// </summary>
        /// <param name="State">This is the state of the license agreement</param>
        public void WriteAgreementState(bool State)
        {
            try
            {
                string[] cfg = keyManagerReader.ReadAllLines("CIDER.cfg");

                Regex regex = new Regex(@"LIAG:(true|false)");

                ArrayList list = new ArrayList();
                bool foundLIAG = false;

                foreach (string s in cfg)
                {
                    Match match = regex.Match(s);

                    string line;

                    if (match.Success)
                    {
                        line = $"LIAG:{State.ToString()}";
                        foundLIAG = true;
                    }
                    else
                    {
                        line = s;
                    }

                    list.Add(line);
                }

                if (!foundLIAG)
                    list.Add($"LIAG:{State.ToString()}");

                keyManagerReader.WriteAllLines((string[])list.ToArray(typeof(string)), "CIDER.cfg");
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Error whilst writing license agreement status");
            }
        }

        /// <summary>
        /// This function reads the license agreement state
        /// </summary>
        /// <returns>The return value of this function specifies wether prior data could be found on the status of the license agreement</returns>
        public bool ReadAgreementState()
        {
            try
            {
                string[] cfg = keyManagerReader.ReadAllLines("CIDER.cfg");

                Regex regex = new Regex(@"LIAG:(true|false|True|False|TRUE|FALSE)");

                foreach (string s in cfg)
                {
                    Match match = regex.Match(s);
                    if (match.Success)
                    {
                        LicenseManager.LicensesAccepted = true;
                        return true;
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Error whilst reading license agreement status");
                return false;
            }
        }
    }
}
