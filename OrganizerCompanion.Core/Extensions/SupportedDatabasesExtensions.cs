using OrganizerCompanion.Core.Enums;

namespace OrganizerCompanion.Core.Extensions
{
    internal static class SupportedDatabasesExtensions
    {
        private static readonly Dictionary<SupportedDatabases, (string Name, int? Port)> DatabaseData = new()
        {
            { SupportedDatabases.SQLite, ("SQLite", null) },
            { SupportedDatabases.SQLServer, ("Microsoft SQL Server", 1433) },
            { SupportedDatabases.MySQL, ("MySQL", 3306) },
            { SupportedDatabases.PostgreSQL, ("PostgreSQL", 5432) }
        };

        /// <summary>
        /// Gets the official name of the database.
        /// </summary>
        /// <param name="database">The database enum value.</param>
        /// <returns>The official name of the database.</returns>
        public static string GetName(this SupportedDatabases database)
        {
            return DatabaseData.TryGetValue(database, out var data) ? data.Name : database.ToString();
        }

        /// <summary>
        /// Gets the default network port for the database.
        /// </summary>
        /// <param name="database">The database enum value.</param>
        /// <returns>The default port number, or null if not applicable (e.g., for file-based databases).</returns>
        public static int? GetDefaultPort(this SupportedDatabases database)
        {
            return DatabaseData.TryGetValue(database, out var data) ? data.Port : null;
        }
    }
}
