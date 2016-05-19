using System;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Trains.Core.DataStructures;

namespace Trains.Core.Presentation.Commands
{
    public class CommandResult
    {
        public string Message;
        public bool Success { get; }
        private CommandResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public static CommandResult Ok(string message)
        {
            return new CommandResult(true, message);
        }

        public static CommandResult Fail(string message)
        {
            return new CommandResult(false, string.IsNullOrEmpty(message) ? "Invalid input" : message);
        }
    }

}