using System;
using System.Collections.Generic;
using System.Text;
using NUnit;
using CIDER;
using CIDER.MVVMBase;
using NUnit.Framework;
using NSubstitute;
using System.ComponentModel;

namespace CIDER.UnitTests
{
    class DelegateCommandUnitTests
    {
        [Test]
        public void DelegateCommand_WhenCalled_CallsFucntion()
        {
            bool wasCalled = false;

            DelegateCommand delegateCommand = new DelegateCommand((o) => wasCalled = true);

            delegateCommand.Execute(this);

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void DelegateCommand_WhenCanExecuteCalled_CallsCanExecuteIsCalled()
        {
            bool wasCalled = false;

            DelegateCommand delegateCommand = new DelegateCommand((o) => { }, (o) => { wasCalled = true; return true; });

            delegateCommand.CanExecute(this);

            Assert.IsTrue(wasCalled);
        }
    }

    class ViewModelBaseUnitTests
    {
        [Test]
        public void ViewModelBase_SetPropertyNewData_EventCalled()
        {
            ViewModelBaseTestClass modelBase = new ViewModelBaseTestClass();
            bool wasCalled = false;
            modelBase.PropertyChanged += (o, e) => { wasCalled = true; };
            string oldString = "1";
            string newString = "2";

            modelBase.ExposedSet(ref oldString, newString);

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ViewModelBase_SetPropertyNewData_isTrue()
        {
            ViewModelBaseTestClass modelBase = new ViewModelBaseTestClass();
            bool wasCalled = false;
            string oldString = "1";
            string newString = "2";

            wasCalled = modelBase.ExposedSet(ref oldString, newString);

            Assert.IsTrue(wasCalled);
        }

        [Test]
        public void ViewModelBase_SetPropertySameData_isFalse()
        {
            ViewModelBaseTestClass modelBase = new ViewModelBaseTestClass();
            bool wasCalled = true;
            string oldString = "1";
            string newString = "1";

            wasCalled = modelBase.ExposedSet(ref oldString, newString);

            Assert.IsFalse(wasCalled);
        }
    }

    class ViewModelBaseTestClass : ViewModelBase
    {
        public bool ExposedSet<T>(ref T field, T value)
        {
            return SetProperty(ref field, value);
        }
    }
}
