using System;
using System.ComponentModel;
using System.Linq;

namespace Benday.Presentation;

public interface IViewModelBase : INotifyPropertyChanged
{
    event MessageBoxEventHandler MessageBoxRequested;
}


