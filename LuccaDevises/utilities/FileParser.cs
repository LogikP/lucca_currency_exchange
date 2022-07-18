
namespace LuccaDevises.utilities
{
    public class FileParser
    {
        private static IEnumerable<string> OpenFile(string filePath)
        {
            var lines = Array.Empty<string>();
            try
            {
                lines = File.ReadAllLines(filePath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Environment.Exit(84);
            }

            return lines;
        }

        public FileParser(string filePath)
        {
            foreach (var line in OpenFile(filePath))
            {
                Console.WriteLine(line);
            }
        }
    }
}