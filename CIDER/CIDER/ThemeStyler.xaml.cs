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
using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace CIDER
{
    /// <summary>
    /// Interaction logic for ThemeStyler.xaml
    /// </summary>
    public partial class ThemeStyler : MetroWindow
    {
        private ThemeStylerViewModel model;

        /// <summary>
        /// The constructor for the ThemeStyle Window
        /// </summary>
        public ThemeStyler()
        {
            InitializeComponent();

            model = new ThemeStylerViewModel();
            this.DataContext = model;
        }

        private void AccentColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.AccentColorChanged(AccentColor.SelectedItem.ToString());
        }
    }
}