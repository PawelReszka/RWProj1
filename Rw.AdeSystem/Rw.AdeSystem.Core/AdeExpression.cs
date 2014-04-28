#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;

#endregion

namespace Rw.AdeSystem.Core
{
    public enum AdeExpressionKind
    {
        DomainPhrase,
        Query
    }

    public class AdeExpression
    {
        public enum Type
        {
            DAlways,
            DAfter,
            DAlwaysCauses,
            DInitially,
            DObservable,
            DTypicallyCauses,
            // etc..
            QTypicallyExecutable,
            QTypicallyInvolved
            // etc..
        }

        private readonly List<string> _actions = new List<string>();
        private readonly List<string> _executors = new List<string>();
        private readonly List<AdeForm> _formulas = new List<AdeForm>();

        // tutaj dla kwerend powinny byc ejszcze stany, ale nie mam pojecia na teraz jak to zrealizowac

        public AdeExpression(string line, AdeExpressionKind kind)
        {
            ParseFromTextLine(line, kind);
        }

        public AdeExpressionKind ExpressionKind { get; private set; }
        public Type SubType { get; private set; }

        private void ParseFromTextLine(string adeString, AdeExpressionKind kind)
        {
            if (string.IsNullOrEmpty(adeString))
            {
                throw new ArgumentException();
            }
            switch (kind)
            {
                case AdeExpressionKind.DomainPhrase:
                {
                    if (adeString.Split(' ').First(s => s.Length > 0) == "always")
                    {
                        SubType = Type.DAlways;
                        ParseDAlways(adeString);
                    }
                    break;
                }
                case AdeExpressionKind.Query:
                {
                    break;
                }
                default:
                    throw new Exception();
            }
        }

        /// <summary>
        ///     Zwraca sposob swojego wykonania na enginie prologa
        /// </summary>
        public Action<PrologEngine> ExecutePrologQuery()
        {
            switch (SubType)
            {
                case Type.DAlways:
                    return DAlwaysToProlog();
                default:
                    throw new Exception();
            }
        }

        private void ParseDAlways(string adeString)
        {
            _formulas.Add(new AdeForm(adeString.Substring(adeString.IndexOf(' ') + 1)));
        }

        private Action<PrologEngine> DAlwaysToProlog()
        {
            // nie wiem czy ostatecznie tak bedzie wygladalo wywolanie always w prologu
            // ale zeby zrozumiec ocb
            return pe => pe.AssertFact(_formulas.First().ToPrologString());
        }
    }
}