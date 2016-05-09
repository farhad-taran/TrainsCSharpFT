using System.Collections.Generic;

namespace Trains.Core.Domain
{

    public class TraversalSearchResult<T>
    {
        public T Node { get; set; }
        public Stack<T> Stack { get; set; }
        public HashSet<T> Visited { get; set; }

        public TraversalSearchResult(T current, Stack<T> stack, HashSet<T> visited)
        {
            this.Node = current;
            this.Stack = stack;
            this.Visited = visited;
        }
    }

}