using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Validation.Attributes;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class ContactDTO : IContactDTO
    {
        #region Explicit Interface Implementations
        [JsonIgnore]
        List<Interfaces.Type.IEmail> Interfaces.Type.IPerson.Emails { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        List<Interfaces.Type.IPhoneNumber> Interfaces.Type.IPerson.PhoneNumbers { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        List<Interfaces.Type.IAddress> Interfaces.Type.IPerson.Addresses { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public bool IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public int CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        public string? CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public T Cast<T>() where T : IDomainEntity
        {
            throw new NotImplementedException();
        }
        public string ToJson()
        {
            throw new NotImplementedException();
        }
        #endregion

        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id { get; set; } = 0;
        [Required, JsonPropertyName("firstName")]
        public string? FirstName { get; set; } = null;
        [Required, JsonPropertyName("middleName")]
        public string? MiddleName { get; set; } = null;
        [Required, JsonPropertyName("lastName")]
        public string? LastName { get; set; } = null;
        [Required, JsonPropertyName("fullName")]
        public string? FullName { get; set; } = null;
        [Required, JsonPropertyName("pronouns")]
        public Pronouns? Pronouns { get; set; } = null;
        [Required, JsonPropertyName("birthDate")]
        public DateTime? BirthDate { get; set; } = null;
        [JsonPropertyName("deceasedDate"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DeceasedDate { get; set; } = null;
        [Required, JsonPropertyName("userName")]
        public string? UserName { get; set; } = null;
        [Required, JsonPropertyName("isActive")]
        public bool? IsActive { get; set; } = null;
        [JsonPropertyName("isDeceased"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsDeceased { get; set; } = null;
        [Required, JsonPropertyName("isAdmin")]
        public bool? IsAdmin { get; set; } = null;
        [JsonPropertyName("isSuperUser"), JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsSuperUser { get; set; } = null;
        [Required, JsonPropertyName("joinedDate")]
        public DateTime? JoinedDate { get; set; } = null;
        [Required, JsonPropertyName("emails"), EmailsValidator]
        public List<EmailDTO> Emails { get; set; } = [];
        [Required, JsonPropertyName("phoneNumbers"), PhoneNumbersValidator]
        public List<PhoneNumberDTO> PhoneNumbers { get; set; } = [];
        [Required, JsonPropertyName("addresses")]
        public List<IAddressDTO> Addresses { get; set; } = [];
        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = null;
    }
}
