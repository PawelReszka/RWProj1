namespace Rw.AdeSystem.Core.Expressions
{
    public class NoninertialExpression : Expression
    {
        public string ActionName { get; set; }

        public NoninertialExpression(string line) : base(line)
        {
            var tokens = line.Trim().Split(' ');
            ActionName = tokens[1];
        }

        public override void ToProlog()
        {
            AdeSystem.PrologEngine.AssertFact("sinertial("+ActionName+")");
        }
    }
}