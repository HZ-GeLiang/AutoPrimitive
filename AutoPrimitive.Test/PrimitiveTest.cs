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
        public void 方法参数()
        {
            int a = 3;
            Assert.AreEqual("3", MethodClass.GetList_2(a.ToPrimitive()));
            Assert.AreEqual("3", new MethodClass().GetList_1(a.ToPrimitive()));

        }
    }
}