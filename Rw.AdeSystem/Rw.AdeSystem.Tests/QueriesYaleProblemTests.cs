using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rw.AdeSystem.Core.Queries;
using SbsSW.SwiPlCs;

namespace Rw.AdeSystem.Tests
{
    [TestClass]
    public class QueriesYaleProblemTests
    {
        private const string FalseString = "False";
        private const string TrueString = "True";
        private const string YaleProblem = @"initially h & a & w
                                            always h <-> !m
                                            always w -> a
                                            CHOWN by Mietus causes h
                                            CHOWN by Hador causes m
                                            SHOOT by Mietus causes !a if !m & h
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
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();
            //Act
            var query = new PossiblyExecutableQuery("possibly executable ENTICE by hador from !a & !w");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result.ToLower(), FalseString.ToLower());

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test02()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new AlwaysExecutableQuery("always executable ENTICE by Hador from !a & !w");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, FalseString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test03()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new PossiblyAccessibleQuery("possibly accessible a from !a");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test04()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new AlwaysAccessibleQuery("always accessible a from !a");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test05()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new TypicallyAccessibleQuery("typically accessible a from !a");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test07()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new PossiblyInvolvedQuery("possibly involved Mietus in SHOOT,SHOOT,ENTICE by Hador,Mietus,Mietus");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test08()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new AlwaysInvolvedQuery("always involved Mietus in SHOOT,SHOOT,ENTICE by Hador,Mietus,Mietus");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test09()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new TypicallyInvolvedQuery("typically involved Mietus in SHOOT,SHOOT,ENTICE by Hador,Mietus,Mietus");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }
        [TestMethod]
        public void Test10()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new PossiblyInvolvedQuery("possibly involved Hador in SHOOT,SHOOT,ENTICE by Hador,Mietus,Mietus");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test11()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new AlwaysInvolvedQuery("always involved Hador in SHOOT,SHOOT,ENTICE by Hador,Mietus,Mietus");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test12()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new TypicallyInvolvedQuery("typically involved Hador in SHOOT,SHOOT,ENTICE by Hador,Mietus,Mietus");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test13()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new PossiblyInvolvedQuery("possibly involved Mietus in SHOOT,SHOOT,ENTICE by Hador,Mietus,Hador");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test14()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new AlwaysInvolvedQuery("always involved Mietus in SHOOT,SHOOT,ENTICE by Hador,Mietus,Hador");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, FalseString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test15()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new TypicallyInvolvedQuery("typically involved Mietus in SHOOT,SHOOT,ENTICE by Hador,Mietus,Hador");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }
        [TestMethod]
        public void Test16()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new PossiblyInvolvedQuery("possibly involved Hador in SHOOT,SHOOT,ENTICE by Hador,Mietus,Hador");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test17()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new AlwaysInvolvedQuery("always involved Hador in SHOOT,SHOOT,ENTICE by Hador,Mietus,Hador");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, FalseString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test18()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new TypicallyInvolvedQuery("typically involved Hador in SHOOT,SHOOT,ENTICE by Hador,Mietus,Hador");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test19()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.LoadDomain(YaleProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new PossiblyExecutableQuery("possibly executable shoot by hador");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }
    }
}
