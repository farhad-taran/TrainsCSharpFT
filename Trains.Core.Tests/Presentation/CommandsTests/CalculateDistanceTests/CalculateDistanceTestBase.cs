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

namespace Trains.Core.Tests.Presentation.CommandsTests.CalculateDistanceTests
{
    [TestClass]
    public class CalculateDistanceTestBase
    {
        protected Mock<IConsoleService> ConsoleService = new Mock<IConsoleService>();

        CalculateDistance command;
        protected CommandResult CommandResult;
        protected Graph<char> Graph = new Graph<char>(5);

        [TestInitialize]
        public virtual void BaseInitialize()
        {
            command = new CalculateDistance(ConsoleService.Object, Graph);
            CommandResult = command.Execute();
        }
    }

    [TestClass]
    public class WhenInvalidInput : CalculateDistanceTestBase
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
    public class WhenValidInput : CalculateDistanceTestBase
    {
        [TestInitialize]
        public override void BaseInitialize()
        {
            ConsoleService.Setup(x => x.ReadLine()).Returns("d A-B-C");
            var a = new Node<char>('A');
            var b = new Node<char>('B');
            var c = new Node<char>('C');
            var d = new Node<char>('D');
            var e = new Node<char>('E');
            Graph.AddNode(a);
            Graph.AddNode(b);
            Graph.AddNode(c);
            Graph.AddNode(d);
            Graph.AddNode(e);
            Graph.ConnectNode(a, b, 5);
            Graph.ConnectNode(b, c, 4);
            Graph.ConnectNode(c, d, 8);
            Graph.ConnectNode(d, c, 8);
            Graph.ConnectNode(d, e, 6);
            Graph.ConnectNode(a, d, 5);
            Graph.ConnectNode(c, e, 2);
            Graph.ConnectNode(e, b, 3);
            Graph.ConnectNode(a, e, 7);
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
            Assert.AreEqual("9", CommandResult.Message);
        }
    }
}
