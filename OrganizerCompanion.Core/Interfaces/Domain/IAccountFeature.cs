namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IAccountFeature : IDomainEntity
    {
        int? AccountId { get; }
        IAccount? Account { get; set; }
        int? FeatureId { get;  }
        IFeature? Feature { get; set; }
    }
}
