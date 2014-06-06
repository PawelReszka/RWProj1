namespace Rw.AdeSystem.Core.Expressions
{
    public class NoninertialExpression : Expression
    {

        public NoninertialExpression(string line) : base(line)
        {
            var tokens = line.Trim().Split(' ');
            AdeSystem.NoninertialFluents.Add(tokens[1]);
        }

        public override void ToProlog()
        {
            
        }
    }
}