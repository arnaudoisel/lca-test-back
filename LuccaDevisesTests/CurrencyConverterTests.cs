using LuccaDevises;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevisesTests
{
    [TestClass]
    public class CurrencyConverterTests
    {
        [TestMethod]
        public void Convert_ShouldConvertAmount()
        {
            var input = InputBuilder.GetInstance()
                .ReadFirstLine("EUR;550;JPY")
                .ReadCurrencyChangeRatesLines(new string[] {
                    "6",
                    "AUD;CHF;0.9661",
                    "JPY;KRW;13.1151",
                    "EUR;CHF;1.2053",
                    "AUD;JPY;86.0305",
                    "EUR;USD;1.2989",
                    "JPY;INR;0.6571"
                })
                .Build();
            var currencyConverter = new CurrencyConverter(input);

            decimal chf = RoundAmount(550M * (decimal)1.2053);
            double chfToAud = RoundRate(1 / 0.9661);
            decimal aud = RoundAmount(chf * (decimal)chfToAud);
            decimal jpy = RoundAmount(aud * (decimal)86.0305);
            
            decimal expectedResult = decimal.Round(jpy, 0, MidpointRounding.AwayFromZero);
            decimal actualResult = currencyConverter.Convert();

            Assert.AreEqual(expectedResult, actualResult,
                "Amount converted is incorrect.");
        }

        private decimal RoundAmount(decimal money)
        {
            return decimal.Round(money, 4, MidpointRounding.AwayFromZero);
        }

        private double RoundRate(double rate)
        {
            return Math.Round(rate, 4);
        }
    }
}
