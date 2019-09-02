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
            this._xAcceleration = new List<float>();
            this._xVelocity = new List<float>();
            this._yAcceleration = new List<float>();
            this._yVelocity = new List<float>();
            this._zAcceleration = new List<float>();
            this._zVelocity = new List<float>();
            this._pressure = new List<float>();
            this._height = new List<float>();
            this._route = new LocationCollection();
            _numberOfPoints = 0;
        }

        private string _routeName;
        private DateTime _routeDate;
        private DateTime _routeStartTime;
        private DateTime _routeEndTime;
        private int _dataPointsVelocity;
        private int _dataPointsAcceleration;
        private LocationCollection _route;
        private List<float> _pressure;
        private List<float> _height;
        private List<float> _xAcceleration;
        private List<float> _yAcceleration;
        private List<float> _zAcceleration;
        private List<float> _xVelocity;
        private List<float> _yVelocity;
        private List<float> _zVelocity;
        private bool _isValidRoute;
        private int _averageSattelitesInUse;
        private int _numberOfPoints;

        public string RouteName { get { return _routeName; } set { _routeName = value; } }
        public DateTime RouteDate { get { return _routeDate; } set { _routeDate = value; } }
        public DateTime RouteStartTime { get { return _routeStartTime; } set { _routeStartTime = value; } }
        public DateTime RouteEndTime { get { return _routeEndTime; } set { _routeEndTime = value; } }
        public bool IsValidRoute { get { return _isValidRoute; } set { _isValidRoute = value; } }
        public int DataPointsAcceleration { get { return _dataPointsAcceleration; } set { _dataPointsAcceleration = value; } }
        public int DataPointsVelocity { get { return _dataPointsVelocity; } set { _dataPointsVelocity = value; } }
        public List<float> Pressure { get { return _pressure; } set { _pressure = value; } }
        public List<float> XAcceleration { get { return _xAcceleration; } set { _xAcceleration = value; } }
        public List<float> YAcceleration { get { return _yAcceleration; } set { _yAcceleration = value; } }
        public List<float> ZAcceleration { get { return _zAcceleration; } set { _zAcceleration = value; } }
        public List<float> XVelocity { get { return _xVelocity; } set { _xVelocity = value; } }
        public List<float> YVelocity { get { return _yVelocity; } set { _yVelocity = value; } }
        public List<float> ZVelocity { get { return _zVelocity; } set { _zVelocity = value; } }
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

        internal bool ClearData()
        ///This allows the load function to clear the data. If if this is not done the data just keeps on being added to the end of the existing data - growing indefinitely
        ///The only other option to this would be to create a new object to write into and then do a deep copy into this object.
        {
            try
            {
                this._xAcceleration = new List<float>();
                this._xVelocity = new List<float>();
                this._yAcceleration = new List<float>();
                this._yVelocity = new List<float>();
                this._zAcceleration = new List<float>();
                this._zVelocity = new List<float>();
                this._pressure = new List<float>();
                this._height = new List<float>();
                this._route = new LocationCollection();
                this._numberOfPoints = 0;
                this._routeDate = new DateTime();
                this._routeEndTime = new DateTime();
                this._routeStartTime = new DateTime();
                this._isValidRoute = new bool();
                this._routeName = null;
                this._averageSattelitesInUse = new int();
                this._dataPointsAcceleration = new int();
                this._dataPointsVelocity = new int();

                return true;
            }catch(Exception)
            {
                return false;
            }
        }
    }
}
