using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IUserDTO : IDomainEntity
    {
        string? FirstName { get; set; }
        string? MiddleName { get; set; }
        string? LastName { get; set; }
        string? FullName { get; }
        Pronouns? Pronouns { get; set; }
        DateTime? BirthDate { get; set; }
        DateTime? DeceasedDate { get; set; }
        DateTime? JoinedDate { get; set; }
        string? UserName { get; set; }
        bool? IsActive { get; set; }
        bool? IsDeceased { get; set; }
        bool? IsAdmin { get; set; }
        bool? IsSuperUser { get; set; }
        List<IEmailDTO> Emails { get; set; }
        List<IPhoneNumberDTO> PhoneNumbers { get; set; }
        List<IAddressDTO> Addresses { get; set; }
    }
}
