namespace AutoPrimitive.Test.Tests
{
    [TestClass]
    public class PrimitiveBoolTest
    {
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
        public void js值转Bool类型()
        {
            bool bool1 = "true".ToPrimitive();
            bool bool2 = "false".ToPrimitive();
            Assert.AreEqual(bool1, true);
            Assert.AreEqual(bool2, false);
        }
    }
}