/* Copyright (C) 2020  Johannes Schiemer 
	This program is free software: you can redistribute it and/or modify 
	it under the terms of the GNU General Public License as published by 
	the Free Software Foundation, either version 3 of the License, or 
	(at your option) any later version. 
	This program is distributed in the hope that it will be useful, 
	but WITHOUT ANY WARRANTY; without even the implied warranty of 
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
	GNU General Public License for more details. 
	You should have received a copy of the GNU General Public License 
	along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/
using CIDER.MVVMBase;
using System;
using System.Linq;

namespace CIDER.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the AngleTimed page
    /// </summary>
    public class AngleTimedViewModel : ViewModelBase
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

        /// <summary>
        /// This is the constructor for the AngleTimedViewModel
        /// </summary>
        /// <param name="data">A DataProvider object to read the data from</param>
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

        /// <summary>
        /// This contains the maximum of the slider
        /// </summary>
        public int slMaximum
        {
            get { return _slMaximum; }
            set { SetProperty(ref _slMaximum, value); }
        }

        /// <summary>
        /// This contains the frequency of the slider ticks
        /// </summary>
        public int slTickFrequency
        {
            get { return _slTickFrequency; }
            set { SetProperty(ref _slTickFrequency, value); }
        }

        /// <summary>
        /// This should be called when the slider value changes
        /// </summary>
        /// <param name="Value">The value of the slider</param>
        public void SliderValueChanged(int Value)
        {
            var Roll = _data.Roll.ElementAt(Value);
            var Pitch = _data.Pitch.ElementAt(Value);
            var Yaw = _data.Yaw.ElementAt(Value);

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
        /// <summary>
        /// This contains the value for the right pitch progbar
        /// </summary>
        public float RValPitch
        {
            get { return _rValPitch; }
            set { SetProperty(ref _rValPitch, value); }
        }

        /// <summary>
        /// This contains the value for the left pitch progbar
        /// </summary>
        public float LValPitch
        {
            get { return _lValPitch; }
            set { SetProperty(ref _lValPitch, value); }
        }

        /// <summary>
        /// This contains the value for the right roll progbar
        /// </summary>
        public float RValRoll
        {
            get { return _rValRoll; }
            set { SetProperty(ref _rValRoll, value); }
        }

        /// <summary>
        /// This contains the value for the left roll progbar
        /// </summary>
        public float LValRoll
        {
            get { return _lValRoll; }
            set { SetProperty(ref _lValRoll, value); }
        }

        /// <summary>
        /// This contains the value for the right yaw progbar
        /// </summary>
        public float RValYaw
        {
            get { return _rValYaw; }
            set { SetProperty(ref _rValYaw, value); }
        }

        /// <summary>
        /// This contains the value for the left yaw progbar
        /// </summary>
        public float LValYaw
        {
            get { return _lValYaw; }
            set { SetProperty(ref _lValYaw, value); }
        }

        //The following are the Data Bindings for the maximums
        /// <summary>
        /// this contains the value of the maximum of the right pitch progbar
        /// </summary>
        public float RMaxPitch
        {
            get { return _rMaxPitch; }
            set { SetProperty(ref _rMaxPitch, value); }
        }

        /// <summary>
        /// this contains the value of the maximum of the left pitch progbar
        /// </summary>
        public float LMaxPitch
        {
            get { return _lMaxPitch; }
            set { SetProperty(ref _lMaxPitch, value); }
        }

        /// <summary>
        /// this contains the value of the maximum of the right roll progbar
        /// </summary>
        public float RMaxRoll
        {
            get { return _rMaxRoll; }
            set { SetProperty(ref _rMaxRoll, value); }
        }

        /// <summary>
        /// this contains the value of the maximum of the left roll progbar
        /// </summary>
        public float LMaxRoll
        {
            get { return _lMaxRoll; }
            set { SetProperty(ref _lMaxRoll, value); }
        }

        /// <summary>
        /// this contains the value of the maximum of the right yaw progbar
        /// </summary>
        public float RMaxYaw
        {
            get { return _rMaxYaw; }
            set { SetProperty(ref _rMaxYaw, value); }
        }

        /// <summary>
        /// this contains the value of the maximum of the left yaw progbar
        /// </summary>
        public float LMaxYaw
        {
            get { return _lMaxYaw; }
            set { SetProperty(ref _lMaxYaw, value); }
        }

        /// <summary>
        /// This contains to be displayed next to the pitch progbars
        /// </summary>
        public string PitchText
        {
            get { return _pitchText; }
            set { SetProperty(ref _pitchText, value); }
        }

        /// <summary>
        /// This contains to be displayed next to the yaw progbars
        /// </summary>
        public string YawText
        {
            get { return _yawText; }
            set { SetProperty(ref _yawText, value); }
        }

        /// <summary>
        /// This contains to be displayed next to the roll progbars
        /// </summary>
        public string RollText
        {
            get { return _rollText; }
            set { SetProperty(ref _rollText, value); }
        }
    }
}