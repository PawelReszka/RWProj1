using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rw.AdeSystem.Core.Queries;
using SbsSW.SwiPlCs;

namespace Rw.AdeSystem.Tests
{
    [TestClass]
    public class QueriesLightbulbsTests
    {
        private const string FalseString = "False";
        private const string TrueString = "True";
        private const string LighbulbsProblem = @"initially s1 & s2
                                                always l <-> (s1 <-> s2)
                                                noninertial l
                                                TURN1 by Hador causes s1 if !s1
                                                TURN1 by Hador causes !s1 if s1
                                                TURN1 by Mietus typically causes s1 if !s1
                                                TURN1 by Mietus typically causes !s1 if s1
                                                TURN2 by Hador causes s2 if !s2
                                                TURN2 by Hador typically causes !s2 if s2
                                                TURN2 by Mietus typically causes s2 if !s2
                                                TURN2 by Mietus causes !s2 if s2";

        [TestMethod]
        public void Test01()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(LighbulbsProblem);

            //Act
            var query = new AlwaysAfterQuery("always !s1&!s2 after TURN1,TURN2 by epsilon,epsilon from s1&s2");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test02()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(LighbulbsProblem);

            //Act
            var query = new PossiblyAfterQuery("possibly !s1&!s2 after TURN1,TURN2 by epsilon,epsilon from s1&s2");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test03()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(LighbulbsProblem);

            //Act
            var query = new TypicallyAfterQuery("typically !s1&!s2 after TURN1,TURN2 by epsilon,epsilon from s1&s2");
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
            Core.AdeSystem.LoadDomain(LighbulbsProblem);

            //Act
            var query = new AlwaysAfterQuery("always !s1&!s2 after TURN2,TURN1 by epsilon,epsilon from s1&s2");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, FalseString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test05()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(LighbulbsProblem);

            //Act
            var query = new PossiblyAfterQuery("possibly !s1&!s2 after TURN2,TURN1 by epsilon,epsilon from s1&s2");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, TrueString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test06()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(LighbulbsProblem);

            //Act
            var query = new TypicallyAfterQuery("typically !s1&!s2 after TURN2,TURN1 by epsilon,epsilon from s1&s2");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, FalseString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test07()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(LighbulbsProblem);

            //Act
            var query = new PossiblyInvolvedQuery("possibly involved Hador in TURN2, TURN1 by epsilon,epsilon");
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
            Core.AdeSystem.LoadDomain(LighbulbsProblem);

            //Act
            var query = new AlwaysInvolvedQuery("always involved Hador in TURN2, TURN1 by epsilon,epsilon");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, FalseString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test09()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(LighbulbsProblem);

            //Act
            var query = new TypicallyInvolvedQuery("typically involved Hador in TURN2, TURN1 by epsilon,epsilon");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, FalseString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test10()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(LighbulbsProblem);

            //Act
            var query = new PossiblyInvolvedQuery("possibly involved Mietus in TURN2, TURN1 by epsilon,epsilon");
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
            Core.AdeSystem.LoadDomain(LighbulbsProblem);

            //Act
            var query = new AlwaysInvolvedQuery("always involved Mietus in TURN2, TURN1 by epsilon,epsilon");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, FalseString);

            PlEngine.PlCleanup();
        }

        [TestMethod]
        public void Test12()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(LighbulbsProblem);

            //Act
            var query = new TypicallyInvolvedQuery("typically involved Mietus in TURN2, TURN1 by epsilon,epsilon");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, FalseString);

            PlEngine.PlCleanup();
        }
    }
}