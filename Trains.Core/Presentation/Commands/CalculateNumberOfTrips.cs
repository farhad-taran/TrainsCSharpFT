using System.Linq;
using System.Text.RegularExpressions;
using Trains.Core.DataStructures;
using Trains.Core.Utilities;

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
        static Regex maximumRegex = new Regex(@"tc\s[A-E]-[A-E]\sM\d+", RegexOptions.Compiled);
        static Regex exactRegex = new Regex(@"tc\s[A-E]-[A-E]\sE\d+", RegexOptions.Compiled);


        public CalculateNumberOfTrips(IConsoleService console, Graph<char> graph)
        {
            this.graph = graph;
            this.consoleService = console;
        }

        public CommandResult Execute()
        {
            consoleService.Write("Please enter command in the following formats : tc C-C M3 or tc C-C E3");
            var input = consoleService.ReadLine();
            var countMaximumMatch = maximumRegex.Match(input);
            var countExactMatch = exactRegex.Match(input);
            if (countMaximumMatch.Success == false && countExactMatch.Success == false)
            {
                return CommandResult.Fail("Invalid command");
            }

            var chars = input.ToCharArray();
            var firstNodeChar = chars[3];
            var lastNodeChar = chars[5];
            int tripsCount = int.Parse(chars[8].ToString());
            bool maxTrips = chars[7] == 'M';

            var startNode = graph.GetNode(firstNodeChar);
            var cost = startNode.GetRoutes(lastNodeChar);

            var trips = cost.Where(
                x =>
                x.StartsAt(firstNodeChar) &&
                x.EndsAt(lastNodeChar));

            var count = maxTrips ? 
                trips.Where(x => x.Trips <= tripsCount) : 
                trips.Where(x => x.Trips == tripsCount);

            var message = $"{trips.Count()}";

            return CommandResult.Ok(message);
        }
    }
}
