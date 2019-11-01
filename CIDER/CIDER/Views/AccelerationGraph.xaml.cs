using CIDER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for AccelerationGraph page
    /// </summary>
    public partial class AccelerationGraph : Page
    {
        private AccelerationGraphViewModel model;

        /// <summary>
        /// This is the constructor for the AccelerationGraph page
        /// </summary>
        /// <param name="Data">A DataProvider object to read the data from</param>
        public AccelerationGraph(DataProvider Data)
        {
            InitializeComponent();
            model = new AccelerationGraphViewModel(Data);
            this.DataContext = model;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            model.Dispose();
        }
    }
}