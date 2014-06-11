using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;

namespace Rw.AdeSystem.Core.Expressions
{
    public class ByCausesExpression : Expression
    {
        public string ActionName { get; set; }

        public string Executor { get; set; } 

        public string Effects {get;set;}
        public ByCausesExpression(string line)
            : base(line)
        {
            var tokens = line.Trim().Split(' ');
            AdeSystem.Actions.Add(tokens[0]);
            AdeSystem.Executors.Add(tokens[2]);
            ActionName = tokens[0];
            Executor = tokens[2];
            Effects = FluentParser.GetSubstring(line, " causes ").Replace("!", "not_").Replace("&", ", ");
            AdeSystem.Fluents.Add(tokens.Last().Replace("!", ""));

        }

        public override void ToProlog()
        {
            var effects = String.Join(", ", Effects);
            AdeSystem.PrologEngine.AssertFact("causes(" + ActionName.ToLower() + ", " + Executor.ToLower() + ", [" + effects.ToLower() + "], [])");
        }
    }
}