using AutoPrimitive.Utils;

namespace AutoPrimitive.Test
{
    [TestClass]
    public class OtherTest
    {

        [TestMethod]
        public void test()
        {
            var strNumber = "4E-06";
            var d1 = 0.000004d;
            double d2 = strNumber.ToPrimitive();
            Assert.AreEqual(d1, d2);
            //var aa = MathUtil.ToInt32("aa");
        }


        [TestMethod]
        public void ToDictionary()
        {
            //todo: 字典的key 没法自动转换
            var list = new List<UserProduct>() { new UserProduct() { ProductId = 1 } };
            {
                Dictionary<string, int> dict = new();
                dict = list.ToDictionary(a => (string)a.ProductId.ToPrimitive(), a => a.SortNo);
                Assert.AreEqual(dict.Keys.First(), "1");
            }

            {
                Dictionary<long, int> dict = new();
                dict = list.ToDictionary(a => (long)a.ProductId.ToPrimitive(), a => a.SortNo);
                Assert.AreEqual(dict.Keys.First(), 1);
            }
        }


    }


    public class UserProduct
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int ProductId { get; set; }
        public int SortNo { get; set; }
    }

}