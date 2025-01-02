
#if NETSTANDARD1_3_OR_GREATER || NET6_0_OR_GREATER
using System.Runtime.CompilerServices;

namespace AutoPrimitive;

internal static class StringExtension
{
    public static bool HasValue(this string value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return false;
        }
        return true;
    }

    public static FormattableString ToFormattableString(this string str)
    {
        return FormattableStringFactory.Create(str);
    }

    public static FormattableString ToFormattableString(this string str, object[] parameters)
    {
        if (parameters == null)
        {
            return FormattableStringFactory.Create(str);
        }
        else
        {
            return FormattableStringFactory.Create(str, parameters);
        }
    }
}
#endif
