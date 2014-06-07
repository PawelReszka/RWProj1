namespace Rw.AdeSystem.Core.Queries
{
    public class TypicallyInvolvedQuery : InvolvedQuery
    {
        public TypicallyInvolvedQuery(string line) : base(line)
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