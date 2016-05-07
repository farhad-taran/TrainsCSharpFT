using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trains.Core.Presentation.Commands;
using Trains.DataStructures;

namespace Trains.Core.Presentation
{


    public interface ICommandResult
    {
    }

    public class ConsoleUserInterface : IUserInterface<string>
    {
        readonly IList<MenuItem> menuItems;

        public ConsoleUserInterface(IList<MenuItem> menuItems)
        {
            this.menuItems = menuItems;
        }

        public GraphSearchResult<char> ExecuteCommand(ICommand command)
        {
            throw new NotImplementedException();
        }

        public bool MapInputToCommand(string input, out ICommand command)
        {
            command = menuItems.SingleOrDefault(x => x.Key == input)?.Command;
            return command != null;
        }
    }

    public class MenuItem
    {
        readonly ICommand command;
        readonly string caption;
        readonly string key;

        public MenuItem(string caption,string key,ICommand command)
        {
            this.key = key;
            this.caption = caption;
            this.command = command;
        }

        public string Key => key;
        public ICommand Command => command;
    }
}
