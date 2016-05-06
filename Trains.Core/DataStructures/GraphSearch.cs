using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.DataStructures;

namespace Trains.DataStructures
{
    /// <summary>
    /// since we dont have any state we can fall back to using static classes
    /// and methods for the search operation, if we need to mock these function 
    /// calls then we can use delgates that represent the signature of these methods
    /// in conjuntion with a mocking framework or provide a fake implementation in code,
    /// although it is preferable to only mock functions that could potentially cause any 
    /// substantial delay (network, or I/O calls) in process that runs the tests and if that is not
    /// an issue then just allow your tests to excercise the overall state of the program by 
    /// letting your test code excercise all the layers involved, another way that we can make the process of
    /// testing classes that utalize this static method easier is to make use of a result class that
    /// encapsulates the result of each call to this method, we can then stub this result class in our
    /// tests and create the different state that we require for each of our unit tests
    /// </summary>
    public static class GraphSearch
    {
        public static GraphSearchResult<T> MakeDepthFirstSearch<T>(T startNode, Graph<T> graph)
        {
            if (graph == null)
                return GraphSearchResult<T>.NotFound("Graph is null.");
            if (graph.Any() == false)
                return GraphSearchResult<T>.NotFound("Graph is empty.");

            GraphNode<T> root = graph.GetNode(startNode);

            if (root == null)
                return GraphSearchResult<T>.NotFound("Graph does not contain node.");

            var nodes = DepthFirstSearch(root, graph);

            return GraphSearchResult<T>.Found(root, nodes);
        }

        private static IEnumerable<GraphNode<T>> DepthFirstSearch<T>(GraphNode<T> startNode, Graph<T> graph)
        {
            var visited = new HashSet<GraphNode<T>>();
            var stack = new Stack<GraphNode<T>>();
            stack.Push(startNode);
            while (stack.Count != 0)
            {
                var current = stack.Pop();

                if (!visited.Add(current))
                    continue;

                yield return current;

                var neighbours = current.Neighbors
                                      .Where(n => !visited.Contains(n));

                // If you don't care about the left-to-right order, remove the Reverse
                foreach (var neighbour in neighbours.Reverse())
                    stack.Push(neighbour);
            }
        }
    }

    public class GraphSearchResult<T>
    {
        public GraphNode<T> RootNode { get; }
        public bool Success { get; }
        public string Message { get; }
        public int NeighborsCount { get; }
        public int AvailableRoutesCount { get; }

        private Dictionary<T, int> routeCosts;


        private GraphSearchResult(GraphNode<T> rootNode, IEnumerable<GraphNode<T>> routes, string message)
        {
            RootNode = rootNode;
            Success = (rootNode != null && string.IsNullOrWhiteSpace(message));
            Message = message;
            NeighborsCount = RootNode == null ? 0 : RootNode.Neighbors.Count;
            routeCosts = CalculateRouteCosts(rootNode, routes);
            AvailableRoutesCount = routeCosts.Count;
        }

        private Dictionary<T, int> CalculateRouteCosts(GraphNode<T> rootNode, IEnumerable<GraphNode<T>> routes)
        {
            var calculatedRouteCosts = new Dictionary<T, int>();
            if (Success == false)
            {
                return calculatedRouteCosts;
            }

            foreach (var node in routes)
            {
                if (node.NodeKey.Equals(rootNode.NodeKey) == false)
                {
                    var totalCost = routes
                        .TakeWhile(x => x.NodeKey.Equals(node.NodeKey) == false)
                        .Sum(x => x.Costs.Sum(y => y.Value));
                    calculatedRouteCosts.Add(node.NodeKey, totalCost);
                }
            }
            return calculatedRouteCosts;
        }

        public int? GetRouteCost(T nodeKey)
        {
            int cost;
            return (routeCosts.TryGetValue(nodeKey, out cost)) ? cost : (int?)null;
        }

        internal static GraphSearchResult<T> NotFound(string message)
        {
            return new GraphSearchResult<T>(null, null, message);
        }

        internal static GraphSearchResult<T> Found(GraphNode<T> root, IEnumerable<GraphNode<T>> routes)
        {
            return new GraphSearchResult<T>(root, routes, null);
        }
    }
}
