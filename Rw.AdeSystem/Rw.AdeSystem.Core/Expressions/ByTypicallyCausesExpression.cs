using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rw.AdeSystem.Core.Expressions
{
    public class ByTypicallyCausesExpression : ByCausesExpression
    {
        public ByTypicallyCausesExpression(string line) : base(line)
        {
        }

        public override void ToProlog()
        {
            var effects = FluentParser.GetConditions(Effects);
            AdeSystem.PrologEngine.AssertFact("typically_causes(" + ActionName.ToLower() + ", " + Executor.ToLower() + ", [" + effects.ToLower() + "], [[]])");
        }
    }
}
