using LuccaDevises;
using LuccaDevises.graph;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace LuccaDevisesTests.graph
{
    [TestClass]
    public class BreadthFirstSearchTests
    {
        [TestMethod]
        public void FindShortestPath_WithTreeGraph_ShouldFindShortestPath()
        {
            var bfs = new BreadthFirstSearch();

            var graph = new Graph();
            graph.AddEdge(new Vertex("a"), new Vertex("b"));
            graph.AddEdge(new Vertex("a"), new Vertex("c"));
            graph.AddEdge(new Vertex("a"), new Vertex("d"));
            graph.AddEdge(new Vertex("c"), new Vertex("e"));
            graph.AddEdge(new Vertex("c"), new Vertex("f"));
            graph.AddEdge(new Vertex("f"), new Vertex("g"));

            Vertex source = new Vertex("a");
            Vertex destination = new Vertex("g");

            List<Vertex> actualPath = bfs.FindShortestPath(graph, source, destination);

            string[] expectedPath = { "a", "c", "f", "g" };

            Assert.AreEqual(
                expectedPath.Length,
                actualPath.Count,
                "Length of shortest path is incorrect.");

            AssertPathIs(actualPath, expectedPath);
        }

        private void AssertPathIs(
            List<Vertex> actualPath,
            string[] expectedPath)
        {
            for (int i=0; i < expectedPath.Length; i++)
            {
                Assert.AreEqual(expectedPath[i], actualPath[i].label,
                    string.Format(
                        "Expected vertex {0} at position {1} in path but got {2}.",
                        expectedPath[i], i, actualPath[i].label));
            }
        }

        [TestMethod]
        public void FindShortestPath_WithCyclicGraph_ShouldFindShortestPath()
        {
            var bfs = new BreadthFirstSearch();

            var graph = new Graph();
            graph.AddEdge(new Vertex("a"), new Vertex("b"));
            graph.AddEdge(new Vertex("a"), new Vertex("c"));
            graph.AddEdge(new Vertex("a"), new Vertex("d"));
            graph.AddEdge(new Vertex("c"), new Vertex("e"));
            graph.AddEdge(new Vertex("b"), new Vertex("e"));
            graph.AddEdge(new Vertex("e"), new Vertex("f"));
            graph.AddEdge(new Vertex("d"), new Vertex("f"));

            Vertex source = new Vertex("a");
            Vertex destination = new Vertex("f");

            List<Vertex> actualPath = bfs.FindShortestPath(graph, source, destination);

            string[] expectedPath = { "a", "d", "f" };

            Assert.AreEqual(
                expectedPath.Length,
                actualPath.Count,
                "Length of shortest path is incorrect.");

            AssertPathIs(actualPath, expectedPath);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException),
            "Shortest path was find although source and destination vertices were not connected.")]
        public void FindShortestPath_WithNotConnectedVertices_ShouldThrowException()
        {
            var bfs = new BreadthFirstSearch();

            var graph = new Graph();
            graph.AddEdge(new Vertex("a"), new Vertex("b"));
            graph.AddEdge(new Vertex("a"), new Vertex("c"));
            graph.AddEdge(new Vertex("a"), new Vertex("d"));
            graph.AddEdge(new Vertex("e"), new Vertex("f"));
            graph.AddEdge(new Vertex("e"), new Vertex("g"));
            graph.AddEdge(new Vertex("g"), new Vertex("h"));

            Vertex source = new Vertex("a");
            Vertex destination = new Vertex("h");

            bfs.FindShortestPath(graph, source, destination);
        }
    }
}
