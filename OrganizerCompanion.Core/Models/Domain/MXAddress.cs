using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Interfaces.Type;
using OrganizerCompanion.Core.Models.DataTransferObject;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class MXAddress : Interfaces.Domain.IMXAddress
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private string? _street = null;
        private string? _neighborhood = null;
        private string? _postalCode = null;
        private string? _city = null;
        private INationalSubdivision? _state = null;
        private string? _country = Countries.Mexico.GetName();
        private Types? _type = null;
        private bool _isPrimary = false;
        private IDomainEntity? _linkedEntity = null;
        private DateTime _createdDate = DateTime.UtcNow;
        #endregion

        #region Properties
        [Key]
        [Column("MXAddressId")]
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

        [Required, JsonPropertyName("street")]
        public string? Street
        {
            get => _street;
            set
            {
                _street = value;
                ModifiedDate = DateTime.UtcNow;
            }

        }

        [Required, JsonPropertyName("neighborhood")]
        public string? Neighborhood
        {
            get => _neighborhood;
            set
            {
                _neighborhood = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("postalCode")]
        public string? PostalCode
        {
            get => _postalCode;
            set
            {
                _postalCode = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("city")]
        public string? City
        {
            get => _city;
            set
            {
                _city = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("state")]
        public Interfaces.Type.INationalSubdivision? State
        {
            get => _state;
            set
            {
                _state = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [JsonIgnore]
        public MXStates? StateEnum
        {
            get => null; // Cannot reverse-lookup from IState to enum
            set
            {
                _state = value?.ToStateModel();
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("country")]
        public string? Country
        {
            get => _country;
            set
            {
                _country = value;
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
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("linkedEntityId"), Range(0, int.MaxValue, ErrorMessage = "Linked Entity Id must be a non-negative number.")]
        public int? LinkedEntityId => _linkedEntity?.Id ?? null;

        [NotMapped]
        [Required, JsonPropertyName("linkedEntityType")]
        public string? LinkedEntityType => _linkedEntity?.GetType().Name;

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate => _createdDate;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = null;


        #endregion

        #region Constructors
        public MXAddress() { }

        [JsonConstructor]
        public MXAddress(
            int id,
            string street,
            string neighborhood,
            string postalCode,
            string city,
            INationalSubdivision state,
            string country,
            Types type,
            bool isPrimary,
            int linkedEntityId,
            string? linkedEntityType,
            IDomainEntity? linkedEntity,
            DateTime createdDate,
            DateTime? modifiedDate)
        {
            _id = id;
            _street = street;
            _neighborhood = neighborhood;
            _postalCode = postalCode;
            _city = city;
            _state = state;
            _country = country;
            _type = type;
            _isPrimary = isPrimary;
            _linkedEntity = linkedEntity;
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public MXAddress(IMXAddressDTO dto, IDomainEntity? linkedEntity = null)
        {
            _id = dto.Id;
            _street = dto.Street;
            _neighborhood = dto.Neighborhood;
            _postalCode = dto.PostalCode;
            _city = dto.City;
            _state = dto.State;
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
                if (typeof(T) == typeof(MXAddressDTO) || typeof(T) == typeof(IMXAddressDTO) || typeof(T) == typeof(IAddressDTO))
                {
                    object dto = new MXAddressDTO
                    {
                        Id = Id,
                        Street = Street,
                        Neighborhood = Neighborhood,
                        PostalCode = PostalCode,
                        City = City,
                        State = State,
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
                    throw new InvalidCastException($"Cannot cast MXAddress to type {typeof(T).Name}.");
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
                _id, _street, _city, stateDisplay, _postalCode);
        }
        #endregion
    }
}
