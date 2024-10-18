using System.ComponentModel;
using System.Reflection;

namespace AutoPrimitive;

/*
 https://learn.microsoft.com/zh-tw/dotnet/api/system.data.metadata.edm.primitivetypekind?view=netframework-4.8.1
*/

/// <summary>
/// 基本类型（Primitive Type）的各种类型转换
/// </summary>
public static class PrimitiveExtensions
{
    //Activator.CreateInstance(typeof(Primitive<>).MakeGenericType(obj.GetType()), obj);

    //数值类型: short ushort int uint char float double long ulong decimal
    //其他类型: bool byte sbyte

    #region 值类型

    //数值类型:
    public static dynamic ToPrimitive(this short obj) => new Primitive<short>(obj);

    public static dynamic ToPrimitive(this ushort obj) => new Primitive<ushort>(obj);

    public static dynamic ToPrimitive(this int obj) => new Primitive<int>(obj);

    public static dynamic ToPrimitive(this uint obj) => new Primitive<uint>(obj);

    public static dynamic ToPrimitive(this char obj) => new Primitive<char>(obj);

    public static dynamic ToPrimitive(this float obj) => new Primitive<float>(obj);

    public static dynamic ToPrimitive(this double obj) => new Primitive<double>(obj);

    public static dynamic ToPrimitive(this long obj) => new Primitive<long>(obj);

    public static dynamic ToPrimitive(this ulong obj) => new Primitive<ulong>(obj);

    public static dynamic ToPrimitive(this decimal obj) => new Primitive<decimal>(obj);

    //其他类型:
    public static dynamic ToPrimitive(this bool obj) => new Primitive<bool>(obj);

    public static dynamic ToPrimitive(this byte obj) => new Primitive<byte>(obj);

    public static dynamic ToPrimitive(this sbyte obj) => new Primitive<sbyte>(obj);

    #endregion

    public static dynamic ToPrimitive(this Guid obj) => new PrimitiveGuid(obj, null);

    public static dynamic ToPrimitive(this Guid obj, string format) => new PrimitiveGuid(obj, format);

    #region 值类型:Nullable

    //数值类型:
    public static dynamic ToPrimitive(this short? obj) => new PrimitiveNullable<short?>(obj);

    public static dynamic ToPrimitive(this ushort? obj) => new PrimitiveNullable<ushort?>(obj);

    public static dynamic ToPrimitive(this int? obj) => new PrimitiveNullable<int?>(obj);

    public static dynamic ToPrimitive(this uint? obj) => new PrimitiveNullable<uint?>(obj);

    public static dynamic ToPrimitive(this char? obj) => new PrimitiveNullable<char?>(obj);

    public static dynamic ToPrimitive(this float? obj) => new PrimitiveNullable<float?>(obj);

    public static dynamic ToPrimitive(this double? obj) => new PrimitiveNullable<double?>(obj);

    public static dynamic ToPrimitive(this long? obj) => new PrimitiveNullable<long?>(obj);

    public static dynamic ToPrimitive(this ulong? obj) => new PrimitiveNullable<ulong?>(obj);

    public static dynamic ToPrimitive(this decimal? obj) => new PrimitiveNullable<decimal?>(obj);

    //其他类型:
    public static dynamic ToPrimitive(this bool? obj) => new PrimitiveNullable<bool?>(obj);

    public static dynamic ToPrimitive(this byte? obj) => new PrimitiveNullable<byte?>(obj);

    public static dynamic ToPrimitive(this sbyte? obj) => new PrimitiveNullable<sbyte?>(obj);

    #endregion

    public static dynamic ToPrimitive(this Guid? obj) => new PrimitiveGuidNullable(obj, null);

    public static dynamic ToPrimitive(this Guid? obj, string format) => new PrimitiveGuidNullable(obj, format);

    //string
    public static dynamic ToPrimitive(this string obj) => new PrimitiveString(obj);

    #region 枚举

    //Enum

    public static dynamic ToPrimitive(this Enum enumItem)
    {
        return new PrimitiveEnum(enumItem);
    }

    //byte、sbyte、short、ushort、int、uint、long 或 ulong

    public static dynamic ToPrimitive<T>(this byte value) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this sbyte value) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this short value) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this ushort value) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this int value) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this uint value) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this long value) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this ulong value) where T : Enum
    {
        return Enum.Parse(typeof(T), value.ToString());
    }

    public static dynamic ToPrimitive<T>(this string value) where T : Enum
    {
        return Enum.Parse(typeof(T), value);
    }

    public static dynamic ToPrimitive<TOtherEnum>(this Enum primitive) where TOtherEnum : Enum
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

    public static dynamic ToPrimitive(this DateTime obj) => new PrimitiveDateTime(obj, PrimitiveDateTimeConfig.DefaultFormat);

    public static dynamic ToPrimitive(this DateTime obj, string format) => new PrimitiveDateTime(obj, format);

    public static dynamic ToPrimitive(this DateTime? obj) => new PrimitiveDateTimeNullable(obj, PrimitiveDateTimeConfig.DefaultFormat);

    public static dynamic ToPrimitive(this DateTime? obj, string format) => new PrimitiveDateTimeNullable(obj, format);

#if NET6_0_OR_GREATER
    public static dynamic ToPrimitive(this DateOnly obj) => new PrimitiveDateOnly(obj);
    public static dynamic ToPrimitive(this DateOnly obj, string format) => new PrimitiveDateOnly(obj, format);
#endif
    #endregion
}