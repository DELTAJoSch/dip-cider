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
using CIDER;
using NUnit.Framework;
using System;

namespace CIDER.UnitTests
{
    public class SystemTimeUnitTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void SystemTime_WhenSet_NowReturnsFakeTime()
        {
            SystemTime time = Generate();
            DateTime testTime = new DateTime(1990, 1, 1);
            DateTime assertTime = new DateTime();

            SystemTime.Set(testTime);

            assertTime = time.Now;

            Assert.AreEqual(testTime, assertTime);
        }

        [Test]
        public void SystemTime_WhenSetThenReset_ReturnsDifferentTime()
        {
            SystemTime time = Generate();
            DateTime testTime = new DateTime(1990, 1, 1);
            DateTime assertTime = new DateTime();

            SystemTime.Set(testTime);
            SystemTime.Reset();

            assertTime = time.Now;

            Assert.AreNotEqual(testTime, assertTime);

        }

        private SystemTime Generate()
        {
            SystemTime systemTime = new SystemTime();
            return systemTime;
        }
    }
}