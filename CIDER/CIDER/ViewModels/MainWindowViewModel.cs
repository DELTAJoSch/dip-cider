using CIDER.MVVMBase;
using CIDER.Views;
using System;
using System.Diagnostics.Tracing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CIDER.ViewModels
{
    //NOTICE: due to the .net core restrictions, this class is untestable as no frame can be created inside the unit test framework
    public class MainWindowViewModel : ViewModelBase, IDisposable
    /*/Summary
     * This is the ViewModel for the Main Window (contains view selection buttons and frame)
     * This class handles the button presses - they change the views
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

        private readonly DelegateCommand _changeToHeightCommand;

        private bool _mapEnabled;

        private Frame _frame;

        private DataProvider dataProvider;

        public MainWindowViewModel(Frame frame)
        ///Due to the frame control being broken/bugged a mvvm approach is not doable without using external frameworks like mvvmlight
        ///Therefor the frame is just passed to the constructor - this is not optimal but it works without problems.The only possible problem is the decreased readability
        {
            _frame = frame;

            //connect delegate commands to icommand handlers
            _changeToHeightCommand = new DelegateCommand(OnChangeToHeight);
            _changeToLoadCommand = new DelegateCommand(OnChangeToLoad);
            _changeToAboutCommand = new DelegateCommand(OnChangeToAbout);
            _changeToAngleGraphCommand = new DelegateCommand(OnChangeToAngleGraph);
            _changeToAngleTimedCommand = new DelegateCommand(OnChangeToAngleTimed);
            _changeToMapRouteCommand = new DelegateCommand(OnChangeToMapRoute);
            _changeToMapTimedCommand = new DelegateCommand(OnChangeToMapTimed);
            _changeToVelocityGraphCommand = new DelegateCommand(OnChangeToVelocityGraph);
            _changeToVelocityTimedCommand = new DelegateCommand(OnChangeToVelocityTimed);

            _frame.Navigate(new About(dataProvider));

            dataProvider = new DataProvider();

            MapEnabled = true;

            KeyManager manager = new KeyManager(dataProvider);
            if (!manager.Fetch())
            {
                MapEnabled = false;
            }

            KeyManager.MapKeyChangedEvent += KeyManager_MapKeyChangedEvent;
        }
        private void KeyManager_MapKeyChangedEvent(object sender, EventArgs e)
        {
            UpdateMapStatus();
        }

        public ICommand ChangeToAboutCommand => _changeToAboutCommand;
        public ICommand ChangeToAngleGraphCommand => _changeToAngleGraphCommand;
        public ICommand ChangeToAngleTimedCommand => _changeToAngleTimedCommand;
        public ICommand ChangeToLoadCommand => _changeToLoadCommand;
        public ICommand ChangeToMapRouteCommand => _changeToMapRouteCommand;
        public ICommand ChangeToMapTimedCommand => _changeToMapTimedCommand;
        public ICommand ChangeToVelocityGraphCommand => _changeToVelocityGraphCommand;
        public ICommand ChangeToVelocityTimedCommand => _changeToVelocityTimedCommand;
        public ICommand ChangeToHeightCommand => _changeToHeightCommand;

        public bool MapEnabled { get { return _mapEnabled; } set { SetProperty(ref _mapEnabled, value); } }

        private void UpdateMapStatus()
        {
            MapEnabled = true;

            KeyManager manager = new KeyManager(dataProvider);
            if (!manager.Fetch())
            {
                MapEnabled = false;
            }
        }

        //these functions are called on button presses
        private void OnChangeToAbout(object sender)
        {
            _frame.Navigate(new About(dataProvider));
        }
        private void OnChangeToAngleGraph(object sender)
        {
            _frame.Navigate(new AccelerationGraph(dataProvider));
        }

        private void OnChangeToAngleTimed(object sender)
        {
            _frame.Navigate(new AccelerationTimed(dataProvider));
        }

        private void OnChangeToLoad(object sender)
        {
            _frame.Navigate(new Load(dataProvider));
        }
        private void OnChangeToMapRoute(object sender)
        {
            _frame.Navigate(new MapRoute(dataProvider));
        }

        private void OnChangeToMapTimed(object sender)
        {
            _frame.Navigate(new MapTimed(dataProvider));
        }

        private void OnChangeToVelocityGraph(object sender)
        {
            _frame.Navigate(new VelocityGraph(dataProvider));
        }

        private void OnChangeToVelocityTimed(object sender)
        {
            _frame.Navigate(new VelocityTimed(dataProvider));
        }

        private void OnChangeToHeight(object sender)
        {
            _frame.Navigate(new Height(dataProvider));
        }

        public void Dispose()
        {
            KeyManager.MapKeyChangedEvent -= KeyManager_MapKeyChangedEvent;
        }
    }
}