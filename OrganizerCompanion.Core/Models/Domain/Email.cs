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
    public class Email : IEmail
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private string? _emailAddress = null;
        private Types? _type = null;
        private bool _isPrimary = false;
        private IDomainEntity? _linkedEntity = null;
        private bool _isConfirmed = false;
        private DateTime _createdDate = DateTime.UtcNow;

        private int? _contactId;
        private Contact? _contact;
        private int? _organizationId;
        private Organization? _organization;
        #endregion

        #region Properties
        [Key]
        [Column("EmailId")]
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

        [Required, JsonPropertyName("emailAddress")]
        public string? EmailAddress
        {
            get => _emailAddress;
            set
            {
                _emailAddress = value;
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

        [Required, JsonPropertyName("isPrimary")]
        public bool IsPrimary
        {
            get => _isPrimary;
            set
            {
                _isPrimary = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("linkedEntity")]
        public IDomainEntity? LinkedEntity
        {
            get => _linkedEntity;
            set
            {
                _linkedEntity = value;

                // Assign to appropriate backing field based on entity type
                if (value is Contact contact)
                {
                    _contact = contact;
                    _contactId = contact.Id;
                    _organization = null;
                    _organizationId = null;
                }
                else if (value is Organization organization)
                {
                    _organization = organization;
                    _organizationId = organization.Id;
                    _contact = null;
                    _contactId = null;
                }
                else if (value == null)
                {
                    _contact = null;
                    _contactId = null;
                    _organization = null;
                    _organizationId = null;
                }

                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("linkedEntityId"), Range(0, int.MaxValue, ErrorMessage = "Linked Entity Id must be a non-negative number.")]
        public int? LinkedEntityId
        {
            get => _linkedEntity?.Id ?? null;
        }

        [NotMapped]
        [Required, JsonPropertyName("linkedEntityType")]
        public string? LinkedEntityType => _linkedEntity?.GetType().Name;

        [Required, JsonPropertyName("isConfirmed")]
        public bool IsConfirmed
        {
            get => _isConfirmed;
            set
            {
                _isConfirmed = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

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
        public Email() { }

        [JsonConstructor]
        public Email(
            int id,
            string? emailAddress,
            Types? type,
            bool isPrimary,
            IDomainEntity? linkedEntity,
            bool isConfirmed,
            DateTime createdDate,
            DateTime? modifiedDate)
        {
            _id = id;
            _emailAddress = emailAddress;
            _type = type;
            _isPrimary = isPrimary;
            _linkedEntity = linkedEntity;
            _isConfirmed = isConfirmed;
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public Email(
            string? emailAddress,
            Types? type,
            bool isPrimary,
            IDomainEntity? linkedEntity)
        {
            _emailAddress = emailAddress;
            _type = type;
            _isPrimary = isPrimary;
            _linkedEntity = linkedEntity;
        }

        public Email(IEmailDTO dto, IDomainEntity? linkedEntity = null)
        {
            _id = dto.Id;
            _emailAddress = dto.EmailAddress;
            _type = dto.Type;
            _isPrimary = dto.IsPrimary;
            _linkedEntity = linkedEntity;
            _isConfirmed = dto.IsConfirmed;
            _createdDate = dto.CreatedDate;
            ModifiedDate = dto.ModifiedDate;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            try
            {
                if (typeof(T) == typeof(EmailDTO) || typeof(T) == typeof(IEmailDTO))
                {
                    object dto = new EmailDTO
                    {
                        Id = Id,
                        EmailAddress = EmailAddress,
                        Type = Type,
                        IsPrimary = IsPrimary,
                        CreatedDate = CreatedDate,
                        ModifiedDate = ModifiedDate,
                    };
                    return (T)dto;
                }
                else
                {
                    throw new InvalidCastException($"Cannot cast Email to type {typeof(T).Name}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.EmailAddress:{1}.IsPrimary:{2}", _id, _emailAddress, _isPrimary);
        #endregion
    }
}
