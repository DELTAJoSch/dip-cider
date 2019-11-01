using CIDER.MVVMBase;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIDER.ViewModels
{
    /// <summary>
    /// This is the ViewModel of the MapRoute page
    /// </summary>
    public class MapRouteViewModel : ViewModelBase
    {
        private DataProvider _data;
        private ApplicationIdCredentialsProvider _apiKey;

        /// <summary>
        /// This event is raised when the route changes
        /// </summary>
        public event EventHandler RouteChangedEvent;

        private List<MapPolyline> _mapPolylines;
        private RouteMaker maker;

        /// <summary>
        /// This is the constructor for the MapRouteViewModel
        /// </summary>
        /// <param name="data">A DataProvider object to read the data from</param>
        public MapRouteViewModel(DataProvider data)
        {
            _data = data;

            _mapPolylines = new List<MapPolyline>();

            maker = new RouteMaker();

            MapZoomLevel = 12.6;

            if (_data.Route.Count > 0)
                MapCenter = _data.Route.First();
            else
                MapCenter = new Location(48.236096, 14.188624);

            //  set the api key read from the key file
            APIKey = new ApplicationIdCredentialsProvider(data.APIKey);
        }

        /// <summary>
        /// This contains the APIKey for the map
        /// </summary>
        public ApplicationIdCredentialsProvider APIKey { get { return _apiKey; } set { SetProperty(ref _apiKey, value); } }

        /// <summary>
        /// This is a list of polylines to display on the map
        /// </summary>
        public List<MapPolyline> MapPolylines { get { return _mapPolylines; } private set { _mapPolylines = value; } }

        /// <summary>
        /// This contains the map center
        /// </summary>
        public Location MapCenter;

        /// <summary>
        /// This contains the zoom level of the map
        /// </summary>
        public double MapZoomLevel;

        /// <summary>
        /// This function draws the route. It needs to be called after the constructor finishes
        /// </summary>
        public void Initialize()
        {
            _mapPolylines = maker.CreateRoute(_data);
            RaiseEvent(new EventArgs());
        }

        /// <summary>
        /// This function calculates the center of the map
        /// </summary>
        public void CalculateCenter()
        {
            MapZoomLevel = 12.6;

            if (_data.Route.Count > 0)
                MapCenter = _data.Route.First();
            else
                MapCenter = new Location(48.236096, 14.188624);
        }

        private void RaiseEvent(EventArgs e)
        //  This function raises the event
        {
            EventHandler handler = RouteChangedEvent;
            if (handler != null)
                handler.Invoke(this, e);
        }
    }
}