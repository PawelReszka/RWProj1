using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Rw.AdeSystem.Core.Expressions
{
    public class PreservesExpression : Expression
    {
        public string ActionName { get; set; }
        public string ExecutorName { get; set; }
        public List<string> Fluents = new List<string>();

        public PreservesExpression(string line)
            : base(line)
        {
            //nie wiem czy dobrze rozumiem ale np to moze sparsowac : Entice by hador preserves a
            var tokens = line.Trim().Split(' ');
            ActionName = tokens[0];
            ExecutorName = tokens[2]; //2 zeby ominac by? nie wiem czy jakos inaczej trzeba to zrobic
            Fluents.Add(tokens.Last().Replace("!", "not_"));
        }

        public override void ToProlog()
        {
            var fluents = new StringBuilder();
            for (int i = 0; i < Fluents.Count; i++)
            {
                fluents.Append(Fluents[i]);
                if (i != Fluents.Count - 1)
                    fluents.Append(", ");
            }
            AdeSystem.PrologEngine.AssertFact("preserve(" + ActionName + "," + ExecutorName + ",[" + fluents + "])");
        }
    }
}