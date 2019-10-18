using CIDER.MVVMBase;
using OxyPlot;
using System;

namespace CIDER.ViewModels
{
    public class VelocityGraphViewModel : ViewModelBase, IDisposable
    {
        private DataProvider _data;
        private PlotModel _plot;
        private PlotModel blank;
        private PlotModel data;

        public VelocityGraphViewModel(DataProvider dataProvider)
        {
            _data = dataProvider;

            PlotManager manager = new PlotManager();

            manager.AddLineSeries(_data.Velocity, "Vel", OxyColors.IndianRed);

            data = manager.GetPlotModel("Velocity");
            blank = new PlotModel();
            blank.Title = "Velocity";
            Plot = data;

            MainWindow.OnResizeStartEvent += MainWindow_OnResizeStartEvent;
            MainWindow.OnResizeEndEvent += MainWindow_OnResizeEndEvent;
        }

        private void MainWindow_OnResizeEndEvent(object sender, EventArgs e)
        {
            Plot = data;
        }

        private void MainWindow_OnResizeStartEvent(object sender, EventArgs e)
        {
            Plot = blank;
        }

        public void Dispose()
        {
            MainWindow.OnResizeStartEvent -= MainWindow_OnResizeStartEvent;
            MainWindow.OnResizeEndEvent -= MainWindow_OnResizeEndEvent;
        }

        public PlotModel Plot
        {
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