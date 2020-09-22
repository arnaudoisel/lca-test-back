using LuccaDevises;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;

namespace LuccaDevisesTests
{
    [TestClass]
    public class InputBuilderReadCurrencyChangeRatesLinesTests
    {
        private const string ValidNumberOfChangeRates = "3";
        private const int ValidNumberOfChangeRatesInteger = 3;
        private static readonly Tuple<string, string, string> ValidCurrencyChangeRate1
            = new Tuple<string, string, string>("AUD", "CHF", "0.9661");
        private static readonly Tuple<string, string, string> ValidCurrencyChangeRate2
            = new Tuple<string, string, string>("JPY", "KRW", "13.1151");
        private static readonly Tuple<string, string, string> ValidCurrencyChangeRate3
            = new Tuple<string, string, string>("EUR", "CHF", "1.2053");

        [TestMethod]
        public void ReadCurrencyChangeRatesLines_WithValidLines_ShouldCorrectlyBuildCurrencyChangeRatesAndGraph()
        {
            string[] lines = BuildValidLines();

            Input input = InputBuilder.GetInstance().ReadCurrencyChangeRatesLines(lines).Build();
            
            AssertCurrencyChangeRatesAreValid(input.CurrencyChangeRates);
            AssertGraphIsValid(input.CurrencyChangesGraph);
        }

        private string[] BuildValidLines()
        {
            string[] lines = {
                ValidNumberOfChangeRates,
                string.Format("{0};{1};{2}", ValidCurrencyChangeRate1.Item1, ValidCurrencyChangeRate1.Item2, ValidCurrencyChangeRate1.Item3),
                string.Format("{0};{1};{2}", ValidCurrencyChangeRate2.Item1, ValidCurrencyChangeRate2.Item2, ValidCurrencyChangeRate2.Item3),
                string.Format("{0};{1};{2}", ValidCurrencyChangeRate3.Item1, ValidCurrencyChangeRate3.Item2, ValidCurrencyChangeRate3.Item3)
            };

            return lines;
        }

        private void AssertCurrencyChangeRatesAreValid(CurrencyChangeRates rates)
        {
            Assert.AreEqual(ValidNumberOfChangeRatesInteger * 2, rates.Count(),
                "Number of currency change rates built is invalid.");

            double expectedChangeRate = double.Parse(
                ValidCurrencyChangeRate1.Item3,
                NumberStyles.AllowDecimalPoint,
                CultureInfo.InvariantCulture);

            Assert.AreEqual(
                expectedChangeRate,
                rates.Get(ValidCurrencyChangeRate1.Item1, ValidCurrencyChangeRate1.Item2),
                "Currency change rate was not correctly read.");
            
            Assert.AreEqual(
                Math.Round(1 / expectedChangeRate, 4, MidpointRounding.AwayFromZero),
                rates.Get(ValidCurrencyChangeRate1.Item2, ValidCurrencyChangeRate1.Item1),
                "Inverted currency change rate was not correctly built.");
        }

        private void AssertGraphIsValid(Graph graph)
        {
            Assert.AreEqual(
                ValidNumberOfChangeRatesInteger * 2,
                graph.CountEdges(),
                "Number of edges built is incorrect.");

            Assert.AreEqual(
                2,
                graph.GetAdjacentVertices(new Vertex(ValidCurrencyChangeRate1.Item2)).Count,
                "Number of adjacent verticices built is incorrect.");

            CollectionAssert.Contains(
                graph.GetAdjacentVertices(new Vertex(ValidCurrencyChangeRate1.Item2)),
                new Vertex(ValidCurrencyChangeRate1.Item1),
                "Adjacent vertex was not build correctly.");
            CollectionAssert.Contains(
                graph.GetAdjacentVertices(new Vertex(ValidCurrencyChangeRate3.Item2)),
                new Vertex(ValidCurrencyChangeRate3.Item1),
                "Adjacent vertex was not build correctly.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Missing count currency change rates line was read without issue.")]
        public void ReadCurrencyChangeRates_WithMissingCountLine_ShouldThrowException()
        {
            string[] lines = BuildValidLines();

            InputBuilder.GetInstance().ReadCurrencyChangeRatesLines(lines[1..^0]);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid count (string) currency change rates line was read without issue.")]
        public void ReadCurrencyChangeRates_WithInvalidStringCountLine_ShouldThrowException()
        {
            string[] lines = BuildValidLines();
            lines[0] = "invalid_count_currency_change_rates_line";

            InputBuilder.GetInstance().ReadCurrencyChangeRatesLines(lines);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid count (decimal) currency change rates line was read without issue.")]
        public void ReadCurrencyChangeRates_WithInvalidDecimalCountLine_ShouldThrowException()
        {
            string[] lines = BuildValidLines();
            lines[0] = "1.13";

            InputBuilder.GetInstance().ReadCurrencyChangeRatesLines(lines);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Count of currency change rates lines greater than actual number of currency change rates lines was read without issue.")]
        public void ReadCurrencyChangeRates_WithCountGreaterActualNumberOfLine_ShouldThrowException()
        {
            string[] lines = BuildValidLines();
            lines[0] = "4";

            InputBuilder.GetInstance().ReadCurrencyChangeRatesLines(lines);
        }

        [TestMethod]
        public void ReadCurrencyChangeRates_WithCountLowerThanActualNumberOfLine_ShouldCorrectlyBuildRatesAndGraphWithFirstLines()
        {
            string[] lines = BuildValidLines();
            lines[0] = "2";

            Input input = InputBuilder.GetInstance().ReadCurrencyChangeRatesLines(lines).Build();

            Assert.AreEqual(4, input.CurrencyChangeRates.Count(),
                "Number of currency change rates built is invalid.");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid currency in first column of change rate line was read without issue.")]
        public void ReadCurrencyChangeRates_WithInvalidCurrencyInFirstColumnOfChangeRateLine_ShouldThrowException()
        {
            string[] lines = BuildValidLines();
            lines[1] = "AU;CHF;0.9661";

            InputBuilder.GetInstance().ReadCurrencyChangeRatesLines(lines);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid currency in second column of change rate line was read without issue.")]
        public void ReadCurrencyChangeRates_WithInvalidCurrencyInSecondColumnOfChangeRateLine_ShouldThrowException()
        {
            string[] lines = BuildValidLines();
            lines[1] = "AUD;CH;0.9661";

            InputBuilder.GetInstance().ReadCurrencyChangeRatesLines(lines);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Invalid change rate was read without issue.")]
        public void ReadCurrencyChangeRates_WithInvalidChangeRate_ShouldThrowException()
        {
            string[] lines = BuildValidLines();
            lines[1] = "AUD;CHF;invalid_change_rate";

            InputBuilder.GetInstance().ReadCurrencyChangeRatesLines(lines);
        }
    }
}
