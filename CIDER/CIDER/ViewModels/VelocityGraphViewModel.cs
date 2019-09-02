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

            manager.AddLineSeries(_data.XVelocity, "F/B", OxyColors.IndianRed);
            manager.AddLineSeries(_data.YVelocity, "L/R", OxyColors.Indigo);
            manager.AddLineSeries(_data.ZVelocity, "U/D", OxyColors.Olive);

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
