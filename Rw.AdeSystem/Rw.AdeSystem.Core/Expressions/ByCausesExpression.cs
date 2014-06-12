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

        public List<string> Effects  = new List<string>();
        public ByCausesExpression(string line)
            : base(line)
        {
            var tokens = line.Trim().Split(' ');
            AdeSystem.Actions.Add(tokens[0]);
            AdeSystem.Executors.Add(tokens[2]);
            ActionName = tokens[0];
            Executor = tokens[2];
            var ef = FluentParser.GetSubstring(line, " causes ");
            Effects = LogicFormulaParser.GetConditions(ef);
            AdeSystem.Fluents.Add(tokens.Last().Replace("!", ""));

        }

        public override void ToProlog()
        {
            var effects = FluentParser.GetConditions(Effects);
            AdeSystem.PrologEngine.AssertFact("causes(" + ActionName.ToLower() + ", " + Executor.ToLower() + ", [" + effects.ToLower() + "], [[]])");
        }
    }
}