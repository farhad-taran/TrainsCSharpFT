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

namespace Trains.Core.Tests.Presentation.CommandsTests.AddDirectedEdgeTests
{
    [TestClass]
    public class AddDirectedEdgeTestBase
    {
        protected Mock<IConsoleService> ConsoleService = new Mock<IConsoleService>();

        AddDirectedEdge command;
        protected CommandResult CommandResult;
        protected Graph<char> Graph = new Graph<char>(5);

        [TestInitialize]
        public virtual void BaseInitialize()
        {
            command = new AddDirectedEdge(ConsoleService.Object,Graph);
            CommandResult = command.Execute();
        }
    }

    [TestClass]
    public class WhenUserInputIsInvalid: AddDirectedEdgeTestBase
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
            ConsoleService.Verify(x => x.Write("Please enter command in the following format : a AB5"), Times.Once());
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

        [TestMethod]
        public void DoesNotAddAnyItemsToGraph()
        {
            Assert.IsFalse(Graph.Nodes.Any());
        }
    }

    [TestClass]
    public class WhenUserInputIsValid:AddDirectedEdgeTestBase
    {
        [TestInitialize]
        public override void BaseInitialize()
        {
            ConsoleService.Setup(x => x.ReadLine()).Returns("AB5");
            base.BaseInitialize();
        }

        [TestMethod]
        public void WritesCommandInstructions()
        {
            ConsoleService.Verify(x => x.Write("Please enter command in the following format : a AB5"), Times.Once());
        }

        [TestMethod]
        public void ReadsUserInput()
        {
            ConsoleService.Verify(x => x.ReadLine(), Times.Once());
        }

        [TestMethod]
        public void ReturnsSuccessResult()
        {
            Assert.IsTrue(CommandResult.Success);
            Assert.AreEqual("Inserted directed edge AB5", CommandResult.Message);
        }

        [TestMethod]
        public void AddsDirectedEdgeToGraph()
        {
            Assert.AreEqual(2, Graph.Nodes.Count);
            Assert.AreEqual(5, Graph.GetCost('A','B'));
        }
    }
}
