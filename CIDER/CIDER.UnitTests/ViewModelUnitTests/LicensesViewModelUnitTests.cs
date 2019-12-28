using CIDER.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.UnitTests.ViewModelUnitTests
{
    [TestFixture]
    public class LicensesViewModelUnitTests
    {
        [Test]
        public void LicensesViewModel_CheckboxStateChanged_ChangesButtonState()
        {
            LicenseWriter writer = new LicenseWriter(new FakeLicenseReader());
            LicensesViewModel model = new LicensesViewModel(writer);

            Assert.IsFalse(model.CommandAcceptEnabled);

            model.CheckboxStateChangedCommand.Execute(this);

            Assert.IsTrue(model.CommandAcceptEnabled);
        }

        [Test]
        public void LicensesViewModel_SaveAcceptAgreement_SavesAgreement()
        {
            LicenseWriter writer = new LicenseWriter(new FakeLicenseReader());
            LicensesViewModel model = new LicensesViewModel(writer);
            LicenseHolder.AcceptedLicense = false;

            model.CheckboxStateChangedCommand.Execute(this);
            model.SaveAcceptAgreement();

            Assert.IsTrue(LicenseHolder.AcceptedLicense);
        }

        [Test]
        public void LicensesViewModel_SaveAcceptAgreement_SavesAgreementToFile()
        {
            var reader = new FakeLicenseReader();
            LicenseWriter writer = new LicenseWriter(reader);
            LicensesViewModel model = new LicensesViewModel(writer);
            LicenseHolder.AcceptedLicense = false;

            model.CheckboxStateChangedCommand.Execute(this);
            model.SaveAcceptAgreement();

            foreach (string s in reader.NewFile)
            {
                if (s == "LIAG:True")
                {
                    Assert.Pass();
                }
            }
        }
    }
}
