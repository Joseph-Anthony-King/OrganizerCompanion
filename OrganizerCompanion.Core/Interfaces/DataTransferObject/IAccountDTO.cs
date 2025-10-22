using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IAccountDTO : IDomainEntity
    {
        string? AccountName { get; set; }
        string? AccountNumber { get; set; }
        string? License { get; set; }
        List<IFeatureDTO> Features { get; set; }
        List<ISubAccountDTO>? Accounts { get; set; }
    }
}
