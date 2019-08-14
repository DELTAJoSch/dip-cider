using CIDER.MVVMBase;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace CIDER.ViewModels
{
    //IMPORTANT!!!!!!!!!!! Still needs to be tested!
    internal class MainWindowViewModel : ViewModelBase
    /*/Summary
     * This is the ViewModel for the Main Window (contains view selection buttons and frame)
     * This class handles the button presses - they change the views
     * Due to the frame control being broken/bugged a mvvm approach is not doable without using external frameworks like mvvmlight
     * Therefor the frame is just passed to the constructor - this is not optimal but it works without problems. The only possible problem is the decreased readability
    /*/ 
    {
        private readonly DelegateCommand _changeToAboutCommand;

        private readonly DelegateCommand _changeToAngleGraphCommand;

        private readonly DelegateCommand _changeToAngleTimedCommand;

        private readonly DelegateCommand _changeToLoadCommand;

        private readonly DelegateCommand _changeToMapRouteCommand;

        private readonly DelegateCommand _changeToMapTimedCommand;

        private readonly DelegateCommand _changeToVelocityGraphCommand;

        private readonly DelegateCommand _changeToVelocityTimedCommand;

        private Frame _frame;

        //these are the uris to the views (pages) stored into variables - separated for readability
        private Uri about = new Uri("Views/About.xaml", UriKind.Relative);

        private Uri angleGraph = new Uri("Views/AngleGraph.xaml", UriKind.Relative);

        private Uri angleTimed = new Uri("Views/AngleTimed.xaml", UriKind.Relative);

        private Uri load = new Uri("Views/Load.xaml", UriKind.Relative);

        private Uri mapRoute = new Uri("Views/MapRoute.xaml", UriKind.Relative);

        private Uri mapTimed = new Uri("Views/MapTimed.xaml", UriKind.Relative);

        private Uri velocityGraph = new Uri("Views/VelocityGraph.xaml", UriKind.Relative);

        private Uri velocityTimed = new Uri("Views/VelocityTimed.xaml", UriKind.Relative);

        public MainWindowViewModel(Frame frame)
        {
            _frame = frame;
            _frame.Navigate(about);

            //connect delegate commands to icommand handlers
            _changeToLoadCommand = new DelegateCommand(OnChangeToLoad);
            _changeToAboutCommand = new DelegateCommand(OnChangeToAbout);
            _changeToAngleGraphCommand = new DelegateCommand(OnChangeToAngleGraph);
            _changeToAngleTimedCommand = new DelegateCommand(OnChangeToAngleTimed);
            _changeToMapRouteCommand = new DelegateCommand(OnChangeToMapRoute);
            _changeToMapTimedCommand = new DelegateCommand(OnChangeToMapTimed);
            _changeToVelocityGraphCommand = new DelegateCommand(OnChangeToVelocityGraph);
            _changeToVelocityTimedCommand = new DelegateCommand(OnChangeToVelocityTimed);
        }
        public ICommand ChangeToAboutCommand => _changeToAboutCommand;
        public ICommand ChangeToAngleGraphCommand => _changeToAngleGraphCommand;
        public ICommand ChangeToAngleTimedCommand => _changeToAngleTimedCommand;
        public ICommand ChangeToLoadCommand => _changeToLoadCommand;
        public ICommand ChangeToMapRouteCommand => _changeToMapRouteCommand;
        public ICommand ChangeToMapTimedCommand => _changeToMapTimedCommand;
        public ICommand ChangeToVelocityGraphCommand => _changeToVelocityGraphCommand;
        public ICommand ChangeToVelocityTimedCommand => _changeToVelocityTimedCommand;

        //these functions are called on button presses
        private void OnChangeToAbout(object sender)
        {
            _frame.Navigate(about);
        }

        private void OnChangeToAngleGraph(object sender)
        {
            _frame.Navigate(angleGraph);
        }

        private void OnChangeToAngleTimed(object sender)
        {
            _frame.Navigate(angleTimed);
        }

        private void OnChangeToLoad(object sender)
        {
            _frame.Navigate(load);
        }
        private void OnChangeToMapRoute(object sender)
        {
            _frame.Navigate(mapRoute);
        }

        private void OnChangeToMapTimed(object sender)
        {
            _frame.Navigate(mapTimed);
        }

        private void OnChangeToVelocityGraph(object sender)
        {
            _frame.Navigate(velocityGraph);
        }

        private void OnChangeToVelocityTimed(object sender)
        {
            _frame.Navigate(velocityTimed);
        }
    }
}