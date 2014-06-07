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
                GoalString = FluentParser.GetSubstring(line, " accessible ", " from ");
                var conditions = FluentParser.GetSubstring(line, " from ");
                List<string> litValues;
                List<Token> literals;
                var expression = LogicFormulaParser.Parse(conditions, out literals, out litValues);
                ConditionsStrings = LogicFormulaParser.GetFluentStrings(expression);
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
                foreach (var conditionsString in ConditionsStrings)
                {
                    query = prefix+"_accessible([" + GoalString + "], [" + conditionsString.Replace("!", "not_") + "])";
                    result.Add(query);
                    
                }
            }
            return result;
        }

        public List<string> ConditionsStrings { get; set; }

        public string GoalString { get; set; }
    }
}