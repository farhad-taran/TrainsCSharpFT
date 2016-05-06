using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains.Core.DataStructures
{
    public class GraphNode<T> 
    {
        private List<int> costs;
        private T data;
        private List<GraphNode<T>> neighbors = null;

        public GraphNode(T value)
        {
            this.data = value;
            this.neighbors = new List<GraphNode<T>>();
            this.costs = new List<int>();
        }

        public List<GraphNode<T>> Neighbors => neighbors;
        public List<int> Costs => costs;
        public T Value => data;
    }
}
