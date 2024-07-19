namespace AutoPrimitive.Types
{
    /*
     注:字符串转枚举没法实现
     */

    public readonly struct PrimitiveString
    {
        public PrimitiveString(string val)
        {
            Value = val;
        }

        public string Value { get; }

        public static implicit operator PrimitiveString(string val) => new PrimitiveString(val);

        //数值类型: short ushort int uint char float double long ulong decimal
        public static implicit operator short(PrimitiveString primitive) => short.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator ushort(PrimitiveString primitive) => ushort.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator int(PrimitiveString primitive) => int.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator uint(PrimitiveString primitive) => uint.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator char(PrimitiveString primitive) => char.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator float(PrimitiveString primitive) => float.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator double(PrimitiveString primitive) => double.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator long(PrimitiveString primitive) => long.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator ulong(PrimitiveString primitive) => ulong.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator decimal(PrimitiveString primitive) => decimal.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator bool(PrimitiveString primitive) =>
            bool.TryParse(primitive.Value, out var result1) && result1 == true ||
            int.TryParse(primitive.Value, out var result2) && result2 != 0;

        public static implicit operator byte(PrimitiveString primitive) => byte.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator sbyte(PrimitiveString primitive) => sbyte.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator Guid(PrimitiveString primitive) => Guid.TryParse(primitive.Value, out var result) ? result : default;

        //可空
        public static implicit operator short?(PrimitiveString primitive) => short.TryParse(primitive.Value, out var result) ? result : default(short?);

        public static implicit operator ushort?(PrimitiveString primitive) => ushort.TryParse(primitive.Value, out var result) ? result : default(ushort?);

        public static implicit operator int?(PrimitiveString primitive) => int.TryParse(primitive.Value, out var result) ? result : default(int?);

        public static implicit operator uint?(PrimitiveString primitive) => uint.TryParse(primitive.Value, out var result) ? result : default(uint?);

        public static implicit operator char?(PrimitiveString primitive) => char.TryParse(primitive.Value, out var result) ? result : default(char?);

        public static implicit operator float?(PrimitiveString primitive) => float.TryParse(primitive.Value, out var result) ? result : default(float?);

        public static implicit operator double?(PrimitiveString primitive) => double.TryParse(primitive.Value, out var result) ? result : default(double?);

        public static implicit operator long?(PrimitiveString primitive) => long.TryParse(primitive.Value, out var result) ? result : default(long?);

        public static implicit operator ulong?(PrimitiveString primitive) => ulong.TryParse(primitive.Value, out var result) ? result : default(ulong?);

        public static implicit operator decimal?(PrimitiveString primitive) => decimal.TryParse(primitive.Value, out var result) ? result : default(decimal?);

        public static implicit operator bool?(PrimitiveString primitive)
        {
            if (primitive == null)
            {
                return null;
            }
            return bool.TryParse(primitive.Value, out var result1) && result1 == true ||
                   int.TryParse(primitive.Value, out var result2) && result2 != 0;
        }

        public static implicit operator byte?(PrimitiveString primitive) => byte.TryParse(primitive.Value, out var result) ? result : default(byte?);

        public static implicit operator sbyte?(PrimitiveString primitive) => sbyte.TryParse(primitive.Value, out var result) ? result : default(sbyte?);

        public static implicit operator Guid?(PrimitiveString primitive) => Guid.TryParse(primitive.Value, out var result) ? result : default(Guid?);

        //日期
        public static implicit operator DateTime(PrimitiveString primitive) => DateTime.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator DateTime?(PrimitiveString primitive) => DateTime.TryParse(primitive.Value, out var result) ? result : default(DateTime?);

#if NET6_0_OR_GREATER
        public static implicit operator DateOnly(PrimitiveString primitive) => DateOnly.TryParse(primitive.Value, out var result) ? result : default;
        public static implicit operator TimeOnly(PrimitiveString primitive) => TimeOnly.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator DateOnly?(PrimitiveString primitive) => DateOnly.TryParse(primitive.Value, out var result) ? result : default(DateOnly?);
        public static implicit operator TimeOnly?(PrimitiveString primitive) => TimeOnly.TryParse(primitive.Value, out var result) ? result : default(TimeOnly?);
#endif

        //string
        public static implicit operator string(PrimitiveString primitive) => primitive.Value;

        //操作符/方法的重写
        public static bool operator ==(PrimitiveString a, PrimitiveString b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveString a, PrimitiveString b) => !a.Value.Equals(b.Value);

        public override string ToString()
        {
            return Value == null ? null : Value.ToString();
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
            if (obj is PrimitiveString other)
            {
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                return Equals(other.Value, Value);
            }

            if (Value == null)
            {
                return obj == null;
            }

            return Equals(obj, Convert.ChangeType(Value, obj.GetType())); //使用内置的 ChangeType
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public static PrimitiveString operator +(PrimitiveString a, PrimitiveString b)
            => a.Value == null && b.Value == null ? null : a.Value ?? "" + b.Value ?? "";
    }
}
