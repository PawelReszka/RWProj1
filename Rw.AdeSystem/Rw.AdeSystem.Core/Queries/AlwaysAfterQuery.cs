namespace Rw.AdeSystem.Core.Queries
{
    public class AlwaysAfterQuery : Query
    {
        public AlwaysAfterQuery(string line) : base(line)
        {
            if (line.Contains("from"))
            {
                GoalString = FluentParser.GetSubstring(line, " always ", " after ");
                ActionsString = FluentParser.GetSubstring(line, " after ", " by ");
                ExecutorsString = FluentParser.GetSubstring(line, " by ", " from ");
                ConditionsString = FluentParser.GetSubstring(line, " from ");
            }
            else
            {
                GoalString = FluentParser.GetSubstring(line, " always ", " after ");
                ActionsString = FluentParser.GetSubstring(line, " after ", " by ");
                ExecutorsString = FluentParser.GetSubstring(line, " by ");
            }
        }

        public string ExecutorsString { get; set; }

        public string ActionsString { get; set; }

        public string ConditionsString { get; set; }

        public string GoalString { get; set; }

        public override string ToProlog()
        {
            return "Nie zdefiniowana kwerenda - trzeba zrobić";
        }
    }
}