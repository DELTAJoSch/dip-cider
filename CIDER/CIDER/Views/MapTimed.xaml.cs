﻿using CIDER.ViewModels;
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
    /// Interaction logic for MapTimed.xaml
    /// </summary>
    public partial class MapTimed : Page
    {
        MapTimedViewModel model;
        public MapTimed(DataProvider data)
        {
            InitializeComponent();

            model = new MapTimedViewModel(data);
            this.DataContext = model;

            model.RouteChangedEvent += Model_RouteChangedEvent;

            model.Init();
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
        }

        private void onUnload(object sender, RoutedEventArgs e)
        {
            model.RouteChangedEvent -= Model_RouteChangedEvent;
        }
    }
}
