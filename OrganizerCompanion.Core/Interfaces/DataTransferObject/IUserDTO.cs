using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IUserDTO : IDomainEntity, Type.IPerson
    {
        string? UserName { get; set; }
        bool? IsActive { get; set; }
        bool? IsDeceased { get; set; }
        bool? IsAdmin { get; set; }
        bool? IsSuperUser { get; set; }
        new public List<IEmailDTO> Emails { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        new public List<IPhoneNumberDTO> PhoneNumbers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        new public List<IAddressDTO> Addresses { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
