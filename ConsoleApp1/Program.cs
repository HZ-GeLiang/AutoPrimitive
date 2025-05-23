using AutoPrimitive;

namespace ConsoleApp1
{
    internal class Program
    {
        static void Main(string[] args)
        {

            {
                string s = null;
                int? d = s.ToPrimitive();

            }

            {
                string s = "1";
                int? d = s.ToPrimitive();

            }

            {
                string s = "";
                int? d = s.ToPrimitive();

            }

            {
                string s = "a";
                int? d = s.ToPrimitive();

            }


        }
    }
}
