using CIDER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for AngleGraph.xaml
    /// </summary>
    public partial class AccelerationGraph : Page
    {
        private AccelerationGraphViewModel model;

        public AccelerationGraph(DataProvider _data)
        {
            InitializeComponent();
            model = new AccelerationGraphViewModel(_data);
            this.DataContext = model;
        }

        private void Page_Unloaded(object sender, RoutedEventArgs e)
        {
            model.Dispose();
        }
    }
}