using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.Process;

namespace Trains.Core.Tests.Process.NodeIdTests
{
    [TestClass]
    public abstract class NeighborsIdTestBase
    {
        protected NeighborsId NeighborsId;
        protected abstract string NodeIdString { get; }

        [TestInitialize]
        public virtual void BaseInitialize()
        {
            NeighborsId = new NeighborsId(NodeIdString);
        }
    }

    [TestClass]
    public class WhenNodeIdIsEmptyThrowsArgumentException : NeighborsIdTestBase
    {
        protected override string NodeIdString => string.Empty;

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public override void BaseInitialize()
        {
            base.BaseInitialize();
        }
    }

    [TestClass]
    public class WhenNodeIdIsNullThrowsArgumentNullException : NeighborsIdTestBase
    {
        protected override string NodeIdString => null;

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public override void BaseInitialize()
        {
            base.BaseInitialize();
        }
    }

    [TestClass]
    public class WhenNodeIdIsNotInCorrectFormatThrowsArgumentException : NeighborsIdTestBase
    {
        protected override string NodeIdString => "1s2";

        [TestMethod, ExpectedException(typeof(ArgumentException))]
        public override void BaseInitialize()
        {
            base.BaseInitialize();
        }
    }

    [TestClass]
    public class WhenNeighborsIdConstructedWithValidString : NeighborsIdTestBase
    {
        protected override string NodeIdString => "AE2";

        [TestMethod]
        public void HasPredecessorNode()
        {
            Assert.AreEqual('A', NeighborsId.PredecessorNode);
        }

        [TestMethod]
        public void HasSuccessorNode()
        {
            Assert.AreEqual('E', NeighborsId.SuccessorNode);
        }

        [TestMethod]
        public void HasCost()
        {
            Assert.AreEqual(2, NeighborsId.Cost);
        }
    }
}
