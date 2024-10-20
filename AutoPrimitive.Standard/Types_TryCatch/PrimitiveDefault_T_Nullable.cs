using System.Reflection;

namespace AutoPrimitive
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public readonly struct PrimitiveDefaultNullable<T>
    {
        public PrimitiveDefaultNullable(T val, object @default)
        {
            bool IsNullableType(Type type) => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

            Value = val;
            if (IsNullableType(typeof(T)) == false)
            {
                throw new Exception("只能是可空类型");
            }

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

        public static implicit operator PrimitiveDefaultNullable<T>(T val) => new(val, default(T));

        //数值类型: short ushort int uint char float double long ulong decimal
        //其他类型: bool byte sbyte
        //其他:string

        //数值类型:
        public static implicit operator short(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<short>(() => PrimitiveDefault.Value == null ? default : Convert.ToInt16(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator ushort(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<ushort>(() => PrimitiveDefault.Value == null ? default : Convert.ToUInt16(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator int(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<int>(() => PrimitiveDefault.Value == null ? default : Convert.ToInt32(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator uint(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<uint>(() => PrimitiveDefault.Value == null ? default : Convert.ToUInt32(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator char(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<char>(() => PrimitiveDefault.Value == null ? default : Convert.ToChar(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator float(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<float>(() => PrimitiveDefault.Value == null ? default : Convert.ToSingle(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator double(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<double>(() => PrimitiveDefault.Value == null ? default : Convert.ToDouble(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator long(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<long>(() => PrimitiveDefault.Value == null ? default : Convert.ToInt64(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator ulong(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<ulong>(() => PrimitiveDefault.Value == null ? default : Convert.ToUInt64(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator decimal(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<decimal>(() => PrimitiveDefault.Value == null ? default : Convert.ToDecimal(PrimitiveDefault.Value), PrimitiveDefault.Default);

        //其他类型
        public static implicit operator bool(PrimitiveDefaultNullable<T> PrimitiveDefault)
        {
            return h<bool>(() =>
            {
                //非零即真
                if (PrimitiveDefault.Value == null)
                {
                    return false;
                }
                if (bool.TryParse(PrimitiveDefault.Value.ToString(), out var result1) && result1 == true)
                {
                    return true;
                }

                if (int.TryParse(PrimitiveDefault.Value.ToString(), out var result2) && result2 != 0)
                {
                    return true;
                }
                return false;
            }, PrimitiveDefault.Default);
        }

        public static implicit operator byte(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<byte>(() => PrimitiveDefault.Value == null ? default : Convert.ToByte(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator sbyte(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<sbyte>(() => PrimitiveDefault.Value == null ? default : Convert.ToSByte(PrimitiveDefault.Value), PrimitiveDefault.Default);

        //可空(这边的 default 关键字必须是 default(xxx), 否则编译器是不会返回可空的
        //数值类型:
        public static implicit operator short?(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<short?>(() => PrimitiveDefault.Value == null ? default(short?) : Convert.ToInt16(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator ushort?(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<ushort?>(() => PrimitiveDefault.Value == null ? default(ushort?) : Convert.ToUInt16(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator int?(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<int?>(() => PrimitiveDefault.Value == null ? default(int?) : Convert.ToInt32(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator uint?(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<uint?>(() => PrimitiveDefault.Value == null ? default(uint?) : Convert.ToUInt32(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator char?(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<char?>(() => PrimitiveDefault.Value == null ? default(char?) : Convert.ToChar(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator float?(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<float?>(() => PrimitiveDefault.Value == null ? default(float?) : Convert.ToSingle(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator double?(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<double?>(() => PrimitiveDefault.Value == null ? default(double?) : Convert.ToDouble(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator long?(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<long?>(() => PrimitiveDefault.Value == null ? default(long?) : Convert.ToInt64(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator ulong?(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<ulong?>(() => PrimitiveDefault.Value == null ? default(ulong?) : Convert.ToUInt64(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator decimal?(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<decimal?>(() => PrimitiveDefault.Value == null ? default(decimal?) : Convert.ToDecimal(PrimitiveDefault.Value), PrimitiveDefault.Default);

        //其他类型
        public static implicit operator bool?(PrimitiveDefaultNullable<T> PrimitiveDefault)
        {
            return h<DateTime>(() =>
            {
                if (PrimitiveDefault.Value == null)
                {
                    return null;
                }
                //非零即真
                if (bool.TryParse(PrimitiveDefault.Value.ToString(), out var result1) && result1 == true)
                {
                    return true;
                }
                if (int.TryParse(PrimitiveDefault.Value.ToString(), out var result2) && result2 != 0)
                {
                    return true;
                }
                return false;
            }, PrimitiveDefault.Default);
        }

        public static implicit operator byte?(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<byte?>(() => PrimitiveDefault.Value == null ? default(byte?) : Convert.ToByte(PrimitiveDefault.Value), PrimitiveDefault.Default);

        public static implicit operator sbyte?(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<sbyte?>(() => PrimitiveDefault.Value == null ? default(sbyte?) : Convert.ToSByte(PrimitiveDefault.Value), PrimitiveDefault.Default);

        //string
        public static implicit operator string(PrimitiveDefaultNullable<T> PrimitiveDefault) => h<string>(() => PrimitiveDefault.Value == null ? default : PrimitiveDefault.Value.ToString(), PrimitiveDefault.Default);

        //日期
        public static implicit operator DateTime(PrimitiveDefaultNullable<T> PrimitiveDefault)
        {
            return h<DateTime>(() =>
            {
                if (PrimitiveDefault.Value != null)
                {
                    //尝试进行js时间戳的转换
                    if (Convert_JS_timestamp(PrimitiveDefault, out var dt))
                    {
                        return dt;
                    }

                    return Convert.ToDateTime(PrimitiveDefault.Value);
                }

                return default;
            }, PrimitiveDefault.Default);
        }

        public static implicit operator DateTime?(PrimitiveDefaultNullable<T> PrimitiveDefault)
        {
            return h<DateTime>(() =>
            {
                if (PrimitiveDefault.Value != null)
                {
                    //尝试进行js时间戳的转换
                    if (Convert_JS_timestamp(PrimitiveDefault, out var dt))
                    {
                        return dt;
                    }
                    return Convert.ToDateTime(PrimitiveDefault.Value);
                }
                return default(DateTime?);
            }, PrimitiveDefault.Default);
        }

        private static bool Convert_JS_timestamp(PrimitiveDefaultNullable<T> PrimitiveDefault, out DateTime dateTime)
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
            catch (Exception ex)
            {
            }

#endif
            dateTime = default;
            return false;
        }

        //操作符/方法的重写
        public static bool operator ==(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<T> b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<T> b) => !a.Value.Equals(b.Value);

        public override string ToString()
        {
            return Value?.ToString();
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

            if (obj is PrimitiveDefaultNullable<T> other)
            {
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                return Equals(other.Value, Value);
            }

            if (Value == null)
            {
                /*
                 * 注:如果这个if写在开头,那么
                 * Assert.AreEqual(null, val.ToPrimitiveDefault());  将不能写
                 * Assert.AreEqual(null, (decimal?)val.ToPrimitiveDefault()); 必须写成这样
                 * 反之, 如果写在当前位置,  Assert.AreEqual(null, val.ToPrimitiveDefault()) 是可以运行成功的
                 */
                return obj == null;
            }

            //return object.Equals(obj, ChangeType(this.Value, obj.GetType())); //ok(使用自定义的 ChangeType )

            return Equals(obj, Convert.ChangeType(Value, obj.GetType())); //使用内置的 ChangeType
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}