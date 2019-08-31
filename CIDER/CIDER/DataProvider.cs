using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER
{
    public class DataProvider
    /*/Summary
     * This class holds all the values used by the program concerning the current route
    /*/
    {
        public DataProvider()
        {
            this._angles = new List<Tuple<float, float, float>>();
            this._velocity = new List<Tuple<float, float, float>>();
            this._pressure = new List<float>();
            this._height = new List<float>();
            this._route = new LocationCollection();
            _numberOfPoints = 0;
        }

        private string _routeName;
        private DateTime _routeDate;
        private DateTime _routeStartTime;
        private DateTime _routeEndTime;
        private int _dataPoints;
        private LocationCollection _route;
        private List<float> _pressure;
        private List<float> _height;
        private List<Tuple<float, float, float>> _angles;
        private List<Tuple<float, float, float>> _velocity;
        private bool _isValidRoute;
        private int _averageSattelitesInUse;
        private int _numberOfPoints;

        public string RouteName { get { return _routeName; } set { _routeName = value; } }
        public DateTime RouteDate { get { return _routeDate; } set { _routeDate = value; } }
        public DateTime RouteStartTime { get { return _routeStartTime; } set { _routeStartTime = value; } }
        public DateTime RouteEndTime { get { return _routeEndTime; } set { _routeEndTime = value; } }
        public List<Tuple<float,float,float>> Angles { get { return _angles; } set { _angles = value; } }
        public List<Tuple<float,float,float>> Velocity { get { return _velocity; } set { _velocity = value; } }
        public bool IsValidRoute { get { return _isValidRoute; } set { _isValidRoute = value; } }
        public int DataPoints { get { return _dataPoints; } set { _dataPoints = value; } }
        public List<float> Pressure { get { return _pressure; } set { _pressure = value; } }
        public List<float> Height { get { return _height; } set { _height = value; } }
        public LocationCollection Route { get { return _route; } set { _route = value; } }
        public int AverageSattelitesInUse { get { return _averageSattelitesInUse; } set
            {
                if (_numberOfPoints == 0)
                    _averageSattelitesInUse = value;
                else
                {
                    float a = _numberOfPoints * _averageSattelitesInUse;
                    _numberOfPoints++;
                    float res = (a + value) / _numberOfPoints;
                    _averageSattelitesInUse = (int)res;
                }
            }
        }
    }
}
