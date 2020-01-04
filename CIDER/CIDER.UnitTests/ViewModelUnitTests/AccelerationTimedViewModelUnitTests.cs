/* Copyright (C) 2020  Johannes Schiemer 
	This program is free software: you can redistribute it and/or modify 
	it under the terms of the GNU General Public License as published by 
	the Free Software Foundation, either version 3 of the License, or 
	(at your option) any later version. 
	This program is distributed in the hope that it will be useful, 
	but WITHOUT ANY WARRANTY; without even the implied warranty of 
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
	GNU General Public License for more details. 
	You should have received a copy of the GNU General Public License 
	along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/
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
