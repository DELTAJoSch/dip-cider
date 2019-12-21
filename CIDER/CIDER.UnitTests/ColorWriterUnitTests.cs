using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.UnitTests
{
    [TestFixture]
    public class ColorWriterUnitTests
    {
        [Test]
        public void ColorWriter_GetSetThemeing_ReturnsCorrectThemeing()
        {
            var reader = new FakeColorReader();
            reader.multiple = true;

            ColorWriter writer = new ColorWriter(reader);

            var res = writer.GetSetTheming();

            Assert.AreEqual("BaseLight", res.Item1);
            Assert.AreEqual("Cobalt", res.Item2);
        }

        [Test]
        public void ColorWriter_GetSetThemeingWithoutThemeing_ThrowsError()
        {
            var reader = new FakeColorReader();
            reader.multiple = true;
            reader.hasThemeing = false;

            ColorWriter writer = new ColorWriter(reader);

            Assert.Throws(typeof(ColorWriterNoColorException), () => writer.GetSetTheming());
        }

        [Test]
        public void ColorWriter_SetThemeing_SetsThemeingCorrectly()
        {
            var reader = new FakeColorReader();
            reader.multiple = true;

            ColorWriter writer = new ColorWriter(reader);

            writer.SetTheming("Mauve", "BaseDark");

            bool theme = false;
            bool accent = false;

            foreach (string s in reader.NewFile)
            {
                if (s == "THEME:BaseDark")
                    theme = true;

                if (s == "ACCENT:Mauve")
                    accent = true;
            }

            Assert.IsTrue(theme);
            Assert.IsTrue(accent);
        }
    }

    public class FakeColorReader : IReader
    {
        public bool multiple = false;
        public bool hasThemeing = true;
        public string Theme = "BaseLight";
        public string Accent = "Cobalt";

        public bool FileExists(string filename)
        {
            return true;
        }

        public string[] ReadAllLines(string filename)
        {
            if (filename == "CIDER.cfg" && multiple == true && hasThemeing == true)
            {
                string[] vs = { "KEY:x.key", $"THEME:{Theme}", $"ACCENT:{Accent}" };
                return vs;
            }
            else if (filename == "CIDER.cfg" && multiple == false && hasThemeing == true)
            {
                string[] vs = { $"THEME:{Theme}", $"ACCENT:{Accent}" };
                return vs;
            }
            else if(hasThemeing == false && filename == "CIDER.cfg")
            {
                string[] vs = { "KEY:x.key" };
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
