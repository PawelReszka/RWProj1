using System;
using System.Collections.Generic;
using System.Linq;

namespace Rw.AdeSystem.Core.Expressions
{
    public class AlwaysExpression : Expression
    {
        private static int FormulaCounter = 0;
        public BoolExpr Expression { get; set; }
        public AlwaysExpression(string line) : base(line)
        {
            line = line.Trim().Remove(0, "always".Length);
            List<Token> tokens;
            List<string> tokenValues;
            var expression = LogicFormulaParser.Parse(line, out tokens, out tokenValues);
            Conditions = LogicFormulaParser.GetConditions(line);
            AdeSystem.Fluents.AddRange(tokens.Select(i=>i.Value));
            AdeSystem.Fluents = AdeSystem.Fluents.Distinct().ToList();
            Expression = expression;
        }

        public List<string> Conditions { get; set; }

        public override void ToProlog()
        {
            var states = new List<string>();
            foreach (var condition in Conditions)
            {
                var name = "s" + FormulaCounter++;
                AdeSystem.PrologEngine.AssertFact("stmt("+name+",["+condition+"])");
                states.Add(name);
            }
            var formula = "f" + FormulaCounter++;
            AdeSystem.PrologEngine.AssertFact("formula("+formula+", ["+String.Join(", ", states)+"])");
            AdeSystem.PrologEngine.AssertFact("always("+formula+")");

        }
    }
}