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

        [TestInitialize]
        public virtual void BaseInitialize()
        {
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
    public class WhenValidInput:CalculateDistanceTestBase
    {
        [TestInitialize]
        public override void BaseInitialize()
        {
            ConsoleService.Setup(x => x.ReadLine()).Returns("d A-B-C");
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

            var sssss = c.Costs.Select(x => Graph.DepthFirstTraversal(x.Key, 'C').Where(y => y.Node.NodeKey == 'C').ToList()).ToList();

            var sss = GraphExtensions.DepthFirstTraversal(Graph,'C','B')
                .Where(x=>x.Node.NodeKey == 'C');

            foreach (var item in sss.Single().Stack)
            {
                int cost=0;
                if (item.Costs.TryGetValue('C', out cost))
                {
                    int total = cost;

                    // total must be added to first item cost trip to the current item to be total
                    //ie A.cost to B + total
                }
            }

            var ggg = GraphExtensions.DepthFirstTraversal(Graph, 'C')
                .Where(x => x.NodeKey == 'C');

            //
            //The number of different routes from C to C with a distance of less than 30.In the sample data, the trips are: CDC, CEBC, CEBCDC, CDCEBC, CDEBC, CEBCEBC, CEBCEBCEBC.
            // get the neihbors of c 
            // for each neighbor find all the routes to c
            //then query based on less than 30
            
            
             


          var s = new[] {'A','B', 'C' }
            .Select(x => Graph.DepthFirstTraversal(x).Where(y => y.NodeKey == x)
            .ToList());
            

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
}
