using System.ComponentModel;
using System.Reflection;

namespace AutoPrimitive.Types
{
    /// <summary>
    /// enum的implicit conversions
    /// </summary>
    public readonly struct PrimitiveEnum
    {
        //参考: https://stackoverflow.com/q/261663/3563013 

        public PrimitiveEnum(Enum val)
        {
            Value = val;
        }
        public Enum Value { get; }

        public static implicit operator PrimitiveEnum(Enum val) => new PrimitiveEnum(val);
        public static implicit operator Enum(PrimitiveEnum primitive) => primitive.Value;

        public static implicit operator byte(PrimitiveEnum primitive) => Convert.ToByte(primitive.Value);
        public static implicit operator sbyte(PrimitiveEnum primitive) => Convert.ToSByte(primitive.Value);
        public static implicit operator short(PrimitiveEnum primitive) => Convert.ToInt16(primitive.Value);
        public static implicit operator ushort(PrimitiveEnum primitive) => Convert.ToUInt16(primitive.Value);
        public static implicit operator int(PrimitiveEnum primitive) => Convert.ToInt32(primitive.Value);
        public static implicit operator uint(PrimitiveEnum primitive) => Convert.ToUInt32(primitive.Value);
        public static implicit operator long(PrimitiveEnum primitive) => Convert.ToInt64(primitive.Value);
        public static implicit operator ulong(PrimitiveEnum primitive) => Convert.ToUInt64(primitive.Value);

        public static implicit operator string(PrimitiveEnum primitive) => primitive.Value.ToString();

        //测试发现即使这块代码没有也能对 nullable 进行比较, 原因是 nullable<int> 在和 int 做比较了.
        public static implicit operator byte?(PrimitiveEnum primitive) => new byte?(Convert.ToByte(primitive.Value));
        public static implicit operator sbyte?(PrimitiveEnum primitive) => new sbyte?(Convert.ToSByte(primitive.Value));
        public static implicit operator short?(PrimitiveEnum primitive) => new short?(Convert.ToInt16(primitive.Value));
        public static implicit operator ushort?(PrimitiveEnum primitive) => new ushort?(Convert.ToUInt16(primitive.Value));
        public static implicit operator int?(PrimitiveEnum primitive) => new int?(Convert.ToInt32(primitive.Value));
        public static implicit operator uint?(PrimitiveEnum primitive) => new uint?(Convert.ToUInt32(primitive.Value));
        public static implicit operator long?(PrimitiveEnum primitive) => new long?(Convert.ToInt64(primitive.Value));
        public static implicit operator ulong?(PrimitiveEnum primitive) => new ulong?(Convert.ToUInt64(primitive.Value));

        //操作符/方法的重写
        public static bool operator ==(PrimitiveEnum a, PrimitiveEnum b) => a.Value.Equals(b.Value);
        public static bool operator !=(PrimitiveEnum a, PrimitiveEnum b) => !a.Value.Equals(b.Value);

        public override string ToString()
        {
            return Value.ToString();
        }

        public override bool Equals(object obj)
        {
            if (obj is PrimitiveEnum other)
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
