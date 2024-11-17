namespace AutoPrimitive
{
    /// <summary>
    /// Guid 的 implicit conversions
    /// </summary>
    public readonly struct PrimitiveGuidNullable
    {
        public PrimitiveGuidNullable(Guid? val) : this(val, PrimitiveGuidConfig.DefaultFormat)
        {
        }

        public PrimitiveGuidNullable(Guid? val, string format)
        {
            Value = val;
            Format = format;
        }

        public Guid? Value { get; }
        public string Format { get; }

        public static implicit operator string(PrimitiveGuidNullable primitive) =>
            primitive.Value.HasValue
            ? primitive.Format == null
                ? primitive.Value.ToString()
                : primitive.Value.Value.ToString(primitive.Format)
            : null;

        //操作符/方法的重写
        public static bool operator ==(PrimitiveGuidNullable a, PrimitiveGuidNullable b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveGuidNullable a, PrimitiveGuidNullable b) => !a.Value.Equals(b.Value);

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return Value == null;
            }
            if (obj is PrimitiveGuid other)
            {
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                return Equals(Value, other.Value);
            }
            else if (obj is PrimitiveGuidNullable other2)
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
            return Value?.GetHashCode() ?? 0;
        }

        public override string ToString()
        {
            if (Value != null)
            {
                if (this.Format != null)
                {
                    return Value.Value.ToString(this.Format);
                }
                else
                {
                    return Value.Value.ToString();
                }
            }
            return null;
        }

        public string ToString(string format)
        {
            return Value == null ? null : Value.Value.ToString(format);
        }
    }
}