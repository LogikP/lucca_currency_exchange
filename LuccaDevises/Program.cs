using LuccaDevises.utilities;

namespace LuccaDevises
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                if (args.Length != 1)
                {
                    throw new Exception("Bad number of arguments");
                }
                var fileParser = new FileParser(args[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Environment.Exit(84);
            }
        }
    }
}