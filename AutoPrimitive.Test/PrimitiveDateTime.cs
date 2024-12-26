using System.Text;

namespace AutoPrimitive.Test
{
    public class Test_Date
    {
        public DateTime Now { get; set; }
    }

    [TestClass]
    public class PrimitiveDateTimeTest
    {
        [TestMethod]
        public void Test_对象类型()
        {
            var obj = new Test_Date();
            obj.Now = new DateTime(2024, 11, 11, 11, 11, 11, 111);
            DateTime dt = obj.Now.ToPrimitive("yyyy-MM-dd");
            Assert.AreEqual(new DateTime(2024, 11, 11), dt);
        }

        [TestMethod]
        public void Test_DateTime_JS时间戳_DateTime()
        {
            //2021年10月18日17时5分55秒0毫秒
            DateTime dt = new DateTime(2021, 10, 18, 17, 5, 55, 0);
            {
                //字符串
                {
                    string ts = 1634547955000.ToString();
                    DateTime d = ts.ToPrimitive();
                    Assert.AreEqual(d, dt);
                }

                {
                    string ts = 1634547955.ToString();
                    DateTime d = ts.ToPrimitive();
                    Assert.AreEqual(d, dt);
                }
            }

            {
                //long
                {
                    long ts = 1634547955000;
                    DateTime d = ts.ToPrimitive();
                    Assert.AreEqual(d, dt);
                }

                {
                    long ts = 1634547955;
                    DateTime d = ts.ToPrimitive();
                    Assert.AreEqual(d, dt);
                }
            }

            {
                //int
                {
                    int ts = 1634547955;
                    DateTime d = ts.ToPrimitive();
                    Assert.AreEqual(d, dt);
                }
            }
        }

        [TestMethod]
        public void Test_DateTime_JS时间戳_DateTime_Nullable()
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
        public void Test_DateTime()
        {
            var time = new DateTime(2024, 1, 1, 1, 1, 1, 111);

            {
                DateTime d1 = time;

                Assert.AreEqual("2024-01-01 01:01:01.111", d1.ToPrimitive());
                Assert.AreEqual("2024-01-01", d1.ToPrimitive("yyyy-MM-dd"));

                Assert.AreEqual(new DateOnly(2024, 1, 1), d1.ToPrimitive());
                Assert.AreEqual(new TimeOnly(1, 1, 1, 111), d1.ToPrimitive());

                var txt1 = new StringBuilder().Append($"CreateDate<'{d1.ToPrimitive()}'").ToString();
                var txt2 = new StringBuilder().AppendFormat("CreateDate<'{0}'", d1.ToPrimitive()).ToString();

                Assert.AreEqual(txt1, "CreateDate<'2024-01-01 01:01:01.111'");
                Assert.AreEqual(txt1, txt2);
            }

            {
                DateTime? d1 = time;

                Assert.AreEqual("2024-01-01 01:01:01.111", d1.ToPrimitive());
                Assert.AreEqual("2024-01-01", d1.ToPrimitive("yyyy-MM-dd"));

                Assert.AreEqual(new DateOnly(2024, 1, 1), d1.ToPrimitive());
                Assert.AreEqual(new TimeOnly(1, 1, 1, 111), d1.ToPrimitive());

                var txt1 = new StringBuilder().Append($"CreateDate<'{d1.ToPrimitive()}'").ToString();
                var txt2 = new StringBuilder().AppendFormat("CreateDate<'{0}'", d1.ToPrimitive()).ToString();

                Assert.AreEqual(txt1, "CreateDate<'2024-01-01 01:01:01.111'");
                Assert.AreEqual(txt1, txt2);
            }
        }

    }
}