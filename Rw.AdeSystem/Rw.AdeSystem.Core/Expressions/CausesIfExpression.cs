using System;
using System.Collections.Generic;
using System.Text;

namespace Rw.AdeSystem.Core.Expressions
{
    public class CausesIfExpression : CausesExpression
    {
        public List<string> Conditions = new List<string>(); 
        public CausesIfExpression(string line) : base(line.Substring(0,line.IndexOf("if")))
        {
            var con = line.Substring(line.IndexOf("if")+2).Trim();
            List<string> litVal;
            List<Token> lit;
            var exp = LogicFormulaParser.Parse(con, out lit, out litVal);
            Conditions = LogicFormulaParser.GetConditions(con);
        }

        public override void ToProlog()
        {
            var effects = String.Join(", ", Effects);
            foreach (var condition in Conditions)
            {
                AdeSystem.PrologEngine.AssertFact("causes(" + ActionName.ToLower() + ", epsilon, [" + effects.ToLower() + "], [" + condition.ToLower() + "])");
            }
        }
    }
}