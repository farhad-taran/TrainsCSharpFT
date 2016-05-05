using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Trains.Core.DataStructures;

namespace Trains.Core.Tests.DataStructures.GraphTests
{

    [TestClass]
    public class WhenCreatedWithSizeZero
    {

        [TestMethod,ExpectedException(typeof(ArgumentException))]
        public void ThrowsArgumentException()
        {
            var graph = new Graph(0);
        }
    }

}