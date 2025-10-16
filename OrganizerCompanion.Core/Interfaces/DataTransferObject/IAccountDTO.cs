using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.Type;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IAccountDTO : IDomainEntity
    {
        string? AccountName { get; set; }
        string? AccountNumber { get; set; }
        string? License { get; set; }
        DatabaseConnection? DatabaseConnection { get; set; }
        List<IFeatureDTO> Features { get; set; }
        int? MainAccountId { get; set; }
        List<IAccountDTO>? Accounts { get; set; }
    }
}
