using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trains.Core.Process;

namespace Trains.Core.Tests.Process.StringInputProcessorTests
{
    [TestClass]
    public abstract class StringInputProcessorTests
    {
        protected StringInputProcessor StringInputProcessor = new StringInputProcessor();
        protected string Output;
        protected abstract string Input { get; }

        [TestInitialize]
        public virtual void BaseInitialize()
        {
            Output = StringInputProcessor.Process(Input);
        }
    }

    [TestClass]
    public class WhenStringInputIsNull : StringInputProcessorTests
    {
        protected override string Input => null;

        [TestMethod]
        public void ReturnsMessage()
        {
            Assert.IsNotNull(Output);
        }
    }

    [TestClass]
    public class WhenStringInputIsInWrongFormat : StringInputProcessorTests
    {
        protected override string Input => "abcd";

        [TestMethod]
        public void ReturnsMessage()
        {
            Assert.IsNotNull(Output);
        }
    }
}
