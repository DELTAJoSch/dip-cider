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
using OxyPlot;
using System;

namespace CIDER.ViewModels
{
    /// <summary>
    /// This is the ViewModel for the VelocityGraph page
    /// </summary>
    public class VelocityGraphViewModel : ViewModelBase, IDisposable
    {
        private DataProvider _data;
        private PlotModel _plot;
        private PlotModel blank;
        private PlotModel data;

        /// <summary>
        /// This is the constructor of the VelocityGraphViewModel
        /// </summary>
        /// <param name="dataProvider">A DataProvider object to read the data from</param>
        public VelocityGraphViewModel(DataProvider dataProvider)
        {
            _data = dataProvider;

            PlotManager manager = new PlotManager();

            manager.AddLineSeries(_data.Velocity, "Vel", OxyColors.IndianRed);

            data = manager.GetPlotModel("Velocity").Result;
            blank = new PlotModel();
            blank.Title = "Velocity";
            Plot = data;

            MainWindow.OnResizeStartEvent += MainWindow_OnResizeStartEvent;
            MainWindow.OnResizeEndEvent += MainWindow_OnResizeEndEvent;
        }

        private void MainWindow_OnResizeEndEvent(object sender, EventArgs e)
        {
            Plot = data;
        }

        private void MainWindow_OnResizeStartEvent(object sender, EventArgs e)
        {
            Plot = blank;
        }

        /// <summary>
        /// This function needs to be called before dereferencing an instance of this class so the GC can collect it
        /// </summary>
        public void Dispose()
        {
            MainWindow.OnResizeStartEvent -= MainWindow_OnResizeStartEvent;
            MainWindow.OnResizeEndEvent -= MainWindow_OnResizeEndEvent;
        }

        /// <summary>
        /// This contains the plot to be shown
        /// </summary>
        public PlotModel Plot
        {
            get
            {
                return _plot;
            }
            set
            {
                SetProperty(ref _plot, value);
            }
        }
    }
}