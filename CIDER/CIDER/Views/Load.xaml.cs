using CIDER.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CIDER.Views
{
    /// <summary>
    /// Interaction logic for Load.xaml
    /// </summary>
    public partial class Load : Page
    {
        private DataProvider _dataProvider;
        public Load(DataProvider data)
        {
            InitializeComponent();
            _dataProvider = data;
            LoadViewModel loadView = new LoadViewModel(_dataProvider, new CIDER.LoadIO.FolderChecker(), new CIDER.LoadIO.FolderSelector(), new CIDER.LoadIO.FileIO());
            this.DataContext = loadView;
        }
    }
}
