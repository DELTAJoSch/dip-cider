using CIDER.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for MapTimed.xaml
    /// </summary>
    public partial class MapTimed : Page
    {
        private MapTimedViewModel model;

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

        public void slValueChanged(object sender, EventArgs e)
        {
            model.UpdateRoute((int)slValue.Value);
            model.CalculateCenter();

            map.SetView(model.MapCenter, model.MapZoomLevel);
        }

        private void onUnload(object sender, RoutedEventArgs e)
        {
            model.RouteChangedEvent -= Model_RouteChangedEvent;
        }
    }
}