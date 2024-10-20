﻿namespace AutoPrimitive
{
    /// <summary>
    /// Guid 的 implicit conversions
    /// </summary>
    public readonly struct PrimitiveDefaultGuidNullable
    {
        public PrimitiveDefaultGuidNullable(Guid? val) : this(val, "")
        {
        }

        public PrimitiveDefaultGuidNullable(Guid? val, string format)
        {
            Value = val;
            Format = format;
        }

        public Guid? Value { get; }
        public string Format { get; }

        public static implicit operator string(PrimitiveDefaultGuidNullable PrimitiveDefault) =>
            PrimitiveDefault.Value.HasValue
            ? PrimitiveDefault.Format == null
                ? PrimitiveDefault.Value.ToString()
                : PrimitiveDefault.Value.Value.ToString(PrimitiveDefault.Format)
            : null;

        //操作符/方法的重写
        public static bool operator ==(PrimitiveDefaultGuidNullable a, PrimitiveDefaultGuidNullable b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveDefaultGuidNullable a, PrimitiveDefaultGuidNullable b) => !a.Value.Equals(b.Value);

        public override bool Equals(object obj)
        {
            if (obj is PrimitiveDefaultGuid other)
            {
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                return Equals(Value, other.Value);
            }
            else if (obj is PrimitiveDefaultGuidNullable other2)
            {
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                return Equals(Value, other2.Value);
            }

            return false;
        }

        public override int GetHashCode()
        {
            return Value == null ? this.GetHashCode() : Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value == null ? this.ToString() : Value.ToString();
        }
    }
}