using CIDER.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for MapRoute.xaml
    /// </summary>
    public partial class MapRoute : Page
    {
        private MapRouteViewModel model;

        public MapRoute(DataProvider data)
        {
            InitializeComponent();

            model = new MapRouteViewModel(data);
            this.DataContext = model;
            model.RouteChangedEvent += Model_RouteChangedEvent;
            model.Initialize();
            Map.SetView(model.MapCenter, model.MapZoomLevel);
        }

        private void Model_RouteChangedEvent(object sender, EventArgs e)
        {
            Map.Children.Clear();
            foreach (MapPolyline line in model.MapPolylines)
            {
                Map.Children.Add(line);
            }

            model.CalculateCenter();
            Map.SetView(model.MapCenter, model.MapZoomLevel);
        }

        private void onUnload(object sender, RoutedEventArgs e)
        {
            model.RouteChangedEvent -= Model_RouteChangedEvent;
        }
    }
}