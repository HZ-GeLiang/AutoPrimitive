namespace AutoPrimitive
{
    using AutoPrimitive.Types;
    using System;

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

        //string
        public static dynamic ToPrimitive(this string obj) => new PrimitiveString(obj);


        //Enum
        public static dynamic ToPrimitive<T>(this T obj) where T : Enum
        {
            return new PrimitiveEnum(obj);
        }


        #region 日期

#if NET6_0_OR_GREATER
        public static dynamic ToPrimitive(this DateTime obj) => new PrimitiveDateTime(obj);
        public static dynamic ToPrimitive(this DateTime obj, string format) => new PrimitiveDateTime(obj, format);

        public static dynamic ToPrimitive(this DateOnly obj) => new PrimitiveDateOnly(obj);
        public static dynamic ToPrimitive(this DateOnly obj, string format) => new PrimitiveDateOnly(obj, format);
#endif
        #endregion
    }
}
