using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace LuccaDevises
{
	/// <summary>
	/// Helper class to build an <see cref="Input"/>
	/// </summary>
	public class InputBuilder
    {
		private readonly Input input;

		private InputBuilder()
        {
			this.input = new Input();
        }
		public static InputBuilder GetInstance()
        {
			return new InputBuilder();
        }

		/// <summary>
		/// Reads the first line of input file.
		/// </summary>
		/// <returns>The current instance of <see cref="InputBuilder"/> for chaining.</returns>
		/// <param name="line">The line to read.</param>
		/// <remarks>
		/// Line should have format :
		/// <para>SOURCE_CURRENCY;AMOUNT;DESTINATION_CURRENCY</para>
		/// <para>Example :</para>
		/// <code>
		/// EUR;123;JPY
		/// </code>
		/// </remarks>
		/// <exception cref="ArgumentException">If the line format is incorrect.</exception>
		public InputBuilder ReadFirstLine(string line)
		{
			string[] lineArray = line.Split(';');

			if (lineArray.Length != 3)
				throw new ArgumentException(String.Format(
					"First line of input file {0} is invalid. It should contain source currency, amount and destination currency separated by ';' character.", line));

			ReadSourceCurrency(lineArray[0]);
			ReadAmount(lineArray[1]);
			ReadDestinationCurrency(lineArray[2]);

			return this;
		}

		private void ReadSourceCurrency(string sourceCurrency)
        {
			VerifyCurrencyFormat(sourceCurrency);

			this.input.SourceCurrency = sourceCurrency;
		}

		private void VerifyCurrencyFormat(string currency)
        {
			if (currency.Length != 3)
				throw new ArgumentException(String.Format("Currency {0} is invalid. It should be 3 characters long.", currency));
		}

		private void ReadAmount(string amountString)
        {
			try
			{
				decimal amount = decimal.Parse(amountString, CultureInfo.InvariantCulture);

				if (!IsInteger(amount))
					throw new ArgumentException(String.Format("Amount {0} is invalid. It should is not an integer.", amountString));
				if (amount < 0)
					throw new ArgumentException(String.Format("Amount {0} is invalid. It should be positive.", amountString));

				this.input.Amount = amount;
			}
			catch (FormatException e)
			{
				throw new ArgumentException(String.Format("Amount {0} is invalid. It should be a number (integer greater than 0).", amountString) ,e);
			}
		}

		private bool IsInteger(decimal number)
        {
			return number % 1 == 0;
		}

		private void ReadDestinationCurrency(string destinationCurrency)
        {
			VerifyCurrencyFormat(destinationCurrency);

			this.input.DestinationCurrency = destinationCurrency;
        }

		/// <summary>
		/// Reads all lines of input file except the first one.
		/// </summary>
		/// <returns>The current instance of <see cref="InputBuilder"/> for chaining.</returns>
		/// <param name="lines">The lines to read.</param>
		/// <remarks>
		/// Lines should have the following format :
		/// <para>Example :</para>
		/// <code>
		/// <para>3</para>
		/// <para>EUR;JPY;1.2445</para>
		/// <para>AUD;JPY;0.3421</para>
		/// <para>JPY;CHF;921.2192</para>
		/// </code>
		/// </remarks>
		/// <exception cref="ArgumentException">If the lines format is incorrect.</exception>
		public InputBuilder ReadCurrencyChangeRatesLines(String[] lines)
		{
			string numberOfCurrencyChangeRatesString = lines[0];
            if (!Int32.TryParse(numberOfCurrencyChangeRatesString, out int numberOfCurrencyChangeRates))
                throw new ArgumentException(String.Format("Format of number of currency change rates {0} is invalid.", numberOfCurrencyChangeRatesString));

            if (lines.Length > numberOfCurrencyChangeRates + 1)
				Console.WriteLine(String.Format("Only {0} currency change rates lines are read. If you want more lines to be read, increase number of lines specified at line 2 of input file.", numberOfCurrencyChangeRates));
			else if (lines.Length <= numberOfCurrencyChangeRates)
				throw new ArgumentException(String.Format("Input file is invalid. Expected {0} currency change rates lines but got {1}.", numberOfCurrencyChangeRates, lines.Length - 1));

			for (int i = 1; i <= numberOfCurrencyChangeRates; i++)
			{
				ReadCurrencyChangeRateLine(lines[i]);
			}

			return this;
		}

		private void ReadCurrencyChangeRateLine(string line)
        {
			string[] currencyChangeRateArray = line.Split(';');

			if (currencyChangeRateArray.Length != 3)
				throw new ArgumentException(String.Format(
					"Currency rate line {0} is invalid. It should contain source currency, destination currency and change rate separated by ';' character.", line));

			string sourceCurrency = currencyChangeRateArray[0];
			VerifyCurrencyFormat(sourceCurrency);

			string destinationCurrency = currencyChangeRateArray[1];
			VerifyCurrencyFormat(destinationCurrency);

			double changeRate = ReadChangeRate(currencyChangeRateArray[2]);

			input.AddCurrencyChangeRate(sourceCurrency, destinationCurrency, changeRate);
		}

		private double ReadChangeRate(string changeRateString)
        {
			if (!double.TryParse(changeRateString, NumberStyles.AllowDecimalPoint, CultureInfo.InvariantCulture, out double changeRate))
				throw new ArgumentException(String.Format("Format of change rate {0} is invalid.", changeRateString));

			return changeRate;
		}

		public Input Build()
		{
			return this.input;
		}
	}
}
