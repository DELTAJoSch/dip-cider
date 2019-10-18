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

        /// <summary>
        /// This is the constructor for the LicensesViewModel
        /// </summary>
        public LicensesViewModel()
        {
            CommandAcceptEnabled = false;
            _checkboxStateChangedCommand = new DelegateCommand(checkboxStateChanged);

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

        }

        private void checkboxStateChanged(object obj)
        {
            CommandAcceptEnabled = true;
        }

        /// <summary>
        /// This is the command handler for the checkbox state.
        /// </summary>
        public ICommand CheckboxStateChanged => _checkboxStateChangedCommand;

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
