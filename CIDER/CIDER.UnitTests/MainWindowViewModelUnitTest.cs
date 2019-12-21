using CIDER.ViewModels;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDER.UnitTests
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class MainWindowViewModelUnitTest
    {
        private bool Called = false;

        [Test]
        public void MainWindowViewModel_ButtonStatesLicenseAccepted_ChangeCorrectly()
        {
            var model = Factories.GetMainWindowViewModelStub();

            model.ButtonState(true);

            Assert.IsTrue(model.ButtonEnabled);
        }

        [Test]
        public void MainWindowViewModel_ButtonStatesLicenseNotAccepted_ChangeCorrectly()
        {
            var model = Factories.GetMainWindowViewModelStub(false);

            model.ButtonState(true);

            Assert.IsFalse(model.ButtonEnabled);
        }

        [Test]
        public void MainWindowViewModel_NavigateToAbout_NavigatesCorrectly()
        {
            var keymanager = Substitute.For<TestKeyManager>();
            MainWindowViewModel main = new MainWindowViewModel(keymanager, new DataProvider(), new FakeLicenseReader(), true);

            main.OnFrameChangeEvent += Main_OnFrameChangeEvent;

            main.ChangeToAboutCommand.Execute(this);

            main.OnFrameChangeEvent -= Main_OnFrameChangeEvent;

            Assert.IsTrue(Called);
        }
        [Test]
        public void MainWindowViewModel_NavigateToAccelerationGraph_NavigatesCorrectly()
        {
            var keymanager = Substitute.For<TestKeyManager>();
            MainWindowViewModel main = new MainWindowViewModel(keymanager, new DataProvider(), new FakeLicenseReader(), true);

            main.OnFrameChangeEvent += Main_OnFrameChangeEvent;

            main.ChangeToAccelerationGraphCommand.Execute(this);

            main.OnFrameChangeEvent -= Main_OnFrameChangeEvent;

            Assert.IsTrue(Called);
        }
        
        [Test]
        public void MainWindowViewModel_NavigateToAccelerationTimed_NavigatesCorrectly()
        {
            var keymanager = Substitute.For<TestKeyManager>();
            MainWindowViewModel main = new MainWindowViewModel(keymanager, new DataProvider(), new FakeLicenseReader(), true);

            main.OnFrameChangeEvent += Main_OnFrameChangeEvent;

            main.ChangeToAccelerationTimedCommand.Execute(this);

            main.OnFrameChangeEvent -= Main_OnFrameChangeEvent;

            Assert.IsTrue(Called);
        }
        
        [Test]
        public void MainWindowViewModel_NavigateToAngleGraph_NavigatesCorrectly()
        {
            var keymanager = Substitute.For<TestKeyManager>();
            MainWindowViewModel main = new MainWindowViewModel(keymanager, new DataProvider(), new FakeLicenseReader(), true);

            main.OnFrameChangeEvent += Main_OnFrameChangeEvent;

            main.ChangeToAngleGraphCommand.Execute(this);

            main.OnFrameChangeEvent -= Main_OnFrameChangeEvent;

            Assert.IsTrue(Called);
        }
        
        [Test]
        public void MainWindowViewModel_NavigateToAngleTimed_NavigatesCorrectly()
        {
            var keymanager = Substitute.For<TestKeyManager>();
            MainWindowViewModel main = new MainWindowViewModel(keymanager, new DataProvider(), new FakeLicenseReader(), true);

            main.OnFrameChangeEvent += Main_OnFrameChangeEvent;

            main.ChangeToAngleTimedCommand.Execute(this);

            main.OnFrameChangeEvent -= Main_OnFrameChangeEvent;

            Assert.IsTrue(Called);
        }
        
        [Test]
        public void MainWindowViewModel_NavigateToArtificialHorizon_NavigatesCorrectly()
        {
            var keymanager = Substitute.For<TestKeyManager>();
            MainWindowViewModel main = new MainWindowViewModel(keymanager, new DataProvider(), new FakeLicenseReader(), true);

            main.OnFrameChangeEvent += Main_OnFrameChangeEvent;

            main.ChangeToHorizonCommand.Execute(this);

            main.OnFrameChangeEvent -= Main_OnFrameChangeEvent;

            Assert.IsTrue(Called);
        }
        
        [Test]
        public void MainWindowViewModel_NavigateToHeight_NavigatesCorrectly()
        {
            var keymanager = Substitute.For<TestKeyManager>();
            MainWindowViewModel main = new MainWindowViewModel(keymanager, new DataProvider(), new FakeLicenseReader(), true);

            main.OnFrameChangeEvent += Main_OnFrameChangeEvent;

            main.ChangeToHeightCommand.Execute(this);

            main.OnFrameChangeEvent -= Main_OnFrameChangeEvent;

            Assert.IsTrue(Called);
        }
        
        [Test]
        public void MainWindowViewModel_NavigateToVelocityGraph_NavigatesCorrectly()
        {
            var keymanager = Substitute.For<TestKeyManager>();
            MainWindowViewModel main = new MainWindowViewModel(keymanager, new DataProvider(), new FakeLicenseReader(), true);

            main.OnFrameChangeEvent += Main_OnFrameChangeEvent;

            main.ChangeToVelocityGraphCommand.Execute(this);

            main.OnFrameChangeEvent -= Main_OnFrameChangeEvent;

            Assert.IsTrue(Called);
        }
        
        [Test]
        public void MainWindowViewModel_NavigateToVelocityTimed_NavigatesCorrectly()
        {
            var keymanager = Substitute.For<TestKeyManager>();
            MainWindowViewModel main = new MainWindowViewModel(keymanager, new DataProvider(), new FakeLicenseReader(), true);

            main.OnFrameChangeEvent += Main_OnFrameChangeEvent;

            main.ChangeToVelocityTimedCommand.Execute(this);

            main.OnFrameChangeEvent -= Main_OnFrameChangeEvent;

            Assert.IsTrue(Called);
        }

        [Test]
        public void MainWindowViewModel_NavigateToMapRoute_NavigatesCorrectly()
        {
            var keymanager = Substitute.For<TestKeyManager>();
            MainWindowViewModel main = new MainWindowViewModel(keymanager, new DataProvider(), new FakeLicenseReader(), true);

            main.OnFrameChangeEvent += Main_OnFrameChangeEvent;

            main.ChangeToMapRouteCommand.Execute(this);

            main.OnFrameChangeEvent -= Main_OnFrameChangeEvent;

            Assert.IsTrue(Called);
        }

        [Test]
        public void MainWindowViewModel_NavigateToMapTimed_NavigatesCorrectly()
        {
            var keymanager = Substitute.For<TestKeyManager>();
            MainWindowViewModel main = new MainWindowViewModel(keymanager, new DataProvider(), new FakeLicenseReader(), true);

            main.OnFrameChangeEvent += Main_OnFrameChangeEvent;

            main.ChangeToMapTimedCommand.Execute(this);

            main.OnFrameChangeEvent -= Main_OnFrameChangeEvent;

            Assert.IsTrue(Called);
        }

        [Test]
        public void MainWindowViewModel_NavigateToLoad_NavigatesCorrectly()
        {
            var keymanager = Substitute.For<TestKeyManager>();
            MainWindowViewModel main = new MainWindowViewModel(keymanager, new DataProvider(), new FakeLicenseReader(), true);

            main.OnFrameChangeEvent += Main_OnFrameChangeEvent;

            main.ChangeToLoadCommand.Execute(this);

            main.OnFrameChangeEvent -= Main_OnFrameChangeEvent;

            Assert.IsTrue(Called);
        }

        [TearDown]
        public void Teardown()
        {
            Called = false;
        }

        private void Main_OnFrameChangeEvent(object sender, EventArgs e)
        {
            Called = true;
        }
    }
}
