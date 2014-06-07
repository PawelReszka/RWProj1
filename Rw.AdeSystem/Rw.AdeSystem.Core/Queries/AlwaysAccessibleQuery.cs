using System.Collections.Generic;
using System.Linq;

namespace Rw.AdeSystem.Core.Queries
{
    public class AlwaysAccessibleQuery : AccessibleQuery
    {
        public AlwaysAccessibleQuery(string line)
            : base(line)
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