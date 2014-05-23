namespace Rw.AdeSystem.Core.Expressions
{
    public class PreservesExpression : Expression
    {
        public PreservesExpression(string line) : base(line)
        {
        }

        public override void ToProlog()
        {
            throw new System.NotImplementedException();
        }
    }
}