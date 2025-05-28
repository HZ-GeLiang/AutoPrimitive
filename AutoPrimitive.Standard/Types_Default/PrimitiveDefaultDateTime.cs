namespace AutoPrimitive
{
    /// <summary>
    /// DateTime 的 implicit conversions
    /// </summary>
    public readonly struct PrimitiveDefaultDateTime : IEquatable<PrimitiveDefaultDateTime>
    {
        public PrimitiveDefaultDateTime(DateTime val) : this(val, PrimitiveDateTimeConfig.DefaultFormat)
        {
        }

        public PrimitiveDefaultDateTime(DateTime val, string format)
        {
            Value = val;
            Format = format;
        }

        public DateTime Value { get; }
        public string Format { get; }

        public static implicit operator string(PrimitiveDefaultDateTime PrimitiveDefault) => PrimitiveDefault.Value.ToString(PrimitiveDefault.Format);

#if NET6_0_OR_GREATER
        public static implicit operator DateOnly(PrimitiveDefaultDateTime PrimitiveDefault) => new DateOnly(PrimitiveDefault.Value.Year, PrimitiveDefault.Value.Month, PrimitiveDefault.Value.Day);
        public static implicit operator TimeOnly(PrimitiveDefaultDateTime PrimitiveDefault) => new TimeOnly(PrimitiveDefault.Value.Hour, PrimitiveDefault.Value.Minute, PrimitiveDefault.Value.Second, PrimitiveDefault.Value.Millisecond);

        public static implicit operator DateOnly?(PrimitiveDefaultDateTime PrimitiveDefault) => new DateOnly(PrimitiveDefault.Value.Year, PrimitiveDefault.Value.Month, PrimitiveDefault.Value.Day);
        public static implicit operator TimeOnly?(PrimitiveDefaultDateTime PrimitiveDefault) => new TimeOnly(PrimitiveDefault.Value.Hour, PrimitiveDefault.Value.Minute, PrimitiveDefault.Value.Second, PrimitiveDefault.Value.Millisecond);
#endif

        //操作符/方法的重写
        public static bool operator ==(PrimitiveDefaultDateTime a, PrimitiveDefaultDateTime b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveDefaultDateTime a, PrimitiveDefaultDateTime b) => !a.Value.Equals(b.Value);

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

            if (obj is PrimitiveDefaultDateTime other)
            {
                if (this == other)
                {
                    return true;
                }

                return Equals(Value, other.Value);
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

        public override string ToString()
        {
            return this.Value.ToString(this.Format);
        }

        public bool Equals(PrimitiveDefaultDateTime other)
        {
            if (this == other)
            {
                return true;
            }

            return Equals(Value, other.Value);
        }

        #region 运算符重载

        public static TimeSpan operator -(PrimitiveDefaultDateTime a, PrimitiveDefaultDateTime b) => a.Value - b.Value;

        #endregion
    }
}