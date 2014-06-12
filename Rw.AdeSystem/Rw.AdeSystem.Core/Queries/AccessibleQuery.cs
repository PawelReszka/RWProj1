using System.Collections.Generic;
using System.Linq;

namespace Rw.AdeSystem.Core.Queries
{
    public class AccessibleQuery : Query
    {
        public AccessibleQuery(string line) : base(line)
        {
            if (line.Contains("from"))
            {
                GoalString = FluentParser.GetConditions(LogicFormulaParser.GetConditions(FluentParser.GetSubstring(line, " accessible ", " from ")));
                var conditions = FluentParser.GetSubstring(line, " from ");
                List<string> litValues;
                List<Token> literals;
                var expression = LogicFormulaParser.Parse(conditions, out literals, out litValues);
                ConditionsStrings = FluentParser.GetConditions(LogicFormulaParser.GetConditions(conditions));
            }
            else
            {
                GoalString = FluentParser.GetSubstring(line, " accessible ");
            }
        }

        public override string ToProlog()
        {
            return "";
        }

        public List<string> GetQueries(string prefix)
        {
            var result = new List<string>();

            string query = "";
            if (ConditionsStrings == null || !ConditionsStrings.Any())
            {
                query = prefix+"_accessible([" + GoalString + "])";
                result.Add(query);
            }
            else
            {

                    query = prefix+"_accessible([" + GoalString + "], [" + ConditionsStrings + "])";
                    result.Add(query);

            }
            return result;
        }

        public string ConditionsStrings { get; set; }

        public string GoalString { get; set; }
    }
}