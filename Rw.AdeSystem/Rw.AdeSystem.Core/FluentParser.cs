using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rw.AdeSystem.Core
{
    public static class FluentParser
    {
        public static string Parse(string fluentCommaSep)
        {
            return "["+fluentCommaSep.Trim().Replace("!","not_")+"]";
        }
        public static string Parse(IList<string> fluents)
        {
            if (fluents.Count == 1) return Parse(fluents[0]);
            return "[" +  string.Join(", ",fluents).Trim().Replace("!", "not_") + "]";
        }

        public static string GetSubstring(string str, string start, string end = null)
        {
            str = str.Trim();
            if (end != null)
            {
                return str.Substring(str.IndexOf(str) + str.Length, str.IndexOf(str)).Trim();
            }
            return str.Substring(str.IndexOf(str) + str.Length).Trim();
        }
    }
}
