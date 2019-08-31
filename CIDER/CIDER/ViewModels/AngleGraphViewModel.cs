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
    public class AngleGraphViewModel:ViewModelBase
    {
        PlotModel _plot;
        DataProvider _data;

        public AngleGraphViewModel(DataProvider Data)
        {
            _data = Data;

            PlotModel CreatePlot = new PlotModel();
            CreatePlot.Title = "Acceleration";

            LineSeries XSeries = new LineSeries();
            LineSeries YSeries = new LineSeries();
            LineSeries ZSeries = new LineSeries();

            double t = 0;

            foreach (Tuple<float, float, float> tuple in _data.Angles)
            {
                XSeries.Points.Add(new DataPoint(t, (double)tuple.Item1));
                YSeries.Points.Add(new DataPoint(t, (double)tuple.Item2));
                ZSeries.Points.Add(new DataPoint(t, (double)tuple.Item3));
                t++;
            }

            XSeries.Color = OxyColor.FromRgb(38, 255, 0);
            YSeries.Color = OxyColor.FromRgb(255, 0, 230);
            ZSeries.Color = OxyColor.FromRgb(0, 242, 255);

            XSeries.Title = "F/B";
            YSeries.Title = "L/R";
            ZSeries.Title = "U/D";

            CreatePlot.Series.Add(XSeries);
            CreatePlot.Series.Add(YSeries);
            CreatePlot.Series.Add(ZSeries);

            Plot = CreatePlot;

            Plot.InvalidatePlot(true);
        }

        public PlotModel Plot
        {
            get { return _plot; }
            set { SetProperty(ref _plot, value); }
        }
    }
}
