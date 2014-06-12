using System.Collections.Generic;

namespace Rw.AdeSystem.Core.Queries
{
    public class ExecutableQuery : Query
    {
        public ExecutableQuery(string line) : base(line)
        {
            if (line.Contains("from"))
            {
                ActionsString = FluentParser.GetSubstring(line, " executable ", " by ");
                ExecutorsString = FluentParser.GetSubstring(line, " by ", " from ");
                var conditions = FluentParser.GetSubstring(line, " from ");
                List<string> litValues;
                List<Token> literals;
                var expression = LogicFormulaParser.Parse(conditions, out literals, out litValues);
                ConditionsStrings = FluentParser.GetConditions(LogicFormulaParser.GetConditions(conditions));
            }
            else
            {
                ActionsString = FluentParser.GetSubstring(line, " executable ", " by ");
                ExecutorsString = FluentParser.GetSubstring(line, " by ");
            }
        }

        public string ConditionsStrings { get; set; }

        public string ExecutorsString { get; set; }

        public string ActionsString { get; set; }

        public override string ToProlog()
        {
            throw new System.NotImplementedException();
        }

        public List<string> GetQueries(string prefix)
        {
            var result = new List<string>();

            string query = "";
            if (ConditionsStrings == null)
            {
                query = prefix+"_executable([" + ActionsString + "], ["+ExecutorsString+"])";
                result.Add(query);
            }
            else
            {

                    query = prefix + "_executable( [" + ConditionsStrings + "], [" + ActionsString + "], [" + ExecutorsString + "])";
                    result.Add(query);
                
            }
            return result;
        }
    }
}