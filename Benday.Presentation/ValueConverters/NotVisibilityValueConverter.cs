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
