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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER
{
    /// <summary>
    /// This class is a static manager class responsible for providing a quick way to get access to added license texts
    /// </summary>
    public static class LicenseManager
    {
        /// <summary>
        /// This bool saves the state of the license agreement (accepted/not accepted)
        /// </summary>
        public static bool LicensesAccepted;

        /// <summary>
        /// This List contains all the different licenses that are used in the project
        /// </summary>
        public static readonly List<string> Licenses = new List<string>();

        /// <summary>
        /// This functions adds a license text to the license list
        /// </summary>
        /// <param name="License"></param>
        public static void AddLicense(string License)
        {
            Licenses.Add(License);
        }

        /// <summary>
        /// This function clears the license list
        /// </summary>
        public static void ClearLicenses()
        {
            Licenses.Clear();
        }
    }
}
