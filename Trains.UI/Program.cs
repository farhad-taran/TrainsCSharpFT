using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.DataStructures;
using Trains.Core.Presentation;
using Trains.Core.Presentation.Commands;

namespace Trains.UI
{
    class Program
    {
        static void Main(string[] args)
        {
            var graph = new Graph<char>();
            var consoleService = new ConsoleService();
            consoleService.Write("press esc to exit");
            var menuItems = new[]
            {
                new MenuItem("Reset","r",new ResetCommand(consoleService,graph)),
                new MenuItem("Add directed edge","a",new AddDirectedEdge(consoleService,graph)),
                new MenuItem("Calculate distance","cd",new CalculateDistance(consoleService,graph)),
                new MenuItem("Calculate number of trips","tc",new CalculateNumberOfTrips(consoleService,graph)),
                new MenuItem("Calculate shortest route","sr",new CalculateShortestRoute(consoleService,graph)),
            };

            var consoleUserInterface = new ConsoleUserInterface(menuItems);

            while(true)
            {
                var command = consoleUserInterface.MapInputToCommand(Console.ReadLine());
                if (command != null)
                    consoleService.Write(command.Execute().Message);
            }
        }
    }
}
