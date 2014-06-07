using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rw.AdeSystem.Core.Expressions
{
    public class PreservesExpression : Expression
    {
        public string ActionName { get; set; }
        public string ExecutorName { get; set; }
        public string Fluent { get; set; }

        public List<string> Fluents = new List<string>();

        public PreservesExpression(string line)
            : base(line)
        {

            ActionName = line.Substring(0, line.IndexOf(" by")).Trim();
            ExecutorName = FluentParser.GetSubstring(line, " by ", " preserves").Trim(); 
            if (line.Contains(" if "))
            {
                Fluent = FluentParser.GetSubstring(line, " preserves ", " if ");
                var con = FluentParser.GetSubstring(line, " if ");
                List<string> litVal;
                List<Token> lit;
                var exp = LogicFormulaParser.Parse(con, out lit, out litVal);
                Fluents = LogicFormulaParser.GetFluentStrings(exp);
            }
            else
            {
                Fluent = FluentParser.GetSubstring(line, " preserves ");                
            }
        }

        public override void ToProlog()
        {
            if (!Fluents.Any())
            {
                AdeSystem.PrologEngine.AssertFact("preserve(" + ActionName + "," + ExecutorName + ",[" + Fluent + "])");
            }
        }
    }
}