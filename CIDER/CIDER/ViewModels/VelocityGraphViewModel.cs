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
    public class VelocityGraphViewModel:ViewModelBase
    {
        private DataProvider _data;
        private PlotModel _plot;
        public VelocityGraphViewModel(DataProvider data)
        {
            _data = data;

            CreatePlot();
        }

        private async void CreatePlot()
        {
            Task<Tuple<List<DataPoint>, List<DataPoint>, List<DataPoint>>> getPoints = GetDataAsync();

            PlotModel model = new PlotModel();
            model.Title = "Velocity";

            LineSeries xSeries = new LineSeries();
            LineSeries ySeries = new LineSeries();
            LineSeries zSeries = new LineSeries();

            xSeries.Title = "F/B";
            ySeries.Title = "L/R";
            zSeries.Title = "U/D";

            xSeries.Color = OxyColor.FromRgb(252, 186, 3);
            ySeries.Color = OxyColor.FromRgb(3, 219, 252);
            zSeries.Color = OxyColor.FromRgb(252, 3, 78);

            await getPoints;

            var res = getPoints.Result;

            xSeries.Points.AddRange(res.Item1);
            ySeries.Points.AddRange(res.Item2);
            zSeries.Points.AddRange(res.Item3);

            model.Series.Add(xSeries);
            model.Series.Add(ySeries);
            model.Series.Add(zSeries);

            Plot = model;
        }

        private async Task<Tuple<List<DataPoint>, List<DataPoint>, List<DataPoint>>> GetDataAsync()
        {
            List<DataPoint> x = new List<DataPoint>();
            List<DataPoint> y = new List<DataPoint>();
            List<DataPoint> z = new List<DataPoint>();

            float t = 0;

            foreach (Tuple<float, float, float> data in _data.Velocity)
            {
                x.Add(new DataPoint(t, data.Item1));
                y.Add(new DataPoint(t, data.Item2));
                z.Add(new DataPoint(t, data.Item3));
                ++t;
            }

            return new Tuple<List<DataPoint>, List<DataPoint>, List<DataPoint>>(x, y, z);
        }
        public PlotModel Plot {
            get
            {
                return _plot;
            }
            set
            {
                SetProperty(ref _plot, value);
            }
        }
    }
}
