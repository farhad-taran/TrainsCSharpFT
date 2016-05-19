using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.DataStructures;
using Trains.Core.Domain;
using Trains.Core.Utilities;

namespace Trains.Core.Domain
{
    public static class GraphSearch
    {
        public static IEnumerable<SearchResult<T>> GetAllPossibleRoutes<T>(this Graph<T> graph, T destination)
        {
            List<SearchResult<T>> searchResults = new List<SearchResult<T>>();

            foreach (var item in graph.Nodes)
            {
                List<SearchResult<T>> routeResults = item.DepthFirstTraversal()
                    .Where(x => x.CurrentNode.NodeKey.Equals(destination))
                    .ToList();
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
