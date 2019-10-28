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
            PlotModel model = manager.GetPlotModel("Model");

            Assert.AreEqual("Model", model.Title);
            Assert.AreEqual("Test", model.Series.ElementAt(0).Title);
        }
    }
}
