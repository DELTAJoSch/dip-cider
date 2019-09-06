using CIDER.ViewModels;
using Microsoft.Maps.MapControl.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for MapRoute.xaml
    /// </summary>
    public partial class MapRoute : Page
    {
        MapRouteViewModel model;
        public MapRoute(DataProvider data)
        {
            InitializeComponent();

            model = new MapRouteViewModel(data);
            this.DataContext = model;
            model.RouteChangedEvent += Model_RouteChangedEvent;
            model.Initialize();
        }

        private void Model_RouteChangedEvent(object sender, EventArgs e)
        {
            Map.Children.Clear();
            foreach(MapPolyline line in model.MapPolylines)
            {
                Map.Children.Add(line);
            }
        }

        private void onUnload(object sender, RoutedEventArgs e)
        {
            model.RouteChangedEvent -= Model_RouteChangedEvent;
        }
    }
}
