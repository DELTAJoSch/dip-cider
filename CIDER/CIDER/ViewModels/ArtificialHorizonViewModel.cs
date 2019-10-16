using CIDER.MVVMBase;
using System;
using System.Linq;

namespace CIDER.ViewModels
{
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

        public ArtificialHorizonViewModel(DataProvider Data)
        {
            _data = Data;

            slMaximum = _data.DataPointsAngle;
            slTickFrequency = slMaximum / 200;

            if (slTickFrequency < 1)
                slTickFrequency = 1;
        }

        public double Pitch { get { return _Pitch; } set { SetProperty(ref _Pitch, value); } }
        public double Roll { get { return _Roll; } set { SetProperty(ref _Roll, value); } }
        public double Yaw { get { return _Yaw; } set { SetProperty(ref _Yaw, value); } }
        public double Velocity { get { return _Velocity; } set { SetProperty(ref _Velocity, value); } }
        public double ClimbVelocity { get { return _ClimbVelocity; } set { SetProperty(ref _ClimbVelocity, value); } }

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