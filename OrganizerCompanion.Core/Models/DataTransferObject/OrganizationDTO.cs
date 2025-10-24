using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class OrganizationDTO : IOrganizationDTO, IDomainEntity
    {
        #region IDomainEntity Implementation
        public T Cast<T>() where T : IDomainEntity
        {
            throw new NotImplementedException("Cast method not implemented for OrganizationDTO.");
        }

        public string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
        #endregion

        #region Explicit Interface Implementations
        [JsonIgnore]
        List<IEmailDTO> IOrganizationDTO.Emails
        {
            get => Emails.ConvertAll(email => (IEmailDTO)email);
            set => Emails = value.ConvertAll(email => (EmailDTO)email);
        }

        [JsonIgnore]
        List<IPhoneNumberDTO> IOrganizationDTO.PhoneNumbers
        {
            get => PhoneNumbers.ConvertAll(phone => (IPhoneNumberDTO)phone);
            set => PhoneNumbers = value.ConvertAll(phone => (PhoneNumberDTO)phone);
        }

        [JsonIgnore]
        List<IContactDTO> IOrganizationDTO.Members
        {
            get => Members.ConvertAll(member => (IContactDTO)member);
            set => Members = value.ConvertAll(member => (ContactDTO)member);
        }

        [JsonIgnore]
        List<IContactDTO> IOrganizationDTO.Contacts
        {
            get => Contacts.ConvertAll(contact => (IContactDTO)contact);
            set => Contacts = value.ConvertAll(contact => (ContactDTO)contact);
        }

        [JsonIgnore]
        List<IAccountDTO> IOrganizationDTO.Accounts
        {
            get => Accounts.ConvertAll(account => (IAccountDTO)account);
            set => Accounts = value.ConvertAll(account => (AccountDTO)account);
        }
        #endregion

        #region Properties
        [Required, JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [Required, JsonPropertyName("organizationName")]
        public string? OrganizationName { get; set; } = null;

        [Required, JsonPropertyName("emails")]
        public List<EmailDTO> Emails { get; set; } = [];

        [Required, JsonPropertyName("phoneNumbers")]
        public List<PhoneNumberDTO> PhoneNumbers { get; set; } = [];

        [Required, JsonPropertyName("addresses")]
        public List<IAddressDTO> Addresses { get; set; } = [];

        [Required, JsonPropertyName("members")]
        public List<ContactDTO> Members { get; set; } = [];

        [Required, JsonPropertyName("contacts")]
        public List<ContactDTO> Contacts { get; set; } = [];

        [Required, JsonPropertyName("accounts")]
        public List<AccountDTO> Accounts { get; set; } = [];

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = default;
        #endregion
    }
}
