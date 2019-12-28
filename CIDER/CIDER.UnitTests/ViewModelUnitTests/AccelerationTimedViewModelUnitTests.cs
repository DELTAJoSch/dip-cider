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
    public class AccelerationTimedViewModelUnitTests
    {
        [Test]
        public void AccelerationTimedViewModel_SliderValueChanged_ChangesXValuesCorrectly()
        {
            var data = Factories.GetAccelerationData();
            var model = new AccelerationTimedViewModel(data);

            model.SliderValueChanged(2);

            Assert.AreEqual(15f, model.LValFB);
            Assert.AreEqual(0f, model.RValFB);
            Assert.AreEqual("Forwards/Backwards: 15 m/s^2", model.FBText);
        }

        [Test]
        public void AccelerationTimedViewModel_SliderValueChanged_ChangesYValuesCorrectly()
        {
            var data = Factories.GetAccelerationData();
            var model = new AccelerationTimedViewModel(data);

            model.SliderValueChanged(2);

            Assert.AreEqual(15f, model.LValUD);
            Assert.AreEqual(0f, model.RValUD);
            Assert.AreEqual("Up/Down: 15 m/s^2", model.UDText);
        }

        [Test]
        public void AccelerationTimedViewModel_SliderValueChanged_ChangesZValuesCorrectly()
        {
            var data = Factories.GetAccelerationData();
            var model = new AccelerationTimedViewModel(data);

            model.SliderValueChanged(2);

            Assert.AreEqual(15f, model.LValLR);
            Assert.AreEqual(0f, model.RValLR);
            Assert.AreEqual("Left/Right: 15 m/s^2", model.LRText);
        }
    }
}
