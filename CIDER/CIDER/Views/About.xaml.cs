using CIDER.ViewModels;
using System.Windows.Controls;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Page
    {
        public About(DataProvider data)
        {
            InitializeComponent();

            var context = new AboutViewModel(new Starter(), new KeyManager(data, new KeyManagerReader()));
            this.DataContext = context;
        }
    }
}