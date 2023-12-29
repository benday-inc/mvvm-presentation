using System;
using System.Linq;

namespace Benday.Presentation.ValueConverters;


public class StringOrInt32ValueConverter : BendayValueConverterBase
{

    private object? ConvertBackForInt32(object? value)
    {
        if (value == null)
        {
            return 0;
        }
        else
        {
            var valueAsString = value.ToString();

            if (string.IsNullOrEmpty(valueAsString) == true)
            {
                return 0;
            }
            else
            {
                int returnValue = 0;

                if (int.TryParse(valueAsString, out returnValue) == true)
                {
                    return returnValue;
                }
                else
                {
                    return 0;
                }
            }
        }
    }

    protected override object? ConvertTo(object? value, Type targetType)
    {
        if (value == null)
        {
            return string.Empty;
        }
        else
        {
            return value.ToString();
        }
    }

    protected override object? ConvertFrom(object? value, Type targetType)
    {
        if (targetType == typeof(int))
        {
            return ConvertBackForInt32(value);
        }
        else
        {
            return value;
        }
    }
}
