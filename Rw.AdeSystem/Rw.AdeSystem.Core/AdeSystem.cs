#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using Rw.AdeSystem.Core.Expressions;
using Rw.AdeSystem.Core.Queries;

#endregion

namespace Rw.AdeSystem.Core
{
    public static class AdeSystem
    {
        #region Fields & ctors

        public static readonly List<Expression> DomainPhrases = new List<Expression>();
        public static readonly PrologEngine PrologEngine = PrologEngine.Instance;
        public static readonly AdeSignature Signature = new AdeSignature();

        public static List<string> Actions = new List<string>();
        public static List<string> Executors = new List<string>();
        public static List<string> Fluents = new List<string>();
        public static List<string> NoninertialFluents = new List<string>();

        public static void Initialize(params string[][] prologEngineInitParams)
        {
            PrologEngine.Initialize(prologEngineInitParams);
        }

        public static void Initialize(params string[] prologEngineInitParams)
        {
            PrologEngine.Initialize(prologEngineInitParams);
        }

        #endregion

        #region System domain

        public static void LoadDomainFromFile(string filename)
        {
            LoadDomain(Helpers.LoadFromFile(filename));
        }

        public static void LoadDomain(string domainInAdeString)
        {
            //TODO: Dodac wyrażenia regularne dla pozostałych wyrażeń języka 
            //Na razie wystarczy, żeby rozróżniać wyrażenia i załadować akcje/wykonawcow do odpowiednich list - Actions, Executors
            //Fluenty na razie można zostawić - parser wyrażen logicznych zrobi Konrad i Paweł
            //Na razie nie trzeba robic nic Prologowego

            foreach (var lineNotTrimmed in domainInAdeString.Split(new[] {Environment.NewLine}, StringSplitOptions.None))
            {
                var line = lineNotTrimmed.Trim();
                //Always
                if (Regex.IsMatch(line, @"always [a-z,|,&,!,\s,(,)<=>]*"))
                {
                    DomainPhrases.Add(new AlwaysExpression(line));
                }
                //Initially
                else if (Regex.IsMatch(line, @"initially [a-z,&,!,\s]*"))
                {
                    DomainPhrases.Add(new InitiallyExpression(line));
                }
                // zakladam ze slowa kluczowe sa zabronione jako nazwy akcji, fluentow, etc -> mniej kodu
                else if (line.Contains(" releases "))
                {
                    DomainPhrases.Add(new ReleasesExpression(line));    
                }                    
                //by EPSILON Causes xxx if...
                else if (Regex.IsMatch(line, @"[A-Z]+ causes [a-z,&,!,\s]* if [a-z,&,!,\s]*"))
                {
                    DomainPhrases.Add(new CausesIfExpression(line));
                }
                //by EXECUTORS Causes xxx if...
                else if (Regex.IsMatch(line, @"[A-Z]+ by [a-zA-Z,\s]+ causes [a-z,&,!,\s]* if [a-z,&,!,\s]*"))
                {
                    DomainPhrases.Add(new ByCausesIfExpression(line));
                }
                //by EPSILON Causes
                else if (Regex.IsMatch(line, @"[A-Z]+ causes [a-z,&,!,\s]*"))
                {
                    DomainPhrases.Add(new CausesExpression(line));
                }
                // by EXECUTORS Causes
                else if (Regex.IsMatch(line, @"[A-Z]+ by [a-zA-Z,\s]+ causes [a-z&!\s]*"))
                {
                    DomainPhrases.Add(new ByCausesExpression(line));
                }
                //by EPSILON Typically Causes xxx if...
                else if (Regex.IsMatch(line, @"[A-Z]+ typically causes [a-z,&,!,\s]* if [a-z,&,!,\s]*"))
                {
                    DomainPhrases.Add(new TypicallyCausesIfExpression(line));
                }
                //by EXECUTORS Typically Causes xxx if...
                else if (Regex.IsMatch(line, @"[A-Z]+ by [a-zA-Z,\s]+ typically causes [a-z,&,!,\s]* if [a-z,&,!,\s]*"))
                {
                    DomainPhrases.Add(new ByTypicallyCausesIfExpression(line));
                }
                //by EPSILON Typically Causes
                else if (Regex.IsMatch(line, @"[A-Z]+ typically causes [a-z,&,!,\s]*"))
                {
                    DomainPhrases.Add(new TypicallyCausesExpression(line));
                }
                // by EXECUTORS Typically Causes
                else if (Regex.IsMatch(line, @"[A-Z]+ by [a-zA-Z,\s]+ typically causes [a-z&!\s]*"))
                {
                    DomainPhrases.Add(new ByTypicallyCausesExpression(line));
                }
                
                //by EXECUTORS preserves xxx if...
                else if (Regex.IsMatch(line, @"[A-Z]+ by [a-zA-Z,\s]+ preserves [a-z,&,!,\s]* if [a-z,&,!,\s]*"))
                {
                    DomainPhrases.Add(new PreservesExpression(line));
                }
                //noninertial x
                else if (Regex.IsMatch(line, @"noninertial [a-z,&,!,\s]*"))
                {
                    DomainPhrases.Add(new NoninertialExpression(line));
                }
                else
                {
                    throw new ArgumentException("Nieznane wyrażenie języka ADE.");
                }
            }
            if (DomainPhrases.Count == 0)
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Metoda konstruujaca podstawowe fakty i reguly Prologa
        /// </summary>
        public static void ConstructSystemDomain()
        {
            LoadEngine();
            Fluents = Fluents.Select(i=>i.ToLower()).Distinct().ToList();
            Actions = Actions.Select(i => i.ToLower()).Distinct().ToList();
            Executors = Executors.Select(i => i.ToLower()).Distinct().ToList();

            foreach (var f in Fluents)
            {
                PrologEngine.AssertFact("fluent(" + f + ")");
            }

            foreach (var action in Actions)
            {
                PrologEngine.AssertFact("action(" + action + ")");
            }
            foreach (var executor in Executors)
            {
                PrologEngine.AssertFact("executor(" + executor + ")");
            }
            foreach (var f in Fluents)
            {
                PrologEngine.AssertFact("sneg(" + f + ",not_" + f + ")");
            }
            foreach (var f in Fluents)
            {
                PrologEngine.AssertFact("sinertial("+ f + ")");
            }
            int counter = 0;
            foreach (var f in Fluents)
            {
                PrologEngine.AssertFact("order("+counter+", "+f+")");
                counter++;
            }
            InitializeStates(DomainPhrases.OfType<AlwaysExpression>().ToList());
            foreach (var domainPhrase in DomainPhrases)
            {
                domainPhrase.ToProlog();
            }
        }

        public static void InitializeStates(List<AlwaysExpression> expressions)
        {
            int stateCounter = 0;
            for (int i = 0; i < Math.Pow(2,Fluents.Count()); i++)
            {
                var toProlog = true;
                var dictionary = new Dictionary<string, bool>();
                for (int j = 0; j < Fluents.Count; j++)
                {
                    dictionary.Add(Fluents[j], GetBitValue(i, j));
                }
                foreach (var e in expressions)
                {
                    
                    if (!LogicFormulaParser.Eval(e.Expression, dictionary))
                    {
                        toProlog = false;
                        break;
                    }

                }
                if(!toProlog) continue;
                var fs = dictionary.Where(a => a.Value).Select(a => a.Key).ToList();
                fs.AddRange(dictionary.Where(a => !a.Value).Select(a => "not_"+a.Key));
                var fluents = String.Join(",", fs);
                PrologEngine.AssertFact("state(state"+stateCounter+",["+fluents+"])");
                stateCounter++;
            }
        }

        public static string ParseQuery(string query)
        {
            query = query.ToLower();
            Query q = null;
            if (query.Contains("always accesible"))
            {
                q = new AlwaysAccessibleQuery(query);
            }
            else if(query.Contains("always") && query.Contains("after"))
            {
                q = new AlwaysAfterQuery(query);
            }
            else if (query.Contains("always executable"))
            {
                q = new AlwaysExecutableQuery(query);
            }
            else if (query.Contains("always involved"))
            {
                q = new AlwaysInvolvedQuery(query);
            }
            else if (query.Contains("possibly accesible"))
            {
                q = new PossiblyAccessibleQuery(query);
            }
            else if (query.Contains("possibly") && query.Contains("after"))
            {
                q = new PossiblyAfterQuery(query);
            }
            else if (query.Contains("possibly executable"))
            {
                q = new PossiblyExecutableQuery(query);
            }
            else if (query.Contains("possibly involved"))
            {
                q = new PossiblyInvolvedQuery(query);
            }
            else if (query.Contains("typically accesible"))
            {
                q = new TypicallyAccessibleQuery(query);
            }
            else if (query.Contains("typically") && query.Contains("after"))
            {
                q = new TypicallyAfterQuery(query);
            }
            else if (query.Contains("typically involved"))
            {
                q = new TypicallyInvolvedQuery(query);
            }
            if(q!=null)
                q.ToProlog();
            return "Nie rozpoznana kwerenda";
        }

        private static bool GetBitValue(int number, int position)
        {
            return (number & (1 << position - 1)) != 0;
        }

        private static void LoadEngine()
        {
            PrologEngine.AssertFacts("x");
           
        }
        #endregion
    }
}