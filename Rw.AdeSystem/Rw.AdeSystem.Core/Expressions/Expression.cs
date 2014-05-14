
namespace Rw.AdeSystem.Core.Expressions
{
    /// <summary>
    /// Bazowa (abstrakcyjna) klasa dla wyrazen jezyka ADE
    /// </summary>
    public abstract class Expression
    {
        /// <summary>
        /// Wyrazenie jezyka ADE
        /// </summary>
        public string Content { get; set; }

        /// <summary>
        /// Konstruktor wyrazenia
        /// </summary>
        /// <param name="line">Wyrazenie jezyka ADE</param>
        public Expression(string line)
        {
            Content = line;
        }

        /// <summary>
        /// Metoda, ktora dodaje do nowe fakty i reguly, odpowiednie dla danego wyrazenia, do bazy wiedzy Prologa
        /// </summary>
        public abstract void ToProlog();
    }
}