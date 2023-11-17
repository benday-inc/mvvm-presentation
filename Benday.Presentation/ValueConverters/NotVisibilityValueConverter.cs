using System;
using System.Globalization;
using System.Linq;

namespace Benday.Presentation.ValueConverters;

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
            return Visibility.Visible;
        }
        else
        {
            return Visibility.Collapsed;
        }
    }

    protected override object? ConvertFrom(object? value, Type targetType)
    {
        if (value == null)
        {
            return false;
        }

        var valueAsVisibility = (Visibility)value;

        if (valueAsVisibility == Visibility.Visible)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}
