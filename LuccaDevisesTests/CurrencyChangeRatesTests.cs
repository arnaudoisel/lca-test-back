using LuccaDevises;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevisesTests
{
    [TestClass]
    public class CurrencyChangeRatesTests
    {

        [TestMethod]
        public void Add_OneRate_ShouldAddCorrectly()
        {
            var rates = new CurrencyChangeRates();

            string sourceCurrency = "AUD";
            string destinationCurrency = "CHF";
            double changeRate = 1.1444;

            rates.Add(sourceCurrency, destinationCurrency, changeRate);

            Assert.AreEqual(
                1,
                rates.Count(),
                "Resulting change rates size is invalid.");

            Assert.AreEqual(
                changeRate,
                rates.Get(sourceCurrency, destinationCurrency),
                "Change rate was not added correctly.");
        }

        [TestMethod]
        public void Add_MultipleRates_ShouldAddCorrectly()
        {
            var rates = new CurrencyChangeRates();

            string sourceCurrency1 = "AUD";
            string destinationCurrency1 = "CHF";
            double changeRate1 = 1.1444;

            string sourceCurrency2 = "EUR";
            string destinationCurrency2 = "JPY";
            double changeRate2 = 0.9444;

            rates.Add(sourceCurrency1, destinationCurrency1, changeRate1);
            rates.Add(sourceCurrency2, destinationCurrency2, changeRate2);

            Assert.AreEqual(
                2,
                rates.Count(),
                "Resulting change rates size is invalid.");

            Assert.AreEqual(
                changeRate1,
                rates.Get(sourceCurrency1, destinationCurrency1),
                "Change rate was not added correctly.");

            Assert.AreEqual(
                changeRate2,
                rates.Get(sourceCurrency2, destinationCurrency2),
                "Change rate was not added correctly.");
        }

        [TestMethod]
        public void Add_SameCurrencyChangeTwoTimes_ShouldAddOnlyFirst()
        {
            var rates = new CurrencyChangeRates();

            string sourceCurrency = "AUD";
            string destinationCurrency = "CHF";
            double changeRate1 = 1.1444;
            double changeRate2 = 1.1444;

            rates.Add(sourceCurrency, destinationCurrency, changeRate1);
            rates.Add(sourceCurrency, destinationCurrency, changeRate2);

            Assert.AreEqual(
                1,
                rates.Count(),
                "Resulting change rates size is invalid.");

            Assert.AreEqual(
                changeRate1,
                rates.Get(sourceCurrency, destinationCurrency),
                "Change rate was not added correctly.");
        }
    }
}
