using System;

namespace Trains.Core.Process
{
    public class StringInputProcessor
    {
        public string Process(string input)
        {
            try
            {
                var node = new NeighborsId(input);
            }
            catch (ArgumentNullException anex)
            {
                return anex.Message;
            }
            catch (ArgumentException aex)
            {
                return aex.Message;
            }
            throw new NotImplementedException();
        }
    }
}
