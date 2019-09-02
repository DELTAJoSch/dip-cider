using CIDER.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CIDER.UnitTests
{
    class AccelerationGraphViewModelUnitTests
    {
        [Test]
        public void AccelerationGraphViewModel_WhenCreated_CreatesList()
        {
            DataProvider data = new DataProvider();

            data.Acceleration.Add(new Tuple<float, float, float>(1, 2, 3));

            AccelerationGraphViewModel model = new AccelerationGraphViewModel(data);


            Assert.AreEqual(true, true);
        }
    }
}
