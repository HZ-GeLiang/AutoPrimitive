namespace AutoPrimitive
{
    public readonly struct PrimitiveDefault<T> where T : struct
    {
        public PrimitiveDefault(T val, T @default)
        {
            Value = val;
            Default = @default;
        }

        public static dynamic h<T_Target>(Func<dynamic> func, T defaultValue)
        {
            try
            {
                return func();
            }
            catch (Exception ex)
            {
                if (typeof(T) == typeof(T_Target))
                {
                    return defaultValue;
                }
                else
                {
                    return Convert.ChangeType(defaultValue, typeof(T_Target));
                }
            }
        }

        public T Value { get; }
        public T Default { get; }

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
        public static implicit operator bool(PrimitiveDefault<T> PrimitiveDefault) =>
            bool.TryParse(PrimitiveDefault.Value.ToString(), out var result1) && result1 == true ||
            int.TryParse(PrimitiveDefault.Value.ToString(), out var result2) && result2 != 0; //非零即真

        public static implicit operator byte(PrimitiveDefault<T> PrimitiveDefault) => Convert.ToByte(PrimitiveDefault.Value);

        public static implicit operator sbyte(PrimitiveDefault<T> PrimitiveDefault) => Convert.ToSByte(PrimitiveDefault.Value);

        //string
        public static implicit operator string(PrimitiveDefault<T> PrimitiveDefault) => PrimitiveDefault.Value.ToString();

        //日期
        public static implicit operator DateTime(PrimitiveDefault<T> PrimitiveDefault)
        {
            //尝试进行js时间戳的转换
            if (Convert_JS_timestamp(PrimitiveDefault, out var dt))
            {
                return dt;
            }
            return Convert.ToDateTime(PrimitiveDefault.Value);
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
        /// 注:如果用 object.Equals来比较的, 请确保 参数1为  PrimitiveDefault类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
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

        #region 运算符重载

        //https://learn.microsoft.com/zh-cn/dotnet/csharp/language-reference/operators/operator-overloading

        #region Add

        //public static PrimitiveDefault<T> operator +(PrimitiveDefault<T> a, PrimitiveDefault<T> b) => new PrimitiveDefault<T>((dynamic)a.Value + b.Value);//优先级太低.测试发现,不会进来

        //short ushort int uint char float double long ulong decimal string
        public static PrimitiveDefault<T> operator +(PrimitiveDefault<T> a, PrimitiveDefault<short> b) => Add(a.Value, b.Value);

        public static PrimitiveDefault<T> operator +(PrimitiveDefault<T> a, PrimitiveDefault<ushort> b) => Add(a.Value, b.Value);

        public static PrimitiveDefault<T> operator +(PrimitiveDefault<T> a, PrimitiveDefault<int> b) => Add(a.Value, b.Value);

        public static PrimitiveDefault<T> operator +(PrimitiveDefault<T> a, PrimitiveDefault<uint> b) => Add(a.Value, b.Value);

        public static PrimitiveDefault<T> operator +(PrimitiveDefault<T> a, PrimitiveDefault<char> b) => Add(a.Value, b.Value);

        public static PrimitiveDefault<T> operator +(PrimitiveDefault<T> a, PrimitiveDefault<float> b) => Add(a.Value, b.Value);

        public static PrimitiveDefault<T> operator +(PrimitiveDefault<T> a, PrimitiveDefault<double> b) => Add(a.Value, b.Value);

        public static PrimitiveDefault<T> operator +(PrimitiveDefault<T> a, PrimitiveDefault<long> b) => Add(a.Value, b.Value);

        public static PrimitiveDefault<T> operator +(PrimitiveDefault<T> a, PrimitiveDefault<ulong> b) => Add(a.Value, b.Value);

        public static PrimitiveDefault<T> operator +(PrimitiveDefault<T> a, PrimitiveDefault<decimal> b) => Add(a.Value, b.Value);

        public static PrimitiveDefault<T> operator +(PrimitiveDefault<T> a, PrimitiveDefaultString b) => Add(a.Value, b.Value);

        private static PrimitiveDefault<T> Add(dynamic aValue, dynamic bValue)
        {
            dynamic v1 = Convert.ChangeType(aValue, typeof(T));
            dynamic v2 = Convert.ChangeType(bValue, typeof(T));
            return new PrimitiveDefault<T>(v1 + v2, default(T));
        }

        #endregion

        #region Subtract

        //short ushort int uint char float double long ulong decimal string

        public static PrimitiveDefault<T> operator -(PrimitiveDefault<T> a, PrimitiveDefault<short> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefault<T> operator -(PrimitiveDefault<T> a, PrimitiveDefault<ushort> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefault<T> operator -(PrimitiveDefault<T> a, PrimitiveDefault<int> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefault<T> operator -(PrimitiveDefault<T> a, PrimitiveDefault<uint> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefault<T> operator -(PrimitiveDefault<T> a, PrimitiveDefault<char> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefault<T> operator -(PrimitiveDefault<T> a, PrimitiveDefault<float> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefault<T> operator -(PrimitiveDefault<T> a, PrimitiveDefault<double> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefault<T> operator -(PrimitiveDefault<T> a, PrimitiveDefault<long> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefault<T> operator -(PrimitiveDefault<T> a, PrimitiveDefault<ulong> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefault<T> operator -(PrimitiveDefault<T> a, PrimitiveDefault<decimal> b) => Subtract(a.Value, b.Value);

        public static PrimitiveDefault<T> operator -(PrimitiveDefault<T> a, PrimitiveDefaultString b) => Subtract(a.Value, b.Value);

        private static PrimitiveDefault<T> Subtract(dynamic aValue, dynamic bValue)
        {
            dynamic v1 = Convert.ChangeType(aValue, typeof(T));
            dynamic v2 = Convert.ChangeType(bValue, typeof(T));
            return new PrimitiveDefault<T>(v1 - v2, default(T));
        }

        #endregion

        #region Multiply

        //short ushort int uint char float double long ulong decimal string

        public static PrimitiveDefault<T> operator *(PrimitiveDefault<T> a, PrimitiveDefault<short> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefault<T> operator *(PrimitiveDefault<T> a, PrimitiveDefault<ushort> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefault<T> operator *(PrimitiveDefault<T> a, PrimitiveDefault<int> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefault<T> operator *(PrimitiveDefault<T> a, PrimitiveDefault<uint> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefault<T> operator *(PrimitiveDefault<T> a, PrimitiveDefault<char> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefault<T> operator *(PrimitiveDefault<T> a, PrimitiveDefault<float> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefault<T> operator *(PrimitiveDefault<T> a, PrimitiveDefault<double> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefault<T> operator *(PrimitiveDefault<T> a, PrimitiveDefault<long> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefault<T> operator *(PrimitiveDefault<T> a, PrimitiveDefault<ulong> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefault<T> operator *(PrimitiveDefault<T> a, PrimitiveDefault<decimal> b) => Multiply(a.Value, b.Value);

        public static PrimitiveDefault<T> operator *(PrimitiveDefault<T> a, PrimitiveDefaultString b) => Multiply(a.Value, b.Value);

        private static PrimitiveDefault<T> Multiply(dynamic aValue, dynamic bValue)
        {
            dynamic v1 = Convert.ChangeType(aValue, typeof(T));
            dynamic v2 = Convert.ChangeType(bValue, typeof(T));
            return new PrimitiveDefault<T>(v1 * v2, default(T));
        }

        #endregion

        #region Divide

        //short ushort int uint char float double long ulong decimal string

        public static PrimitiveDefault<T> operator /(PrimitiveDefault<T> a, PrimitiveDefault<short> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefault<T> operator /(PrimitiveDefault<T> a, PrimitiveDefault<ushort> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefault<T> operator /(PrimitiveDefault<T> a, PrimitiveDefault<int> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefault<T> operator /(PrimitiveDefault<T> a, PrimitiveDefault<uint> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefault<T> operator /(PrimitiveDefault<T> a, PrimitiveDefault<char> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefault<T> operator /(PrimitiveDefault<T> a, PrimitiveDefault<float> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefault<T> operator /(PrimitiveDefault<T> a, PrimitiveDefault<double> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefault<T> operator /(PrimitiveDefault<T> a, PrimitiveDefault<long> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefault<T> operator /(PrimitiveDefault<T> a, PrimitiveDefault<ulong> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefault<T> operator /(PrimitiveDefault<T> a, PrimitiveDefault<decimal> b) => Divide(a.Value, b.Value);

        public static PrimitiveDefault<T> operator /(PrimitiveDefault<T> a, PrimitiveDefaultString b) => Divide(a.Value, b.Value);

        private static PrimitiveDefault<T> Divide(dynamic aValue, dynamic bValue)
        {
            dynamic v1 = Convert.ChangeType(aValue, typeof(T));
            dynamic v2 = Convert.ChangeType(bValue, typeof(T));
            return new PrimitiveDefault<T>(v1 / v2, default(T));
        }

        #endregion

        #endregion
    }
}