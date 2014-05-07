#region Using directives

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

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
        public enum AdeType
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
        public readonly List<string> _fluents = new List<string>();

        // tutaj dla kwerend powinny byc ejszcze stany, ale nie mam pojecia na teraz jak to zrealizowac

        public AdeExpression(string line, AdeExpressionKind kind)
        {
            ParseFromTextLine(line, kind);
        }

        public AdeExpressionKind ExpressionKind { get; private set; }
        public AdeType SubType { get; private set; }

        private List<string> reverseStringFormat(string template, string str)
        {
            string pattern = "^" + Regex.Replace(template, @"\{[0-9]+\}", "(.*?)") + "$";

            Regex r = new Regex(pattern);
            Match m = r.Match(str);

            List<string> ret = new List<string>();

            for (int i = 1; i < m.Groups.Count; i++)
            {
                ret.Add(m.Groups[i].Value.ToLower());
            }

            return ret;
        }

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
                    //if (adeString.Split(' ').First(s => s.Length > 0) == "always")
                    if (Regex.IsMatch(adeString, @"always [a-z,|,&,!,\s,(,)]*"))
                    {
                        SubType = AdeType.DAlways;
                    }
                    else if (Regex.IsMatch(adeString, @"initially [a-z,&,!,\s]*"))
                    {
                        SubType = AdeType.DInitially;

                        var fluents = adeString.Trim().Remove(0, "initially".Length).Trim().Replace(" ", "");
                        _fluents.Add(fluents);
                        _fluents.ForEach(i => PrologEngine.Instance.AssertFact(string.Format("is_true({0})", i)));
                    }
                    else if (Regex.IsMatch(adeString, @"[A-Z]+ causes [a-z,&,!,\s]*"))
                    {
                        var xx = reverseStringFormat("{0} causes {1}", adeString);
                        _actions.Add(xx[0]);
                        PrologEngine.Instance.AssertFact(string.Format("causes({0},{1},_)", xx[0], xx[1]));
                        SubType = AdeType.DAlwaysCauses;
                    }
                    else if (Regex.IsMatch(adeString, @"[A-Z]+ by [a-zA-Z]+ causes [a-z,&,!,\s]*"))
                    {
                        var xx = reverseStringFormat("{0} by {1} causes {2}", adeString);
                        _actions.Add(xx[0]);
                        _executors.Add(xx[1]);
                        PrologEngine.Instance.AssertFact(string.Format("causes({0},{1},and(h,a))", xx[0], xx[1]));
                        SubType = AdeType.DAlwaysCauses;
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
                case AdeType.DAlways:
                    return DAlwaysToProlog();
                default:
                    throw new Exception();
            }
        }

        private void ParseDAlways(string adeString)
        {
            _fluents.Add(adeString.Substring(adeString.IndexOf(' ') + 1));
        }

        private Action<PrologEngine> DAlwaysToProlog()
        {
            // nie wiem czy ostatecznie tak bedzie wygladalo wywolanie always w prologu
            // ale zeby zrozumiec ocb
            return pe => pe.AssertFact(_fluents.First());
        }

        private Action<PrologEngine> DInitiallyToProlog()
        {
            // nie wiem czy ostatecznie tak bedzie wygladalo wywolanie always w prologu
            // ale zeby zrozumiec ocb
            return pe => pe.AssertFact(_fluents.First());
        }
    }
}