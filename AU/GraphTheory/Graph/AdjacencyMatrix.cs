namespace AU.GraphTheory.Graph
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AdjacencyMatrix : IGraph
    {
        /// <summary>
        /// The graph internal matrix representation.
        /// </summary>
        private byte[][] Matrix { get; set; }

        /// <summary>
        /// The weights of the edges internal storage.
        /// </summary>
        private Dictionary<int, double?>[] Weights { get; set; }

        public int N { get; private set; }

        public int M { get; private set; }

        public List<int> Vertices
        {
            get
            {
                return new List<int>(Enumerable.Range(0, N));
            }
        }

        public IEnumerable<int> this[int vertex]
        {
            get
            {
                foreach (var u in Vertices)
                    foreach (var v in Vertices)
                        if (Matrix[u][v] == 1)
                            yield return v;
            }
        }

        /// <summary>
        /// The constructor of graph adjacency matrix implementation.
        /// </summary>
        /// <param name="size">The size of the matrix, which is the number of the vertices |V|.</param>
        public AdjacencyMatrix(int size)
        {
            Matrix = new byte[size][];
            N = size;

            Weights = new Dictionary<int, double?>[N];

            for (int i = 0; i < size; ++i)
            {
                Matrix[i] = new byte[size];
                Weights[i] = new Dictionary<int, double?>();
            }
        }

        public void Add(int vertexU, int vertexV, double? weight = null)
        {
            if (!CheckInput(vertexU, vertexV))
                return;

            Matrix[vertexU][vertexV] = 1;
            M++;

            Weights[vertexU][vertexV] = weight;
        }

        public void Remove(int vertexU, int vertexV)
        {
            if (!CheckInput(vertexU, vertexV))
                return;

            Matrix[vertexU][vertexV] = 0;
            M--;

            Weights[vertexU][vertexV] = null;
        }

        public bool Exists(int vertexU, int vertexV)
        {
            if (!CheckInput(vertexU, vertexV))
                return false;

            if (Matrix[vertexU][vertexV] == 0)
                return false;

            return true;
        }

        public double? Weight(int vertexU, int vertexV)
        {
            if (!CheckInput(vertexU, vertexV))
                return null;

            return Weights[vertexU][vertexV];
        }

        private bool CheckInput(int vertexU, int vertexV)
        {
            if (vertexU < 0 || vertexV < 0 || vertexU >= N || vertexV >= N)
                return false;
            return true;
        }

        /// <summary>
        /// An overridden ToString() to visualize the adjacency matrix.
        /// </summary>
        public override string ToString()
        {
            var visualized = new StringBuilder();

            for (int i = 0; i < N; ++i)
                visualized.Append(string.Format("\t({0})", i));

            for (int i = 0; i < N; ++i)
            {
                visualized.Append("\n");

                visualized.Append(string.Format("\n({0}) ", i));

                for (var j = 0; j < N; ++j)
                    visualized.Append(string.Format("\t {0}", Matrix[i][j]));
            }
            return visualized.ToString();
        }
    }
}