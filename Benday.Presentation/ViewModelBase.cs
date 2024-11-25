using System;
using System.ComponentModel;
using System.Linq;

namespace Benday.Presentation;

/// <summary>
/// Base class for view models that provides an implementation of INotifyPropertyChanged.
/// </summary>
public abstract class ViewModelBase : INotifyPropertyChanged
{
    public void RaisePropertyChanged(string propertyName)
    {
        if (PropertyChanged != null)
        {
            var temp = PropertyChanged;

            if (temp != null)
            {
                temp(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    public bool HasPropertyChangedSubscriber
    {
        get
        {
            return PropertyChanged != null;
        }
    }
}


