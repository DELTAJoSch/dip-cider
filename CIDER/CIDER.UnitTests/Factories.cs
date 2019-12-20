using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.UnitTests
{
    public static class Factories
    {
        public static ViewModels.MainWindowViewModel GetMainWindowViewModelStub()
        {
            var read = NSubstitute.Substitute.For<IReader>();
            return new ViewModels.MainWindowViewModel(new KeyManager(new DataProvider(), new FakeKeyManagerReader()), new DataProvider(), read, true);
        }
    }
}
