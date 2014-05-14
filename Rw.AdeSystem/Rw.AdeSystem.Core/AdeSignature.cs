using System;
using System.Collections.Generic;
using System.Linq;

namespace Rw.AdeSystem.Core
{
    public class AdeSignature
    {
        public AdeSignature()
        {
            Fluents = new List<string>();
            Actions = new List<string>();
            Executors = new List<string>();
        }

        public List<string> Fluents { get; private set; }
        public List<string> Actions { get; private set; }
        public List<string> Executors { get; private set; }

        public static void LoadSignatureFromFile(string filename)
        {
            LoadSignature(Helpers.LoadFromFile(filename));
        }

        public static void LoadSignature(string signature)
        {
            // TODO: trzeba ustalic strukture plikow/tekstu ktore wczytujemy do sygnatury/domeny/akcji (na razie przyjmuje 3 linijki, wyrazenia oddzielone przecinkami
            var lines = signature.Split(new[] { Environment.NewLine }, StringSplitOptions.None);
            if (lines.Length != 3)
            {
                throw new ArgumentException();
            }
            AdeSystem.Signature.Fluents.AddRange(lines[0].Split(',', ' ').Where(s => s.Length > 0));
            AdeSystem.Signature.Actions.AddRange(lines[1].Split(',', ' ').Where(s => s.Length > 0));
            AdeSystem.Signature.Executors.AddRange(lines[2].Split(',', ' ').Where(s => s.Length > 0));
        }

        public static void LoadSignature(string fluents, string actions, string executors)
        {
            AdeSystem.Signature.Fluents.AddRange(fluents.Split(',', ' ').Where(s => s.Length > 0));
            AdeSystem.Signature.Actions.AddRange(actions.Split(',', ' ').Where(s => s.Length > 0));
            AdeSystem.Signature.Executors.AddRange(executors.Split(',', ' ').Where(s => s.Length > 0));
        }
    }
}