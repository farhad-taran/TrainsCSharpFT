﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trains.Core.DataStructures;
using Trains.DataStructures;

namespace Trains.Core.Presentation.Commands
{
    public class CalculateDistance : ICommand
    {
        private readonly IConsoleService consoleService;
        private readonly Graph<char> graph;

        /// <summary>
        /// regex is not performant so we create an static instance using the compiled
        /// flag rather than keep creating a new instance.
        /// </summary>
        static Regex commandRegex = new Regex(@"d\s*([A-E]){1}(-[A-E]{1})*", RegexOptions.Compiled);

        static Regex nodesRegex = new Regex(@"[A-E]");

        public CalculateDistance(IConsoleService console, Graph<char> graph)
        {
            this.graph = graph;
            this.consoleService = console;
        }

        public CommandResult Execute()
        {
            consoleService.Write("Please enter command in the following format : d A-B-C");
            string input = consoleService.ReadLine();
            Match match = commandRegex.Match(input);
            if (match.Success == false)
                return CommandResult.Fail("");

            var nodes = nodesRegex.Matches(input).Cast<Match>().Select(x => char.Parse(x.Value)).ToList();
            var firstChar = nodes.First();
            var lastChar = nodes.Last();

            var startNode = graph.GetNode(firstChar);
            var searchResult = startNode
                .DepthFirstTraversal()
                .Where(x => x.CurrentNode.NodeKey == lastChar);

            var routes = searchResult
                .GetRoutes(lastChar)
                .ByIds(nodes);     

            return CommandResult.Ok($"{routes.TotalCost}");
        }        
    }
}
