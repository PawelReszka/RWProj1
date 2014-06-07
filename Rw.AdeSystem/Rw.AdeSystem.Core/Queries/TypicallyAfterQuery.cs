namespace Rw.AdeSystem.Core.Queries
{
    public class TypicallyAfterQuery : AfterQuery 
    {
        public TypicallyAfterQuery(string line) : base(line, "typically")
        {
        }

        public override string ToProlog()
        {
            var queries = base.GetQueries("typically");
            var result = false;
            foreach (var query in queries)
            {
                result = PrologEngine.ExecuteQuery(query);
                if (result)
                    break;
            }
            return result.ToString();
        }
    }
}