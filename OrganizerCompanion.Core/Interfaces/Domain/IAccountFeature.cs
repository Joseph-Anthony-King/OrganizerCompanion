namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IAccountFeature : IDomainEntity
    {
        int AccountId { get; set; }
        IAccount? Account { get; set; }
        int FeatureId { get; set; }
        IFeature? Feature { get; set; }
    }
}
