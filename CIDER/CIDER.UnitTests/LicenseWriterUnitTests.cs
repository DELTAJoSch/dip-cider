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
using System.Windows.Forms;

namespace CIDER.UnitTests
{
    [TestFixture]
    public class LicenseWriterUnitTests
    {
        [Test]
        public void LicenseWriter_WriteAgreementStateWithOtherParameters_WritesLicenseCorrectly()
        {
            var reader = new FakeLicenseReader();
            reader.multiple = true;

            LicenseWriter writer = new LicenseWriter(reader);

            writer.WriteAgreementState(false);

            foreach(string s in reader.NewFile)
            {
                if(s == "LIAG:False")
                {
                    Assert.Pass();
                }
            }

            Assert.Fail();
        }

        [Test]
        public void LicenseWriter_WriteAgreementStateWithoutOtherParameters_WritesLicenseCorrectly()
        {
            var reader = new FakeLicenseReader();
            reader.multiple = false;

            LicenseWriter writer = new LicenseWriter(reader);

            writer.WriteAgreementState(false);

            foreach (string s in reader.NewFile)
            {
                if (s == "LIAG:False")
                {
                    Assert.Pass();
                }
            }

            Assert.Fail();
        }

        [Test]
        public void LicenseWriter_ReadAgreementStateAgreementFalse_ReadsStateCorrectly()
        {
            var reader = new FakeLicenseReader();
            reader.multiple = true;
            reader.state = false;

            LicenseWriter writer = new LicenseWriter(reader);

            Assert.IsFalse(writer.ReadAgreementState());
        }

        [Test]
        public void LicenseWriter_ReadAgreementStateAgreementTrue_ReadsStateCorrectly()
        {
            var reader = new FakeLicenseReader();
            reader.multiple = true;
            reader.state = true;

            LicenseWriter writer = new LicenseWriter(reader);

            Assert.IsTrue(writer.ReadAgreementState());
        }

        [Test]
        public void LicenseWriter_ReadAgreementStateAgreementTrue_UpdatesStateInManagerCorrectly()
        {
            LicenseManager.LicensesAccepted = false;

            var reader = new FakeLicenseReader();
            reader.multiple = true;
            reader.state = true;

            LicenseWriter writer = new LicenseWriter(reader);

            writer.ReadAgreementState();

            Assert.IsTrue(LicenseManager.LicensesAccepted);
        }
    }

    public class FakeLicenseReader : IReader
    {
        public bool multiple = false;
        public bool state = true;

        public bool FileExists(string filename)
        {
            return true;
        }

        public string[] ReadAllLines(string filename)
        {
            if (filename == "CIDER.cfg" && multiple == true)
            {
                string[] vs = { "KEY:x.key", $"LIAG:{state}"};
                return vs;
            }
            else if (filename == "CIDER.cfg" && multiple == false)
            {
                string[] vs = { $"LIAG:{state}" };
                return vs;
            }
            else
            {
                throw new Exception();
            }
        }

        public string[] NewFile;

        public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.OpenFileDialog dialog)
        {
            dialog.FileName = "w.key";
            return System.Windows.Forms.DialogResult.OK;
        }

        public void WriteAllLines(string[] lines, string filename)
        {
            NewFile = lines;
        }

        public void WriteAllText(string text, string filename)
        {
            NewFile[0] = text;
        }
    }
}
