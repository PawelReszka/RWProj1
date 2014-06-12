using System;

namespace Rw.AdeSystem.Core.Expressions
{
    public class TypicallyCausesIfExpression : CausesIfExpression
    {
        public TypicallyCausesIfExpression(string line)
            : base(line)
        {
        }

        public override void ToProlog()
        {
            var effects = FluentParser.GetConditions(Effects);
            var conditions = FluentParser.GetConditions(Conditions);
            AdeSystem.PrologEngine.AssertFact("typically_causes(" + ActionName.ToLower() + ", epsilon, [" + effects.ToLower() + "], [" + conditions.ToLower() + "])");
        }
    }
}