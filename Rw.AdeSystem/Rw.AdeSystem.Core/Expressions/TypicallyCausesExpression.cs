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
            var effects = FluentParser.GetConditions(Effects);
            AdeSystem.PrologEngine.AssertFact("typically_causes(" + ActionName.ToLower() + ", epsilon, [" + effects.ToLower() + "], [[]])");
        }
    }
}