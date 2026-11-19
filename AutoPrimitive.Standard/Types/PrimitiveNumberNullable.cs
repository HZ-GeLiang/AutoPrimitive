using System.Reflection;

namespace AutoPrimitive
{
    /// <summary>
    /// 可空大数值类型(BigInteger?/Half?/Int128?/UInt128?)的包装, 负责反向转基础数值
    /// </summary>
    public readonly struct PrimitiveNumberNullable<T>
    {
        public PrimitiveNumberNullable(T val)
        {
            bool IsNullableType(Type type) => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

            Value = val;
            if (IsNullableType(typeof(T)) == false)
            {
                throw new Exception("只能是可空类型");
            }
        }

        public T Value { get; }

        //数值类型: 非可空目标, 源为 null 时返回 default; 直接走显式转换运算符
        public static implicit operator short(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default : (short)(dynamic)primitive.Value;

        public static implicit operator ushort(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default : (ushort)(dynamic)primitive.Value;

        public static implicit operator int(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default : (int)(dynamic)primitive.Value;

        public static implicit operator uint(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default : (uint)(dynamic)primitive.Value;

        public static implicit operator long(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default : (long)(dynamic)primitive.Value;

        public static implicit operator ulong(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default : (ulong)(dynamic)primitive.Value;

        public static implicit operator byte(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default : (byte)(dynamic)primitive.Value;

        public static implicit operator sbyte(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default : (sbyte)(dynamic)primitive.Value;

        public static implicit operator float(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default : (float)(dynamic)primitive.Value;

        public static implicit operator double(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default : (double)(dynamic)primitive.Value;

        public static implicit operator decimal(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default : (decimal)(dynamic)primitive.Value;

        //可空数值类型: 源为 null 时返回 null
        public static implicit operator short?(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default(short?) : (short)(dynamic)primitive.Value;

        public static implicit operator ushort?(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default(ushort?) : (ushort)(dynamic)primitive.Value;

        public static implicit operator int?(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default(int?) : (int)(dynamic)primitive.Value;

        public static implicit operator uint?(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default(uint?) : (uint)(dynamic)primitive.Value;

        public static implicit operator long?(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default(long?) : (long)(dynamic)primitive.Value;

        public static implicit operator ulong?(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default(ulong?) : (ulong)(dynamic)primitive.Value;

        public static implicit operator byte?(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default(byte?) : (byte)(dynamic)primitive.Value;

        public static implicit operator sbyte?(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default(sbyte?) : (sbyte)(dynamic)primitive.Value;

        public static implicit operator float?(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default(float?) : (float)(dynamic)primitive.Value;

        public static implicit operator double?(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default(double?) : (double)(dynamic)primitive.Value;

        public static implicit operator decimal?(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default(decimal?) : (decimal)(dynamic)primitive.Value;

        //string
        public static implicit operator string(PrimitiveNumberNullable<T> primitive) => primitive.Value == null ? default : primitive.Value.ToString();

        public override string ToString()
        {
            return Value?.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return Value == null;
            }

            if (obj is PrimitiveNumberNullable<T> other)
            {
                return Equals(other.Value, Value);
            }

            if (Value == null)
            {
                return obj == null;
            }

            return Equals(obj, Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}
