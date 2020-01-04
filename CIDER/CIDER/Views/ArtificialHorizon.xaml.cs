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
    /// Interaction logic for the ArtificialHorizon page
    /// </summary>
    public partial class ArtificialHorizon : Page
    {
        private ArtificialHorizonViewModel model;

        /// <summary>
        /// The constructor for the ArtificialHorizon page
        /// </summary>
        /// <param name="data">A DataPRovider object to read the data from</param>
        public ArtificialHorizon(DataProvider data)
        {
            InitializeComponent();
            model = new ArtificialHorizonViewModel(data);
            this.DataContext = model;
        }

        private void SlValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            model.SliderValueChanged((int)slValue.Value);
        }
    }
}