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
            foreach (var condition in Conditions)
            {
                AdeSystem.PrologEngine.AssertFact("typically_causes(" + ActionName.ToLower() + ", epsilon, [" + effects.ToLower() + "], [" + condition.ToLower() + "])");
            }
        }
    }
}