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
