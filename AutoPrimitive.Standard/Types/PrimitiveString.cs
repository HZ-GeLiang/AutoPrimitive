namespace AutoPrimitive
{
    /*
     注:字符串转枚举没法实现
    字符串转其他类型时, 如果转失败, 返回 default 值
    */

    public readonly struct PrimitiveString : IEquatable<PrimitiveString>
    {
        public PrimitiveString(string val)
        {
            Value = val;
        }

        public string Value { get; }

        public static implicit operator PrimitiveString(string val) => new PrimitiveString(val);

        //数值类型: short ushort int uint char float double long ulong decimal
        public static implicit operator short(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (short.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default;
        }

        public static implicit operator ushort(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (ushort.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default;
        }

        public static implicit operator int(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                {
                    if (int.TryParse(primitive.Value, out var result))
                    {
                        return result;
                    }
                }
                try
                {
                    var result = MathUtil.ToInt32(primitive.Value, out var ex);
                    if (ex == null)
                    {
                        return result;
                    }
                }
                catch (Exception)
                {

                }
            }

            return default;
        }

        public static implicit operator uint(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (uint.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default;
        }

        public static implicit operator char(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (char.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default;
        }

        public static implicit operator float(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (float.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default;
        }


        public static implicit operator double(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                {
                    if (double.TryParse(primitive.Value, out var result))
                    {
                        return result;
                    }
                }
                try
                {
                    var result = MathUtil.ToDouble(primitive.Value, out var ex);
                    if (ex == null)
                    {
                        return result;
                    }
                }
                catch (Exception)
                {

                }
            }

            return default;

        }

        public static implicit operator long(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                {
                    if (long.TryParse(primitive.Value, out var result))
                    {
                        return result;
                    }
                }
                try
                {
                    var result = MathUtil.ToInt64(primitive.Value, out var ex);
                    if (ex != null)
                    {
                        return result;
                    }
                }
                catch (Exception)
                {

                }
            }

            return default;
        }

        public static implicit operator ulong(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (ulong.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default;
        }

        public static implicit operator decimal(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                {
                    if (decimal.TryParse(primitive.Value, out var result))
                    {
                        return result;
                    }
                }
                try
                {
                    var result = MathUtil.ToDecimal(primitive.Value, out var ex);
                    if (ex != null)
                    {
                        return result;
                    }
                }
                catch (Exception)
                {

                }
            }
            return default;
        }


        public static implicit operator bool(PrimitiveString primitive)
        {
            return
                bool.TryParse(primitive.Value, out var result1) && result1 == true ||
                int.TryParse(primitive.Value, out var result2) && result2 != 0;
        }


        public static implicit operator byte(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (byte.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default;
        }

        public static implicit operator sbyte(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (sbyte.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default;
        }

        public static implicit operator Guid(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (Guid.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default;
        }

        //可空
        public static implicit operator short?(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (short.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default(short?);
        }

        public static implicit operator ushort?(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (ushort.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default(ushort?);
        }

        public static implicit operator int?(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                {
                    if (int.TryParse(primitive.Value, out var result))
                    {
                        return result;
                    }
                }
                try
                {
                    var result = MathUtil.ToInt32(primitive.Value, out var ex);
                    if (ex == null)
                    {
                        return result;
                    }
                }
                catch (Exception)
                {

                }
            }

            return default(int?);
        }

        public static implicit operator uint?(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (uint.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default(uint?);
        }

        public static implicit operator char?(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (char.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default(char?);
        }

        public static implicit operator float?(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (float.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default(float?);
        }

        public static implicit operator double?(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                {
                    if (double.TryParse(primitive.Value, out var result))
                    {
                        return result;
                    }
                }

                try
                {
                    var result = MathUtil.ToDouble(primitive.Value, out var ex);
                    if (ex != null)
                    {
                        return result;
                    }
                }
                catch (Exception)
                {

                }
            }

            return default(double?);
        }

        public static implicit operator long?(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                {
                    if (long.TryParse(primitive.Value, out var result))
                    {
                        return result;
                    }
                }
                try
                {
                    var result = MathUtil.ToInt64(primitive.Value, out var ex);
                    if (ex != null)
                    {
                        return result;
                    }
                }
                catch (Exception)
                {

                }
            }

            return default(long?);
        }

        public static implicit operator ulong?(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (ulong.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default(ulong?);
        }

        public static implicit operator decimal?(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                {
                    if (decimal.TryParse(primitive.Value, out var result))
                    {
                        return result;
                    }
                }
                try
                {
                    var result = MathUtil.ToDecimal(primitive.Value, out var ex);
                    if (ex != null)
                    {
                        return result;
                    }
                }
                catch (Exception)
                {

                }
            }



            return default(decimal?);
        }

        public static implicit operator bool?(PrimitiveString primitive)
        {
            if (primitive == null)
            {
                return null;
            }
            return bool.TryParse(primitive.Value, out var result1) && result1 == true ||
                   int.TryParse(primitive.Value, out var result2) && result2 != 0;
        }

        public static implicit operator byte?(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (byte.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default(byte?);
        }

        public static implicit operator sbyte?(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (sbyte.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default(sbyte?);
        }

        public static implicit operator Guid?(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                if (Guid.TryParse(primitive.Value, out var result))
                {
                    return result;
                }
            }
            return default(Guid?);
        }

        //日期
        public static implicit operator DateTime(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                {
                    //普通日期, 可以识别成日期的字符串值.
                    if (DateTime.TryParse(primitive.Value, out var dt))
                    {
                        return dt;
                    }
                }

                {
                    //JS时间戳, 长度=10,11,13,14
                    if (JsTimeConverter.Convert_JS_timestamp(primitive.Value, out var dt))
                    {
                        return dt.Value;
                    }
                }

                {
                    //yyyymmddhhmmss 格式的字符串 , 长度=14
                    if (DateTimeConverter.TryParseYmdHms(primitive.Value, out var dt))
                    {
                        return dt;
                    }
                }

                {
                    //yyyymmdd 格式的字符串,长度=8
                    if (DateTimeConverter.TryParseYmd(primitive.Value, out var dt))
                    {
                        return dt;
                    }
                }

                {
                    //JS日期对象, 长度24起步,  类似 Fri Aug 15 2025 08:07:32 GMT+0800 (香港标准时间)
                    if (JsTimeConverter.Convert_JS_DateObject(primitive.Value, out var dt))
                    {
                        return dt.Value;
                    }
                }
            }
            return default;
        }

        public static implicit operator DateTime?(PrimitiveString primitive)
        {
            if (primitive.Value != null)
            {
                {
                    //普通日期
                    if (DateTime.TryParse(primitive.Value, out var dt))
                    {
                        return dt;
                    }
                }

                {
                    //JS时间戳
                    if (JsTimeConverter.Convert_JS_timestamp(primitive.Value, out var dt))
                    {
                        return dt;
                    }
                }

                {
                    //yyyymmddhhmmss 格式的字符串
                    if (DateTimeConverter.TryParseYmdHms(primitive.Value, out var dt))
                    {
                        return dt;
                    }
                }

                {
                    //yyyymmdd 格式的字符串
                    if (DateTimeConverter.TryParseYmd(primitive.Value, out var dt))
                    {
                        return dt;
                    }
                }

                {
                    //JS日期对象
                    if (JsTimeConverter.Convert_JS_DateObject(primitive.Value, out var dt))
                    {
                        return dt;
                    }
                }
            }
            return default(DateTime?);
        }


#if NET6_0_OR_GREATER
        public static implicit operator DateOnly(PrimitiveString primitive) => DateOnly.TryParse(primitive.Value, out var result) ? result : default;
        public static implicit operator TimeOnly(PrimitiveString primitive) => TimeOnly.TryParse(primitive.Value, out var result) ? result : default;

        public static implicit operator DateOnly?(PrimitiveString primitive) => DateOnly.TryParse(primitive.Value, out var result) ? result : default(DateOnly?);
        public static implicit operator TimeOnly?(PrimitiveString primitive) => TimeOnly.TryParse(primitive.Value, out var result) ? result : default(TimeOnly?);
#endif

        //string
        public static implicit operator string(PrimitiveString primitive) => primitive.Value;

#if NETSTANDARD1_3_OR_GREATER || NET6_0_OR_GREATER
        //FormattableString

        public static implicit operator FormattableString(PrimitiveString primitive)
            => primitive.Value == null ? default(FormattableString) : primitive.Value.ToFormattableString();
#endif


        #region 操作符/方法的重写

        public static bool operator ==(PrimitiveString a, PrimitiveString b) => a.Value.Equals(b.Value);

        public static bool operator !=(PrimitiveString a, PrimitiveString b) => !a.Value.Equals(b.Value);

        //非可空

        public static bool operator ==(short a, PrimitiveString b)
        {
            short c = b;
            return a == c;
        }

        public static bool operator !=(short a, PrimitiveString b)
        {
            short c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, short b)
        {
            short c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, short b)
        {
            short c = a;
            return b != c;
        }



        public static bool operator ==(ushort a, PrimitiveString b)
        {
            ushort c = b;
            return a == c;
        }

        public static bool operator !=(ushort a, PrimitiveString b)
        {
            ushort c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, ushort b)
        {
            ushort c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, ushort b)
        {
            ushort c = a;
            return b != c;
        }



        public static bool operator ==(int a, PrimitiveString b)
        {
            int c = b;
            return a == c;
        }

        public static bool operator !=(int a, PrimitiveString b)
        {
            int c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, int b)
        {
            int c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, int b)
        {
            int c = a;
            return b != c;
        }


        public static bool operator ==(uint a, PrimitiveString b)
        {
            uint c = b;
            return a == c;
        }

        public static bool operator !=(uint a, PrimitiveString b)
        {
            uint c = b;
            return a != c;
        }


        public static bool operator ==(char a, PrimitiveString b)
        {
            char c = b;
            return a == c;
        }

        public static bool operator !=(char a, PrimitiveString b)
        {
            char c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, char b)
        {
            char c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, char b)
        {
            char c = a;
            return b != c;
        }



        public static bool operator ==(float a, PrimitiveString b)
        {
            float c = b;
            return a == c;
        }

        public static bool operator !=(float a, PrimitiveString b)
        {
            float c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, float b)
        {
            float c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, float b)
        {
            float c = a;
            return b != c;
        }



        public static bool operator ==(double a, PrimitiveString b)
        {
            double c = b;
            return a == c;
        }

        public static bool operator !=(double a, PrimitiveString b)
        {
            double c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, double b)
        {
            double c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, double b)
        {
            double c = a;
            return b != c;
        }



        public static bool operator ==(long a, PrimitiveString b)
        {
            long c = b;
            return a == c;
        }

        public static bool operator !=(long a, PrimitiveString b)
        {
            long c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, long b)
        {
            long c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, long b)
        {
            long c = a;
            return b != c;
        }


        public static bool operator ==(ulong a, PrimitiveString b)
        {
            ulong c = b;
            return a == c;
        }

        public static bool operator !=(ulong a, PrimitiveString b)
        {
            ulong c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, ulong b)
        {
            ulong c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, ulong b)
        {
            ulong c = a;
            return b != c;
        }


        public static bool operator ==(decimal a, PrimitiveString b)
        {
            decimal c = b;
            return a == c;
        }

        public static bool operator !=(decimal a, PrimitiveString b)
        {
            decimal c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, decimal b)
        {
            decimal c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, decimal b)
        {
            decimal c = a;
            return b != c;
        }


        public static bool operator ==(bool a, PrimitiveString b)
        {
            bool c = b;
            return a == c;
        }

        public static bool operator !=(bool a, PrimitiveString b)
        {
            bool c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, bool b)
        {
            bool c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, bool b)
        {
            bool c = a;
            return b != c;
        }


        public static bool operator ==(sbyte a, PrimitiveString b)
        {
            sbyte c = b;
            return a == c;
        }

        public static bool operator !=(sbyte a, PrimitiveString b)
        {
            sbyte c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, sbyte b)
        {
            sbyte c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, sbyte b)
        {
            sbyte c = a;
            return b != c;
        }


        public static bool operator ==(Guid a, PrimitiveString b)
        {
            Guid c = b;
            return a == c;
        }

        public static bool operator !=(Guid a, PrimitiveString b)
        {
            Guid c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, Guid b)
        {
            Guid c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, Guid b)
        {
            Guid c = a;
            return b != c;
        }


        public static bool operator ==(PrimitiveString a, uint b)
        {
            uint c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, uint b)
        {
            uint c = a;
            return b != c;
        }


        public static bool operator ==(DateTime a, PrimitiveString b)
        {
            DateTime c = b;
            return a == c;
        }

        public static bool operator !=(DateTime a, PrimitiveString b)
        {
            DateTime c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, DateTime b)
        {
            DateTime c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, DateTime b)
        {
            DateTime c = a;
            return b != c;
        }



        //可空

        public static bool operator ==(short? a, PrimitiveString b)
        {
            short? c = b;
            return a == c;
        }

        public static bool operator !=(short? a, PrimitiveString b)
        {
            short? c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, short? b)
        {
            short? c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, short? b)
        {
            short? c = a;
            return b != c;
        }



        public static bool operator ==(ushort? a, PrimitiveString b)
        {
            ushort? c = b;
            return a == c;
        }

        public static bool operator !=(ushort? a, PrimitiveString b)
        {
            ushort? c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, ushort? b)
        {
            ushort? c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, ushort? b)
        {
            ushort? c = a;
            return b != c;
        }



        public static bool operator ==(int? a, PrimitiveString b)
        {
            int? c = b;
            return a == c;
        }

        public static bool operator !=(int? a, PrimitiveString b)
        {
            int? c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, int? b)
        {
            int? c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, int? b)
        {
            int? c = a;
            return b != c;
        }


        public static bool operator ==(uint? a, PrimitiveString b)
        {
            uint? c = b;
            return a == c;
        }

        public static bool operator !=(uint? a, PrimitiveString b)
        {
            uint? c = b;
            return a != c;
        }


        public static bool operator ==(char? a, PrimitiveString b)
        {
            char? c = b;
            return a == c;
        }

        public static bool operator !=(char? a, PrimitiveString b)
        {
            char? c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, char? b)
        {
            char? c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, char? b)
        {
            char? c = a;
            return b != c;
        }



        public static bool operator ==(float? a, PrimitiveString b)
        {
            float? c = b;
            return a == c;
        }

        public static bool operator !=(float? a, PrimitiveString b)
        {
            float? c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, float? b)
        {
            float? c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, float? b)
        {
            float? c = a;
            return b != c;
        }



        public static bool operator ==(double? a, PrimitiveString b)
        {
            double? c = b;
            return a == c;
        }

        public static bool operator !=(double? a, PrimitiveString b)
        {
            double? c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, double? b)
        {
            double? c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, double? b)
        {
            double? c = a;
            return b != c;
        }



        public static bool operator ==(long? a, PrimitiveString b)
        {
            long? c = b;
            return a == c;
        }

        public static bool operator !=(long? a, PrimitiveString b)
        {
            long? c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, long? b)
        {
            long? c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, long? b)
        {
            long? c = a;
            return b != c;
        }


        public static bool operator ==(ulong? a, PrimitiveString b)
        {
            ulong? c = b;
            return a == c;
        }

        public static bool operator !=(ulong? a, PrimitiveString b)
        {
            ulong? c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, ulong? b)
        {
            ulong? c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, ulong? b)
        {
            ulong? c = a;
            return b != c;
        }


        public static bool operator ==(decimal? a, PrimitiveString b)
        {
            decimal? c = b;
            return a == c;
        }

        public static bool operator !=(decimal? a, PrimitiveString b)
        {
            decimal? c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, decimal? b)
        {
            decimal? c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, decimal? b)
        {
            decimal? c = a;
            return b != c;
        }


        public static bool operator ==(bool? a, PrimitiveString b)
        {
            bool? c = b;
            return a == c;
        }

        public static bool operator !=(bool? a, PrimitiveString b)
        {
            bool? c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, bool? b)
        {
            bool? c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, bool? b)
        {
            bool? c = a;
            return b != c;
        }


        public static bool operator ==(sbyte? a, PrimitiveString b)
        {
            sbyte? c = b;
            return a == c;
        }

        public static bool operator !=(sbyte? a, PrimitiveString b)
        {
            sbyte? c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, sbyte? b)
        {
            sbyte? c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, sbyte? b)
        {
            sbyte? c = a;
            return b != c;
        }


        public static bool operator ==(Guid? a, PrimitiveString b)
        {
            Guid? c = b;
            return a == c;
        }

        public static bool operator !=(Guid? a, PrimitiveString b)
        {
            Guid? c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, Guid? b)
        {
            Guid? c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, Guid? b)
        {
            Guid? c = a;
            return b != c;
        }


        public static bool operator ==(PrimitiveString a, uint? b)
        {
            uint? c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, uint? b)
        {
            uint? c = a;
            return b != c;
        }


        public static bool operator ==(DateTime? a, PrimitiveString b)
        {
            DateTime? c = b;
            return a == c;
        }

        public static bool operator !=(DateTime? a, PrimitiveString b)
        {
            DateTime? c = b;
            return a != c;
        }

        public static bool operator ==(PrimitiveString a, DateTime? b)
        {
            DateTime? c = a;
            return b == c;
        }

        public static bool operator !=(PrimitiveString a, DateTime? b)
        {
            DateTime? c = a;
            return b != c;
        }

        #endregion

        public override string ToString()
        {
            return Value == null ? null : Value.ToString();
        }

        /// <summary>
        /// 注:如果用 object.Equals来比较的, 请确保 参数1为  Primitive类型
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
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

            if (obj is PrimitiveString other)
            {
                if (this == other)
                {
                    return true;
                }

                return Equals(other.Value, Value);
            }

            if (Value == null)
            {
                return obj == null;
            }

            return Equals(obj, Convert.ChangeType(Value, obj.GetType())); //使用内置的 ChangeType
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }

        public bool Equals(PrimitiveString other)
        {
            if (this == other)
            {
                return true;
            }

            return Equals(other.Value, Value);
        }

        public static PrimitiveString operator +(PrimitiveString a, PrimitiveString b)
            => a.Value == null && b.Value == null ? null : a.Value ?? "" + b.Value ?? "";
    }
}