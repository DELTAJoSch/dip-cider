using CIDER.ViewModels;
using NSubstitute;
using NUnit.Framework;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.UnitTests.ViewModelUnitTests
{
    [TestFixture]
    public class HeightViewModelUnitTests
    {
        [Test]
        public void HeightViewModel_SliderValueChanged_ChangesToCorrectValues()
        {
            var data = Factories.GetHeightData();
            var model = new HeightViewModel(data);

            model.slValueChanged(2);

            Assert.AreEqual(30f, model.HeightValL);
            Assert.AreEqual(0f, model.HeightValR);
            Assert.AreEqual("Height: 30 ft", model.HeightText);
        }
    }
}
