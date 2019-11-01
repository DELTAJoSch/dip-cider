using CIDER.MVVMBase;
using System;
using System.Linq;

namespace CIDER.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the AccelerationTimedView
    /// </summary>
    public class AccelerationTimedViewModel : ViewModelBase
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

        /// <summary>
        /// This is the constructor for the AccelerationTimedViewModel
        /// </summary>
        /// <param name="data">A DataProvider object to read the data from</param>
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

        /// <summary>
        /// This contains the maxximum of the slider
        /// </summary>
        public int slMaximum
        {
            get { return _slMaximum; }
            set { SetProperty(ref _slMaximum, value); }
        }

        /// <summary>
        /// This contains the frequency of slider ticks
        /// </summary>
        public int slTickFrequency
        {
            get { return _slTickFrequency; }
            set { SetProperty(ref _slTickFrequency, value); }
        }

        /// <summary>
        /// This function should be called when the value of the slider changes
        /// </summary>
        /// <param name="value">This is the value of the slider</param>
        public void SliderValueChanged(int value)
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
        /// <summary>
        /// The value of the right up-down progBar
        /// </summary>
        public float RValUD
        {
            get { return _rValUD; }
            set { SetProperty(ref _rValUD, value); }
        }

        /// <summary>
        /// The value of the left up-down progBar
        /// </summary>
        public float LValUD
        {
            get { return _lValUD; }
            set { SetProperty(ref _lValUD, value); }
        }

        /// <summary>
        /// The value of the right forwards-backwards progBar
        /// </summary>
        public float RValFB
        {
            get { return _rValFB; }
            set { SetProperty(ref _rValFB, value); }
        }

        /// <summary>
        /// The value of the left forwards-backwards progBar
        /// </summary>
        public float LValFB
        {
            get { return _lValFB; }
            set { SetProperty(ref _lValFB, value); }
        }

        /// <summary>
        /// The value of the right left-right progBar
        /// </summary>
        public float RValLR
        {
            get { return _rValLR; }
            set { SetProperty(ref _rValLR, value); }
        }

        /// <summary>
        /// The value of the left left-right progBar
        /// </summary>
        public float LValLR
        {
            get { return _lValLR; }
            set { SetProperty(ref _lValLR, value); }
        }

        //The following are the Data Bindings for the maximums
        /// <summary>
        /// This is the value for the maximum of the right up-down progBar
        /// </summary>
        public float RMaxUD
        {
            get { return _rMaxUD; }
            set { SetProperty(ref _rMaxUD, value); }
        }

        /// <summary>
        /// This is the value for the maximum of the left up-down progBar
        /// </summary>
        public float LMaxUD
        {
            get { return _lMaxUD; }
            set { SetProperty(ref _lMaxUD, value); }
        }

        /// <summary>
        /// This is the value for the maximum of the right forward-backward progBar
        /// </summary>
        public float RMaxFB
        {
            get { return _rMaxFB; }
            set { SetProperty(ref _rMaxFB, value); }
        }

        /// <summary>
        /// This is the value for the maximum of the left forward-backward progBar
        /// </summary>
        public float LMaxFB
        {
            get { return _lMaxFB; }
            set { SetProperty(ref _lMaxFB, value); }
        }

        /// <summary>
        /// This is the value for the maximum of the right left-right progBar
        /// </summary>
        public float RMaxLR
        {
            get { return _rMaxLR; }
            set { SetProperty(ref _rMaxLR, value); }
        }

        /// <summary>
        /// This is the value for the maximum of the left left-right progBar
        /// </summary>
        public float LMaxLR
        {
            get { return _lMaxLR; }
            set { SetProperty(ref _lMaxLR, value); }
        }

        /// <summary>
        /// This is the text to be shown with the up-down progBar
        /// </summary>
        public string UDText
        {
            get { return _udText; }
            set { SetProperty(ref _udText, value); }
        }

        /// <summary>
        /// This is the text to be shown with the left-right progBar
        /// </summary>
        public string LRText
        {
            get { return _lrText; }
            set { SetProperty(ref _lrText, value); }
        }

        /// <summary>
        /// This is the text to be shown with the forward-backward progBar
        /// </summary>
        public string FBText
        {
            get { return _fbText; }
            set { SetProperty(ref _fbText, value); }
        }
    }
}