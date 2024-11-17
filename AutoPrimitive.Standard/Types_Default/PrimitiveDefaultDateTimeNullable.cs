namespace AutoPrimitive
{
    /// <summary>
    /// DateTime 的 implicit conversions
    /// </summary>
    public readonly struct PrimitiveDefaultDateTimeNullable : IEquatable<PrimitiveDefaultDateTimeNullable>
    {
        public PrimitiveDefaultDateTimeNullable(DateTime? val) : this(val, PrimitiveDateTimeConfig.DefaultFormat)
        {
        }

        public PrimitiveDefaultDateTimeNullable(DateTime? val, string format)
        {
            Value = val;
            Format = format;
        }

        public DateTime? Value { get; }
        public string Format { get; }

        public static implicit operator string(PrimitiveDefaultDateTimeNullable PrimitiveDefault) => PrimitiveDefault.Value.HasValue
            ? PrimitiveDefault.Format == null
                ? PrimitiveDefault.Value.ToString()
                : PrimitiveDefault.Value.Value.ToString(PrimitiveDefault.Format)
            : null;

#if NET6_0_OR_GREATER

        public static implicit operator DateOnly(PrimitiveDefaultDateTimeNullable PrimitiveDefault) => PrimitiveDefault.Value.HasValue
            ? new DateOnly(PrimitiveDefault.Value.Value.Year, PrimitiveDefault.Value.Value.Month, PrimitiveDefault.Value.Value.Day)
            : default;
        public static implicit operator TimeOnly(PrimitiveDefaultDateTimeNullable PrimitiveDefault) => PrimitiveDefault.Value.HasValue
            ? new TimeOnly(PrimitiveDefault.Value.Value.Hour, PrimitiveDefault.Value.Value.Minute, PrimitiveDefault.Value.Value.Second, PrimitiveDefault.Value.Value.Millisecond)
          : default;

        public static implicit operator DateOnly?(PrimitiveDefaultDateTimeNullable PrimitiveDefault) => PrimitiveDefault.Value.HasValue
            ? new DateOnly(PrimitiveDefault.Value.Value.Year, PrimitiveDefault.Value.Value.Month, PrimitiveDefault.Value.Value.Day)
            : null;

        public static implicit operator TimeOnly?(PrimitiveDefaultDateTimeNullable PrimitiveDefault) => PrimitiveDefault.Value.HasValue
            ? new TimeOnly(PrimitiveDefault.Value.Value.Hour, PrimitiveDefault.Value.Value.Minute, PrimitiveDefault.Value.Value.Second, PrimitiveDefault.Value.Value.Millisecond)
            : null;
#endif

        //操作符/方法的重写
        public static bool operator ==(PrimitiveDefaultDateTimeNullable a, PrimitiveDefaultDateTimeNullable b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveDefaultDateTimeNullable a, PrimitiveDefaultDateTimeNullable b) => !a.Value.Equals(b.Value);

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

            if (obj is PrimitiveDefaultDateTimeNullable other)
            {
                if (ReferenceEquals(this, other))
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
            return Value == null ? this.GetHashCode() : Value.GetHashCode();
        }

        public override string ToString()
        {
            return Value == null ? this.ToString() : this.Value.Value.ToString(this.Format);
        }

        public bool Equals(PrimitiveDefaultDateTimeNullable other)
        {
            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Equals(Value, other.Value);
        }

        #region 运算符重载

        public static TimeSpan operator -(PrimitiveDefaultDateTimeNullable a, PrimitiveDefaultDateTimeNullable b)
        {
            if (a.Value.HasValue == false)
            {
                throw new ArgumentNullException(nameof(a));
            }
            if (b.Value.HasValue == false)
            {
                throw new ArgumentNullException(nameof(b));
            }
            return a.Value.Value - b.Value.Value;
        }

        #endregion
    }
}