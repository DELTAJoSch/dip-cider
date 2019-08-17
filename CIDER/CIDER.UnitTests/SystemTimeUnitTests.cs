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