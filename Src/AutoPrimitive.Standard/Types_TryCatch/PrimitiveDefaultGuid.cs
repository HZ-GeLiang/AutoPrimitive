namespace AutoPrimitive
{
    /// <summary>
    /// Guid 的 implicit conversions
    /// </summary>
    public readonly struct PrimitiveDefaultGuid
    {
        public PrimitiveDefaultGuid(Guid val) : this(val, PrimitiveGuidConfig.DefaultFormat)
        {
        }

        public PrimitiveDefaultGuid(Guid val, string format)
        {
            Value = val;
            Format = format;
        }

        public Guid Value { get; }
        public string Format { get; }

        public static implicit operator string(PrimitiveDefaultGuid PrimitiveDefault) =>
            PrimitiveDefault.Format == null
            ? PrimitiveDefault.Value.ToString()
            : PrimitiveDefault.Value.ToString(PrimitiveDefault.Format);

        //操作符/方法的重写
        public static bool operator ==(PrimitiveDefaultGuid a, PrimitiveDefaultGuid b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveDefaultGuid a, PrimitiveDefaultGuid b) => !a.Value.Equals(b.Value);

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
            return Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value.ToString();
        }
    }
}