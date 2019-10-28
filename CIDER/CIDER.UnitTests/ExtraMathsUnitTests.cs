using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.UnitTests
{
    public class ExtraMathsUnitTests
    {
        [Test]
        public void ExtraMaths_DegToRad_ReturnsCorrectValue()
        {
            Assert.AreEqual(1.57, Math.Round(ExtraMath.DegToRad(90),2));
        }

        [Test]
        public void ExtraMaths_RadToDeg_ReturnsCorrectValue()
        {
            Assert.AreEqual(180, ExtraMath.RadToDeg(Math.PI));
        }

        [Test]
        public void ExtraMaths_CalculateAngle_ReturnsCorrectAngle()
        {
            var val = ExtraMath.CalculateAngle(10, 10, 10);

            Assert.AreEqual(35.264389f, val.Item1);
            Assert.AreEqual(35.264389f, val.Item2);
            Assert.AreEqual(35.264389f, val.Item3);
        }
    }
}
