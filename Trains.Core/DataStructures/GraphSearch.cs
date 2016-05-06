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
    /// letting your test code excercise all the layers involved
    /// </summary>
    public static class GraphSearch
    {
        public static GraphSearchResult<T> MakeDepthFirstSearch<T>(T value, Graph<T> graph)
        {
            if (graph == null)
                return GraphSearchResult<T>.NotFound("Graph is null.");
            if (graph.Any() == false)
                return GraphSearchResult<T>.NotFound("Graph is empty.");

            GraphNode<T> startNode;
            var startNode = graph.Contains(value,out startNode);

            var visited = new HashSet<GraphNode<T>>();
            var stack = new Stack<GraphNode<T>>();

            stack.Push(startNode)

            return GraphSearchResult<T>.Found(new GraphNode<T>());

            throw new NotImplementedException();
        }
    }

    public class GraphSearchResult<T>
    {
        public GraphNode<T> GraphNode { get; }
        public bool Success { get; }
        public string Message { get; }
        public int NeighborsCount { get; }

        private GraphSearchResult(GraphNode<T> graphNode,string message)
        {
            GraphNode = graphNode;
            Success = (graphNode != null && string.IsNullOrWhiteSpace(message));
            Message = message;
            NeighborsCount = GraphNode == null ? 0 : GraphNode.Neighbors.Count;
        }

        internal static GraphSearchResult<T> NotFound(string message)
        {
            return new GraphSearchResult<T>(null,message);
        }

        internal static GraphSearchResult<T> Found(GraphNode<T> node)
        {
            return new GraphSearchResult<T>(node,null);
        }
    }
}
