using System.Text;

namespace AutoPrimitive.Test
{
    [TestClass]
    public class PrimitiveDateTest
    {
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
