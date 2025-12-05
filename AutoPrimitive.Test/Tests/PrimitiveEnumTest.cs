using AutoPrimitive.Test.enums;

namespace AutoPrimitive.Test.Tests
{
    [TestClass]
    public class PrimitiveEnumTest
    {

        [TestMethod]
        public void 枚举值的转换()
        {
            //DayOfWeek item = "Friday".ToPrimitive();//无法通过 implicit 来实现

            {
                int value = (int)DayOfWeek.Monday;
                var week = value.ToPrimitive<DayOfWeek>(); //var 为dynamic?  不优雅
                Assert.AreEqual(DayOfWeek.Monday, week);
            }

            {
                MyDayOfWeek item = (MyDayOfWeek)(int)DayOfWeek.Friday.ToPrimitive();//太麻烦了
                var equal = item == MyDayOfWeek.Friday;
                Assert.AreEqual(equal, true);
                Assert.AreEqual(item, MyDayOfWeek.Friday);
            }

            {
                DayOfWeek item = "Friday".ToPrimitive<DayOfWeek>(); //只能这样写
                Assert.AreEqual(DayOfWeek.Friday, item.ToPrimitive());
            }

            {
                DayOfWeek item = 5.ToPrimitive<DayOfWeek>();  //只能这样写
                Assert.AreEqual(DayOfWeek.Friday, item.ToPrimitive());
            }

            {
                //是否转换成功
                DayOfWeek obj_enum1 = 3.ToPrimitive<DayOfWeek>();
                Assert.AreEqual(Enum.IsDefined<DayOfWeek>(obj_enum1), true);

                DayOfWeek obj_enum2 = 33.ToPrimitive<DayOfWeek>();
                Assert.AreEqual(Enum.IsDefined<DayOfWeek>(obj_enum2), false);

            }

            {
                MyDayOfWeek item = DayOfWeek.Friday.ToPrimitive<MyDayOfWeek>();  //转换为另一个枚举对象
                Assert.AreEqual(item, MyDayOfWeek.Friday);
            }

            {
                BankEnum item = ChinaBankEnum.中国工商银行.ToPrimitive<BankEnum>();
                //Assert.AreEqual(item.ToPrimitive(), BankEnum.CMSB.ToPrimitive()); // core 1.0 下这个成立
                Assert.AreEqual(item.ToPrimitive(), BankEnum.ICBC.ToPrimitive()); //因为支持了 DescriptionAttribute
            }
        }

        [TestMethod]
        public void 枚举值的比较()
        {
            {
                //PrimitiveEnum 之前的单元测试
                Assert.AreEqual(true, new int?(5) == (PrimitiveEnum)DayOfWeek.Friday);
                Assert.AreEqual(true, 5 == (PrimitiveEnum)DayOfWeek.Friday);

                Assert.AreEqual(true, new long?(5) == (PrimitiveEnum)DayOfWeek.Friday);
                Assert.AreEqual(true, 5L == (PrimitiveEnum)DayOfWeek.Friday);

                Assert.AreEqual(false, new sbyte?() == (PrimitiveEnum)DayOfWeek.Friday);

                Assert.AreEqual(5, (PrimitiveEnum)DayOfWeek.Friday);
                Assert.AreEqual(5, new PrimitiveEnum(DayOfWeek.Friday));

                Assert.AreEqual(5L, (PrimitiveEnum)DayOfWeek.Friday);
                Assert.AreEqual(5L, new PrimitiveEnum(DayOfWeek.Friday));

                Assert.AreEqual(true, DayOfWeek.Friday == (PrimitiveEnum)DayOfWeek.Friday);
                Assert.AreEqual(true, (PrimitiveEnum)DayOfWeek.Friday == (PrimitiveEnum)DayOfWeek.Friday);

                Assert.AreEqual(false, DayOfWeek.Friday != (PrimitiveEnum)DayOfWeek.Friday);
                Assert.AreEqual(false, (PrimitiveEnum)DayOfWeek.Friday != (PrimitiveEnum)DayOfWeek.Friday);
            }

            Assert.AreEqual(true, new int?(5) == DayOfWeek.Friday.ToPrimitive());
            Assert.AreEqual(true, 5 == DayOfWeek.Friday.ToPrimitive());

            Assert.AreEqual(true, new long?(5) == DayOfWeek.Friday.ToPrimitive());
            Assert.AreEqual(true, 5L == DayOfWeek.Friday.ToPrimitive());

            Assert.AreEqual(false, new sbyte?() == DayOfWeek.Friday.ToPrimitive());

            Assert.AreEqual(5, DayOfWeek.Friday.ToPrimitive());
            Assert.AreEqual(5, new PrimitiveEnum(DayOfWeek.Friday.ToPrimitive()));

            Assert.AreEqual(5L, DayOfWeek.Friday.ToPrimitive());
            Assert.AreEqual(5L, new PrimitiveEnum(DayOfWeek.Friday));

            Assert.AreEqual(true, DayOfWeek.Friday == (PrimitiveEnum)DayOfWeek.Friday.ToPrimitive());
            Assert.AreEqual(true, (PrimitiveEnum)DayOfWeek.Friday == DayOfWeek.Friday.ToPrimitive());

            Assert.AreEqual(false, DayOfWeek.Friday != (PrimitiveEnum)DayOfWeek.Friday.ToPrimitive());
            Assert.AreEqual(false, (PrimitiveEnum)DayOfWeek.Friday != DayOfWeek.Friday.ToPrimitive());

            Assert.AreEqual("Friday", DayOfWeek.Friday.ToPrimitive());
        }
    }
}