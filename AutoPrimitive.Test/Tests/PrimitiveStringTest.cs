using System.Numerics;

namespace AutoPrimitive.Test.Tests
{
    public class Test01
    {
        public DateTime? Start { get; set; }
    }

    [TestClass]
    public class PrimitiveStringTest
    {
        [TestMethod]
        public void 代码运行时不报错_日期比较()
        {

            {
                var item = new Test01() { Start = DateTime.Now };
                bool result2 = item.Start.Value == "2025-01-01".ToPrimitive();
            }

            {
                var item = new Test01() { };

                bool result3 = "2025-01-01".ToPrimitive() == item.Start;
                bool result = item.Start == "2025-01-01".ToPrimitive();
            }
        }


        [TestMethod]
        public void 转可空int()
        {
            {
                string? s = null;
                int? d = s.ToPrimitive();
                Assert.AreEqual(d, null);
            }

            {
                string s = "1";
                int? d = s.ToPrimitive();
                Assert.AreEqual(d, 1);
            }

            {
                string s = "";
                int? d = s.ToPrimitive();
                Assert.AreEqual(d, null);
            }

            {
                string s = "a";
                int? d = s.ToPrimitive();
                Assert.AreEqual(d, null);
            }


        }

        [TestMethod]
        public void 转Bool()
        {
            {
                string s = "true";
                bool d = s.ToPrimitive();
                Assert.AreEqual(d, true);
            }

            {
                string s = "true";
                if (s.ToPrimitive())
                {
                    Assert.AreEqual(true, true);
                }
                else
                {
                    Assert.AreEqual(false, true);
                }
            }

            {
                string s = "1";
                bool d = s.ToPrimitive();
                Assert.AreEqual(d, true);
            }

            {
                string s = "false";
                bool d = s.ToPrimitive();
                Assert.AreEqual(d, false);
            }

            {
                string s = "0";
                bool d = s.ToPrimitive();
                Assert.AreEqual(d, false);
            }
        }

        [TestMethod]
        public void 转日期()
        {
            {
                var str = "2024年1月";
                DateTime d = str.ToPrimitive();
                Assert.AreEqual(d, new DateTime(2024, 1, 1));
            }

            {
                DateTime d = "2024-04-06 23:59:59.999".ToPrimitive();
                Assert.AreEqual(d, Convert.ToDateTime("2024-04-06 23:59:59.999"));
            }
        }

        [TestMethod]
        public void 转TimeSpan()
        {
            {
                TimeSpan d = "01:02:03".ToPrimitive();
                Assert.AreEqual(d, new TimeSpan(1, 2, 3));
            }

            {
                TimeSpan? d = "01:02:03".ToPrimitive();
                Assert.AreEqual(d, new TimeSpan(1, 2, 3));
            }

            {
                TimeSpan d = "abc".ToPrimitive();
                Assert.AreEqual(d, default(TimeSpan));
            }

            {
                TimeSpan? d = "abc".ToPrimitive();
                Assert.AreEqual(d, null);
            }
        }

        [TestMethod]
        public void 转DateTimeOffset()
        {
            {
                DateTimeOffset d = "2024-04-06 23:59:59+08:00".ToPrimitive();
                Assert.AreEqual(d, DateTimeOffset.Parse("2024-04-06 23:59:59+08:00"));
            }

            {
                DateTimeOffset? d = "2024-04-06 23:59:59+08:00".ToPrimitive();
                Assert.AreEqual(d, DateTimeOffset.Parse("2024-04-06 23:59:59+08:00"));
            }

            {
                DateTimeOffset d = "abc".ToPrimitive();
                Assert.AreEqual(d, default(DateTimeOffset));
            }

            {
                DateTimeOffset? d = "abc".ToPrimitive();
                Assert.AreEqual(d, null);
            }
        }

        [TestMethod]
        public void 转BigInteger()
        {
            {
                BigInteger d = "123456789012345678901234567890".ToPrimitive();
                Assert.AreEqual(d, BigInteger.Parse("123456789012345678901234567890"));
            }

            {
                BigInteger? d = "123456789012345678901234567890".ToPrimitive();
                Assert.AreEqual(d, BigInteger.Parse("123456789012345678901234567890"));
            }

            {
                BigInteger d = "abc".ToPrimitive();
                Assert.AreEqual(d, default(BigInteger));
            }

            {
                BigInteger? d = "abc".ToPrimitive();
                Assert.AreEqual(d, null);
            }
        }

        [TestMethod]
        public void 转Half()
        {
            {
                Half d = "1.5".ToPrimitive();
                Assert.AreEqual(d, Half.Parse("1.5"));
            }

            {
                Half? d = "1.5".ToPrimitive();
                Assert.AreEqual(d, Half.Parse("1.5"));
            }

            {
                Half d = "abc".ToPrimitive();
                Assert.AreEqual(d, default(Half));
            }

            {
                Half? d = "abc".ToPrimitive();
                Assert.AreEqual(d, null);
            }
        }

#if NET6_0_OR_GREATER
        [TestMethod]
        public void 转TimeOnly()
        {
            {
                TimeOnly d = "23:59:59".ToPrimitive();
                Assert.AreEqual(d, TimeOnly.Parse("23:59:59"));
            }

            {
                TimeOnly? d = "23:59:59".ToPrimitive();
                Assert.AreEqual(d, TimeOnly.Parse("23:59:59"));
            }

            {
                TimeOnly d = "abc".ToPrimitive();
                Assert.AreEqual(d, default(TimeOnly));
            }

            {
                TimeOnly? d = "abc".ToPrimitive();
                Assert.AreEqual(d, null);
            }
        }
#endif

#if NET7_0_OR_GREATER
        [TestMethod]
        public void 转Int128()
        {
            {
                Int128 d = "123456789012345678901234567890".ToPrimitive();
                Assert.AreEqual(d, Int128.Parse("123456789012345678901234567890"));
            }

            {
                Int128? d = "123456789012345678901234567890".ToPrimitive();
                Assert.AreEqual(d, Int128.Parse("123456789012345678901234567890"));
            }

            {
                Int128 d = "abc".ToPrimitive();
                Assert.AreEqual(d, default(Int128));
            }

            {
                Int128? d = "abc".ToPrimitive();
                Assert.AreEqual(d, null);
            }
        }

        [TestMethod]
        public void 转UInt128()
        {
            {
                UInt128 d = "123456789012345678901234567890".ToPrimitive();
                Assert.AreEqual(d, UInt128.Parse("123456789012345678901234567890"));
            }

            {
                UInt128? d = "123456789012345678901234567890".ToPrimitive();
                Assert.AreEqual(d, UInt128.Parse("123456789012345678901234567890"));
            }

            {
                UInt128 d = "abc".ToPrimitive();
                Assert.AreEqual(d, default(UInt128));
            }

            {
                UInt128? d = "abc".ToPrimitive();
                Assert.AreEqual(d, null);
            }
        }
#endif

        [TestMethod]
        public void 转基础数值类型_合法字符串()
        {
            short @short = "12".ToPrimitive();
            Assert.AreEqual((short)12, @short);

            ushort @ushort = "12".ToPrimitive();
            Assert.AreEqual((ushort)12, @ushort);

            int @int = "12".ToPrimitive();
            Assert.AreEqual(12, @int);

            uint @uint = "12".ToPrimitive();
            Assert.AreEqual((uint)12, @uint);

            long @long = "12".ToPrimitive();
            Assert.AreEqual(12L, @long);

            ulong @ulong = "12".ToPrimitive();
            Assert.AreEqual(12UL, @ulong);

            decimal @decimal = "12.34".ToPrimitive();
            Assert.AreEqual(12.34M, @decimal);

            float @float = "12.34".ToPrimitive();
            Assert.AreEqual(12.34f, @float);

            double @double = "12.34".ToPrimitive();
            Assert.AreEqual(12.34, @double);

            byte @byte = "12".ToPrimitive();
            Assert.AreEqual((byte)12, @byte);

            sbyte @sbyte = "-5".ToPrimitive();
            Assert.AreEqual((sbyte)-5, @sbyte);

            char @char = "a".ToPrimitive();
            Assert.AreEqual('a', @char);
        }

        [TestMethod]
        public void 转基础数值类型_非法字符串返回默认值()
        {
            short @short = "abc".ToPrimitive();
            Assert.AreEqual(default(short), @short);

            ushort @ushort = "abc".ToPrimitive();
            Assert.AreEqual(default(ushort), @ushort);

            int @int = "abc".ToPrimitive();
            Assert.AreEqual(0, @int);

            uint @uint = "abc".ToPrimitive();
            Assert.AreEqual(0U, @uint);

            long @long = "abc".ToPrimitive();
            Assert.AreEqual(0L, @long);

            ulong @ulong = "abc".ToPrimitive();
            Assert.AreEqual(0UL, @ulong);

            decimal @decimal = "abc".ToPrimitive();
            Assert.AreEqual(0M, @decimal);

            float @float = "abc".ToPrimitive();
            Assert.AreEqual(0f, @float);

            double @double = "abc".ToPrimitive();
            Assert.AreEqual(0.0, @double);

            byte @byte = "abc".ToPrimitive();
            Assert.AreEqual((byte)0, @byte);

            sbyte @sbyte = "abc".ToPrimitive();
            Assert.AreEqual((sbyte)0, @sbyte);

            char @char = "abc".ToPrimitive();
            Assert.AreEqual(default(char), @char);
        }

        [TestMethod]
        public void 转可空数值_非法字符串返回null()
        {
            int? @int = "abc".ToPrimitive();
            Assert.AreEqual(null, @int);

            long? @long = "abc".ToPrimitive();
            Assert.AreEqual(null, @long);

            decimal? @decimal = "abc".ToPrimitive();
            Assert.AreEqual(null, @decimal);

            double? @double = "abc".ToPrimitive();
            Assert.AreEqual(null, @double);

            float? @float = "abc".ToPrimitive();
            Assert.AreEqual(null, @float);
        }

        [TestMethod]
        public void 空串和null_返回默认或null()
        {
            string? nullStr = null;

            int? nullableFromNull = nullStr.ToPrimitive();
            Assert.AreEqual(null, nullableFromNull);

            int valueFromNull = nullStr.ToPrimitive();
            Assert.AreEqual(0, valueFromNull);

            int? nullableFromEmpty = "".ToPrimitive();
            Assert.AreEqual(null, nullableFromEmpty);

            int valueFromEmpty = "".ToPrimitive();
            Assert.AreEqual(0, valueFromEmpty);
        }

        [TestMethod]
        public void 转Guid()
        {
            var g = Guid.NewGuid();

            Guid d = g.ToString().ToPrimitive();
            Assert.AreEqual(g, d);

            Guid d2 = "abc".ToPrimitive();
            Assert.AreEqual(default(Guid), d2);

            Guid? d3 = "abc".ToPrimitive();
            Assert.AreEqual(null, d3);
        }
    }
}