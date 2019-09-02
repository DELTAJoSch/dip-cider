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

            PlotManager manager = new PlotManager();

            List<float> x = new List<float>();
            List<float> y = new List<float>();
            List<float> z = new List<float>();

            Parallel.ForEach(_data.Velocity, tp =>
            {
                x.Add(tp.Item1);
                y.Add(tp.Item2);
                z.Add(tp.Item3);
            });

            manager.AddLineSeries(x, "F/B", OxyColors.IndianRed);
            manager.AddLineSeries(y, "L/R", OxyColors.Indigo);
            manager.AddLineSeries(z, "U/D", OxyColors.Olive);

            Plot = manager.GetPlotModel("Velocity");
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
