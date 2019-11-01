using CIDER.MVVMBase;
using OxyPlot;
using System;

namespace CIDER.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the VelocityGraph page
    /// </summary>
    public class VelocityGraphViewModel : ViewModelBase, IDisposable
    {
        private DataProvider _data;
        private PlotModel _plot;
        private PlotModel blank;
        private PlotModel data;

        /// <summary>
        /// This is the constructor of the VelocityGraphViewModel
        /// </summary>
        /// <param name="dataProvider">A DataProvider object to read the data from</param>
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

        /// <summary>
        /// This function needs to be called before dereferencing an instance of this class so the GC can collect it
        /// </summary>
        public void Dispose()
        {
            MainWindow.OnResizeStartEvent -= MainWindow_OnResizeStartEvent;
            MainWindow.OnResizeEndEvent -= MainWindow_OnResizeEndEvent;
        }

        /// <summary>
        /// This contains the plot to be shown
        /// </summary>
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