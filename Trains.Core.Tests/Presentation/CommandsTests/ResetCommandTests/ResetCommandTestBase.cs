using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.Presentation;
using Trains.Core.Presentation.Commands;

namespace Trains.Core.Tests.Presentation.CommandsTests.ResetCommandTests
{
    [TestClass]
    public class ResetCommandTestBase
    {
        protected Mock<IConsoleService> ConsoleService = new Mock<IConsoleService>();
        protected Mock<ICommand> DecorateeCommand = new Mock<ICommand>();

        ResetCommand command;
        protected CommandResult CommandResult;

        [TestInitialize]
        public virtual void BaseInitialize()
        {
            command = new ResetCommand(ConsoleService.Object,null);
            CommandResult = command.Execute();
        }
    }
}
