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
    }
}
