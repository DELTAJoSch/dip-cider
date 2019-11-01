using CIDER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for VelocityTimed page
    /// </summary>
    public partial class VelocityTimed : Page
    {
        private VelocityTimedViewModel model;

        /// <summary>
        /// This is the constructor of hte VelocityTimed page
        /// </summary>
        /// <param name="data">A DataProvider object to read the data from</param>
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