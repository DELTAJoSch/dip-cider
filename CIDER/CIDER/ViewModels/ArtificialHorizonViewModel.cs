using CIDER.MVVMBase;
using System;
using System.Linq;

namespace CIDER.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the ArtificiaHorizon page
    /// </summary>
    public class ArtificialHorizonViewModel : ViewModelBase
    {
        private DataProvider _data;
        private double _Pitch;
        private double _Roll;
        private double _Yaw;
        private double _Velocity;
        private double _ClimbVelocity;
        private int _slMaximum;
        private int _slTickFrequency;

        /// <summary>
        /// This is the constructor for the ArtificialHorizonViewModel
        /// </summary>
        /// <param name="Data">A DataPRovider object to read the data from</param>
        public ArtificialHorizonViewModel(DataProvider Data)
        {
            _data = Data;

            slMaximum = _data.DataPointsAngle;
            slTickFrequency = slMaximum / 200;

            if (slTickFrequency < 1)
                slTickFrequency = 1;
        }

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
    }
}