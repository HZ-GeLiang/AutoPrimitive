namespace AutoPrimitive
{
#if NET6_0_OR_GREATER

    /// <summary>
    /// DateTime 的implicit conversions
    /// </summary>
    public readonly struct PrimitiveDefaultDateOnly
    {
        public PrimitiveDefaultDateOnly(DateOnly val) : this(val, "yyyy-MM-dd")
        {
        }

        public PrimitiveDefaultDateOnly(DateOnly val, string format)
        {
            Value = val;
            Format = format;
        }

        public DateOnly Value { get; }
        public string Format { get; }

        public static implicit operator string(PrimitiveDefaultDateOnly PrimitiveDefault) =>
            new DateTime(PrimitiveDefault.Value.Year, PrimitiveDefault.Value.Month, PrimitiveDefault.Value.Day).ToString(PrimitiveDefault.Format);

        public static implicit operator DateTime(PrimitiveDefaultDateOnly PrimitiveDefault) =>
            new DateTime(PrimitiveDefault.Value.Year, PrimitiveDefault.Value.Month, PrimitiveDefault.Value.Day);

        public static implicit operator DateTime?(PrimitiveDefaultDateOnly PrimitiveDefault) =>
           new DateTime(PrimitiveDefault.Value.Year, PrimitiveDefault.Value.Month, PrimitiveDefault.Value.Day);

        //操作符/方法的重写

        public static bool operator ==(PrimitiveDefaultDateOnly a, PrimitiveDefaultDateOnly b) => a.Value.Equals(b.Value);
        public static bool operator !=(PrimitiveDefaultDateOnly a, PrimitiveDefaultDateOnly b) => !a.Value.Equals(b.Value);

        public override bool Equals(object obj)
        {
            //if (obj.GetType() == this.Value.GetType())
            //{
            //    return object.Equals(obj, this.Value);
            //}

            if (obj == null)
            {
                return Value == null;
            }

            if (obj is PrimitiveDefaultDateOnly other)
            {
                if (ReferenceEquals(this, obj))
                {
                    return true;
                }

                return object.Equals(Value, other.Value);
            }

            //if (this.Value == null)
            //{
            //    return obj == null;
            //}

            return false;
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        #region 运算符重载

        public static TimeSpan operator -(PrimitiveDefaultDateOnly a, PrimitiveDefaultDateOnly b) => (DateTime)a - (DateTime)b;

        #endregion
    }

#endif
}