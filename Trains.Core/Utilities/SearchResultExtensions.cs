using System.Collections.Generic;
using System.Linq;
using Trains.Core.DataStructures;
using Trains.Core.Domain;

namespace Trains.Core.Utilities
{
    public static class SearchResultExtensions
    {
        public static IList<RouteCost<T>> GetRoutes<T>(this IEnumerable<SearchResult<T>> searchResults, T destination)
        {
            var startOfRoute = searchResults.FirstOrDefault()?.Visited.First();
            List<TraversalSearchResult<GraphNode<T>>> allPossibleRoutes = new List<TraversalSearchResult<GraphNode<T>>>();
            foreach (var result in searchResults)
            {
                allPossibleRoutes.AddRange(result.Visited.SelectMany(x => Search.Traversal(x, n => n.Neighbors).ToList()));
                allPossibleRoutes.AddRange(result.Stack.SelectMany(x => Search.Traversal(x, n => n.Neighbors).ToList()));
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
                .Where(x => x.Count > 0 && startOfRoute.Neighbors.Any(n => n.NodeKey.Equals(x.First().From)))
                .OrderBy(x => x.Count)
                .Select(x => new RouteCost<T>(startOfRoute, x))
                .GroupBy(g => new { trips = g.Trips, total = g.TotalCost })
                .Select(x => x.First())
                .ToList();
        }
    }
}
