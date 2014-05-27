using System.Collections.Generic;

namespace Rw.AdeSystem.Core.Expressions
{
    public class AlwaysExpression : Expression
    {
        public AlwaysExpression(string line) : base(line)
        {
            line = line.Trim().Remove(0, "always".Length);
            List<Token> tokens;
            List<string> tokenValues;
            var expression = LogicFormulaParser.Parse(line, out tokens, out tokenValues);
            BoolExpr l, r;
            l = expression.Left;
            r = expression.Right;
            if (l.IsLeaf() && r.IsLeaf())
            {
                
            }
            expression = expression;
        }

        public override void ToProlog()
        {
            throw new System.NotImplementedException();
        }
    }
}