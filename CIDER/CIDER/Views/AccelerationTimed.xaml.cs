using CIDER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for the AccelerationTimed page
    /// </summary>
    public partial class AccelerationTimed : Page
    {
        private AccelerationTimedViewModel model;

        /// <summary>
        /// This is the constructor for the AccelerationTimed Window
        /// </summary>
        /// <param name="data">A DataProvider to read the data from</param>
        public AccelerationTimed(DataProvider data)
        {
            InitializeComponent();

            model = new AccelerationTimedViewModel(data);

            this.DataContext = model;
        }

        private void slValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            model.SliderValueChanged((int)slValue.Value);
        }
    }
}