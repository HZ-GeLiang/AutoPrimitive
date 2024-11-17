namespace AutoPrimitive
{
    /*
     注:字符串转枚举没法实现
    */

    public readonly struct PrimitiveDefaultString : IEquatable<PrimitiveDefaultString>
    {
        public PrimitiveDefaultString(string val, object @default)
        {
            Value = val;
            Default = @default;
        }

        public static dynamic h<T_Target>(Func<dynamic> func, object @default)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                if (typeof(string) == typeof(T_Target))
                {
                    return @default;
                }
                else
                {
                    return Convert.ChangeType(@default, typeof(T_Target));
                }
            }
        }

        public string Value { get; }
        public object Default { get; }

        public static implicit operator PrimitiveDefaultString(string val) => new PrimitiveDefaultString(val, default(string));

        //数值类型: short ushort int uint char float double long ulong decimal
        public static implicit operator short(PrimitiveDefaultString PrimitiveDefault) => h<short>(() => short.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator ushort(PrimitiveDefaultString PrimitiveDefault) => h<ushort>(() => ushort.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator int(PrimitiveDefaultString PrimitiveDefault) => h<int>(() => int.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator uint(PrimitiveDefaultString PrimitiveDefault) => h<uint>(() => uint.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator char(PrimitiveDefaultString PrimitiveDefault)
        {
            return h<char>(() =>
            {
                //非零即真
                if (char.TryParse(PrimitiveDefault.Value, out var result))
                {
                    return result;
                }
                return PrimitiveDefault.Default;
            }, PrimitiveDefault.Default);
        }

        public static implicit operator float(PrimitiveDefaultString PrimitiveDefault) => h<float>(() => float.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator double(PrimitiveDefaultString PrimitiveDefault) => h<double>(() => double.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator long(PrimitiveDefaultString PrimitiveDefault) => h<long>(() => long.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator ulong(PrimitiveDefaultString PrimitiveDefault) => h<ulong>(() => ulong.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator decimal(PrimitiveDefaultString PrimitiveDefault) => h<decimal>(() => decimal.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator bool(PrimitiveDefaultString PrimitiveDefault)
        {
            return h<bool>(() =>
            {
                //非零即真
                if (bool.TryParse(PrimitiveDefault.Value, out var result1) && result1 == true)
                {
                    return true;
                }
                if (int.TryParse(PrimitiveDefault.Value, out var result2) && result2 != 0)
                {
                    return true;
                }
                return PrimitiveDefault.Default;
            }, PrimitiveDefault.Default);
        }

        public static implicit operator byte(PrimitiveDefaultString PrimitiveDefault) => h<byte>(() => byte.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator sbyte(PrimitiveDefaultString PrimitiveDefault) => h<sbyte>(() => sbyte.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator Guid(PrimitiveDefaultString PrimitiveDefault) => h<Guid>(() => Guid.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        //可空
        public static implicit operator short?(PrimitiveDefaultString PrimitiveDefault) => h<short?>(() => short.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator ushort?(PrimitiveDefaultString PrimitiveDefault) => h<ushort?>(() => ushort.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator int?(PrimitiveDefaultString PrimitiveDefault) => h<int?>(() => int.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator uint?(PrimitiveDefaultString PrimitiveDefault) => h<uint?>(() => uint.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator char?(PrimitiveDefaultString PrimitiveDefault)
        {
            return h<char?>(() =>
            {
                //非零即真
                if (PrimitiveDefault == null)
                {
                    return null;
                }
                if (char.TryParse(PrimitiveDefault.Value, out var result))
                {
                    return result;
                }
                return PrimitiveDefault.Default;
            }, PrimitiveDefault.Default);
        }

        public static implicit operator float?(PrimitiveDefaultString PrimitiveDefault) => h<float?>(() => float.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator double?(PrimitiveDefaultString PrimitiveDefault) => h<double?>(() => double.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator long?(PrimitiveDefaultString PrimitiveDefault) => h<long?>(() => long.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator ulong?(PrimitiveDefaultString PrimitiveDefault) => h<ulong?>(() => ulong.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator decimal?(PrimitiveDefaultString PrimitiveDefault) => h<decimal?>(() => decimal.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator bool?(PrimitiveDefaultString PrimitiveDefault)
        {
            return h<bool?>(() =>
            {
                //非零即真
                if (PrimitiveDefault == null)
                {
                    return null;
                }
                if (bool.TryParse(PrimitiveDefault.Value, out var result1) && result1 == true)
                {
                    return true;
                }
                if (int.TryParse(PrimitiveDefault.Value, out var result2) && result2 != 0)
                {
                    return true;
                }
                return PrimitiveDefault.Default;
            }, PrimitiveDefault.Default);
        }

        public static implicit operator byte?(PrimitiveDefaultString PrimitiveDefault) => h<byte?>(() => byte.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator sbyte?(PrimitiveDefaultString PrimitiveDefault) => h<sbyte?>(() => sbyte.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator Guid?(PrimitiveDefaultString PrimitiveDefault) => h<Guid?>(() => Guid.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        //日期
        public static implicit operator DateTime(PrimitiveDefaultString PrimitiveDefault)
        {
            return h<DateTime>(() =>
            {
                if (Convert_JS_timestamp(PrimitiveDefault.Value, out var dt))
                {
                    return dt;
                }

                return DateTime.Parse(PrimitiveDefault.Value);
            }, PrimitiveDefault.Default);
        }

        public static implicit operator DateTime?(PrimitiveDefaultString PrimitiveDefault)
        {
            return h<DateTime?>(() =>
            {
                if (PrimitiveDefault == null)
                {
                    return null;
                }
                if (Convert_JS_timestamp(PrimitiveDefault.Value, out var dt))
                {
                    return dt;
                }
                return DateTime.Parse(PrimitiveDefault.Value);
            }, PrimitiveDefault.Default);
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
        public static implicit operator DateOnly(PrimitiveDefaultString PrimitiveDefault) => h<DateOnly>(() => DateOnly.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);
        public static implicit operator TimeOnly(PrimitiveDefaultString PrimitiveDefault) => h<TimeOnly>(() => TimeOnly.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator DateOnly?(PrimitiveDefaultString PrimitiveDefault) => h<DateOnly?>(() => DateOnly.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);
        public static implicit operator TimeOnly?(PrimitiveDefaultString PrimitiveDefault) => h<TimeOnly?>(() => TimeOnly.Parse(PrimitiveDefault.Value), PrimitiveDefault.Default);
#endif

        //string
        public static implicit operator string(PrimitiveDefaultString PrimitiveDefault) => PrimitiveDefault.Value;

        //操作符/方法的重写
        public static bool operator ==(PrimitiveDefaultString a, PrimitiveDefaultString b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveDefaultString a, PrimitiveDefaultString b) => !a.Value.Equals(b.Value);

        public override string ToString()
        {
            return Value == null ? null : Value.ToString();
        }

        /// <summary>
        /// 注:如果用 object.Equals来比较的, 请确保 参数1为  PrimitiveDefault类型
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

            if (obj is PrimitiveDefaultString other)
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

        public bool Equals(PrimitiveDefaultString other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(other.Value, Value);
        }

        public static PrimitiveDefaultString operator +(PrimitiveDefaultString a, PrimitiveDefaultString b)
            => a.Value == null && b.Value == null ? null : a.Value ?? "" + b.Value ?? "";
    }
}