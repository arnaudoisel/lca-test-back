using LuccaDevises;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevisesTests
{
    [TestClass]
    public class GraphTests
    {
        [TestMethod]
        public void AddEdge_With4Edges_ShouldAddEdgesCorrectly()
        {
            var graph = new Graph();

            graph.AddEdge(new Vertex("a"), new Vertex("b"));
            graph.AddEdge(new Vertex("b"), new Vertex("c"));
            graph.AddEdge(new Vertex("c"), new Vertex("d"));
            graph.AddEdge(new Vertex("a"), new Vertex("c"));

            Assert.AreEqual(8, graph.CountEdges(),
                "Invalid number of edges in graph.");

            AssertGraphContainsVertex(graph, "a");
            AssertGraphContainsVertex(graph, "b");
            AssertGraphContainsVertex(graph, "c");
            AssertGraphContainsVertex(graph, "d");

            AssertVertexHasAdjacentVertices(graph, "a",
                new string[] { "b", "c" });
            AssertVertexHasAdjacentVertices(graph, "b",
                new string[] { "a", "c" });
            AssertVertexHasAdjacentVertices(graph, "c",
                new string[] { "a", "b", "d" });
            AssertVertexHasAdjacentVertices(graph, "d",
                new string[] { "c" });
        }

        private void AssertGraphContainsVertex(Graph graph, string vertex)
        {
            Assert.IsTrue(
                graph.Contains(new Vertex(vertex)),
                "Vertex not added properly to graph.");
        }

        private void AssertVertexHasAdjacentVertices(
            Graph graph,
            string vertex,
            string[] expectedAdjacentVertices)
        {
            List<Vertex> adjacentVertices = graph.GetAdjacentVertices(new Vertex(vertex));

            Assert.AreEqual(
                expectedAdjacentVertices.Length,
                adjacentVertices.Count,
                String.Format("Invalid number of adjacent vertices from {0}", vertex));

            foreach (string expectedAdjacentVertex in expectedAdjacentVertices)
            {
                CollectionAssert.Contains(
                    adjacentVertices, new Vertex(expectedAdjacentVertex),
                    String.Format("'{0}' should be adjacent to '{1}'", expectedAdjacentVertex, vertex));
            }
        }
    }
}
