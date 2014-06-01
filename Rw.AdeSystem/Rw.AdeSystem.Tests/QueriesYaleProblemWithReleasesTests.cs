using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rw.AdeSystem.Core.Queries;
using SbsSW.SwiPlCs;

namespace Rw.AdeSystem.Tests
{
    [TestClass]
    public class QueriesYaleProblemWithReleasesTests
    {
        private const string YaleProblem = @"initially h ∧ a ∧ w
                                            always h <-> !m
                                            always w -> a
                                            CHOWN by Mietus  causes h
                                            CHOWN by Hador causes m
                                            SHOOT by Mietus causes !a if m
                                            SHOOT by Hador typically causes !a if h
                                            SHOOT by Mietus  releases w if m
                                            SHOOT by Hador releases w if h";
       
    }
}
