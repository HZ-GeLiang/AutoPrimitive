namespace AutoPrimitive.Test
{
    [TestClass]
    public class PrimitiveTryCatchTest
    {
        [TestMethod]
        public void 类型转换失败_使用默认值()
        {
            long val = (long)int.MaxValue + 1;
            int result = val.ToPrimitive(-1);
            Assert.AreEqual(result, -1);
        }
    }
}