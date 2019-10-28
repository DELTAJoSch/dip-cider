using CIDER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for ArtificialHorizon.xaml
    /// </summary>
    public partial class ArtificialHorizon : Page
    {
        private ArtificialHorizonViewModel model;

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