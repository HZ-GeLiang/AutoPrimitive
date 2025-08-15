using AutoPrimitive;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp_Nfx_4_5_Std1_0
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string timeString = "Fri Aug 15 2025 08:07:32 GMT+0800 (香港标准时间)";
            DateTime dt = timeString.ToPrimitive();
            Console.WriteLine(dt);
        }
    }
}
