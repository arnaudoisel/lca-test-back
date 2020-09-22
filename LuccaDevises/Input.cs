using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevises
{
	/// <summary>
	/// Object representation of an input file.
	/// </summary>
    public class Input
    {
		/// <value>The source currency for conversion.</value>
		public string SourceCurrency { get; set; }

		/// <value>The destination currency for conversion.</value>
		public string DestinationCurrency { get; set; }

		/// <value>The amount of money to convert.</value>
		public decimal Amount { get; set; }
		
		/// <value>The currency change rates.</value>
		public readonly CurrencyChangeRates CurrencyChangeRates = new CurrencyChangeRates();
		
		/// <value>The currency changes represented as <see cref="Graph"/>.</value>
		public readonly Graph CurrencyChangesGraph = new Graph();

		/// <summary>
		/// Adds new currency change rate to list.
		/// </summary>
		/// <param name="sourceCurrency">The source currency</param>
		/// <param name="destinationCurrency">The destination currency</param>
		/// <param name="changeRate">The rate for currency conversion</param>
		public void AddCurrencyChangeRate(
			string sourceCurrency, string destinationCurrency, double changeRate)
        {
			CurrencyChangeRates.Add(sourceCurrency, destinationCurrency, changeRate);

			double reverseChangeRate = Math.Round(1 / changeRate, 4, MidpointRounding.AwayFromZero);
			CurrencyChangeRates.Add(destinationCurrency, sourceCurrency, reverseChangeRate);

			CurrencyChangesGraph.AddEdge(new Vertex(sourceCurrency), new Vertex(destinationCurrency));
		}
	}
}
