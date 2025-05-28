using AutoPrimitive;

namespace ConsoleApp1
{
    public class Test01
    {
        public DateTime? Start { get; set; }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            {
                var item = new Test01();
                var result3 = "2025-01-01".ToPrimitive() == item.Start;
                var result = item.Start == "2025-01-01".ToPrimitive();

                var result2 = item.Start.Value == "2025-01-01".ToPrimitive();

            }

            {
                string? s = null;
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
