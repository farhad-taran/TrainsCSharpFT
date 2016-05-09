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
            //this is the composition root of our application
            //ideally we should not be newing up or instantiating from within our classes
            // and the object graph should be visible from this location so that all dependencies are visible
            // IOC containers work in the same manner by bringing composition into a central place

            var graph = new Graph<char>();
            var consoleService = new ConsoleService();
            consoleService.Write("Enter q to exit");
            var menuItems = new[]
            {
                new MenuItem("Reset","r",new ResetCommand(consoleService,graph)),
                new MenuItem("Add directed edge","a",new AddDirectedEdge(consoleService,graph)),
                new MenuItem("Calculate distance","cd",new CalculateDistance(consoleService,graph)),
                new MenuItem("Calculate number of trips","tc",new CalculateNumberOfTrips(consoleService,graph)),
                new MenuItem("Calculate shortest route","sr",new CalculateShortestRoute(consoleService,graph)),
                new MenuItem("Calculate shortest route","nr",new CalculateRoutesWithLessDistance(consoleService,graph)),
            };

            menuItems.Select(x => $"{x.Key} : {x.Caption}")
                .ToList()
                .ForEach(x => consoleService.Write(x));

            while (true)
            {
                var input = consoleService.ReadLine();
                if(input =="q")
                {
                    Environment.Exit(0);
                }
                var command = menuItems.SingleOrDefault(x => x.Key == input)?.Command;
                if (command != null)
                    consoleService.Write(command.Execute().Message);
            }
        }

    }
}
