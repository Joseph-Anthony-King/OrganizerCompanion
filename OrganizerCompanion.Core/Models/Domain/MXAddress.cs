using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Interfaces.Type;
using IMXAddress = OrganizerCompanion.Core.Interfaces.Domain.IMXAddress;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class MXAddress : IMXAddress
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

        [Required, JsonPropertyName("street")]
        public string? Street
        {
            get => _street;
            set
            {
                _street = value;
                DateModified = DateTime.Now;
            }

        }

        [Required, JsonPropertyName("neighborhood")]
        public string? Neighborhood
        {
            get => _neighborhood;
            set
            {
                _neighborhood = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("postalCode")]
        public string? PostalCode
        {
            get => _postalCode;
            set
            {
                _postalCode = value;
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

        [Required, JsonPropertyName("state")]
        public Interfaces.Type.INationalSubdivision? State
        {
            get => _state;
            set
            {
                _state = value;
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
        public MXAddress () { }

        [JsonConstructor]
        public MXAddress (
            int id, 
            string street, 
            string neighborhood, 
            string postalCode, 
            string city, 
            INationalSubdivision state, 
            string country, 
            Types type,
            DateTime dateCreated, 
            DateTime? dateModified,
            bool? isCast = null,
            int? castId = null,
            string? castType = null)
        {
            _id = id;
            _street = street;
            _neighborhood = neighborhood;
            _postalCode = postalCode;
            _city = city;
            _state = state;
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
            var stateDisplay = _state?.Abbreviation ?? _state?.Name ?? "Unknown";
            return string.Format(base.ToString() + ".Id:{0}.Street1:{1}.City:{2}.State:{3}.Zip:{4}",
                _id, _street, _city, stateDisplay, _postalCode);
        }
        #endregion
    }
}
