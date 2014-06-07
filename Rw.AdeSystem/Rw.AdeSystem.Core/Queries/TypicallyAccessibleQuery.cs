namespace Rw.AdeSystem.Core.Queries
{
    public class TypicallyAccessibleQuery : AccessibleQuery
    {
        public TypicallyAccessibleQuery(string line) : base(line)
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