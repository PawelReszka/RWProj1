using System.Linq;

namespace Rw.AdeSystem.Core.Queries
{
    public class PossiblyExecutableQuery : Query
    {
        string ActionsString { get; set; }
        string ExecutorsString { get; set; }
        string ConditionsString { get; set; }

        public PossiblyExecutableQuery(string line) : base(line)
        {
            ActionsString = FluentParser.GetSubstring(line, "executable", "by");
            
            if (line.Contains("from"))
            {
                ExecutorsString = FluentParser.GetSubstring(line, "by", "from");
                ConditionsString = FluentParser.GetSubstring(line, "from").Replace("!", "not_");
            }
            else
            {
                ExecutorsString = FluentParser.GetSubstring(line, "by");
                ConditionsString = "";
            }
            
        }

        public override string ToProlog()
        {
          //  PrologEngine.ExecuteQuery(" possibly(["++"],[entice,shoot],[epsilon,hador],[not_alive,has_gun_hador])")
        }
    }
}