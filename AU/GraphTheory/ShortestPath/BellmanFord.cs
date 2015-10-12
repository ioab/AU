namespace AU.GraphTheory.ShortestPath
{
    using AU.GraphTheory.Graph;
    using System.Collections.Generic;

    public class BellmanFord
    {
        private GraphExtension Graph { get; set; }

        /// <summary>
        /// the accessory for the shortest path edges weights.
        /// </summary>
        public double[] Shortest
        {
            get { return Graph.Shortest; }
        }

        /// <summary>
        /// the accessory for the shortest path predecessors (path indicators).
        /// </summary>
        public int?[] Predecessors
        {
            get { return Graph.Predecessors; }
        }

        public BellmanFord()
        {
            Graph = new GraphExtension();
        }

        /// <summary>
        /// The Bellman-Ford shortest path procedure for a directed &quot;possibly negative&quot; weighted graphs.
        /// </summary>
        /// <param name="directedGraph">
        /// a directed graph containing a set V of n vertices
        /// and a set E of m directed edges with arbitrary weights..
        /// </param>
        /// <param name="sourceVertex">a source vertex in V.</param>
        /// <remarks>
        /// Result:
        ///     Same as Dijstrak().
        /// </remarks>
        public void Compute(IGraph directedGraph, int sourceVertex)
        {
            Graph.Shortest = new double[directedGraph.N];
            Graph.Predecessors = new int?[directedGraph.N];

            for (int i = 0; i < directedGraph.N; ++i)
            {
                Graph.Shortest[i] = double.PositiveInfinity;
                Graph.Predecessors[i] = null;
            }

            Graph.Shortest[sourceVertex] = 0;

            foreach (var u in directedGraph.Vertices)
                foreach (var v in directedGraph[u])
                    Graph.Relax(directedGraph, u, v);
        }

        /// <summary>
        /// The Find-Negative-Weight-Cycle() procedure to determine whether a graph has a negative-weight cycle,
        /// and how to construct one if it does.
        /// </summary>
        /// <param name="graph">A directed graph containing a set V of n vertices and a set E of m directed edges with arbitrary weights on which
        /// the BellmanFord() procedure has already been run.</param>
        /// <returns>
        /// Either a list of vertices in a negative-weight cycle, in order,
        /// or an empty list if the graph has no negative-weight cycles.
        /// </returns>
        public List<int> FindNegativeWeightCycle(IGraph directedGraph)
        {
            foreach (var u in directedGraph.Vertices)
                foreach (var v in directedGraph[u])
                    if (Shortest[u] + directedGraph.Weight(u, v) < Shortest[v])
                    {
                        int vTemp = v;
                        bool[] visited = new bool[directedGraph.N];         // Initialized by default to false //

                        int x = v;
                        while (!visited[x])
                        {
                            visited[x] = true;
                            x = (int)Graph.Predecessors[x];
                        }
                        vTemp = (int)Graph.Predecessors[x];

                        var cycle = new List<int>() { x };
                        while (vTemp != x)
                        {
                            cycle.Insert(0, vTemp);
                            vTemp = (int)Graph.Predecessors[vTemp];
                        }
                        return cycle;
                    }

            /**
             *  No negative edges, return empty list .. (P.104).
             */
            return new List<int>();
        }
    }
}