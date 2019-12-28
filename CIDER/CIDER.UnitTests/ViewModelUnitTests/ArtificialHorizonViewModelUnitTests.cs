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
    public class ArtificialHorizonViewModelUnitTests
    {
        [Test]
        public void ArtificialHorizonViewModel_SliderChanged_CorrectValues()
        {
            var data = Factories.GetArtificialHorizonData();
            var model = new ArtificialHorizonViewModel(data);

            model.SliderValueChanged(2);

            Assert.AreEqual(15f, model.Roll);
            Assert.AreEqual(15f, model.Yaw);
            Assert.AreEqual(15f, model.Pitch);
            Assert.AreEqual(30f, model.Velocity);
            Assert.AreEqual(10d, model.ClimbVelocity);
        }
    }
}
