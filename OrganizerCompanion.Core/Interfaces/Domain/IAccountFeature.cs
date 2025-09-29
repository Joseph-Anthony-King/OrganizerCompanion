namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IAccountFeature : IDomainEntity
    {
        int AccountId { get; set; }
        int FeatureId { get; set; }
    }
}
