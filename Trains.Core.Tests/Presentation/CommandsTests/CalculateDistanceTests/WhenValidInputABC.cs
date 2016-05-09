using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Trains.Core.Presentation.Commands;

namespace Trains.Core.Tests.Presentation.CommandsTests.CalculateDistanceTests
{

    [TestClass]
    public class WhenValidInputABC:CalculateDistanceTestBase
    {
        [TestInitialize]
        public override void BaseInitialize()
        {
            ConsoleService.Setup(x => x.ReadLine()).Returns("d A-B-C");                     
            base.BaseInitialize();
        }

        [TestMethod]
        public void WritesCommandInstructions()
        {
            ConsoleService.Verify(x => x.Write("Please enter command in the following format : d A-B-C"), Times.Once());
        }

        [TestMethod]
        public void ReadsUserInput()
        {
            ConsoleService.Verify(x => x.ReadLine(), Times.Once());
        }

        [TestMethod]
        public void ReturnsOkResult()
        {
            Assert.IsTrue(CommandResult.Success);
            Assert.AreEqual("9",CommandResult.Message);
        }
    }
}