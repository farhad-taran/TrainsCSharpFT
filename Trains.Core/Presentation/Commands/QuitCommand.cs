using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains.Core.Presentation.Commands
{
    public class QuitCommand : ICommand
    {
        readonly IConsoleService consoleService;
        readonly ICommand decoratee;

        public QuitCommand(IConsoleService consoleService, ICommand decoratee)
        {
            this.decoratee = decoratee;
            this.consoleService = consoleService;
        }

        public CommandResult Execute()
        {
            throw new NotImplementedException();
        }
    }
}
