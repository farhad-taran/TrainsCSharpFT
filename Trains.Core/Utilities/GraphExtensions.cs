using System.Collections.Generic;
using System.Linq;
using Trains.Core.DataStructures;
using Trains.Core.Domain;

namespace Trains.Core.Utilities
{
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
}
