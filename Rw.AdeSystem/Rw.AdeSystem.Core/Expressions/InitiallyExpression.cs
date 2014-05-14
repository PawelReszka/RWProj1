namespace Rw.AdeSystem.Core.Expressions
{
    public class InitiallyExpression : Expression
    {
        public InitiallyExpression(string line) : base(line)
        {
            var fluents = line.Trim().Remove(0, "initially".Length).Trim().Replace(" ", "");
            AdeSystem.Fluents.Add(fluents);
        }

        public override void ToProlog()
        {
            throw new System.NotImplementedException();
        }
    }
}