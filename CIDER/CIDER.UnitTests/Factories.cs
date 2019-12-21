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

            data.Route.Add(new Microsoft.Maps.MapControl.WPF.Location(0, 0));
            data.Route.Add(new Microsoft.Maps.MapControl.WPF.Location(0, 1));
            data.Route.Add(new Microsoft.Maps.MapControl.WPF.Location(1, 1));
            data.Route.Add(new Microsoft.Maps.MapControl.WPF.Location(1, 2));
            data.Route.Add(new Microsoft.Maps.MapControl.WPF.Location(1, 3));
            data.Route.Add(new Microsoft.Maps.MapControl.WPF.Location(1, 4));

            return data;
        }
    }
}
