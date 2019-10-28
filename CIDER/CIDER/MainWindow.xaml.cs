using CIDER.ViewModels;
using MahApps.Metro.Controls;
using System;
using System.Windows;
using System.Windows.Interop;

namespace CIDER
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : MetroWindow
    {
        public static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private MainWindowViewModel viewModel;

        public static event EventHandler OnResizeStartEvent;

        public static event EventHandler OnResizeEndEvent;

        public MainWindow()
        {
            InitializeComponent();

            viewModel = new MainWindowViewModel();

            DataContext = viewModel;

            viewModel.OnFrameChangeEvent += ViewModel_OnFrameChangeEvent;

            viewModel.Initalize();
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            HwndSource source = PresentationSource.FromVisual(this) as HwndSource;
            source.AddHook(WndProc);
        }

        private void ViewModel_OnFrameChangeEvent(object sender, EventArgs e)
        {
            try
            {
                frmMain.Navigate(viewModel.FrameContent);
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Error whilst changing View");
            }
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            viewModel.OnFrameChangeEvent -= ViewModel_OnFrameChangeEvent;

            viewModel.Dispose();
        }

        private void RaiseResizeStartEvent(EventArgs e)
        {
            EventHandler handler = OnResizeStartEvent;
            if (handler != null)
                handler.Invoke(this, e);
        }

        private void RaiseResizeEndEvent(EventArgs e)
        {
            EventHandler handler = OnResizeEndEvent;
            if (handler != null)
                handler.Invoke(this, e);
        }

        private IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == 561)
            {
                RaiseResizeStartEvent(new EventArgs());
            }

            if (msg == 562)
            {
                RaiseResizeEndEvent(new EventArgs());
            }

            return IntPtr.Zero;
        }
    }
}