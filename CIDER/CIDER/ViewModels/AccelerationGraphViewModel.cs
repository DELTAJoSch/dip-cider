﻿using CIDER.MVVMBase;
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER.ViewModels
{
    public class AccelerationGraphViewModel:ViewModelBase
    ///Summary
    ///This is the View Model for the AngleGraphViewModel
    ///TODO: Make loading async or do it in an extra thread
    {
        PlotModel _plot;
        DataProvider _data;

        public AccelerationGraphViewModel(DataProvider Data)
        {
            _data = Data;

            PlotManager manager = new PlotManager();

            manager.AddLineSeries(_data.XAcceleration, "F/B", OxyColors.Blue);
            manager.AddLineSeries(_data.YAcceleration, "L/R", OxyColors.Chartreuse);
            manager.AddLineSeries(_data.ZAcceleration, "U/D", OxyColors.Gold);

            Plot = manager.GetPlotModel("Acceleration");
        }

        public PlotModel Plot
         //Data Binding for the Graph
        {
            get { return _plot; }
            set { SetProperty(ref _plot, value); }
        }
    }
}
