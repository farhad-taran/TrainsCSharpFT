using System;
using System.Collections.Generic;
using Trains.Core.Domain;

namespace Trains.Core.DataStructures
{
    public class Search
    {
        public static IEnumerable<TraversalSearchResult<T>> Traversal<T>(T item, Func<T, IEnumerable<T>> children)
        {
            var seen = new HashSet<T>();
            var stack = new Stack<T>();
            seen.Add(item);
            stack.Push(item);
            yield return new TraversalSearchResult<T>(item, new Stack<T>(stack), new HashSet<T>(seen));
            while (stack.Count > 0)
            {
                T current = stack.Pop();
                foreach (T newItem in children(current))
                {
                    if (!seen.Contains(newItem))
                    {
                        seen.Add(newItem);
                        stack.Push(newItem);
                        yield return new TraversalSearchResult<T>(item, new Stack<T>(stack), new HashSet<T>(seen)); 
                    }
                }
            }
        }

    }
}
