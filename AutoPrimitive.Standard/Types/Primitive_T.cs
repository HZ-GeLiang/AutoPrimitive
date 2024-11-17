namespace AutoPrimitive
{
    public readonly struct Primitive<T> where T : struct
    {
        public Primitive(T val)
        {
            Value = val;
        }

        public T Value { get; }

        public static implicit operator Primitive<T>(T val) => new(val);

        //数值类型: short ushort int uint char float double long ulong decimal
        //其他类型: bool byte sbyte
        //其他:string

        //数值类型:
        public static implicit operator short(Primitive<T> primitive) => Convert.ToInt16(primitive.Value);

        public static implicit operator ushort(Primitive<T> primitive) => Convert.ToUInt16(primitive.Value);

        public static implicit operator int(Primitive<T> primitive) => Convert.ToInt32(primitive.Value);

        public static implicit operator uint(Primitive<T> primitive) => Convert.ToUInt32(primitive.Value);

        public static implicit operator char(Primitive<T> primitive) => Convert.ToChar(primitive.Value);

        public static implicit operator float(Primitive<T> primitive) => Convert.ToSingle(primitive.Value);

        public static implicit operator double(Primitive<T> primitive) => Convert.ToDouble(primitive.Value);

        public static implicit operator long(Primitive<T> primitive) => Convert.ToInt64(primitive.Value);

        public static implicit operator ulong(Primitive<T> primitive) => Convert.ToUInt64(primitive.Value);

        public static implicit operator decimal(Primitive<T> primitive) => Convert.ToDecimal(primitive.Value);

        //其他类型
        public static implicit operator bool(Primitive<T> primitive)
        {//非零即真
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

        public static implicit operator byte(Primitive<T> primitive) => Convert.ToByte(primitive.Value);

        public static implicit operator sbyte(Primitive<T> primitive) => Convert.ToSByte(primitive.Value);

        //string
        public static implicit operator string(Primitive<T> primitive) => primitive.Value.ToString();

        //日期
        public static implicit operator DateTime(Primitive<T> primitive)
        {
            //尝试进行js时间戳的转换
            if (Convert_JS_timestamp(primitive, out var dt))
            {
                return dt;
            }
            return Convert.ToDateTime(primitive.Value);
        }

        private static bool Convert_JS_timestamp(Primitive<T> primitive, out DateTime dateTime)
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

        public override string ToString()
        {
            return Value.ToString();
        }

        /// <summary>
        /// 注:如果用 object.Equals来比较的, 请确保 参数1为  Primitive类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false; //因为T是Struct
            }

            if (obj is Primitive<T>)
            {
                return Equals(((Primitive<T>)obj).Value, Value);
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

        #region 运算符重载

        //https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/operators/operator-overloading

        #region Add

        //public static Primitive<T> operator +(Primitive<T> a, Primitive<T> b) => new Primitive<T>((dynamic)a.Value + b.Value);//优先级太低.测试发现,不会进来

        //short ushort int uint char float double long ulong decimal string
        public static Primitive<T> operator +(Primitive<T> a, Primitive<short> b) => Add(a.Value, b.Value);

        public static Primitive<T> operator +(Primitive<T> a, Primitive<ushort> b) => Add(a.Value, b.Value);

        public static Primitive<T> operator +(Primitive<T> a, Primitive<int> b) => Add(a.Value, b.Value);

        public static Primitive<T> operator +(Primitive<T> a, Primitive<uint> b) => Add(a.Value, b.Value);

        public static Primitive<T> operator +(Primitive<T> a, Primitive<char> b) => Add(a.Value, b.Value);

        public static Primitive<T> operator +(Primitive<T> a, Primitive<float> b) => Add(a.Value, b.Value);

        public static Primitive<T> operator +(Primitive<T> a, Primitive<double> b) => Add(a.Value, b.Value);

        public static Primitive<T> operator +(Primitive<T> a, Primitive<long> b) => Add(a.Value, b.Value);

        public static Primitive<T> operator +(Primitive<T> a, Primitive<ulong> b) => Add(a.Value, b.Value);

        public static Primitive<T> operator +(Primitive<T> a, Primitive<decimal> b) => Add(a.Value, b.Value);

        public static Primitive<T> operator +(Primitive<T> a, PrimitiveString b) => Add(a.Value, b.Value);

        private static Primitive<T> Add(dynamic aValue, dynamic bValue)
        {
            dynamic v1 = Convert.ChangeType(aValue, typeof(T));
            dynamic v2 = Convert.ChangeType(bValue, typeof(T));
            return new Primitive<T>(v1 + v2);
        }

        #endregion

        #region Subtract

        //short ushort int uint char float double long ulong decimal string

        public static Primitive<T> operator -(Primitive<T> a, Primitive<short> b) => Subtract(a.Value, b.Value);

        public static Primitive<T> operator -(Primitive<T> a, Primitive<ushort> b) => Subtract(a.Value, b.Value);

        public static Primitive<T> operator -(Primitive<T> a, Primitive<int> b) => Subtract(a.Value, b.Value);

        public static Primitive<T> operator -(Primitive<T> a, Primitive<uint> b) => Subtract(a.Value, b.Value);

        public static Primitive<T> operator -(Primitive<T> a, Primitive<char> b) => Subtract(a.Value, b.Value);

        public static Primitive<T> operator -(Primitive<T> a, Primitive<float> b) => Subtract(a.Value, b.Value);

        public static Primitive<T> operator -(Primitive<T> a, Primitive<double> b) => Subtract(a.Value, b.Value);

        public static Primitive<T> operator -(Primitive<T> a, Primitive<long> b) => Subtract(a.Value, b.Value);

        public static Primitive<T> operator -(Primitive<T> a, Primitive<ulong> b) => Subtract(a.Value, b.Value);

        public static Primitive<T> operator -(Primitive<T> a, Primitive<decimal> b) => Subtract(a.Value, b.Value);

        public static Primitive<T> operator -(Primitive<T> a, PrimitiveString b) => Subtract(a.Value, b.Value);

        private static Primitive<T> Subtract(dynamic aValue, dynamic bValue)
        {
            dynamic v1 = Convert.ChangeType(aValue, typeof(T));
            dynamic v2 = Convert.ChangeType(bValue, typeof(T));
            return new Primitive<T>(v1 - v2);
        }

        #endregion

        #region Multiply

        //short ushort int uint char float double long ulong decimal string

        public static Primitive<T> operator *(Primitive<T> a, Primitive<short> b) => Multiply(a.Value, b.Value);

        public static Primitive<T> operator *(Primitive<T> a, Primitive<ushort> b) => Multiply(a.Value, b.Value);

        public static Primitive<T> operator *(Primitive<T> a, Primitive<int> b) => Multiply(a.Value, b.Value);

        public static Primitive<T> operator *(Primitive<T> a, Primitive<uint> b) => Multiply(a.Value, b.Value);

        public static Primitive<T> operator *(Primitive<T> a, Primitive<char> b) => Multiply(a.Value, b.Value);

        public static Primitive<T> operator *(Primitive<T> a, Primitive<float> b) => Multiply(a.Value, b.Value);

        public static Primitive<T> operator *(Primitive<T> a, Primitive<double> b) => Multiply(a.Value, b.Value);

        public static Primitive<T> operator *(Primitive<T> a, Primitive<long> b) => Multiply(a.Value, b.Value);

        public static Primitive<T> operator *(Primitive<T> a, Primitive<ulong> b) => Multiply(a.Value, b.Value);

        public static Primitive<T> operator *(Primitive<T> a, Primitive<decimal> b) => Multiply(a.Value, b.Value);

        public static Primitive<T> operator *(Primitive<T> a, PrimitiveString b) => Multiply(a.Value, b.Value);

        private static Primitive<T> Multiply(dynamic aValue, dynamic bValue)
        {
            dynamic v1 = Convert.ChangeType(aValue, typeof(T));
            dynamic v2 = Convert.ChangeType(bValue, typeof(T));
            return new Primitive<T>(v1 * v2);
        }

        #endregion

        #region Divide

        //short ushort int uint char float double long ulong decimal string

        public static Primitive<T> operator /(Primitive<T> a, Primitive<short> b) => Divide(a.Value, b.Value);

        public static Primitive<T> operator /(Primitive<T> a, Primitive<ushort> b) => Divide(a.Value, b.Value);

        public static Primitive<T> operator /(Primitive<T> a, Primitive<int> b) => Divide(a.Value, b.Value);

        public static Primitive<T> operator /(Primitive<T> a, Primitive<uint> b) => Divide(a.Value, b.Value);

        public static Primitive<T> operator /(Primitive<T> a, Primitive<char> b) => Divide(a.Value, b.Value);

        public static Primitive<T> operator /(Primitive<T> a, Primitive<float> b) => Divide(a.Value, b.Value);

        public static Primitive<T> operator /(Primitive<T> a, Primitive<double> b) => Divide(a.Value, b.Value);

        public static Primitive<T> operator /(Primitive<T> a, Primitive<long> b) => Divide(a.Value, b.Value);

        public static Primitive<T> operator /(Primitive<T> a, Primitive<ulong> b) => Divide(a.Value, b.Value);

        public static Primitive<T> operator /(Primitive<T> a, Primitive<decimal> b) => Divide(a.Value, b.Value);

        public static Primitive<T> operator /(Primitive<T> a, PrimitiveString b) => Divide(a.Value, b.Value);

        private static Primitive<T> Divide(dynamic aValue, dynamic bValue)
        {
            dynamic v1 = Convert.ChangeType(aValue, typeof(T));
            dynamic v2 = Convert.ChangeType(bValue, typeof(T));
            return new Primitive<T>(v1 / v2);
        }

        #endregion

        #endregion
    }
}