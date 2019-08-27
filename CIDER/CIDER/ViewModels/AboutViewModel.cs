using CIDER.MVVMBase;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CIDER.ViewModels
{
    public class AboutViewModel:ViewModelBase
        ///Summary
        ///This is the ViewModel for the About View
        ///The constructor takes a ProcessStarter Interface - this is so a seam for unit testing exists
        ///On init it also sets the text in the about and information TextBlocks. They can be changed afterwards, but this is not needed in normal operation
        ///When the button in the view is pressed, the view model responds to it by calling the function fromn the processStarter interface
    {
        private readonly DelegateCommand _mailToClick;
        private readonly IProcessStarter _handler;

        public AboutViewModel(IProcessStarter starter)
        {
            _mailToClick = new DelegateCommand(mailto);
            AboutText = "This Software is Licensed under the GNU GPL - v3.\nThis Software was designed and written by Johannes Schiemer for his school diploma " +
                "project. The software is designed to be used in conjunction with the FDR built and engineered by Klaus Obermüller and Alexander Stoiber.\nWe are NOT responsible for any " +
                "damages created through misuse, user errors ore unexpected behaviour." +
                "\nCredits: Icons made by Smashicons from www.flaticon.com";
            InfoText = "To load a new Track please select the 'Load' menu option. After loading you should be able to select " +
                "any of the other menu options where you can enjoy the full functionality of this program";

            _handler = starter;
        }

        public ICommand RequestNavigate => _mailToClick;
        private void mailto(object sender) //executed by button
        {
            _handler.Start(new ProcessStartInfo("mailto:deltajosch@gmail.com"));
        }

        private string _aboutText;
        public string AboutText
        {
            get => _aboutText;
            set => SetProperty(ref _aboutText, value);
        }

        private string _infoText;
        public string InfoText
        {
            get => _infoText;
            set => SetProperty(ref _infoText, value);
        }
    }

    public class Starter : IProcessStarter
    {
        public void Start(ProcessStartInfo info)
        {
            Process.Start(info);
        }
    }
}
