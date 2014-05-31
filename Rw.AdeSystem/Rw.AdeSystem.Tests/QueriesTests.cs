using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rw.AdeSystem.Core.Queries;
using SbsSW.SwiPlCs;

namespace Rw.AdeSystem.Tests
{
    [TestClass]
    public class QueriesTests
    {
        private const string yaleProblem = @"initially h & a & w
                                            always h <-> !m
                                            always w -> a
                                            CHOWN by Miętus causes h
                                            CHOWN by Hador causes m
                                            SHOOT by Miętus causes !a if m
                                            SHOOT by Hador typically causes !a if h
                                            ENTICE by Hador causes w
                                            ENTICE by Miętus typically causes w
                                            ENTICE by Hador preserves a";
        [TestMethod]
        public void Test()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(yaleProblem);

            //Act
            var query = new PossiblyExecutableQuery("possibly executable ENTICE by Hador from !a & !w");


            //Assert

            PlEngine.PlCleanup();
        }
    }
}
