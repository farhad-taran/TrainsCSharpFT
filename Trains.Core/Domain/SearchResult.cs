using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.DataStructures;

namespace Trains.Core.Domain
{

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
}
