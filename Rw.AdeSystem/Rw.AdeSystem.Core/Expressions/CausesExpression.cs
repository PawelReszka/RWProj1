using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rw.AdeSystem.Core.Expressions
{

    
//releases(shoot,mietus,[walking], f3).
//releases(shoot,hador,[walking], f4).


    public class CausesExpression : Expression
    {
        public string ActionName { get; set; }

        public List<string> Effects  = new List<string>();
        public CausesExpression(string line) : base(line)
        {
            var tokens = line.Trim().Split(' ');
            AdeSystem.Actions.Add(tokens[0]);
            ActionName = tokens[0];
            Effects.Add(tokens.Last());
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
            AdeSystem.PrologEngine.AssertFact("causes("+ActionName+", epsilon, ["+effects+"], []).");
        }
    }
}