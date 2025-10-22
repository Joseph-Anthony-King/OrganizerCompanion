namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IAccount : IDomainEntity
    {
        string? AccountName { get; set; }
        string? AccountNumber { get; set; }
        string? License { get; set; }
        List<IAccountFeature> Features { get; set; }
        List<ISubAccount>? Accounts { get; set; }
    }
}
