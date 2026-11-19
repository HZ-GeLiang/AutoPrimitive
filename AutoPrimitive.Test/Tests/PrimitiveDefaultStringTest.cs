using System.Numerics;

namespace AutoPrimitive.Test.Tests
{
    [TestClass]
    public class PrimitiveDefaultStringTest
    {
        [TestMethod]
        public void 字符串转数值_成功解析使用解析值()
        {
            int i = "123".ToPrimitive(-1);
            Assert.AreEqual(123, i);

            long l = "123456789012".ToPrimitive(-1L);
            Assert.AreEqual(123456789012L, l);

            double d = "123.45".ToPrimitive(-1.0);
            Assert.AreEqual(123.45, d);

            decimal m = "123.456".ToPrimitive(-1M);
            Assert.AreEqual(123.456M, m);
        }

        [TestMethod]
        public void 字符串转数值_解析失败回退默认值()
        {
            int i = "abc".ToPrimitive(-1);
            Assert.AreEqual(-1, i);

            long l = "abc".ToPrimitive(-1L);
            Assert.AreEqual(-1L, l);

            double d = "abc".ToPrimitive(-1.0);
            Assert.AreEqual(-1.0, d);

            decimal m = "abc".ToPrimitive(-1M);
            Assert.AreEqual(-1M, m);
        }

        [TestMethod]
        public void 字符串转Bool_失败回退默认值()
        {
            bool t = "true".ToPrimitive(false);
            Assert.AreEqual(true, t);

            bool f = "abc".ToPrimitive(true);
            Assert.AreEqual(true, f);
        }

        [TestMethod]
        public void 字符串转DateTime_失败回退默认值()
        {
            var now = new DateTime(2024, 1, 1);
            DateTime result = "aaa".ToPrimitive(now);
            Assert.AreEqual(now, result);
        }

        [TestMethod]
        public void 字符串转TimeSpan_成功与回退()
        {
            TimeSpan parsed = "01:02:03".ToPrimitive(TimeSpan.Zero);
            Assert.AreEqual(new TimeSpan(1, 2, 3), parsed);

            TimeSpan fallback = "abc".ToPrimitive(TimeSpan.FromMinutes(5));
            Assert.AreEqual(TimeSpan.FromMinutes(5), fallback);
        }

        [TestMethod]
        public void 字符串转DateTimeOffset_成功与回退()
        {
            DateTimeOffset parsed = "2024-04-06 23:59:59+08:00".ToPrimitive(DateTimeOffset.MinValue);
            Assert.AreEqual(DateTimeOffset.Parse("2024-04-06 23:59:59+08:00"), parsed);

            var fallbackDefault = new DateTimeOffset(2024, 1, 1, 0, 0, 0, TimeSpan.Zero);
            DateTimeOffset fallback = "abc".ToPrimitive(fallbackDefault);
            Assert.AreEqual(fallbackDefault, fallback);
        }

        [TestMethod]
        public void 字符串转BigInteger_成功与回退()
        {
            BigInteger parsed = "123456789012345678901234567890".ToPrimitive(BigInteger.Zero);
            Assert.AreEqual(BigInteger.Parse("123456789012345678901234567890"), parsed);

            BigInteger fallback = "abc".ToPrimitive(BigInteger.Parse("999"));
            Assert.AreEqual(BigInteger.Parse("999"), fallback);
        }

#if NET5_0_OR_GREATER
        [TestMethod]
        public void 字符串转Half_成功与回退()
        {
            Half parsed = "1.5".ToPrimitive(default(Half));
            Assert.AreEqual(Half.Parse("1.5"), parsed);

            Half fallback = "abc".ToPrimitive(Half.Parse("2"));
            Assert.AreEqual(Half.Parse("2"), fallback);
        }
#endif

#if NET6_0_OR_GREATER
        [TestMethod]
        public void 字符串转TimeOnly_成功与回退()
        {
            TimeOnly parsed = "23:59:59".ToPrimitive(TimeOnly.MinValue);
            Assert.AreEqual(TimeOnly.Parse("23:59:59"), parsed);

            TimeOnly fallback = "abc".ToPrimitive(new TimeOnly(1, 2, 3));
            Assert.AreEqual(new TimeOnly(1, 2, 3), fallback);
        }
#endif

#if NET7_0_OR_GREATER
        [TestMethod]
        public void 字符串转Int128_成功与回退()
        {
            Int128 parsed = "123456789012345678901234567890".ToPrimitive(default(Int128));
            Assert.AreEqual(Int128.Parse("123456789012345678901234567890"), parsed);

            Int128 fallback = "abc".ToPrimitive(Int128.Parse("999"));
            Assert.AreEqual(Int128.Parse("999"), fallback);
        }

        [TestMethod]
        public void 字符串转UInt128_成功与回退()
        {
            UInt128 parsed = "123456789012345678901234567890".ToPrimitive(default(UInt128));
            Assert.AreEqual(UInt128.Parse("123456789012345678901234567890"), parsed);

            UInt128 fallback = "abc".ToPrimitive(UInt128.Parse("999"));
            Assert.AreEqual(UInt128.Parse("999"), fallback);
        }
#endif
    }
}
