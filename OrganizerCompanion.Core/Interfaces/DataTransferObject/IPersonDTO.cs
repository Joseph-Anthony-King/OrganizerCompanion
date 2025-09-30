namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IPersonDTO : Type.IPerson
    {
        new List<IEmailDTO> Emails { get; set; }
        new List<IPhoneNumberDTO> PhoneNumbers { get; set; }
        new List<IAddressDTO> Addresses { get; set; }
        string? UserName { get; set; }
        bool? IsActive { get; set; }
        bool? IsDeceased { get; set; }
        bool? IsAdmin { get; set; }
        bool? IsSuperUser { get; set; }
    }
}
