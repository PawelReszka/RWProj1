namespace Rw.AdeSystem.Core.Queries
{
    public class PossiblyExecutableQuery : Query
    {
        public PossiblyExecutableQuery(string line) : base(line)
        {
        }

        public override void ToProlog()
        {
            throw new System.NotImplementedException();
        }
    }
}