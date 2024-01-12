using System;
using System.Linq;

namespace Benday.Presentation;
/// <summary>
/// Interface for requesting showing messages to the user from inside of a view model.
/// </summary>
public interface IMessageManager
{
    /// <summary>
    /// Show a message to the user for an exception.
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    Task ShowMessage(Exception ex);

    /// <summary>
    /// Show a message to the user.
    /// </summary>
    /// <param name="message">The message text to show</param>
    /// <param name="title">The title of the message box</param>
    /// <returns></returns>
    Task ShowMessage(string message, string title);
}


