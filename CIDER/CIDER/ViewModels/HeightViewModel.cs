using CIDER.MVVMBase;
using OxyPlot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.ViewModels
{
    public class HeightViewModel : ViewModelBase
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

        public HeightViewModel(DataProvider data)
        {
            _data = data;

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
            manager.AddLineSeries(_data.Height, "Height",OxyColors.Coral);
            Plot = manager.GetPlotModel("Height");
        }
        public float HeightMaxL { get { return _heightMaxL; } set { SetProperty(ref _heightMaxL, value); } }
        public float HeightMaxR { get { return _heightMaxR; } set { SetProperty(ref _heightMaxR, value); } }
        public float HeightValL { get { return _heightValL; } set { SetProperty(ref _heightValL, value); } }
        public float HeightValR { get { return _heightValR; } set { SetProperty(ref _heightValR, value); } }
        public float slTickFrequency { get { return _slTickFrequency; } set { SetProperty(ref _slTickFrequency, value); } }
        public float slMaximum { get { return _slMaximum; } set { SetProperty(ref _slMaximum, value); } }
        public string HeightText { get { return _heightText; } set { SetProperty(ref _heightText, value); } }
        public PlotModel Plot { get { return _plot; } set { SetProperty(ref _plot, value); } }

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
            catch(IndexOutOfRangeException ex)
            {
                logger.Debug(ex, "no height data");
            }
            catch(Exception ex)
            {
                logger.Info(ex, "Error whilst loading Data");
            }
        }
    }
}
