namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IOrganization : IDomainEntity
    {
        string? OrganizationName { get; set; }
        List<IEmail> Emails { get; set; }
        List<IPhoneNumber> PhoneNumbers { get; set; }
        List<IAddress> Addresses { get; set; }
        List<IPerson> Members { get; set; }
        List<IPerson> Contacts { get; set; }
        List<IAccount> Accounts { get; set; }
    }
}
