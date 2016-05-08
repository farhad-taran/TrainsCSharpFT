using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.DataStructures;

namespace Trains.DataStructures
{
    /// <summary>
    /// 
    /// note to reader: I usually do not like to put comments in my code unless I am doing something really complex
    /// or using a framework that is not used by many people in the industry, and also I would like to believe 
    /// that my code can be easily read and undrestood by anyone who comes across it but for the purposes
    /// of this excerices I felt it necessary so to describe the intention behind the implementation.
    /// 
    /// 
    /// since the actual work of searching is not really part of the data structure itself, I didnt feel like it
    /// should be part of that class and is a seprate concern, also we dont have any state and we only have a single
    /// public method that defines the interface of this class to its consumers, and currently we only implement a 
    /// single search strategy which is depth first, so we only have a single implementation of a conceptual interface,
    /// we can fall back to using static classes and methods for the search operation, if we need to mock these
    /// function calls then we can use delgates that represent the signature of these methods
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


        public static GraphSearchResult<T> MakeDepthFirstSearch<T>(this Graph<T> graph, T startNode)
        {
            if (graph == null)
                return GraphSearchResult<T>.Fail("Graph is null.");
            if (graph.Any() == false)
                return GraphSearchResult<T>.Fail("Graph is empty.");

            GraphNode<T> root = graph.GetNode(startNode);

            if (root == null)
                return GraphSearchResult<T>.Fail("Graph does not contain node.");

            var nodes = DepthFirstSearch(root, graph);

            return GraphSearchResult<T>.Ok(root, nodes);
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

                var neighbours = current.Neighbors.Where(n => !visited.Contains(n));

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

        internal static GraphSearchResult<T> Fail(string message)
        {
            return new GraphSearchResult<T>(null, null, message);
        }

        internal static GraphSearchResult<T> Ok(GraphNode<T> root, IEnumerable<GraphNode<T>> routes)
        {
            return new GraphSearchResult<T>(root, routes, null);
        }
    }

    public static class GraphExtensions
    {
        public static IEnumerable<SearchResult<T>> DepthFirstTraversal<T>(this GraphNode<T> start)
        {
            List<SearchResult<T>> results = new List<SearchResult<T>>();
            var visited = new HashSet<GraphNode<T>>();
            var stack = new Stack<GraphNode<T>>();
            stack.Push(start);
            while (stack.Count != 0)
            {
                var current = stack.Pop();
                if (!visited.Add(current))
                    continue;

                yield return new SearchResult<T>(start, current, new Stack<GraphNode<T>>(stack), new HashSet<GraphNode<T>>(visited));
                var neighbours = current.Neighbors.Where(n => !visited.Contains(n));

                foreach (var neighbour in neighbours)
                {
                    stack.Push(neighbour);
                }
            }
        }

    }


    public class SearchResult<T>
    {
        public GraphNode<T> CurrentNode { get; }
        public GraphNode<T> StartNode { get; }
        public Stack<GraphNode<T>> Stack { get; set; }
        public HashSet<GraphNode<T>> Visited { get; set; }

        public SearchResult(GraphNode<T> start, GraphNode<T> current, Stack<GraphNode<T>> stack, HashSet<GraphNode<T>> visited)
        {
            this.CurrentNode = current;
            this.Stack = stack;
            this.Visited = visited;
            this.StartNode = start;
        }
    }

    public static class SearchResultExtensions
    {
        public static IList<RouteCost<T>> GetRoutes<T>(this IEnumerable<SearchResult<T>> searchResults, T destination)
        {
            var startOfRoute = searchResults.FirstOrDefault()?.Visited.First();
            List<Search.SearchResult<GraphNode<T>>> allPossibleRoutes = new List<Search.SearchResult<GraphNode<T>>>();
            foreach (var result in searchResults)
            {
                allPossibleRoutes.AddRange(result.Visited.SelectMany(x => Search.Traversal2(x, n => n.Neighbors).ToList()));
                allPossibleRoutes.AddRange(result.Stack.SelectMany(x => Search.Traversal2(x, n => n.Neighbors).ToList()));
            }
            var validRoutes = allPossibleRoutes.Where(x => x.Stack.Peek().NodeKey.Equals(destination));
            var routes = validRoutes.Select(x =>
            {
                var routeCostsList = x.Visited.SelectWithPrevious((prev, curr) =>
                {
                    int total = 0;
                    int prevToCurrentCost;
                    if (prev.Costs.TryGetValue(curr.NodeKey, out prevToCurrentCost))
                    {
                        total += prevToCurrentCost;
                    }
                    return new Route<T>(prev, curr, prevToCurrentCost);
                }).ToList();
                return routeCostsList;
            }).ToList();
            return routes
                .Where(x=>x.Count > 0 && startOfRoute.Neighbors.Any(n => n.NodeKey.Equals(x.First().From)))
                .OrderBy(x => x.Count)
                .Select(x => new RouteCost<T>(startOfRoute, x))
                .GroupBy(g=>new { trips = g.Trips, total = g.TotalCost})
                .Select(x=>x.First())
                .ToList();
        }

        public static RouteCost<T> GetCostByIds<T>(this IEnumerable<RouteCost<T>> routeCosts, IEnumerable<T> ids)
        {
            return routeCosts.Where(x => x.IsOnRoute(ids)).FirstOrDefault();
        }
    }


    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Route<T>
    {
        public T From => from.NodeKey;
        public T To => to.NodeKey;
        public int Cost => cost;
        readonly GraphNode<T> from;
        readonly GraphNode<T> to;
        readonly int cost;

        public Route(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            this.cost = cost;
            this.to = to;
            this.from = from;
        }

        private string DebuggerDisplay => $"from: {from.NodeKey} to: {to.NodeKey} cost: {cost}";
    }

    public static class Extensions
    {
        public static IEnumerable<TResult> SelectWithPrevious<TSource, TResult>
    (this IEnumerable<TSource> source,
     Func<TSource, TSource, TResult> projection)
        {
            using (var iterator = source.GetEnumerator())
            {
                if (!iterator.MoveNext())
                {
                    yield break;
                }
                TSource previous = iterator.Current;
                while (iterator.MoveNext())
                {
                    yield return projection(previous, iterator.Current);
                    previous = iterator.Current;
                }
            }
        }
    }
}
