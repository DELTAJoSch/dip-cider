using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using CIDER;
using CIDER.MVVMBase;
using NUnit.Framework;
using NSubstitute;

namespace Tests
{
    class ViewModelBaseUnitTests
    {
        [Test]
        public void ICommand_WhenCalled_CallsFucntion()
        {
            bool wasCalled = false;

            DelegateCommand delegateCommand = new DelegateCommand((o) => wasCalled = true);

            delegateCommand.Execute(this);

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ICommand_WhenCanExecuteCalled_CallsCanExecuteIsCalled()
        {
            bool wasCalled = false;

            DelegateCommand delegateCommand = new DelegateCommand((o) => { }, (o) => { wasCalled = true; return true; });

            delegateCommand.CanExecute(this);

            Assert.IsTrue(wasCalled);
        }
    }
}
