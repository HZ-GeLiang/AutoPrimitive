namespace AutoPrimitive.Test
{
    [TestClass]
    public class 类型转换失败_使用默认值
    {
        [TestMethod]
        public void long_To_other()
        {
            {
                long val = (long)int.MaxValue + 1;
                int result = val.ToPrimitive(-1);
                Assert.AreEqual(result, -1);
            }

            {
                long val = (long)int.MaxValue + 1;
                Int16 result = val.ToPrimitive(-1);
                Assert.AreEqual(result, Convert.ToInt16(-1));
            }
        }

        [TestMethod]
        public void string_To_other()
        {
            {
                string val = "2147483648";
                int result = val.ToPrimitive(-1);
                Assert.AreEqual(result, -1);
            }

            {
                var now = new DateTime(2024, 1, 1);
                string val = "aaa";
                DateTime result = val.ToPrimitive(now);
                Assert.AreEqual(result, now);
            }
        }
    }
}