using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class CAAddress : ICAAddress
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
        private Interfaces.Type.ISubNationalSubdivision? _province = null;
        private string? _zipCode = null;
        private string? _country = Countries.Canada.GetName();
        private Types? _type = null;
        private bool _isCast = false;
        private int _castId = 0;
        private string? _castType = null;
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

        [Required, JsonPropertyName("street1")]
        public string? Street1
        {
            get => _street1;
            set
            {
                _street1 = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("street2")]
        public string? Street2
        {
            get => _street2;
            set
            {
                _street2 = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("city")]
        public string? City
        {
            get => _city;
            set
            {
                _city = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("province")]
        public Interfaces.Type.ISubNationalSubdivision? Province
        {
            get => _province;
            set
            {
                _province = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("zipCode")]
        public string? ZipCode
        {
            get => _zipCode;
            set
            {
                _zipCode = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("country")]
        public string? Country
        {
            get => _country;
            set
            {
                _country = value;
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

        [Required, JsonPropertyName("isCast")]
        public bool IsCast
        {
            get => _isCast;
            set
            {
                _isCast = value;
                DateModified = DateTime.Now;
            }
        }

        [JsonPropertyName("castId"), Range(0, int.MaxValue, ErrorMessage = "Converted ID must be a non-negative number"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public int CastId
        {
            get => _castId;
            set
            {
                _castId = value;
                DateModified = DateTime.Now;
            }
        }

        [JsonPropertyName("castType"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? CastType
        {
            get => _castType;
            set
            {
                _castType = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get => _dateCreated; }

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public CAAddress() { }

        [JsonConstructor]
        public CAAddress(
            int id, 
            string street1, 
            string street2, 
            string city, 
            Interfaces.Type.ISubNationalSubdivision province, 
            string zipCode, 
            string country, 
            Types type,
            DateTime dateCreated, 
            DateTime? dateModified,
            bool? isCast = null,
            int? castId = null,
            string? castType = null)
        {
            _id = id;
            _street1 = street1;
            _street2 = street2;
            _city = city;
            _province = province;
            _zipCode = zipCode;
            _country = country;
            _type = type;
            _isCast = isCast != null && (bool)isCast;
            _castId = castId != null ? (int)castId : 0;
            _castType = castType;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();

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
