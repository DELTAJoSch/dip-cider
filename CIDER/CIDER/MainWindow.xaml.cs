/* Copyright (C) 2020  Johannes Schiemer 
	This program is free software: you can redistribute it and/or modify 
	it under the terms of the GNU General Public License as published by 
	the Free Software Foundation, either version 3 of the License, or 
	(at your option) any later version. 
	This program is distributed in the hope that it will be useful, 
	but WITHOUT ANY WARRANTY; without even the implied warranty of 
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
	GNU General Public License for more details. 
	You should have received a copy of the GNU General Public License 
	along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/
using CIDER.ViewModels;
using MahApps.Metro.Controls;
using MahApps.Metro.Controls.Dialogs;
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
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
        private MainWindowViewModel viewModel;
        private bool firstRender = true;

        /// <summary>
        /// This is the event that is raised when the resizing of the window begins
        /// </summary>
        public static event EventHandler OnResizeStartEvent;

        /// <summary>
        /// This is the event that is raised when the resize of the window ends
        /// </summary>
        public static event EventHandler OnResizeEndEvent;

        /// <summary>
        /// This is the constructo for the main window
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();

            var data = new DataProvider();

            viewModel = new MainWindowViewModel(new KeyManager(data, new FileReader()), data, new FileReader(), DialogCoordinator.Instance);

            DataContext = viewModel;

            viewModel.OnFrameChangeEvent += ViewModel_OnFrameChangeEvent;
        }

        /// <summary>
        /// This function overrides the standard onsourceinitialized function
        /// </summary>
        /// <param name="e">The event args of the init event</param>
        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            // This gets the Window handle and connects our own wndproc check so we can raise custom events
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

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            viewModel.Initalize();
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