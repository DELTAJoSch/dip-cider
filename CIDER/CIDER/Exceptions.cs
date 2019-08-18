using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER
{
    public class FileDialogExitedException : Exception
    {
        public FileDialogExitedException()
        {
        }

        public FileDialogExitedException(string message)
            : base(message)
        {
        }

        public FileDialogExitedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}
