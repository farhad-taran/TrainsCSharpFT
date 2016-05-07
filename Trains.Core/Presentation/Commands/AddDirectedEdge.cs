using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trains.Core.DataStructures;

namespace Trains.Core.Presentation.Commands
{
    public class AddDirectedEdge:ICommand
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
            consoleService.Write("Please enter command in the following format : a AB5");
            var input = consoleService.ReadLine();

            var match = regex.Match(input);
            if (match.Success == false)
                return CommandResult.Fail("Input must be two uppercase characters between A and E followed by an integer cost.eg AE2");
            var predecessorChar = input[0];
            var successorChar = input[1];
            var cost = Convert.ToInt32(input[2].ToString());
            var predecessorNode = new Node<char>(predecessorChar);
            var successorNode = new Node<char>(successorChar);

            graph.AddNode(predecessorNode);
            graph.AddNode(successorNode);
            graph.ConnectNode(predecessorNode, successorNode, cost);

            return CommandResult.Ok($"Inserted directed edge {input}");
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
            return new CommandResult(true,message);
        }

        public static CommandResult Fail(string message)
        {
            return new CommandResult(false,message);
        }
    }
}
