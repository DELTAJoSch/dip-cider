using CIDER.MVVMBase;
using OxyPlot;
using System;
using System.Linq;

namespace CIDER.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the Height page
    /// </summary>
    public class HeightViewModel : ViewModelBase, IDisposable
    {
        private float _heightMaxL;
        private float _heightMaxR;
        private float _heightValL;
        private float _heightValR;
        private string _heightText;
        private PlotModel _plot;
        private float _slMaximum;
        private float _slTickFrequency;
        private DataProvider _data;
        private PlotModel data;
        private PlotModel blank;

        /// <summary>
        /// This is the constructor for the HeightViewModel page
        /// </summary>
        /// <param name="dataProvider">A DataProvider object to read the data from</param>
        public HeightViewModel(DataProvider dataProvider)
        {
            _data = dataProvider;

            slMaximum = _data.Height.Count - 1;
            if (slMaximum < 1000)
                slTickFrequency = 2;
            if (slMaximum > 1000 && slMaximum < 10000)
                slTickFrequency = 10;
            if (slMaximum > 10000 && slMaximum < 1000000)
                slTickFrequency = 500;
            if (slMaximum > 1000000)
                slTickFrequency = 2000;

            if (_data.Height.Count != 0)
            {
                slValueChanged(0);
                HeightMaxL = HeightMaxR = _data.Height.Max() + 10;
            }
            else
            {
                HeightText = "Height";
                HeightMaxR = HeightMaxL = 10;
            }

            PlotManager manager = new PlotManager();
            manager.AddLineSeries(_data.Height, "Height", OxyColors.Coral);

            data = manager.GetPlotModel("Height");
            blank = new PlotModel();
            blank.Title = "Height";
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
        /// This function needs to be called before the object is dereferenced so the GC can collect it
        /// </summary>
        public void Dispose()
        {
            MainWindow.OnResizeStartEvent -= MainWindow_OnResizeStartEvent;
            MainWindow.OnResizeEndEvent -= MainWindow_OnResizeEndEvent;
        }

        /// <summary>
        /// This contains the maximum of the value of the left height progress bar
        /// </summary>
        public float HeightMaxL { get { return _heightMaxL; } set { SetProperty(ref _heightMaxL, value); } }

        /// <summary>
        /// This contains the maximum of the value of the right height progress bar
        /// </summary>
        public float HeightMaxR { get { return _heightMaxR; } set { SetProperty(ref _heightMaxR, value); } }

        /// <summary>
        /// This contains the value of the left height progress bar
        /// </summary>
        public float HeightValL { get { return _heightValL; } set { SetProperty(ref _heightValL, value); } }

        /// <summary>
        /// This contains the value of the right height progress bar
        /// </summary>
        public float HeightValR { get { return _heightValR; } set { SetProperty(ref _heightValR, value); } }

        /// <summary>
        /// This contains the tick frequency of the slider
        /// </summary>
        public float slTickFrequency { get { return _slTickFrequency; } set { SetProperty(ref _slTickFrequency, value); } }

        /// <summary>
        /// This contains the maximum of the slider
        /// </summary>
        public float slMaximum { get { return _slMaximum; } set { SetProperty(ref _slMaximum, value); } }

        /// <summary>
        /// This contains the text to be displayed next to the height progress bar
        /// </summary>
        public string HeightText { get { return _heightText; } set { SetProperty(ref _heightText, value); } }

        /// <summary>
        /// This contains the Plot to be shown in the plot area
        /// </summary>
        public PlotModel Plot { get { return _plot; } set { SetProperty(ref _plot, value); } }

        /// <summary>
        /// This function should be called when the slider value changes
        /// </summary>
        /// <param name="value">The value of the slider</param>
        public void slValueChanged(int value)
        {
            try
            {
                var x = _data.Height.ElementAt(value);

                if (x < 0)
                {
                    HeightValR = -x;
                    HeightValL = 0;
                }
                else
                {
                    HeightValL = x;
                    HeightValR = 0;
                }

                HeightText = $"Height: {x} Fuß";
            }
            catch (IndexOutOfRangeException ex)
            {
                logger.Debug(ex, "no height data");
            }
            catch (Exception ex)
            {
                logger.Info(ex, "Error whilst loading Data");
            }
        }
    }
}