namespace AutoPrimitive
{
    /// <summary>
    /// DateTime 的 implicit conversions
    /// </summary>
    public readonly struct PrimitiveDateTimeNullable
    {
        public PrimitiveDateTimeNullable(DateTime? val) : this(val, PrimitiveDateTimeConfig.DefaultFormat)
        {
        }

        public PrimitiveDateTimeNullable(DateTime? val, string format)
        {
            Value = val;
            Format = format;
        }

        public DateTime? Value { get; }
        public string Format { get; }

        public static implicit operator string(PrimitiveDateTimeNullable primitive) => primitive.Value.HasValue
            ? primitive.Format == null
                ? primitive.Value.ToString()
                : primitive.Value.Value.ToString(primitive.Format)
            : null;

#if NET6_0_OR_GREATER

        public static implicit operator DateOnly(PrimitiveDateTimeNullable primitive) => primitive.Value.HasValue
            ? new DateOnly(primitive.Value.Value.Year, primitive.Value.Value.Month, primitive.Value.Value.Day)
            : default;
        public static implicit operator TimeOnly(PrimitiveDateTimeNullable primitive) => primitive.Value.HasValue
            ? new TimeOnly(primitive.Value.Value.Hour, primitive.Value.Value.Minute, primitive.Value.Value.Second, primitive.Value.Value.Millisecond)
          : default;

        public static implicit operator DateOnly?(PrimitiveDateTimeNullable primitive) => primitive.Value.HasValue
            ? new DateOnly(primitive.Value.Value.Year, primitive.Value.Value.Month, primitive.Value.Value.Day)
            : null;

        public static implicit operator TimeOnly?(PrimitiveDateTimeNullable primitive) => primitive.Value.HasValue
            ? new TimeOnly(primitive.Value.Value.Hour, primitive.Value.Value.Minute, primitive.Value.Value.Second, primitive.Value.Value.Millisecond)
            : null;
#endif

        //操作符/方法的重写
        public static bool operator ==(PrimitiveDateTimeNullable a, PrimitiveDateTimeNullable b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveDateTimeNullable a, PrimitiveDateTimeNullable b) => !a.Value.Equals(b.Value);

        public override bool Equals(object obj)
        {
            //if (obj.GetType() == this.Value.GetType())
            //{
            //    return object.Equals(obj, this.Value);
            //}

            if (obj is PrimitiveDateTimeNullable other)
            {
                if (ReferenceEquals(this, obj))
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

        #region 运算符重载

        public static TimeSpan operator -(PrimitiveDateTimeNullable a, PrimitiveDateTimeNullable b)
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