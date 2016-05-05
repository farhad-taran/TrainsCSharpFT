using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trains.Core.DataStructures;

namespace Trains.Core.Tests.DataStructures.GraphTests
{

    [TestClass]
    public class WhenCreatedUsingValidChildNodesListThenCallingSize
    {
        [TestMethod]
        public void ThenItReturnsTheRightSize()
        {
            var graph = new Graph(new List<int>[2]);
            Assert.AreEqual(2, graph.Size);
        }
    }

}