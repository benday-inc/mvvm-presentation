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

/// <summary>
/// View model class that represents a field on a form. For example, "FirstName", "LastName", "Age", or "BirthDate".
/// It provides properties for validation, visibility, and enabled. If you're using Benday.Presentation.Controls, 
/// those field controls will bind to the properties on this class with a single, simple binding expression.
/// </summary>
/// <typeparam name="T">Data type being wrapped by this field. Typically types like int, string, DateTime, etc.</typeparam>
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
    /// <summary>
    /// Gets or sets the Value of this field. This observable property.
    /// </summary>
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

    /// <summary>
    /// Event raised when the Value property changes on this field.
    /// </summary>
    public event EventHandler? OnValueChanged;
    
    /// <summary>
    /// Raises the OnValueChanged event.
    /// </summary>
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
    /// <summary>
    /// Should the field be visible on the form?
    /// </summary>
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
    /// <summary>
    /// Represents the validation status of this field.  If IsValid is false, then ValidationMessage should contain a message.
    /// NOTE: this class does not perform any validation logic on its own.  It is up to the consumer to set this value and if necessary the 
    /// ValidationMessage value.
    /// </summary>
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
    /// <summary>
    /// Should the field be enabled on the form?
    /// </summary>
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
    /// <summary>
    /// Gets or sets the ValidationMessage property. This observable property. This is intended to be displayed when IsValid is false.
    /// NOTE: this class does not do any validation logic on its own.  It is up to the consumer to set this value.
    /// </summary>
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

    /// <summary>
    /// Returns the Value property of this field as a string. If Value is null, returns String.Empty.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
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


