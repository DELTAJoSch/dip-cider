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
        private string _routeName;
        private DateTime _routeDate;
        private DateTime _routeStartTime;
        private DateTime _routeEndTime;
        private int _dataPoints;
        //private LocationCollection _route;
        private List<Tuple<float, float, float>> _angles;
        private List<Tuple<float, float, float>> _velocity;
        private bool _isValidRoute;

        public string RouteName { get { return _routeName; } set { _routeName = value; } }
        public DateTime RouteDate { get { return _routeDate; } set { _routeDate = value; } }
        public DateTime RouteStartTime { get { return _routeStartTime; } set { _routeStartTime = value; } }
        public DateTime RouteEndTime { get { return _routeEndTime; } set { _routeEndTime = value; } }
        public List<Tuple<float,float,float>> Angles { get { return _angles; } set { _angles = value; } }
        public List<Tuple<float,float,float>> Velocity { get { return _velocity; } set { _velocity = value; } }
        public bool IsValidRoute { get { return _isValidRoute; } set { _isValidRoute = value; } }
        public int DataPoints { get { return _dataPoints; } set { _dataPoints = value; } }
    }
}
