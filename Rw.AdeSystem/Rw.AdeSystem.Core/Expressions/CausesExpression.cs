using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rw.AdeSystem.Core.Expressions
{

    public class CausesExpression : Expression
    {
        public string ActionName { get; set; }

        public List<string> Effects  = new List<string>();
        public CausesExpression(string line) : base(line)
        {
            var tokens = line.Trim().Split(' ');
            AdeSystem.Actions.Add(tokens[0]);
            ActionName = tokens[0];
            Effects.Add(tokens.Last().Replace("!", "not_"));
            AdeSystem.Fluents.Add(tokens.Last().Replace("!",""));
        }

        public override void ToProlog()
        {
            var effects = String.Join(", ", Effects);
            AdeSystem.PrologEngine.AssertFact("causes("+ActionName+", epsilon, ["+effects+"], [])");
        }
    }
}