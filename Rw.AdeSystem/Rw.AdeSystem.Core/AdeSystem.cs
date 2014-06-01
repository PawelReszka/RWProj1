﻿#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
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
            //TODO: Dodac wyrażenia regularne dla pozostałych wyrażeń języka 
            //Na razie wystarczy, żeby rozróżniać wyrażenia i załadować akcje/wykonawcow do odpowiednich list - Actions, Executors
            //Fluenty na razie można zostawić - parser wyrażen logicznych zrobi Konrad i Paweł
            //Na razie nie trzeba robic nic Prologowego
            foreach (var line in domainInAdeString.Split(new[] { Environment.NewLine }, StringSplitOptions.None))
            {
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
                //by EPSILON Causes xxx if...
                else if (Regex.IsMatch(line, @"[A-Z]+ causes [a-z,&,!,\s]* if [a-z,&,!,\s]*"))
                {
                    DomainPhrases.Add(new CausesIfExpression(line));
                }
                //by EXECUTORS Causes xxx if...
                else if (Regex.IsMatch(line, @"[A-Z]+ by [a-zA-Z,\s]+ causes [a-z,&,!,\s]* if [a-z,&,!,\s]*"))
                {
                    throw new NotImplementedException();
                }
                //by EPSILON Causes
                else if (Regex.IsMatch(line, @"[A-Z]+ causes [a-z,&,!,\s]*"))
                {
                    DomainPhrases.Add(new CausesExpression(line));

                    //W komentarzu jest jakies nasze podejscie do czytania akcji etc z wyrazenia

                    //var format = ReverseStringFormat("{0} causes {1}", line);
                    //Actions.Add(format[0]);
                }
                // by EXECUTORS Causes
                else if (Regex.IsMatch(line, @"[A-Z]+ by [a-zA-Z,\s]+ causes [a-z&!\s]*"))
                {
                    throw new NotImplementedException();

                    //W komentarzu jest jakies nasze podejscie do czytania akcji etc z wyrazenia

                    //var format = ReverseStringFormat("{0} by {1} causes {2}", line);
                    //Actions.Add(format[0]);
                    //Executors.Add(format[1]);
                }

                //by EPSILON Typically Causes
                else if (Regex.IsMatch(line, @"[A-Z]+ typically causes [a-z,&,!,\s]*"))
                {
                    throw new NotImplementedException();
                }
                // by EXECUTORS Typically Causes
                else if (Regex.IsMatch(line, @"[A-Z]+ by [a-zA-Z,\s]+ typically causes [a-z&!\s]*"))
                {
                    throw new NotImplementedException();
                }
                //by EPSILON Typically Causes xxx if...
                else if (Regex.IsMatch(line, @"[A-Z]+ typically causes [a-z,&,!,\s]* if [a-z,&,!,\s]*"))
                {
                    throw new NotImplementedException();
                }
                //by EXECUTORS Typically Causes xxx if...
                else if (Regex.IsMatch(line, @"[A-Z]+ by [a-zA-Z,\s]+ typically causes [a-z,&,!,\s]* if [a-z,&,!,\s]*"))
                {
                    throw new NotImplementedException();
                }
                //by EXECUTORS preserves xxx if...
                else if (Regex.IsMatch(line, @"[A-Z]+ by [a-zA-Z,\s]+ preserves [a-z,&,!,\s]* if [a-z,&,!,\s]*"))
                {
                    throw new NotImplementedException();
                }
                //noninertial x
                else if (Regex.IsMatch(line, @"noninertial [a-z,&,!,\s]*"))
                {
                    throw new NotImplementedException();
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
            foreach (var f in Fluents.Distinct())
            {
                PrologEngine.AssertFact("fluent(" + f + ")");
            }
            foreach (var f in Fluents.Distinct())
            {
                PrologEngine.AssertFact("sneg(" + f + ",not_" + f + ")");
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

        private static bool GetBitValue(int number, int position)
        {
            return (number & (1 << position - 1)) != 0;
        }
        #endregion
    }
}