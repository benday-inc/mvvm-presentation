using System;
using System.Linq;

namespace Benday.Presentation;

/// <summary>
/// Event arguments for requesting that a message box to be shown to the user.
/// </summary>
public class MessageBoxEventArgs : EventArgs
{
    public MessageBoxEventArgs(string message)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentException("message is null or empty.", "message");

        Message = message;
        IsUnexpectedException = false;
        Exception = null;
    }

    /// <summary>
    /// Initializes a new instance of the MessageBoxEventArgs class.
    /// </summary>
    public MessageBoxEventArgs(string message, bool isUnexpectedException, Exception exception)
    {
        if (string.IsNullOrEmpty(message))
            throw new ArgumentException("message is null or empty.", "message");
        if (exception == null)
            throw new ArgumentNullException("exception", "exception is null.");

        Message = message;
        IsUnexpectedException = isUnexpectedException;
        Exception = exception;
    }

    /// <summary>
    /// Initializes a new instance of the MessageBoxEventArgs class.
    /// </summary>
    public MessageBoxEventArgs(string message, Exception exception)
        : this(message, true, exception)
    {

    }

    /// <summary>
    /// The message to show to the user.
    /// </summary>
    public string Message { get; private set; }

    /// <summary>
    /// True if the exception was unexpected.
    /// </summary>
    public bool IsUnexpectedException { get; private set; }

    /// <summary>
    /// The exception that was thrown.
    /// </summary>
    public Exception? Exception { get; private set; }
}


