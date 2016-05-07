using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.DataStructures;
using Trains.Core.Presentation;
using Trains.Core.Presentation.Commands;

namespace Trains.Core.Tests.Presentation.ConsoleUserInterfaceTests
{
    [TestClass]
    public class ConsoleUserInterfaceTestBase
    {
        protected ConsoleUserInterface Interface;
        protected virtual IList<MenuItem> MenuItems => new MenuItem[0];

        protected Graph<char> Graph = new Graph<char>(5);

        [TestInitialize]
        public virtual void BaseInitialize()
        {
            Interface = new ConsoleUserInterface(MenuItems);
        }
    }

    [TestClass]
    public class MapInputToCommandTestBase : ConsoleUserInterfaceTestBase
    {
        protected virtual string input => string.Empty;
        protected ICommand command;
        protected bool IsValidInput;

        [TestInitialize]
        public override void BaseInitialize()
        {
            base.BaseInitialize();
            IsValidInput = Interface.MapInputToCommand(input, out command);
        }
    }

    [TestClass]
    public class WhenMapInputToCommandCalledWithInputThatMapsToCommand : MapInputToCommandTestBase
    {
        protected override string input => "a";
        protected override IList<MenuItem> MenuItems => new[]
        {
            new MenuItem("Add a directed edge to the graph using the following sample command : a AB5","a",new AddDirectedEdge(new ConsoleService(),Graph))
        };

        [TestMethod]
        public void ReturnsTrueForIsValidInput()
        {
            Assert.IsTrue(IsValidInput);
        }

        [TestMethod]
        public void ReturnsValidCommand()
        {
            Assert.IsNotNull(command);
        }
    }

    [TestClass]
    public class WhenMapInputToCommandCalledWithInputThatDoesNotMapToCommand : MapInputToCommandTestBase
    {
        protected override string input => "b";
        protected override IList<MenuItem> MenuItems => new[]
        {
            new MenuItem("Add a directed edge to the graph using the following sample command : a AB5","a", new AddDirectedEdge(new ConsoleService(),Graph))
        };

        [TestMethod]
        public void ReturnsFalseForIsValidInput()
        {
            Assert.IsFalse(IsValidInput);
        }

        [TestMethod]
        public void ReturnsValidCommand()
        {
            Assert.IsNull(command);
        }
    }


    [TestClass]
    public class WhenReadCommandCalledWithNoneTerminateCommand : MapInputToCommandTestBase
    {

    }

    [TestClass]
    public class WhenReadCommandCalledWithQuitTerminateCommand : MapInputToCommandTestBase
    {

    }
}
