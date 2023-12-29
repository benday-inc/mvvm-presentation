using System;
using System.Globalization;
using System.Linq;

namespace Benday.Presentation.ValueConverters;

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
public class ToUpperCaseValueConverter : BendayValueConverterBase
{
    protected override object? ConvertTo(object? value, Type targetType)
    {
        if (value == null)
        {
            return String.Empty;
        }
        else
        {
            return value.ToString()!.ToUpper();
        }
    }

    protected override object? ConvertFrom(object? value, Type targetType)
    {
        return value;
    }
}