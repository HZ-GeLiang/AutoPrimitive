using AutoPrimitive.Test.enums;
using AutoPrimitive.Types;

namespace AutoPrimitive.Test
{
    [TestClass]
    public class PrimitiveEnumTest
    {
        [TestMethod]
        public void Test_枚举值的转换()
        {
            //DayOfWeek item = "Friday".ToPrimitive();//无法通过 implicit 来实现

            {
                MyDayOfWeek item = (MyDayOfWeek)(int)DayOfWeek.Friday.ToPrimitive();//太麻烦了
                Assert.AreEqual(item, MyDayOfWeek.Friday);
            }

            {
                DayOfWeek item = "Friday".ToPrimitive<DayOfWeek>();
                Assert.AreEqual(DayOfWeek.Friday, item.ToPrimitive());
            }
            {
                DayOfWeek item = 5.ToPrimitive<DayOfWeek>();
                Assert.AreEqual(DayOfWeek.Friday, item.ToPrimitive());
            }

            {
                MyDayOfWeek item = DayOfWeek.Friday.ToPrimitive<MyDayOfWeek>();
                Assert.AreEqual(item, MyDayOfWeek.Friday);
            }

            {
                BankEnum item = ChinaBankEnum.中国工商银行.ToPrimitive<BankEnum>();
                //Assert.AreEqual(item.ToPrimitive(), BankEnum.CMSB.ToPrimitive()); // core 1.0 下这个成立
                Assert.AreEqual(item.ToPrimitive(), BankEnum.ICBC.ToPrimitive()); //因为支持了 DescriptionAttribute
            }
        }

        [TestMethod]
        public void Test_枚举值的比较()
        {
            {
                //PrimitiveEnum 之前的单元测试
                Assert.AreEqual(true, new Nullable<int>(5) == (PrimitiveEnum)DayOfWeek.Friday);
                Assert.AreEqual(true, 5 == (PrimitiveEnum)DayOfWeek.Friday);

                Assert.AreEqual(true, new Nullable<long>(5) == (PrimitiveEnum)DayOfWeek.Friday);
                Assert.AreEqual(true, 5L == (PrimitiveEnum)DayOfWeek.Friday);

                Assert.AreEqual(false, new Nullable<sbyte>() == (PrimitiveEnum)DayOfWeek.Friday);

                Assert.AreEqual(5, (PrimitiveEnum)DayOfWeek.Friday);
                Assert.AreEqual(5, new PrimitiveEnum(DayOfWeek.Friday));

                Assert.AreEqual(5L, (PrimitiveEnum)DayOfWeek.Friday);
                Assert.AreEqual(5L, new PrimitiveEnum(DayOfWeek.Friday));

                Assert.AreEqual(true, DayOfWeek.Friday == (PrimitiveEnum)DayOfWeek.Friday);
                Assert.AreEqual(true, ((PrimitiveEnum)DayOfWeek.Friday) == ((PrimitiveEnum)DayOfWeek.Friday));

                Assert.AreEqual(false, DayOfWeek.Friday != (PrimitiveEnum)DayOfWeek.Friday);
                Assert.AreEqual(false, ((PrimitiveEnum)DayOfWeek.Friday) != ((PrimitiveEnum)DayOfWeek.Friday));

            }


            Assert.AreEqual(true, new Nullable<int>(5) == DayOfWeek.Friday.ToPrimitive());
            Assert.AreEqual(true, 5 == DayOfWeek.Friday.ToPrimitive());

            Assert.AreEqual(true, new Nullable<long>(5) == DayOfWeek.Friday.ToPrimitive());
            Assert.AreEqual(true, 5L == DayOfWeek.Friday.ToPrimitive());

            Assert.AreEqual(false, new Nullable<sbyte>() == DayOfWeek.Friday.ToPrimitive());

            Assert.AreEqual(5, DayOfWeek.Friday.ToPrimitive());
            Assert.AreEqual(5, new PrimitiveEnum(DayOfWeek.Friday.ToPrimitive()));

            Assert.AreEqual(5L, DayOfWeek.Friday.ToPrimitive());
            Assert.AreEqual(5L, new PrimitiveEnum(DayOfWeek.Friday));

            Assert.AreEqual(true, DayOfWeek.Friday == (PrimitiveEnum)DayOfWeek.Friday.ToPrimitive());
            Assert.AreEqual(true, ((PrimitiveEnum)DayOfWeek.Friday) == (DayOfWeek.Friday).ToPrimitive());

            Assert.AreEqual(false, DayOfWeek.Friday != (PrimitiveEnum)DayOfWeek.Friday.ToPrimitive());
            Assert.AreEqual(false, ((PrimitiveEnum)DayOfWeek.Friday) != (DayOfWeek.Friday.ToPrimitive()));

            Assert.AreEqual("Friday", DayOfWeek.Friday.ToPrimitive());
        }
    }
}
