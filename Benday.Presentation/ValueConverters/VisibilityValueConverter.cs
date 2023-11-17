﻿using System;
using System.Linq;

namespace Benday.Presentation.ValueConverters;

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
            bool.TryParse(value.ToString(), out valueAsBoolean);
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
