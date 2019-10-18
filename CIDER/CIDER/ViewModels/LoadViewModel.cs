using CIDER.LoadIO;
using CIDER.MVVMBase;
using System;
using System.Windows.Input;

namespace CIDER.ViewModels
{
    public class LoadViewModel : ViewModelBase
    ///Summary
    ///The ViewModel for the Load page.
    {
        private DataProvider _dataProvider;
        private DelegateCommand _loadClickCommand;
        private DelegateCommand _selectClickCommand;
        private string _pathText;
        private string _checkImage;
        private IFolderSelectionInterface _folderSelector;
        private IChecker _folderChecker;
        private FileIO _fileIO;
        private MainWindowViewModel _main;
        private string _path;
        private bool _loadEnabled;

        public LoadViewModel(DataProvider data, IChecker folderChecker, IFolderSelectionInterface selector, FileIO fileIO, MainWindowViewModel main)
        ///The constructor takes the different Objects as Arguments - this makes the code testable
        {
            _dataProvider = data;
            _loadClickCommand = new DelegateCommand(OnLoadClick);
            _selectClickCommand = new DelegateCommand(OnSelectClick);
            _folderChecker = folderChecker;
            _folderSelector = selector;
            _fileIO = fileIO;
            _main = main;
            _path = null;

            if (String.IsNullOrEmpty(_path))
                LoadEnabled = false;
        }

        public ICommand LoadClickCommand => _loadClickCommand;
        public ICommand SelectClickCommand => _selectClickCommand;

        private void OnLoadClick(object sender)
        {
            _dataProvider.ClearData();

            _fileIO.ReadCSV(_dataProvider, _path, new Reader(), _main);
            _fileIO.ReadNmea(_dataProvider, _path, new Reader(), _main);

            logger.Debug("Load Clicked");
        }

        private void OnSelectClick(object sender)
        ///Select Button Clicked
        {
            logger.Debug("Select Clicked");
            try
            {
                _path = _folderSelector.SelectFolder();
                PathText = _path;

                //Set the Is Valid Folder Icon
                if (_folderChecker.IsCorrectFolder(_path) == true)
                    CheckImage = @"~\..\..\Icons\success.png";
                else
                    CheckImage = @"~\..\..\Icons\forbidden.png";

                LoadEnabled = true;
            }
            catch (FileDialogExitedException e)
            {
                PathText = "";
                _path = null;
                CheckImage = null;

                logger.Debug(e, "File Dialog Exited");
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Selection failed");
            }

            if (String.IsNullOrEmpty(_path))
                LoadEnabled = false;
        }

        public string PathText
        ///string for the path textbox
        {
            get { return _pathText; }
            set { SetProperty(ref _pathText, value); }
        }

        public string CheckImage
        ///image location for the correct folder / wrong folder structure icon
        {
            get { return _checkImage; }
            set { SetProperty(ref _checkImage, value); }
        }

        public bool LoadEnabled
        ///bool for enabling load button
        {
            get { return _loadEnabled; }
            set { SetProperty(ref _loadEnabled, value); }
        }
    }
}