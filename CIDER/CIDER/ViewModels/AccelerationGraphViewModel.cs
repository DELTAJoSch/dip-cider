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

            PlotManager manager = new PlotManager();

            List<float> x = new List<float>();
            List<float> y = new List<float>();
            List<float> z = new List<float>();

            Parallel.ForEach(_data.Acceleration, tp =>
            {
                x.Add(tp.Item1);
                y.Add(tp.Item2);
                z.Add(tp.Item3);
            });

            manager.AddLineSeries(x, "F/B", OxyColors.Blue);
            manager.AddLineSeries(y, "L/R", OxyColors.Chartreuse);
            manager.AddLineSeries(z, "U/D", OxyColors.Gold);

            Plot = manager.GetPlotModel("Acceleration");
        }

        public PlotModel Plot
         //Data Binding for the Graph
        {
            get { return _plot; }
            set { SetProperty(ref _plot, value); }
        }
    }
}
