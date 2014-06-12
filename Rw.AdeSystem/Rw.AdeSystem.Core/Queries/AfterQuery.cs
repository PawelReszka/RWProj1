using System.Collections.Generic;
using System.Linq;

namespace Rw.AdeSystem.Core.Queries
{
    public class AfterQuery : Query
    {
        public AfterQuery(string line, string prefix)
            : base(line)
        {
            line = line.ToLower();
            if (line.Contains("from"))
            {
                GoalString = FluentParser.GetSubstring(line, prefix + " ", " after ");
                ActionsString = FluentParser.GetSubstring(line, " after ", " by ");
                ExecutorsString = FluentParser.GetSubstring(line, " by ", " from ");
                var conditions = FluentParser.GetSubstring(line, " from ");
                List<string> litValues;
                List<Token> literals;
                var expression = LogicFormulaParser.Parse(conditions, out literals, out litValues);
                ConditionsStrings = FluentParser.GetConditions(LogicFormulaParser.GetConditions(conditions));
                GoalString = GoalString.Replace("&", ",").Replace("!", "not_");
            }
            else
            {
                GoalString = FluentParser.GetSubstring(line, " always ", " after ");
                ActionsString = FluentParser.GetSubstring(line, " after ", " by ");
                ExecutorsString = FluentParser.GetSubstring(line, " by ");
            }
        }

        public string ConditionsStrings { get; set; }

        public string ExecutorsString { get; set; }

        public string ActionsString { get; set; }

        public string GoalString { get; set; }

        public override string ToProlog()
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetQueries(string prefix)
        {
            var result = new List<string>();
            string query = "";
            if (ConditionsStrings == null || !ConditionsStrings.Any())
            {
                query = prefix + "_after([" + GoalString + "],[" + ActionsString + "],[" + ExecutorsString + "],[])";

                result.Add(query);
            }
            else
            {

                result.Add(prefix + "_after([" + GoalString + "],[" + ActionsString + "],[" + ExecutorsString + "],[" + ConditionsStrings + "])");

            }
            return result;
        }
    }
}