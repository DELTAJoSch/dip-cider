using CIDER.MVVMBase;
using OxyPlot;
using System;

namespace CIDER.ViewModels
{
    public class AccelerationGraphViewModel : ViewModelBase, IDisposable
    ///Summary
    ///This is the View Model for the AngleGraphViewModel
    ///TODO: Make loading async or do it in an extra thread
    {
        private PlotModel _plot;
        private PlotModel data;
        private PlotModel blank;
        private DataProvider _data;

        public AccelerationGraphViewModel(DataProvider dataProvider)
        {
            _data = dataProvider;

            PlotManager manager = new PlotManager();

            manager.AddLineSeries(_data.XAcceleration, "F/B", OxyColors.Blue);
            manager.AddLineSeries(_data.YAcceleration, "L/R", OxyColors.Chartreuse);
            manager.AddLineSeries(_data.ZAcceleration, "U/D", OxyColors.Gold);

            data = manager.GetPlotModel("Acceleration");
            blank = new PlotModel();
            blank.Title = "Acceleration";
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
        //Data Binding for the Graph
        {
            get { return _plot; }
            set { SetProperty(ref _plot, value); }
        }
    }
}