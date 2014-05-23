namespace Rw.AdeSystem.Core.Expressions
{
    public class ByExpression : Expression
    {
        public ByExpression(string line) : base(line)
        {
        }

        public override void ToProlog()
        {
            throw new System.NotImplementedException();
        }
    }
}