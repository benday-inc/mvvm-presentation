using System;
using System.Linq;

namespace Benday.Presentation;

/// <summary>
/// Delegate for requesting a message box to be shown to the user.
/// </summary>
/// <param name="sender"></param>
/// <param name="args"></param>
public delegate void MessageBoxEventHandler(object sender, MessageBoxEventArgs args);


