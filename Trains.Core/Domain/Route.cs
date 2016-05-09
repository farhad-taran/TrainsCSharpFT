using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.DataStructures;

namespace Trains.Core.Domain
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public class Route<T>
    {
        public T From => from.NodeKey;
        public T To => to.NodeKey;
        public int Cost => cost;
        readonly GraphNode<T> from;
        readonly GraphNode<T> to;
        readonly int cost;

        public Route(GraphNode<T> from, GraphNode<T> to, int cost)
        {
            this.cost = cost;
            this.to = to;
            this.from = from;
        }

        private string DebuggerDisplay => $"from: {from.NodeKey} to: {to.NodeKey} cost: {cost}";
    }
}
