using CIDER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for VelocityTimed.xaml
    /// </summary>
    public partial class VelocityTimed : Page
    {
        private VelocityTimedViewModel model;

        public VelocityTimed(DataProvider data)
        {
            InitializeComponent();

            model = new VelocityTimedViewModel(data);
            this.DataContext = model;
        }

        private void slValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            model.SliderValueChanged((int)slValue.Value);
        }
    }
}