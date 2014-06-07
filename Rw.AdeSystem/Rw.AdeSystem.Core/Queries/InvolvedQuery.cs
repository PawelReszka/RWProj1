using System.Collections.Generic;

namespace Rw.AdeSystem.Core.Queries
{
    public class InvolvedQuery : Query
    {
        public InvolvedQuery(string line) : base(line)
        {
            ExecutorString = FluentParser.GetSubstring(line, " involved ", " in ");
            ActionsString = FluentParser.GetSubstring(line, " in ", " by ");

            ExecutorsString = FluentParser.GetSubstring(line, " by ");
        }

        public string ExecutorsString { get; set; }

        public string ActionsString { get; set; }

        public string ExecutorString { get; set; }


        public override string ToProlog()
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetQueries(string prefix)
        {
            var result = new List<string>();
            var query = prefix+"_involved(" + ExecutorString + ", [" + ActionsString + "], [" + ExecutorsString + "])";
            result.Add(query);
            return result;
        }
    }
}