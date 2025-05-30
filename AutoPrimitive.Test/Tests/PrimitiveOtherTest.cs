﻿namespace AutoPrimitive.Test.Tests
{
    [TestClass]
    public class PrimitiveOtherTest
    {
        [TestMethod]
        public void 转换类型为自己()
        {
            {
                string s = "123";
                string d = s.ToPrimitive();
                Assert.AreEqual(d, s);
            }

            {
                int s = 123;
                int d = s.ToPrimitive();
                Assert.AreEqual(d, s);
            }
        }
    }
}