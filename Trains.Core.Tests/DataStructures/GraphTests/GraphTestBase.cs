using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trains.Core.DataStructures;

namespace Trains.Core.Tests.DataStructures.GraphTests
{
    [TestClass]
    public abstract class GraphTestBase
    {
        protected abstract int Size { get; }
        protected abstract Action<Graph> Action { get; }
        protected Graph Graph { get; private set; } 

        [TestInitialize]
        public void BaseInitialize()
        {
            Graph = new Graph(Size);
            Action(Graph);
        }
    }

    [TestClass]
    public class WhenAddingAnEdgeBetweenVerticesOneAndTwo : GraphTestBase
    {
        protected override int Size => 3;

        protected override Action<Graph> Action => graph => graph.AddEdge(1, 2);

        [TestMethod]
        public void ThenHasEdgeReturnsTrueForOneAndTwo()
        {
            Assert.IsTrue(Graph.HasEdge(1, 2));
        }

        [TestMethod]
        public void ThenHasEdgeReturnsFalseForTwoAndThree()
        {
            Assert.IsFalse(Graph.HasEdge(2, 3));
        }

        [TestMethod]
        public void ThenHasEdgeReturnsFalseForOneAndThree()
        {
            Assert.IsFalse(Graph.HasEdge(1, 3));
        }
    }

    [TestClass]
    public class WhenAddingAnEdgeBetweenVerticesTwoAndThree : GraphTestBase
    {
        protected override int Size => 3;

        protected override Action<Graph> Action => graph => graph.AddEdge(2, 3);

        [TestMethod]
        public void ThenHasEdgeReturnsTrueForOneAndTwo()
        {
            Assert.IsTrue(Graph.HasEdge(2, 3));
        }

        [TestMethod]
        public void ThenHasEdgeReturnsFalseForOneAndTwo()
        {
            Assert.IsFalse(Graph.HasEdge(1, 2));
        }

        [TestMethod]
        public void ThenHasEdgeReturnsFalseForOneAndThree()
        {
            Assert.IsFalse(Graph.HasEdge(1, 3));
        }
    }

    [TestClass]
    public class WhenAddingAnEdgeBetweenVerticesOneAndThree : GraphTestBase
    {
        protected override int Size => 3;

        protected override Action<Graph> Action => graph => graph.AddEdge(1, 3);

        [TestMethod]
        public void ThenHasEdgeReturnsTrueForOneAndThree()
        {
            Assert.IsTrue(Graph.HasEdge(1, 3));
        }

        [TestMethod]
        public void ThenHasEdgeReturnsFalseForOneAndTwo()
        {
            Assert.IsFalse(Graph.HasEdge(1, 2));
        }

        [TestMethod]
        public void ThenHasEdgeReturnsFalseForTwoAndThree()
        {
            Assert.IsFalse(Graph.HasEdge(2, 3));
        }
    }

    [TestClass]
    public class WhenGettingSuccessorsOfVeritxWithOneSuccessor : GraphTestBase
    {
        protected override Action<Graph> Action => (graph) =>
        {
            graph.AddEdge(1, 3);
        };

        protected override int Size => 3;

        [TestMethod]
        public void ReturnsOneSuccessor()
        {
            Assert.AreEqual(1, Graph.GetSuccessors(1).Count);
            Assert.AreEqual(3, Graph.GetSuccessors(1)[0]);
        }
    }
}
