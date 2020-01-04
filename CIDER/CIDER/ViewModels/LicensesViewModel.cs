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
using CIDER.MVVMBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CIDER.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the License Window.
    /// The purpose of this class is to handle all the interaction logic for the License Window.
    /// </summary>
    public class LicensesViewModel: ViewModelBase
    {
        private readonly DelegateCommand _checkboxStateChangedCommand;
        private bool _commandAcceptEnabled;
        private string _licensesText;
        private LicenseWriter writer;

        /// <summary>
        /// This is the constructor for the LicensesViewModel
        /// </summary>
        public LicensesViewModel(LicenseWriter Writer)
        {
            CommandAcceptEnabled = false;
            _checkboxStateChangedCommand = new DelegateCommand(checkboxStateChanged);

            writer = Writer;

            Parallel.ForEach(LicenseManager.Licenses, s =>
            {
                LicensesText += s;
            });
        }

        /// <summary>
        /// This function is called when the accept button is pressed, just before the window closes.
        /// </summary>
        public void SaveAcceptAgreement()
        {
            LicenseManager.LicensesAccepted = true;
            writer.WriteAgreementState(true);
            LicenseHolder.AcceptedLicense = true;
        }

        private void checkboxStateChanged(object obj)
        {
            CommandAcceptEnabled = !CommandAcceptEnabled;
        }

        /// <summary>
        /// This is the command handler for the checkbox state.
        /// </summary>
        public ICommand CheckboxStateChangedCommand => _checkboxStateChangedCommand;

        /// <summary>
        /// This is the Data Binding for the license text (textbox)
        /// </summary>
        public string LicensesText { get { return _licensesText; } set { SetProperty(ref _licensesText, value); } }

        /// <summary>
        /// This is the Data Binding for the enabled state of the accept button
        /// </summary>
        public bool CommandAcceptEnabled { get { return _commandAcceptEnabled; } set { SetProperty(ref _commandAcceptEnabled, value); } }
    }
}
