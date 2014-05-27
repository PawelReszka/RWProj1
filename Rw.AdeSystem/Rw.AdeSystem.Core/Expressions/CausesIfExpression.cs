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
            Conditions.Add(con);
        }

        public override void ToProlog()
        {
            var effects = new StringBuilder();
            for (int i = 0; i < Effects.Count; i++)
            {
                effects.Append(Effects[i]);
                if (i != Effects.Count - 1)
                    effects.Append(", ");
            }
            var conditions = new StringBuilder();
            for (int i = 0; i < Conditions.Count; i++)
            {
                conditions.Append(Conditions[i]);
                if (i != Conditions.Count - 1)
                    conditions.Append(", ");
            }
            AdeSystem.PrologEngine.AssertFact("causes(" + ActionName + ", epsilon, [" + effects + "], [" + conditions + "])");
        }
    }
}