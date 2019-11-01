using CIDER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for the ArtificialHorizon page
    /// </summary>
    public partial class ArtificialHorizon : Page
    {
        private ArtificialHorizonViewModel model;

        /// <summary>
        /// The constructor for the ArtificialHorizon page
        /// </summary>
        /// <param name="data">A DataPRovider object to read the data from</param>
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