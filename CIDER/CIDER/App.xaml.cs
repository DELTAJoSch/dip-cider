﻿using MahApps.Metro;
using System;
using System.Threading.Tasks;
using System.Windows;

namespace CIDER
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();

        /// <summary>
        /// This function overrides the stnadard OnStartup function
        /// </summary>
        /// <param name="e">The startup event args</param>
        protected override void OnStartup(StartupEventArgs e)
        {
            ThemeManager.AddAccent("CIDER_Standard", new Uri("pack://application:,,,/Themes/CIDER_Standard.xaml"));

            try
            {
                ColorWriter writer = new ColorWriter(new FileReader());
                var thm = writer.GetSetTheming();

                ThemeManager.ChangeAppStyle(App.Current, ThemeManager.GetAccent(thm.Item2), ThemeManager.GetAppTheme(thm.Item1));
            }
            catch (Exception ex)
            {
                logger.Warn(ex, "Error whilst reading theme. Reverting back to standard.");

                // now change app style to the custom accent and current theme
                ThemeManager.ChangeAppStyle(Application.Current,
                                            ThemeManager.GetAccent("CIDER_Standard"),
                                            ThemeManager.GetAppTheme("BaseLight"));
            }

            base.OnStartup(e);

            SetupExceptionHandling();
        }

        private void SetupExceptionHandling()
        {
            AppDomain.CurrentDomain.UnhandledException += (s, e) =>
                LogUnhandledException((Exception)e.ExceptionObject, "AppDomain.CurrentDomain.UnhandledException");

            DispatcherUnhandledException += (s, e) =>
                LogUnhandledException(e.Exception, "Application.Current.DispatcherUnhandledException");

            TaskScheduler.UnobservedTaskException += (s, e) =>
                LogUnhandledException(e.Exception, "TaskScheduler.UnobservedTaskException");
        }

        private void LogUnhandledException(Exception ex, string source)
        {
            string message = $"Unhandled exception ({source})";
            try
            {
                System.Reflection.AssemblyName assemblyName = System.Reflection.Assembly.GetExecutingAssembly().GetName();
                message = string.Format("Unhandled exception in {0} v{1}", assemblyName.Name, assemblyName.Version);
            }
            catch (Exception e)
            {
                logger.Error(e, "Exception in LogUnhandledException");
            }
            finally
            {
                logger.Fatal(ex, message);
                MessageBox.Show("Due to an unexpected error this application is going to close. If you are able to send the log file via email, that would be appreciated.", "An unexpected error ocurred.", MessageBoxButton.OK, MessageBoxImage.Error);
                System.Windows.Application.Current.Shutdown(1);
            }
        }
    }
}