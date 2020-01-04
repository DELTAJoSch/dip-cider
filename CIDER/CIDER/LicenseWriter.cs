/* Copyright (C) 2020  Johannes Schiemer 
	This program is free software: you can redistribute it and/or modify 
	it under the terms of the GNU General Public License as published by 
	the Free Software Foundation, either version 3 of the License, or 
	(at your option) any later version. 
	This program is distributed in the hope that it will be useful, 
	but WITHOUT ANY WARRANTY; without even the implied warranty of 
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
	GNU General Public License for more details. 
	You should have received a copy of the GNU General Public License 
	along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/
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
        private IReader Reader;
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// This is the constructor for the LicenseWriter class
        /// </summary>
        /// <param name="Reader">Pass a Object that implements the IReader here - inject unit testing mocks and fakes here</param>
        public LicenseWriter(IReader Reader)
        {
            this.Reader = Reader;
        }

        /// <summary>
        /// This function writes the state of the license agreement to the config file
        /// </summary>
        /// <param name="State">This is the state of the license agreement</param>
        public void WriteAgreementState(bool State)
        {
            try
            {
                string[] cfg = Reader.ReadAllLines("CIDER.cfg");

                Regex regex = new Regex(@"LIAG:(true|false|True|False|TRUE|FALSE)");

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

                Reader.WriteAllLines((string[])list.ToArray(typeof(string)), "CIDER.cfg");
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
                string[] cfg = Reader.ReadAllLines("CIDER.cfg");

                Regex regexTrue = new Regex(@"LIAG:(true|True|TRUE)");
                Regex regexFalse = new Regex(@"LIAG:(false|False|FALSE)");

                foreach (string s in cfg)
                {
                    Match matchTrue = regexTrue.Match(s);
                    Match matchFalse = regexFalse.Match(s);
                    if (matchTrue.Success)
                    {
                        LicenseManager.LicensesAccepted = true;
                        return true;
                    }else if (matchFalse.Success)
                    {
                        LicenseManager.LicensesAccepted = false;
                        return false;
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
