using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;

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
            this._yAcceleration = new List<float>();
            this._zAcceleration = new List<float>();
            this._pressure = new List<float>();
            this._height = new List<float>();
            this._Velocity = new List<float>();
            this._heading = new List<float>();
            this._yaw = new List<float>();
            this._roll = new List<float>();
            this._pitch = new List<float>();
            this._route = new LocationCollection();
            this._numberOfPoints = 0;
        }

        private string _routeName;
        private DateTime _routeDate;
        private DateTime _routeStartTime;
        private DateTime _routeEndTime;
        private int _dataPointsVelocity;
        private int _dataPointsAngle;
        private int _dataPointsAcceleration;
        private LocationCollection _route;
        private List<float> _pressure;
        private List<float> _height;
        private List<float> _xAcceleration;
        private List<float> _yAcceleration;
        private List<float> _zAcceleration;
        private List<float> _roll;
        private List<float> _pitch;
        private List<float> _yaw;
        private List<float> _Velocity;
        private List<float> _heading;
        private bool _isValidRoute;
        private int _averageSattelitesInUse;
        private int _numberOfPoints;
        private string _apiKey;

        public string RouteName { get { return _routeName; } set { _routeName = value; } }
        public string APIKey { get { return _apiKey; } set { _apiKey = value; } }
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
        public List<float> Roll { get { return _roll; } set { _roll = value; } }
        public List<float> Pitch { get { return _pitch; } set { _pitch = value; } }
        public List<float> Yaw { get { return _yaw; } set { _yaw = value; } }
        public int DataPointsAngle { get { return _dataPointsAngle; } set { _dataPointsAngle = value; } }
        public List<float> Heading { get { return _heading; } set { _heading = value; } }
        public List<float> Velocity { get { return _Velocity; } set { _Velocity = value; } }
        public List<float> Height { get { return _height; } set { _height = value; } }
        public LocationCollection Route { get { return _route; } set { _route = value; } }

        public int AverageSattelitesInUse
        {
            get { return _averageSattelitesInUse; }
            set
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
                this._yAcceleration = new List<float>();
                this._zAcceleration = new List<float>();
                this._yaw = new List<float>();
                this._roll = new List<float>();
                this._pitch = new List<float>();
                this._Velocity = new List<float>();
                this._pressure = new List<float>();
                this._height = new List<float>();
                this._route = new LocationCollection();
                this._numberOfPoints = 0;
                this._dataPointsAngle = new int();
                this._routeDate = new DateTime();
                this._routeEndTime = new DateTime();
                this._routeStartTime = new DateTime();
                this._isValidRoute = new bool();
                this._routeName = null;
                this._averageSattelitesInUse = new int();
                this._dataPointsAcceleration = new int();
                this._dataPointsVelocity = new int();

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}