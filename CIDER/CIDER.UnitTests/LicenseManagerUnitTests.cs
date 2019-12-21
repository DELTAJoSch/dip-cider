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
