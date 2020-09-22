using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevises
{
    /// <summary>
    /// Currency change rates.
    /// </summary>
    /// <remarks>
    /// Rates are represented internally as a dictionnary.<br/>
    /// Keys are a pair of source currency and destination currency.<br/>
    /// Values are the corresponding rates.
    /// </remarks>
    public class CurrencyChangeRates
    {
        private readonly Dictionary<Tuple<string, string>, double> currencyChangeRates = new Dictionary<Tuple<string, string>, double>();

        /// <summary>
        /// Adds a pair currency / destination currency to the rates dictionnary.
        /// </summary>
        /// <param name="sourceCurrency">The source currency.</param>
        /// <param name="destinationCurrency">The destionation currency.</param>
        /// <param name="changeRate">The currency change rate for conversion.</param>
        public void Add(string sourceCurrency, string destinationCurrency, double changeRate)
        {
            currencyChangeRates.TryAdd(new Tuple<string, string>(sourceCurrency, destinationCurrency), changeRate);
        }

        /// <summary>
        /// Gets the currency change rate of a given pair source currency / destination currency.
        /// </summary>
        /// <param name="sourceCurrency">The source currency.</param>
        /// <param name="destinationCurrency">The destination currency.</param>
        /// <returns></returns>
        public double Get(string sourceCurrency, string destinationCurrency)
        {
            return currencyChangeRates[new Tuple<string, string>(sourceCurrency, destinationCurrency)];
        }

        /// <summary>
        /// Counts the number of currency change rates.
        /// </summary>
        /// <returns>The number of currency change rates.</returns>
        public int Count()
        {
            return currencyChangeRates.Count;
        }
    }
}
