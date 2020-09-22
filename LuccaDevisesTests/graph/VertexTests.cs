using LuccaDevises;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace LuccaDevisesTests
{
    [TestClass]
    public class VertexTests
    {
        [TestMethod]
        public void NewVertex_ShouldCreateVertexWithLabel()
        {
            string label = "test";

            Vertex vertex = new Vertex(label);

            Assert.AreEqual(label, vertex.label, "Vertex was not constructed correctly with label.");
        }

        [TestMethod]
        public void TwoVertices_WithSameLabel_ShouldBeEqual()
        {
            Vertex vertex1 = new Vertex("test");
            Vertex vertex2 = new Vertex("test");
            Assert.AreEqual(vertex1, vertex2, "Vertices with same label should be equal.");
        }
    }
}
