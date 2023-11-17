using System;
using System.Diagnostics;
using System.Linq;

namespace Benday.Presentation;

public class MessageBoxMessageManager : IMessageManager
{
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

    public async Task ShowMessage(string message, string title = "Info")
    {        
        await Shell.Current.CurrentPage.DisplayAlert(title, message, "OK");
    }
}
