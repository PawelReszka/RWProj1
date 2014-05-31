namespace Rw.AdeSystem.Core.Queries
{
    public class AlwaysAccessibleQuery : Query
    {
        public AlwaysAccessibleQuery(string line) : base(line)
        {
        }

        public override void ToProlog()
        {
            throw new System.NotImplementedException();
        }
    }
}