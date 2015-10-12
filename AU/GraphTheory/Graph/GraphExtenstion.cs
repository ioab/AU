namespace AU.GraphTheory.Graph
{
    public class GraphExtension
    {
        /// <summary>
        /// the container for the shortest path edges weights.
        /// </summary>
        public double[] Shortest { get; set; }

        /// <summary>
        /// the container for the shortest path predecessors (path indicators).
        /// </summary>
        public int?[] Predecessors { get; set; }

        /// <summary>
        /// The graph shortest path relax procedure.
        /// Inputs:
        ///     u, v: vertices such that there is an edge (u, v).
        /// Result:
        ///     The value of shortest[v] might decrease, and if it does, then pred[v] becomes u.
        /// </summary>
        public void Relax(IGraph dag, int u, int v)
        {
            var uPath = (dag.Weight(u, v) ?? double.PositiveInfinity) + Shortest[u];

            if (uPath < Shortest[v])
            {
                Shortest[v] = uPath;
                Predecessors[v] = u;
            }
        }
    }
}