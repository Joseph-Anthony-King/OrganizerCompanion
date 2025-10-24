using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Interfaces.Type;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class USAddress : Interfaces.Domain.IUSAddress
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private string? _street1 = null;
        private string? _street2 = null;
        private string? _city = null;
        private INationalSubdivision? _state = null;
        private string? _zipCode = null;
        private string? _country = Countries.UnitedStates.GetName();
        private Types? _type = null;
        private bool _isPrimary = false;
        private IDomainEntity? _linkedEntity = null;
        private DateTime _createdDate = DateTime.UtcNow;
        #endregion

        #region Properties
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
                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("street1")]
        public string? Street1
        {
            get => _street1;
            set
            {
                _street1 = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("street2")]
        public string? Street2
        {
            get => _street2;
            set
            {
                _street2 = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("city")]
        public string? City
        {
            get => _city;
            set
            {
                _city = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("state")]
        public Interfaces.Type.INationalSubdivision? State
        {
            get => _state;
            set
            {
                _state = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [JsonIgnore]
        public USStates? StateEnum
        {
            get => null; // Cannot reverse-lookup from IState to enum
            set
            {
                _state = value?.ToStateModel();
                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("zipCode")]
        public string? ZipCode
        {
            get => _zipCode;
            set
            {
                _zipCode = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("country")]
        public string? Country
        {
            get => _country;
            set
            {
                _country = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("type")]
        public Types? Type
        {
            get => _type;
            set
            {
                _type = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("isPrimary")]
        public bool IsPrimary
        {
            get => _isPrimary;
            set
            {
                _isPrimary = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("linkedEntity")]
        public IDomainEntity? LinkedEntity
        {
            get => _linkedEntity;
            set
            {
                _linkedEntity = value;
                ModifiedDate = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("linkedEntityId"), Range(0, int.MaxValue, ErrorMessage = "Linked Entity Id must be a non-negative number.")]
        public int? LinkedEntityId => _linkedEntity?.Id;

        [Required, JsonPropertyName("linkedEntityType")]
        public string? LinkedEntityType => LinkedEntity?.GetType().Name;

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate => _createdDate;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = null;
        #endregion

        #region Constructors
        public USAddress() { }

        [JsonConstructor]
        public USAddress(
            int id,
            string? street1,
            string? street2,
            string? city,
            Interfaces.Type.INationalSubdivision? state,
            string? zipCode,
            string? country,
            Types? type,
            bool isPrimary,
            IDomainEntity? linkedEntity,
            DateTime createdDate,
            DateTime? modifiedDate)
        {
            _id = id;
            _street1 = street1;
            _street2 = street2;
            _city = city;
            _state = state;
            _zipCode = zipCode;
            _country = country;
            _type = type;
            _isPrimary = isPrimary;
            _linkedEntity = linkedEntity;
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public USAddress(IUSAddressDTO dto, IDomainEntity? linkedEntity = null)
        {
            _id = dto.Id;
            _street1 = dto.Street1;
            _street2 = dto.Street2;
            _city = dto.City;
            _state = dto.State;
            _zipCode = dto.ZipCode;
            _country = dto.Country;
            _type = dto.Type;
            _isPrimary = dto.IsPrimary;
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
                if (typeof(T) == typeof(USAddressDTO) || typeof(T) == typeof(IUSAddressDTO) || typeof(T) == typeof(IAddressDTO))
                {
                    object dto = new USAddressDTO
                    {
                        Id = Id,
                        Street1 = Street1,
                        Street2 = Street2,
                        City = City,
                        State = State,
                        ZipCode = ZipCode,
                        Country = Country,
                        Type = Type,
                        IsPrimary = IsPrimary,
                        CreatedDate = CreatedDate,
                        ModifiedDate = ModifiedDate
                    };
                    return (T)dto;
                }
                else
                {
                    throw new InvalidCastException($"Cannot cast USAddress to type {typeof(T).Name}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string ToString()
        {
            var stateDisplay = _state?.Abbreviation ?? _state?.Name ?? "Unknown";
            return string.Format(base.ToString() + ".Id:{0}.Street1:{1}.City:{2}.State:{3}.Zip:{4}",
                _id, _street1, _city, stateDisplay, _zipCode);
        }
        #endregion
    }
}
