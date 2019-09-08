﻿using CIDER.MVVMBase;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.ViewModels
{
    public class MapRouteViewModel:ViewModelBase
    ///Summary
    ///This is the viewmodel for the map route view
    {
        DataProvider _data;
        private ApplicationIdCredentialsProvider _apiKey;
        public event EventHandler RouteChangedEvent;
        private List<MapPolyline> _mapPolylines;
        private RouteMaker maker;

        public MapRouteViewModel(DataProvider data)
        {
            _data = data;

            _mapPolylines = new List<MapPolyline>();

            maker = new RouteMaker();

            //set the api key read from the key file
            APIKey = new ApplicationIdCredentialsProvider(data.APIKey);
        }

        public ApplicationIdCredentialsProvider APIKey { get { return _apiKey; } set { SetProperty(ref _apiKey, value); } }
        public List<MapPolyline> MapPolylines { get { return _mapPolylines; } private set { _mapPolylines = value; } }

        public void Initialize()
        ///Summary
        ///This function draws the route. It needs to be called after the constructor finishes
        {
            _mapPolylines = maker.CreateRoute(_data);
            RaiseEvent(new EventArgs());
        }

       

        private void RaiseEvent(EventArgs e)
        ///Summary
        ///This function raises the event
        {
            EventHandler handler = RouteChangedEvent;
            if(handler!=null)
                handler.Invoke(this, e);
        }
    }
}
