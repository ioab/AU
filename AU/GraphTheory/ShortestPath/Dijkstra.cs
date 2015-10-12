namespace AU.GraphTheory.ShortestPath
{
    using AU.GraphTheory.Graph;
    using AU.Lib;

    public class Dijkstra
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

        public Dijkstra()
        {
            Graph = new GraphExtension();
        }

        /// <summary>
        /// The dijkstra's shortest path procedure for a directed &quot; non-negative &quot; weighted graphs.
        /// </summary>
        /// <param name="directedGraph">
        /// a directed graph containing a set V of n vertices 
        /// and a set E of m directed edges with nonnegative weights.
        /// </param>
        /// <param name="sourceVertex">a source vertex in V.</param>
        /// <remarks>
        /// Result:
        ///     For each non-source vertex v in V, shortest[v] is the weight sp(s,v) of a shortest path from s to v and pred(v) is the vertex
        ///     preceding v on some shortest path.
        ///     For the source vertex s, shortest(s) = 0 and pred(s) = NULL.
        ///     If there is no path from s to v, then shortest[v] = infinity, and pred(v) = NULL.
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

            var queue = new PriorityQueue<int>();

            foreach (var v in directedGraph.Vertices)
                queue.Insert(v);

            while (queue.Count > 0)
            {
                var u = queue.ExtractMin();
                double tempShortest;

                foreach (var v in directedGraph[u])
                {
                    tempShortest = Shortest[v];

                    Graph.Relax(directedGraph, u, v);

                    if (tempShortest != Shortest[v])
                        queue.DecreaseKey(v);
                }
            }
        }
    }
}