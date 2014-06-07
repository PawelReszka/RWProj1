namespace Rw.AdeSystem.Core.Queries
{
    public class AlwaysAccessibleQuery : Query
    {
        public AlwaysAccessibleQuery(string line)
            : base(line)
        {
            if (line.Contains("from"))
            {
                GoalString = FluentParser.GetSubstring(line, " accesible ", " from ");
                ConditionsString = FluentParser.GetSubstring(line, " from ");
            }
            else
            {
                GoalString = FluentParser.GetSubstring(line, " accesible ");
            }
        }

        public string ConditionsString { get; set; }

        public string GoalString { get; set; }

        public override string ToProlog()
        {
            string query="";
            if (ConditionsString == null)
                query = "always_accessible(["+GoalString+"])";
            else
                query = "always_accessible([" + GoalString + "], ["+ConditionsString+"])";
            return PrologEngine.ExecuteQuery(query);
        }
    }
}