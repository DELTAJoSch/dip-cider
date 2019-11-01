using CIDER.MVVMBase;
using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CIDER.ViewModels
{
    /// <summary>
    /// The ViewModel for the ThemeStyler window
    /// </summary>
    public class ThemeStylerViewModel : ViewModelBase
    {
        private List<string> _accentColorItemSource;

        private readonly DelegateCommand _darkThemeSelectedCommand;
        private readonly DelegateCommand _lightThemeSelectedCommand;

        private ColorWriter writer;

        /// <summary>
        /// The constructor for the ThemeStyler viewmodel
        /// </summary>
        public ThemeStylerViewModel()
        {
            List<String> source = new List<String>();

            Parallel.ForEach(ThemeManager.Accents, x =>
            {
                source.Add(x.Name);
            });

            writer = new ColorWriter(new FileReader());

            source.Sort();

            AccentColorItemSource = source;

            _lightThemeSelectedCommand = new DelegateCommand(LightThemeSelectedCommand);
            _darkThemeSelectedCommand = new DelegateCommand(DarkThemeSelectedCommand);
        }

        /// <summary>
        /// This list contains all available accent colors
        /// </summary>
        public List<string> AccentColorItemSource { get { return _accentColorItemSource; } private set { SetProperty(ref _accentColorItemSource, value); } }

        /// <summary>
        /// This is the command that is fired when the dark theme button is pressed
        /// </summary>
        public ICommand DarkThemeCommand => _darkThemeSelectedCommand;

        /// <summary>
        /// This is the command that is fired when the light theme button is pressed
        /// </summary>
        public ICommand LightThemeCommand => _lightThemeSelectedCommand;

        /// <summary>
        /// This function is called when a new color is selected
        /// </summary>
        /// <param name="color">The name of the selected color</param>
        public void AccentColorChanged(string color)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(App.Current, ThemeManager.GetAccent(color), theme.Item1);

            writer.SetTheming(color, theme.Item1.Name);
        }

        private void DarkThemeSelectedCommand(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(App.Current, theme.Item2, ThemeManager.GetAppTheme("BaseDark"));

            writer.SetTheming(theme.Item2.Name, "BaseDark");
        }

        private void LightThemeSelectedCommand(object sender)
        {
            var theme = ThemeManager.DetectAppStyle(Application.Current);
            ThemeManager.ChangeAppStyle(App.Current, theme.Item2, ThemeManager.GetAppTheme("BaseLight"));

            writer.SetTheming(theme.Item2.Name, "BaseLight");
        }
    }
}