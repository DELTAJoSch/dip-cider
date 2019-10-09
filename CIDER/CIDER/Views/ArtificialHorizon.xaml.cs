using CIDER.ViewModels;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for ArtificialHorizon.xaml
    /// </summary>
    public partial class ArtificialHorizon : Page
    {
        ArtificialHorizonViewModel model;
        public ArtificialHorizon(DataProvider data)
        {
            InitializeComponent();
            model = new ArtificialHorizonViewModel(data);
            this.DataContext = model;
        }

        private void SlValue_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            model.SliderValueChanged((int)slValue.Value);
        }
    }
}
