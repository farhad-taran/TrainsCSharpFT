using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains.Core.DataStructures
{
    public class Node<T>
    {
        private T data;
        private NodeList<T> neighbors = null;

        public Node(T data) : this(data, null) { }
        public Node(T data, NodeList<T> neighbors)
        {
            this.data = data;
            this.neighbors = neighbors;
        }

        public T Value
        {
            get
            {
                return data;
            }
            set
            {
                data = value;
            }
        }

        public bool Visited { get; internal set; }

        protected NodeList<T> Neighbors
        {
            get
            {
                return neighbors;
            }
            set
            {
                neighbors = value;
            }
        }
    }

    public class Node
    {
        public bool Visited { get; set; }

        public bool IsRoot { get; set; }

        private readonly char _label;
        public char Label
        {
            get { return _label; }
        }

        private readonly int _column;
        public int Column
        {
            get { return _column; }
        }

        private readonly int _row;
        public int Row
        {
            get { return _row; }
        }

        public Node(char c, int column, int row, bool isRoot)
        {
            _label = c;
            _column = column;
            _row = row;
            IsRoot = isRoot;
        }

        public Node(char c)
        {
            _label = c;
        }
    }

    public class Graph<T>
    {
        private static List<Node<T>> _nodes;
        public List<Node<T>> Nodes
        {
            get { return _nodes; }
        }

        //Edges, connections or the lines that connect the nodes together will be represented as adjacency Matrix
        //   A B C 
        // A 0 1 0
        // B 1 0 1
        // C 1 1 0
        public static int[,] AdjMatrix;

        public Graph(int size)
        {
            //set the capacity to 1000 nodes as stated in the instructions
            if (size > 1000)
            {
                throw new ArgumentException("Maximum size for the graph is 1000.");
            }

            _nodes = new List<Node<T>>(size);
            AdjMatrix = new int[size, size];
        }

        public void AddNode(Node<T> node)
        {
            _nodes.Add(node);
        }

        public int GetCost(T start, T end)
        {
            var startNodeIdx = _nodes.IndexOf(_nodes.SingleOrDefault(x => x.Value.Equals(start)));
            var endNodeIdx = _nodes.IndexOf(_nodes.SingleOrDefault(x => x.Value.Equals(end)));
            return AdjMatrix[startNodeIdx, endNodeIdx];
        }

        public object GetNode(T v)
        {
            throw new NotImplementedException();
        }

        //use this to connect two nodes
        //all nodes must first be added to the graph
        //in order to create a matrix which can represent all the nodes
        public void ConnectNode(Node<T> start, Node<T> end, int weight)
        {
            int startIndex = _nodes.IndexOf(start);
            int endIndex = _nodes.IndexOf(end);

            AdjMatrix[startIndex, endIndex] = weight;
            AdjMatrix[endIndex, startIndex] = weight;
        }

        private Node<T> GetUnvisitedChildNode(Node<T> n)
        {

            int index = _nodes.IndexOf(n);
            int i = 0;
            while (i < _nodes.Count)
            {
                if (AdjMatrix[index, i] > 0 && _nodes[i].Visited == false)
                {
                    return _nodes[i];
                }
                i++;
            }
            return null;
        }

        public SearchResult<T> DepthFirstSearch(T start,List<T> nodesToFind)
        {
            if (!nodesToFind.Any())
            {
                throw new Exception("Please provide a list of nodes to search for.");
            }

            var stack = new Stack<Node<T>>();

            var res = new SearchResult<T>();

            var startNode = _nodes.FirstOrDefault(x => x.Value.Equals(start));

            stack.Push(startNode);

            startNode.Visited = true;
            res.History.Add(startNode);

            while (stack.Count > 0)
            {
                var node = stack.Peek();
                var child = GetUnvisitedChildNode(node);
                if (child != null)
                {
                    child.Visited = true;
                    res.History.Add(child);

                    if (nodesToFind.Contains(child.Value))
                    {
                        res.Node = child;
                        res.Found = true;
                        ++res.Steps;
                        break;
                    }
                    stack.Push(child);
                }
                else
                {
                    stack.Pop();
                }
            }
            return res;
        }
    }

    public class SearchResult<T>
    {
        public int Steps { get; set; }
        public Node<T> Node { get; set; }
        public bool Found { get; set; }
        public List<Node<T>> History { get; set; }

        public SearchResult()
        {
            History = new List<Node<T>>();
        }
    }

}


