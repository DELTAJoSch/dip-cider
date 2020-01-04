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
using Microsoft.Maps.MapControl.WPF;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CIDER.UnitTests
{
    [TestFixture, Apartment(ApartmentState.STA)]
    public class RouteMakerUnitTests
    {
        private static double earthRadius = 6367;

        [Test]
        public void RouteMaker_CreateRouteFull_CreatesRoute()
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                throw new ThreadStateException("The current threads apartment state is not STA");
            }

            DataProvider data = Factories.GetRouteData();

            RouteMaker maker = new RouteMaker();
            var testData = maker.CreateRoute(data);

            List<Microsoft.Maps.MapControl.WPF.MapPolyline> locations = new List<Microsoft.Maps.MapControl.WPF.MapPolyline>();

            var arrow = new Microsoft.Maps.MapControl.WPF.MapPolyline();
            var line = new Microsoft.Maps.MapControl.WPF.MapPolyline();

            Setup(line);

            line.Locations = data.Route;

            arrow = GetArrow(data.Route.First(), data.Route.ElementAt(1)); 

            locations.Add(line);
            locations.Add(arrow);

            Assert.IsTrue(CompareMapPolylines.compare(locations.ElementAt(0), testData.ElementAt(0)));
            Assert.IsTrue(CompareMapPolylines.compare(locations.ElementAt(1), testData.ElementAt(1)));
        }

        [Test]
        public void RouteMaker_CreateRoutePoint_CreatesRoute()
        {
            if (Thread.CurrentThread.GetApartmentState() != ApartmentState.STA)
            {
                throw new ThreadStateException("The current threads apartment state is not STA");
            }

            DataProvider data = Factories.GetRouteData();

            RouteMaker maker = new RouteMaker();
            var testData = maker.CreateRoute(data, 3);

            List<Microsoft.Maps.MapControl.WPF.MapPolyline> locations = new List<Microsoft.Maps.MapControl.WPF.MapPolyline>();

            MapPolyline arrow = new Microsoft.Maps.MapControl.WPF.MapPolyline();
            MapPolyline line = new Microsoft.Maps.MapControl.WPF.MapPolyline();

            Setup(line);

            line.Locations = new LocationCollection();

            for(int i = 0; i < 3; i++)
            {
                var loc = data.Route.ElementAt(i);
                line.Locations.Add(new Location(loc));
            }

            arrow = GetArrow(data.Route.First(), data.Route.ElementAt(1));

            locations.Add(line);
            locations.Add(arrow);

            Assert.IsTrue(CompareMapPolylines.compare(locations.ElementAt(0), testData.ElementAt(0)));
            Assert.IsTrue(CompareMapPolylines.compare(locations.ElementAt(1), testData.ElementAt(1)));
        }

        private static void Setup(Microsoft.Maps.MapControl.WPF.MapPolyline polyline)
        {
            polyline.Stroke = new System.Windows.Media.SolidColorBrush(System.Windows.Media.Colors.Orange);
            polyline.StrokeThickness = 3.5;
            polyline.Opacity = 0.9;
        }

        private static MapPolyline GetArrow(Location a, Location b)
        //  Summary
        //  This function creates the arrow
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
                return new MapPolyline();
            }
        }

        private static LocationCollection generatePolylinePointsWithArrow(Location anchor, Location towards)
        //  Summary:
        //  This code was adapted and translated from https://rbrundritt.wordpress.com/2009/05/31/drawing-arrow-heads-on-polylines/
        {
            LocationCollection arr = new LocationCollection();

            //  last point in polyline array
            Location anchorPoint = anchor;
            //  bearing from first point to second point in pointline array
            double bearing = calculateBearing(anchorPoint, towards);
            //  length of arrow head lines in km
            double arrowLength = 0.15;
            //  angle of arrow lines relative to polyline in degrees
            double arrowAngle = 40;
            //  calculate coordinates of arrow tips
            Tuple<double, double> arrowPoint1 = calculateCoord(anchorPoint, bearing - arrowAngle, arrowLength);
            Tuple<double, double> arrowPoint2 = calculateCoord(anchorPoint, bearing + arrowAngle, arrowLength);
            //  go from last point in polyline to one arrow tip, then back to the
            //  last point then to the second arrow tip.

            arr.Add(new Location(arrowPoint1.Item1, arrowPoint1.Item2));
            arr.Add(anchorPoint);
            arr.Add(new Location(arrowPoint2.Item1, arrowPoint2.Item2));

            return arr;
        }

        private static Tuple<double, double> calculateCoord(Location origin, double brng, double arcLength)
        {
            double lat1 = ExtraMath.DegToRad(origin.Latitude);
            double lon1 = ExtraMath.DegToRad(origin.Longitude);

            double centralAngle = arcLength / earthRadius;

            double lat2 = Math.Asin(Math.Sin(lat1) * Math.Cos(centralAngle) + Math.Cos(lat1) * Math.Sin(centralAngle) * Math.Cos(ExtraMath.DegToRad(brng)));
            double lon2 = lon1 + Math.Atan2(Math.Sin(ExtraMath.DegToRad(brng)) * Math.Sin(centralAngle) * Math.Cos(lat1), Math.Cos(centralAngle) - Math.Sin(lat1) * Math.Sin(lat2));

            return new Tuple<double, double>(ExtraMath.RadToDeg(lat2), ExtraMath.RadToDeg(lon2));
        }

        private static double calculateBearing(Location A, Location B)
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

    public static class CompareMapPolylines
    {
        public static bool compare(MapPolyline polylineOne, MapPolyline polylineTwo)
        {
            for (int i = 0; i < polylineOne.Locations.Count; i++)
            {
                if (polylineOne.Locations.ElementAt(i) != polylineTwo.Locations.ElementAt(i))
                    return false;
            }

            return true;
        }
    }
}
