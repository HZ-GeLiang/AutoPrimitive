﻿using AutoPrimitive.Utils;
using System;

namespace AutoPrimitive
{
    /*
     注:字符串转枚举没法实现
    字符串转其他类型时, 如果转失败, 返回 default 值
    */

    public readonly struct PrimitiveString : IEquatable<PrimitiveString>
    {
        public PrimitiveString(string val)
        {
            Value = val;
        }

        public string Value { get; }

        public static implicit operator PrimitiveString(string val) => new PrimitiveString(val);

        //数值类型: short ushort int uint char float double long ulong decimal
        public static implicit operator short(PrimitiveString primitive) => short.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator ushort(PrimitiveString primitive) => ushort.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator int(PrimitiveString primitive)
        {
            if (int.TryParse(primitive.Value, out var result))
            {
                return result;
            }
            try
            {
                return MathUtil.ToInt32(primitive.Value);
            }
            catch (Exception)
            {

            }
            return default;
        }

        public static implicit operator uint(PrimitiveString primitive) => uint.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator char(PrimitiveString primitive) => char.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator float(PrimitiveString primitive) => float.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator double(PrimitiveString primitive)
        {
            if (double.TryParse(primitive.Value, out var result))
            {
                return result;
            }
            try
            {
                return MathUtil.ToDouble(primitive.Value);
            }
            catch (Exception)
            {

            }
            return default;

        }

        public static implicit operator long(PrimitiveString primitive)
        {
            if (long.TryParse(primitive.Value, out var result))
            {
                return result;
            }
            try
            {
                return MathUtil.ToInt64(primitive.Value);
            }
            catch (Exception)
            {

            }
            return default;
        }

        public static implicit operator ulong(PrimitiveString primitive) => ulong.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator decimal(PrimitiveString primitive)
        {
            if (decimal.TryParse(primitive.Value, out var result))
            {
                return result;
            }
            try
            {
                return MathUtil.ToDecimal(primitive.Value);
            }
            catch (Exception)
            {

            }
            return default;
        }

        public static implicit operator bool(PrimitiveString primitive) =>
            bool.TryParse(primitive.Value, out var result1) && result1 == true ||
            int.TryParse(primitive.Value, out var result2) && result2 != 0;

        public static implicit operator byte(PrimitiveString primitive) => byte.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator sbyte(PrimitiveString primitive) => sbyte.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator Guid(PrimitiveString primitive) => Guid.TryParse(primitive.Value, out var result) ? result : default;

        //可空
        public static implicit operator short?(PrimitiveString primitive) => short.TryParse(primitive.Value, out var result) ? result : default(short?);

        public static implicit operator ushort?(PrimitiveString primitive) => ushort.TryParse(primitive.Value, out var result) ? result : default(ushort?);

        public static implicit operator int?(PrimitiveString primitive)
        {
            if (int.TryParse(primitive.Value, out var result))
            {
                return result;
            }
            try
            {
                return MathUtil.ToInt32(primitive.Value);
            }
            catch (Exception)
            {

            }
            return default(int?);
        }

        public static implicit operator uint?(PrimitiveString primitive) => uint.TryParse(primitive.Value, out var result) ? result : default(uint?);

        public static implicit operator char?(PrimitiveString primitive) => char.TryParse(primitive.Value, out var result) ? result : default(char?);

        public static implicit operator float?(PrimitiveString primitive) => float.TryParse(primitive.Value, out var result) ? result : default(float?);

        public static implicit operator double?(PrimitiveString primitive)
        {
            if (double.TryParse(primitive.Value, out var result))
            {
                return result;
            }
            try
            {
                return MathUtil.ToDouble(primitive.Value);
            }
            catch (Exception)
            {

            }

            return default(double?);
        }

        public static implicit operator long?(PrimitiveString primitive)
        {
            if (long.TryParse(primitive.Value, out var result))
            {
                return result;
            }
            try
            {
                return MathUtil.ToInt64(primitive.Value);
            }
            catch (Exception)
            {

            }

            return default(long?);
        }

        public static implicit operator ulong?(PrimitiveString primitive) => ulong.TryParse(primitive.Value, out var result) ? result : default(ulong?);

        public static implicit operator decimal?(PrimitiveString primitive)
        {
            if (decimal.TryParse(primitive.Value, out var result))
            {
                return result;
            }
            try
            {
                return MathUtil.ToDecimal(primitive.Value);
            }
            catch (Exception)
            {

            }

            return default(decimal?);
        }

        public static implicit operator bool?(PrimitiveString primitive)
        {
            if (primitive == null)
            {
                return null;
            }
            return bool.TryParse(primitive.Value, out var result1) && result1 == true ||
                   int.TryParse(primitive.Value, out var result2) && result2 != 0;
        }

        public static implicit operator byte?(PrimitiveString primitive) => byte.TryParse(primitive.Value, out var result) ? result : default(byte?);

        public static implicit operator sbyte?(PrimitiveString primitive) => sbyte.TryParse(primitive.Value, out var result) ? result : default(sbyte?);

        public static implicit operator Guid?(PrimitiveString primitive) => Guid.TryParse(primitive.Value, out var result) ? result : default(Guid?);

        //日期
        public static implicit operator DateTime(PrimitiveString primitive)
        {
            {
                if (Convert_JS_timestamp(primitive.Value, out var dt))
                {
                    return dt;
                }
            }

            {
                if (DateTime.TryParse(primitive.Value, out var dt))
                {
                    return dt;
                }
            }

            return default;
        }

        public static implicit operator DateTime?(PrimitiveString primitive)
        {
            {
                if (Convert_JS_timestamp(primitive.Value, out var dt))
                {
                    return dt;
                }
            }
            {
                if (DateTime.TryParse(primitive.Value, out var dt))
                {
                    return dt;
                }
            }

            return default(DateTime?);
        }

        /// <summary>
        /// JavaScript时间戳
        /// </summary>
        /// <param name="js_timestamp"></param>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        internal static bool Convert_JS_timestamp(string js_timestamp, out DateTime dateTime)
        {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER

            try
            {
                if (js_timestamp.Length == 13 || js_timestamp.Length == 14) //负数时, 长度要+1
                {
                    // 毫秒级时间戳：毫秒级时间戳是指自 1970 年 1 月 1 日 00:00:00 UTC 起的毫秒数。毫秒级时间戳通常是 13 位长度的数字
                    if (long.TryParse(js_timestamp, out var timestamp))
                    {
                        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                        dateTime = dateTimeOffset.LocalDateTime;
                        return true;
                    }
                }
                else if (js_timestamp.Length == 10 || js_timestamp.Length == 11)  //负数时, 长度要+1
                {
                    // 秒级时间戳是指自 1970 年 1 月 1 日 00:00:00 UTC 起的秒数。秒级时间戳通常是 10 位长度的数字
                    if (long.TryParse(js_timestamp, out var timestamp))
                    {
                        var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                        dateTime = dateTimeOffset.LocalDateTime;
                        return true;
                    }
                }
            }
            catch (Exception ex)
            {
            }
#endif
            dateTime = default;
            return false;
        }

#if NET6_0_OR_GREATER
        public static implicit operator DateOnly(PrimitiveString primitive) => DateOnly.TryParse(primitive.Value, out var result) ? result : default;
        public static implicit operator TimeOnly(PrimitiveString primitive) => TimeOnly.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator DateOnly?(PrimitiveString primitive) => DateOnly.TryParse(primitive.Value, out var result) ? result : default(DateOnly?);
        public static implicit operator TimeOnly?(PrimitiveString primitive) => TimeOnly.TryParse(primitive.Value, out var result) ? result : default(TimeOnly?);
#endif

        //string
        public static implicit operator string(PrimitiveString primitive) => primitive.Value;

#if NETSTANDARD1_3_OR_GREATER || NET6_0_OR_GREATER
        //FormattableString

        public static implicit operator FormattableString(PrimitiveString primitive)
            => primitive.Value == null ? default(FormattableString) : primitive.Value.ToFormattableString();
#endif

        //操作符/方法的重写
        public static bool operator ==(PrimitiveString a, PrimitiveString b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveString a, PrimitiveString b) => !a.Value.Equals(b.Value);

        public override string ToString()
        {
            return Value == null ? null : Value.ToString();
        }

        /// <summary>
        /// 注:如果用 object.Equals来比较的, 请确保 参数1为  Primitive类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            //if (obj.GetType() == this.Value.GetType())
            //{
            //    return object.Equals(obj, this.Value);
            //}

            if (obj == null)
            {
                return Value == null;
            }

            if (obj is PrimitiveString other)
            {
                if (ReferenceEquals(this, other))
                {
                    return true;
                }

                return Equals(other.Value, Value);
            }

            if (Value == null)
            {
                return obj == null;
            }

            return Equals(obj, Convert.ChangeType(Value, obj.GetType())); //使用内置的 ChangeType
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(PrimitiveString other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(other.Value, Value);
        }

        public static PrimitiveString operator +(PrimitiveString a, PrimitiveString b)
            => a.Value == null && b.Value == null ? null : a.Value ?? "" + b.Value ?? "";
    }
}