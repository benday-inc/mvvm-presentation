using System;
using System.Linq;

namespace Benday.Presentation;

public class MessageBoxRequestedEventArgs
{
    public MessageBoxRequestedEventArgs(Exception ex)
    {
        Message = ex.ToString();
    }

    public string Message { get; private set; }
}


