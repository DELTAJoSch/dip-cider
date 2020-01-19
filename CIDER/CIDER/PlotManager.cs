﻿/* Copyright (C) 2020  Johannes Schiemer 
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
using OxyPlot;
using OxyPlot.Series;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CIDER
{
    /// <summary>
    /// This class is used to create plots
    /// </summary>
    public class PlotManager
    {
        private PlotModel _plot;

        /// <summary>
        /// This list contains all the line series in a plot
        /// </summary>
        public List<LineSeries> Series;

        /// <summary>
        /// This is the constructor for the PlotManager class
        /// </summary>
        public PlotManager()
        {
            Series = new List<LineSeries>();
        }

        /// <summary>
        /// This function is used to get a plotmodel
        /// </summary>
        /// <param name="Title">This is the Title of the plot returned</param>
        /// <returns>Returns a plotmodel</returns>
        public async Task<PlotModel> GetPlotModel(string Title)
        {
            await Create(Title);

            return _plot;
        }

        private async Task Create(string Title)
        {
            //Create a new PlotModel
            PlotModel CreatePlot = new PlotModel();
            CreatePlot.Title = Title;

            Parallel.ForEach(Series, x =>
            {
                CreatePlot.Series.Add(x);
            });

            _plot = CreatePlot;
        }

        private async Task<List<DataPoint>> GetLineSeriesAsync(List<float> data, int interval)
        //  This Task reads all the data points and convert them to line series
        //  It´s executed asynchronously, so this doesn't block when large datasets are involved
        {
            List<DataPoint> Points = new List<DataPoint>();

            double t = 0;

            foreach (float p in data)
            {
                Points.Add(new DataPoint(t, p));
                t += interval;
            }

            return Points;
        }

        private async void CreateSeries(List<float> data, string name, OxyColor color, int interval)
        {
            var Task = GetLineSeriesAsync(data, interval);

            LineSeries ls = new LineSeries();

            ls.Title = name;
            ls.Color = color;

            await Task;

            ls.Points.AddRange(Task.Result);
            ls.TextColor = OxyColors.Black;

            Series.Add(ls);
        }

        /// <summary>
        /// This function adds a new lineseries to the plot
        /// </summary>
        /// <param name="data">The data to be shown</param>
        /// <param name="name">The name of the lineseries</param>
        public void AddLineSeries(List<float> data, string name)
        {
            Random random = new Random();

            CreateSeries(data, name, OxyColor.FromRgb((byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255)), 1);
        }

        /// <summary>
        /// This function adds a new lineseries to the plot
        /// </summary>
        /// <param name="data">The data to be shown</param>
        /// <param name="name">The name of the lineseries</param>
        /// <param name="color">the color of the lineseries</param>
        public void AddLineSeries(List<float> data, string name, OxyColor color)
        {
            CreateSeries(data, name, color, 1);
        }

        /// <summary>
        /// This function adds a new lineseries to the plot
        /// </summary>
        /// <param name="data">The data to be shown</param>
        /// <param name="name">The name of the lineseries</param>
        /// <param name="color">the color of the lineseries</param>
        /// <param name="interval">the interval between the points</param>
        public void AddLineSeries(List<float> data, string name, OxyColor color, int interval)
        {
            CreateSeries(data, name, color, interval);
        }

        /// <summary>
        /// This function adds a new lineseries to the plot
        /// </summary>
        /// <param name="data">The data to be shown</param>
        /// <param name="name">The name of the lineseries</param>
        /// <param name="interval">the interval between the points</param>
        public void AddLineSeries(List<float> data, string name, int interval)
        {
            Random random = new Random();

            CreateSeries(data, name, OxyColor.FromRgb((byte)random.Next(255), (byte)random.Next(255), (byte)random.Next(255)), interval);
        }
    }
}