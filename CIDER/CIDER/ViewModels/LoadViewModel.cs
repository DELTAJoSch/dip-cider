/* Copyright (C) 2020  Johannes Schiemer 
	This program is free software: you can redistribute it and/or modify 
	it under the terms of the GNU General Public License as published by 
	the Free Software Foundation, either version 3 of the License, or 
	(at your option) any later version. 
	This program is distributed in the hope that it will be useful, 
	but WITHOUT ANY WARRANTY; without even the implied warranty of 
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
	GNU General Public License for more details. 
	You should have received a copy of the GNU General Public License 
	along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/
using CIDER.LoadIO;
using CIDER.MVVMBase;
using System;
using System.Windows.Input;

namespace CIDER.ViewModels
{
    /// <summary>
    /// The ViewModel for the Load page.
    /// </summary>
    public class LoadViewModel : ViewModelBase
    {
        private DataProvider _dataProvider;
        private DelegateCommand _loadClickCommand;
        private DelegateCommand _selectClickCommand;
        private string _pathText;
        private string _checkImage;
        private IFolderSelector _folderSelector;
        private IChecker _folderChecker;
        private FileIO _fileIO;
        private MainWindowViewModel _main;
        private string _path;
        private bool _loadEnabled;

        /// <summary>
        /// This is the constructor for the LoadViewModel
        /// </summary>
        /// <param name="data">A DataProvider object to store the ingested data in</param>
        /// <param name="folderChecker">An object implementing the IChecker interface to check the folders integrity</param>
        /// <param name="selector">An object implementing the IFolderSelector interface used to select the folder</param>
        /// <param name="fileIO">An object implementing the FileIO</param>
        /// <param name="main">An instance of the MainWindowViewModel</param>
        public LoadViewModel(DataProvider data, IChecker folderChecker, IFolderSelector selector, FileIO fileIO, MainWindowViewModel main)
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

        /// <summary>
        /// The command fired when the loadButon is clicked
        /// </summary>
        public ICommand LoadClickCommand => _loadClickCommand;

        /// <summary>
        /// The command fired when the select folder button is clicked
        /// </summary>
        public ICommand SelectClickCommand => _selectClickCommand;

        private async void OnLoadClick(object sender)
        {
            _dataProvider.ClearData();

            await _fileIO.ReadCSV(_dataProvider, _path, new Reader(), _main);
            await _fileIO.ReadNmea(_dataProvider, _path, new Reader(), _main);

            logger.Debug("Load Clicked");
        }

        private void OnSelectClick(object sender)
        // Select Button Clicked
        {
            logger.Debug("Select Clicked");
            try
            {
                _path = _folderSelector.SelectFolder();
                PathText = _path;

                // Set the Is Valid Folder Icon
                if (_folderChecker.IsCorrectFolder(_path) == true)
                    CheckImage = @"..\Icons\002-success.png";
                else
                    CheckImage = @"..\Icons\001-error.png";

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

        /// <summary>
        /// The string to display in the text box containig the path
        /// </summary>
        public string PathText
        {
            get { return _pathText; }
            set { SetProperty(ref _pathText, value); }
        }

        /// <summary>
        /// image location for the correct folder / wrong folder structure icon
        /// </summary>
        public string CheckImage
        {
            get { return _checkImage; }
            set { SetProperty(ref _checkImage, value); }
        }

        /// <summary>
        /// bool for enabling load button
        /// </summary>
        public bool LoadEnabled
        {
            get { return _loadEnabled; }
            set { SetProperty(ref _loadEnabled, value); }
        }
    }
}