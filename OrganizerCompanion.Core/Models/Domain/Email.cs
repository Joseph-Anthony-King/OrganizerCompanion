using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.Domain;

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
        Types? Interfaces.Type.IEmail.Type 
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
        public Email() { }

        [JsonConstructor]
        public Email(
            int id, 
            string? emailAddress, 
            Types? type, 
            DateTime dateCreated, 
            DateTime? dateModified)
        {
            ArgumentOutOfRangeException.ThrowIfNegative(id, nameof(id));

            _id = id;
            _emailAddress = emailAddress;
            _type = type;
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
        public IDomainEntity Cast<T>() => throw new NotImplementedException();

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id{0}.EmailAddress{1}", _id, _emailAddress);
        #endregion
    }
}
