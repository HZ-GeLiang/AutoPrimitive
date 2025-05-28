namespace AutoPrimitive.Test
{
    /// <summary>
    /// 个人调试
    /// </summary>
    [TestClass]
    public class DebuggerTest
    {

        [TestMethod]
        public void 转换异常()
        {
            var numStr = ((long)int.MaxValue + 1).ToString();
            int num = numStr.ToPrimitive(); //异常返回 default
        }


        [TestMethod]
        public void 类实体赋值()
        {
            var list = new List<UserProduct>()
            {
                 new UserProduct(){ ID=1}
            };
            var list_Select = list.Select(a => new UserProduct2()
            {
                ID = a.ID.ToPrimitive()
            });
            Assert.AreEqual(list_Select.First().ID, "1");
        }

        [TestMethod]
        public void 科学计数法()
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

    public class UserProduct2
    {
        public string ID { get; set; }
    }


}