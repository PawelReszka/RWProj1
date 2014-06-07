using System.Collections.Generic;

namespace Rw.AdeSystem.Core.Queries
{
    public class AfterQuery : Query
    {
        public AfterQuery(string line, string prefix) : base(line)
        {
            if (line.Contains("from"))
            {
                GoalString = FluentParser.GetSubstring(line, prefix+" ", " after ");
                ActionsString = FluentParser.GetSubstring(line, " after ", " by ");
                ExecutorsString = FluentParser.GetSubstring(line, " by ", " from ");
                var conditions = FluentParser.GetSubstring(line, " from ");
                List<string> litValues;
                List<Token> literals;
                var expression = LogicFormulaParser.Parse(conditions, out literals, out litValues);
                ConditionsStrings = LogicFormulaParser.GetFluentStrings(expression);
            }
            else
            {
                GoalString = FluentParser.GetSubstring(line, " always ", " after ");
                ActionsString = FluentParser.GetSubstring(line, " after ", " by ");
                ExecutorsString = FluentParser.GetSubstring(line, " by ");
            }
        }

        public List<string> ConditionsStrings { get; set; }

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

            return result;
        }
    }
}