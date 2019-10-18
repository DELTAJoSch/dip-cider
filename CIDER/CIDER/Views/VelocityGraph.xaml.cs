using CIDER.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for VelocityGraph.xaml
    /// </summary>
    public partial class VelocityGraph : Page
    {
        private VelocityGraphViewModel model;

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