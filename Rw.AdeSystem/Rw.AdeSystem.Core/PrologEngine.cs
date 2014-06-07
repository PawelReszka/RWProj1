using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public static bool ExecuteQuery(string query)
        {
            //query = "inertial(huu)";
            bool result = false;
            using (var q = new PlQuery(query))
            {
                result = q.NextSolution();
            }
            return result;
        }

        public void AssertFacts(string prologFacts)
        {
            string path = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), @"engine.pl");
            PlQuery.PlCall("consult('" + "D:/engine.pl" + "')");
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
