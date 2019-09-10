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
using System.Windows.Shapes;

namespace CIDER
{
    /// <summary>
    /// Interaction logic for LoadBoxWindow.xaml
    /// </summary>
    public partial class LoadBoxWindow : Window
    {
        public LoadBoxWindow()
        {
            InitializeComponent();
        }

        public void SetProgress(double progress)
        {
            LoadBar.Value = progress;
        }

        public void SetMax(double max)
        {
            LoadBar.Maximum = max;
        }
    }
}
