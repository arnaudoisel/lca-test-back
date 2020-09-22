using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevises
{
	/// <summary>
	/// Representation of an undirected graph with unweighted edges.
	/// </summary>
	public class Graph
    {
		private readonly Dictionary<Vertex, List<Vertex>> adjacentVertices = new Dictionary<Vertex, List<Vertex>>();

		private void AddVertex(Vertex vertex)
		{
			adjacentVertices.TryAdd(vertex, new List<Vertex>());
		}

		/// <summary>
		/// Adds edges to the graph and vertices if they are not already in the graph.
		/// </summary>
		/// <remarks>
		/// The graph is undirected, the link between vertices goes both ways.
		/// </remarks>
		/// <param name="vertex1"></param>
		/// <param name="vertex2"></param>
		public void AddEdge(Vertex vertex1, Vertex vertex2)
		{
			AddVertex(vertex1);
			AddVertex(vertex2);

			adjacentVertices[vertex1].Add(vertex2);
			adjacentVertices[vertex2].Add(vertex1);
		}

		/// <summary>
		/// Get vertices adjacent to a given vertex.
		/// </summary>
		/// <param name="vertex"></param>
		/// <returns>The list of adjacent vertices in addition order.</returns>
		public List<Vertex> GetAdjacentVertices(Vertex vertex)
		{
			return adjacentVertices[vertex];
		}

		/// <summary>
		/// Count the number of edges.
		/// </summary>
		/// <remarks>
		/// In this particular case, we count an edge twice.<br/>
		/// For exemple, for (A - B) we count A -> B and B -> A.
		/// </remarks>
		/// <returns>The number of edges.</returns>
		public int CountEdges()
        {
			int count = 0;
			foreach (var vertex in adjacentVertices.Keys)
			{
				foreach(var adjacentVertex in adjacentVertices[vertex])
                {
					count++;
                }
			}
			return count;
        }

		/// <summary>
		/// Checks if vertex is in the graph.
		/// </summary>
		/// <param name="vertex">The vertex to check.</param>
		/// <returns>True if vertex is in the graph, false either.</returns>
		public bool Contains(Vertex vertex)
        {
			return adjacentVertices.ContainsKey(vertex);
        }
	}
}
