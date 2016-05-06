using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains.Core.DataStructures
{
    public class Graph<T>
    {
        private List<GraphNode<T>> nodeSet;

        public Graph() : this(null) { }
        public Graph(List<GraphNode<T>> nodeSet)
        {
            if (nodeSet == null)
                this.nodeSet = new List<GraphNode<T>>();
            else
                this.nodeSet = nodeSet;
        }

        public void AddNode(GraphNode<T> node)
        {
            // adds a node to the graph
            nodeSet.Add(node);
        }

        public void AddDirectedEdge(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            from.Neighbors.Add(to);
            from.Costs.Add(cost);
        }

        public bool Any()
        {
            return nodeSet.Any();
        }

        public GraphNode<T> GetNode(T value)
        {
            return nodeSet.SingleOrDefault(x => x.Value.Equals(value));
        }

        public List<GraphNode<T>> Nodes
        {
            get
            {
                return nodeSet;
            }
        }

        public int Count
        {
            get { return nodeSet.Count; }
        }
    }
}
