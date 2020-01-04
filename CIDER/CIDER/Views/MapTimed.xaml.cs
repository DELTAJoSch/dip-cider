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
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for the MapTimed page
    /// </summary>
    public partial class MapTimed : Page
    {
        private MapTimedViewModel model;

        /// <summary>
        /// This is the constructor of the MapTimed page
        /// </summary>
        /// <param name="data"></param>
        public MapTimed(DataProvider data)
        {
            InitializeComponent();

            model = new MapTimedViewModel(data);
            this.DataContext = model;

            model.RouteChangedEvent += Model_RouteChangedEvent;

            model.Initialize();

            map.SetView(model.MapCenter, model.MapZoomLevel);
        }

        private void Model_RouteChangedEvent(object sender, EventArgs e)
        {
            map.Children.Clear();

            foreach (MapPolyline line in model.MapPolylines)
            {
                map.Children.Add(line);
            }
        }

        /// <summary>
        /// This function is called when the slider value changes
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The event args this was called with</param>
        public void slValueChanged(object sender, EventArgs e)
        {
            model.SliderValueChanged((int)slValue.Value);
            model.CalculateCenter();

            map.SetView(model.MapCenter, model.MapZoomLevel);
        }

        private void onUnload(object sender, RoutedEventArgs e)
        {
            model.RouteChangedEvent -= Model_RouteChangedEvent;
        }
    }
}