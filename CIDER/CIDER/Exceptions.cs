using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIDER
{
    [Serializable]
    public class FileDialogExitedException : Exception
    ///Summary
    ///This is a custom exception. It is used to show that a file dialog has been exited without selecting any files/folders
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
