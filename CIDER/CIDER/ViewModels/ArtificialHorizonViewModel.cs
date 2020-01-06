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
using System.Windows.Forms;
using System.Windows.Input;

namespace CIDER.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the ArtificiaHorizon page
    /// </summary>
    public class ArtificialHorizonViewModel : ViewModelBase, IDisposable
    {
        private DataProvider _data;
        private double _Pitch;
        private double _Roll;
        private double _Yaw;
        private double _Velocity;
        private double _ClimbVelocity;
        private int _slMaximum;
        private int _slTickFrequency;
        private bool playing = false;
        private readonly Timer playTimer;
        private int playFrame;
        private readonly DelegateCommand _playPauseClickedCommand;

        /// <summary>
        /// This is the constructor for the ArtificialHorizonViewModel
        /// </summary>
        /// <param name="Data">A DataPRovider object to read the data from</param>
        public ArtificialHorizonViewModel(DataProvider Data)
        {
            _playPauseClickedCommand = new DelegateCommand(PlayPause);

            _data = Data;

            playTimer = new Timer();

            slMaximum = _data.DataPointsAngle;
            slTickFrequency = slMaximum / 200;

            if (slTickFrequency < 1)
                slTickFrequency = 1;

            slTickFrequency = 1;
        }

        /// <summary>
        /// Command connected to the MailTo Button
        /// </summary>
        public ICommand PlayPauseClickedCommand => _playPauseClickedCommand;

        /// <summary>
        /// This contains the value of the current pitch
        /// </summary>
        public double Pitch { get { return _Pitch; } set { SetProperty(ref _Pitch, value); } }
        
        /// <summary>
        /// This contains the calue of the current roll angle
        /// </summary>
        public double Roll { get { return _Roll; } set { SetProperty(ref _Roll, value); } }

        /// <summary>
        /// This contains the value of the current yaw angle
        /// </summary>
        public double Yaw { get { return _Yaw; } set { SetProperty(ref _Yaw, value); } }

        /// <summary>
        /// This contains the value of the current velocity
        /// </summary>
        public double Velocity { get { return _Velocity; } set { SetProperty(ref _Velocity, value); } }

        /// <summary>
        /// This contains the current climb velocity
        /// </summary>
        public double ClimbVelocity { get { return _ClimbVelocity; } set { SetProperty(ref _ClimbVelocity, value); } }

        /// <summary>
        /// This contains the maximum value of the slider
        /// </summary>
        public int slMaximum
        {
            get { return _slMaximum; }
            set { SetProperty(ref _slMaximum, value); }
        }

        /// <summary>
        /// This contains the slider tick frequency
        /// </summary>
        public int slTickFrequency
        {
            get { return _slTickFrequency; }
            set { SetProperty(ref _slTickFrequency, value); }
        }

        /// <summary>
        /// This function should be called when the slider changes its value
        /// </summary>
        /// <param name="Value">The value of the slider</param>
        public void SliderValueChanged(int Value)
        {
            try
            {
                Pitch = _data.Pitch.ElementAt(Value);
                Roll = _data.Roll.ElementAt(Value); ;
                Yaw = _data.Yaw.ElementAt(Value); ;

                try
                {
                    Velocity = _data.Velocity.ElementAt((int)Value);
                }
                catch (Exception ex)
                {
                    logger.Warn(ex, "Velocity not available");
                }

                try
                {
                    ClimbVelocity = _data.Height.ElementAt((int)Value + 1) - _data.Height.ElementAt((int)Value);
                }
                catch (Exception ex)
                {
                    logger.Warn(ex, "Error whilst calculating climb rate");
                }
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Couldn't calculate Angle");
            }
        }

        private void PlayPause(object sender)
        {
            if (playing)
            {
                playTimer.Stop();
                playTimer.Tick -= PlayTimer_Tick;
            }
            else
            {
                playFrame = 0;
                playTimer.Interval = 50;
                playTimer.Tick += PlayTimer_Tick;
                playTimer.Start();
            }

            playing = !playing;
        }

        private void PlayTimer_Tick(object sender, EventArgs e)
        {
            if(playFrame < slMaximum)
            {
                SliderValueChanged(playFrame);
                playFrame++;
            }
            else
            {
                PlayPause(this);
                SliderValueChanged(0);
            }
        }

        /// <summary>
        /// This function disposes of the object.
        /// </summary>
        public void Dispose()
        {
            try
            {
                playTimer.Tick -= PlayTimer_Tick;
                playTimer.Stop();
            }
            catch(Exception ex)
            {
                logger.Error(ex, "Error during Disposal");
            }
        }
    }
}