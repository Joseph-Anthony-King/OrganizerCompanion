using OrganizerCompanion.Core.Enums;

namespace OrganizerCompanion.Core.Interfaces.Type
{
    internal interface IDatabaseConnection
    {
        string? ConnectionString { get; set; }
        SupportedDatabases? DatabaseType { get; set; }
    }
}
