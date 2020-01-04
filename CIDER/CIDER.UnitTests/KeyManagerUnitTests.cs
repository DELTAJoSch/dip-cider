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
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.UnitTests
{
    public class KeyManagerUnitTests
    {
        private bool Called;

        [Test]
        public void KeyManager_Fetch_ReturnsValue()
        {
            DataProvider data = Substitute.For<DataProvider>();
            FakeKeyManagerReader reader = new FakeKeyManagerReader();
            KeyManager manager = new KeyManager(data, reader);

            manager.Fetch();

            Assert.AreEqual("abc", data.APIKey);
        }

        [Test]
        public void KeyManager_Put_ReturnsValue()
        {
            DataProvider data = Substitute.For<DataProvider>();
            FakeKeyManagerReader reader = new FakeKeyManagerReader();
            KeyManager manager = new KeyManager(data, reader);

            manager.Put();

            Assert.AreEqual("KEY:w.key", reader.NewFileName);
        }

        [Test]
        public void KeyManager_Put_CallsEvent()
        {
            DataProvider data = Substitute.For<DataProvider>();
            FakeKeyManagerReader reader = new FakeKeyManagerReader();
            KeyManager manager = new KeyManager(data, reader);
            Called = false;
            KeyManager.MapKeyChangedEvent += KeyManager_MapKeyChangedEvent;

            manager.Put();

            KeyManager.MapKeyChangedEvent -= KeyManager_MapKeyChangedEvent;

            Assert.IsTrue(Called);
        }

        private void KeyManager_MapKeyChangedEvent(object sender, EventArgs e)
        {
            Called = true;
        }
    }

    public class FakeKeyManagerReader : IReader
    {
        private bool hasKey;

        public FakeKeyManagerReader(bool hasKey = true)
        {
            this.hasKey = hasKey;
        }

        public bool FileExists(string filename)
        {
            return true;
        }

        public string[] ReadAllLines(string filename)
        {
            if (filename == "CIDER.cfg" && hasKey == true)
            {
                string[] vs = { "KEY:x.key" };
                return vs;
            }
            else if(filename == "x.key")
            {
                string[] vs = { "abc" };
                return vs;
            }
            else if(filename == "CIDER.cfg" && hasKey == false)
            {
                string[] vs = { "" };
                return vs;
            }
            else
            {
                throw new Exception();
            }
        }

        public string NewFileName;

        public System.Windows.Forms.DialogResult ShowDialog(System.Windows.Forms.OpenFileDialog dialog)
        {
            dialog.FileName = "w.key";
            return System.Windows.Forms.DialogResult.OK;
        }

        public void WriteAllLines(string[] lines, string filename)
        {
            NewFileName = lines[0];
        }

        public void WriteAllText(string text, string filename)
        {
            NewFileName = text;
        }
    }
}
