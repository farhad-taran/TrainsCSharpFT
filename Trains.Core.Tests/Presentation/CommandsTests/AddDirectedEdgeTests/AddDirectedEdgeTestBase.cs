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
        protected Graph<char> Graph = new Graph<char>();

        [TestInitialize]
        public virtual void BaseInitialize()
        {
            command = new AddDirectedEdge(ConsoleService.Object,Graph);
            CommandResult = command.Execute();
        }
    }
}
