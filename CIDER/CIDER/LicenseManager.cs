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
