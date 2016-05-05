using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Trains.Core.Process
{
    /// <summary>
    /// value type to parse and store the common input string,
    /// which represent a set of neibours(directed vertices or nodes that are connected)
    /// </summary>
    public class NeighborsId
    {
        /// <summary>
        /// regex is not performant so we create an static instance using the compiled
        /// flag rather than keep creating a new instance.
        /// </summary>
        static Regex regex = new Regex(@"[A-E]{2}\d{1}", RegexOptions.Compiled); 

        public char PredecessorNode { get; }
        public char SuccessorNode { get; }
        public int Cost { get; }

        public NeighborsId(string nodeId)
        {
            if (nodeId == null)
                throw new ArgumentNullException($"{nameof(nodeId)} cannot be null.");
            var match = regex.Match(nodeId);
            if (match.Success == false)
                throw new ArgumentException("input must be two uppercase characters between A and E followed by an integer cost.eg AE2.");
            PredecessorNode = nodeId[0];
            SuccessorNode = nodeId[1];
            Cost = Convert.ToInt32(nodeId[2].ToString());
        }
    }
}
