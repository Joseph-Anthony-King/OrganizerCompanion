using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.DataTransferObject
{
    internal class UserDTO : IUserDTO
    {
        private DateTime _dateCreated = DateTime.Now;

        #region Explicit Interface Implementations
        [JsonIgnore]
        int IDomainEntity.Id { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        bool IDomainEntity.IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        int IDomainEntity.CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        string? IDomainEntity.CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        DateTime IDomainEntity.DateCreated => throw new NotImplementedException();
        [JsonIgnore]
        DateTime? IDomainEntity.DateModified { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        string? Interfaces.Type.IPerson.FirstName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        string? Interfaces.Type.IPerson.MiddleName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        string? Interfaces.Type.IPerson.LastName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        [JsonIgnore]
        string? Interfaces.Type.IPerson.FullName => throw new NotImplementedException();
        Pronouns? Interfaces.Type.IPerson.Pronouns { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DateTime? Interfaces.Type.IPerson.BirthDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DateTime? Interfaces.Type.IPerson.DeceasedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        DateTime? Interfaces.Type.IPerson.JoinedDate { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
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
        string IDomainEntity.ToJson()
        {
            throw new NotImplementedException();
        }
        T IDomainEntity.Cast<T>()
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
        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = null;
        [Required, JsonPropertyName("dateCreated")]
        public DateTime CreatedAt
        {
            get => _dateCreated; 
            set => _dateCreated = value;
        }
    }
}
