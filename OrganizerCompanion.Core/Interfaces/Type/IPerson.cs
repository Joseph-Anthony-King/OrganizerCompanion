using OrganizerCompanion.Core.Enums;

namespace OrganizerCompanion.Core.Interfaces.Type
{
    internal interface IPerson
    {
        string? FirstName { get; set; }
        string? MiddleName { get; set; }
        string? LastName { get; set; }
        string? FullName { get; }
        Pronouns? Pronouns { get; set; }
        DateTime? BirthDate { get; set; }
        DateTime? DeceasedDate { get; set; }
        DateTime? JoinedDate { get; set; }
        List<IEmail> Emails { get; set; }
        List<IPhoneNumber> PhoneNumbers { get; set; }
        List<IAddress> Addresses { get; set; }
    }
}
