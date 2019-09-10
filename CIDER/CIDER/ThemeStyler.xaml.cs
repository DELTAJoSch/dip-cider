using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using CIDER.ViewModels;
using MahApps.Metro.Controls;

namespace CIDER
{
    /// <summary>
    /// Interaction logic for ThemeStyler.xaml
    /// </summary>
    public partial class ThemeStyler : MetroWindow
    {
        ThemeStylerViewModel model;
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
