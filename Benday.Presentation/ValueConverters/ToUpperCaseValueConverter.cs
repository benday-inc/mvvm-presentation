namespace Benday.Presentation.ValueConverters;

/// <summary>
/// A value converter that converts a string value to an upper case string value.
/// This is a one-way converter.
/// </summary>
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