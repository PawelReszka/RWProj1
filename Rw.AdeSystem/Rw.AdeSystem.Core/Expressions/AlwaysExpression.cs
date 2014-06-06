﻿using System.Collections.Generic;

namespace Rw.AdeSystem.Core.Expressions
{
    public class AlwaysExpression : Expression
    {
        public BoolExpr Expression { get; set; }
        public AlwaysExpression(string line) : base(line)
        {
            line = line.Trim().Remove(0, "always".Length);
            List<Token> tokens;
            List<string> tokenValues;
            var expression = LogicFormulaParser.Parse(line, out tokens, out tokenValues);
            
            Expression = expression;
        }

        public override void ToProlog()
        {
            
        }
    }
}