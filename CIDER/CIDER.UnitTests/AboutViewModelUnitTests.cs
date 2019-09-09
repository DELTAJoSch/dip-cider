using CIDER.ViewModels;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CIDER.UnitTests
{
    [TestFixture]
    class AboutViewModelUnitTests
    {
        [Test]
        public void AboutViewModel_MailtoClick_CallsProcessStarter()
        {
            var handler = Substitute.For<TestStarter>();
            AboutViewModel about = new AboutViewModel(handler, new KeyManager(new DataProvider(), new KeyManagerReader()));

            about.RequestNavigate.Execute(this);

            handler.ReceivedWithAnyArgs().Start(default);
        }

        [TestCase("AboutText")]
        [TestCase("InfoText")]
        public void AboutViewModel_ChangeAboutText_PropertyUpdated(string methodName)
        {
            var handler = Substitute.For<TestStarter>();
            bool wasCalled = false;
            AboutViewModel about = new AboutViewModel(handler, new KeyManager(new DataProvider(), new KeyManagerReader()));
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
}
