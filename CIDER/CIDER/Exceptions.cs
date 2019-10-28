using System;

namespace CIDER
{
    /// <summary>
    /// This class is an exception used in filedialog handling
    /// It is to be raised when the user abruptly exits the filedialog
    /// </summary>
    [Serializable]
    public class FileDialogExitedException : Exception
    {
        /// <summary>
        /// This is the standard constructor
        /// </summary>
        public FileDialogExitedException()
        {
        }

        /// <summary>
        /// This constructor takes a custom message
        /// </summary>
        /// <param name="message">The message to be sent</param>
        public FileDialogExitedException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// This constructor takes a message and a custom innerException
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <param name="inner">The inner exception</param>
        public FileDialogExitedException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// This custom exception is used to signal that the ColorWriter was unable to find a preselected color
    /// </summary>
    [Serializable]
    public class ColorWriterNoColorException : Exception
    {
        /// <summary>
        /// This is the standard constructor
        /// </summary>
        public ColorWriterNoColorException()
        {
        }

        /// <summary>
        /// This constructor takes a custom message
        /// </summary>
        /// <param name="message">The message to be sent</param>
        public ColorWriterNoColorException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// This constructor takes a message and a custom innerException
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <param name="inner">The inner exception</param>
        public ColorWriterNoColorException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }

    /// <summary>
    /// This custom exception signals that the ColorWriter was unable to write the selected theme information
    /// </summary>
    [Serializable]
    public class ColorWriterWritingException : Exception
    {
        /// <summary>
        /// This is the standard constructor
        /// </summary>
        public ColorWriterWritingException()
        {
        }

        /// <summary>
        /// This constructor takes a custom message
        /// </summary>
        /// <param name="message">The message to be sent</param>
        public ColorWriterWritingException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// This constructor takes a message and a custom innerException
        /// </summary>
        /// <param name="message">The message to be sent</param>
        /// <param name="inner">The inner exception</param>
        public ColorWriterWritingException(string message, Exception inner)
            : base(message, inner)
        {
        }
    }
}