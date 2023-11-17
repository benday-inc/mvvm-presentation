using System;
using System.ComponentModel;
using System.Linq;

namespace Benday.Presentation;

public interface ISelectable : INotifyPropertyChanged
{
    bool IsSelected { get; set; }
}


