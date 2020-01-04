﻿/* Copyright (C) 2020  Johannes Schiemer 
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
using System.Windows.Forms;

namespace CIDER.LoadIO
{
    /// <summary>
    /// This class implements the Folderselectioninterface. This class can show a Userinterface allowing the user to select a folder.
    /// If the user exits the dialog without selecting a folder, an exception will be thrown
    /// </summary>
    public class FolderSelector : IFolderSelector
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private string _lastSelected;
        
        /// <summary>
        /// This property contains the path last selected by the user
        /// </summary>
        public string LastSelected { get { return _lastSelected; } private set { _lastSelected = value; } }

        /// <summary>
        /// This Function shows a Dialog prompting the user to select a folder
        /// </summary>
        /// <returns>A Path to the selected file</returns>
        public string SelectFolder()
        {
            FolderBrowserDialog browserDialog = new FolderBrowserDialog();

            browserDialog.Description = "Select CIDER Folder";
            browserDialog.ShowNewFolderButton = false;

            if (browserDialog.ShowDialog() == DialogResult.OK)
            {
                return browserDialog.SelectedPath;
            }
            else
            {
                throw new FileDialogExitedException();
            }
        }
    }

    /// <summary>
    /// This Interface implements functions used for selecting folders. It can be used as a way to inject mocks and stubs.
    /// </summary>
    public interface IFolderSelector
    {
        /// <summary>
        /// This Function should prompt the user to select a folder
        /// </summary>
        /// <returns>A string with the path</returns>
        string SelectFolder();

        /// <summary>
        /// This string is supposed to contain the last selected path
        /// </summary>
        string LastSelected { get; }
    }
}