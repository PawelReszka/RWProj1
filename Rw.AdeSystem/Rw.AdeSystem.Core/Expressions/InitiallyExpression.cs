using System;
using System.Collections.Generic;
using System.Linq;

namespace Rw.AdeSystem.Core.Expressions
{
    public class InitiallyExpression : Expression
    {
        private List<string> Fluents { get; set; }
        public InitiallyExpression(string line) : base(line)
        {
            var fs = line.Trim().Remove(0, "initially".Length).Trim().Replace(" ", "").Split('&').ToList();
            Fluents = fs.Select(i => i.Replace("!", "not_")).ToList();
            AdeSystem.Fluents.AddRange(fs.Select(i=>i.Replace("!","")));

        }

        public override void ToProlog()
        {
            var fluents = String.Join(", ", Fluents);
            AdeSystem.PrologEngine.AssertFact("initially(["+fluents+",not_m])");
        }
    }
}