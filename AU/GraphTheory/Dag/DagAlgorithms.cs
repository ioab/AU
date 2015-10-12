namespace AU.GraphTheory.Dag
{
    using AU.GraphTheory.Graph;
    using System.Collections.Generic;

    public class DagAlgorithms
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

        public DagAlgorithms()
        {
            Graph = new GraphExtension();
        }

        /// <summary>
        /// The topological sort procedure for DAG.
        /// </summary>
        /// <param name="dag">a directed acyclic graph (DAG) with vertices numbered 1 to n.</param>
        /// <returns>
        /// A linear order of the vertices such that u appears before v in the linear order,
        /// if (u, v) is an edge in the graph.
        /// </returns>
        public static List<int> TopologicalSort(IGraph dag)
        {
            // the initialization (to 0) is done by default //
            int[] inDegree = new int[dag.N];

            var linear = new List<int>();

            var next = new Stack<int>();

            foreach (var u in dag.Vertices)
                foreach (var v in dag[u])
                {
                    if (dag.Exists(u, v))
                        inDegree[v]++;
                }

            for (int i = 0; i < inDegree.Length; ++i)
            {
                if (inDegree[i] == 0)
                    next.Push(i);
            }

            while (next.Count > 0)
            {
                var u = next.Pop();
                linear.Add(u);

                foreach (var v in dag.Vertices)
                {
                    if (dag.Exists(u, v))
                        inDegree[v]--;

                    if (!linear.Contains(v) && !next.Contains(v) && inDegree[v] == 0)
                        next.Push(v);
                }
            }

            return linear;
        }

        /// <summary>
        /// The DAG shortest path procedure.
        /// </summary>
        /// <param name="dag">a weighted directed acyclic graph containing a set V of n vertices and a set E of m directed edges.</param>
        /// <param name="sourceVertix">a source vertex in V.</param>
        /// <remarks>
        /// For each non-source vertex v in V, shortest[v] is the weight sp(s,v) of a shortest path from s to v and pred(v) is the vertex
        /// preceding v on some shortest path.
        /// For the source vertex s, shortest(s) = 0 and pred(s) = NULL.
        /// If there is no path from s to v, then shortest[v] = infinity, and pred(v) = NULL.
        /// </remarks>
        public void ShortestPath(IGraph dag, int sourceVertix)
        {
            var linear = TopologicalSort(dag);

            Graph.Shortest = new double[dag.N];
            Graph.Predecessors = new int?[dag.N];

            for (int i = 0; i < Shortest.Length; ++i)
            {
                Shortest[i] = double.PositiveInfinity;
                Graph.Predecessors[i] = null;
            }

            Shortest[sourceVertix] = 0;

            foreach (var u in linear)
            {
                foreach (var v in dag.Vertices)
                    if (dag.Exists(u, v))
                        Graph.Relax(dag, u, v);
            }
        }
    }
}