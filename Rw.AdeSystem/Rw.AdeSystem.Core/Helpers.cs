using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;

namespace Rw.AdeSystem.Core
{
    /// <summary>
    /// Klasa z funkcjami pomocniczymi
    /// </summary>
    public static class Helpers
    {
        /// <summary>
        /// Funkcja tworzace liste stringow z tekstu na podstawie wzorca 
        /// </summary>
        /// <param name="template">Wzorzec (analogiczny jak przy String.Format)</param>
        /// <param name="str">Tekst, na ktorym ma byc uzyty wzorzec</param>
        public static List<string> ReverseStringFormat(string template, string str)
        {
            var pattern = "^" + Regex.Replace(template, @"\{[0-9]+\}", "(.*?)") + "$";

            var r = new Regex(pattern);
            var m = r.Match(str);

            var ret = new List<string>();

            for (var i = 1; i < m.Groups.Count; i++)
            {
                ret.Add(m.Groups[i].Value.ToLower());
            }

            return ret;
        }

        /// <summary>
        /// Funkcja czytajaca plik
        /// </summary>
        /// <param name="filename">Nazwa pliku do czytania</param>
        /// <returns>Zawartosc tekstowa pliku</returns>
        public static string LoadFromFile(string filename)
        {
            string ret;
            using (var sr = new StreamReader(filename))
            {
                ret = sr.ReadToEnd();
            }
            return ret;
        }
    }
}