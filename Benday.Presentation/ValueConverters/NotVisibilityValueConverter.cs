using System;
using System.Globalization;
using System.Linq;

namespace Benday.Presentation.ValueConverters;

/// <summary>
/// A value converter that converts a boolean value to a Visibility value. 
/// If the value supplied is true, then visibility will be returned as false. If the value supplied is false, then the return value is true.
/// </summary>
public class NotVisibilityValueConverter : BendayValueConverterBase
{
    protected override object? ConvertTo(object? value, Type targetType)
    {
        if (value == null)
        {
            return Visibility.Visible;
        }

        bool valueAsBoolean = (bool)value;

        if (valueAsBoolean == false)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    protected override object? ConvertFrom(object? value, Type targetType)
    {
        if (value == null)
        {
            return false;
        }

        if (value is bool)
        {
            var valueAsBoolean = (bool)value;

            return !valueAsBoolean;
        }
        else
        {
            return false;
        }
    }
}
