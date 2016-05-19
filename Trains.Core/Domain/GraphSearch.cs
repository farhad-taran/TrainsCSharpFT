using System.Collections.Generic;
using System.Linq;
using Trains.Core.DataStructures;
using Trains.Core.Utilities;

namespace Trains.Core.Domain
{
    public static class GraphSearch
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

        public static IList<RouteCost<T>> GetRoutes<T>(this GraphNode<T> source, T destination)
        {
            var allPossibleRoutes = source.GetAllPossibleRoutes(destination);
            var routes = allPossibleRoutes.Select(x =>
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
                .Where(x => x.Count > 0 && source.Neighbors.Any(n => n.NodeKey.Equals(x.First().From)))
                .OrderBy(x => x.Count)
                .Select(x => new RouteCost<T>(source, x))
                .GroupBy(g => new { trips = g.Trips, total = g.TotalCost })
                .Select(x => x.First())
                .ToList();
        }

        public static IEnumerable<TraversalSearchResult<GraphNode<T>>> GetAllPossibleRoutes<T>(this GraphNode<T> source, T destination)
        {
            IEnumerable<SearchResult<T>> searchResults = source.DepthFirstTraversal().ToList();

            List<TraversalSearchResult<GraphNode<T>>> allPossibleRoutes = new List<TraversalSearchResult<GraphNode<T>>>();
            foreach (var result in searchResults)
            {
                allPossibleRoutes.AddRange(result.Visited.SelectMany(x => Search.Traversal(x, n => n.Neighbors).ToList()));
                allPossibleRoutes.AddRange(result.Stack.SelectMany(x => Search.Traversal(x, n => n.Neighbors).ToList()));
            }
            var validRoutes = allPossibleRoutes.Where(x => x.Stack.Peek().NodeKey.Equals(destination));
            return validRoutes;
        }

        public static IEnumerable<SearchResult<T>> GetAllPossibleRoutes<T>(this Graph<T> graph, T destination)
        {
            List<SearchResult<T>> searchResults = new List<SearchResult<T>>();

            foreach (var item in graph.Nodes)
            {
                List<SearchResult<T>> routeResults = item.DepthFirstTraversal()
                    .Where(x => x.CurrentNode.NodeKey.Equals(destination))
                    .ToList();

                var selfVisitingCandidate = routeResults.SingleOrDefault(x => x.Visited.Count == 1);
                var doesNotHaveEdgeToSelf = selfVisitingCandidate != null && 
                    selfVisitingCandidate.Visited.Last().Neighbors.SingleOrDefault(x => x.NodeKey.Equals(destination)) == null;

                if (doesNotHaveEdgeToSelf)
                {
                    routeResults.Remove(selfVisitingCandidate);
                }

                searchResults.AddRange(routeResults);
            }
            return searchResults;
        }

        public static IEnumerable<SearchResult<T>> GetRoutes<T>(this Graph<T> graph, T start, T destination)
        {
            List<SearchResult<T>> searchResults = new List<SearchResult<T>>();

            var startNode = graph.GetNode(start);

            foreach (var item in startNode.Neighbors)
            {
                List<SearchResult<T>> routeResults = item.DepthFirstTraversal()
                    .Where(x => x.CurrentNode.NodeKey.Equals(destination))
                    .ToList();
                searchResults.AddRange(routeResults);
            }
            return searchResults;
        }

        public static ShortestRoute GetShortestRoute<T>(this Graph<T> graph, T start, T destination)
        {
            var startNode = graph.GetNode(start);
            var searchResults = graph.GetRoutes(start, destination);
            var shortesRout = searchResults.OrderBy(x => x.Visited.Count).FirstOrDefault();
            if (shortesRout != null)
            {
                var shortestRouteCost = shortesRout.GetTotalCost();
                var startToNeighborCost = startNode.Costs[shortesRout.Visited.First().NodeKey];
                int totalCost = shortestRouteCost + startToNeighborCost;
                var routeTrips = shortesRout.Visited.Count + 1;
                return new ShortestRoute(totalCost, routeTrips);
            }
            return null;
        }

        public static int GetTotalCost<T>(this SearchResult<T> searchResult)
        {
            var visitedNodes = searchResult.Visited.Select(x => x).ToArray();
            int total = 0;
            for (int i = 1; i < visitedNodes.Length; i++)
            {
                var prevNode = visitedNodes[i - 1];
                var currNode = visitedNodes[i];
                total += prevNode.Costs[currNode.NodeKey];
            }
            return total;
        }
    }  
}
