namespace Rw.AdeSystem.Core.Queries
{
    public class PossiblyExecutableQuery : Query
    {
        public PossiblyExecutableQuery(string line) : base(line)
        {
        }

        public override string ToProlog()
        {
            throw new System.NotImplementedException();
        }
    }
}