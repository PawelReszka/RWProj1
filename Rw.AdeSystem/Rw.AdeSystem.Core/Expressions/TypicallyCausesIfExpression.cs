using System;

namespace Rw.AdeSystem.Core.Expressions
{
    public class TypicallyCausesIfExpression : CausesIfExpression
    {
        public TypicallyCausesIfExpression(string line) : base(line)
        {
        }

        public override void ToProlog()
        {
            var effects = String.Join(", ", Effects);
            var conditions = String.Join(", ", Conditions);
            AdeSystem.PrologEngine.AssertFact("typically_causes(" + ActionName + ", epsilon, [" + effects + "], [" + conditions + "])");
        }
    }
}