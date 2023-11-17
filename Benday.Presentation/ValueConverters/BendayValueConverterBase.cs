using System;
using System.Globalization;
using System.Linq;

namespace Benday.Presentation.ValueConverters;

public abstract class BendayValueConverterBase : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return ConvertTo(value, targetType);
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return ConvertFrom(value, targetType);
    }

    protected abstract object? ConvertTo(object? value, Type targetType);
    protected abstract object? ConvertFrom(object? value, Type targetType);
}