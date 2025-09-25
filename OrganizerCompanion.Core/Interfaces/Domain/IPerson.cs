namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IPerson : IDomainEntity, Type.IPerson
    {
        bool? IsActive { get; set; }
        bool? IsDeceased { get; set; }
        bool? IsAdmin { get; set; }
        bool? IsSuperUser { get; set; }
    }
}
