using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trains.Core.DataStructures;
using Trains.DataStructures;

namespace Trains.Core.Presentation.Commands
{
    public class CalculateNumberOfTrips : ICommand
    {
        private readonly IConsoleService consoleService;
        private readonly Graph<char> graph;

        /// <summary>
        /// regex is not performant so we create an static instance using the compiled
        /// flag rather than keep creating a new instance.
        /// </summary>
        static Regex commandRegex = new Regex(@"tc\s[A-E]-[A-E]\sM\d+", RegexOptions.Compiled);

        static Regex nodesRegex = new Regex(@"[A-E]");

        public CalculateNumberOfTrips(IConsoleService console, Graph<char> graph)
        {
            this.graph = graph;
            this.consoleService = console;
        }

        public CommandResult Execute()
        {
            consoleService.Write("Please enter command in the following formats : tc C-C M3 or tc C-C E3");
            var input = consoleService.ReadLine();
            var countMaximumMatch = commandRegex.Match(input);
            if(countMaximumMatch.Success==false)
            {
                return CommandResult.Fail("Invalid command");
            }

            var chars = input.ToCharArray();
            var firstNodeChar = chars[3];
            var lastNodeChar = chars[5];
            int tripsCount = int.Parse(chars[8].ToString());

            var startNode = graph.GetNode(firstNodeChar);
            var searchResult = startNode
                .DepthFirstTraversal().ToList();

            var cost = searchResult
               .GetRoutes(lastNodeChar);

            var trips = cost.Where(x => x.StartsAt(firstNodeChar) && x.EndsAt(lastNodeChar) && x.Trips <= tripsCount);

            var message = $"{trips.Count()}";

            return CommandResult.Ok(message);
        }
    }
}
