#region Using directives

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Rw.AdeSystem.Core.Expressions;

#endregion

namespace Rw.AdeSystem.Core
{
    public static class AdeSystem
    {
        #region Fields & ctors

        public static readonly List<Expression> DomainPhrases = new List<Expression>(); 
        public static readonly PrologEngine PrologEngine = PrologEngine.Instance;
        public static readonly AdeSignature Signature = new AdeSignature();

        public static readonly List<string> Actions = new List<string>();
        public static readonly List<string> Executors = new List<string>();
        public static readonly List<string> Fluents = new List<string>();

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
            //TODO: Dodac wyrażenia regularne dla pozostałych wyrażeń języka - Justyna
            //Na razie wystarczy, żeby rozróżniać wyrażenia i załadować akcje/wykonawcow do odpowiednich list - Actions, Executors
            //Fluenty na razie można zostawić - parser wyrażen logicznych zrobi Konrad i Paweł
            //Na razie nie trzeba robic nic Prologowego
            foreach (var line in domainInAdeString.Split(new[] {Environment.NewLine}, StringSplitOptions.None))
            {
                if (Regex.IsMatch(line, @"always [a-z,|,&,!,\s,(,)]*"))
                {
                    DomainPhrases.Add(new AlwaysExpression(line));
                }
                else if (Regex.IsMatch(line, @"initially [a-z,&,!,\s]*"))
                {
                    DomainPhrases.Add(new InitiallyExpression(line));
                }
                else if (Regex.IsMatch(line, @"[A-Z]+ causes [a-z,&,!,\s]*"))
                {
                    throw new NotImplementedException();

                    //W komentarzu jest jakies nasze podejscie do czytania akcji etc z wyrazenia

                    //var format = ReverseStringFormat("{0} causes {1}", line);
                    //Actions.Add(format[0]);
                }
                else if (Regex.IsMatch(line, @"[A-Z]+ by [a-zA-Z]+ causes [a-z,&,!,\s]*"))
                {
                    throw new NotImplementedException();

                    //W komentarzu jest jakies nasze podejscie do czytania akcji etc z wyrazenia

                    //var format = ReverseStringFormat("{0} by {1} causes {2}", line);
                    //Actions.Add(format[0]);
                    //Executors.Add(format[1]);
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
            //Na razie sa tu jakies przykladowe, proste reguly
            PrologEngine.AssertFact("and(A,B):-(is_true(A),is_true(B))");
            PrologEngine.AssertFact("or(A,B):-(is_true(A); is_true(B))");
            PrologEngine.AssertFact("neg(A):-not(is_true(A))");
        }

        #endregion
    }
}