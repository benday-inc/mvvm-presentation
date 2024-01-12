using System;
using System.ComponentModel;
using System.Linq;

namespace Benday.Presentation;

/// <summary>
/// Interface for a view model that might need to show a message box to the user.
/// </summary>
public interface IViewModelBase : INotifyPropertyChanged
{
    event MessageBoxEventHandler MessageBoxRequested;
}


