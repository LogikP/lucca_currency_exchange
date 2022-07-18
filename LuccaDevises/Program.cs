using LuccaDevises.utilities;

namespace LuccaDevises
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            FileParser fileParser = new FileParser(args[0]);
            Console.WriteLine("Hello, World!");
        }
    }
}