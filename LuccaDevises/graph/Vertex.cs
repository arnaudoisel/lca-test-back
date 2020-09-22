using System;
using System.Collections.Generic;
using System.Text;

namespace LuccaDevises
{
    /// <summary>
    /// Represents a node in a graph.
    /// </summary>
    public class Vertex
    {
        public readonly string label;

        public Vertex(string label)
        {
            this.label = label;
        }

        public override bool Equals(object obj)
        {
            return obj is Vertex vertex &&
                   label == vertex.label;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(label);
        }
    }
}
