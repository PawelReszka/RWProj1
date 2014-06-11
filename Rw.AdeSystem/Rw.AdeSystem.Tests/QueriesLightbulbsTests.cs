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
        private const string LighbulbsProblem = @"initially sone & stwo
                                                always l <-> (sone <-> stwo)
                                                noninertial l
                                                TURNONE by Hador causes sone if !sone
                                                TURNONE by Hador causes !sone if sone
                                                TURNONE by Mietus typically causes sone if !sone
                                                TURNONE by Mietus typically causes !sone if sone
                                                TURNTWO by Hador causes stwo if !stwo
                                                TURNTWO by Hador typically causes !stwo if stwo
                                                TURNTWO by Mietus typically causes stwo if !stwo
                                                TURNTWO by Mietus causes !stwo if stwo";

        [TestMethod]
        public void Test01()
        {
            //Arrange
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            Core.AdeSystem.Initialize(param);

            Core.AdeSystem.Initialize(param);
            Core.AdeSystem.LoadDomain(LighbulbsProblem);
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new AlwaysAfterQuery("always !sone&!stwo after TURNONE,TURNTWO by epsilon,epsilon from sone&stwo");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, FalseString);

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
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new PossiblyAfterQuery("possibly !sone&!stwo after TURNONE,TURNTWO by epsilon,epsilon from sone&stwo");
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
            Core.AdeSystem.ConstructSystemDomain();

            //Act
            var query = new TypicallyAfterQuery("typically !sone&!stwo after TURNONE,TURNTWO by epsilon, epsilon from sone&stwo");
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
            Core.AdeSystem.ConstructSystemDomain();
            //Act
            var query = new AlwaysAfterQuery("always !sone&!stwo after TURNTWO,TURNONE by epsilon,epsilon from sone&stwo");
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
            Core.AdeSystem.ConstructSystemDomain();
            //Act
            var query = new PossiblyAfterQuery("possibly !sone&!stwo after TURNTWO,TURNONE by epsilon,epsilon from sone&stwo");
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
            Core.AdeSystem.ConstructSystemDomain();
            //Act
            var query = new TypicallyAfterQuery("typically !sone&!stwo after TURNTWO,TURNONE by epsilon,epsilon from sone&stwo");
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
            Core.AdeSystem.LoadDomain(LighbulbsProblem);
            Core.AdeSystem.ConstructSystemDomain();
            //Act
            var query = new PossiblyInvolvedQuery("possibly involved Hador in TURNTWO, TURNONE by epsilon,epsilon");
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
            Core.AdeSystem.ConstructSystemDomain();
            //Act
            var query = new AlwaysInvolvedQuery("always involved Hador in TURNTWO, TURNONE by epsilon,epsilon");
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
            Core.AdeSystem.ConstructSystemDomain();
            //Act
            var query = new TypicallyInvolvedQuery("typically involved Hador in TURNTWO, TURNONE by epsilon,epsilon");
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
            Core.AdeSystem.ConstructSystemDomain();
            //Act
            var query = new PossiblyInvolvedQuery("possibly involved Mietus in TURNTWO, TURNONE by epsilon,epsilon");
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
            Core.AdeSystem.ConstructSystemDomain();
            //Act
            var query = new AlwaysInvolvedQuery("always involved Mietus in TURNTWO, TURNONE by epsilon,epsilon");
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
            Core.AdeSystem.ConstructSystemDomain();
            //Act
            var query = new TypicallyInvolvedQuery("typically involved Mietus in TURNTWO, TURNONE by epsilon,epsilon");
            var result = query.ToProlog();

            //Assert
            Assert.AreEqual(result, FalseString);

            PlEngine.PlCleanup();
        }
    }
}