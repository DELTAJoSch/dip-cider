using CIDER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for AngleTimed.xaml
    /// </summary>
    public partial class AngleTimed : Page
    {
        private AngleTimedViewModel model;

        public AngleTimed(DataProvider data)
        {
            InitializeComponent();

            model = new AngleTimedViewModel(data);
            this.DataContext = model;
        }

        private void slValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            model.SliderValueChanged((int)slValue.Value);
        }
    }
}