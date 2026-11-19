namespace AutoPrimitive
{
    /// <summary>
    /// 带默认值兜底的大数值类型包装, 负责反向转基础数值
    /// </summary>
    public readonly struct PrimitiveDefaultNumber<T> where T : struct
    {
        public PrimitiveDefaultNumber(T val, object @default)
        {
            Value = val;
            Default = @default;
        }

        public T Value { get; }

        public object Default { get; }

        private static dynamic h<T_Target>(Func<dynamic> func, object @default)
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

        //数值类型: 直接走显式转换运算符, 异常时回退默认值
        public static implicit operator short(PrimitiveDefaultNumber<T> PrimitiveDefault) => h<short>(() => (short)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator ushort(PrimitiveDefaultNumber<T> PrimitiveDefault) => h<ushort>(() => (ushort)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator int(PrimitiveDefaultNumber<T> PrimitiveDefault) => h<int>(() => (int)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator uint(PrimitiveDefaultNumber<T> PrimitiveDefault) => h<uint>(() => (uint)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator long(PrimitiveDefaultNumber<T> PrimitiveDefault) => h<long>(() => (long)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator ulong(PrimitiveDefaultNumber<T> PrimitiveDefault) => h<ulong>(() => (ulong)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator byte(PrimitiveDefaultNumber<T> PrimitiveDefault) => h<byte>(() => (byte)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator sbyte(PrimitiveDefaultNumber<T> PrimitiveDefault) => h<sbyte>(() => (sbyte)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator float(PrimitiveDefaultNumber<T> PrimitiveDefault) => h<float>(() => (float)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator double(PrimitiveDefaultNumber<T> PrimitiveDefault) => h<double>(() => (double)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator decimal(PrimitiveDefaultNumber<T> PrimitiveDefault) => h<decimal>(() => (decimal)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        //string
        public static implicit operator string(PrimitiveDefaultNumber<T> PrimitiveDefault) => h<string>(() => PrimitiveDefault.Value.ToString(), PrimitiveDefault.Default);

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is PrimitiveDefaultNumber<T>)
            {
                return Equals(((PrimitiveDefaultNumber<T>)obj).Value, Value);
            }

            if (obj.GetType() == Value.GetType())
            {
                return Equals(obj, Value);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
