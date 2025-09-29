namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IFeature : IDomainEntity
    {
        string? FeatureName { get; set; }
        bool IsEnabled { get; set; }
    }
}
