
namespace AutoPrimitive.Test
{
    [TestClass]
    public class OtherTest
    {
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