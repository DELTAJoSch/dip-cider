using CIDER.MVVMBase;
using System;
using System.Linq;

namespace CIDER.ViewModels
{
    public class AccelerationTimedViewModel : ViewModelBase
    ///Summary
    ///This is the ViewModel for the AngleTimedView
    {
        private DataProvider _data;

        //private Data, all corresponding to the bindings
        private int _slMaximum;

        private int _slTickFrequency;
        private float _rValUD;
        private float _lValUD;
        private float _rValFB;
        private float _lValFB;
        private float _rValLR;
        private float _lValLR;
        private float _rMaxUD;
        private float _lMaxUD;
        private float _rMaxFB;
        private float _lMaxFB;
        private float _rMaxLR;
        private float _lMaxLR;
        private string _udText;
        private string _fbText;
        private string _lrText;

        public AccelerationTimedViewModel(DataProvider data)
        {
            _data = data;

            slMaximum = _data.DataPointsAcceleration - 1;
            if (slMaximum < 1000)
                slTickFrequency = 2;
            if (slMaximum > 1000 && slMaximum < 10000)
                slTickFrequency = 10;
            if (slMaximum > 10000 && slMaximum < 1000000)
                slTickFrequency = 500;
            if (slMaximum > 1000000)
                slTickFrequency = 2000;

            RMaxFB = LMaxFB = RMaxLR = LMaxLR = RMaxUD = LMaxUD = 400;

            if ((_data.DataPointsAcceleration == 0) == false)
            {
                SliderValueChanged(0);
            }
            else
            {
                FBText = "Forwards/Backwards";
                UDText = "Up/Down";
                LRText = "Left/Right";
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
        ///Called when the slider changes its value (or when loading)
        ///Sets the correct values of the double progress bars
        {
            float x = _data.XAcceleration.ElementAt(value);
            float y = _data.YAcceleration.ElementAt(value);
            float z = _data.ZAcceleration.ElementAt(value);

            if (x < 0)
            {
                LValFB = 0;
                RValFB = -x;
            }
            else
            {
                LValFB = x;
                RValFB = 0;
            }

            if (y < 0)
            {
                LValLR = 0;
                RValLR = y;
            }
            else
            {
                LValLR = y;
                RValLR = 0;
            }

            if (z < 0)
            {
                LValUD = 0;
                RValUD = -z;
            }
            else
            {
                LValUD = z;
                RValUD = 0;
            }

            FBText = String.Format("Forwards/Backwards: {0} m/s^2", x);
            UDText = String.Format("Up/Down: {0} m/s^2", y);
            LRText = String.Format("Left/Right: {0} m/s^2", z);
        }

        //The following are the Data Bindings for the values
        public float RValUD
        {
            get { return _rValUD; }
            set { SetProperty(ref _rValUD, value); }
        }

        public float LValUD
        {
            get { return _lValUD; }
            set { SetProperty(ref _lValUD, value); }
        }

        public float RValFB
        {
            get { return _rValFB; }
            set { SetProperty(ref _rValFB, value); }
        }

        public float LValFB
        {
            get { return _lValFB; }
            set { SetProperty(ref _lValFB, value); }
        }

        public float RValLR
        {
            get { return _rValLR; }
            set { SetProperty(ref _rValLR, value); }
        }

        public float LValLR
        {
            get { return _lValLR; }
            set { SetProperty(ref _lValLR, value); }
        }

        //The following are the Data Bindings for the maximums
        public float RMaxUD
        {
            get { return _rMaxUD; }
            set { SetProperty(ref _rMaxUD, value); }
        }

        public float LMaxUD
        {
            get { return _lMaxUD; }
            set { SetProperty(ref _lMaxUD, value); }
        }

        public float RMaxFB
        {
            get { return _rMaxFB; }
            set { SetProperty(ref _rMaxFB, value); }
        }

        public float LMaxFB
        {
            get { return _lMaxFB; }
            set { SetProperty(ref _lMaxFB, value); }
        }

        public float RMaxLR
        {
            get { return _rMaxLR; }
            set { SetProperty(ref _rMaxLR, value); }
        }

        public float LMaxLR
        {
            get { return _lMaxLR; }
            set { SetProperty(ref _lMaxLR, value); }
        }

        public string UDText
        {
            get { return _udText; }
            set { SetProperty(ref _udText, value); }
        }

        public string LRText
        {
            get { return _lrText; }
            set { SetProperty(ref _lrText, value); }
        }

        public string FBText
        {
            get { return _fbText; }
            set { SetProperty(ref _fbText, value); }
        }
    }
}