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
    public class AngleTimedViewModelUnitTests
    {
        [Test]
        public void AngleTimedViewModel_SliderValueChanged_PitchChangesCorrectly()
        {
            var data = Factories.GetAngleData();
            var model = new AngleTimedViewModel(data);

            model.SliderValueChanged(1);

            Assert.AreEqual(10f, model.LValPitch);
            Assert.AreEqual(0f, model.RValPitch);
            Assert.AreEqual("Pitch: 10°", model.PitchText);
        }

        [Test]
        public void AngleTimedViewModel_SliderValueChanged_RollChangesCorrectly()
        {
            var data = Factories.GetAngleData();
            var model = new AngleTimedViewModel(data);

            model.SliderValueChanged(1);

            Assert.AreEqual(10f, model.LValRoll);
            Assert.AreEqual(0f, model.RValRoll);
            Assert.AreEqual("Roll: 10°", model.RollText);
        }

        [Test]
        public void AngleTimedViewModel_SliderValueChanged_YawChangesCorrectly()
        {
            var data = Factories.GetAngleData();
            var model = new AngleTimedViewModel(data);

            model.SliderValueChanged(1);

            Assert.AreEqual(10f, model.LValYaw);
            Assert.AreEqual(0f, model.RValYaw);
            Assert.AreEqual("Yaw: 10°", model.YawText);
        }
    }
}
