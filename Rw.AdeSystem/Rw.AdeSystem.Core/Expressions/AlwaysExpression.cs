namespace Rw.AdeSystem.Core.Expressions
{
    public class AlwaysExpression : Expression
    {
        public AlwaysExpression(string line) : base(line)
        {
        }

        public override void ToProlog()
        {
            throw new System.NotImplementedException();
        }
    }
}