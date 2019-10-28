using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CIDER
{
    public class RouteMaker
    {
        private static double earthRadius = 6367;

        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        public List<MapPolyline> CreateRoute(DataProvider _data)
        ///Summary
        ///This function creates the route and adds an arrow at the starting point
        {
            List<MapPolyline> mapPolylines = new List<MapPolyline>();

            MapPolyline polyline = new MapPolyline();

            SetupPolyline(polyline);

            polyline.Locations = _data.Route;

            mapPolylines.Clear();
            mapPolylines.Add(polyline);

            try
            {
                mapPolylines.Add(GetArrow(_data.Route.First(), _data.Route.ElementAt(1)));
            }
            catch (IndexOutOfRangeException ex)
            {
                logger.Debug(ex, "too little elements");
            }
            catch (InvalidOperationException ex)
            {
                logger.Debug(ex, "too little elements");
            }

            return mapPolylines;
        }

        public List<MapPolyline> CreateRoute(DataProvider _data, int NumberOfPoints)
        ///Summary
        ///This function creates the route up to the specified number of points and adds an arrow at the starting point
        {
            List<MapPolyline> mapPolylines = new List<MapPolyline>();

            MapPolyline polyline = new MapPolyline();

            SetupPolyline(polyline);

            LocationCollection locations = new LocationCollection();

            try
            {
                for (int i = 0; i < NumberOfPoints; i++)
                {
                    locations.Add(_data.Route.ElementAt(i));
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                logger.Debug(ex, "element not available");
            }

            polyline.Locations = locations;

            mapPolylines.Clear();
            mapPolylines.Add(polyline);

            try
            {
                mapPolylines.Add(GetArrow(_data.Route.First(), _data.Route.ElementAt(1)));
            }
            catch (IndexOutOfRangeException ex)
            {
                logger.Debug(ex, "too little elements");
            }
            catch (InvalidOperationException ex)
            {
                logger.Debug(ex, "too little elements");
            }

            return mapPolylines;
        }

        private void SetupPolyline(MapPolyline polyline)
        {
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.LawnGreen);
            polyline.StrokeThickness = 3.5;
            polyline.Opacity = 0.9;
        }

        private MapPolyline GetArrow(Location a, Location b)
        ///Summary
        ///This function creates the arrow
        {
            MapPolyline polyline = new MapPolyline();
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Aqua);
            polyline.StrokeThickness = 3.5;
            polyline.Opacity = 0.9;

            try
            {
                LocationCollection arrow = generatePolylinePointsWithArrow(a, b);

                polyline.Locations = arrow;
                return polyline;
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Failed creating arrow. Possibly single point only.");
                return new MapPolyline();
            }
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
            Tuple<double, double> arrowPoint1 = calculateCoord(anchorPoint, bearing - arrowAngle, arrowLength);
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