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
        private int _linkedEntityId = 0;
        private IDomainEntity? _linkedEntity = null;
        private string? _linkedEntityType = null;
        private bool _isConfirmed = false;
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        [Required, JsonPropertyName("id"), Range(1, int.MaxValue, ErrorMessage = "ID must be a positive number")]
        public int Id
        {
            get => _id;
            set
            {
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

        [Required, JsonPropertyName("linkedEntityId"), Range(0, int.MaxValue, ErrorMessage = "Linked Entity ID must be a non-negative number")]
        public int LinkedEntityId
        {
            get => _linkedEntityId;
            set
            {
                _linkedEntityId = value;
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
                _linkedEntityType = value?.GetType().Name;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("linkedEntityType")]
        public string? LinkedEntityType => _linkedEntityType;

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

        [JsonIgnore]
        public bool IsCast { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public int CastId { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [JsonIgnore]
        public string? CastType { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get => _dateCreated; }

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
            int linkedEntityId,
            IDomainEntity? linkedEntity,
            string? linkedEntityType,
            bool isConfirmed,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            _id = id;
            _emailAddress = emailAddress;
            _type = type;
            _linkedEntityId = linkedEntityId;
            _linkedEntity = linkedEntity;
            _linkedEntityType = linkedEntityType;
            _isConfirmed = isConfirmed;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }

        public Email(
            string? emailAddress,
            Types? type)
        {
            _emailAddress = emailAddress;
            _type = type;
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
                        DateCreated = this.DateCreated,
                        DateModified = this.DateModified,
                    };
                    return (T)dto;
                }
                else throw new InvalidCastException($"Cannot cast Email to type {typeof(T).Name}, casting is not supported for this type");
            }
            catch (Exception ex)
            {
                throw new InvalidCastException($"Error casting Email to type {typeof(T).Name}: {ex.Message}", ex);
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id{0}.EmailAddress{1}", _id, _emailAddress);
        #endregion
    }
}
