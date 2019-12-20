using CIDER.ViewModels;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Page
    {
        /// <summary>
        /// The constructor for the About View
        /// </summary>
        /// <param name="data">a dataProvider object containing the track data, normally handed by the mainViewModel</param>
        public About(DataProvider data)
        {
            InitializeComponent();

            var context = new AboutViewModel(new Starter(), new KeyManager(data, new FileReader()), new Licenser());
            this.DataContext = context;
        }
    }
}