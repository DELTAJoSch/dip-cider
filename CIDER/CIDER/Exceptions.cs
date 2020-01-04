/* Copyright (C) 2020  Johannes Schiemer 
	This program is free software: you can redistribute it and/or modify 
	it under the terms of the GNU General Public License as published by 
	the Free Software Foundation, either version 3 of the License, or 
	(at your option) any later version. 
	This program is distributed in the hope that it will be useful, 
	but WITHOUT ANY WARRANTY; without even the implied warranty of 
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the 
	GNU General Public License for more details. 
	You should have received a copy of the GNU General Public License 
	along with this program.  If not, see <https://www.gnu.org/licenses/>. 
*/
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