using System.Reflection;

namespace AutoPrimitive
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public readonly struct PrimitiveDefaultNullable<T>
    {
        public PrimitiveDefaultNullable(T val)
        {
            bool IsNullableType(Type type) => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

            Value = val;
            if (IsNullableType(typeof(T)) == false)
            {
                throw new Exception("只能是可空类型");
            }
        }

        public T Value { get; }

        public static implicit operator PrimitiveDefaultNullable<T>(T val) => new(val);

        //数值类型: short ushort int uint char float double long ulong decimal
        //其他类型: bool byte sbyte
        //其他:string

        //数值类型:
        public static implicit operator short(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default : Convert.ToInt16(PrimitiveDefault.Value);

        public static implicit operator ushort(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default : Convert.ToUInt16(PrimitiveDefault.Value);

        public static implicit operator int(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default : Convert.ToInt32(PrimitiveDefault.Value);

        public static implicit operator uint(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default : Convert.ToUInt32(PrimitiveDefault.Value);

        public static implicit operator char(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default : Convert.ToChar(PrimitiveDefault.Value);

        public static implicit operator float(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default : Convert.ToSingle(PrimitiveDefault.Value);

        public static implicit operator double(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default : Convert.ToDouble(PrimitiveDefault.Value);

        public static implicit operator long(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default : Convert.ToInt64(PrimitiveDefault.Value);

        public static implicit operator ulong(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default : Convert.ToUInt64(PrimitiveDefault.Value);

        public static implicit operator decimal(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default : Convert.ToDecimal(PrimitiveDefault.Value);

        //其他类型
        public static implicit operator bool(PrimitiveDefaultNullable<T> PrimitiveDefault) =>
            PrimitiveDefault.Value != null ||
            bool.TryParse(PrimitiveDefault.Value.ToString(), out var result1) && result1 == true ||
            int.TryParse(PrimitiveDefault.Value.ToString(), out var result2) && result2 != 0; //非零即真

        public static implicit operator byte(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default : Convert.ToByte(PrimitiveDefault.Value);

        public static implicit operator sbyte(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default : Convert.ToSByte(PrimitiveDefault.Value);

        //可空(这边的 default 关键字必须是 default(xxx), 否则编译器是不会返回可空的
        //数值类型:
        public static implicit operator short?(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default(short?) : Convert.ToInt16(PrimitiveDefault.Value);

        public static implicit operator ushort?(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default(ushort?) : Convert.ToUInt16(PrimitiveDefault.Value);

        public static implicit operator int?(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default(int?) : Convert.ToInt32(PrimitiveDefault.Value);

        public static implicit operator uint?(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default(uint?) : Convert.ToUInt32(PrimitiveDefault.Value);

        public static implicit operator char?(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default(char?) : Convert.ToChar(PrimitiveDefault.Value);

        public static implicit operator float?(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default(float?) : Convert.ToSingle(PrimitiveDefault.Value);

        public static implicit operator double?(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default(double?) : Convert.ToDouble(PrimitiveDefault.Value);

        public static implicit operator long?(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default(long?) : Convert.ToInt64(PrimitiveDefault.Value);

        public static implicit operator ulong?(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default(ulong?) : Convert.ToUInt64(PrimitiveDefault.Value);

        public static implicit operator decimal?(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default(decimal?) : Convert.ToDecimal(PrimitiveDefault.Value);

        //其他类型
        public static implicit operator bool?(PrimitiveDefaultNullable<T> PrimitiveDefault)
        {
            if (PrimitiveDefault.Value == null)
            {
                return null;
            }

            return bool.TryParse(PrimitiveDefault.Value.ToString(), out var result1) && result1 == true ||
                   int.TryParse(PrimitiveDefault.Value.ToString(), out var result2) && result2 != 0; //非零即真
        }

        public static implicit operator byte?(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default(byte?) : Convert.ToByte(PrimitiveDefault.Value);

        public static implicit operator sbyte?(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default(sbyte?) : Convert.ToSByte(PrimitiveDefault.Value);

        //string
        public static implicit operator string(PrimitiveDefaultNullable<T> PrimitiveDefault) => PrimitiveDefault.Value == null ? default : PrimitiveDefault.Value.ToString();

        //日期
        public static implicit operator DateTime(PrimitiveDefaultNullable<T> PrimitiveDefault)
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
        }

        public static implicit operator DateTime?(PrimitiveDefaultNullable<T> PrimitiveDefault)
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

        ////自定义的 ChangeType
        //private static object ChangeType(object val, Type type)
        //{
        //    bool IsNullableType(Type type) => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

        //    Type GetNullableTType(Type type) => type.GetProperty("Value").PropertyType;

        //    return Convert.ChangeType(val, IsNullableType(type) ? GetNullableTType(type) : type);
        //}

        #region 运算符重载

        //https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/operators/operator-overloading

        #region Add

        //short ushort int uint char float double long ulong decimal string

        public static PrimitiveDefaultNullable<T> operator +(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<short> b) => Add(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator +(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<ushort> b) => Add(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator +(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<int> b) => Add(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator +(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<uint> b) => Add(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator +(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<char> b) => Add(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator +(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<float> b) => Add(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator +(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<double> b) => Add(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator +(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<long> b) => Add(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator +(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<ulong> b) => Add(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator +(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<decimal> b) => Add(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator +(PrimitiveDefaultNullable<T> a, PrimitiveDefaultString b) => Add(a.Value, b.Value);

        private static PrimitiveDefaultNullable<T> Add(dynamic aValue, dynamic bValue)
        {
            if (aValue == null && bValue == null)
            {
                return new PrimitiveDefaultNullable<T>(default);
            }
            else if (aValue == null)
            {
                return new PrimitiveDefaultNullable<T>(bValue);
            }
            else if (bValue == null)
            {
                return new PrimitiveDefaultNullable<T>(aValue);
            }
            else
            {
                dynamic v1 = Convert.ChangeType(aValue, typeof(T));
                dynamic v2 = Convert.ChangeType(bValue, typeof(T));
                return new PrimitiveDefaultNullable<T>(v1 + v2);
            }
        }

        #endregion

        #region Subtract

        //short ushort int uint char float double long ulong decimal string

        public static PrimitiveDefaultNullable<T> operator -(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<short> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator -(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<ushort> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator -(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<int> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator -(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<uint> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator -(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<char> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator -(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<float> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator -(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<double> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator -(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<long> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator -(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<ulong> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator -(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<decimal> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator -(PrimitiveDefaultNullable<T> a, PrimitiveDefaultString b) => Subtract(a.Value, b.Value);

        private static PrimitiveDefaultNullable<T> Subtract(dynamic aValue, dynamic bValue)
        {
            if (aValue == null || bValue == null)
            {
                return new PrimitiveDefaultNullable<T>(default);
            }
            else
            {
                dynamic v1 = Convert.ChangeType(aValue, typeof(T));
                dynamic v2 = Convert.ChangeType(bValue, typeof(T));
                return new PrimitiveDefaultNullable<T>(v1 - v2);
            }
        }

        #endregion

        #region Multiply

        //short ushort int uint char float double long ulong decimal string

        public static PrimitiveDefaultNullable<T> operator *(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<short> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator *(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<ushort> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator *(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<int> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator *(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<uint> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator *(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<char> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator *(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<float> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator *(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<double> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator *(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<long> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator *(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<ulong> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator *(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<decimal> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator *(PrimitiveDefaultNullable<T> a, PrimitiveDefaultString b) => Multiply(a.Value, b.Value);

        private static PrimitiveDefaultNullable<T> Multiply(dynamic aValue, dynamic bValue)
        {
            if (aValue == null || bValue == null)
            {
                return new PrimitiveDefaultNullable<T>(default);
            }
            else
            {
                dynamic v1 = Convert.ChangeType(aValue, typeof(T));
                dynamic v2 = Convert.ChangeType(bValue, typeof(T));
                return new PrimitiveDefaultNullable<T>(v1 * v2);
            }
        }

        #endregion

        #region Divide

        //short ushort int uint char float double long ulong decimal string

        public static PrimitiveDefaultNullable<T> operator /(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<short> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator /(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<ushort> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator /(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<int> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator /(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<uint> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator /(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<char> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator /(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<float> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator /(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<double> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator /(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<long> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator /(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<ulong> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator /(PrimitiveDefaultNullable<T> a, PrimitiveDefaultNullable<decimal> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefaultNullable<T> operator /(PrimitiveDefaultNullable<T> a, PrimitiveDefaultString b) => Divide(a.Value, b.Value);

        private static PrimitiveDefaultNullable<T> Divide(dynamic aValue, dynamic bValue)
        {
            if (aValue == null || bValue == null)
            {
                return new PrimitiveDefaultNullable<T>(default);
            }
            else
            {
                dynamic v1 = Convert.ChangeType(aValue, typeof(T));
                dynamic v2 = Convert.ChangeType(bValue, typeof(T));
                return new PrimitiveDefaultNullable<T>(v1 / v2);
            }
        }

        #endregion

        #endregion
    }
}