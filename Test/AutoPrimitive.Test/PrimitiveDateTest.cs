using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPrimitive.Test
{
    [TestClass]
    public class PrimitiveDateTest
    {
        [TestMethod]
        public void Test_DateTime()
        {
            var d1 = new DateTime(2024, 1, 1, 1, 1, 1, 111);

            Assert.AreEqual("2024-01-01 01:01:01", d1.ToPrimitive());
            Assert.AreEqual("2024-01-01", d1.ToPrimitive("yyyy-MM-dd"));

            Assert.AreEqual(new DateOnly(2024, 1, 1), d1.ToPrimitive());
            Assert.AreEqual(new TimeOnly(1, 1, 1, 111), d1.ToPrimitive());
        }

        [TestMethod]
        public void Test_DateOnly()
        {
            var d1 = new DateOnly(2024, 1, 1);

            Assert.AreEqual("2024-01-01", d1.ToPrimitive());
            Assert.AreEqual("2024-01-01 00:00:00", d1.ToPrimitive("yyyy-MM-dd HH:mm:ss"));
            Assert.AreEqual(new DateTime(2024, 1, 1), d1.ToPrimitive());
        }
    }
}
