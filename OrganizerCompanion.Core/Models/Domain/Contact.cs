using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class Contact : IContact
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private string? _firstName = null;
        private string? _middleName = null;
        private string? _lastName = null;
        private string? _userName = null;
        private Pronouns? _pronouns = null;
        private DateTime? _birthDate = null;
        private DateTime? _deceasedDate = null;
        private DateTime? _joinedDate = null;
        private List<Email?> _emails = [];
        private List<PhoneNumber?> _phoneNumbers = [];
        private List<IAddress?> _addresses = [];
        private bool? _isActive = null;
        private bool? _isDeceased = null;
        private bool? _isAdmin = null;
        private bool? _isSuperUser = null;
        private int _linkedEntityId = 0;
        private IDomainEntity? _linkedEntity = null;
        private string? _linkedEntityType = null;
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        #region Explicit Interface Properties
        List<Interfaces.Type.IEmail?> Interfaces.Type.IPerson.Emails
        {
            get => [.. _emails];
            set
            {
                _emails = value?.ConvertAll(email => (Email?)email) ?? [];
                DateModified = DateTime.Now;
            }
        }

        List<Interfaces.Type.IPhoneNumber?> Interfaces.Type.IPerson.PhoneNumbers
        {
            get => [.. _phoneNumbers];
            set
            {
                _phoneNumbers = value?.ConvertAll(phone => (PhoneNumber?)phone) ?? [];
                DateModified = DateTime.Now;
            }
        }

        List<Interfaces.Type.IAddress?> Interfaces.Type.IPerson.Addresses
        {
            get => [.. _addresses.Cast<Interfaces.Type.IAddress?>()];
            set
            {
                _addresses = value?.ConvertAll(address => (IAddress?)address) ?? [];
                DateModified = DateTime.Now;
            }
        }
        #endregion

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

        [Required, JsonPropertyName("firstName")]
        public string? FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("middleName")]
        public string? MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("lastName")]
        public string? LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("fullName")]
        public string? FullName => _firstName == null && _middleName == null && _lastName == null ? null :
            _firstName == null || _lastName == null ?
                throw new ArgumentNullException("FirstName and/or LastName properties cannot be null") :
            _middleName == null ?
                    $"{_firstName} {_lastName}" : $"{_firstName} {_middleName} {_lastName}";

        [JsonPropertyName("userName"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("pronouns")]
        public Pronouns? Pronouns
        {
            get => _pronouns;
            set
            {
                _pronouns = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("birthDate")]
        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                DateModified = DateTime.Now;
            }
        }

        [JsonPropertyName("deceasedDate"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DeceasedDate
        {
            get => _deceasedDate;
            set
            {
                _deceasedDate = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("joinDate")]
        public DateTime? JoinedDate
        {
            get => _joinedDate;
            set
            {
                _joinedDate = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("emails")]
        public List<Email?> Emails
        {
            get => _emails.ConvertAll(email => (Email)email!)!;
            set
            {
                _emails = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("phoneNumbers")]
        public List<PhoneNumber?> PhoneNumbers
        {
            get => _phoneNumbers.ConvertAll(phone => (PhoneNumber)phone!)!;
            set
            {
                _phoneNumbers = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("addresses")]
        public List<IAddress?> Addresses
        {
            get => _addresses;
            set
            {
                _addresses = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("isActive")]
        public bool? IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("isDeceased")]
        public bool? IsDeceased
        {
            get => _isDeceased;
            set
            {
                _isDeceased = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("isAdmin")]
        public bool? IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                DateModified = DateTime.Now;
            }
        }

        [JsonPropertyName("isSuperUser"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsSuperUser
        {
            get => _isSuperUser;
            set
            {
                _isSuperUser = value;
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
        public Contact() { }

        [JsonConstructor]
        public Contact(
            int id,
            string? firstName,
            string? middleName,
            string? lastName,
            string? userName,
            Pronouns? pronouns,
            DateTime? birthDate,
            DateTime? deceasedDate,
            DateTime? joinedDate,
            List<Email?> emails,
            List<PhoneNumber?> phoneNumbers,
            List<IAddress?> addresses,
            bool? isActive,
            bool? isDeceased,
            bool? isAdmin,
            bool? isSuperUser,
            int linkedEntityId,
            IDomainEntity? linkedEntity,
            string? linkedEntityType,
            DateTime dateCreated,
            DateTime? dateModified,
            bool? isCast = null,
            int? castId = null,
            string? castType = null)
        {
            _id = id;
            _firstName = firstName;
            _middleName = middleName;
            _lastName = lastName;
            _userName = userName;
            _pronouns = pronouns;
            _birthDate = birthDate;
            _deceasedDate = deceasedDate;
            _joinedDate = joinedDate;
            _emails = emails ?? [];
            _phoneNumbers = phoneNumbers ?? [];
            _addresses = addresses ?? [];
            _isActive = isActive;
            _isDeceased = isDeceased;
            _isAdmin = isAdmin;
            _isSuperUser = isSuperUser;
            _linkedEntityId = linkedEntityId;
            _linkedEntity = linkedEntity;
            _linkedEntityType = linkedEntityType;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id{0}.FullName{1}", _id, FullName);
        #endregion
    }
}
