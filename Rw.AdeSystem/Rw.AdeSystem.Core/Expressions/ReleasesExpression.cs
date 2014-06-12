using System;
using System.Collections.Generic;
using System.Linq;

namespace Rw.AdeSystem.Core.Expressions
{
    public class ReleasesExpression : Expression
    {
        public List<string> Conditions = new List<string>();

        public List<string> Effects = new List<string>();

        public string Executor { get; set; }

        public string ActionName { get; set; }

        public ReleasesExpression(string line) : base(line)
        {
            var tokens = line.Trim().Split(' ');
            AdeSystem.Actions.Add(tokens[0]);
            
            ActionName = tokens[0];
            
            
            AdeSystem.Fluents.Add(tokens.Last().Replace("!", ""));
            
            if (line.Contains(" by "))
            {
                Executor = FluentParser.GetSubstring(line, " by ", " releases ");
                AdeSystem.Executors.Add(Executor);
                
            }
            if (line.Contains(" if "))
            {
                var con = line.Substring(line.IndexOf("if") + 2).Trim();
                Conditions = LogicFormulaParser.GetConditions(con);
                Effects.Add(FluentParser.GetSubstring(line, " releases ", " if "));
            }
            else
            {
                Effects.Add(FluentParser.GetSubstring(line, " releases "));                
            }
        }



        public override void ToProlog()
        {
            
        }

        public void ToProlog(List<string> executors)
        {
            var effects = String.Join(", ", Effects);
            var condition = "[]";
            if (Conditions != null && Conditions.Any())
            {
                condition = FluentParser.GetConditions(Conditions);
            }
            if (Executor != null && Executor.Any())
            {
                AdeSystem.PrologEngine.AssertFact("releases(" + ActionName.ToLower() + ", " + Executor.ToLower() + ", [" +
                                                  effects.ToLower() + "], [" + condition.ToLower() + "])");

            }
            else
            {
                foreach (var executor in executors)
                {
                    AdeSystem.PrologEngine.AssertFact("releases(" + ActionName.ToLower() + ", " + executor.ToLower() + ", [" +
                                                  effects.ToLower() + "], [" + condition.ToLower() + "])");
                }
            }
        }
    }
}