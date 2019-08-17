using CIDER.MVVMBase;
using System;
using System.Windows.Controls;
using System.Windows.Input;

namespace CIDER.ViewModels
{
    //due to the .net core restrictions, this class is untestable as no frame can be created inside the unit test framework
    public class MainWindowViewModel : ViewModelBase
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

        //these are the uris to the views (pages)
        private Uri about;

        private Uri angleGraph;

        private Uri angleTimed;

        private Uri load;

        private Uri mapRoute;

        private Uri mapTimed;

        private Uri velocityGraph;

        private Uri velocityTimed;

        private FrameHandler frameHandler;

        public MainWindowViewModel(Frame frame, Uri aboutUri, Uri angleGraphUri, Uri angleTimeUri, Uri loadUri, Uri mapRouteUri, Uri mapTimedUri, Uri velocityGraphUri, Uri velocityTimedUri, FrameHandler handler)
        {
            _frame = frame;

            //connect delegate commands to icommand handlers
            _changeToLoadCommand = new DelegateCommand(OnChangeToLoad);
            _changeToAboutCommand = new DelegateCommand(OnChangeToAbout);
            _changeToAngleGraphCommand = new DelegateCommand(OnChangeToAngleGraph);
            _changeToAngleTimedCommand = new DelegateCommand(OnChangeToAngleTimed);
            _changeToMapRouteCommand = new DelegateCommand(OnChangeToMapRoute);
            _changeToMapTimedCommand = new DelegateCommand(OnChangeToMapTimed);
            _changeToVelocityGraphCommand = new DelegateCommand(OnChangeToVelocityGraph);
            _changeToVelocityTimedCommand = new DelegateCommand(OnChangeToVelocityTimed);

            about = aboutUri;
            angleGraph = angleGraphUri;
            angleTimed = angleTimeUri;
            load = loadUri;
            mapRoute = mapRouteUri;
            mapTimed = mapTimedUri;
            velocityGraph = velocityGraphUri;
            velocityTimed = velocityTimedUri;

            frameHandler = handler;

            frameHandler.Navigate(about, _frame);
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
            frameHandler.Navigate(about, _frame);
        }

        private void OnChangeToAngleGraph(object sender)
        {
            frameHandler.Navigate(angleGraph, _frame);
        }

        private void OnChangeToAngleTimed(object sender)
        {
            frameHandler.Navigate(angleTimed, _frame);
        }

        private void OnChangeToLoad(object sender)
        {
            frameHandler.Navigate(load, _frame);
        }
        private void OnChangeToMapRoute(object sender)
        {
            frameHandler.Navigate(mapRoute, _frame);
        }

        private void OnChangeToMapTimed(object sender)
        {
            frameHandler.Navigate(mapTimed, _frame);
        }

        private void OnChangeToVelocityGraph(object sender)
        {
            frameHandler.Navigate(velocityGraph, _frame);
        }

        private void OnChangeToVelocityTimed(object sender)
        {
            frameHandler.Navigate(velocityTimed, _frame);
        }
    }
}