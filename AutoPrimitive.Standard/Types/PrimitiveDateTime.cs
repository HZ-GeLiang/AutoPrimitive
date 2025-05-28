namespace AutoPrimitive
{
    /// <summary>
    /// DateTime 的 implicit conversions
    /// </summary>
    public readonly struct PrimitiveDateTime : IEquatable<PrimitiveDateTime>
    {
        public PrimitiveDateTime(DateTime val) : this(val, PrimitiveDateTimeConfig.DefaultFormat)
        {
        }

        public PrimitiveDateTime(DateTime val, string format)
        {
            Value = val;
            Format = format;
        }

        public DateTime Value { get; }
        public string Format { get; }

        public static implicit operator DateTime(PrimitiveDateTime primitive) => Convert.ToDateTime(primitive.Value.ToString(primitive.Format));

        public static implicit operator DateTime?(PrimitiveDateTime primitive) => Convert.ToDateTime(primitive.Value.ToString(primitive.Format));

        public static implicit operator string(PrimitiveDateTime primitive) => primitive.Value.ToString(primitive.Format);

#if NET6_0_OR_GREATER
        public static implicit operator DateOnly(PrimitiveDateTime primitive) => new DateOnly(primitive.Value.Year, primitive.Value.Month, primitive.Value.Day);
        public static implicit operator TimeOnly(PrimitiveDateTime primitive) => new TimeOnly(primitive.Value.Hour, primitive.Value.Minute, primitive.Value.Second, primitive.Value.Millisecond);

        public static implicit operator DateOnly?(PrimitiveDateTime primitive) => new DateOnly(primitive.Value.Year, primitive.Value.Month, primitive.Value.Day);
        public static implicit operator TimeOnly?(PrimitiveDateTime primitive) => new TimeOnly(primitive.Value.Hour, primitive.Value.Minute, primitive.Value.Second, primitive.Value.Millisecond);
#endif

        //操作符/方法的重写
        public static bool operator ==(PrimitiveDateTime a, PrimitiveDateTime b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveDateTime a, PrimitiveDateTime b) => !a.Value.Equals(b.Value);

        public override bool Equals(object obj)
        {
            //if (obj.GetType() == this.Value.GetType())
            //{
            //    return object.Equals(obj, this.Value);
            //}

            if (obj == null)
            {
                return false;
            }

            if (obj is PrimitiveDateTime other)
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

        public bool Equals(PrimitiveDateTime other)
        {
            if (this == other)
            {
                return true;
            }

            return Equals(Value, other.Value);
        }

        #region 运算符重载

        public static TimeSpan operator -(PrimitiveDateTime a, PrimitiveDateTime b) => a.Value - b.Value;

        #endregion
    }
}