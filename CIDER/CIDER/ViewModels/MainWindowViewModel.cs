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

        private bool _mapAvailable;

        private bool _buttonEnabled;

        private object _frameContent;

        private DataProvider dataProvider;

        public event EventHandler OnFrameChangeEvent;

        public MainWindowViewModel()
        ///Due to the frame control being broken/bugged a mvvm approach is not doable without using external frameworks like mvvmlight
        ///Therefor the frame is just passed to the constructor - this is not optimal but it works without problems.The only possible problem is the decreased readability
        {
            //connect delegate commands to icommand handlers
            _changeToHeightCommand = new DelegateCommand(OnChangeToHeight);
            _changeToLoadCommand = new DelegateCommand(OnChangeToLoad);
            _changeToAboutCommand = new DelegateCommand(OnChangeToAbout);
            _changeToAngleGraphCommand = new DelegateCommand(OnChangeToAccelerationGraph);
            _changeToAngleTimedCommand = new DelegateCommand(OnChangeToAccelerationTimed);
            _changeToMapRouteCommand = new DelegateCommand(OnChangeToMapRoute);
            _changeToMapTimedCommand = new DelegateCommand(OnChangeToMapTimed);
            _changeToVelocityGraphCommand = new DelegateCommand(OnChangeToVelocityGraph);
            _changeToVelocityTimedCommand = new DelegateCommand(OnChangeToVelocityTimed);

            dataProvider = new DataProvider();

            MapEnabled = true;
            _mapAvailable = true;

            KeyManager manager = new KeyManager(dataProvider, new KeyManagerReader());
            if (!manager.Fetch())
            {
                MapEnabled = false;
                _mapAvailable = false;
            }

            ButtonState(true);

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
        public object FrameContent { get { return _frameContent; } private set { _frameContent = value; } }
        public bool ButtonEnabled { get { return _buttonEnabled; } set { SetProperty(ref _buttonEnabled, value); } }

        private void UpdateMapStatus()
        {
            MapEnabled = true;
            _mapAvailable = true;

            KeyManager manager = new KeyManager(dataProvider, new KeyManagerReader());
            if (!manager.Fetch())
            {
                MapEnabled = false;
                _mapAvailable = false;
            }
        }

        public void Initalize()
        {
            FrameContent = new About(dataProvider);
            RaiseEvent(new EventArgs());
        }

        //these functions are called on button presses
        private void OnChangeToAbout(object sender)
        {
            FrameContent = new About(dataProvider);
            RaiseEvent(new EventArgs());
        }
        private void OnChangeToAccelerationGraph(object sender)
        {
            FrameContent = new AccelerationGraph(dataProvider);
            RaiseEvent(new EventArgs());
        }

        private void OnChangeToAccelerationTimed(object sender)
        {
            FrameContent = new AccelerationTimed(dataProvider);
            RaiseEvent(new EventArgs());
        }

        private void OnChangeToLoad(object sender)
        {
            FrameContent = new Load(dataProvider, this);
            RaiseEvent(new EventArgs());
        }
        private void OnChangeToMapRoute(object sender)
        {
            FrameContent = new MapRoute(dataProvider);
            RaiseEvent(new EventArgs());
        }

        private void OnChangeToMapTimed(object sender)
        {
            FrameContent = new MapTimed(dataProvider);
            RaiseEvent(new EventArgs());
        }

        private void OnChangeToVelocityGraph(object sender)
        {
            FrameContent = new VelocityGraph(dataProvider);
            RaiseEvent(new EventArgs());
        }

        private void OnChangeToVelocityTimed(object sender)
        {
            FrameContent = new VelocityTimed(dataProvider);
            RaiseEvent(new EventArgs());
        }

        private void OnChangeToHeight(object sender)
        {
            FrameContent = new Height(dataProvider);
            RaiseEvent(new EventArgs());
        }

        private void RaiseEvent(EventArgs e)
        {
            EventHandler handler = OnFrameChangeEvent;
            if (handler != null)
                handler.Invoke(this, e);
        }

        public void ButtonState(bool state)
        {
            if (_mapAvailable)
                MapEnabled = state;

            ButtonEnabled = state;
        }
        public void Dispose()
        {
            KeyManager.MapKeyChangedEvent -= KeyManager_MapKeyChangedEvent;
        }
    }
}