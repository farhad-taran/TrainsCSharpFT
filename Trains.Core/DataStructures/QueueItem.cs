using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains.Core.DataStructures
{
    public class QueueItem<T>
    {
        public GraphNode<T> Node { get; private set; }
        public HashSet<GraphNode<T>> Visited { get; private set; }

        public QueueItem(GraphNode<T> node, HashSet<GraphNode<T>> visited)
        {
            Node = node;
            Visited = visited;
        }
    }
}
