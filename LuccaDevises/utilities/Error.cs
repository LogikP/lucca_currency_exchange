using System.Text.RegularExpressions;

namespace LuccaDevises.utilities
{
    // interface IError
    // {
    // }

    public class Error
    {
        private readonly Regex _lineFormat = new Regex(@"([a-zA-Z\s]{3});(-?[0-9]*);([a-zA-Z\s]{3})$");
        private readonly Regex _lineNumber = new Regex(@"([0-9]*)$");
        private readonly Regex _lineToConvert = new Regex(@"([a-zA-Z\s]{3});([a-zA-Z\s]{3});[0-9]*(?:\.[0-9]{0,4})$");

        #region LinesVerification

        private static Boolean CheckPositivity(string toConvert)
        {
            int from = toConvert.IndexOf(';') + 1;
            int to = toConvert.LastIndexOf(';');
            int number = int.Parse(toConvert.Substring(from, to - from));

            return number > 0;
        }

        private void CheckFirstLine(string firstLine)
        {
            Regexp(firstLine, this._lineFormat);

            if (!CheckPositivity(firstLine))
            {
                throw new Exception("You must convert positive numbers");
            }
        }

        private void CheckSecondLine(string secondLine)
        {
            if (!this._lineNumber.IsMatch(secondLine) || int.Parse(secondLine) <= 0)
            {
                throw new Exception("the second line must be a positive number");
            }
        }

        private static Boolean CheckConversionArrayLength(int counter, int input)
        {
            return counter == input;
        }

        private void CheckConversionArray(string[] array)
        {
            int i = 2;
            for (; i < array.Length; i++)
            {
                Regexp(array[i], this._lineToConvert);
            }

            if (!CheckConversionArrayLength((i - 2), int.Parse(array[1])))
            {
                throw new Exception("Bad number of conversions");
            }
        }

        private static void Regexp(string line, Regex regex)
        {
            if (!regex.IsMatch(line))
                throw new Exception("Bad format: " + line + ", the format must be : AAA;ZZZ;0.000");
        }

        #endregion

        public Error()
        {
        }

        public void CheckFile(string[] file)
        {
            if (file.Length < 3)
            {
                throw new Exception("The file given in input must contains at least 3 lines");
            }

            this.CheckFirstLine(file[0]);
            this.CheckSecondLine(file[1]);
            this.CheckConversionArray(file);
        }
    }
}