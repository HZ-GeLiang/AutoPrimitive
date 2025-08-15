using System.Globalization;

namespace AutoPrimitive.Test_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string cleanedTime = "Fri Aug 15 2025 08:07:32 GMT+0800";
            string format = "ddd MMM dd yyyy HH:mm:ss 'GMT'zzz";

            var TryParse = DateTimeOffset.TryParseExact(cleanedTime, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTimeOffset result);

            var item = new Test01() { Start = DateTime.Now };
            bool result2 = item.Start.Value == "2025-01-01".ToPrimitive();

            Console.WriteLine("Hello, World!");
        }
        public class Test01
        {
            public DateTime? Start { get; set; }
        }

    }
}
