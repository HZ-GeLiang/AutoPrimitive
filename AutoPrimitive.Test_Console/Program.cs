using System.Globalization;

namespace AutoPrimitive.Test_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {

            string negativeTs = "-631152000000"; // 1949-10-01 的近似时间戳
            if (long.TryParse(negativeTs, out var ts))
            {
                var date = DateTimeOffset.FromUnixTimeMilliseconds(ts).LocalDateTime;
                var js_date = date.ToString("yyyy-MM-dd"); // 输出：1949-10-01（具体取决于时区）

                Console.WriteLine(js_date);
            }


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
