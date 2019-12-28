using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.UnitTests
{
    public static class Factories
    {
        public static ViewModels.MainWindowViewModel GetMainWindowViewModelStub(bool license = true, bool hasKey = true)
        {
            var read = new FakeLicenseReader();
            read.state = license;

            return new ViewModels.MainWindowViewModel(new KeyManager(new DataProvider(), new FakeKeyManagerReader(hasKey)), new DataProvider(), read, true);
        }

        public static DataProvider GetRouteData()
        {
            var data = new DataProvider();

            data.IsValidRoute = true;

            data.APIKey = "FRANZ-OTTO";

            data.Route.Add(new Microsoft.Maps.MapControl.WPF.Location(0, 0));
            data.Route.Add(new Microsoft.Maps.MapControl.WPF.Location(0, 1));
            data.Route.Add(new Microsoft.Maps.MapControl.WPF.Location(1, 1));
            data.Route.Add(new Microsoft.Maps.MapControl.WPF.Location(1, 2));
            data.Route.Add(new Microsoft.Maps.MapControl.WPF.Location(1, 3));
            data.Route.Add(new Microsoft.Maps.MapControl.WPF.Location(1, 4));

            return data;
        }

        public static DataProvider GetHeightData()
        {
            var data = new DataProvider();

            data.IsValidRoute = true;

            data.Height.Add(10);
            data.Height.Add(20);
            data.Height.Add(30);

            return data;
        }

        public static DataProvider GetVelocityData()
        {
            var data = new DataProvider();

            data.IsValidRoute = true;

            data.Velocity.Add(10);
            data.Velocity.Add(20);
            data.Velocity.Add(30);

            data.DataPointsVelocity = 3;

            return data;
        }

        public static DataProvider GetAccelerationData()
        {
            var data = new DataProvider();

            data.IsValidRoute = true;

            data.XAcceleration.Add(5);
            data.XAcceleration.Add(10);
            data.XAcceleration.Add(15);

            data.YAcceleration.Add(5);
            data.YAcceleration.Add(10);
            data.YAcceleration.Add(15);

            data.ZAcceleration.Add(5);
            data.ZAcceleration.Add(10);
            data.ZAcceleration.Add(15);

            data.DataPointsAcceleration = 3;

            return data;
        }

        public static DataProvider GetAngleData()
        {
            var data = new DataProvider();

            data.IsValidRoute = true;

            data.Roll.Add(5);
            data.Roll.Add(10);
            data.Roll.Add(15);

            data.Yaw.Add(5);
            data.Yaw.Add(10);
            data.Yaw.Add(15);

            data.Pitch.Add(5);
            data.Pitch.Add(10);
            data.Pitch.Add(15);

            data.DataPointsAngle = 3;

            return data;
        }

        public static DataProvider GetArtificialHorizonData()
        {
            var data = new DataProvider();

            data.IsValidRoute = true;

            data.XAcceleration.Add(5);
            data.XAcceleration.Add(10);
            data.XAcceleration.Add(15);

            data.YAcceleration.Add(5);
            data.YAcceleration.Add(10);
            data.YAcceleration.Add(15);

            data.ZAcceleration.Add(5);
            data.ZAcceleration.Add(10);
            data.ZAcceleration.Add(15);

            data.DataPointsAcceleration = 3;

            data.Roll.Add(5);
            data.Roll.Add(10);
            data.Roll.Add(15);

            data.Yaw.Add(5);
            data.Yaw.Add(10);
            data.Yaw.Add(15);

            data.Pitch.Add(5);
            data.Pitch.Add(10);
            data.Pitch.Add(15);

            data.DataPointsAngle = 3;

            data.Velocity.Add(10);
            data.Velocity.Add(20);
            data.Velocity.Add(30);

            data.DataPointsVelocity = 3;

            data.Height.Add(10);
            data.Height.Add(20);
            data.Height.Add(30);
            data.Height.Add(40);

            return data;
        }
    }
}
