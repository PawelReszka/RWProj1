namespace Rw.AdeSystem.Core.Queries
{
    public class PossiblyInvolvedQuery : Query
    {
        public PossiblyInvolvedQuery(string line)
            : base(line)
        {
            ExecutorString = FluentParser.GetSubstring(line, " involved ", " in ");
            ActionsString = FluentParser.GetSubstring(line, " in ", " by ");

            ExecutorsString = FluentParser.GetSubstring(line, " by ");

        }

        public string ExecutorString { get; set; }

        public string ExecutorsString { get; set; }

        public string ActionsString { get; set; }

        public override string ToProlog()
        {
            var query = "always_involved("+ExecutorString+", ["+ActionsString+"], ["+ExecutorsString+"])";
            return PrologEngine.ExecuteQuery(query);
        }
    }
}