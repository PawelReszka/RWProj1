using System.Collections.Generic;

namespace Rw.AdeSystem.Core.Queries
{
    public class AlwaysExecutableQuery : ExecutableQuery
    {
        public AlwaysExecutableQuery(string line) : base(line)
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