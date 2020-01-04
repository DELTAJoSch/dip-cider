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
using NUnit.Framework;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.UnitTests
{
    public class PlotManagerUnitTests
    {
        [Test]
        public void PlotManager_AddLineSeriesV1_AddsCorrectLineSeries()
        {
            PlotManager manager = new PlotManager();
            List<float> TestList = new List<float>();
            TestList.Add(1);
            TestList.Add(2);

            manager.AddLineSeries(TestList, "Test");

            Assert.AreEqual("Test", manager.Series.ElementAt(0).Title);
            Assert.AreEqual(new DataPoint(0, 1), manager.Series.ElementAt(0).Points.ElementAt(0));
            Assert.AreEqual(new DataPoint(1, 2), manager.Series.ElementAt(0).Points.ElementAt(1));
        }

        [Test]
        public void PlotManager_AddLineSeriesV2_AddsCorrectLineSeries()
        {
            PlotManager manager = new PlotManager();
            List<float> TestList = new List<float>();
            TestList.Add(1);
            TestList.Add(2);

            manager.AddLineSeries(TestList, "Test", 2);

            Assert.AreEqual("Test", manager.Series.ElementAt(0).Title);
            Assert.AreEqual(new DataPoint(0, 1), manager.Series.ElementAt(0).Points.ElementAt(0));
            Assert.AreEqual(new DataPoint(2, 2), manager.Series.ElementAt(0).Points.ElementAt(1));
        }

        [Test]
        public void PlotManager_AddLineSeriesV3_AddsCorrectLineSeries()
        {
            PlotManager manager = new PlotManager();
            List<float> TestList = new List<float>();
            TestList.Add(1);
            TestList.Add(2);

            manager.AddLineSeries(TestList, "Test", OxyColors.Aqua);

            Assert.AreEqual("Test", manager.Series.ElementAt(0).Title);
            Assert.AreEqual(new DataPoint(0, 1), manager.Series.ElementAt(0).Points.ElementAt(0));
            Assert.AreEqual(new DataPoint(1, 2), manager.Series.ElementAt(0).Points.ElementAt(1));
            Assert.AreEqual(OxyColors.Aqua, manager.Series.ElementAt(0).Color);
        }

        [Test]
        public void PlotManager_AddLineSeriesV4_AddsCorrectLineSeries()
        {
            PlotManager manager = new PlotManager();
            List<float> TestList = new List<float>();
            TestList.Add(1);
            TestList.Add(2);

            manager.AddLineSeries(TestList, "Test", OxyColors.Aqua, 2);

            Assert.AreEqual("Test", manager.Series.ElementAt(0).Title);
            Assert.AreEqual(new DataPoint(0, 1), manager.Series.ElementAt(0).Points.ElementAt(0));
            Assert.AreEqual(new DataPoint(2, 2), manager.Series.ElementAt(0).Points.ElementAt(1));
            Assert.AreEqual(OxyColors.Aqua, manager.Series.ElementAt(0).Color);
        }

        [Test]
        public void PlotManager_GetPlotModel_CreatesPlotModel()
        {
            PlotManager manager = new PlotManager();
            List<float> TestList = new List<float>();
            TestList.Add(1);
            TestList.Add(2);

            manager.AddLineSeries(TestList, "Test", OxyColors.Aqua, 2);
            PlotModel model = manager.GetPlotModel("Model").Result;

            Assert.AreEqual("Model", model.Title);
            Assert.AreEqual("Test", model.Series.ElementAt(0).Title);
        }
    }
}
