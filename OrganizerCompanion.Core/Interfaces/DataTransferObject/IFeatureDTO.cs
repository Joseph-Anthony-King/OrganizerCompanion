using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IFeatureDTO : IDomainEntity
    {
        string? FeatureName { get; set; }
        bool IsEnabled { get; set; }
    }
}
