using System;
using System.Collections.Generic;
using System.Text;

namespace Rw.AdeSystem.Core.Expressions
{
    public class ByCausesIfExpression : ByCausesExpression
    {
        public List<string> Conditions = new List<string>();
        public ByCausesIfExpression(string line)
            : base(line.Substring(0, line.IndexOf("if")))
        {
            var con = line.Substring(line.IndexOf("if") + 2).Trim();
            Conditions.Add(con.Replace("!", "not_"));
        }

        public override void ToProlog()
        {
            var effects = String.Join(", ", Effects);
            var conditions = String.Join(", ", Conditions);
            AdeSystem.PrologEngine.AssertFact("causes(" + ActionName.ToLower() + ", " + Executor.ToLower() + ", [" + effects.ToLower() + "], [" + conditions.ToLower() + "])");
        }
    }
}