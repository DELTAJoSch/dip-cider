using CIDER.MVVMBase;
using System;
using System.Linq;

namespace CIDER.ViewModels
{
    public class VelocityTimedViewModel : ViewModelBase
    {
        private DataProvider _data;
        private int _slMaximum;
        private int _slTickFrequency;
        private float _rVal;
        private float _lVal;
        private float _rMax;
        private float _lMax;
        private string _Text;

        public VelocityTimedViewModel(DataProvider data)
        {
            _data = data;

            slMaximum = _data.DataPointsVelocity - 1;
            if (slMaximum < 1000)
                slTickFrequency = 2;
            if (slMaximum > 1000 && slMaximum < 10000)
                slTickFrequency = 10;
            if (slMaximum > 10000 && slMaximum < 1000000)
                slTickFrequency = 500;
            if (slMaximum > 1000000)
                slTickFrequency = 2000;

            if ((_data.DataPointsVelocity == 0) == false)
            {
                SliderValueChanged(0);
                RMax = LMax = _data.Velocity.Max() + 10;
            }
            else
            {
                Text = "Velocity";
                RMax = LMax = 10;
            }
        }

        public int slMaximum
        {
            get { return _slMaximum; }
            set { SetProperty(ref _slMaximum, value); }
        }

        public int slTickFrequency
        {
            get { return _slTickFrequency; }
            set { SetProperty(ref _slTickFrequency, value); }
        }

        public void SliderValueChanged(int value)
        {
            float x = _data.Velocity.ElementAt(value);

            if (x < 0)
            {
                LVal = 0;
                RVal = -x;
            }
            else
            {
                LVal = x;
                RVal = 0;
            }

            Text = String.Format("Velocity: {0} m/s", x);
        }

        //The following are the Data Bindings for the values
        public float RVal
        {
            get { return _rVal; }
            set { SetProperty(ref _rVal, value); }
        }

        public float LVal
        {
            get { return _lVal; }
            set { SetProperty(ref _lVal, value); }
        }

        //The following are the Data Bindings for the maximums
        public float RMax
        {
            get { return _rMax; }
            set { SetProperty(ref _rMax, value); }
        }

        public float LMax
        {
            get { return _lMax; }
            set { SetProperty(ref _lMax, value); }
        }

        public string Text
        {
            get { return _Text; }
            set { SetProperty(ref _Text, value); }
        }
    }
}