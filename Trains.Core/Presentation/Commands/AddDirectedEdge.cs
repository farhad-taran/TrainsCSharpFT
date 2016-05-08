using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trains.Core.DataStructures;

namespace Trains.Core.Presentation.Commands
{
    public class AddDirectedEdge : ICommand
    {
        /// <summary>
        /// regex is not performant so we create an static instance using the compiled
        /// flag rather than keep creating a new instance.
        /// </summary>
        static Regex regex = new Regex(@"[A-E]{2}\d{1}", RegexOptions.Compiled);

        readonly IConsoleService consoleService;

        //in a more complex application we could probably hide the graph manipulation inside a application service and 
        //instead inject that into our consumers
        readonly Graph<char> graph;

        public AddDirectedEdge(IConsoleService console, Graph<char> graph)
        {
            this.graph = graph;
            this.consoleService = console;
        }

        public CommandResult Execute()
        {
            consoleService.Write("Please enter command in the following format : a AB5 or AB5,BC6,CD7");
            var input = consoleService.ReadLine();

            var matches = regex.Matches(input);
            if (matches.Count == 0)
                return CommandResult.Fail("Input must be two uppercase characters between A and E followed by an integer cost.eg AE2");

            var inputs = matches.Cast<Match>().Select(x => x.Value).ToList();

            foreach (var item in inputs)
            {
                var predecessorChar = item[0];
                var successorChar = item[1];
                var cost = Convert.ToInt32(item[2].ToString());
                var predecessorNode = graph.GetNode(predecessorChar); 
                var successorNode = graph.GetNode(successorChar);

                if(predecessorNode == null)
                {
                    predecessorNode = new GraphNode<char>(predecessorChar);
                    graph.AddNode(predecessorNode);
                }
                if (successorNode == null)
                {
                    successorNode = new GraphNode<char>(successorChar);
                    graph.AddNode(successorNode);
                }

                graph.AddDirectedEdge(predecessorNode, successorNode, cost);
            }           

            return CommandResult.Ok($"Inserted directed edges for {input}");
        }
    }

    public class CommandResult
    {
        public string Message;
        public bool Success { get; }
        private CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static CommandResult Ok(string message)
        {
            return new CommandResult(true, message);
        }

        public static CommandResult Fail(string message)
        {
            return new CommandResult(false, string.IsNullOrEmpty(message) ? "Invalid input" : message);
        }
    }
}
