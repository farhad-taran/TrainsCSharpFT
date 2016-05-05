using System;
using System.Collections.Generic;
using System.Linq;

namespace Trains.Core.DataStructures
{
    /// <summary>Represents a directed unweighted graph structure
    /// </summary>
    public class Graph
    {
        // Contains the child nodes for each vertex of the graph
        // assuming that the vertices are numbered 0 ... Size-1
        private List<int>[] _childNodes;

        /// <summary>Constructs an empty graph of given size</summary>
        /// <param name="size">number of vertices</param>
        public Graph(int size)
        {
            if (size == 0)
                throw new ArgumentException("size needs to be larger than zero.");

            _childNodes = new List<int>[size];
            for (int i = 0; i < size; i++)
            {
                // Assing an empty list of adjacents for each vertex
                _childNodes[i] = new List<int>();
            }
        }

        /// <summary>Constructs a graph by given list of
        /// child nodes (successors) for each vertex</summary>
        /// <param name="childNodes">children for each node</param>
        public Graph(List<int>[] childNodes)
        {
            if (childNodes.Any() == false)
                throw new ArgumentException("child nodes cannot be empty.");

            _childNodes = childNodes;
        }

        /// <summary>
        /// Returns the size of the graph (number of vertices)
        /// </summary>
        public int Size
        {
            get { return _childNodes.Length; }
        }

        /// <summary>Adds new edge from u to v</summary>
        /// <param name="u">the starting vertex</param>
        /// <param name="v">the ending vertex</param>
        public void AddEdge(int u, int v)
        {
            _childNodes[u].Add(v);
        }

        /// <summary>Removes the edge from u to v if such exists
        /// </summary>
        /// <param name="u">the starting vertex</param>
        /// <param name="v">the ending vertex</param>
        public void RemoveEdge(int u, int v)
        {
            _childNodes[u].Remove(v);
        }

        /// <summary>
        /// Checks whether there is an edge between vertex u and v
        /// </summary>
        /// <param name="u">the starting vertex</param>
        /// <param name="v">the ending vertex</param>
        /// <returns>true if there is an edge between
        /// vertex u and vertex v</returns>
        public bool HasEdge(int u, int v)
        {
            bool hasEdge = _childNodes[u].Contains(v);
            return hasEdge;
        }

        /// <summary>Returns the successors of a given vertex
        /// </summary>
        /// <param name="v">the vertex</param>
        /// <returns>list of all successors of vertex v</returns>
        public IList<int> GetSuccessors(int v)
        {
            return _childNodes[v];
        }
    }
}