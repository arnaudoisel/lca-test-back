using LuccaDevises;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace LuccaDevisesTests
{
    [TestClass]
    public class InputTests
    {
        [TestMethod]
        public void AddCurrencyChangeRate_WithOneRate_ShouldCorrectlyBuildRatesDictionnaryAndGraph()
        {
            string sourceCurrency = "AUD";
            string destinationCurrency = "CHF";
            double changeRate = 1.9281;

            Input input = new Input();
            input.AddCurrencyChangeRate(sourceCurrency, destinationCurrency, changeRate);

            Assert.AreEqual(
                changeRate,
                input.CurrencyChangeRates.Get(sourceCurrency, destinationCurrency),
                "Rate was not correctly added to input."
                );

            Assert.AreEqual(
                Math.Round(1 / changeRate, 4, MidpointRounding.AwayFromZero),
                input.CurrencyChangeRates.Get(destinationCurrency, sourceCurrency),
                "Inverted rate was not correctly added to input."
                );

            Assert.AreEqual(
                2,
                input.CurrencyChangesGraph.CountEdges(),
                "Number of edges in graph does not corresponds to the number of currencies added.");

            Assert.IsTrue(
                input.CurrencyChangesGraph.Contains(new Vertex(sourceCurrency)),
                String.Format("Currency {0} was not added correctly to graph.", sourceCurrency));

            Assert.IsTrue(
                input.CurrencyChangesGraph.Contains(new Vertex(destinationCurrency)),
                String.Format("Currency {0} was not added correctly to graph.", destinationCurrency));

            CollectionAssert.Contains(
                input.CurrencyChangesGraph.GetAdjacentVertices(new Vertex(sourceCurrency)),
                new Vertex(destinationCurrency),
                String.Format("Graph does not contain edge : {0} -> {1}", sourceCurrency, destinationCurrency));

            CollectionAssert.Contains(
                input.CurrencyChangesGraph.GetAdjacentVertices(new Vertex(destinationCurrency)),
                new Vertex(sourceCurrency),
                String.Format("Graph does not contain edge : {0} -> {1}", destinationCurrency, sourceCurrency));

        }
    }
}
