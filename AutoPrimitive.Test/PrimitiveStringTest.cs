namespace AutoPrimitive.Test
{

    public class Test01
    {
        public DateTime? Start { get; set; }
    }

    [TestClass]
    public class PrimitiveStringTest
    {
        [TestMethod]
        public void 日期比较()
        {
            var item = new Test01();

            var result3 = "2025-01-01".ToPrimitive() == item.Start;
            var result = item.Start == "2025-01-01".ToPrimitive();
            var result2 = item.Start.Value == "2025-01-01".ToPrimitive();
        }


        [TestMethod]
        public void 转可空int()
        {

            {
                string s = null;
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
    }
}