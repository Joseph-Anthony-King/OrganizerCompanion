using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
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
    internal class CAAddress : Interfaces.Domain.ICAAddress
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
        private INationalSubdivision? _province = null;
        private string? _zipCode = null;
        private string? _country = Countries.Canada.GetName();
        private Types? _type = null;
        private bool _isPrimary = false;
        private IDomainEntity? _linkedEntity = null;
        private DateTime _createdDate = DateTime.UtcNow;
        #endregion

        #region Properties
        [Key]
        [Column("CAAddressId")]
        [Required, JsonPropertyName("id")]
        [Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(
                        nameof(Id),
                        "Id must be a non-negative number.");
                }

                _id = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("street1")]
        public string? Street1
        {
            get => _street1;
            set
            {
                _street1 = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("street2")]
        public string? Street2
        {
            get => _street2;
            set
            {
                _street2 = value;
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

        [Required, JsonPropertyName("province")]
        public Interfaces.Type.INationalSubdivision? Province
        {
            get => _province;
            set
            {
                _province = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [JsonIgnore]
        public CAProvinces? ProvinceEnum
        {
            get => null; // Cannot reverse-lookup from IState to enum
            set
            {
                _province = value?.ToStateModel();
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("zipCode")]
        public string? ZipCode
        {
            get => _zipCode;
            set
            {
                _zipCode = value;
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
        [Required]
        [JsonPropertyName("linkedEntityId")]
        [Range(0, int.MaxValue, ErrorMessage = "Linked Entity Id must be a non-negative number.")]
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
        public CAAddress() { }

        [JsonConstructor]
        public CAAddress(
            int id,
            string street1,
            string street2,
            string city,
            Interfaces.Type.INationalSubdivision province,
            string zipCode,
            string country,
            Types type,
            bool isPrimary,
            IDomainEntity? linkedEntity,
            DateTime createdDate,
            DateTime? modifiedDate)
        {
            _id = id;
            _street1 = street1;
            _street2 = street2;
            _city = city;
            _province = province;
            _zipCode = zipCode;
            _country = country;
            _type = type;
            _isPrimary = isPrimary;
            _linkedEntity = linkedEntity;
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public CAAddress(
            string street1,
            string street2,
            string city,
            Interfaces.Type.INationalSubdivision province,
            string zipCode,
            string country,
            Types type,
            bool isPrimary,
            IDomainEntity? linkedEntity)
        {
            _street1 = street1;
            _street2 = street2;
            _city = city;
            _province = province;
            _zipCode = zipCode;
            _country = country;
            _type = type;
            _isPrimary = isPrimary;
            _linkedEntity = linkedEntity;
        }

        public CAAddress(ICAAddressDTO dto, IDomainEntity? linkedEntity = null)
        {
            _id = dto.Id;
            _street1 = dto.Street1;
            _street2 = dto.Street2;
            _city = dto.City;
            _province = dto.Province;
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
                if (typeof(T) == typeof(CAAddressDTO) || typeof(T) == typeof(ICAAddressDTO) || typeof(T) == typeof(IAddressDTO))
                {
                    object dto = new CAAddressDTO
                    {
                        Id = Id,
                        Street1 = Street1,
                        Street2 = Street2,
                        City = City,
                        Province = Province,
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
                    throw new InvalidCastException($"Cannot cast CAAddress to type {typeof(T).Name}.");
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
            var provinceDisplay = _province?.Abbreviation ?? _province?.Name ?? "Unknown";
            return string.Format(base.ToString() + ".Id:{0}.Street1:{1}.City:{2}.Province:{3}.Zip:{4}",
                _id, _street1, _city, provinceDisplay, _zipCode);
        }
        #endregion
    }
}
