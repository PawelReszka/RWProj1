using System;
using SbsSW.SwiPlCs;

namespace Rw.AdeSystem.Core
{
    static class Program
    {
        static void Main(string[] args)
        {
            //Environment.SetEnvironmentVariable("SWI_HOME_DIR", @"C:\swipl\bin");
            if (PlEngine.IsInitialized) return;
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            PlEngine.Initialize(param);
            PlQuery.PlCall("assert(father(martin, inka))");
            PlQuery.PlCall("assert(father(uwe, gloria))");
            PlQuery.PlCall("assert(father(uwe, melanie))");
            PlQuery.PlCall("assert(father(uwe, ayala))");
            using (var q = new PlQuery("father(P, C), atomic_list_concat([P,' is_father_of ',C], L)"))
            {
                foreach (var v in q.SolutionVariables)
                    Console.WriteLine(v["L"].ToString());

                Console.WriteLine("all child's from uwe:");
                q.Variables["P"].Unify("uwe");
                foreach (var v in q.SolutionVariables)
                    Console.WriteLine(v["C"].ToString());
            }
            PlEngine.PlCleanup();
            Console.WriteLine("finshed!");
        }
    }
}