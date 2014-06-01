using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Rw.AdeSystem.Tests
{
    [TestClass]
    public class QueriesLightbulbsTests
    {
        private const string LighbulbsProblem = @"initially s1 ∧ s2
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
    }
}