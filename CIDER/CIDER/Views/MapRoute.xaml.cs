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
    /// Interaction logic for the map route page
    /// </summary>
    public partial class MapRoute : Page
    {
        private MapRouteViewModel model;

        /// <summary>
        /// The constructor for the MapRoute page
        /// </summary>
        /// <param name="data">A DataProvider object to read the data from</param>
        public MapRoute(DataProvider data)
        {
            InitializeComponent();

            model = new MapRouteViewModel(data);
            this.DataContext = model;
            model.RouteChangedEvent += Model_RouteChangedEvent;
            model.Initialize();
            Map.SetView(model.MapCenter, model.MapZoomLevel);
        }

        private void Model_RouteChangedEvent(object sender, EventArgs e)
        {
            Map.Children.Clear();
            foreach (MapPolyline line in model.MapPolylines)
            {
                Map.Children.Add(line);
            }

            model.CalculateCenter();
            Map.SetView(model.MapCenter, model.MapZoomLevel);
        }

        private void onUnload(object sender, RoutedEventArgs e)
        {
            model.RouteChangedEvent -= Model_RouteChangedEvent;
        }
    }
}