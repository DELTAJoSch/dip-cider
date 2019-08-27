using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER
{
    public interface IProcessStarter
    ///Summary
    ///This interface is used as an abstraction. This is done so the classes using this class can be unit tested. It is a seam.
    {
        void Start(ProcessStartInfo info);
    }
}
