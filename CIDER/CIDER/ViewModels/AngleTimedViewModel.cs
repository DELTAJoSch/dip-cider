using CIDER.MVVMBase;
using System;
using System.Linq;

namespace CIDER.ViewModels
{
    public class AngleTimedViewModel : ViewModelBase
    ///Summary
    ///This is the ViewModel for the AngleTimedView
    {
        private DataProvider _data;

        //private Data, all corresponding to the bindings
        private int _slMaximum;

        private int _slTickFrequency;
        private float _rValPitch;
        private float _lValPitch;
        private float _rValRoll;
        private float _lValRoll;
        private float _rValYaw;
        private float _lValYaw;
        private float _rMaxPitch;
        private float _lMaxPitch;
        private float _rMaxRoll;
        private float _lMaxRoll;
        private float _rMaxYaw;
        private float _lMaxYaw;
        private string _pitchText;
        private string _rollText;
        private string _yawText;

        public AngleTimedViewModel(DataProvider data)
        {
            _data = data;

            slMaximum = _data.DataPointsAngle - 1;
            slTickFrequency = slMaximum / 200;

            if (slTickFrequency < 1)
                slTickFrequency = 1;

            RMaxRoll = LMaxRoll = RMaxYaw = LMaxYaw = RMaxPitch = LMaxPitch = 180;

            if ((_data.DataPointsAngle == 0) == false)
            {
                SliderValueChanged(0);
            }
            else
            {
                RollText = "Roll";
                PitchText = "Pitch";
                YawText = "Yaw";
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

        public void SliderValueChanged(int Value)
        ///Called when the slider changes its value (or when loading)
        ///Sets the correct values of the double progress bars
        {
            var Roll = _data.Roll.ElementAt(Value);
            var Pitch = _data.Pitch.ElementAt(Value);
            var Yaw = _data.Yaw.ElementAt(Value);

            if (Roll > 180)
                Roll -= 360;
            if (Roll < -180)
                Roll += 360;

            if (Pitch > 180)
                Pitch -= 360;
            if (Pitch < -180)
                Pitch += 360;

            if (Yaw > 180)
                Yaw -= 360;
            if (Yaw < -180)
                Yaw += 360;

            if (Roll < 0)
            {
                LValRoll = 0;
                RValRoll = -(float)Roll;
            }
            else
            {
                LValRoll = (float)Roll;
                RValRoll = 0;
            }

            if (Yaw < 0)
            {
                LValYaw = 0;
                RValYaw = -(float)Yaw;
            }
            else
            {
                LValYaw = (float)Yaw;
                RValYaw = 0;
            }

            if (Pitch < 0)
            {
                LValPitch = 0;
                RValPitch = -(float)Pitch;
            }
            else
            {
                LValPitch = (float)Pitch;
                RValPitch = 0;
            }

            RollText = String.Format("Roll: {0}°", Roll);
            PitchText = String.Format("Pitch: {0}°", Pitch);
            YawText = String.Format("Yaw: {0}°", Yaw);
        }

        //The following are the Data Bindings for the values
        public float RValPitch
        {
            get { return _rValPitch; }
            set { SetProperty(ref _rValPitch, value); }
        }

        public float LValPitch
        {
            get { return _lValPitch; }
            set { SetProperty(ref _lValPitch, value); }
        }

        public float RValRoll
        {
            get { return _rValRoll; }
            set { SetProperty(ref _rValRoll, value); }
        }

        public float LValRoll
        {
            get { return _lValRoll; }
            set { SetProperty(ref _lValRoll, value); }
        }

        public float RValYaw
        {
            get { return _rValYaw; }
            set { SetProperty(ref _rValYaw, value); }
        }

        public float LValYaw
        {
            get { return _lValYaw; }
            set { SetProperty(ref _lValYaw, value); }
        }

        //The following are the Data Bindings for the maximums
        public float RMaxPitch
        {
            get { return _rMaxPitch; }
            set { SetProperty(ref _rMaxPitch, value); }
        }

        public float LMaxPitch
        {
            get { return _lMaxPitch; }
            set { SetProperty(ref _lMaxPitch, value); }
        }

        public float RMaxRoll
        {
            get { return _rMaxRoll; }
            set { SetProperty(ref _rMaxRoll, value); }
        }

        public float LMaxRoll
        {
            get { return _lMaxRoll; }
            set { SetProperty(ref _lMaxRoll, value); }
        }

        public float RMaxYaw
        {
            get { return _rMaxYaw; }
            set { SetProperty(ref _rMaxYaw, value); }
        }

        public float LMaxYaw
        {
            get { return _lMaxYaw; }
            set { SetProperty(ref _lMaxYaw, value); }
        }

        public string PitchText
        {
            get { return _pitchText; }
            set { SetProperty(ref _pitchText, value); }
        }

        public string YawText
        {
            get { return _yawText; }
            set { SetProperty(ref _yawText, value); }
        }

        public string RollText
        {
            get { return _rollText; }
            set { SetProperty(ref _rollText, value); }
        }
    }
}