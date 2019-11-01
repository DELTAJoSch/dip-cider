using CIDER.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for the MapTimed page
    /// </summary>
    public partial class MapTimed : Page
    {
        private MapTimedViewModel model;

        /// <summary>
        /// This is the constructor of the MapTimed page
        /// </summary>
        /// <param name="data"></param>
        public MapTimed(DataProvider data)
        {
            InitializeComponent();

            model = new MapTimedViewModel(data);
            this.DataContext = model;

            model.RouteChangedEvent += Model_RouteChangedEvent;

            model.Init();

            map.SetView(model.MapCenter, model.MapZoomLevel);
        }

        private void Model_RouteChangedEvent(object sender, EventArgs e)
        {
            map.Children.Clear();

            foreach (MapPolyline line in model.MapPolylines)
            {
                map.Children.Add(line);
            }
        }

        /// <summary>
        /// This function is called when the slider value changes
        /// </summary>
        /// <param name="sender">The object that called this method</param>
        /// <param name="e">The event args this was called with</param>
        public void slValueChanged(object sender, EventArgs e)
        {
            model.SliderValueChanged((int)slValue.Value);
            model.CalculateCenter();

            map.SetView(model.MapCenter, model.MapZoomLevel);
        }

        private void onUnload(object sender, RoutedEventArgs e)
        {
            model.RouteChangedEvent -= Model_RouteChangedEvent;
        }
    }
}