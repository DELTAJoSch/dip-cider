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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CIDER
{
    /// <summary>
    /// This is the License Window.
    /// The purpose of this Window is to provide a way to show the user all the licenses and handle all the license-agreement related issues.
    /// </summary>
    public partial class Licenses : MetroWindow
    {
        private LicensesViewModel model;

        /// <summary>
        /// This is the constructor for the License Window
        /// The DataContext is set here
        /// </summary>
        public Licenses()
        {
            InitializeComponent();

            model = new LicensesViewModel(new LicenseWriter(new FileReader()));
            this.DataContext = model;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            model.SaveAcceptAgreement();
            this.Close();
        }
    }
}
