using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class PhoneNumber : IPhoneNumber
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };
        
        private int _id = 0;
        private string? _phone = null;
        private Types? _type = null;
        private Countries? _country = null;
        private IDomainEntity? _linkedEntity = null;
        private readonly DateTime _createdDate = DateTime.UtcNow;

        private int? _contactId;
        private Contact? _contact;
        private int? _organizationId;
        private Organization? _organization;
        #endregion

        #region Properties
        [Key]
        [Column("PhoneNumberId")]
        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id 
        { 
            get => _id; 
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(Id), "Id must be a non-negative number.");
                }

                _id = value; 
                ModifiedDate = DateTime.UtcNow; 
            } 
        }

        [Required, JsonPropertyName("phone")]
        public string? Phone 
        { 
            get => _phone; 
            set 
            { 
                _phone = value; 
                ModifiedDate = DateTime.UtcNow; 
            } 
        }

        [Required, JsonPropertyName("type")]
        public Types? Type 
        { 
            get => _type; 
            set 
            { 
                _type = value; 
                ModifiedDate = DateTime.UtcNow; 
            }
        }

        [Required, JsonPropertyName("country")]
        public Countries? Country
        {
            get => _country;
            set
            {
                _country = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("linkedEntityId"), Range(0, int.MaxValue, ErrorMessage = "Linked Entity Id must be a non-negative number.")]
        public int? LinkedEntityId => _linkedEntity?.Id;

        [NotMapped]
        [Required, JsonPropertyName("linkedEntity")]
        public IDomainEntity? LinkedEntity
        {
            get
            {
                if (_linkedEntity == null)
                {
                    if (_contact != null)
                    {
                        _linkedEntity = _contact;
                    }
                    else if (_organization != null)
                    {
                        _linkedEntity = _organization;
                    }
                }
                return _linkedEntity;
            }
            set
            {
                _linkedEntity = value;
                if (value is Contact contact)
                {
                    _contact = contact;
                    _contactId = contact.Id;
                }
                else if (value is Organization organization)
                {
                    _organization = organization;
                    _organizationId = organization.Id;
                }
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("linkedEntityType")]
        public string? LinkedEntityType => _linkedEntity?.GetType().Name;

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate => _createdDate;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = null;

        [JsonIgnore]
        public int? ContactId { get => _contactId; set => _contactId = value; }
        [NotMapped]
        internal Contact? Contact { get => _contact; set => _contact = value; }
        [JsonIgnore]
        public int? OrganizationId { get => _organizationId; set => _organizationId = value; }
        [NotMapped]
        internal Organization? Organization { get => _organization; set => _organization = value; }
        #endregion

        #region Constructors
        public PhoneNumber() { }

        [JsonConstructor]
        public PhoneNumber(
            int id,
            string? phone,
            Types? type,
            Countries? country,
            IDomainEntity? linkedEntity,
            DateTime createdDate,
            DateTime? modifiedDate)
        {
            _id = id;
            _phone = phone;
            _type = type;
            _country = country;
            _linkedEntity = linkedEntity;
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public PhoneNumber(IPhoneNumberDTO dto, IDomainEntity? linkedEntity = null)
        {
            _id = dto.Id;
            _phone = dto.Phone;
            _type = dto.Type;
            _country = dto.Country;
            _linkedEntity = linkedEntity;
            _createdDate = dto.CreatedDate;
            ModifiedDate = dto.ModifiedDate;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            try
            {
                if (typeof(T) == typeof(PhoneNumberDTO) || typeof(T) == typeof(IPhoneNumberDTO))
                {
                    object dto = new PhoneNumberDTO
                    {
                        Id = Id,
                        Phone = Phone,
                        Type = Type,
                        Country = Country,
                        CreatedDate = CreatedDate,
                        ModifiedDate = ModifiedDate
                    };
                    return (T)dto;
                }
                else
                {
                    throw new InvalidCastException($"Cannot cast PhoneNumber to type {typeof(T).Name}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id{0}.Phone{1}", _id, _phone);
        #endregion
    }
}
