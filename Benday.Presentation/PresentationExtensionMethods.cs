using System;
using System.Linq;

namespace Benday.Presentation;

public static class PresentationExtensionMethods
{
    public static bool CaseInsensitiveContains(this string text, string value,
        StringComparison stringComparison = StringComparison.CurrentCultureIgnoreCase)
    {
        return text.IndexOf(value, stringComparison) >= 0;
    }

    public static bool IsNullOrWhitespace(this string text)
    {
        return string.IsNullOrWhiteSpace(text);
    }

    public static bool IsNull(this string text)
    {
        if (text == null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}