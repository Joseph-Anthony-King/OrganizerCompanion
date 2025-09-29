namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IPerson : IDomainEntity, Type.IPerson
    {
        string? UserName { get; set; }
        bool? IsActive { get; set; }
        bool? IsDeceased { get; set; }
        bool? IsAdmin { get; set; }
        bool? IsSuperUser { get; set; }
    }
}
