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
        public void Test_字符串转换类型()
        {
            {
                string s = "true";
                bool d = s.ToPrimitive();
                Assert.AreEqual(d, true);
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
        }
    }
}
