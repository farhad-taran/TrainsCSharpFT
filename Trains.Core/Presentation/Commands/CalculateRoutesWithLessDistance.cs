using System.Text.RegularExpressions;
using Trains.Core.DataStructures;
using Trains.Core.Domain;

namespace Trains.Core.Presentation.Commands
{
    public class CalculateRoutesWithLessDistance : ICommand
    {
        private readonly IConsoleService consoleService;
        private readonly Graph<char> graph;

        static Regex commandRegex = new Regex(@"nr\s*([A-E]){1}(-[A-E]{1})*\s\d+", RegexOptions.Compiled);

        public CalculateRoutesWithLessDistance(IConsoleService console, Graph<char> graph)
        {
            this.graph = graph;
            this.consoleService = console;
        }

        public CommandResult Execute()
        {
            consoleService.Write("Please enter command in the following formats : nr C-C 30");
            string input = consoleService.ReadLine();
            Match match = commandRegex.Match(input);
            if (match.Success == false)
                return CommandResult.Fail("");

            var chars = input.ToCharArray();
            var firstNodeChar = chars[3];
            var lastNodeChar = chars[5];

            var startNode = graph.GetNode(firstNodeChar);

            var shortestRoute = graph.GetShortestRoute(firstNodeChar, lastNodeChar);

            var s = graph.GetAllPossibleRoutes(firstNodeChar, lastNodeChar);

            var message = $"{shortestRoute.Total}";

            return CommandResult.Ok(message);
        }
    }
}
