namespace AutoPrimitive
{
    /// <summary>
    /// enum的implicit conversions
    /// </summary>
    public readonly struct PrimitiveDefaultEnum
    {
        //参考: https://stackoverflow.com/q/261663/3563013

        public PrimitiveDefaultEnum(Enum val)
        {
            Value = val;
        }

        public Enum Value { get; }

        public static implicit operator PrimitiveDefaultEnum(Enum val) => new PrimitiveDefaultEnum(val);

        public static implicit operator Enum(PrimitiveDefaultEnum PrimitiveDefault) => PrimitiveDefault.Value;

        public static implicit operator byte(PrimitiveDefaultEnum PrimitiveDefault) => Convert.ToByte(PrimitiveDefault.Value);

        public static implicit operator sbyte(PrimitiveDefaultEnum PrimitiveDefault) => Convert.ToSByte(PrimitiveDefault.Value);

        public static implicit operator short(PrimitiveDefaultEnum PrimitiveDefault) => Convert.ToInt16(PrimitiveDefault.Value);

        public static implicit operator ushort(PrimitiveDefaultEnum PrimitiveDefault) => Convert.ToUInt16(PrimitiveDefault.Value);

        public static implicit operator int(PrimitiveDefaultEnum PrimitiveDefault) => Convert.ToInt32(PrimitiveDefault.Value);

        public static implicit operator uint(PrimitiveDefaultEnum PrimitiveDefault) => Convert.ToUInt32(PrimitiveDefault.Value);

        public static implicit operator long(PrimitiveDefaultEnum PrimitiveDefault) => Convert.ToInt64(PrimitiveDefault.Value);

        public static implicit operator ulong(PrimitiveDefaultEnum PrimitiveDefault) => Convert.ToUInt64(PrimitiveDefault.Value);

        public static implicit operator string(PrimitiveDefaultEnum PrimitiveDefault) => PrimitiveDefault.Value.ToString();

        //测试发现即使这块代码没有也能对 nullable 进行比较, 原因是 nullable<int> 在和 int 做比较了.
        public static implicit operator byte?(PrimitiveDefaultEnum PrimitiveDefault) => new byte?(Convert.ToByte(PrimitiveDefault.Value));

        public static implicit operator sbyte?(PrimitiveDefaultEnum PrimitiveDefault) => new sbyte?(Convert.ToSByte(PrimitiveDefault.Value));

        public static implicit operator short?(PrimitiveDefaultEnum PrimitiveDefault) => new short?(Convert.ToInt16(PrimitiveDefault.Value));

        public static implicit operator ushort?(PrimitiveDefaultEnum PrimitiveDefault) => new ushort?(Convert.ToUInt16(PrimitiveDefault.Value));

        public static implicit operator int?(PrimitiveDefaultEnum PrimitiveDefault) => new int?(Convert.ToInt32(PrimitiveDefault.Value));

        public static implicit operator uint?(PrimitiveDefaultEnum PrimitiveDefault) => new uint?(Convert.ToUInt32(PrimitiveDefault.Value));

        public static implicit operator long?(PrimitiveDefaultEnum PrimitiveDefault) => new long?(Convert.ToInt64(PrimitiveDefault.Value));

        public static implicit operator ulong?(PrimitiveDefaultEnum PrimitiveDefault) => new ulong?(Convert.ToUInt64(PrimitiveDefault.Value));

        //操作符/方法的重写
        public static bool operator ==(PrimitiveDefaultEnum a, PrimitiveDefaultEnum b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveDefaultEnum a, PrimitiveDefaultEnum b) => !a.Value.Equals(b.Value);

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return Value == null;
            }

            if (obj is PrimitiveDefaultEnum other)
            {
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                return Equals(Value, other.Value);
            }

            if (Value == null)
            {
                return obj == null;
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}