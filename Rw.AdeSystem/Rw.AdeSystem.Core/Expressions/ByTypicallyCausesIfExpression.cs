using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rw.AdeSystem.Core.Expressions
{
    public class ByTypicallyCausesIfExpression : ByCausesIfExpression
    {
        public ByTypicallyCausesIfExpression(string line)
            : base(line)
        {
        }

        public override void ToProlog()
        {
            var effects = String.Join(", ", Effects);
            var conditions = String.Join(", ", Conditions);
            AdeSystem.PrologEngine.AssertFact("typically_causes(" + ActionName + ", " + Executor + ", [" + effects + "], [" + conditions + "])");
        }
    }
}
