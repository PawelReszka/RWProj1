#region Using directives

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

#endregion

namespace Rw.AdeSystem.Core
{
    public class AdeSystem
    {
        #region Fields & ctors

        private readonly List<AdeExpression> _domainPhrases = new List<AdeExpression>();
        private readonly PrologEngine _prologEngine = PrologEngine.Instance;
        private readonly Signature _signature = new Signature();

        public AdeSystem(params string[][] prologEngineInitParams)
        {
            _prologEngine.Initialize(prologEngineInitParams);
        }

        public AdeSystem(params string[] prologEngineInitParams)
        {
            _prologEngine.Initialize(prologEngineInitParams);
        }

        #endregion

        #region Language signature

        public void LoadSignatureFromFile(string filename)
        {
            LoadSignature(LoadFromFile(filename));
        }

        public void LoadSignature(string signature)
        {
            // TODO: trzeba ustalic strukture plikow/tekstu ktore wczytujemy do sygnatury/domeny/akcji (na razie przyjmuje 3 linijki, wyrazenia oddzielone przecinkami
            var lines = signature.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
            if (lines.Length != 3)
            {
                throw new ArgumentException();
            }
            lines[0].Split(',', ' ').Where(s => s.Length > 0).ToList().ForEach(s => _signature.Fluents.Add(s));
            lines[1].Split(',', ' ').Where(s => s.Length > 0).ToList().ForEach(s => _signature.Actions.Add(s));
            lines[2].Split(',', ' ').Where(s => s.Length > 0).ToList().ForEach(s => _signature.Executors.Add(s));
        }

        public void LoadSignature(string fluents, string actions, string executors)
        {
            fluents.Split(',', ' ').Where(s => s.Length > 0).ToList().ForEach(s => _signature.Fluents.Add(s));
            actions.Split(',', ' ').Where(s => s.Length > 0).ToList().ForEach(s => _signature.Actions.Add(s));
            executors.Split(',', ' ').Where(s => s.Length > 0).ToList().ForEach(s => _signature.Executors.Add(s));
        }

        private class Signature
        {
            public Signature()
            {
                Fluents = new HashSet<string>();
                Actions = new HashSet<string>();
                Executors = new HashSet<string>();
            }

            public HashSet<string> Fluents { get; private set; }
            public HashSet<string> Actions { get; private set; }
            public HashSet<string> Executors { get; private set; }
        }

        #endregion

        #region System domain

        public void LoadDomainFromFile(string filename)
        {
            LoadDomain(LoadFromFile(filename));
        }

        public void LoadDomain(string domainInAdeString)
        {
            foreach (var line in domainInAdeString.Split(new[] {Environment.NewLine}, StringSplitOptions.None))
            {
                _domainPhrases.Add(new AdeExpression(line, AdeExpressionKind.DomainPhrase));
            }
            if (_domainPhrases.Count == 0)
            {
                throw new ArgumentException();
            }
        }

        public void ConstructSystemDomain()
        {
            foreach (var phrase in _domainPhrases)
            {
                phrase.ExecutePrologQuery()(_prologEngine);
            }
        }

        #endregion

        #region Queries and querying

        #endregion

        #region Helpers

        private string LoadFromFile(string filename)
        {
            string ret;
            using (var sr = new StreamReader(filename))
            {
                ret = sr.ReadToEnd();
            }
            return ret;
        }

        #endregion
    }
}