using System;
using System.Diagnostics;
using System.Linq;

namespace Benday.Presentation.ValueConverters;

/// <summary>
/// A value converter that converts a boolean value to a Visibility value.
/// This might be useless in .NET MAUI since MAUI uses booleans for visibility but I'm leaving it in for now.
/// </summary>
public class VisibilityValueConverter : BendayValueConverterBase
{
    protected override object? ConvertTo(object? value, Type targetType)
    {
        bool valueAsBoolean = false;

        if (value is bool)
        {
            valueAsBoolean = (bool)value;
        }
        else if (value != null)
        {
            var result = bool.TryParse(value.ToString(), out valueAsBoolean);

            if (result == false)
            {
                Trace.TraceWarning("Unable to convert '{0}' to boolean.", value);
            }
        }

        if (valueAsBoolean == true)
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
            return true;
        }
        else
        {
            var valueAsVisibility = (Visibility)value;

            if (valueAsVisibility == Visibility.Visible)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
