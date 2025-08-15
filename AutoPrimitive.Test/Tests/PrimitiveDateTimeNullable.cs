using System.Text;

namespace AutoPrimitive.Test.Tests
{
    [TestClass]
    public class PrimitiveDateTimeNullableTest
    {
        [TestMethod]
        public void datetime_nullable()
        {
            {
                DateTime? d1 = new DateTime(2024, 1, 1, 1, 1, 1, 111);

                string str = d1.ToPrimitive("yyyy-MM-dd");
                Assert.AreEqual(str, "2024-01-01");

                {
                    DateOnly? DateOnly_nullable = d1.ToPrimitive();
                    TimeOnly? TimeOnly_nullable = d1.ToPrimitive();

                    Assert.AreEqual(DateOnly_nullable, new DateOnly(2024, 1, 1));
                    Assert.AreEqual(TimeOnly_nullable, new TimeOnly(1, 1, 1, 111));
                }
                {
                    DateOnly DateOnly = d1.ToPrimitive();
                    TimeOnly TimeOnly = d1.ToPrimitive();

                    Assert.AreEqual(DateOnly, new DateOnly(2024, 1, 1));
                    Assert.AreEqual(TimeOnly, new TimeOnly(1, 1, 1, 111));
                }
            }

            {
                DateTime? d1 = null;
                string str = d1.ToPrimitive("yyyy-MM-dd");
                Assert.AreEqual(str, null);
                {
                    DateOnly? DateOnly_nullable = d1.ToPrimitive();
                    TimeOnly? TimeOnly_nullable = d1.ToPrimitive();

                    Assert.AreEqual(DateOnly_nullable, null);
                    Assert.AreEqual(TimeOnly_nullable, null);
                }

                {
                    DateOnly DateOnly = d1.ToPrimitive();
                    TimeOnly TimeOnly = d1.ToPrimitive();

                    Assert.AreEqual(DateOnly, default);
                    Assert.AreEqual(TimeOnly, default);
                }
            }
        }

        [TestMethod]
        public void DateTime_JS时间戳_DateTime_Nullable()
        {
            //2021年10月18日17时5分55秒0毫秒
            DateTime? dt = new DateTime(2021, 10, 18, 17, 5, 55, 0);
            {
                //字符串
                {
                    string ts = 1634547955000.ToString();
                    DateTime? d = ts.ToPrimitive();
                    Assert.AreEqual(d, dt);
                }

                {
                    string ts = 1634547955.ToString();
                    DateTime? d = ts.ToPrimitive();
                    Assert.AreEqual(d, dt);
                }
            }

            {
                //long
                {
                    long ts = 1634547955000;
                    DateTime? d = ts.ToPrimitive();
                    Assert.AreEqual(d, dt);
                }

                {
                    long ts = 1634547955;
                    DateTime? d = ts.ToPrimitive();
                    Assert.AreEqual(d, dt);
                }
            }

            {
                //int
                {
                    int ts = 1634547955;
                    DateTime? d = ts.ToPrimitive();
                    Assert.AreEqual(d, dt);
                }
            }
        }

        [TestMethod]
        public void DateTime_JSDate对象_DateTime_Nullable()
        {

            DateTime? dt = new DateTime(2025, 08, 22, 08, 07, 32, 0);
            {
                DateTime d = "Fri Aug 22 2025 08:07:32 GMT+0800 (香港标准时间)".ToPrimitive();
                Assert.AreEqual(d, dt);
            }

            {
                DateTime d = "Fri Aug 22 2025 08:07:32 GMT+0800 (中国标准时间)".ToPrimitive();
                Assert.AreEqual(d, dt);
            }

            {
                DateTime d = "Fri Aug 22 2025 08:07:32 GMT+0800 ".ToPrimitive();
                Assert.AreEqual(d, dt);
            }

            {
                DateTime d = "Fri Aug 22 2025 08:07:32 GMT+0800".ToPrimitive();
                Assert.AreEqual(d, dt);
            }


            {
                DateTime d = "Fri Aug 22 2025 08:07:32".ToPrimitive();
                Assert.AreEqual(d, dt);
            }


            {
                DateTime d = "Fri Aug 22 2025 08:07:32 GMT+0700".ToPrimitive();
                DateTime? dt2 = new DateTime(2025, 08, 22, 09, 07, 32, 0);
                Assert.AreEqual(d, dt2);
            }
        }
    }
}