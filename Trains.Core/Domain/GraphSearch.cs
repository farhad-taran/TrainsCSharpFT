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
    /// 

    public static class GraphSearch
    {
        public static IEnumerable<SearchResult<T>> GetAllPossibleRoutes<T>(this Graph<T> graph, T start, T destination)
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
