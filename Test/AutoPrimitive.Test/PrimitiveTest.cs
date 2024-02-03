using AutoPrimitive.Types;

namespace AutoPrimitive.Test
{
    [TestClass]
    public class PrimitiveTest
    {
        [TestMethod]
        public void 基础类型自动转()
        {
            //Assert.AreEqual(123.456M, (123.456M.ToPrimitive())); //object ,对比 object  会失败
            Assert.AreEqual("123.456", 123.456M.ToPrimitive());

            {
                //object 测试
                decimal v1 = 123.456M;
                var v2 = new Primitive<decimal>(123.456M);
                Assert.AreEqual(false, object.Equals(v1, v2));
                Assert.AreEqual(true, object.Equals(v2, v1));

                object v2_o = 123.456M.ToPrimitive();
                Assert.AreEqual(false, object.Equals(v1, v2_o));
                Assert.AreEqual(true, object.Equals(v2_o, v1));
            }

            Assert.AreEqual(123.456M, (decimal)(123.456M.ToPrimitive())); //需要类型转换
            Assert.AreEqual(123.456f, (float)(123.456M.ToPrimitive()));
            Assert.AreEqual(123.456M.ToPrimitive(), 123.456f); //放前面不需要
            Assert.AreEqual(123.456M.ToPrimitive(), "123.456");

            //Assert.AreEqual(123.456M.ToPrimitive(), 123);
            Assert.AreEqual((object.Equals(123.456M.ToPrimitive(), 123)) == true, true);
            Assert.AreEqual(true, object.Equals(123.456M.ToPrimitive(), 123));
        }

        [TestMethod]
        public void 基础类型自动转_可空_不为空()
        {

            Assert.AreEqual("123.456", ((decimal?)123.456M).ToPrimitive());

            {
                //object 测试
                decimal v1 = 123.456M;
                var v2 = new PrimitiveNullable<decimal?>(((decimal?)123.456M));
                Assert.AreEqual(false, object.Equals(v1, v2));
                Assert.AreEqual(true, object.Equals(v2, v1));

                object v2_o = 123.456M.ToPrimitive();
                Assert.AreEqual(false, object.Equals(v1, v2_o));
                Assert.AreEqual(true, object.Equals(v2_o, v1));
            }

            Assert.AreEqual(123.456M, (decimal)(((decimal?)123.456M).ToPrimitive())); //需要类型转换
            Assert.AreEqual(123.456f, (((decimal?)123.456M).ToPrimitive()));
            Assert.AreEqual(((decimal?)123.456M).ToPrimitive(), 123.456f); //放前面不需要
            Assert.AreEqual(((decimal?)123.456M).ToPrimitive(), "123.456");

            //Assert.AreEqual(((decimal?)123.456M).ToPrimitive(), 123);
            Assert.AreEqual((object.Equals(((decimal?)123.456M).ToPrimitive(), 123)) == true, true);
            Assert.AreEqual(true, object.Equals(((decimal?)123.456M).ToPrimitive(), 123));
        }

        [TestMethod]
        public void 基础类型自动转_可空_为空()
        {
            decimal? a1 = null;
            float? a2 = null;
            string? a3 = null;
            int? a4 = null;
            bool? a5 = null;


            decimal? val = null;
            var test = (string?)((val).ToPrimitive());

            Assert.AreEqual(null, val.ToPrimitive());
            Assert.AreEqual(val.ToPrimitive(), null);

            Assert.AreEqual(null, (decimal?)val.ToPrimitive());
            Assert.AreEqual((decimal?)val.ToPrimitive(), null);

            {
                //object 测试
                decimal? v1 = null;
                var v2 = new PrimitiveNullable<decimal?>(val);
                Assert.AreEqual(false, object.Equals(v1, v2));
                //Assert.AreEqual(true, object.Equals(v2, v1)); //没有进入到重写的Equals中.直接返回false (因为没有具体的变量, dynamic当做了object)

                //写法1
                Assert.AreEqual(true, object.Equals((decimal?)v2, v1));

                //写法2:
                decimal? v2_2 = new PrimitiveNullable<decimal?>(val);
                Assert.AreEqual(true, object.Equals(v2_2, v1));

            }

            Assert.AreEqual(a1, (decimal?)((val).ToPrimitive())); //需要类型转换
            Assert.AreEqual(a2, ((val).ToPrimitive()));
            Assert.AreEqual(val.ToPrimitive(), a2); //放前面不需要
            Assert.AreEqual(val.ToPrimitive(), a3);

            //Assert.AreEqual(val.ToPrimitive(), a4);//没有进入到重写的Equals中.直接返回false  (因为没有具体的变量, dynamic当做了object)
            //Assert.AreEqual(true, object.Equals(val.ToPrimitive(), a5));//没有进入到重写的Equals中.直接返回false  (因为没有具体的变量, dynamic当做了object)

            //写法1
            Assert.AreEqual((int?)val.ToPrimitive(), a4);
            Assert.AreEqual(true, object.Equals((bool?)val.ToPrimitive(), a5));

            //写法2:
            int? a4_1 = val.ToPrimitive();
            Assert.AreEqual(a4_1, a4);

            bool? a5_1 = val.ToPrimitive();
            Assert.AreEqual(true, object.Equals(a5_1, a5));
        }

        [TestMethod]
        public void Bool类型_转其他类型()
        {
            /*
                Assert.AreEqual(1, new PrimitiveBool(true));
                Assert.AreEqual(1L, (PrimitiveBool)true);
                Assert.AreEqual("True", new PrimitiveBool(true));

                Assert.AreEqual(0, (PrimitiveBool)false);
                Assert.AreEqual(0L, new PrimitiveBool(false));
                Assert.AreEqual("False", new PrimitiveBool(false));
           */

            var val_true = true;
            Assert.AreEqual(1, val_true.ToPrimitive());
            Assert.AreEqual(1L, val_true.ToPrimitive());
            Assert.AreEqual("True", val_true.ToPrimitive());

            var val_false = false;
            Assert.AreEqual(0, val_false.ToPrimitive());
            Assert.AreEqual(0L, val_false.ToPrimitive());
            Assert.AreEqual("False", val_false.ToPrimitive());
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

            //DayOfWeek f = "Friday".ToPrimitive();//无法通过 implicit 来实现

        }


        [TestMethod]
        public void Test_日期()
        {
            {
                //DateTime
                var d1 = new DateTime(2024, 1, 1, 1, 1, 1, 111);

                Assert.AreEqual("2024-01-01 01:01:01", d1.ToPrimitive());
                Assert.AreEqual("2024-01-01", d1.ToPrimitive("yyyy-MM-dd"));

                Assert.AreEqual(new DateOnly(2024, 1, 1), d1.ToPrimitive());
                Assert.AreEqual(new TimeOnly(1, 1, 1, 111), d1.ToPrimitive());
            }

            {
                //DateOnly
                var d1 = new DateOnly(2024, 1, 1);

                Assert.AreEqual("2024-01-01", d1.ToPrimitive());
                Assert.AreEqual("2024-01-01 00:00:00", d1.ToPrimitive("yyyy-MM-dd HH:mm:ss"));
                Assert.AreEqual(new DateTime(2024, 1, 1), d1.ToPrimitive());
            }

        }


        [TestMethod]
        public void Test_使用测试()
        {
            {
                int tax = 6;
                string dis = (0.01M * tax).ToPrimitive();
                Assert.AreEqual(dis, "0.06");
            }

            {
                string a = "12.456";
                string b = "0.456";
                string c_str = (((decimal)(a.ToPrimitive())) - ((decimal)(b.ToPrimitive()))).ToPrimitive();
                Assert.AreEqual(c_str, "12.000");
            }

            {
                var a = 12.1;
                string b = "0.456";
                var c = a.ToPrimitive() + b.ToPrimitive();
                //Assert.AreEqual(c, 12.556);
                Assert.AreEqual((object.Equals(c, 12.556)) == true, true);
            }
        }


        [TestMethod]
        public void Test_Add操作符()
        {
            {
                double a = 12.1;
                long b = 10L;
                var c = a.ToPrimitive() + b.ToPrimitive();
                //Assert.AreEqual(c, 22.1);
                Assert.AreEqual(object.Equals(c, 22.1d) == true, true);
            }
            {
                long a = 10L;
                double b = 12.1;
                var c = a.ToPrimitive() + b.ToPrimitive();
                //Assert.AreEqual(c, 22);
                Assert.AreEqual(object.Equals(c, 22) == true, true);
            }

            {
                var a = 12.1;
                var b = 0.456;
                var c = a.ToPrimitive() + b.ToPrimitive();
                //Assert.AreEqual(c, 12.556);
                Assert.AreEqual(object.Equals(c, 12.556) == true, true);
            }

            {
                var a = 12.1;
                string b = "0.456";
                var c = a.ToPrimitive() + b.ToPrimitive();
                //Assert.AreEqual(c, 12.556);
                Assert.AreEqual(object.Equals(c, 12.556) == true, true);
            }

        }

        [TestMethod]
        public void Test_转换类型为自己()
        {
            {
                string s = "123";
                string d = s.ToPrimitive();
                Assert.AreEqual(d, s);
            }

            {

                int s = 123;
                int d = s.ToPrimitive();
                Assert.AreEqual(d, s);
            }
        }

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