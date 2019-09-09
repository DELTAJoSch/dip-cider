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
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        MainWindowViewModel viewModel;
        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWindowViewModel();

            DataContext = viewModel;

            viewModel.OnFrameChangeEvent += ViewModel_OnFrameChangeEvent;

            viewModel.Initalize();
        }

        private void ViewModel_OnFrameChangeEvent(object sender, EventArgs e)
        {
            try
            {
                frmMain.Navigate(viewModel.FrameContent);
            }catch(Exception ex)
            {
                logger.Warn(ex, "Error whilst changing View");
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            viewModel.OnFrameChangeEvent -= ViewModel_OnFrameChangeEvent;

            viewModel.Dispose();
        }
    }
}