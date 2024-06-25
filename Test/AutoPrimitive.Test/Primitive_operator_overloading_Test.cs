namespace AutoPrimitive.Test
{
    [TestClass]
    public class Primitive_operator_overloading_Test
    {
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
    }
}
