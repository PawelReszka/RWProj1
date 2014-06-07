using System;

namespace Rw.AdeSystem.Core.Expressions
{
    public class TypicallyCausesExpression : CausesExpression
    {
        public TypicallyCausesExpression(string line) : base(line)
        {
        }

        public override void ToProlog()
        {
            var effects = String.Join(", ", Effects);
            AdeSystem.PrologEngine.AssertFact("typically_causes(" + ActionName + ", epsilon, [" + effects + "], [])");
        }
    }
}