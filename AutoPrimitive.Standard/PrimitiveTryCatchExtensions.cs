using System.ComponentModel;
using System.Reflection;

namespace AutoPrimitive;

/// <summary>
/// 基本类型（Primitive Type）的各种类型转换
/// </summary>
public static class PrimitiveTryCatchExtensions
{
    //Activator.CreateInstance(typeof(Primitive<>).MakeGenericType(obj.GetType()), obj);

    //数值类型: short ushort int uint char float double long ulong decimal
    //其他类型: bool byte sbyte

    public static dynamic h<T>(Func<dynamic> func, T defaultValue)
    {
        try
        {
            return func();
        }
        catch (Exception ex)
        {
            return defaultValue;
        }
    }

    #region 值类型

    //数值类型:
    public static dynamic ToPrimitive(this short obj, short defaultValue) => h(() => new PrimitiveDefault<short>(obj, defaultValue), defaultValue);

    public static dynamic ToPrimitive(this ushort obj, ushort defaultValue) => h(() => new PrimitiveDefault<ushort>(obj, defaultValue), defaultValue);

    public static dynamic ToPrimitive(this int obj, int defaultValue) => h(() => new PrimitiveDefault<int>(obj, defaultValue), defaultValue);

    public static dynamic ToPrimitive(this uint obj, uint defaultValue) => h(() => new PrimitiveDefault<uint>(obj, defaultValue), defaultValue);

    public static dynamic ToPrimitive(this char obj, char defaultValue) => h(() => new PrimitiveDefault<char>(obj, defaultValue), defaultValue);

    public static dynamic ToPrimitive(this float obj, float defaultValue) => h(() => new PrimitiveDefault<float>(obj, defaultValue), defaultValue);

    public static dynamic ToPrimitive(this double obj, double defaultValue) => h(() => new PrimitiveDefault<double>(obj, defaultValue), defaultValue);

    public static dynamic ToPrimitive(this long obj, long defaultValue) => h(() => new PrimitiveDefault<long>(obj, defaultValue), defaultValue);

    public static dynamic ToPrimitive(this ulong obj, ulong defaultValue) => h(() => new PrimitiveDefault<ulong>(obj, defaultValue), defaultValue);

    public static dynamic ToPrimitive(this decimal obj, decimal defaultValue) => h(() => new PrimitiveDefault<decimal>(obj, defaultValue), defaultValue);

    //其他类型:
    public static dynamic ToPrimitive(this bool obj, bool defaultValue) => new PrimitiveDefault<bool>(obj, defaultValue);

    public static dynamic ToPrimitive(this byte obj, byte defaultValue) => new PrimitiveDefault<byte>(obj, defaultValue);

    public static dynamic ToPrimitive(this sbyte obj, sbyte defaultValue) => new PrimitiveDefault<sbyte>(obj, defaultValue);

    #endregion

    public static dynamic ToPrimitive(this Guid obj, Guid defaultValue) => new PrimitiveGuid(obj, null);

    public static dynamic ToPrimitive(this Guid obj, string format, Guid defaultValue) => new PrimitiveGuid(obj, format);

    #region 值类型:Nullable

    //数值类型:
    public static dynamic ToPrimitive(this short? obj, short? defaultValue) => new PrimitiveNullable<short?>(obj);

    public static dynamic ToPrimitive(this ushort? obj, ushort? defaultValue) => new PrimitiveNullable<ushort?>(obj);

    public static dynamic ToPrimitive(this int? obj, int? defaultValue) => new PrimitiveNullable<int?>(obj);

    public static dynamic ToPrimitive(this uint? obj, uint? defaultValue) => new PrimitiveNullable<uint?>(obj);

    public static dynamic ToPrimitive(this char? obj, char? defaultValue) => new PrimitiveNullable<char?>(obj);

    public static dynamic ToPrimitive(this float? obj, float? defaultValue) => new PrimitiveNullable<float?>(obj);

    public static dynamic ToPrimitive(this double? obj, double? defaultValue) => new PrimitiveNullable<double?>(obj);

    public static dynamic ToPrimitive(this long? obj, long? defaultValue) => new PrimitiveNullable<long?>(obj);

    public static dynamic ToPrimitive(this ulong? obj, ulong? defaultValue) => new PrimitiveNullable<ulong?>(obj);

    public static dynamic ToPrimitive(this decimal? obj, decimal? defaultValue) => new PrimitiveNullable<decimal?>(obj);

    //其他类型:
    public static dynamic ToPrimitive(this bool? obj, bool? defaultValue) => new PrimitiveNullable<bool?>(obj);

    public static dynamic ToPrimitive(this byte? obj, byte? defaultValue) => new PrimitiveNullable<byte?>(obj);

    public static dynamic ToPrimitive(this sbyte? obj, sbyte? defaultValue) => new PrimitiveNullable<sbyte?>(obj);

    #endregion

    public static dynamic ToPrimitive(this Guid? obj, Guid? defaultValue) => new PrimitiveGuidNullable(obj, null);

    public static dynamic ToPrimitive(this Guid? obj, string format, Guid? defaultValue) => new PrimitiveGuidNullable(obj, format);

    //string
    public static dynamic ToPrimitive(this string obj, string defaultValue) => new PrimitiveString(obj);

    #region 枚举

    //Enum

    public static dynamic ToPrimitive(this Enum enumItem, object defaultValue)
    {
        return new PrimitiveEnum(enumItem);
    }

    //byte、sbyte、short、ushort、int、uint、long 或 ulong

    public static dynamic ToPrimitive<T>(this byte value, byte defaultValue) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this sbyte value, sbyte defaultValue) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this short value, short defaultValue) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this ushort value, ushort defaultValue) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this int value, int defaultValue) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this uint value, uint defaultValue) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this long value, long defaultValue) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this ulong value, ulong defaultValue) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this string value, string defaultValue) where T : Enum
    {
        return Enum.Parse(typeof(T), value);
    }

    public static dynamic ToPrimitive<TOtherEnum>(this Enum primitive, object defaultValue) where TOtherEnum : Enum
    {
#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0_OR_GREATER
        //DescriptionAttribute 的 优先级 > 值对应
        var key = Enum.GetName(primitive.GetType(), primitive);

        var fields = typeof(TOtherEnum).GetFields();

        foreach (var field in fields)
        {
            var attribute = field.GetCustomAttribute<DescriptionAttribute>();

            if (attribute is null)
            {
                continue;
            }

            if (key == attribute.Description)
            {
                var obj = (TOtherEnum)field.GetValue(typeof(TOtherEnum));
                return obj;
            }
        }

#endif

        return Enum.Parse(typeof(TOtherEnum), Convert.ToInt32(primitive).ToString());//按枚举值转换
    }

    #endregion

    #region 日期

    public static dynamic ToPrimitive(this DateTime obj, DateTime defaultValue) => new PrimitiveDateTime(obj, PrimitiveDateTimeConfig.DefaultFormat);

    public static dynamic ToPrimitive(this DateTime obj, string format, DateTime defaultValue) => new PrimitiveDateTime(obj, format);

    public static dynamic ToPrimitive(this DateTime? obj, DateTime? defaultValue) => new PrimitiveDateTimeNullable(obj, PrimitiveDateTimeConfig.DefaultFormat);

    public static dynamic ToPrimitive(this DateTime? obj, string format, DateTime? defaultValue) => new PrimitiveDateTimeNullable(obj, format);

#if NET6_0_OR_GREATER
    public static dynamic ToPrimitive(this DateOnly obj, DateOnly defaultValue) => new PrimitiveDateOnly(obj);
    public static dynamic ToPrimitive(this DateOnly obj, string format, DateOnly defaultValue) => new PrimitiveDateOnly(obj, format);
#endif
    #endregion
}