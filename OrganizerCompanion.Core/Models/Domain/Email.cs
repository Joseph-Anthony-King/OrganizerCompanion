using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class Email : IEmail
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
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Id), "Id must be a non-negative number.");
                _id = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("emailAddress")]
        public string? EmailAddress
        {
            get => _emailAddress;
            set
            {
                _emailAddress = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("type")]
        public Types? Type
        {
            get => _type;
            set
            {
                _type = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("isPrimary")]
        public bool IsPrimary
        {
            get => _isPrimary;
            set
            {
                _isPrimary = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("linkedEntity")]
        public IDomainEntity? LinkedEntity
        {
            get => _linkedEntity;
            set
            {
                _linkedEntity = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("linkedEntityId"), Range(0, int.MaxValue, ErrorMessage = "Linked Entity Id must be a non-negative number.")]
        public int? LinkedEntityId
        {
            get => _linkedEntity?.Id ?? null;
        }

        [Required, JsonPropertyName("linkedEntityType")]
        public string? LinkedEntityType => _linkedEntity?.GetType().Name;

        [Required, JsonPropertyName("isConfirmed")]
        public bool IsConfirmed
        {
            get => _isConfirmed;
            set
            {
                _isConfirmed = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated => _dateCreated;

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = default(DateTime);
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
            DateTime dateCreated,
            DateTime? dateModified)
        {
            _id = id;
            _emailAddress = emailAddress;
            _type = type;
            _isPrimary = isPrimary;
            _linkedEntity = linkedEntity;
            _isConfirmed = isConfirmed;
            _dateCreated = dateCreated;
            DateModified = dateModified;
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
                        Id = this.Id,
                        EmailAddress = this.EmailAddress,
                        Type = this.Type,
                        IsPrimary = this.IsPrimary,
                        DateCreated = this.DateCreated,
                        DateModified = this.DateModified,
                    };
                    return (T)dto;
                }
                else throw new InvalidCastException($"Cannot cast Email to type {typeof(T).Name}.");
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
