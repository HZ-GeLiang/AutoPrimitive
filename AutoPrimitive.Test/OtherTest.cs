using AutoPrimitive.Test.ExtensionMethods;

namespace AutoPrimitive.Test
{
    [TestClass]
    public class OtherTest
    {
        [TestMethod]
        public void ToDictionary()
        {
            {
                Dictionary<string, int> dict = new();
                var list = new List<UserProduct>() { new UserProduct() { ProductId = 1 } };
                list.ToDictionarySameKeyContinue(a => a.ProductId.ToPrimitive(), a => a.SortNo);

                Assert.AreEqual(dict.Keys.First(), "1");
            }

            {
                Dictionary<long, int> dict = new();
                var list = new List<UserProduct>() { new UserProduct() { ProductId = 1 } };
                list.ToDictionarySameKeyContinue(a => a.ProductId.ToPrimitive(), a => a.SortNo);

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