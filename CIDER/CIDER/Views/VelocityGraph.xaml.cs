﻿using CIDER.ViewModels;
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
    /// Interaction logic for VelocityGraph.xaml
    /// </summary>
    public partial class VelocityGraph : Page
    {
        private VelocityGraphViewModel model;
        public VelocityGraph(DataProvider data)
        {
            InitializeComponent();

            model = new VelocityGraphViewModel(data);
            this.DataContext = model;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            model.Dispose();
        }
    }
}
