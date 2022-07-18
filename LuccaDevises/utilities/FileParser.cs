namespace LuccaDevises.utilities
{

    // interface IFileParser
    // {
    //     
    // }
    public class FileParser
    {
        private static IEnumerable<string> OpenFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            Error error = new Error();
            error.CheckFile(lines);
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