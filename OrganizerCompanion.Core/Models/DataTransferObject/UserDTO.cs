using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class UserDTO : IUserDTO
    {
        #region Properties
        #region Explicit Interface Implementations
        [JsonIgnore]
        List<IEmailDTO> IUserDTO.Emails
        {
            get => Emails!.ConvertAll(email => email.Cast<IEmailDTO>());
            set => Emails = value.ConvertAll(email => (EmailDTO)email);
        }

        [JsonIgnore]
        List<IPhoneNumberDTO> IUserDTO.PhoneNumbers
        {
            get => PhoneNumbers!.ConvertAll(phoneNumber => phoneNumber.Cast<IPhoneNumberDTO>());
            set => PhoneNumbers = value.ConvertAll(phoneNumber => (PhoneNumberDTO)phoneNumber);
        }

        public T Cast<T>() where T : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public string ToJson()
        {
            throw new NotImplementedException();
        }
        #endregion

        [Required, JsonPropertyName("id")]
        public int Id { get; set; } = 0;

        [Required, JsonPropertyName("firstName")]
        public string? FirstName { get; set; } = null;

        [JsonPropertyName("middleName")]
        public string? MiddleName { get; set; } = null;

        [Required, JsonPropertyName("lastName")]
        public string? LastName { get; set; } = null;

        [Required, JsonPropertyName("fullName")]
        public string? FullName { get; set; } = null;

        [Required, JsonPropertyName("userName")]
        public string? UserName { get; set; } = null;

        [Required, JsonPropertyName("emails")]
        public List<EmailDTO> Emails { get; set; } = [];

        [Required, JsonPropertyName("phoneNumbers")]
        public List<PhoneNumberDTO> PhoneNumbers { get; set; } = [];

        [Required, JsonPropertyName("addresses")]
        public List<IAddressDTO> Addresses { get; set; } = [];

        [Required, JsonPropertyName("pronouns")]
        public Pronouns? Pronouns { get; set; } = null;

        [Required, JsonPropertyName("birthDate")]
        public DateTime? BirthDate { get; set; } = null;

        [JsonPropertyName("deceasedDate"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DeceasedDate { get; set; } = null;

        [Required, JsonPropertyName("joinedDate")]
        public DateTime? JoinedDate { get; set; } = null;

        [Required, JsonPropertyName("isActive")]
        public bool? IsActive { get; set; } = null;

        [Required, JsonPropertyName("isDeceased")]
        public bool? IsDeceased { get; set; } = null;

        [Required, JsonPropertyName("isAdmin")]
        public bool? IsAdmin { get; set; } = null;

        [JsonPropertyName("superUser"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsSuperUser { get; set; } = null;

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate { get; set; } = DateTime.Now;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = default;
        #endregion
    }
}
