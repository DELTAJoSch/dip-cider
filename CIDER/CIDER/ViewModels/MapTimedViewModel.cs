using CIDER.MVVMBase;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.ViewModels
{
    public class MapTimedViewModel : ViewModelBase
    {
        private ApplicationIdCredentialsProvider _apiKey;
        private DataProvider _data;
        private List<MapPolyline> _mapPolylines;
        private int _slMaximum;
        private int _slTickFrequency;
        private RouteMaker maker;

        public event EventHandler RouteChangedEvent;

        public MapTimedViewModel(DataProvider data)
        {
            _data = data;

            maker = new RouteMaker();
            _mapPolylines = new List<MapPolyline>();

            //set the api key read from the key file
            APIKey = new ApplicationIdCredentialsProvider(data.APIKey);

            slMaximum = _data.DataPointsAcceleration - 1;
            if (slMaximum < 1000)
                slTickFrequency = 2;
            if (slMaximum > 1000 && slMaximum < 10000)
                slTickFrequency = 10;
            if (slMaximum > 10000 && slMaximum < 1000000)
                slTickFrequency = 500;
            if (slMaximum > 1000000)
                slTickFrequency = 2000;
        }

        public ApplicationIdCredentialsProvider APIKey { get { return _apiKey; } set { SetProperty(ref _apiKey, value); } }
        public List<MapPolyline> MapPolylines { get { return _mapPolylines; } private set { _mapPolylines = value; } }

        public int slMaximum
        {
            get { return _slMaximum; }
            set { SetProperty(ref _slMaximum, value); }
        }
        public int slTickFrequency
        {
            get { return _slTickFrequency; }
            set { SetProperty(ref _slTickFrequency, value); }
        }
        public void Init()
        {
            if (_data.Route.Count != 0)
            {
                UpdateRoute(0);
            }
        }

        public void UpdateRoute(int value)
        ///Summary
        ///Called when the sldider value changed
        {
            MapPolylines =  maker.CreateRoute(_data, value);

            RaiseEvent(new EventArgs());
        }
        private void RaiseEvent(EventArgs e)
        ///Summary
        ///This function raises the event
        {
            EventHandler handler = RouteChangedEvent;
            if (handler != null)
                handler.Invoke(this, e);
        }
    }
}
