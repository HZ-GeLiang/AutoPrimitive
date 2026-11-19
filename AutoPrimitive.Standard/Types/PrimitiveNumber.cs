namespace AutoPrimitive
{
    /// <summary>
    /// 非 IConvertible 大数值类型(BigInteger/Half/Int128/UInt128)的包装, 负责反向转基础数值
    /// </summary>
    public readonly struct PrimitiveNumber<T> where T : struct
    {
        public PrimitiveNumber(T val)
        {
            Value = val;
        }

        public T Value { get; }

        //数值类型: 直接走源类型的显式转换运算符, 避免经 double 中转丢失大整数精度
        public static implicit operator short(PrimitiveNumber<T> primitive) => (short)(dynamic)primitive.Value;

        public static implicit operator ushort(PrimitiveNumber<T> primitive) => (ushort)(dynamic)primitive.Value;

        public static implicit operator int(PrimitiveNumber<T> primitive) => (int)(dynamic)primitive.Value;

        public static implicit operator uint(PrimitiveNumber<T> primitive) => (uint)(dynamic)primitive.Value;

        public static implicit operator long(PrimitiveNumber<T> primitive) => (long)(dynamic)primitive.Value;

        public static implicit operator ulong(PrimitiveNumber<T> primitive) => (ulong)(dynamic)primitive.Value;

        public static implicit operator byte(PrimitiveNumber<T> primitive) => (byte)(dynamic)primitive.Value;

        public static implicit operator sbyte(PrimitiveNumber<T> primitive) => (sbyte)(dynamic)primitive.Value;

        public static implicit operator float(PrimitiveNumber<T> primitive) => (float)(dynamic)primitive.Value;

        public static implicit operator double(PrimitiveNumber<T> primitive) => (double)(dynamic)primitive.Value;

        public static implicit operator decimal(PrimitiveNumber<T> primitive) => (decimal)(dynamic)primitive.Value;

        //string
        public static implicit operator string(PrimitiveNumber<T> primitive) => primitive.Value.ToString();

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

            if (obj is PrimitiveNumber<T>)
            {
                return Equals(((PrimitiveNumber<T>)obj).Value, Value);
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
