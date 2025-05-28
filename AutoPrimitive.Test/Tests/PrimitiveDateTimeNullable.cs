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
    }
}