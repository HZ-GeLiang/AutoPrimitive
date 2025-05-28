namespace AutoPrimitive;

/// <summary>
/// 基本类型（Primitive Type）的各种类型转换
/// </summary>
public static class PrimitiveTryCatchExtensions
{
    //Activator.CreateInstance(typeof(Primitive<>).MakeGenericType(obj.GetType()), obj);

    //数值类型: short ushort int uint char float double long ulong decimal
    //其他类型: bool byte sbyte

    private static dynamic h<T>(Func<dynamic> func, T defaultValue)
    {
        //确保 new PrimitiveDefault 异常的时候可以有默认值  (感觉没什么用)
        //示例:
        //public static dynamic ToPrimitive(this short obj, short defaultValue) => h(() => new PrimitiveDefault<short>(obj, defaultValue), defaultValue);

        try
        {
            return func();
        }
        catch (Exception)
        {
            return defaultValue;
        }
    }

    #region 值类型

    //数值类型:

    public static dynamic ToPrimitive(this short obj, object defaultValue) => new PrimitiveDefault<short>(obj, defaultValue);

    public static dynamic ToPrimitive(this ushort obj, object defaultValue) => new PrimitiveDefault<ushort>(obj, defaultValue);

    public static dynamic ToPrimitive(this int obj, object defaultValue) => new PrimitiveDefault<int>(obj, defaultValue);

    public static dynamic ToPrimitive(this uint obj, object defaultValue) => new PrimitiveDefault<uint>(obj, defaultValue);

    public static dynamic ToPrimitive(this char obj, object defaultValue) => new PrimitiveDefault<char>(obj, defaultValue);

    public static dynamic ToPrimitive(this float obj, object defaultValue) => new PrimitiveDefault<float>(obj, defaultValue);

    public static dynamic ToPrimitive(this double obj, object defaultValue) => new PrimitiveDefault<double>(obj, defaultValue);

    public static dynamic ToPrimitive(this long obj, object defaultValue) => new PrimitiveDefault<long>(obj, defaultValue);

    public static dynamic ToPrimitive(this ulong obj, object defaultValue) => new PrimitiveDefault<ulong>(obj, defaultValue);

    public static dynamic ToPrimitive(this decimal obj, object defaultValue) => new PrimitiveDefault<decimal>(obj, defaultValue);

    //其他类型:
    public static dynamic ToPrimitive(this bool obj, object defaultValue) => new PrimitiveDefault<bool>(obj, defaultValue);

    public static dynamic ToPrimitive(this byte obj, object defaultValue) => new PrimitiveDefault<byte>(obj, defaultValue);

    public static dynamic ToPrimitive(this sbyte obj, object defaultValue) => new PrimitiveDefault<sbyte>(obj, defaultValue);

    #endregion

    //public static dynamic ToPrimitive(this Guid obj, object defaultValue) => new PrimitiveDefaultGuid(obj, null, defaultValue);

    //public static dynamic ToPrimitive(this Guid obj, string format, object defaultValue) => new PrimitiveDefaultGuid(obj, format, defaultValue);

    #region 值类型:Nullable

    //数值类型:
    public static dynamic ToPrimitive(this short? obj, object defaultValue) => new PrimitiveDefaultNullable<short?>(obj, defaultValue);

    public static dynamic ToPrimitive(this ushort? obj, object defaultValue) => new PrimitiveDefaultNullable<ushort?>(obj, defaultValue);

    public static dynamic ToPrimitive(this int? obj, object defaultValue) => new PrimitiveDefaultNullable<int?>(obj, defaultValue);

    public static dynamic ToPrimitive(this uint? obj, object defaultValue) => new PrimitiveDefaultNullable<uint?>(obj, defaultValue);

    public static dynamic ToPrimitive(this char? obj, object defaultValue) => new PrimitiveDefaultNullable<char?>(obj, defaultValue);

    public static dynamic ToPrimitive(this float? obj, object defaultValue) => new PrimitiveDefaultNullable<float?>(obj, defaultValue);

    public static dynamic ToPrimitive(this double? obj, object defaultValue) => new PrimitiveDefaultNullable<double?>(obj, defaultValue);

    public static dynamic ToPrimitive(this long? obj, object defaultValue) => new PrimitiveDefaultNullable<long?>(obj, defaultValue);

    public static dynamic ToPrimitive(this ulong? obj, object defaultValue) => new PrimitiveDefaultNullable<ulong?>(obj, defaultValue);

    public static dynamic ToPrimitive(this decimal? obj, object defaultValue) => new PrimitiveDefaultNullable<decimal?>(obj, defaultValue);

    //其他类型:
    public static dynamic ToPrimitive(this bool? obj, object defaultValue) => new PrimitiveDefaultNullable<bool?>(obj, defaultValue);

    public static dynamic ToPrimitive(this byte? obj, object defaultValue) => new PrimitiveDefaultNullable<byte?>(obj, defaultValue);

    public static dynamic ToPrimitive(this sbyte? obj, object defaultValue) => new PrimitiveDefaultNullable<sbyte?>(obj, defaultValue);

    #endregion

    //public static dynamic ToPrimitive(this Guid? obj, Guid? defaultValue) => new PrimitiveDefaultGuid(obj, null);

    //public static dynamic ToPrimitive(this Guid? obj, string format, Guid? defaultValue) => new PrimitiveDefaultGuid(obj, format);

    //string
    public static dynamic ToPrimitive(this string obj, object defaultValue) => new PrimitiveDefaultString(obj, defaultValue);

    #region 枚举 Enum

    //    public static dynamic ToPrimitive(this Enum enumItem, object defaultValue)
    //    {
    //        return new PrimitiveDefaultEnum(enumItem);
    //    }

    //    //byte、sbyte、short、ushort、int、uint、long 或 ulong

    //    public static dynamic ToPrimitive<T>(this byte value, object defaultValue) where T : Enum
    //    {
    //        return Enum.Parse(typeof(T), value.ToString());
    //    }

    //    public static dynamic ToPrimitive<T>(this sbyte value, object defaultValue) where T : Enum
    //    {
    //        return Enum.Parse(typeof(T), value.ToString());
    //    }

    //    public static dynamic ToPrimitive<T>(this short value, object defaultValue) where T : Enum
    //    {
    //        return Enum.Parse(typeof(T), value.ToString());
    //    }

    //    public static dynamic ToPrimitive<T>(this ushort value, object defaultValue) where T : Enum
    //    {
    //        return Enum.Parse(typeof(T), value.ToString());
    //    }

    //    public static dynamic ToPrimitive<T>(this int value, object defaultValue) where T : Enum
    //    {
    //        return Enum.Parse(typeof(T), value.ToString());
    //    }

    //    public static dynamic ToPrimitive<T>(this uint value, object defaultValue) where T : Enum
    //    {
    //        return Enum.Parse(typeof(T), value.ToString());
    //    }

    //    public static dynamic ToPrimitive<T>(this long value, object defaultValue) where T : Enum
    //    {
    //        return Enum.Parse(typeof(T), value.ToString());
    //    }

    //    public static dynamic ToPrimitive<T>(this ulong value, object defaultValue) where T : Enum
    //    {
    //        return Enum.Parse(typeof(T), value.ToString());
    //    }

    //    public static dynamic ToPrimitive<T>(this string value, object defaultValue) where T : Enum
    //    {
    //        return Enum.Parse(typeof(T), value);
    //    }

    //    public static dynamic ToPrimitive<TOtherEnum>(this Enum primitive, object defaultValue) where TOtherEnum : Enum
    //    {
    //#if NETSTANDARD2_1_OR_GREATER || NETCOREAPP2_0_OR_GREATER
    //        //DescriptionAttribute 的 优先级 > 值对应
    //        var key = Enum.GetName(primitive.GetType(), primitive);

    //        var fields = typeof(TOtherEnum).GetFields();

    //        foreach (var field in fields)
    //        {
    //            var attribute = field.GetCustomAttribute<DescriptionAttribute>();

    //            if (attribute is null)
    //            {
    //                continue;
    //            }

    //            if (key == attribute.Description)
    //            {
    //                var obj = (TOtherEnum)field.GetValue(typeof(TOtherEnum));
    //                return obj;
    //            }
    //        }

    //#endif

    //        return Enum.Parse(typeof(TOtherEnum), Convert.ToInt32(primitive).ToString());//按枚举值转换
    //    }

    #endregion

    #region 日期

    //public static dynamic ToPrimitive(this DateTime obj, object defaultValue) => new PrimitiveDateTime(obj, PrimitiveDateTimeConfig.DefaultFormat);

    //public static dynamic ToPrimitive(this DateTime obj, string format, object defaultValue) => new PrimitiveDateTime(obj, format);

    //public static dynamic ToPrimitive(this DateTime? obj, object defaultValue) => new PrimitiveDateTimeNullable(obj, PrimitiveDateTimeConfig.DefaultFormat);

    //public static dynamic ToPrimitive(this DateTime? obj, string format, object defaultValue) => new PrimitiveDateTimeNullable(obj, format);

#if NET6_0_OR_GREATER
    //public static dynamic ToPrimitive(this DateOnly obj, object defaultValue) => new PrimitiveDateOnly(obj);
    //public static dynamic ToPrimitive(this DateOnly obj, string format, object defaultValue) => new PrimitiveDateOnly(obj, format);
#endif

    #endregion
}