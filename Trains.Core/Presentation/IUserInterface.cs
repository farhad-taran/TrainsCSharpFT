using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.Presentation.Commands;
using Trains.DataStructures;

namespace Trains.Core.Presentation
{
    /// <summary>
    /// defines the interface for the application process input and output
    /// </summary>
    interface IUserInterface<TInput>
    {
        bool MapInputToCommand(TInput input, out ICommand command);
        GraphSearchResult<char> ExecuteCommand(ICommand command);
    }
}
