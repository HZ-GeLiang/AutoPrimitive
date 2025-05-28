namespace AutoPrimitive.Test_Console
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
