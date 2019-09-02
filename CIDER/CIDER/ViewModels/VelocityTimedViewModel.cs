﻿using CIDER.MVVMBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.ViewModels
{
    public class VelocityTimedViewModel:ViewModelBase
    {
        private DataProvider _data;
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

        public VelocityTimedViewModel(DataProvider data)
        {
            _data = data;

            slMaximum = _data.DataPointsVelocity-1;
            if (slMaximum < 1000)
                slTickFrequency = 2;
            if (slMaximum > 1000 && slMaximum < 10000)
                slTickFrequency = 10;
            if (slMaximum > 10000 && slMaximum < 1000000)
                slTickFrequency = 500;
            if (slMaximum > 1000000)
                slTickFrequency = 2000;

            RMaxFB = LMaxFB = RMaxLR = LMaxLR = RMaxUD = LMaxUD = 400;

            if((_data.DataPointsVelocity == 0) == false)
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
        {
            float x = _data.XVelocity.ElementAt(value);
            float y = _data.YVelocity.ElementAt(value);
            float z = _data.ZVelocity.ElementAt(value); ;

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
                RValLR = -y;
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

            FBText = String.Format("Forwards/Backwards: {0} m/s", x);
            UDText = String.Format("Up/Down: {0} m/s", y);
            LRText = String.Format("Left/Right: {0} m/s", z); 
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
