using AU.GraphTheory.Dag;
using AU.GraphTheory.Graph;
using AU.GraphTheory.ShortestPath;
using System;

namespace Demo
{
    internal class GraphTheory
    {
        public static void Demo()
        {
            Dag();

            Dikstra();

            BellmanFord();
        }

        public static void Dag()
        {
            var dag = new AdjacencyMatrix(5);
            dag.Add(0, 2);
            dag.Add(1, 4);
            dag.Add(6, 3); // Bad Input .. nothing //
            dag.Add(3, 4);
            Console.WriteLine("\n " + dag);

            Console.WriteLine("Topological Sort result :");
            var l = DagAlgorithms.TopologicalSort(dag);
            l.ForEach((i) => Console.WriteLine(i));

            var dag2 = new AdjacencyList(5);
            dag2.Add(0, 2, 1);
            dag2.Add(1, 4, 10);
            dag2.Add(2, 3, 5);
            dag2.Add(3, 1, 2);
            dag2.Add(3, 4, 1);
            Console.WriteLine(dag2);

            Console.WriteLine("\t Vertices: " + dag2.N + "\n\t Edges: " + dag2.M);
            Console.WriteLine("\nTopological Sort result :");
            var l1 = DagAlgorithms.TopologicalSort(dag2);
            l1.ForEach((i) => Console.WriteLine(i));

            // test removing an edge .. //
            dag2.Remove(1, 4);
            Console.WriteLine(dag2);
            Console.WriteLine("\nTopological Sort result :");
            var l2 = DagAlgorithms.TopologicalSort(dag2);
            l2.ForEach((i) => Console.WriteLine(i));

            Console.WriteLine("Shortest Path: ");
            var dagAlgo = new DagAlgorithms();
            dagAlgo.ShortestPath(dag2, 0);

            for (int i = 0; i < dagAlgo.Shortest.Length; i++)
                Console.WriteLine("\t {0} +  -> Weights : [{1}]", i, dagAlgo.Shortest[i]);
            

            Console.WriteLine("\nPredessors: ");
            for (int i = 0; i < dagAlgo.Predecessors.Length; i++)
            {
                var pred = dagAlgo.Predecessors[i];
                Console.WriteLine("pred[{0}] : {1}", i, pred != null ? pred.ToString() : "NULL");
            }

            var dagTime = new AdjacencyList(5); // Or AdjacencyMatrix(5) //
            dagTime.Add(1, 2, 2.5);
            dagTime.Add(1, 3, 5);
            dagTime.Add(3, 4, 3.25);
            //dagTime.Add(4, 0, null); // if e.g.: Unreachable in P time //
            dagTime.Add(4, 0, 10.5);
            Console.WriteLine(dagTime);

            Console.WriteLine("\nShortest Path: ");
            var dagAlgo2 = new DagAlgorithms();
            dagAlgo2.ShortestPath(dagTime, 1);

            for (int i = 0; i < dagAlgo2.Shortest.Length; i++)
                Console.WriteLine("\t {0} +  -> Weights : [{1}]", i, dagAlgo2.Shortest[i]);

            Console.WriteLine("\nPredessors: ");
            for (int i = 0; i < dagAlgo2.Predecessors.Length; i++)
            {
                var pred = dagAlgo2.Predecessors[i];
                Console.WriteLine("pred[{0}] : {1}", i, pred != null ? pred.ToString() : "NULL");
            }
        }

        private static void Dikstra()
        {
            var directedGraph = new AdjacencyList(5);
            directedGraph.Add(1, 2, 1);
            directedGraph.Add(1, 1, 4);         // Dijkstra accepts cycles, But not negative weights //
            directedGraph.Add(2, 3, 5);
            directedGraph.Add(3, 4, 10);
            directedGraph.Add(4, 0, 2);

            var dijskra = new Dijkstra();
            dijskra.Compute(directedGraph, 1);
            for (int i = 0; i < dijskra.Shortest.Length; i++)
                Console.WriteLine("\t {0} +  -> Weights : [{1}]", i, dijskra.Shortest[i]);
            
            Console.WriteLine("\nPredessors: ");
            for (int i = 0; i < dijskra.Predecessors.Length; i++)
            {
                var pred = dijskra.Predecessors[i];
                Console.WriteLine("pred[{0}] : {1}", i, pred != null ? pred.ToString() : "NULL");
            }
        }

        private static void BellmanFord()
        {
            var directedGraph = new AdjacencyList(5);
            directedGraph.Add(1, 2, 1);
            directedGraph.Add(1, 1, 4);         // bellman-ford accepts cycles, And negative weights too. //
            directedGraph.Add(2, 3, -5);
            directedGraph.Add(3, 4, -2);
            directedGraph.Add(4, 0, 2);
            directedGraph.Add(4, 3, -2);

            var bF = new BellmanFord();
            bF.Compute(directedGraph, 1);
            for (int i = 0; i < bF.Shortest.Length; i++)
                Console.WriteLine("\t {0} +  -> Weights : [{1}]", i, bF.Shortest[i]);

            Console.WriteLine("\nPredessors: ");
            for (int i = 0; i < bF.Predecessors.Length; i++)
            {
                var pred = bF.Predecessors[i];
                Console.WriteLine("pred[{0}] : {1}", i, pred != null ? pred.ToString() : "NULL");
            }

            Console.WriteLine("Negative Cycle :");
            bF.FindNegativeWeightCycle(directedGraph).ForEach(i => Console.Write(i + " -> "));
            Console.WriteLine();
        }
    }
}