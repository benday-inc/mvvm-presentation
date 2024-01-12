using System;
using System.Diagnostics;
using System.Linq;

namespace Benday.Presentation;

/// <summary>
/// Message manager that shows a message box to the user.
/// This implementation uses the Shell.Current.CurrentPage.DisplayAlert() method.
/// </summary>
public class MessageBoxMessageManager : IMessageManager
{
    /// <summary>
    /// Show a message to the user for an exception.
    /// </summary>
    /// <param name="ex"></param>
    /// <returns></returns>
    public async Task ShowMessage(Exception ex)
    {
        if (ex is null)
        {
            Trace.TraceError("MessageBoxMessageManager got a call to ShowMesage(ex) with a null exception.");
        }
        else
        {
            Trace.TraceError(ex.ToString());

            await ShowMessage(ex.Message, "Error");
        }
    }

    /// <summary>
    /// Show a message to the user.
    /// </summary>
    /// <param name="message"></param>
    /// <param name="title"></param>
    /// <returns></returns>
    public async Task ShowMessage(string message, string title = "Info")
    {        
        await Shell.Current.CurrentPage.DisplayAlert(title, message, "OK");
    }
}
