using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.DataStructures;

namespace Trains.Core.Presentation.Commands
{
    public class ResetCommand:ICommand
    {
        readonly IConsoleService consoleService;
        readonly Graph<char> graph;

        public ResetCommand(IConsoleService consoleService, Graph<char> graph)
        {
            this.graph = graph;
            this.consoleService = consoleService;
        }

        public CommandResult Execute()
        {
            consoleService.Write("Clearing nodes from graph");
            graph.Nodes.Clear();
            return CommandResult.Ok("All nodes cleared");
        }
    }
}
