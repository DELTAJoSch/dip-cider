using CIDER.MVVMBase;
using OxyPlot;
using System;

namespace CIDER.ViewModels
{
    /// <summary>
    /// The ViewModel for the acceleration graph
    /// </summary>
    public class AccelerationGraphViewModel : ViewModelBase, IDisposable
    {
        private PlotModel _plot;
        private PlotModel data;
        private PlotModel blank;
        private DataProvider _data;

        /// <summary>
        /// This is the constructor for the AccelerationGraphViewModel
        /// </summary>
        /// <param name="dataProvider">A DataProvider object to read the data from</param>
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

        /// <summary>
        /// As this class implements the IDisposable interface this function needs to be called before the GC can clean up an instance of this class
        /// </summary>
        public void Dispose()
        {
            MainWindow.OnResizeStartEvent -= MainWindow_OnResizeStartEvent;
            MainWindow.OnResizeEndEvent -= MainWindow_OnResizeEndEvent;
        }

        /// <summary>
        /// This contains the PlotModel to be displayed by the plot
        /// </summary>
        public PlotModel Plot
        {
            get { return _plot; }
            set { SetProperty(ref _plot, value); }
        }
    }
}