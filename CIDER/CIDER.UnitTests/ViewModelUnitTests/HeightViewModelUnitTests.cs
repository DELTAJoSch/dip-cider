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
