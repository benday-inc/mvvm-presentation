using System;
using System.Linq;

namespace Benday.Presentation.ValueConverters;

/// <summary>
/// A value converter that calls ToString() using a format string and returns the value.
/// This only converts one way, from the source to the target.
/// </summary>
public class StringFormatValueConverter : BendayValueConverterBase
{
    private readonly string _FormatString;

    /// <summary>
    /// Creates a new <see cref="StringFormatValueConverter"/>
    /// </summary>
    /// <param name="formatString">Format string, it can take zero or one parameters, the first one being replaced by the source value</param>
    public StringFormatValueConverter(string formatString) : base()
    {
        _FormatString = formatString;
    }

    protected override object? ConvertTo(object? value, Type targetType)
    {
        return string.Format(System.Globalization.CultureInfo.CurrentUICulture, _FormatString, value);
    }

    protected override object? ConvertFrom(object? value, Type targetType)
    {
        throw new NotImplementedException();
    }
}
