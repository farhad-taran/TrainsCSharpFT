using System.Text.RegularExpressions;
using Trains.Core.DataStructures;
using Trains.Core.Domain;

namespace Trains.Core.Presentation.Commands
{
    public class CalculateShortestRoute : ICommand
    {
        private readonly IConsoleService consoleService;
        private readonly Graph<char> graph;

        /// <summary>
        /// regex is not performant so we create an static instance using the compiled
        /// flag rather than keep creating a new instance.
        /// </summary>
        static Regex commandRegex = new Regex(@"sr\s[A-E]-[A-E]", RegexOptions.Compiled);

        public CalculateShortestRoute(IConsoleService console, Graph<char> graph)
        {
            this.graph = graph;
            this.consoleService = console;
        }

        public CommandResult Execute()
        {
            consoleService.Write("Please enter command in the following formats : sr A-C");
            string input = consoleService.ReadLine();
            Match match = commandRegex.Match(input);
            if (match.Success == false)
                return CommandResult.Fail("");

            var chars = input.ToCharArray();
            var firstNodeChar = chars[3];
            var lastNodeChar = chars[5];

            var startNode = graph.GetNode(firstNodeChar);

            var shortestRoute = graph.GetShortestRoute(firstNodeChar, lastNodeChar);
            
            var message = $"{shortestRoute.Total}";

            return CommandResult.Ok(message);
        }
    }
}
