using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IOrganizationDTO : IDomainEntity
    {
        string? OrganizationName { get; set; }
        List<IEmailDTO> Emails { get; set; }
        List<IPhoneNumberDTO> PhoneNumbers { get; set; }
        List<IAddressDTO> Addresses { get; set; }
        List<IContactDTO> Members { get; set; }
        List<IContactDTO> Contacts { get; set; }
        List<IAccountDTO> Accounts { get; set; }
    }
}
