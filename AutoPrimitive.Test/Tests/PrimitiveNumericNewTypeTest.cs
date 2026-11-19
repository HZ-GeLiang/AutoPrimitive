using System.Numerics;

namespace AutoPrimitive.Test.Tests
{
    [TestClass]
    public class PrimitiveNumericNewTypeTest
    {
        [TestMethod]
        public void 数值转BigInteger()
        {
            BigInteger fromInt = 123.ToPrimitive();
            Assert.AreEqual(BigInteger.Parse("123"), fromInt);

            BigInteger fromLong = 123456789012L.ToPrimitive();
            Assert.AreEqual(BigInteger.Parse("123456789012"), fromLong);

            BigInteger fromDecimal = 123M.ToPrimitive();
            Assert.AreEqual(BigInteger.Parse("123"), fromDecimal);

            BigInteger fromULong = 123456789012UL.ToPrimitive();
            Assert.AreEqual(BigInteger.Parse("123456789012"), fromULong);
        }

        [TestMethod]
        public void 可空数值转BigInteger_非空与为空()
        {
            int? source = 123;
            BigInteger? fromNullable = source.ToPrimitive();
            Assert.AreEqual(BigInteger.Parse("123"), fromNullable);

            int? empty = null;
            BigInteger? fromNull = empty.ToPrimitive();
            Assert.IsNull(fromNull);
        }

#if NET5_0_OR_GREATER
        [TestMethod]
        public void 数值转Half()
        {
            Half fromInt = 5.ToPrimitive();
            Assert.AreEqual(Half.Parse("5"), fromInt);

            Half fromDouble = 1.5.ToPrimitive();
            Assert.AreEqual(Half.Parse("1.5"), fromDouble);
        }

        [TestMethod]
        public void 可空数值转Half_非空与为空()
        {
            double? source = 1.5;
            Half? fromNullable = source.ToPrimitive();
            Assert.AreEqual(Half.Parse("1.5"), fromNullable);

            double? empty = null;
            Half? fromNull = empty.ToPrimitive();
            Assert.IsNull(fromNull);
        }
#endif

#if NET7_0_OR_GREATER
        [TestMethod]
        public void 数值转Int128与UInt128()
        {
            Int128 fromLong = 1234567890123456789L.ToPrimitive();
            Assert.AreEqual(Int128.Parse("1234567890123456789"), fromLong);

            UInt128 fromULong = 12345678901234567890UL.ToPrimitive();
            Assert.AreEqual(UInt128.Parse("12345678901234567890"), fromULong);
        }

        [TestMethod]
        public void 可空数值转Int128与UInt128_非空与为空()
        {
            long? source = 1234567890123456789L;
            Int128? fromNullable = source.ToPrimitive();
            Assert.AreEqual(Int128.Parse("1234567890123456789"), fromNullable);

            ulong? empty = null;
            UInt128? fromNull = empty.ToPrimitive();
            Assert.IsNull(fromNull);
        }
#endif

        [TestMethod]
        public void 默认值变体_数值转新类型()
        {
            BigInteger bi = 123.ToPrimitive(BigInteger.Zero);
            Assert.AreEqual(BigInteger.Parse("123"), bi);

            int? nullableSource = 123;
            BigInteger? biNullable = nullableSource.ToPrimitive(BigInteger.Zero);
            Assert.AreEqual(BigInteger.Parse("123"), biNullable);

#if NET5_0_OR_GREATER
            Half h = 5.ToPrimitive(default(Half));
            Assert.AreEqual(Half.Parse("5"), h);
#endif

#if NET7_0_OR_GREATER
            Int128 i = 123.ToPrimitive(default(Int128));
            Assert.AreEqual(Int128.Parse("123"), i);

            UInt128 u = 123U.ToPrimitive(default(UInt128));
            Assert.AreEqual(UInt128.Parse("123"), u);
#endif
        }

        [TestMethod]
        public void BigInteger反向转基础数值()
        {
            BigInteger source = BigInteger.Parse("123456789012");

            long l = source.ToPrimitive();
            Assert.AreEqual(123456789012L, l);

            double d = source.ToPrimitive();
            Assert.AreEqual(123456789012.0, d);

            decimal m = source.ToPrimitive();
            Assert.AreEqual(123456789012M, m);

            string s = source.ToPrimitive();
            Assert.AreEqual("123456789012", s);
        }

        [TestMethod]
        public void BigInteger反向转其余数值类型()
        {
            BigInteger source = BigInteger.Parse("123456");

            int i = source.ToPrimitive();
            Assert.AreEqual(123456, i);

            ulong ul = source.ToPrimitive();
            Assert.AreEqual(123456UL, ul);

            float f = source.ToPrimitive();
            Assert.AreEqual(123456f, f);

            byte b = BigInteger.Parse("20").ToPrimitive();
            Assert.AreEqual((byte)20, b);
        }

        [TestMethod]
        public void 可空BigInteger反向_非空与为空()
        {
            BigInteger? source = BigInteger.Parse("123");
            long? l = source.ToPrimitive();
            Assert.AreEqual(123L, l);

            BigInteger? empty = null;
            long? fromNull = empty.ToPrimitive();
            Assert.IsNull(fromNull);
        }

#if NET5_0_OR_GREATER
        [TestMethod]
        public void Half反向转基础数值()
        {
            Half source = Half.Parse("5");

            int i = source.ToPrimitive();
            Assert.AreEqual(5, i);

            double d = source.ToPrimitive();
            Assert.AreEqual(5.0, d);

            string s = source.ToPrimitive();
            Assert.AreEqual("5", s);
        }

        [TestMethod]
        public void 可空Half反向_非空与为空()
        {
            Half? source = Half.Parse("5");
            double? d = source.ToPrimitive();
            Assert.AreEqual(5.0, d);

            Half? empty = null;
            double? fromNull = empty.ToPrimitive();
            Assert.IsNull(fromNull);
        }
#endif

#if NET7_0_OR_GREATER
        [TestMethod]
        public void Int128与UInt128反向转基础数值()
        {
            Int128 iSource = Int128.Parse("1234567890123456789");
            long l = iSource.ToPrimitive();
            Assert.AreEqual(1234567890123456789L, l);

            UInt128 uSource = UInt128.Parse("12345678901234567890");
            ulong ul = uSource.ToPrimitive();
            Assert.AreEqual(12345678901234567890UL, ul);
        }

        [TestMethod]
        public void Int128与UInt128反向转字符串与浮点()
        {
            Int128 iSource = Int128.Parse("123");
            double d = iSource.ToPrimitive();
            Assert.AreEqual(123.0, d);
            string s = iSource.ToPrimitive();
            Assert.AreEqual("123", s);

            UInt128 uSource = UInt128.Parse("123");
            string us = uSource.ToPrimitive();
            Assert.AreEqual("123", us);
        }
#endif

        [TestMethod]
        public void 默认值变体_大数值反向转基础数值()
        {
            long l = BigInteger.Parse("123").ToPrimitive(-1L);
            Assert.AreEqual(123L, l);

#if NET5_0_OR_GREATER
            double d = Half.Parse("5").ToPrimitive(-1.0);
            Assert.AreEqual(5.0, d);
#endif
        }

        [TestMethod]
        public void 默认值变体_可空大数值反向_非空与为空()
        {
            BigInteger? source = BigInteger.Parse("123");
            long? l = source.ToPrimitive(-1L);
            Assert.AreEqual(123L, l);

            BigInteger? empty = null;
            long? fromNull = empty.ToPrimitive(-1L);
            Assert.IsNull(fromNull);
        }

        [TestMethod]
        public void 默认值变体_大数值反向_越界回退默认值()
        {
            //BigInteger 超出 short 范围, 显式转换抛 OverflowException, 由 h<> 回退默认值
            short s = BigInteger.Parse("123456789012").ToPrimitive((short)-1);
            Assert.AreEqual((short)-1, s);
        }
    }
}
