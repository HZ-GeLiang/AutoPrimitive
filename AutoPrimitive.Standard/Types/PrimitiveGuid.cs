﻿namespace AutoPrimitive
{
    /// <summary>
    /// Guid 的 implicit conversions
    /// </summary>
    public readonly struct PrimitiveGuid : IEquatable<PrimitiveGuid>
    {
        public PrimitiveGuid(Guid val) : this(val, PrimitiveGuidConfig.DefaultFormat)
        {
        }

        public PrimitiveGuid(Guid val, string format)
        {
            Value = val;
            Format = format;
        }

        public Guid Value { get; }
        public string Format { get; }

        public static implicit operator string(PrimitiveGuid primitive) =>
            primitive.Format == null
            ? primitive.Value.ToString()
            : primitive.Value.ToString(primitive.Format);

        //操作符/方法的重写
        public static bool operator ==(PrimitiveGuid a, PrimitiveGuid b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveGuid a, PrimitiveGuid b) => !a.Value.Equals(b.Value);

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj is PrimitiveGuid other)
            {
                if (this == other)
                {
                    return true;
                }

                return Equals(Value, other.Value);
            }
            else if (obj is PrimitiveGuidNullable other2)
            {
                if (this == other2)
                {
                    return true;
                }

                return Equals(Value, other2.Value);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            if (this.Format != null)
            {
                return Value.ToString(this.Format);
            }
            else
            {
                return Value.ToString();
            }
        }

        public string ToString(string format)
        {
            if (format != null)
            {
                return Value.ToString(format);
            }
            return Value.ToString();
        }

        public bool Equals(PrimitiveGuid other)
        {
            if (this == other)
            {
                return true;
            }

            return Equals(Value, other.Value);
        }
    }
}