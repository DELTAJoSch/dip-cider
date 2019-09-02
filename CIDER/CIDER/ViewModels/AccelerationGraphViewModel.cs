using CIDER.MVVMBase;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.ViewModels
{
    public class AccelerationGraphViewModel:ViewModelBase
    ///Summary
    ///This is the View Model for the AngleGraphViewModel
    ///TODO: Make loading async or do it in an extra thread
    {
        PlotModel _plot;
        DataProvider _data;

        public AccelerationGraphViewModel(DataProvider Data)
        {
            _data = Data;

            LoadPlot();
        }

        private async void LoadPlot()
        ///This code cannot be executed directly from the constructor as this is async
        ///Due to it being void, this code is executed "Fire and Forget"
        {
            //Create a new PlotModel, associate the corresponding DataPoints and display it
            PlotModel CreatePlot = new PlotModel();
            CreatePlot.Title = "Acceleration";

            var Task = GetLineSeriesAsync();

            LineSeries XSeries = new LineSeries();
            LineSeries YSeries = new LineSeries();
            LineSeries ZSeries = new LineSeries();

            XSeries.Color = OxyColor.FromRgb(38, 255, 0);
            YSeries.Color = OxyColor.FromRgb(255, 0, 230);
            ZSeries.Color = OxyColor.FromRgb(0, 242, 255);

            XSeries.Title = "F/B";
            YSeries.Title = "L/R";
            ZSeries.Title = "U/D";

            await Task;

            var res = Task.Result;

            XSeries.Points.AddRange(res.Item1);
            YSeries.Points.AddRange(res.Item2);
            ZSeries.Points.AddRange(res.Item3);

            CreatePlot.Series.Add(XSeries);
            CreatePlot.Series.Add(YSeries);
            CreatePlot.Series.Add(ZSeries);

            Plot = CreatePlot;

            Plot.InvalidatePlot(true);
        }

        private async Task<Tuple<List<DataPoint>, List<DataPoint>, List<DataPoint>>> GetLineSeriesAsync()
        ///This Task reads all the data points and convert them to line series
        ///It´s executed asynchronously, so this doesn't block when large datasets are involved
        {
            List<DataPoint> XData = new List<DataPoint>();
            List<DataPoint> YData = new List<DataPoint>();
            List<DataPoint> ZData = new List<DataPoint>();

            double t = 0;

            foreach (Tuple<float, float, float> tuple in _data.Acceleration)
            {
                XData.Add(new DataPoint(t, (double)tuple.Item1));
                YData.Add(new DataPoint(t, (double)tuple.Item2));
                ZData.Add(new DataPoint(t, (double)tuple.Item3));
                t++;
            }

            return new Tuple<List<DataPoint>, List<DataPoint>, List<DataPoint>>(XData,YData,ZData);
        }

        public PlotModel Plot
         //Data Binding for the Graph
        {
            get { return _plot; }
            set { SetProperty(ref _plot, value); }
        }
    }
}
