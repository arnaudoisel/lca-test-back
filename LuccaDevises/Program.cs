using System;
using System.Globalization;

namespace LuccaDevises
{
    /// <summary>
    /// Entrypoint, class containing the Main method.
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                if (args == null || args.Length != 1)
                    throw new ArgumentException("This program takes as input one argument only that is a file path.");

                Input input = new FileReader().Read(args[0]);
                var currencyConverter = new CurrencyConverter(input);
                decimal result = currencyConverter.Convert();
                Console.WriteLine(result);
            }
            catch (ArgumentException e)
            {
                Console.Write(e.Message);
            }
        }
    }
}
