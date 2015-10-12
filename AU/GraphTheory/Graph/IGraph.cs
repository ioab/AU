namespace AU.GraphTheory.Graph
{
    using System.Collections.Generic;

    /// <summary>
    /// The abstract contract that a graph implementation has to guarantee.
    /// </summary>
    public interface IGraph
    {
        /// <summary>
        /// Number of vertex.
        /// </summary>
        int N { get; }

        /// <summary>
        /// Number of edges.
        /// </summary>
        int M { get; }

        /// <summary>
        /// A helper accessory for the vertices in the graph.
        /// </summary>
        List<int> Vertices { get; }

        /// <summary>
        /// An exposed iterator for the adjacent vertices of a vertex.
        /// Mandatory for adjacent list time complexity performance gain. And helper in adjacent matrix.
        /// </summary>
        /// <param name="vertex">A vertex to retrieve its adjacent vertices.</param>
        /// <returns>An enumerable collection for the adjacent vertices.</returns>
        IEnumerable<int> this[int vertex] { get; }

        /// <summary>
        /// A procedure to add an edge between two vertices.
        /// </summary>
        /// <param name="vertexV">The first vertex in which the edge comes from.</param>
        /// <param name="vertexU">The second vertex in which the edge goes onto</param>
        /// <param name="weight">The weight of the edge. If not provided. Null is default.</param>
        void Add(int vertexU, int vertexV, double? weight = null);

        /// <summary>
        /// A procedure to remove an edge between two vertices.
        /// Input: Same as Add().
        /// </summary>
        void Remove(int vertexU, int vertexV);

        /// <summary>
        /// A procedure to check whether there is an edge between two vertices.
        /// Input: Same as Add().
        /// </summary>
        /// <returns>True if there is an edge (vertexU,vertexV). False, otherwise.</returns>
        bool Exists(int vertexU, int vertexV);

        /// <summary>
        /// A procedure to get the weight of an edge, if exists.
        /// Input: Same as Add().
        /// </summary>
        /// <returns>The weight if there is an edge (vertexU,vertexV). NULL, otherwise.</returns>
        double? Weight(int vertexU, int vertexV);
    }
}