using System.Reflection;

namespace AutoPrimitive
{
    /// <summary>
    /// 带默认值兜底的可空大数值类型包装, 负责反向转基础数值
    /// </summary>
    public readonly struct PrimitiveDefaultNumberNullable<T>
    {
        public PrimitiveDefaultNumberNullable(T val, object @default)
        {
            bool IsNullableType(Type type) => type.GetTypeInfo().IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>);

            Value = val;
            Default = @default;
            if (IsNullableType(typeof(T)) == false)
            {
                throw new Exception("只能是可空类型");
            }
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

        //数值类型: 非可空目标, 源为 null 时返回 default; 直接走显式转换运算符
        public static implicit operator short(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<short>(() => PrimitiveDefault.Value == null ? default : (short)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator ushort(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<ushort>(() => PrimitiveDefault.Value == null ? default : (ushort)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator int(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<int>(() => PrimitiveDefault.Value == null ? default : (int)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator uint(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<uint>(() => PrimitiveDefault.Value == null ? default : (uint)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator long(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<long>(() => PrimitiveDefault.Value == null ? default : (long)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator ulong(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<ulong>(() => PrimitiveDefault.Value == null ? default : (ulong)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator byte(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<byte>(() => PrimitiveDefault.Value == null ? default : (byte)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator sbyte(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<sbyte>(() => PrimitiveDefault.Value == null ? default : (sbyte)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator float(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<float>(() => PrimitiveDefault.Value == null ? default : (float)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator double(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<double>(() => PrimitiveDefault.Value == null ? default : (double)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator decimal(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<decimal>(() => PrimitiveDefault.Value == null ? default : (decimal)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        //可空数值类型: 源为 null 时返回 null
        public static implicit operator short?(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<short?>(() => PrimitiveDefault.Value == null ? default(short?) : (short)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator ushort?(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<ushort?>(() => PrimitiveDefault.Value == null ? default(ushort?) : (ushort)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator int?(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<int?>(() => PrimitiveDefault.Value == null ? default(int?) : (int)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator uint?(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<uint?>(() => PrimitiveDefault.Value == null ? default(uint?) : (uint)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator long?(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<long?>(() => PrimitiveDefault.Value == null ? default(long?) : (long)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator ulong?(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<ulong?>(() => PrimitiveDefault.Value == null ? default(ulong?) : (ulong)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator byte?(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<byte?>(() => PrimitiveDefault.Value == null ? default(byte?) : (byte)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator sbyte?(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<sbyte?>(() => PrimitiveDefault.Value == null ? default(sbyte?) : (sbyte)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator float?(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<float?>(() => PrimitiveDefault.Value == null ? default(float?) : (float)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator double?(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<double?>(() => PrimitiveDefault.Value == null ? default(double?) : (double)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        public static implicit operator decimal?(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<decimal?>(() => PrimitiveDefault.Value == null ? default(decimal?) : (decimal)(dynamic)PrimitiveDefault.Value, PrimitiveDefault.Default);

        //string
        public static implicit operator string(PrimitiveDefaultNumberNullable<T> PrimitiveDefault) => h<string>(() => PrimitiveDefault.Value == null ? default : PrimitiveDefault.Value.ToString(), PrimitiveDefault.Default);

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

            if (obj is PrimitiveDefaultNumberNullable<T> other)
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
