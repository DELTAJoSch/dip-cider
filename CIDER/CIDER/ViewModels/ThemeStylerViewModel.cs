using CIDER.MVVMBase;
using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CIDER.ViewModels
{
    public class ThemeStylerViewModel : ViewModelBase
    {
        private List<string> _accentColorItemSource;

        private readonly DelegateCommand _darkThemeSelectedCommand;
        private readonly DelegateCommand _lightThemeSelectedCommand;

        private ColorWriter writer;

        public ThemeStylerViewModel()
        {
            List<String> source = new List<String>();

            Parallel.ForEach(ThemeManager.Accents, x =>
            {
                source.Add(x.Name);
            });

            writer = new ColorWriter(new KeyManagerReader());

            source.Sort();

            AccentColorItemSource = source;

            _lightThemeSelectedCommand = new DelegateCommand(LightThemeSelectedCommand);
            _darkThemeSelectedCommand = new DelegateCommand(DarkThemeSelectedCommand);
        }

        public List<string> AccentColorItemSource { get { return _accentColorItemSource; } private set { SetProperty(ref _accentColorItemSource, value); } }
        public ICommand DarkThemeCommand => _darkThemeSelectedCommand;
        public ICommand LightThemeCommand => _lightThemeSelectedCommand;

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