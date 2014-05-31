namespace Rw.AdeSystem.Core.Queries
{
    public class AlwaysAfterQuery : Query
    {
        public AlwaysAfterQuery(string line) : base(line)
        {
        }

        public override string ToProlog()
        {
            throw new System.NotImplementedException();
        }
    }
}