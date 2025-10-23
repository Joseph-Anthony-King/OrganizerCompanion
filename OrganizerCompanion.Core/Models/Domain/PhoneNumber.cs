using System.ComponentModel.DataAnnotations;
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

        [Required, JsonPropertyName("phone")]
        public string? Phone 
        { 
            get => _phone; 
            set 
            { 
                _phone = value; 
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

        [Required, JsonPropertyName("country")]
        public Countries? Country
        {
            get => _country;
            set
            {
                _country = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("linkedEntityId"), Range(0, int.MaxValue, ErrorMessage = "Linked Entity Id must be a non-negative number.")]
        public int? LinkedEntityId => _linkedEntity?.Id;

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

        [Required, JsonPropertyName("linkedEntityType")]
        public string? LinkedEntityType => _linkedEntity?.GetType().Name;

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated => _dateCreated;

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = default(DateTime);
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
            DateTime dateCreated,
            DateTime? dateModified)
        {
            _id = id;
            _phone = phone;
            _type = type;
            _country = country;
            _linkedEntity = linkedEntity;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }

        public PhoneNumber(IPhoneNumberDTO dto, IDomainEntity? linkedEntity = null)
        {
            _id = dto.Id;
            _phone = dto.Phone;
            _type = dto.Type;
            _country = dto.Country;
            _linkedEntity = linkedEntity;
            _dateCreated = dto.DateCreated;
            DateModified = dto.DateModified;
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
                        DateCreated = DateCreated,
                        DateModified = DateModified
                    };
                    return (T)dto;
                }
                else throw new InvalidCastException($"Cannot cast PhoneNumber to type {typeof(T).Name}.");
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
