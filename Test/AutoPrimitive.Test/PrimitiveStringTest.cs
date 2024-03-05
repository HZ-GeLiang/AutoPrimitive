using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPrimitive.Test
{
    [TestClass]
    public class PrimitiveStringTest
    {
        [TestMethod]
        public void Test转Bool()
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
        public void Test转日期()
        {
            var str = "2024年1月";
            DateTime d = str.ToPrimitive();
            Assert.AreEqual(d, new DateTime(2024, 1, 1));
        }
    }
}
