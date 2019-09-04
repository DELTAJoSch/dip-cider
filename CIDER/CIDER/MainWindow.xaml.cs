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
        MainWindowViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWindowViewModel(frmMain);

            DataContext = viewModel;
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            viewModel.Dispose();
        }
    }
}