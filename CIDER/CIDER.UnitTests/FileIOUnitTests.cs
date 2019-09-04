using CIDER.LoadIO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CIDER.UnitTests
{
    public class FileIOUnitTests
    {
        [Test]
        public void ReadCSV_WhenCalled_CorrectDate()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();

            iO.ReadCSV(data, "", reader);

            Assert.AreEqual(new DateTime(2019, 08, 26), data.RouteDate);
        }

        [Test]
        public void ReadCSV_WhenCalled_CorrectRouteName()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();

            iO.ReadCSV(data, "", reader);

            Assert.AreEqual("Test", data.RouteName);
        }

        [Test]
        public void ReadCSV_WhenCalled_CorrectStartTime()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();

            iO.ReadCSV(data, "", reader);

            Assert.AreEqual(new DateTime(2019, 08, 26), data.RouteDate);
        }

        [Test]
        public void ReadCSV_WhenCalled_CorrectPressure()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();

            iO.ReadCSV(data, "", reader);

            List<float> vs = new List<float>();
            vs.Add(509);
          
            Assert.AreEqual(vs, data.Pressure);
        }

        [Test, Ignore("broken")]
        public void ReadCSV_WhenCalled_CorrectAcceleration()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();

            iO.ReadCSV(data, "", reader);

            List<float> x = new List<float>();
            List<float> y = new List<float>();
            List<float> z = new List<float>();

            x.Add(17);
            y.Add(7);
            z.Add(38);

            iO.ReadCSV(data, "", reader);

            
            Assert.AreEqual(y, data.YAcceleration);
            Assert.AreEqual(z, data.ZAcceleration);
            Assert.AreEqual(x, data.XAcceleration);
        }

        [Test]
        public void ReadCSV_WhenCalled_CorrectVelocity()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();

            List<float> x = new List<float>();
            List<float> y = new List<float>();
            List<float> z = new List<float>();

            x.Add(66);
            y.Add(61);
            z.Add(98);

            iO.ReadCSV(data, "", reader);

            Assert.AreEqual(x, data.XVelocity);
            Assert.AreEqual(y, data.YVelocity);
            Assert.AreEqual(z, data.ZVelocity);
        }

        [Test]
        public void ReadNmea_WhenCalled_CorrectTime()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();

            iO.ReadNmea(data, "", reader);

            DateTime dateTime = DateTime.Today;
            dateTime = dateTime.AddHours(09);
            dateTime = dateTime.AddMinutes(11);
            dateTime = dateTime.AddSeconds(24);

            Assert.AreEqual(dateTime, data.RouteStartTime);
            Assert.AreEqual(dateTime, data.RouteEndTime);
        }

        [Test]
        public void ReadNmea_WhenCalled_CorrectSattelitesInUse()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();

            iO.ReadNmea(data, "", reader);

            Assert.AreEqual(12, data.AverageSattelitesInUse);
        }
    }

    public class FakeReader : IRead
    {
        public string[] ReadLinesCsv(string path)
        {
            string[] csv = {"Inf;26.08.2019;Test",
                            "Dat;66;61;98;17;7;38;509"};

            return csv;
        }

        public string[] ReadLinesNmea(string path)
        {
            string[] nmea = {"$GPGGA,091124.840,4813.990,N,01411.199,E,1,12,1.0,0.0,M,0.0,M,,*67"};
            return nmea;
        }
    }
}
