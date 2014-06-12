using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rw.AdeSystem.Core.Expressions
{

    public class CausesExpression : Expression
    {
        public string ActionName { get; set; }

        public List<string> Effects { get; set; }
        public CausesExpression(string line) : base(line)
        {
            var tokens = line.Trim().Split(' ');
            AdeSystem.Actions.Add(tokens[0]);
            ActionName = tokens[0];
            var ef = FluentParser.GetSubstring(line, " causes ");
            Effects = LogicFormulaParser.GetConditions(ef);
            AdeSystem.Fluents.Add(tokens.Last().Replace("!",""));
        }

        public override void ToProlog()
        {
            var effects = FluentParser.GetConditions(Effects);
            AdeSystem.PrologEngine.AssertFact("causes(" + ActionName.ToLower() + ", epsilon, [" + effects.ToLower() + "], [[]])");
        }
    }
}