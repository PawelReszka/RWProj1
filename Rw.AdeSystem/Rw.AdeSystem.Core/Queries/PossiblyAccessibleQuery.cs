﻿namespace Rw.AdeSystem.Core.Queries
{
    public class PossiblyAccessibleQuery : AccessibleQuery
    {
        public PossiblyAccessibleQuery(string line) : base(line)
        {
        }

        public override string ToProlog()
        {
            var queries = base.GetQueries("possibly");
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