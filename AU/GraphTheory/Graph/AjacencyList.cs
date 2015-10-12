namespace AU.GraphTheory.Graph
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;

    public class AdjacencyList : IGraph
    {
        /// <summary>
        /// The graph internal list representation.
        /// </summary>
        private List<List<int>> List { get; set; }

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
            get { return List[vertex]; }
        }

        /// <summary>
        /// The constructor of graph adjacency list implementation.
        /// </summary>
        /// <param name="size">The size of the List, which is the number of the vertices |V|.</param>
        public AdjacencyList(int size)
        {
            List = new List<List<int>>(size);
            N = size;

            Weights = new Dictionary<int, double?>[N];

            for (int i = 0; i < size; ++i)
            {
                List.Add(new List<int>());
                Weights[i] = new Dictionary<int, double?>();
            }
        }

        public void Add(int vertexU, int vertexV, double? weight = null)
        {
            if (!CheckInput(vertexU, vertexV))
                return;

            List[vertexU].Add(vertexV);
            M++;

            Weights[vertexU][vertexV] = weight;
        }

        public void Remove(int vertexU, int vertexV)
        {
            if (!CheckInput(vertexU, vertexV))
                return;

            List[vertexU].Remove(vertexV);
            M--;

            Weights[vertexU][vertexV] = null;
        }

        public bool Exists(int vertexU, int vertexV)
        {
            if (!List[vertexU].Exists(v => v == vertexV))
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
        /// An overridden ToString() to visualize the adjacency list.
        /// </summary>
        public override string ToString()
        {
            var visualized = new StringBuilder();

            for (int i = 0; i < N; ++i)
            {
                visualized.Append(string.Format("\n({0}) |\t", i));

                foreach (var j in List[i])
                    visualized.Append(string.Format(" {0}", j));
            }
            return visualized.ToString();
        }
    }
}