using System;

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

    [Serializable]
    public class ColorWriterNoColorException : Exception
    ///Summary
    ///This is a custom exception. It is used to show that a file dialog has been exited without selecting any files/folders
    {
        public ColorWriterNoColorException()
        {
        }

        public ColorWriterNoColorException(string message)
            : base(message)
        {
        }

        public ColorWriterNoColorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    [Serializable]
    public class ColorWriterWritingException : Exception
    ///Summary
    ///This is a custom exception. It is used to show that a file dialog has been exited without selecting any files/folders
    {
        public ColorWriterWritingException()
        {
        }

        public ColorWriterWritingException(string message)
            : base(message)
        {
        }

        public ColorWriterWritingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}