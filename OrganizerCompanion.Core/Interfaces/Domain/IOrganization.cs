namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IOrganization : IDomainEntity
    {
        string? OrganizationName { get; set; }
        List<IAddress?> Addresses { get; set; }
        List<IPerson?> Members { get; set; }
        List<IPhoneNumber?> PhoneNumbers { get; set; }
    }
}
