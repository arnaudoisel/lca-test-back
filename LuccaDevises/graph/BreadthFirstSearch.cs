using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevises.graph
{
	/// <summary>
	/// Search in tree or graph based on breadth-first algorithm.
	/// </summary>
    public class BreadthFirstSearch
    {
		/// <summary>
		/// Finds the shortest path in a graph between a source and a destination.
		/// </summary>
		/// <param name="graph">The graph to travel.</param>
		/// <param name="source">The source vertex.</param>
		/// <param name="destination">The destination vertex.</param>
		/// <returns>The path from source to destination vertices as a List</returns>
		/// <exception cref="ArgumentException">If vertices are not connected, or not in the graph.</exception>
		public List<Vertex> FindShortestPath(Graph graph, Vertex source, Vertex destination)
		{
			var predecessors = new Dictionary<Vertex, Vertex>();
			var visited = new HashSet<Vertex>();
			var queue = new Queue<Vertex>();
			queue.Enqueue(source);
			visited.Add(source);
			while (queue.Count != 0)
			{
				Vertex vertex = queue.Dequeue();
				foreach (var adjacentVertex in graph.GetAdjacentVertices(vertex))
				{
					if (!visited.Contains(adjacentVertex))
					{
						visited.Add(adjacentVertex);
						queue.Enqueue(adjacentVertex);

						predecessors.Add(adjacentVertex, vertex);

						if (adjacentVertex.Equals(destination))
							return GetPath(predecessors, destination);
					}
				}
			}
			throw new ArgumentException(String.Format("There is no path connecting vertices {0} and {1}.", source.label, destination.label));
		}

		private List<Vertex> GetPath(
			Dictionary<Vertex, Vertex> predecessors,
			Vertex destination)
		{
			var path = new List<Vertex>();
			path.Add(destination);

			Vertex crawl = destination;
			while (predecessors.TryGetValue(crawl, out Vertex predecessor))
			{
				path.Add(predecessor);
				crawl = predecessor;
			}

			path.Reverse();

			return path;
		}
	}
}
