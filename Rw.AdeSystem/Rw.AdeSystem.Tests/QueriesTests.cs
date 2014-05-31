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
                                            CHOWN by Mietus causes h
                                            CHOWN by Hador causes m
                                            SHOOT by Mietus causes !a if m
                                            SHOOT by Hador typically causes !a if h
                                            ENTICE by Hador causes w
                                            ENTICE by Mietus typically causes w
                                            ENTICE by Hador preserves a";
        [TestMethod]
        public void Test01()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(yaleProblem);

            //Act
            var query = new PossiblyExecutableQuery("possibly executable ENTICE by Hador from !a & !w");
            var result = query.ToProlog();

            //Assert
            Assert.Equals(result, "False");

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test02()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(yaleProblem);

            //Act
            var query = new AlwaysExecutableQuery("always executable ENTICE by Hador from !a & !w");
            var result = query.ToProlog();

            //Assert
            Assert.Equals(result, "False");

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test03()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(yaleProblem);

            //Act
            var query = new PossiblyAccessibleQuery("possibly accessible a from !a");
            var result = query.ToProlog();

            //Assert
            Assert.Equals(result, "True");

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test04()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(yaleProblem);

            //Act
            var query = new AlwaysAccessibleQuery("always accessible a from !a");
            var result = query.ToProlog();

            //Assert
            Assert.Equals(result, "True");

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test05()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(yaleProblem);

            //Act
            var query = new TypicallyAccessibleQuery("typically accessible a from !a");
            var result = query.ToProlog();

            //Assert
            Assert.Equals(result, "True");

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test06()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(yaleProblem);

            //Act
            var query = new PossiblyExecutableQuery("possibly executable ENTICE by Mietus,Hador from !w");
            var result = query.ToProlog();

            //Assert
            Assert.Equals(result, "True");

            PlEngine.PlCleanup();
        }
    }
}
