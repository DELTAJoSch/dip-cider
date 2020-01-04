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