using CIDER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for the AngleTimed page
    /// </summary>
    public partial class AngleTimed : Page
    {
        private AngleTimedViewModel model;

        /// <summary>
        /// The constructor for the angle timed page
        /// </summary>
        /// <param name="data">A DataProvider object to read the data from</param>
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