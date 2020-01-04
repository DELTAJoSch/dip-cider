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
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for the Height page
    /// </summary>
    public partial class Height : Page
    {
        private HeightViewModel model;

        /// <summary>
        /// This is the constructor for the Height page
        /// </summary>
        /// <param name="data">A DataProvider object to read the data from</param>
        public Height(DataProvider data)
        {
            InitializeComponent();

            model = new HeightViewModel(data);

            this.DataContext = model;
        }

        private void slValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            model.slValueChanged((int)slValue.Value);
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            model.Dispose();
        }
    }
}