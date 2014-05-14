using System.Collections.Generic;
using SbsSW.SwiPlCs;

namespace Rw.AdeSystem.Core
{
    /// <summary>
    /// Ta klasa ma sluzyc jako wygodny opakowywacz dla wywolan interpretera prologa
    /// </summary>
    public class PrologEngine
    {
        private static PrologEngine _instance;

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

        public void AssertFact(string prologFact)
        {
            PlQuery.PlCall("assert(" + prologFact + ")");
            
        }


        //etc rozne potrzebne call'e
    }
}
