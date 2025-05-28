namespace AutoPrimitive
{
    public readonly struct PrimitiveDefault<T> where T : struct
    {
        public PrimitiveDefault(T val, object @default)
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
            catch (Exception)
            {
                if (typeof(T) == typeof(T_Target))
                {
                    return @default;
                }
                else
                {
                    return Convert.ChangeType(@default, typeof(T_Target));
                }
            }
        }

        public T Value { get; }
        public object Default { get; }

        public static implicit operator PrimitiveDefault<T>(T val) => new(val, default(T));

        //数值类型: short ushort int uint char float double long ulong decimal
        //其他类型: bool byte sbyte
        //其他:string

        //数值类型:
        public static implicit operator short(PrimitiveDefault<T> PrimitiveDefault) => h<short>(() => Convert.ToInt16(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator ushort(PrimitiveDefault<T> PrimitiveDefault) => h<ushort>(() => Convert.ToUInt16(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator int(PrimitiveDefault<T> PrimitiveDefault) => h<int>(() => Convert.ToInt32(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator uint(PrimitiveDefault<T> PrimitiveDefault) => h<uint>(() => Convert.ToUInt32(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator char(PrimitiveDefault<T> PrimitiveDefault) => h<char>(() => Convert.ToChar(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator float(PrimitiveDefault<T> PrimitiveDefault) => h<float>(() => Convert.ToSingle(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator double(PrimitiveDefault<T> PrimitiveDefault) => h<double>(() => Convert.ToDouble(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator long(PrimitiveDefault<T> PrimitiveDefault) => h<long>(() => Convert.ToInt64(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator ulong(PrimitiveDefault<T> PrimitiveDefault) => h<ulong>(() => Convert.ToUInt64(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator decimal(PrimitiveDefault<T> PrimitiveDefault) => h<decimal>(() => Convert.ToDecimal(PrimitiveDefault.Value), PrimitiveDefault.Default);

        //其他类型
        public static implicit operator bool(PrimitiveDefault<T> PrimitiveDefault)
        {
            return h<bool>(() =>
            {
                //非零即真
                if (bool.TryParse(PrimitiveDefault.Value.ToString(), out var result1) && result1 == true)
                {
                    return true;
                }
                if (int.TryParse(PrimitiveDefault.Value.ToString(), out var result2) && result2 != 0)
                {
                    return true;
                }
                return Convert.ToBoolean(PrimitiveDefault.Default);
            }, PrimitiveDefault.Default);
        }

        public static implicit operator byte(PrimitiveDefault<T> PrimitiveDefault) => h<byte>(() => Convert.ToByte(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator sbyte(PrimitiveDefault<T> PrimitiveDefault) => h<sbyte>(() => Convert.ToSByte(PrimitiveDefault.Value), PrimitiveDefault.Default);

        //string
        public static implicit operator string(PrimitiveDefault<T> PrimitiveDefault) => h<string>(() => PrimitiveDefault.Value.ToString(), PrimitiveDefault.Default);

        //日期
        public static implicit operator DateTime(PrimitiveDefault<T> PrimitiveDefault)
        {
            return h<DateTime>(() =>
            {
                //尝试进行js时间戳的转换
                if (Convert_JS_timestamp(PrimitiveDefault, out var dt))
                {
                    return dt;
                }
                return Convert.ToDateTime(PrimitiveDefault.Value);
            }, PrimitiveDefault.Default);
        }

        private static bool Convert_JS_timestamp(PrimitiveDefault<T> PrimitiveDefault, out DateTime dateTime)
        {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER
            try
            {
                var t_type = typeof(T);
                if (t_type == typeof(long))
                {
                    var len = PrimitiveDefault.Value.ToString().Length;

                    if (len == 13 || len == 14)
                    {
                        long timestamp = (dynamic)PrimitiveDefault.Value;
                        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                        dateTime = dateTimeOffset.LocalDateTime;
                        return true;
                    }
                    else if (len == 10 || len == 11)
                    {
                        long timestamp = (dynamic)PrimitiveDefault.Value;
                        var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                        dateTime = dateTimeOffset.LocalDateTime;
                        return true;
                    }
                }
                else if (t_type == typeof(int))
                {
                    var len = PrimitiveDefault.Value.ToString().Length;
                    if (len == 10 || len == 11)
                    {
                        long timestamp = (dynamic)PrimitiveDefault.Value;
                        var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                        dateTime = dateTimeOffset.LocalDateTime;
                        return true;
                    }
                }
                else if (t_type == typeof(string))
                {
                    string timestamp = (dynamic)PrimitiveDefault.Value;

                    if (PrimitiveDefaultString.Convert_JS_timestamp(timestamp, out dateTime))
                    {
                        return true;
                    }
                }
            }
            catch (Exception)
            {
            }

#endif
            dateTime = default;
            return false;
        }

        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// 注:如果用 object.Equals来比较的, 请确保 参数1为  PrimitiveDefault类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false; //因为T是Struct
            }

            if (obj is PrimitiveDefault<T>)
            {
                return Equals(((PrimitiveDefault<T>)obj).Value, Value);
            }

            //else

            if (obj.GetType() == Value.GetType())
            {
                return Equals(obj, Value);
            }

            //return object.Equals(obj, ChangeType(this.Value, obj.GetType())); //ok(使用自定义的 ChangeType)

            return Equals(obj, Convert.ChangeType(Value, obj.GetType())); //使用内置的 ChangeType
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}