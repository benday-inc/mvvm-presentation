using System;
using System.Linq;

namespace Benday.Presentation;
public interface IMessageManager
{
    Task ShowMessage(Exception ex);
    Task ShowMessage(string message, string title);
}


