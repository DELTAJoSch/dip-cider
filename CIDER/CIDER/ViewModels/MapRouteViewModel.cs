using CIDER.MVVMBase;
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
        private double earthRadius = 6367;

        public MapRouteViewModel(DataProvider data)
        {
            _data = data;

            _mapPolylines = new List<MapPolyline>();

            //set the api key read from the key file
            APIKey = new ApplicationIdCredentialsProvider(data.APIKey);
        }

        public ApplicationIdCredentialsProvider APIKey { get { return _apiKey; } set { SetProperty(ref _apiKey, value); } }
        public List<MapPolyline> MapPolylines { get { return _mapPolylines; } private set { _mapPolylines = value; } }

        public void Initialize()
        ///Summary
        ///This function draws the route. It needs to be called after the constructor finishes
        {
            CreateRoute();
        }

        private void CreateRoute()
        ///Summary
        ///This function creates the route and adds an arrow at the starting point
        {
            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LawnGreen);
            polyline.StrokeThickness = 3.5;
            polyline.Opacity = 0.9;
            polyline.Locations = _data.Route;

            MapPolylines.Clear();
            MapPolylines.Add(polyline);

            GetArrow();

            RaiseEvent(new EventArgs());
        }

        private void GetArrow()
        ///Summary
        ///This function creates the arrow
        {
            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Aqua);
            polyline.StrokeThickness = 3.5;
            polyline.Opacity = 0.9;

            try
            {
                Location a = _data.Route.First();
                Location b = _data.Route.ElementAt(1);

                LocationCollection arrow = generatePolylinePointsWithArrow(a, b);

                polyline.Locations = arrow;
                MapPolylines.Add(polyline);
            }catch(Exception ex)
            {
                logger.Warn(ex, "Failed creating arrow. Possibly single point only.");
            }
        }

        private void RaiseEvent(EventArgs e)
        ///Summary
        ///This function raises the event
        {
            EventHandler handler = RouteChangedEvent;
            if(handler!=null)
                handler.Invoke(this, e);
        }

        private LocationCollection generatePolylinePointsWithArrow(Location anchor, Location towards)
        ///Summary:
        ///This code was adapted and translated from https://rbrundritt.wordpress.com/2009/05/31/drawing-arrow-heads-on-polylines/
        {
            LocationCollection arr = new LocationCollection();

            //last point in polyline array
            Location anchorPoint = anchor;
            //bearing from first point to second point in pointline array
            double bearing = calculateBearing(anchorPoint, towards);
            //length of arrow head lines in km
            double arrowLength = 0.15;
            //angle of arrow lines relative to polyline in degrees
            double arrowAngle = 40;
            //calculate coordinates of arrow tips
            Tuple<double,double> arrowPoint1 = calculateCoord(anchorPoint, bearing - arrowAngle, arrowLength);
            Tuple<double, double> arrowPoint2 = calculateCoord(anchorPoint, bearing + arrowAngle, arrowLength);
            //go from last point in polyline to one arrow tip, then back to the 
            //last point then to the second arrow tip.

            arr.Add(new Location(arrowPoint1.Item1, arrowPoint1.Item2));
            arr.Add(anchorPoint);
            arr.Add(new Location(arrowPoint2.Item1, arrowPoint2.Item2));

            return arr;
        }
        private Tuple<double, double> calculateCoord(Location origin, double brng, double arcLength)
        {
            double lat1 = ExtraMath.DegToRad(origin.Latitude);
            double lon1 = ExtraMath.DegToRad(origin.Longitude);

            double centralAngle = arcLength / earthRadius;

            double lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos(centralAngle) + Math.Cos(lat1) * Math.Sin(centralAngle) * Math.Cos(ExtraMath.DegToRad(brng)));
            double lon2 = lon1 + Math.Atan2(Math.Sin(ExtraMath.DegToRad(brng)) * Math.Sin(centralAngle) * Math.Cos(lat1), Math.Cos(centralAngle) - Math.Sin(lat1) * Math.Sin(lat2));

            return new Tuple<double, double>(ExtraMath.RadToDeg(lat2), ExtraMath.RadToDeg(lon2));
        }
        private double calculateBearing(Location A, Location B)
        {
            double lat1 = ExtraMath.DegToRad(B.Latitude);
            double lon1 = B.Longitude;
            double lat2 = ExtraMath.DegToRad(A.Latitude);
            double lon2 = A.Longitude;

            double dLon = ExtraMath.DegToRad(lon2 - lon1);

            double y = Math.Sin(dLon) * Math.Cos(lat2);
            double x = Math.Cos(lat1) * Math.Sin(lat2) - Math.Sin(lat1) * Math.Cos(lat2) * Math.Cos(dLon);

            double brng = (ExtraMath.RadToDeg(Math.Atan2(y, x)) + 360) % 360;

            return brng;
        }
    }
}
