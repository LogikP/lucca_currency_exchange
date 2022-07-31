namespace LuccaDevises.utilities
{

    public class FileParser
    {
        public string ToConvert { get; }

        public int ConversionsArrayLength { get; }

        public string[] ConversionsArray { get; }

        private static string[] OpenFile(string filePath)
        {
            var lines = File.ReadAllLines(filePath);
            var error = new Error();
            error.CheckFile(lines);
            
            
            return lines;
        }

        public FileParser(string filePath)
        {
            var lines = OpenFile(filePath);
            this.ToConvert = lines[0];
            this.ConversionsArrayLength = int.Parse(lines[1]);
            this.ConversionsArray = lines[2..];
        }
    }
}