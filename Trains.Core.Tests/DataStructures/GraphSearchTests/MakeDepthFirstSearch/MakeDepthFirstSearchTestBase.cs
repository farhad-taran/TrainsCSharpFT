using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.DataStructures;
using Trains.DataStructures;

namespace Trains.Core.Tests.DataStructures.GraphSearchTests.MakeDepthFirstSearch
{
    [TestClass]
    public class MakeDepthFirstSearchTestBase
    {
        protected GraphSearchResult<char> GraphSearchResult;

        protected virtual char NodeToFind => ' ';

        protected Graph<char> NewEmptyGraph => new Graph<char>(new NodeList<char>());

        [TestInitialize]
        public void BaseInitialize()
        {
            GraphSearchResult = GraphSearch.MakeDepthFirstSearch<char>(NodeToFind, MakeGraph());
        }

        protected virtual Graph<char> MakeGraph()
        {
            return NewEmptyGraph;
        }
    }

    [TestClass]
    public class WhenGraphIsEmpty : MakeDepthFirstSearchTestBase
    {
        protected override Graph<char> MakeGraph()
        {
            return NewEmptyGraph;
        }

        [TestMethod]
        public void SetsFoundFalse()
        {
            Assert.IsFalse(GraphSearchResult.Success);
        }

        [TestMethod]
        public void SetsMessage()
        {
            Assert.AreEqual("Graph is empty.", GraphSearchResult.Message);
        }
    }

    [TestClass]
    public class WhenGraphIsNull : MakeDepthFirstSearchTestBase
    {
        protected override Graph<char> MakeGraph()
        {
            return null;
        }

        [TestMethod]
        public void SetsFoundFalse()
        {
            Assert.IsFalse(GraphSearchResult.Success);
        }

        [TestMethod]
        public void SetsMessage()
        {
            Assert.AreEqual("Graph is null.", GraphSearchResult.Message);
        }
    }

    [TestClass]
    public class WhenGraphIsNotEmptyButDoesNotContainStartNode : MakeDepthFirstSearchTestBase
    {
        protected override char NodeToFind => 'Z';
        protected override Graph<char> MakeGraph()
        {
            var graph = NewEmptyGraph;
            var a = new GraphNode<char>('A');
            var b = new GraphNode<char>('B');
            graph.AddNode(a);
            graph.AddNode(b);
            graph.AddDirectedEdge(a, b, 5);
            return graph;
        }

        [TestMethod]
        public void SetsFoundFalse()
        {
            Assert.IsFalse(GraphSearchResult.Success);
        }

        [TestMethod]
        public void SetsMessage()
        {
            Assert.AreEqual("Graph does not contain node.", GraphSearchResult.Message);
        }
    }

    [TestClass]
    public class WhenOnlyOneDirectedEdgeInGraphAndNodeIsFirst : MakeDepthFirstSearchTestBase
    {
        protected override char NodeToFind => 'A';

        protected override Graph<char> MakeGraph()
        {
            var graph = NewEmptyGraph;
            var a = new GraphNode<char>('A');
            var b = new GraphNode<char>('B');
            graph.AddNode(a);
            graph.AddNode(b);
            graph.AddDirectedEdge(a, b, 5);
            return graph;
        }

        [TestMethod]
        public void SetsFoundTrue()
        {
            Assert.IsTrue(GraphSearchResult.Success);
        }

        [TestMethod]
        public void SetsNeighborsCount()
        {
            Assert.AreEqual(1, GraphSearchResult.NeighborsCount);
        }
    }

    [TestClass]
    public class WhenThreeNodesInGraphAndFirstNodeIsRoot : MakeDepthFirstSearchTestBase
    {
        protected override char NodeToFind => 'A';
        protected override Graph<char> MakeGraph()
        {
            var graph = NewEmptyGraph;
            var a = new GraphNode<char>('A');
            var b = new GraphNode<char>('B');
            var c = new GraphNode<char>('C');
            graph.AddNode(a);
            graph.AddNode(b);
            graph.AddNode(c);
            graph.AddDirectedEdge(a, b, 5);
            graph.AddDirectedEdge(b, c, 4);
            return graph;
        }

        [TestMethod]
        public void SetsFoundTrue()
        {
            Assert.IsTrue(GraphSearchResult.Success);
        }

        [TestMethod]
        public void SetsNeighborsCount()
        {
            Assert.AreEqual(2, GraphSearchResult.NeighborsCount);
        }

        [TestMethod]
        public void SetsRouteCostDictionary()
        {
            Assert.AreEqual(1, GraphSearchResult.RouteCosts.Count);
        }
    }

    [TestClass]
    public class WhenThreeNodesInGraphAndMiddleNodeIsRoot : MakeDepthFirstSearchTestBase
    {
        protected override char NodeToFind => 'B';
        protected override Graph<char> MakeGraph()
        {
            var graph = NewEmptyGraph;
            var a = new GraphNode<char>('A');
            var b = new GraphNode<char>('B');
            var c = new GraphNode<char>('C');
            graph.AddNode(a);
            graph.AddNode(b);
            graph.AddNode(c);
            graph.AddDirectedEdge(a, b, 5);
            graph.AddDirectedEdge(b, c, 4);
            return graph;
        }

        [TestMethod]
        public void SetsFoundTrue()
        {
            Assert.IsTrue(GraphSearchResult.Success);
        }

        [TestMethod]
        public void SetsNeighborsCount()
        {
            Assert.AreEqual(2, GraphSearchResult.NeighborsCount);
        }

        [TestMethod]
        public void SetsRouteCostDictionary()
        {
            Assert.AreEqual(1, GraphSearchResult.RouteCosts.Count);
        }
    }
}
