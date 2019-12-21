using CIDER.LoadIO;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.UnitTests
{
    public class FileIOUnitTests
    {
        [Test]
        public async Task ReadCSV_WhenCalled_CorrectDate()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();
            var read = NSubstitute.Substitute.For<IReader>();

            await iO.ReadCSV(data, "", reader, Factories.GetMainWindowViewModelStub());

            Assert.AreEqual(new DateTime(2019, 08, 26), data.RouteDate);
        }

        [Test]
        public async Task ReadCSV_WhenCalled_CorrectRouteName()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();
            var read = NSubstitute.Substitute.For<IReader>();

            await iO.ReadCSV(data, "", reader, Factories.GetMainWindowViewModelStub());

            Assert.AreEqual("Test", data.RouteName);
        }

        [Test]
        public async Task ReadCSV_WhenCalled_CorrectStartTime()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();
            var read = NSubstitute.Substitute.For<IReader>();

            await iO.ReadCSV(data, "", reader, Factories.GetMainWindowViewModelStub());

            Assert.AreEqual(new DateTime(2019, 08, 26), data.RouteDate);
        }

        [Test]
        public async Task ReadCSV_WhenCalled_CorrectPressure()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();
            var read = NSubstitute.Substitute.For<IReader>();

            await iO.ReadCSV(data, "", reader, Factories.GetMainWindowViewModelStub());

            List<float> vs = new List<float>();
            vs.Add(10f);
          
            Assert.AreEqual(vs, data.Pressure);
        }

        [Test]
        public async Task ReadCSV_WhenCalled_CorrectHeight()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();
            var read = NSubstitute.Substitute.For<IReader>();

            await iO.ReadCSV(data, "", reader, Factories.GetMainWindowViewModelStub());

            List<float> vs = new List<float>();
            vs.Add(509);

            Assert.AreEqual(vs, data.Height);
        }

        [Test]
        public async Task ReadCSV_WhenCalled_CorrectAcceleration()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();
            var read = NSubstitute.Substitute.For<IReader>();

            List<float> x = new List<float>();
            List<float> y = new List<float>();
            List<float> z = new List<float>();

            x.Add(66);
            y.Add(61);
            z.Add(98);

            await iO.ReadCSV(data, "", reader, Factories.GetMainWindowViewModelStub());
            
            Assert.AreEqual(y, data.YAcceleration);
            Assert.AreEqual(z, data.ZAcceleration);
            Assert.AreEqual(x, data.XAcceleration);
        }

        [Test]
        public async Task ReadCSV_WhenCalled_CorrectVelocity()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();
            var read = NSubstitute.Substitute.For<IReader>();

            List<float> x = new List<float>();
            List<float> y = new List<float>();
            List<float> z = new List<float>();

            x.Add(66);
            y.Add(61);
            z.Add(98);
            
            await iO.ReadNmea(data, "", reader, Factories.GetMainWindowViewModelStub());
            

            
        }

        [Test]
        public async Task ReadNmea_WhenCalled_CorrectTime()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();
            var read = NSubstitute.Substitute.For<IReader>();

            await iO.ReadNmea(data, "", reader, Factories.GetMainWindowViewModelStub());

            DateTime dateTime = DateTime.Today;
            dateTime = dateTime.AddHours(09);
            dateTime = dateTime.AddMinutes(11);
            dateTime = dateTime.AddSeconds(24);

            Assert.AreEqual(dateTime, data.RouteStartTime);
            Assert.AreEqual(dateTime, data.RouteEndTime);
        }

        [Test]
        public async Task ReadNmea_WhenCalled_CorrectSattelitesInUse()
        {
            FakeReader reader = new FakeReader();
            FileIO iO = new FileIO();
            DataProvider data = new DataProvider();
            

            await iO.ReadNmea(data, "", reader, Factories.GetMainWindowViewModelStub());

            Assert.AreEqual(12, data.AverageSattelitesInUse);
        }
    }

    public class FakeReader : IRead
    {
        public string[] ReadLinesCsv(string path)
        {
            string[] csv = {"Inf;26.08.2019;Test",
                            "Dat;66;61;98;17;7;38;509;10"};

            return csv;
        }

        public string[] ReadLinesNmea(string path)
        {
            string[] nmea = {"$GPGGA,091124.840,4813.990,N,01411.199,E,1,12,1.0,0.0,M,0.0,M,,*67",
                             "$GPRMC,200842.897,A,5121.3771,N,00747.3832,E,531.49,163.34,170919,,*0C"};
            return nmea;
        }
    }
}
