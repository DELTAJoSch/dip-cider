using CIDER.MVVMBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.ViewModels
{
    public class AccelerationTimedViewModel:ViewModelBase
    ///Summary
    ///This is the ViewModel for the AngleTimedView
    {
        DataProvider _data;
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
        public AccelerationTimedViewModel(DataProvider data)
        {
            _data = data;

            //test
            RValUD = 10;
            RValLR = 20;
            RValFB = 30;
            LValFB = 40;
            LValLR = 50;
            LValUD = 60;
            RMaxUD = 100;
            RMaxLR = 100;
            RMaxFB = 100;
            LMaxFB = 100;
            LMaxLR = 100;
            LMaxUD = 100;
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
    }
}
