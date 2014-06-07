using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using SbsSW.SwiPlCs;

namespace Rw.AdeSystem.Core
{
    /// <summary>
    /// Ta klasa ma sluzyc jako wygodny opakowywacz dla wywolan interpretera prologa
    /// </summary>
    public class PrologEngine
    {
        private static PrologEngine _instance;

        private static string path = "./test.txt";
        private PrologEngine()
        {
        }

        public static PrologEngine Instance
        {
            get { return _instance ?? (_instance = new PrologEngine()); }
        }

        public void Initialize(params string[][] initParams)
        {
            var p = new List<string>();
            foreach (var strArr in initParams)
            {
                p.AddRange(strArr);
            }
            if (PlEngine.IsInitialized) return;
            PlEngine.Initialize(p.ToArray());
        }

        public void Initialize(params string[] initParams)
        {
            if (PlEngine.IsInitialized) return;
            PlEngine.Initialize(initParams);
        }
        public static void CreateFile()
        {
            using (new StreamWriter(path, false))
            {
            }
        }

        private static void SaveLine(string line)
        {
            using (var sw = new StreamWriter(path, true))
            {
                sw.WriteLine(line);
            }
        }
        public void AssertFact(string prologFact)
        {
            PlQuery.PlCall("assert(" + prologFact + ")");
            SaveLine(prologFact);
        }

        public static string ExecuteQuery(string query)
        {
            //query = "inertial(huu)";
            using (var q = new PlQuery(query))
            {
                var x = q.NextSolution();
                var y = q.NextSolution();
                var z = q.NextSolution();
                var d = q.NextSolution();
                var a = q.NextSolution();
                var ad = q.NextSolution();
                var ass = q.NextSolution();
                var aaa = q.NextSolution();
                x = x;
                foreach (var sol in q.Solutions)
                {
                    var asdaa = sol.ToString();
                    x = x;
                }
                x = x;
            }
            return "";
        }

        public void AssertFacts(string prologFacts)
        {
            PlQuery.PlCall("consult('" + "engine.pl" + "')");
            var x = 2;
            //var facts = prologFacts.Split('\n');
            //foreach (var prologFact in facts)
            //{
            //    PlQuery.PlCall("assert((" + prologFact + "))");                
            //}
        }


        //etc rozne potrzebne call'e
    }
}
