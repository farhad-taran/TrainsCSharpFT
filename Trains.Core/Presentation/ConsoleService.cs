using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trains.Core.Presentation
{
    /// <summary>
    /// a facde to allow for testing what is being written or read from the console,
    /// here I prefer to use an interface because I know all my commands will require to use both
    /// of the methods in the facade and it would be easier to inject a single item into the commands 
    /// rather than injecting two delegates, one for writing and one for reading
    /// </summary>
    public interface IConsoleService
    {
        void Write(string message);
        string ReadLine();
    }

    public class ConsoleService : IConsoleService
    {
        public string ReadLine()
        {
            return Console.ReadLine();
        }

        public void Write(string message)
        {
            Console.WriteLine(message);
        }
    }
}
