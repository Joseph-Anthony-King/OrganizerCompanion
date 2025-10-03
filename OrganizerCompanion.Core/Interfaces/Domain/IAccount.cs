using OrganizerCompanion.Core.Enums;

namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IAccount : IDomainEntity
    {
        string? AccountName { get; set; }
        string? AccountNumber { get; set; }
        string? License { get; set; }
        string? DatabaseConnection { get; set; }
        SupportedDatabases? DatabaseType { get; set; }
        int LinkedEntityId { get; set; }
        string? LinkedEntityType { get; }
        IDomainEntity? LinkedEntity { get; set; }
        List<IAccountFeature> Features { get; set; }
    }
}
