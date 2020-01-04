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
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.UnitTests
{
    [TestFixture]
    public class LicenseManagerUnitTests
    {
        [Test]
        public void LicenseManager_LicenseClearCalled_ClearsLicenses()
        {
            LicenseManager.AddLicense("blub");
            LicenseManager.ClearLicenses();

            var emptyList = new List<string>();
            Assert.AreEqual(emptyList, LicenseManager.Licenses);
        }

        [Test]
        public void LicenseManager_LicenseAddCalled_AddsLicense()
        {
            LicenseManager.AddLicense("blub");

            var testList = new List<string>();
            testList.Add("blub");

            Assert.AreEqual(testList, LicenseManager.Licenses);
        }

        [Test]
        public void LicenseManager_LicenseAcceptedStateChanged_CorrectState()
        {
            LicenseManager.LicensesAccepted = false;

            Assert.IsFalse(LicenseManager.LicensesAccepted);

            LicenseManager.LicensesAccepted = true;

            Assert.IsTrue(LicenseManager.LicensesAccepted);
        }

        [TearDown]
        public void LicenseManager_Reset()
        {
            LicenseManager.ClearLicenses();
        }
    }
}
