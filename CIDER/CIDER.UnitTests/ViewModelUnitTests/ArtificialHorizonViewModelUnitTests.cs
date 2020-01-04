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
