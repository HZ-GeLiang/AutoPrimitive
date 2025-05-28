using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AutoPrimitive.Test.Tests
{
    internal class MethodClass
    {
        public string GetList_1(int id)
        {
            return id.ToPrimitive();
        }

        public static string GetList_2(int id)
        {
            return id.ToPrimitive();
        }
    }
}
