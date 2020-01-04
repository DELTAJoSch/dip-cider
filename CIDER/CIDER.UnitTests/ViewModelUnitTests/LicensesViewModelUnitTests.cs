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
