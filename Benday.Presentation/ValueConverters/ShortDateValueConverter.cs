using System;
using System.Globalization;
using System.Linq;

namespace Benday.Presentation.ValueConverters;

/// <summary>
/// A value converter that converts a DateTime value to a string value using the current culture's short date pattern.
/// </summary>
public class ShortDateValueConverter : BendayValueConverterBase
{
    protected override object? ConvertTo(object? value, Type targetType)
    {
        if (value == null || value is DateTime == false)
        {
            return string.Empty;
        }
        else
        {
            DateTime temp = (DateTime)value;

            return temp.Date.ToString(DateTimeFormatInfo.CurrentInfo.ShortDatePattern);
        }
    }

    protected override object? ConvertFrom(object? value, Type targetType)
    {
        return value;
    }
}
