/* Copyright (C) 2020  Johannes Schiemer 
	This program is free software: you can redistribute it and/or modify 
	it under the terms of the GNU General Public License as published by 
	the Free Software Foundation, either version 3 of the License, or 
	(at your option) any later version. 
	This program is distributed in the hope that it will be useful, 
	but WITHOUT ANY WARRANTY; without even the implied warranty of 
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
	GNU General Public License for more details. 
	You should have received a copy of the GNU General Public License 
	along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/
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
