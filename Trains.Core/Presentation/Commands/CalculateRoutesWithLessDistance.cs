using System;
using System.Linq;
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
        static Regex numbersRegex = new Regex(@"\d+", RegexOptions.Compiled);

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
                return CommandResult.Fail("invalid input");

            var chars = input.ToCharArray();
            var firstNodeChar = chars[3];
            var lastNodeChar = chars[5];
            int distance = int.Parse(numbersRegex.Match(input).Value);

            var allPossibleRoutes = graph.GetAllPossibleRoutes(lastNodeChar)
                .Where(x => x.Visited.Count < distance);

            var s = allPossibleRoutes
                .Select(x =>$"{x.CurrentNode.NodeKey},{string.Join(",", x.Visited.Select(y=>y.NodeKey))}")
                .OrderBy(x=>x.Length);

            var message = $"{allPossibleRoutes.Count()}{Environment.NewLine}{string.Join(Environment.NewLine,s)}";

            return CommandResult.Ok(message);
        }
    }
}
