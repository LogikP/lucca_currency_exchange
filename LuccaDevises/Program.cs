using LuccaDevises.algo;
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
                
                FileParser fileParser = new FileParser(args[0]);
                
                Algo algo = new Algo(fileParser.ToConvert, fileParser.ConversionsArrayLength,
                    fileParser.ConversionsArray);
                
                algo.Exchange();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Environment.Exit(84);
            }
        }
    }
}