using CIDER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for the AngleGraph page
    /// </summary>
    public partial class AngleGraph : Page
    {
        private AngleGraphViewModel model;

        /// <summary>
        /// The constructor for the AngleGraph page
        /// </summary>
        /// <param name="Data">A DataProvidrt object to read the data from</param>
        public AngleGraph(DataProvider Data)
        {
            InitializeComponent();
            model = new AngleGraphViewModel(Data);
            this.DataContext = model;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            model.Dispose();
        }
    }
}