namespace DontWreckMyHouse.UI
{
    public class ConsoleIO
    {
        private const string INVALID_NUMBER
           = "[INVALID] Enter a valid number.";
        private const string NUMBER_OUT_OF_RANGE
            = "[INVALID] Enter a number between {0} and {1}.";
        private const string REQUIRED
            = "[INVALID] Value is required.";
        private const string INVALID_DATE
            = "[INVALID] Enter a date in MM/dd/yyyy format.";
        private const string INVALID_BOOL
            = "[INVALID] Please enter 'y' or 'n'.";

        public void Print(string message)
        {
            Console.Write(message);
        }

        public void PrintLine(string message)
        {
            Console.WriteLine(message);
        }

        public string ReadString(string prompt)
        {
            Print(prompt);
            return Console.ReadLine();
        }

        public string ReadRequiredString(string prompt)
        {
            while (true)
            {
                string result = ReadString(prompt);
                if (!string.IsNullOrWhiteSpace(result))
                {
                    return result;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine(REQUIRED);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public decimal ReadDecimal(string prompt)
        {
            decimal result;
            while (true)
            {
                if (decimal.TryParse(ReadRequiredString(prompt), out result))
                {
                    return result;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine(INVALID_NUMBER);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public decimal ReadDecimal(string prompt, decimal min, decimal max)
        {
            while (true)
            {
                decimal result = ReadDecimal(prompt);
                if (result >= min && result <= max)
                {
                    return result;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine(string.Format(NUMBER_OUT_OF_RANGE, min, max));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public int ReadInt(string prompt)
        {
            int result;
            while (true)
            {
                if (int.TryParse(ReadRequiredString(prompt), out result))
                {
                    return result;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine(INVALID_NUMBER);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public int ReadInt(string prompt, int min, int max)
        {
            while (true)
            {
                int result = ReadInt(prompt);
                if (result >= min && result <= max)
                {
                    return result;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine(string.Format(NUMBER_OUT_OF_RANGE, min, max));
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public bool ReadBool(string prompt)
        {
            while (true)
            {
                string input = ReadRequiredString(prompt).ToLower();
                if (input == "y")
                {
                    return true;
                }
                else if (input == "n")
                {
                    return false;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine(INVALID_BOOL);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public DateTime ReadDate(string prompt)
        {
            DateTime result;
            while (true)
            {
                string input = ReadRequiredString(prompt);
                if (DateTime.TryParse(input, out result))
                {
                    return result.Date;
                }
                Console.ForegroundColor = ConsoleColor.Red;
                PrintLine(INVALID_DATE);
                Console.ForegroundColor = ConsoleColor.White;
            }
        }
    }
}
