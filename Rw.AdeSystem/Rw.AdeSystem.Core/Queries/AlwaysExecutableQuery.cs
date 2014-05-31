namespace Rw.AdeSystem.Core.Queries
{
    public class AlwaysExecutableQuery : Query
    {
        public AlwaysExecutableQuery(string line) : base(line)
        {
        }

        public override void ToProlog()
        {
            throw new System.NotImplementedException();
        }
    }
}