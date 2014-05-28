using System;
using System.Collections.Generic;
namespace Rw.AdeSystem.Core.Expressions
{
    public class ReleasesExpression : Expression
    {
        protected string prologString = "releases";
//        A by E releases f if f 
//releases(shoot,mietus,[walking], f3).
//releases(shoot,hador,[walking], f4).
        public ReleasesExpression(string line) : base(line)
        {
            var w = line.Split(' ',',','\t');
            prologString += "("+w[0];
            var i = 2;
            if (w[1] == "by")
            {
                i += 2;
                prologString += ", " + w[2];
            }
            var f = new List<string>();
            var testif = false;
            while (i < w.Length)
            {
                if (w[i] == "if")
                {
                    testif = true;
                    break;
                }
                f.Add(w[i]);
                i++;
            }
            prologString += ", " + FluentParser.Parse(f);
            if (!testif)
            {
                prologString+= ", true).";
                return;
            }
            //var literals = new List<Token>();
            //LogicFormulaParser.Parse((line.Substring(line.IndexOf(" if ")+2).Trim(),out literals);
            prologString += ", "+ line.Substring(line.IndexOf(" if ") + 3).Trim() + ").";
        }

        public override void ToProlog()
        {
            AdeSystem.PrologEngine.AssertFact(prologString);
        }
    }
}