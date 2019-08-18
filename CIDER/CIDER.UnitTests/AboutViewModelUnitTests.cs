using CIDER.ViewModels;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace CIDER.UnitTests
{
    class AboutViewModelUnitTests
    {
        [Test]
        public void AboutViewModel_MailtoClick_CallsProcessStarter()
        {
            var handler = Substitute.For<TestStarter>();
            AboutViewModel about = new AboutViewModel(handler);

            about.RequestNavigate.Execute(this);

            handler.ReceivedWithAnyArgs().Start(default);
        }
    }

    public class TestStarter : ProcessStarter
    {
        public void Start(ProcessStartInfo info)
        {
            
        }
    }
}
