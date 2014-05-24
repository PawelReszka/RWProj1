namespace Rw.AdeSystem.Core.Expressions
{
    public class NoninertialExpression : Expression
    {
        public NoninertialExpression(string line) : base(line)
        {
        }

        public override void ToProlog()
        {
            throw new System.NotImplementedException();
        }
    }
}