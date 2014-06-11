using System.Collections.Generic;

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
                var startIndex = str.IndexOf(start);
                var endIndex = str.IndexOf(end);
                var val = str.Substring(startIndex + start.Length, endIndex - startIndex - start.Length).Trim();
                return val;
            }
            var val2 = str.Substring(str.IndexOf(start) + start.Length).Trim();
            return val2;
        }
    }
}
