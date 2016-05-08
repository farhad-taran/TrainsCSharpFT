using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains.Core.DataStructures
{
    [DebuggerDisplay("{data}")]
    public class GraphNode<T> 
    {
        private Dictionary<T,int> costs;
        private T data;
        private List<GraphNode<T>> neighbors = null;

        public GraphNode(T value)
        {
            this.data = value;
            this.neighbors = new List<GraphNode<T>>();
            this.costs = new Dictionary<T, int>();
        }

        public List<GraphNode<T>> Neighbors => neighbors;
        public Dictionary<T, int> Costs => costs;
        public T NodeKey => data;
    }
}
