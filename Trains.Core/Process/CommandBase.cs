using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.DataStructures;

namespace Trains.Core.Process
{
    public abstract class OperationBase
    {
        public abstract string Instructions { get; }
    }

    public abstract class CommandBase : OperationBase
    {
        public abstract void process(string input, Graph<char> graph);
    }

    public abstract class QueryBase : OperationBase
    {
        public abstract string process(string input, Graph<char> graph);
    }

}
