using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.DataStructures;
using Trains.Core.Presentation;
using Trains.Core.Presentation.Commands;
using Trains.DataStructures;

namespace Trains.Core.Tests.Presentation.CommandsTests.CalculateDistanceTests
{
    [TestClass]
    public class CalculateDistanceTestBase
    {
        protected Mock<IConsoleService> ConsoleService = new Mock<IConsoleService>();

        CalculateDistance command;
        protected CommandResult CommandResult;
        protected Graph<char> Graph = new Graph<char>();

        protected virtual void SetUpGraph()
        {
            var a = new GraphNode<char>('A');
            var b = new GraphNode<char>('B');
            var c = new GraphNode<char>('C');
            var d = new GraphNode<char>('D');
            var e = new GraphNode<char>('E');
            Graph.AddNode(a);
            Graph.AddNode(b);
            Graph.AddNode(c);
            Graph.AddNode(d);
            Graph.AddNode(e);
            Graph.AddDirectedEdge(a, b, 5);
            Graph.AddDirectedEdge(b, c, 4);
            Graph.AddDirectedEdge(c, d, 8);
            Graph.AddDirectedEdge(d, c, 8);
            Graph.AddDirectedEdge(d, e, 6);
            Graph.AddDirectedEdge(a, d, 5);
            Graph.AddDirectedEdge(c, e, 2);
            Graph.AddDirectedEdge(e, b, 3);
            Graph.AddDirectedEdge(a, e, 7);
        }

        protected virtual void SetUpConsoleService(){}

        [TestInitialize]
        public virtual void BaseInitialize()
        {
            SetUpGraph();
            SetUpConsoleService();
            command = new CalculateDistance(ConsoleService.Object, Graph);
            CommandResult = command.Execute();
        }
    }

    [TestClass]
    public class WhenInvalidInput: CalculateDistanceTestBase
    {
        [TestInitialize]
        public override void BaseInitialize()
        {
            ConsoleService.Setup(x => x.ReadLine()).Returns("invalid input");
            base.BaseInitialize();
        }

        [TestMethod]
        public void WritesCommandInstructions()
        {
            ConsoleService.Verify(x => x.Write("Please enter command in the following format : d A-B-C"), Times.Once());
        }

        [TestMethod]
        public void ReadsUserInput()
        {
            ConsoleService.Verify(x => x.ReadLine(), Times.Once());
        }

        [TestMethod]
        public void ReturnsFailResult()
        {
            Assert.IsFalse(CommandResult.Success);
        }
    }

    [TestClass]
    public class WhenValidInputABC:CalculateDistanceTestBase
    {
        [TestInitialize]
        public override void BaseInitialize()
        {
            ConsoleService.Setup(x => x.ReadLine()).Returns("d A-B-C");                     
            base.BaseInitialize();
        }

        [TestMethod]
        public void WritesCommandInstructions()
        {
            ConsoleService.Verify(x => x.Write("Please enter command in the following format : d A-B-C"), Times.Once());
        }

        [TestMethod]
        public void ReadsUserInput()
        {
            ConsoleService.Verify(x => x.ReadLine(), Times.Once());
        }

        [TestMethod]
        public void ReturnsOkResult()
        {
            Assert.IsTrue(CommandResult.Success);
            Assert.AreEqual("9",CommandResult.Message);
        }
    }

    [TestClass]
    public class WhenValidInputADC : CalculateDistanceTestBase
    {
        [TestInitialize]
        public override void BaseInitialize()
        {
            ConsoleService.Setup(x => x.ReadLine()).Returns("d A-D-C");
            base.BaseInitialize();
        }

        [TestMethod]
        public void WritesCommandInstructions()
        {
            ConsoleService.Verify(x => x.Write("Please enter command in the following format : d A-B-C"), Times.Once());
        }

        [TestMethod]
        public void ReadsUserInput()
        {
            ConsoleService.Verify(x => x.ReadLine(), Times.Once());
        }

        [TestMethod]
        public void ReturnsOkResult()
        {
            Assert.IsTrue(CommandResult.Success);
            Assert.AreEqual("13", CommandResult.Message);
        }
    }

    [TestClass]
    public class WhenValidInputAEBCD: CalculateDistanceTestBase
    {
        [TestInitialize]
        public override void BaseInitialize()
        {
            ConsoleService.Setup(x => x.ReadLine()).Returns("d A-E-B-C-D");
            base.BaseInitialize();
        }

        [TestMethod]
        public void WritesCommandInstructions()
        {
            ConsoleService.Verify(x => x.Write("Please enter command in the following format : d A-B-C"), Times.Once());
        }

        [TestMethod]
        public void ReadsUserInput()
        {
            ConsoleService.Verify(x => x.ReadLine(), Times.Once());
        }

        [TestMethod]
        public void ReturnsOkResult()
        {
            Assert.IsTrue(CommandResult.Success);
            Assert.AreEqual("22", CommandResult.Message);
        }
    }
}
