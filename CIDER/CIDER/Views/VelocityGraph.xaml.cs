using CIDER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for the VelocityGraph page
    /// </summary>
    public partial class VelocityGraph : Page
    {
        private VelocityGraphViewModel model;

        /// <summary>
        /// This is the constructor of the VelocityGraph page
        /// </summary>
        /// <param name="data">A DataProvider object to read the data from</param>
        public VelocityGraph(DataProvider data)
        {
            InitializeComponent();

            model = new VelocityGraphViewModel(data);
            this.DataContext = model;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            model.Dispose();
        }
    }
}