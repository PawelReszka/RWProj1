using System;
using System.Collections.Generic;
using System.Linq;

namespace Rw.AdeSystem.Core.Expressions
{
    public class ReleasesExpression : Expression
    {
        public List<string> Conditions { get; set; }

        public List<string> Effects { get; set; }

        public string Executor { get; set; }

        public string ActionName { get; set; }

        public ReleasesExpression(string line) : base(line)
        {
            var tokens = line.Trim().Split(' ');
            AdeSystem.Actions.Add(tokens[0]);
            AdeSystem.Executors.Add(tokens[2]);
            ActionName = tokens[0];
            Executor = tokens[2];
            Effects.Add(tokens[3].Replace("!", "not_"));
            AdeSystem.Fluents.Add(tokens.Last().Replace("!", ""));
            var con = line.Substring(line.IndexOf("if") + 2).Trim();
            Conditions.Add(con.Replace("!", "not_"));
        }



        public override void ToProlog()
        {
            var effects = String.Join(", ", Effects);
            var conditions = String.Join(", ", Conditions);
            AdeSystem.PrologEngine.AssertFact("releases(" + ActionName + ", " + Executor + ", [" + effects + "], [" + conditions + "])");
        }
    }
}