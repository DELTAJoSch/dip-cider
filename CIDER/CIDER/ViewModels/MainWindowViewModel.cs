using CIDER.MVVMBase;
using CIDER.Views;
using MahApps.Metro;
using System;
using System.Windows.Input;

namespace CIDER.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the Main Window (contains view selection buttons and frame)
    /// This class handles the button presses - they change the views
    /// </summary>
    public class MainWindowViewModel : ViewModelBase, IDisposable
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

        private readonly DelegateCommand _changeToAccelerationGraphCommand;

        private readonly DelegateCommand _changeToAccelerationTimedCommand;

        private readonly DelegateCommand _changeToHorizonCommand;

        private bool _mapEnabled;

        private bool _mapAvailable;

        private bool _buttonEnabled;

        private object _frameContent;

        private DataProvider dataProvider;

        /// <summary>
        /// The EventHandler for the OnFrameChangeEvent
        /// This event is fired when the selected frame changes
        /// </summary>
        public event EventHandler OnFrameChangeEvent;

        private bool _licenseAccepted;

        /// <summary>
        /// This is the constructor for the MainWindow ViewModel
        /// </summary>
        public MainWindowViewModel()
        {
            AddLicenses();

            //connect delegate commands to icommand handlers
            _changeToHeightCommand = new DelegateCommand(OnChangeToHeight);
            _changeToLoadCommand = new DelegateCommand(OnChangeToLoad);
            _changeToAboutCommand = new DelegateCommand(OnChangeToAbout);
            _changeToAccelerationGraphCommand = new DelegateCommand(OnChangeToAccelerationGraph);
            _changeToAccelerationTimedCommand = new DelegateCommand(OnChangeToAccelerationTimed);
            _changeToAngleGraphCommand = new DelegateCommand(OnChangeToAngleGraph);
            _changeToAngleTimedCommand = new DelegateCommand(OnChangeToAngleTimed);
            _changeToMapRouteCommand = new DelegateCommand(OnChangeToMapRoute);
            _changeToMapTimedCommand = new DelegateCommand(OnChangeToMapTimed);
            _changeToVelocityGraphCommand = new DelegateCommand(OnChangeToVelocityGraph);
            _changeToVelocityTimedCommand = new DelegateCommand(OnChangeToVelocityTimed);
            _changeToHorizonCommand = new DelegateCommand(OnChangeToHorizonCommand);

            dataProvider = new DataProvider();

            MapEnabled = true;
            _mapAvailable = true;

            KeyManager manager = new KeyManager(dataProvider, new FileReader());

            var reader = new FileReader();
            if (!reader.FileExists("CIDER.cfg"))
                reader.WriteAllText("", "CIDER.cfg");

            if (!manager.Fetch())
            {
                MapEnabled = false;
                _mapAvailable = false;
            }

            if(ThemeManager.DetectAppStyle().Item1 == ThemeManager.GetAppTheme("BaseDark"))
            {
                var theme = ThemeManager.DetectAppStyle();
                ThemeManager.ChangeAppTheme(App.Current, "BaseDark");
            }

            // Check the license
            try
            {
                LicenseWriter licenseWriter = new LicenseWriter(new FileReader());
                _licenseAccepted = licenseWriter.ReadAgreementState();
                LicenseManager.LicensesAccepted = _licenseAccepted;

                if(_licenseAccepted == false)
                {
                    Licenses licenses = new Licenses();
                    licenses.Show();
                    _licenseAccepted = LicenseManager.LicensesAccepted;
                }
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Couldn't configure License State");
                _licenseAccepted = false;
            }

            ButtonState(false);
            MapEnabled = false;

            if (_licenseAccepted)
                ButtonState(true);

            KeyManager.MapKeyChangedEvent += KeyManager_MapKeyChangedEvent;
            LicenseHolder.LicenseChangedEvent += LicenseHolder_LicenseChangedEvent;
        }

        private void LicenseHolder_LicenseChangedEvent(object sender, EventArgs e)
        {
            if (LicenseHolder.AcceptedLicense)
            {
                _licenseAccepted = true;
                ButtonState(true);
            }
        }

        private void KeyManager_MapKeyChangedEvent(object sender, EventArgs e)
        {
            UpdateMapStatus();
        }

        /// <summary>
        /// This command is connected to the "about" button
        /// </summary>
        public ICommand ChangeToAboutCommand => _changeToAboutCommand;

        /// <summary>
        /// This command is connected to the "Acceleration Graph" button
        /// </summary>
        public ICommand ChangeToAccelerationGraphCommand => _changeToAccelerationGraphCommand;

        /// <summary>
        /// This command is connected to the "Acceleration Timed" button
        /// </summary>
        public ICommand ChangeToAccelerationTimedCommand => _changeToAccelerationTimedCommand;

        /// <summary>
        /// This command is connected to the "Load" button
        /// </summary>
        public ICommand ChangeToLoadCommand => _changeToLoadCommand;

        /// <summary>
        /// This command is connected to the "Route" button
        /// </summary>
        public ICommand ChangeToMapRouteCommand => _changeToMapRouteCommand;

        /// <summary>
        /// This command is connected to the "Map Timed" button
        /// </summary>
        public ICommand ChangeToMapTimedCommand => _changeToMapTimedCommand;

        /// <summary>
        /// This command is connected to the "Velocity Graph" button
        /// </summary>
        public ICommand ChangeToVelocityGraphCommand => _changeToVelocityGraphCommand;

        /// <summary>
        /// This command is connected to the "Velocity Timed" button
        /// </summary>
        public ICommand ChangeToVelocityTimedCommand => _changeToVelocityTimedCommand;

        /// <summary>
        /// This command is connected to the "Height" button
        /// </summary>
        public ICommand ChangeToHeightCommand => _changeToHeightCommand;

        /// <summary>
        /// This command is connected to the "Angle Timed" button
        /// </summary>
        public ICommand ChangeToAngleTimedCommand => _changeToAngleTimedCommand;

        /// <summary>
        /// This command is connected to the "Angle Graph" button
        /// </summary>
        public ICommand ChangeToAngleGraphCommand => _changeToAngleGraphCommand;

        /// <summary>
        /// This command is connected to the "Horizon" button
        /// </summary>
        public ICommand ChangeToHorizonCommand => _changeToHorizonCommand;

        /// <summary>
        /// This bool is true when the map views should be enabled
        /// </summary>
        public bool MapEnabled { get { return _mapEnabled; } set { SetProperty(ref _mapEnabled, value); } }

        /// <summary>
        /// This object contains the view to be shown in the main frame
        /// </summary>
        public object FrameContent { get { return _frameContent; } private set { _frameContent = value; } }

        /// <summary>
        /// This bool contains´information on wether the buttons hould be enabled
        /// </summary>
        public bool ButtonEnabled { get { return _buttonEnabled; } set { SetProperty(ref _buttonEnabled, value); } }

        private void UpdateMapStatus()
        {
            MapEnabled = true;
            _mapAvailable = true;

            KeyManager manager = new KeyManager(dataProvider, new FileReader());

            if (!manager.Fetch())
            {
                MapEnabled = false;
                _mapAvailable = false;
            }
        }

        /// <summary>
        /// This function should be started to initialize the view
        /// </summary>
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

        private void OnChangeToAngleTimed(object sender)
        {
            FrameContent = new AngleTimed(dataProvider);
            RaiseEvent(new EventArgs());
        }

        private void OnChangeToAngleGraph(object sender)
        {
            FrameContent = new AngleGraph(dataProvider);
            RaiseEvent(new EventArgs());
        }

        private void OnChangeToHorizonCommand(object sender)
        {
            FrameContent = new ArtificialHorizon(dataProvider);
            RaiseEvent(new EventArgs());
        }

        private void RaiseEvent(EventArgs e)
        {
            EventHandler handler = OnFrameChangeEvent;
            if (handler != null)
                handler.Invoke(this, e);
        }

        /// <summary>
        /// This function sets the state of the buttons
        /// </summary>
        /// <param name="state">the state to be set (if allowed)</param>
        public void ButtonState(bool state)
        {
            if (_licenseAccepted)
            {
                if (_mapAvailable)
                    MapEnabled = state;

                ButtonEnabled = state;
            }
        }

        /// <summary>
        /// As this class implements the IDisposable interface, this must be called before the GC collects this object on dereference
        /// </summary>
        public void Dispose()
        {
            KeyManager.MapKeyChangedEvent -= KeyManager_MapKeyChangedEvent;
            LicenseHolder.LicenseChangedEvent -= LicenseHolder_LicenseChangedEvent;
        }

        private void AddLicenses()
        {
            LicenseManager.AddLicense(LicenseHolder.ThisSoftwareLicense);
            LicenseManager.AddLicense(LicenseHolder.OxyPlotLicense);
            LicenseManager.AddLicense(LicenseHolder.NLOGLicense);
            LicenseManager.AddLicense(LicenseHolder.docFxLicense);
            LicenseManager.AddLicense(LicenseHolder.pilotHUDLicense);
            LicenseManager.AddLicense(LicenseHolder.MahAppsLicense);
        }
    }
}