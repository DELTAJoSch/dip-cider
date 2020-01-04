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
    /// This is the ViewModel for the VelocityTimed page
    /// </summary>
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

        /// <summary>
        /// This is the constructor for the VelocityTimedViewModle
        /// </summary>
        /// <param name="data">A DataProvider object to read the data from</param>
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

        /// <summary>
        /// This contains the maximum value of the slider
        /// </summary>
        public int slMaximum
        {
            get { return _slMaximum; }
            set { SetProperty(ref _slMaximum, value); }
        }

        /// <summary>
        /// This contains the tick frequency of the slider
        /// </summary>
        public int slTickFrequency
        {
            get { return _slTickFrequency; }
            set { SetProperty(ref _slTickFrequency, value); }
        }

        /// <summary>
        /// This function should be called when the slider value changes
        /// </summary>
        /// <param name="value">The value of the slider</param>
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

            Text = String.Format("Velocity: {0} kt", x);
        }

        //The following are the Data Bindings for the values
        /// <summary>
        /// This is the value of the right progress bar
        /// </summary>
        public float RVal
        {
            get { return _rVal; }
            set { SetProperty(ref _rVal, value); }
        }

        /// <summary>
        /// This is the value of the left progress bar
        /// </summary>
        public float LVal
        {
            get { return _lVal; }
            set { SetProperty(ref _lVal, value); }
        }

        //The following are the Data Bindings for the maximums
        /// <summary>
        /// This is the maximum of the value of the right progress bar
        /// </summary>
        public float RMax
        {
            get { return _rMax; }
            set { SetProperty(ref _rMax, value); }
        }

        /// <summary>
        /// This is the maximum of the left value of the progress bar
        /// </summary>
        public float LMax
        {
            get { return _lMax; }
            set { SetProperty(ref _lMax, value); }
        }

        /// <summary>
        /// This contains the text to be displayed next to the progress bars
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set { SetProperty(ref _Text, value); }
        }
    }
}