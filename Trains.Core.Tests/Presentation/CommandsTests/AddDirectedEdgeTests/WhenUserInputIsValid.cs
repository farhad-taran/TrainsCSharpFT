using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.DataStructures;
using Trains.Core.Presentation;
using Trains.Core.Presentation.Commands;

namespace Trains.Core.Tests.Presentation.CommandsTests.AddDirectedEdgeTests
{

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
            ConsoleService.Verify(x => x.Write("Please enter command in the following format : a AB5 or AB5,BC6,CD7"), Times.Once());
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
            Assert.AreEqual("Inserted directed edges for AB5", CommandResult.Message);
        }

        [TestMethod]
        public void AddsDirectedEdgeToGraph()
        {
            Assert.AreEqual(2, Graph.Count);
            Assert.AreEqual(5, Graph.GetNode('A').Costs['B']);
        }
    }
}