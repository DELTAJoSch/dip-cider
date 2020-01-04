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
using CIDER.MVVMBase;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIDER.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the MapTimed page
    /// </summary>
    public class MapTimedViewModel : ViewModelBase
    {
        private ApplicationIdCredentialsProvider _apiKey;
        private DataProvider _data;
        private List<MapPolyline> _mapPolylines;
        private int _slMaximum;
        private int _slTickFrequency;
        private RouteMaker maker;
        private Location _mapCenter;
        private double _mapZoomLevel;

        /// <summary>
        /// This event is raised when the route changed
        /// </summary>
        public event EventHandler RouteChangedEvent;

        /// <summary>
        /// This is the constructor for the MapTimedViewModel
        /// </summary>
        /// <param name="data">A DataProvider object to read the data from</param>
        public MapTimedViewModel(DataProvider data)
        {
            _data = data;

            maker = new RouteMaker();
            _mapPolylines = new List<MapPolyline>();

            //set the api key read from the key file
            APIKey = new ApplicationIdCredentialsProvider(data.APIKey);

            MapZoomLevel = 12.6;

            if (_data.Route.Count > 0)
                MapCenter = _data.Route.First();
            else
                MapCenter = new Location(48.236096, 14.188624);

            slMaximum = _data.Route.Count() - 1;
            if (slMaximum < 1000)
                slTickFrequency = 2;
            if (slMaximum > 1000 && slMaximum < 10000)
                slTickFrequency = 10;
            if (slMaximum > 10000 && slMaximum < 1000000)
                slTickFrequency = 500;
            if (slMaximum > 1000000)
                slTickFrequency = 2000;
        }

        /// <summary>
        /// The API Key for the map view
        /// </summary>
        public ApplicationIdCredentialsProvider APIKey { get { return _apiKey; } set { SetProperty(ref _apiKey, value); } }
        
        /// <summary>
        /// A List of polylines to be shown on the map
        /// </summary>
        public List<MapPolyline> MapPolylines { get { return _mapPolylines; } private set { _mapPolylines = value; } }

        /// <summary>
        /// The maximum of the slider
        /// </summary>
        public int slMaximum
        {
            get { return _slMaximum; }
            set { SetProperty(ref _slMaximum, value); }
        }

        /// <summary>
        /// The location of the center of the map
        /// </summary>
        public Location MapCenter
        {
            get { return _mapCenter; }
            set { SetProperty(ref _mapCenter, value); }
        }

        /// <summary>
        /// The zoom level of the map
        /// </summary>
        public double MapZoomLevel
        {
            get { return _mapZoomLevel; }
            set { SetProperty(ref _mapZoomLevel, value); }
        }

        /// <summary>
        /// The tick frequency of the slider
        /// </summary>
        public int slTickFrequency
        {
            get { return _slTickFrequency; }
            set { SetProperty(ref _slTickFrequency, value); }
        }

        /// <summary>
        /// This function should be called right after the constructor
        /// </summary>
        public void Initialize()
        {
            if (_data.Route.Count != 0)
            {
                SliderValueChanged(0);
            }
        }

        /// <summary>
        /// This function should be called when the slider value changes
        /// </summary>
        /// <param name="value">The value of the slider</param>
        public void SliderValueChanged(int value)
        {
            MapPolylines = maker.CreateRoute(_data, value);

            RaiseEvent(new EventArgs());
        }

        private void RaiseEvent(EventArgs e)
        // This function raises the event
        {
            EventHandler handler = RouteChangedEvent;
            if (handler != null)
                handler.Invoke(this, e);
        }

        /// <summary>
        /// This function calculates the location of the map center
        /// </summary>
        public void CalculateCenter()
        {
            MapZoomLevel = 12.6;

            if (_data.Route.Count > 0)
                MapCenter = _data.Route.First();
            else
                MapCenter = new Location(48.236096, 14.188624);
        }
    }
}