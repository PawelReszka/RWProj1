namespace Rw.AdeSystem.Core.Queries
{
    /// <summary>
    /// Bazowa (abstrakcyjna) klasa dla kwerend jezyka ADE
    /// </summary>
    public abstract class Query
    {
        /// <summary>
        /// Kwerenda jezyka ADE
        /// </summary>
        public string Content { get; set; }

         /// <summary>
        /// Konstruktor kwerendy
        /// </summary>
        /// <param name="line">Kwerenda jezyka ADE</param>
        public Query(string line)
        {
            Content = line;
        }

        /// <summary>
        /// Metoda, ktora wykonuje zapytanie do bazy wiedzy Prologa
        /// </summary>
        public abstract void ToProlog();
    }
}