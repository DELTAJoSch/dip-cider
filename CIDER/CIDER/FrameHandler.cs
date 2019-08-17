using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CIDER
{
    public class FrameHandler
    // this class provides frame navigation
    {
        public void Navigate(Uri uri, Frame frame)
        {
            frame.Navigate(uri);
        }
    }
}
