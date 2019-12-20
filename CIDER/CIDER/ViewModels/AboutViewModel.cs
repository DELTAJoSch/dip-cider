using CIDER.MVVMBase;
using System.Diagnostics;
using System.Windows.Input;

namespace CIDER.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the About View
    /// The constructor takes a ProcessStarter Interface - this is so a seam for unit testing exists
    /// On init it also sets the text in the about and information TextBlocks. They can be changed afterwards, but this is not needed in normal operation
    /// When the button in the view is pressed, the view model responds to it by calling the function fromn the processStarter interface
    /// </summary>
    public class AboutViewModel : ViewModelBase
    {
        private readonly DelegateCommand _mailtoClickCommand;
        private readonly DelegateCommand _setApiKeyCommand;
        private readonly DelegateCommand _changeThemeCommand;
        private readonly DelegateCommand _viewLicenseCommand;
        private readonly IProcessStarter _handler;
        private IKeyManager _manager;
        private ILicense _license;

        /// <summary>
        /// This is the constructor for the About Viewmodel
        /// </summary>
        /// <param name="starter">An object implementing the IProcessStarter interface</param>
        /// <param name="manager">A keymanager object</param>
        /// <param name="license">An object implementing the ILicense interface</param>
        public AboutViewModel(IProcessStarter starter, IKeyManager manager, ILicense license)
        {
            _mailtoClickCommand = new DelegateCommand(mailto);
            _setApiKeyCommand = new DelegateCommand(setApiKey);
            _changeThemeCommand = new DelegateCommand(ChangeTheme);
            _viewLicenseCommand = new DelegateCommand(ViewLicense);

            AboutText = "This Software is Licensed under the GNU GPL - v3.\nThis Software was designed and written by Johannes Schiemer for his school diploma " +
                "project. The software is designed to be used in conjunction with the FDR built and engineered by Klaus Obermüller and Alexander Stoiber.\nWe are NOT responsible for any " +
                "damages created through misuse, user errors ore unexpected behaviour." +
                "\nCredits: See the Licenses";
            InfoText = "To load a new Track please select the 'Load' menu option. After loading you should be able to select " +
                "any of the other menu options where you can enjoy the full functionality of this program";

            _handler = starter;
            _manager = manager;
            _license = license;
        }

        /// <summary>
        /// Command connected to the MailTo Button
        /// </summary>
        public ICommand RequestNavigateCommand => _mailtoClickCommand;

        private void mailto(object sender)
        {
            _handler.Start(new ProcessStartInfo("mailto:deltajosch@gmail.com"));
        }

        /// <summary>
        /// Command connected to the SetApiKey Button
        /// </summary>
        public ICommand SetApiKeyCommand => _setApiKeyCommand;

        private void setApiKey(object sender)
        {
            _manager.Put();
        }

        /// <summary>
        /// Command connected to the "View License" Button
        /// </summary>
        public ICommand ViewLicenseCommand => _viewLicenseCommand;

        private void ViewLicense(object sender)
        {
            _license.Show();
        }

        /// <summary>
        /// Command connected to the theme changer button
        /// </summary>
        public ICommand ChangeThemeCommand => _changeThemeCommand;

        private void ChangeTheme(object sender)
        {
            ThemeStyler styler = new ThemeStyler();
            styler.Show();
        }

        private string _aboutText;

        /// <summary>
        /// Text displayed in the "about" textbox
        /// </summary>
        public string AboutText
        {
            get => _aboutText;
            set => SetProperty(ref _aboutText, value);
        }

        private string _infoText;

        /// <summary>
        /// Text displayed in the "info" textbox
        /// </summary>
        public string InfoText
        {
            get => _infoText;
            set => SetProperty(ref _infoText, value);
        }
    }

    /// <summary>
    /// A class implementing the IProcessStarter interface. Used in production code in combination with the aboutviewmodel
    /// </summary>
    public class Starter : IProcessStarter
    {
        /// <summary>
        /// The Start-function starts a specified process
        /// </summary>
        /// <param name="info"></param>
        public void Start(ProcessStartInfo info)
        {
            Process.Start(info);
        }
    }

    /// <summary>
    /// The implementation of ILicense for production
    /// </summary>
    public class Licenser : ILicense
    {
        /// <summary>
        /// This function shows the license view
        /// </summary>
        public void Show()
        {
            Licenses licenses = new Licenses();
            licenses.Show();
        }
    }
}