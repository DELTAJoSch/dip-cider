using CIDER.ViewModels;
using MahApps.Metro.Controls;
using System.Windows.Controls;

namespace CIDER
{
    /// <summary>
    /// Interaction logic for ThemeStyler.xaml
    /// </summary>
    public partial class ThemeStyler : MetroWindow
    {
        private ThemeStylerViewModel model;

        /// <summary>
        /// The constructor for the ThemeStyle Window
        /// </summary>
        public ThemeStyler()
        {
            InitializeComponent();

            model = new ThemeStylerViewModel();
            this.DataContext = model;
        }

        private void AccentColor_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            model.AccentColorChanged(AccentColor.SelectedItem.ToString());
        }
    }
}