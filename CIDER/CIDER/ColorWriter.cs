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
using System.Text.RegularExpressions;

namespace CIDER
{
    /// <summary>
    /// This class is used to configure the config file to write and read the user-specified color settings
    /// </summary>
    public class ColorWriter
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private IReader _reader;

        /// <summary>
        /// The constructor for the ColorWriter class
        /// </summary>
        /// <param name="reader">Pass a Object that implements the IReader here - inject unit testing mocks and fakes here</param>
        public ColorWriter(IReader reader)
        {
            _reader = reader;
        }

        /// <summary>
        /// This function reads the config file and gets the color and theme from the file (if available)
        /// </summary>
        /// <returns>A tuple with the elements Theme followed by Accent (strings) should be returned</returns>
        public Tuple<string, string> GetSetTheming()
        {
            try
            {
                string[] cfg = _reader.ReadAllLines("CIDER.cfg");

                Regex regex = new Regex(@"THEME:.*");

                string Theme = "", Accent = "";

                foreach (string s in cfg)
                {
                    Match match = regex.Match(s);
                    if (match.Success)
                    {
                        Theme = s.Substring(6);
                    }
                }

                regex = new Regex(@"ACCENT:.*");

                foreach (string s in cfg)
                {
                    Match match = regex.Match(s);
                    if (match.Success)
                    {
                        Accent = s.Substring(7);
                    }
                }

                if (!(String.IsNullOrEmpty(Theme)) & !(String.IsNullOrEmpty(Accent)))
                {
                    return new Tuple<string, string>(Theme, Accent);
                }

                else throw new ColorWriterNoColorException();
            }
            catch (Exception ex)
            {
                logger.Info(ex, "No Color Found");
                throw new ColorWriterNoColorException();
            }
        }

        /// <summary>
        /// Writes the user specified Accent and Theme to the config
        /// </summary>
        /// <param name="Accent">This is the string name of the accent</param>
        /// <param name="Theme">This is the string name of the theme</param>
        public void SetTheming(string Accent, string Theme)
        {
            try
            {
                string[] cfg = _reader.ReadAllLines("CIDER.cfg");

                Regex regex = new Regex(@"ACCENT:.*");

                ArrayList fileIterationOne = new ArrayList();
                bool foundAccent = false;

                foreach (string s in cfg)
                {
                    Match match = regex.Match(s);

                    string line;

                    if (match.Success)
                    {
                        line = $"ACCENT:{Accent}";
                        foundAccent = true;
                    }
                    else
                    {
                        line = s;
                    }

                    fileIterationOne.Add(line);
                }

                if (!foundAccent)
                    fileIterationOne.Add($"ACCENT:{Accent}");

                ArrayList fileIterationTwo = new ArrayList();
                bool foundTheme = false;

                regex = new Regex(@"THEME:.*");

                foreach (string s in (string[])fileIterationOne.ToArray(typeof(string)))
                {
                    Match match = regex.Match(s);

                    string line;

                    if (match.Success)
                    {
                        line = $"THEME:{Theme}";
                        foundTheme = true;
                    }
                    else
                    {
                        line = s;
                    }

                    fileIterationTwo.Add(line);
                }

                if (!foundTheme)
                    fileIterationTwo.Add($"THEME:{Theme}");

                _reader.WriteAllLines((string[])fileIterationTwo.ToArray(typeof(string)), "CIDER.cfg");
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Error whilst setting Color");
                throw new ColorWriterWritingException();
            }
        }
    }
}