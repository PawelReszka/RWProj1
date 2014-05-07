using System;
using System.Text.RegularExpressions;
using SbsSW.SwiPlCs;

namespace Rw.AdeSystem.Core
{
    static class Program
    {
        static void Main(string[] args)
        {
            string always = "always (h & c) | !f";
            string initially = "initially h & l";

            var a = Regex.IsMatch(always, @"always [a-z,|,&,!,\s,(,)]*");
            var ax = Regex.IsMatch(initially, @"always [a-z,|,&,!,\s,(,)]*");
            var b = Regex.IsMatch(initially, @"initially [a-z,&,!,\s]*");

            // ------------
            // przyklad z instrukcji do wrappera SwiPlCs :

            //Environment.SetEnvironmentVariable("SWI_HOME_DIR", @"C:\swipl\bin");
            if (PlEngine.IsInitialized) return;
            String[] param = { /*"-q"*/ };  // suppressing informational and banner messages
            PlEngine.Initialize(param);
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

            var filename = "./text.txt";
            var system = new AdeSystem(param);
            system.LoadDomainFromFile(filename);

            //using (var q = new PlQuery("is_true(X), atomic_list_concat([X,' is_true '], L)"))
            //{
            //    foreach (var v in q.SolutionVariables)
            //        Console.WriteLine(v["L"].ToString());
            //}
   
            system.ConstructSystemDomain();
            using (var q = new PlQuery("causes(P, C, X), atomic_list_concat([P,' is_father_of ',C], L)"))
            {
                //q.Variables["P"].Unify("h");
                //q.Variables["C"].Unify("h");
                foreach (var v in q.SolutionVariables)
                    Console.WriteLine(v["L"].ToString());

            }

            Console.WriteLine("finshed!");
        }
    }
}