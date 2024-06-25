namespace AutoPrimitive.Types
{
#if NET6_0_OR_GREATER

    /// <summary>
    /// DateTime 的implicit conversions
    /// </summary>
    public readonly struct PrimitiveDateOnly
    {
        public PrimitiveDateOnly(DateOnly val) : this(val, "yyyy-MM-dd")
        {

        }

        public PrimitiveDateOnly(DateOnly val, string format)
        {
            Value = val;
            Format = format;
        }

        public DateOnly Value { get; }
        public string Format { get; }

        public static implicit operator string(PrimitiveDateOnly primitive) =>
            new DateTime(primitive.Value.Year, primitive.Value.Month, primitive.Value.Day).ToString(primitive.Format);

        public static implicit operator DateTime(PrimitiveDateOnly primitive) =>
            new DateTime(primitive.Value.Year, primitive.Value.Month, primitive.Value.Day);

        public static implicit operator DateTime?(PrimitiveDateOnly primitive) =>
           new DateTime(primitive.Value.Year, primitive.Value.Month, primitive.Value.Day);


        //操作符/方法的重写

        public static bool operator ==(PrimitiveDateOnly a, PrimitiveDateOnly b) => a.Value.Equals(b.Value);
        public static bool operator !=(PrimitiveDateOnly a, PrimitiveDateOnly b) => !a.Value.Equals(b.Value);

        public override bool Equals(object obj)
        {
            //if (obj.GetType() == this.Value.GetType())
            //{
            //    return object.Equals(obj, this.Value);
            //}

            if (obj is PrimitiveDateOnly other)
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

        public static TimeSpan operator -(PrimitiveDateOnly a, PrimitiveDateOnly b) => (DateTime)a - (DateTime)b;

        #endregion

    }

#endif
}
