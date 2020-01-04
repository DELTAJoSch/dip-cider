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
using CIDER.ViewModels;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for Load.xaml
    /// </summary>
    public partial class Load : Page
    {
        private DataProvider _dataProvider;

        /// <summary>
        /// This is the constructor for the Load page
        /// </summary>
        /// <param name="data">A DataProvider object to store the data in</param>
        /// <param name="main">An instance of the MainWindowViewModel</param>
        public Load(DataProvider data, MainWindowViewModel main)
        {
            InitializeComponent();
            _dataProvider = data;
            LoadViewModel loadView = new LoadViewModel(_dataProvider, new CIDER.LoadIO.FolderChecker(), new CIDER.LoadIO.FolderSelector(), new CIDER.LoadIO.FileIO(), main);
            this.DataContext = loadView;
        }
    }
}