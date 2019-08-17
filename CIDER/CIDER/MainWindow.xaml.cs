using CIDER.ViewModels;
using System;
using System.Windows;

namespace CIDER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            var viewModel = new MainWindowViewModel(frmMain, new Uri("Views/About.xaml", UriKind.Relative), new Uri("Views/AngleGraph.xaml", UriKind.Relative), 
                new Uri("Views/AngleTimed.xaml", UriKind.Relative), new Uri("Views/Load.xaml", UriKind.Relative), new Uri("Views/MapRoute.xaml", UriKind.Relative), 
                new Uri("Views/MapTimed.xaml", UriKind.Relative), new Uri("Views/VelocityGraph.xaml", UriKind.Relative), 
                new Uri("Views/VelocityTimed.xaml", UriKind.Relative), new FrameHandler());

            DataContext = viewModel;
        }
    }
}