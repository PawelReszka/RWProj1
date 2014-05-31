namespace Rw.AdeSystem.Core.Queries
{
    public class PossiblyAccessibleQuery : Query
    {
        public PossiblyAccessibleQuery(string line) : base(line)
        {
        }

        public override void ToProlog()
        {
            throw new System.NotImplementedException();
        }
    }
}