using System.Reflection;

namespace AutoPrimitive
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public readonly struct PrimitiveNullable<T>
    {
        public PrimitiveNullable(T val)
        {
            bool IsNullableType(Type type) => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

            Value = val;
            if (IsNullableType(typeof(T)) == false)
            {
                throw new Exception("只能是可空类型");
            }
        }

        public T Value { get; }

        public static implicit operator PrimitiveNullable<T>(T val) => new(val);

        //数值类型: short ushort int uint char float double long ulong decimal
        //其他类型: bool byte sbyte
        //其他:string

        //数值类型:
        public static implicit operator short(PrimitiveNullable<T> primitive) => primitive.Value == null ? default : Convert.ToInt16(primitive.Value);

        public static implicit operator ushort(PrimitiveNullable<T> primitive) => primitive.Value == null ? default : Convert.ToUInt16(primitive.Value);

        public static implicit operator int(PrimitiveNullable<T> primitive) => primitive.Value == null ? default : Convert.ToInt32(primitive.Value);

        public static implicit operator uint(PrimitiveNullable<T> primitive) => primitive.Value == null ? default : Convert.ToUInt32(primitive.Value);

        public static implicit operator char(PrimitiveNullable<T> primitive) => primitive.Value == null ? default : Convert.ToChar(primitive.Value);

        public static implicit operator float(PrimitiveNullable<T> primitive) => primitive.Value == null ? default : Convert.ToSingle(primitive.Value);

        public static implicit operator double(PrimitiveNullable<T> primitive) => primitive.Value == null ? default : Convert.ToDouble(primitive.Value);

        public static implicit operator long(PrimitiveNullable<T> primitive) => primitive.Value == null ? default : Convert.ToInt64(primitive.Value);

        public static implicit operator ulong(PrimitiveNullable<T> primitive) => primitive.Value == null ? default : Convert.ToUInt64(primitive.Value);

        public static implicit operator decimal(PrimitiveNullable<T> primitive) => primitive.Value == null ? default : Convert.ToDecimal(primitive.Value);

        //其他类型
        public static implicit operator bool(PrimitiveNullable<T> primitive) =>
            primitive.Value != null ||
            bool.TryParse(primitive.Value.ToString(), out var result1) && result1 == true ||
            int.TryParse(primitive.Value.ToString(), out var result2) && result2 != 0; //非零即真

        public static implicit operator byte(PrimitiveNullable<T> primitive) => primitive.Value == null ? default : Convert.ToByte(primitive.Value);

        public static implicit operator sbyte(PrimitiveNullable<T> primitive) => primitive.Value == null ? default : Convert.ToSByte(primitive.Value);

        //可空(这边的 default 关键字必须是 default(xxx), 否则编译器是不会返回可空的
        //数值类型:
        public static implicit operator short?(PrimitiveNullable<T> primitive) => primitive.Value == null ? default(short?) : Convert.ToInt16(primitive.Value);

        public static implicit operator ushort?(PrimitiveNullable<T> primitive) => primitive.Value == null ? default(ushort?) : Convert.ToUInt16(primitive.Value);

        public static implicit operator int?(PrimitiveNullable<T> primitive) => primitive.Value == null ? default(int?) : Convert.ToInt32(primitive.Value);

        public static implicit operator uint?(PrimitiveNullable<T> primitive) => primitive.Value == null ? default(uint?) : Convert.ToUInt32(primitive.Value);

        public static implicit operator char?(PrimitiveNullable<T> primitive) => primitive.Value == null ? default(char?) : Convert.ToChar(primitive.Value);

        public static implicit operator float?(PrimitiveNullable<T> primitive) => primitive.Value == null ? default(float?) : Convert.ToSingle(primitive.Value);

        public static implicit operator double?(PrimitiveNullable<T> primitive) => primitive.Value == null ? default(double?) : Convert.ToDouble(primitive.Value);

        public static implicit operator long?(PrimitiveNullable<T> primitive) => primitive.Value == null ? default(long?) : Convert.ToInt64(primitive.Value);

        public static implicit operator ulong?(PrimitiveNullable<T> primitive) => primitive.Value == null ? default(ulong?) : Convert.ToUInt64(primitive.Value);

        public static implicit operator decimal?(PrimitiveNullable<T> primitive) => primitive.Value == null ? default(decimal?) : Convert.ToDecimal(primitive.Value);

        //其他类型
        public static implicit operator bool?(PrimitiveNullable<T> primitive)
        {
            //非零即真
            if (primitive.Value == null)
            {
                return null;
            }
            if (bool.TryParse(primitive.Value.ToString(), out var result1) && result1 == true)
            {
                return true;
            }
            if (int.TryParse(primitive.Value.ToString(), out var result2) && result2 != 0)
            {
                return true;
            }

            return false;
        }

        public static implicit operator byte?(PrimitiveNullable<T> primitive) => primitive.Value == null ? default(byte?) : Convert.ToByte(primitive.Value);

        public static implicit operator sbyte?(PrimitiveNullable<T> primitive) => primitive.Value == null ? default(sbyte?) : Convert.ToSByte(primitive.Value);

        //string
        public static implicit operator string(PrimitiveNullable<T> primitive) => primitive.Value == null ? default : primitive.Value.ToString();

        //日期
        public static implicit operator DateTime(PrimitiveNullable<T> primitive)
        {
            if (primitive.Value != null)
            {
                //尝试进行js时间戳的转换
                if (Convert_JS_timestamp(primitive, out var dt))
                {
                    return dt;
                }

                return Convert.ToDateTime(primitive.Value);
            }

            return default;
        }

        public static implicit operator DateTime?(PrimitiveNullable<T> primitive)
        {
            if (primitive.Value != null)
            {
                //尝试进行js时间戳的转换
                if (Convert_JS_timestamp(primitive, out var dt))
                {
                    return dt;
                }
                return Convert.ToDateTime(primitive.Value);
            }
            return default(DateTime?);
        }

        private static bool Convert_JS_timestamp(PrimitiveNullable<T> primitive, out DateTime dateTime)
        {
#if NETCOREAPP1_0_OR_GREATER || NETSTANDARD1_3_OR_GREATER
            try
            {
                var t_type = typeof(T);
                if (t_type == typeof(long))
                {
                    var len = primitive.Value.ToString().Length;

                    if (len == 13 || len == 14)
                    {
                        long timestamp = (dynamic)primitive.Value;
                        var dateTimeOffset = DateTimeOffset.FromUnixTimeMilliseconds(timestamp);
                        dateTime = dateTimeOffset.LocalDateTime;
                        return true;
                    }
                    else if (len == 10 || len == 11)
                    {
                        long timestamp = (dynamic)primitive.Value;
                        var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                        dateTime = dateTimeOffset.LocalDateTime;
                        return true;
                    }
                }
                else if (t_type == typeof(int))
                {
                    var len = primitive.Value.ToString().Length;
                    if (len == 10 || len == 11)
                    {
                        long timestamp = (dynamic)primitive.Value;
                        var dateTimeOffset = DateTimeOffset.FromUnixTimeSeconds(timestamp);
                        dateTime = dateTimeOffset.LocalDateTime;
                        return true;
                    }
                }
                else if (t_type == typeof(string))
                {
                    string timestamp = (dynamic)primitive.Value;

                    if (PrimitiveString.Convert_JS_timestamp(timestamp, out dateTime))
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
        public static bool operator ==(PrimitiveNullable<T> a, PrimitiveNullable<T> b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveNullable<T> a, PrimitiveNullable<T> b) => !a.Value.Equals(b.Value);

        public override string ToString()
        {
            return Value?.ToString();
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

            if (obj is PrimitiveNullable<T> other)
            {
                if (this == other)
                {
                    return true;
                }

                return Equals(other.Value, Value);
            }

            if (Value == null)
            {
                /*
                 * 注:如果这个if写在开头,那么
                 * Assert.AreEqual(null, val.ToPrimitive());  将不能写
                 * Assert.AreEqual(null, (decimal?)val.ToPrimitive()); 必须写成这样
                 * 反之, 如果写在当前位置,  Assert.AreEqual(null, val.ToPrimitive()) 是可以运行成功的
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

        public static PrimitiveNullable<T> operator +(PrimitiveNullable<T> a, PrimitiveNullable<short> b) => Add(a.Value, b.Value);

        public static PrimitiveNullable<T> operator +(PrimitiveNullable<T> a, PrimitiveNullable<ushort> b) => Add(a.Value, b.Value);

        public static PrimitiveNullable<T> operator +(PrimitiveNullable<T> a, PrimitiveNullable<int> b) => Add(a.Value, b.Value);

        public static PrimitiveNullable<T> operator +(PrimitiveNullable<T> a, PrimitiveNullable<uint> b) => Add(a.Value, b.Value);

        public static PrimitiveNullable<T> operator +(PrimitiveNullable<T> a, PrimitiveNullable<char> b) => Add(a.Value, b.Value);

        public static PrimitiveNullable<T> operator +(PrimitiveNullable<T> a, PrimitiveNullable<float> b) => Add(a.Value, b.Value);

        public static PrimitiveNullable<T> operator +(PrimitiveNullable<T> a, PrimitiveNullable<double> b) => Add(a.Value, b.Value);

        public static PrimitiveNullable<T> operator +(PrimitiveNullable<T> a, PrimitiveNullable<long> b) => Add(a.Value, b.Value);

        public static PrimitiveNullable<T> operator +(PrimitiveNullable<T> a, PrimitiveNullable<ulong> b) => Add(a.Value, b.Value);

        public static PrimitiveNullable<T> operator +(PrimitiveNullable<T> a, PrimitiveNullable<decimal> b) => Add(a.Value, b.Value);

        public static PrimitiveNullable<T> operator +(PrimitiveNullable<T> a, PrimitiveString b) => Add(a.Value, b.Value);

        private static PrimitiveNullable<T> Add(dynamic aValue, dynamic bValue)
        {
            if (aValue == null && bValue == null)
            {
                return new PrimitiveNullable<T>(default);
            }
            else if (aValue == null)
            {
                return new PrimitiveNullable<T>(bValue);
            }
            else if (bValue == null)
            {
                return new PrimitiveNullable<T>(aValue);
            }
            else
            {
                dynamic v1 = Convert.ChangeType(aValue, typeof(T));
                dynamic v2 = Convert.ChangeType(bValue, typeof(T));
                return new PrimitiveNullable<T>(v1 + v2);
            }
        }

        #endregion

        #region Subtract

        //short ushort int uint char float double long ulong decimal string

        public static PrimitiveNullable<T> operator -(PrimitiveNullable<T> a, PrimitiveNullable<short> b) => Subtract(a.Value, b.Value);

        public static PrimitiveNullable<T> operator -(PrimitiveNullable<T> a, PrimitiveNullable<ushort> b) => Subtract(a.Value, b.Value);

        public static PrimitiveNullable<T> operator -(PrimitiveNullable<T> a, PrimitiveNullable<int> b) => Subtract(a.Value, b.Value);

        public static PrimitiveNullable<T> operator -(PrimitiveNullable<T> a, PrimitiveNullable<uint> b) => Subtract(a.Value, b.Value);

        public static PrimitiveNullable<T> operator -(PrimitiveNullable<T> a, PrimitiveNullable<char> b) => Subtract(a.Value, b.Value);

        public static PrimitiveNullable<T> operator -(PrimitiveNullable<T> a, PrimitiveNullable<float> b) => Subtract(a.Value, b.Value);

        public static PrimitiveNullable<T> operator -(PrimitiveNullable<T> a, PrimitiveNullable<double> b) => Subtract(a.Value, b.Value);

        public static PrimitiveNullable<T> operator -(PrimitiveNullable<T> a, PrimitiveNullable<long> b) => Subtract(a.Value, b.Value);

        public static PrimitiveNullable<T> operator -(PrimitiveNullable<T> a, PrimitiveNullable<ulong> b) => Subtract(a.Value, b.Value);

        public static PrimitiveNullable<T> operator -(PrimitiveNullable<T> a, PrimitiveNullable<decimal> b) => Subtract(a.Value, b.Value);

        public static PrimitiveNullable<T> operator -(PrimitiveNullable<T> a, PrimitiveString b) => Subtract(a.Value, b.Value);

        private static PrimitiveNullable<T> Subtract(dynamic aValue, dynamic bValue)
        {
            if (aValue == null || bValue == null)
            {
                return new PrimitiveNullable<T>(default);
            }
            else
            {
                dynamic v1 = Convert.ChangeType(aValue, typeof(T));
                dynamic v2 = Convert.ChangeType(bValue, typeof(T));
                return new PrimitiveNullable<T>(v1 - v2);
            }
        }

        #endregion

        #region Multiply

        //short ushort int uint char float double long ulong decimal string

        public static PrimitiveNullable<T> operator *(PrimitiveNullable<T> a, PrimitiveNullable<short> b) => Multiply(a.Value, b.Value);

        public static PrimitiveNullable<T> operator *(PrimitiveNullable<T> a, PrimitiveNullable<ushort> b) => Multiply(a.Value, b.Value);

        public static PrimitiveNullable<T> operator *(PrimitiveNullable<T> a, PrimitiveNullable<int> b) => Multiply(a.Value, b.Value);

        public static PrimitiveNullable<T> operator *(PrimitiveNullable<T> a, PrimitiveNullable<uint> b) => Multiply(a.Value, b.Value);

        public static PrimitiveNullable<T> operator *(PrimitiveNullable<T> a, PrimitiveNullable<char> b) => Multiply(a.Value, b.Value);

        public static PrimitiveNullable<T> operator *(PrimitiveNullable<T> a, PrimitiveNullable<float> b) => Multiply(a.Value, b.Value);

        public static PrimitiveNullable<T> operator *(PrimitiveNullable<T> a, PrimitiveNullable<double> b) => Multiply(a.Value, b.Value);

        public static PrimitiveNullable<T> operator *(PrimitiveNullable<T> a, PrimitiveNullable<long> b) => Multiply(a.Value, b.Value);

        public static PrimitiveNullable<T> operator *(PrimitiveNullable<T> a, PrimitiveNullable<ulong> b) => Multiply(a.Value, b.Value);

        public static PrimitiveNullable<T> operator *(PrimitiveNullable<T> a, PrimitiveNullable<decimal> b) => Multiply(a.Value, b.Value);

        public static PrimitiveNullable<T> operator *(PrimitiveNullable<T> a, PrimitiveString b) => Multiply(a.Value, b.Value);

        private static PrimitiveNullable<T> Multiply(dynamic aValue, dynamic bValue)
        {
            if (aValue == null || bValue == null)
            {
                return new PrimitiveNullable<T>(default);
            }
            else
            {
                dynamic v1 = Convert.ChangeType(aValue, typeof(T));
                dynamic v2 = Convert.ChangeType(bValue, typeof(T));
                return new PrimitiveNullable<T>(v1 * v2);
            }
        }

        #endregion

        #region Divide

        //short ushort int uint char float double long ulong decimal string

        public static PrimitiveNullable<T> operator /(PrimitiveNullable<T> a, PrimitiveNullable<short> b) => Divide(a.Value, b.Value);

        public static PrimitiveNullable<T> operator /(PrimitiveNullable<T> a, PrimitiveNullable<ushort> b) => Divide(a.Value, b.Value);

        public static PrimitiveNullable<T> operator /(PrimitiveNullable<T> a, PrimitiveNullable<int> b) => Divide(a.Value, b.Value);

        public static PrimitiveNullable<T> operator /(PrimitiveNullable<T> a, PrimitiveNullable<uint> b) => Divide(a.Value, b.Value);

        public static PrimitiveNullable<T> operator /(PrimitiveNullable<T> a, PrimitiveNullable<char> b) => Divide(a.Value, b.Value);

        public static PrimitiveNullable<T> operator /(PrimitiveNullable<T> a, PrimitiveNullable<float> b) => Divide(a.Value, b.Value);

        public static PrimitiveNullable<T> operator /(PrimitiveNullable<T> a, PrimitiveNullable<double> b) => Divide(a.Value, b.Value);

        public static PrimitiveNullable<T> operator /(PrimitiveNullable<T> a, PrimitiveNullable<long> b) => Divide(a.Value, b.Value);

        public static PrimitiveNullable<T> operator /(PrimitiveNullable<T> a, PrimitiveNullable<ulong> b) => Divide(a.Value, b.Value);

        public static PrimitiveNullable<T> operator /(PrimitiveNullable<T> a, PrimitiveNullable<decimal> b) => Divide(a.Value, b.Value);

        public static PrimitiveNullable<T> operator /(PrimitiveNullable<T> a, PrimitiveString b) => Divide(a.Value, b.Value);

        private static PrimitiveNullable<T> Divide(dynamic aValue, dynamic bValue)
        {
            if (aValue == null || bValue == null)
            {
                return new PrimitiveNullable<T>(default);
            }
            else
            {
                dynamic v1 = Convert.ChangeType(aValue, typeof(T));
                dynamic v2 = Convert.ChangeType(bValue, typeof(T));
                return new PrimitiveNullable<T>(v1 / v2);
            }
        }

        #endregion

        #endregion
    }
}