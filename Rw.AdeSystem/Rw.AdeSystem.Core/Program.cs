using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SbsSW.SwiPlCs;

namespace Rw.AdeSystem.Core
{
    static class Program
    {
        static void Main()
        {
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            PlEngine.Initialize(param);

            const string filename = "./test1.txt";
            AdeSystem.Initialize(param);
            string text = System.IO.File.ReadAllText(filename);
            AdeSystem.LoadDomain(text);
            AdeSystem.ConstructSystemDomain();
            AdeSystem.ParseQuery("possibly involved hador in chown by hador");
            AdeSystem.ParseQuery("possibly involved huj in chowner by epsilon");
            AdeSystem.ParseQuery("possibly involved h in chown, shoot by epsilon, epsilon");
            //Parsowanie wyrazenia logicznego - w literals sa fluenty, a drzewo wyrazenia w logicTree
            List<Token> literals;
            List<string> literalValues;
            var expr = "(A|!B)&C";
            BoolExpr logicTree = LogicFormulaParser.Parse(expr, out literals, out literalValues);
            var dict = literals.ToDictionary(i => i.Value, i => i.Value == "A" || i.Value == "B");
            var b = LogicFormulaParser.Eval(logicTree, dict);
            var simplifiedTree = LogicFormulaParser.AndOrReformTree(logicTree);
            var strings = LogicFormulaParser.GetFluentStrings(simplifiedTree);
            Console.WriteLine(b);
            // ------------
            // przyklad z instrukcji do wrappera SwiPlCs :

            //Environment.SetEnvironmentVariable("SWI_HOME_DIR", @"C:\swipl\bin");
            if (PlEngine.IsInitialized) return;
            //String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            //PlEngine.Initialize(param);

            //const string filename = "./text.txt";
            //AdeSystem.Initialize(param);
            AdeSystem.LoadDomainFromFile(filename);

            //PlQuery.PlCall("assert(father(martin, inka))");
            //PlQuery.PlCall("assert(father(uwe, gloria))");
            //PlQuery.PlCall("assert(father(uwe, melanie))");
            //PlQuery.PlCall("assert(father(uwe, ayala))");
            //using (var q = new PlQuery("father(P, C), atomic_list_concat([P,' is_father_of ',C], L)"))
            //{
            //    foreach (var v in q.SolutionVariables)
            //        Console.WriteLine(v["L"].ToString());

            //    Console.WriteLine("all child's from uwe:");
            //    q.Variables["P"].Unify("uwe");
            //    foreach (var v in q.SolutionVariables)
            //        Console.WriteLine(v["C"].ToString());
            //}
            PlEngine.PlCleanup();
        }
    }
}