using System;
using System.Collections.Generic;
using System.Linq;

namespace Rw.AdeSystem.Core.Expressions
{
    public class InitiallyExpression : Expression
    {
        private List<string> Fluents { get; set; }
        public InitiallyExpression(string line) : base(line)
        {
            var fs = line.Trim().Remove(0, "initially".Length).Trim().Replace(" ", "").Split('&').ToList();
            Fluents = fs.Select(i => i.Replace("!", "not_")).ToList();
            AdeSystem.Fluents.AddRange(fs.Select(i=>i.Replace("!","")));

        }

        public override void ToProlog()
        {
            AdeSystem.PrologEngine.AssertFact("initially_after([],[],[])");

            var fluents = String.Join(", ", Fluents);
            AdeSystem.PrologEngine.AssertFact("initially_after([],[],["+fluents+"])");
        }
    }

    public class AfterExpression: Expression
    {
        private List<string> Fluents { get; set; }
        private List<string> Executors { get; set; }
        private string ActionsString { get; set; }
        private string ExecutorsString { get; set; }
        protected string prefix;
        public AfterExpression(string line)
            : base(line)
        {
           var conditions= line.Substring(0, line.IndexOf("after")).Trim();
            List<string> litValues;
            List<Token> literals;
            var expression = LogicFormulaParser.Parse(conditions, out literals, out litValues);
            Fluents = LogicFormulaParser.GetFluentStrings(expression);
            if(line.Contains("by"))
            {
                ActionsString = FluentParser.GetSubstring(line, " after ", " by ");
                ExecutorsString = FluentParser.GetSubstring(line, " by ");  
            }
            else
            {
                ActionsString = FluentParser.GetSubstring(line, " after ");
                char[] tab=new char[]{','};
                var len=ActionsString.Split().Length;
                List<string> l=new List<string>();
                for(int i=0;i<len;i++)
                    l.Add("epsilon");
                ExecutorsString =  String.Join(", ", l);
            }
        }

        public override void ToProlog()
        {
            AdeSystem.PrologEngine.AssertFact("initially_after([],[],[])");
            foreach (var fluent in Fluents)
            {
                AdeSystem.PrologEngine.AssertFact(prefix + "_after([" + ActionsString + "],[" + ExecutorsString + "],[" + fluent + "])");                
            }
        }

       
    }

    public class InitiallyAfterExpression : AfterExpression
    {
        public InitiallyAfterExpression(string line)
            : base(line)
        {
            prefix = "initially";
        }
    }
    public class ObservableAfterExpression : AfterExpression
    {
        
        public ObservableAfterExpression(string line)
            : base(line.Trim().Remove(0, "observable".Length).Trim())
        {
            prefix = "observable";
        }
    }
}