using System.Collections.Generic;
using System.IO;
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
            //SaveLine(prologFact);
        }

        public static string ExecuteQuery(string query)
        {
            using (var q = new PlQuery(query))
            {

            }
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
