using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.DataStructures;
using Trains.DataStructures;

namespace Trains.Core.DataStructures
{
    public class Search
    {
        public static IEnumerable<T> Traversal<T>(T item, Func<T, IEnumerable<T>> children)
        {
            var seen = new HashSet<T>();
            var stack = new Stack<T>();
            seen.Add(item);
            stack.Push(item);
            yield return item;
            while (stack.Count > 0)
            {
                T current = stack.Pop();
                foreach (T newItem in children(current))
                {
                    if (!seen.Contains(newItem))
                    {
                        seen.Add(newItem);
                        stack.Push(newItem);
                        yield return newItem;
                    }
                }
            }
        }

        public static IEnumerable<SearchResult<T>> Traversal2<T>(T item, Func<T, IEnumerable<T>> children)
        {
            var seen = new HashSet<T>();
            var stack = new Stack<T>();
            seen.Add(item);
            stack.Push(item);
            yield return new SearchResult<T>(item, new Stack<T>(stack), new HashSet<T>(seen));
            while (stack.Count > 0)
            {
                T current = stack.Pop();
                foreach (T newItem in children(current))
                {
                    if (!seen.Contains(newItem))
                    {
                        seen.Add(newItem);
                        stack.Push(newItem);
                        yield return new SearchResult<T>(item, new Stack<T>(stack), new HashSet<T>(seen)); 
                    }
                }
            }
        }

        public class SearchResult<T>
        {
            public T Node { get; set; }
            public Stack<T> Stack { get; set; }
            public HashSet<T> Visited { get; set; }

            public SearchResult(T current, Stack<T> stack, HashSet<T> visited)
            {
                this.Node = current;
                this.Stack = stack;
                this.Visited = visited;
            }
        }
    }
}
