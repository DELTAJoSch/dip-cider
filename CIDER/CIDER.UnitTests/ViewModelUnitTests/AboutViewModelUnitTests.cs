using CIDER.ViewModels;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CIDER.UnitTests.ViewModelUnitTests
{
    [TestFixture]
    class AboutViewModelUnitTests
    {
        [Test]
        public void AboutViewModel_MailtoClick_CallsProcessStarter()
        {
            var handler = Substitute.For<TestStarter>();
            AboutViewModel about = new AboutViewModel(handler, new KeyManager(new DataProvider(), new FileReader()), new Licenser());

            about.RequestNavigateCommand.Execute(this);

            handler.ReceivedWithAnyArgs().Start(default);
        }

        [Test]
        public void AboutViewModel_SetAPIKey_CallsKeyManager()
        {
            var keymanager = Substitute.For<TestKeyManager>();
            AboutViewModel about = new AboutViewModel(new TestStarter(), keymanager, new Licenser());

            about.SetApiKeyCommand.Execute(this);

            keymanager.ReceivedWithAnyArgs().Put();

        }

        [Test]
        public void AboutViewModel_ViewLicense_CallsLicenseShower()
        {
            var licenser = Substitute.For<TestLicenser>();
            AboutViewModel about = new AboutViewModel(new TestStarter(), new KeyManager(new DataProvider(), new FileReader()), licenser);

            about.ViewLicenseCommand.Execute(this);

            licenser.ReceivedWithAnyArgs().Show();

        }

        [TestCase("AboutText")]
        [TestCase("InfoText")]
        public void AboutViewModel_ChangeAboutText_PropertyUpdated(string methodName)
        {
            var handler = Substitute.For<TestStarter>();
            bool wasCalled = false;
            AboutViewModel about = new AboutViewModel(handler, new KeyManager(new DataProvider(), new FileReader()), new Licenser());
            about.PropertyChanged += (o, e) => { wasCalled = true; };

            if(methodName == "AboutText")
            {
                about.AboutText = "Hello";
            }else if(methodName == "InfoText")
            {
                about.InfoText = "Hello";
            }
            else
            {
                throw new AssertionException("Unknown TestCase");
            }

            Assert.IsTrue(wasCalled);
        }
    }

    public class TestStarter : IProcessStarter
    {
        public void Start(ProcessStartInfo info)
        {
            
        }
    }

    public class TestLicenser : ILicense
    {
        public void Show()
        {
        
        }
    }

    public class TestKeyManager : IKeyManager
    {
        public bool Fetch()
        {
            return true;
        }

        public bool Put()
        {
            return true;
        }
    }
}
