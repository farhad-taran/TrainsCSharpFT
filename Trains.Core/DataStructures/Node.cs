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

    class Node
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
    }

    class Graph
    {
        private Node _rootNode;

        private static List<Node> _nodes;
        public List<Node> Nodes
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

            _nodes = new List<Node>(size);
            AdjMatrix = new int[size, size];
        }

        private void SetRootNode(Node n)
        {
            if (_rootNode != null)
            {
                throw new Exception("Root node is already set.");
            }
            _rootNode = n;
        }

        public Node GetRootNode()
        {
            return _rootNode;
        }

        public void AddNode(Node node)
        {
            _nodes.Add(node);

            if (node.IsRoot)
            {
                SetRootNode(node);
            }
        }

        public static Node GetTopNeighbor(Node node)
        {
            //edge for top neghbouring node
            return _nodes.FirstOrDefault(c => c.Column == node.Column && c.Row == node.Row - 1);
        }

        public static Node GetLeftNeighbor(Node node)
        {
            return _nodes.FirstOrDefault(c => c.Column == node.Column - 1 && c.Row == node.Row);
        }

        //use this to connect two nodes
        //all nodes must first be added to the graph
        //in order to create a matrix which can represent all the nodes
        public static void ConnectNode(Node start, Node end, int weight)
        {
            int startIndex = _nodes.IndexOf(start);
            int endIndex = _nodes.IndexOf(end);

            AdjMatrix[startIndex, endIndex] = weight;
            AdjMatrix[endIndex, startIndex] = weight;
        }

        private Node GetUnvisitedChildNode(Node n)
        {

            int index = _nodes.IndexOf(n);
            int i = 0;
            while (i < _nodes.Count)
            {
                if (AdjMatrix[index, i] == 1 && _nodes[i].Visited == false)
                {
                    return _nodes[i];
                }
                i++;
            }
            return null;
        }


        public SearchResult DepthFirstSearch(List<Node> nodesToFind)
        {
            if (!nodesToFind.Any())
            {
                throw new Exception("Please provide a list of nodes to search for.");
            }

            var stack = new Stack<Node>();

            var res = new SearchResult();

            stack.Push(_rootNode);

            _rootNode.Visited = true;
            res.History.Add(_rootNode.Label);

            while (stack.Count > 0)
            {
                var node = stack.Peek();
                var child = GetUnvisitedChildNode(node);
                if (child != null)
                {
                    child.Visited = true;
                    res.History.Add(child.Label);

                    if (nodesToFind.Contains(child))
                    {
                        res.Node = child;
                        res.Found = true;
                        break;
                    }
                    res.Steps++;
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

    class SearchResult
    {
        public int Steps { get; set; }
        public Node Node { get; set; }
        public bool Found { get; set; }
        public List<char> History { get; set; }

        public SearchResult()
        {
            History = new List<char>();
        }
    }

}


