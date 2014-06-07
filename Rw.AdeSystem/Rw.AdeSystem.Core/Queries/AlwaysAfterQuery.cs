using System.Collections.Generic;

namespace Rw.AdeSystem.Core.Queries
{
    public class AlwaysAfterQuery : AfterQuery
    {
        public AlwaysAfterQuery(string line) : base(line, "always")
        {
            
        }

        public override string ToProlog()
        {
            var queries = base.GetQueries("always");
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