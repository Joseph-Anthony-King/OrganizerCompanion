using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class USAddress : IUSAddress
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
        private Interfaces.Type.ISubNationalSubdivision? _state = null;
        private string? _zipCode = null;
        private string? _country = Countries.UnitedStates.GetName();
        private Types? _type = null;
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

        [Required, JsonPropertyName("state")]
        public Interfaces.Type.ISubNationalSubdivision? State 
        { 
            get => _state; 
            set 
            { 
                _state = value; 
                DateModified = DateTime.Now; 
            } 
        }

        [JsonIgnore]
        public USStates? StateEnum
        {
            get => null; // Cannot reverse-lookup from IState to enum
            set 
            { 
                _state = value?.ToStateModel();
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

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get => _dateCreated; }

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public USAddress() { }

        [JsonConstructor]
        public USAddress(
            int id, 
            string? street1, 
            string? street2, 
            string? city, 
            Interfaces.Type.ISubNationalSubdivision? state, 
            string? zipCode, 
            string? country, 
            Types? type, 
            DateTime dateCreated, 
            DateTime? dateModified)
        {
            _id = id;
            _street1 = street1;
            _street2 = street2;
            _city = city;
            _state = state;
            _zipCode = zipCode;
            _country = country;
            _type = type;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }
        #endregion

        #region Methods
        public IDomainEntity Cast<T>() => throw new NotImplementedException();
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
