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
    public class VelocityTimedViewModelUnitTests
    {
        [Test]
        public void VelocityTimedViewModel_SliderValueChanged_ChangesToCorrectValues()
        {
            var data = Factories.GetVelocityData();
            var model = new VelocityTimedViewModel(data);

            model.SliderValueChanged(1);

            Assert.AreEqual("Velocity: 20 kt", model.Text);
            Assert.AreEqual(0f, model.RVal);
            Assert.AreEqual(20f, model.LVal);
        }
    }
}
