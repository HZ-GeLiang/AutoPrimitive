using System.Text;

namespace AutoPrimitive.Test.Tests
{

    [TestClass]
    public class PrimitiveDateOnlyTest
    {
        [TestMethod]
        public void DateOnly()
        {
            var d1 = new DateOnly(2024, 1, 1);

            Assert.AreEqual("2024-01-01", d1.ToPrimitive());
            Assert.AreEqual("2024-01-01 00:00:00", d1.ToPrimitive("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual(new DateTime(2024, 1, 1), d1.ToPrimitive());
        }
    }
}