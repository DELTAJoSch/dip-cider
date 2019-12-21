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
