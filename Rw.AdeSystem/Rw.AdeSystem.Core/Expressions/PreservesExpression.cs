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
            var tokens = line.Trim().Split(' ');
            AdeSystem.Actions.Add(tokens[0]);

            ActionName = tokens[0];
            if (line.Contains(" by "))
            {
                ExecutorName = FluentParser.GetSubstring(line, " by ", " preserves").Trim();
                AdeSystem.Executors.Add(ExecutorName);
            }
            if (line.Contains(" if "))
            {
                Fluent = FluentParser.GetSubstring(line, " preserves ", " if ");
                var con = FluentParser.GetSubstring(line, " if ");
                List<string> litVal;
                List<Token> lit;
                var exp = LogicFormulaParser.Parse(con, out lit, out litVal);
                Fluents = LogicFormulaParser.GetConditions(con);

            }
            else
            {
                Fluent = FluentParser.GetSubstring(line, " preserves ");                
            }
        }

        public override void ToProlog()
        {
            //if (!Fluents.Any())
            //{
               // AdeSystem.PrologEngine.AssertFact("preserve(" + ActionName.ToLower() + "," + ExecutorName.ToLower() + ",[" + Fluent.ToLower() + "],[])");
           // }
        }
        public void ToProlog(List<string> executors)
        {
            
            var condition = "";
            if (Fluents != null && Fluents.Any())
            {
                condition = FluentParser.GetConditions(Fluents);
            }
            if (ExecutorName != null && ExecutorName.Any())
            {
                AdeSystem.PrologEngine.AssertFact("releases(" + ActionName.ToLower() + ", " + ExecutorName.ToLower() + ", [" +
                                                  Fluent.ToLower() + "], [" + condition.ToLower() + "])");

            }
            else
            {
                foreach (var executor in executors)
                {
                    AdeSystem.PrologEngine.AssertFact("releases(" + ActionName.ToLower() + ", " + executor.ToLower() + ", [" +
                                                  Fluent.ToLower() + "], [" + condition.ToLower() + "])");
                }
            }
        }
    }
}