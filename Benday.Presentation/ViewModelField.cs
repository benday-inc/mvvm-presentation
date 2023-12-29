using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Benday.Presentation;

public class ViewModelField<T> : ViewModelBase, INotifyPropertyChanged, IVisibleField
{
    public ViewModelField()
    {
        IsValid = true;
        IsVisible = true;
        IsEnabled = true;
        ValidationMessage = String.Empty;
    }

    public ViewModelField(T initialValue)
        : this()
    {
        _Value = initialValue;
    }

    private T? _Value;
    public T Value
    {
        get
        {
            if (_Value is null)
            {
                if (typeof(T) == typeof(string))
                {
                    T temp = (T)(object)string.Empty;

                    return temp;
                }
                else
                {
                    var temp = default(T);

                    if (temp == null)
                    {
                        throw new InvalidOperationException(
                                                       "Cannot return null for type " + typeof(T).FullName);
                    }
                    else
                    {
                        return temp;
                    }
                }
            }
            return _Value;
        }
        set
        {
            if (EqualityComparer<T>.Default.Equals(
                _Value, value) == false)
            {
                _Value = value;
                RaisePropertyChanged("Value");
                RaiseOnValueChanged();
            }
        }
    }

    public event EventHandler? OnValueChanged;
    public virtual void RaiseOnValueChanged()
    {
        var handler = OnValueChanged;

        if (handler != null)
        {
            handler(this, new EventArgs());
        }
    }

    private const string IsVisiblePropertyName = "IsVisible";

    private bool _IsVisible;
    public bool IsVisible
    {
        get
        {
            return _IsVisible;
        }
        set
        {
            _IsVisible = value;
            RaisePropertyChanged(IsVisiblePropertyName);
        }
    }

    private const string IsValidPropertyName = "IsValid";

    private bool _IsValid;
    public bool IsValid
    {
        get
        {
            return _IsValid;
        }
        set
        {
            _IsValid = value;
            RaisePropertyChanged(IsValidPropertyName);
        }
    }

    private const string IsEnabledPropertyName = "IsEnabled";

    private bool _IsEnabled;
    public bool IsEnabled
    {
        get
        {
            return _IsEnabled;
        }
        set
        {
            _IsEnabled = value;
            RaisePropertyChanged(IsEnabledPropertyName);
        }
    }

    private const string ValidationMessagePropertyName = "ValidationMessage";

    private string _ValidationMessage = string.Empty;
    public string ValidationMessage
    {
        get
        {
            return _ValidationMessage;
        }
        set
        {
            _ValidationMessage = value;
            RaisePropertyChanged(ValidationMessagePropertyName);
        }
    }

    public override string ToString()
    {
        if (Value == null)
        {
            return String.Empty;
        }
        else
        {
            return Value.ToString() ?? 
                throw new InvalidOperationException("ViewModelField.Value got a null return value from ToString().");
        }
    }
}


