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
    {
        private readonly DelegateCommand _mailToClick;
        private readonly ProcessStarter _handler;

        public AboutViewModel(ProcessStarter starter)
        {
            _mailToClick = new DelegateCommand(mailto);
            AboutText = "This Software is Licensed under the GNU GPL - v3.\nThis Software was designed and written by Johannes Schiemer for his school diploma " +
                "project. The software is designed to be used in conjunction with the FDR built and engineered by Klaus Obermüller and Alexander Stoiber.\nWe are NOT responsible for any " +
                "damages created through misuse, user errors ore unexpected behaviour.";
            InfoText = "To load a new Track please select the 'Load' menu option. After loading you should be able to select " +
                "any of the other menu options where you can enjoy the full functionality of this program";

            _handler = starter;
        }

        public ICommand RequestNavigate => _mailToClick;
        private void mailto(object sender)
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

    public class Starter : ProcessStarter
    {
        public void Start(ProcessStartInfo info)
        {
            Process.Start(info);
        }
    }
}
