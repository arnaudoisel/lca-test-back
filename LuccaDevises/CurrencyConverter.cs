using LuccaDevises.graph;
using System;
using System.Collections.Generic;

namespace LuccaDevises
{
    public class CurrencyConverter
    {
		private readonly decimal amount;
		private readonly Graph graph;
		private readonly Vertex source;
		private readonly Vertex destination;
		private readonly CurrencyChangeRates currencyChangeRates;

		public CurrencyConverter(Input input)
        {
			this.amount = input.Amount;
			this.currencyChangeRates = input.CurrencyChangeRates;
			this.graph = input.CurrencyChangesGraph;
			this.source = new Vertex(input.SourceCurrency);
			this.destination = new Vertex(input.DestinationCurrency);
        }

		/// <summary>
		/// Convert an amount of money in a given currency to the desired currency.
		/// </summary>
		/// <remarks>
		/// It uses a Breadth-First algorithm to search for the shortest path between two currencies.
		/// </remarks>
		/// <returns>The amount in the desired destination currency.</returns>
        public decimal Convert()
        {
			List<Vertex> path = new BreadthFirstSearch().FindShortestPath(graph, source, destination);
			return ConvertAmount(amount, path);
		}

		private decimal ConvertAmount(decimal amount, List<Vertex> path)
		{
			for (int i = 0; i < path.Count - 1; i++)
			{
				Vertex source = path[i];
				Vertex destination = path[i + 1];
				double rate = currencyChangeRates.Get(source.label, destination.label);
				amount = decimal.Round(amount * (decimal)rate, 4, MidpointRounding.AwayFromZero);
			}
			return decimal.Round(amount, 0, MidpointRounding.AwayFromZero);
		}
	}
}
