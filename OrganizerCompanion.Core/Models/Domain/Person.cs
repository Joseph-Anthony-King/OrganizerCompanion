using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class Person : IPerson
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
        private DateTime? _joinDate = null;
        private List<IEmail?> _emails = [];
        private List<IPhoneNumber?> _phoneNumbers = [];
        private List<IAddress?> _addresses = [];
        private bool? _isActive = null;
        private bool? _isDeceased = null;
        private bool? _isAdmin = null;
        private bool? _isSuperUser = null;
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        #region Explicit Interface Properties
        List<Interfaces.Type.IEmail?> Interfaces.Type.IPerson.Emails 
        { 
            get => [.. _emails.Cast<Interfaces.Type.IEmail?>()]; 
            set 
            { 
                _emails = value?.OfType<IEmail>().Cast<IEmail?>().ToList() ?? [];
                DateModified = DateTime.Now;
            }
        }

        List<Interfaces.Type.IPhoneNumber?> Interfaces.Type.IPerson.PhoneNumbers 
        { 
            get => [.. _phoneNumbers.Cast<Interfaces.Type.IPhoneNumber?>()]; 
            set 
            { 
                _phoneNumbers = value?.OfType<IPhoneNumber>().Cast<IPhoneNumber?>().ToList() ?? [];
                DateModified = DateTime.Now;
            }
        }

        List<Interfaces.Type.IAddress?> Interfaces.Type.IPerson.Addresses 
        { 
            get => [.. _addresses.Cast<Interfaces.Type.IAddress?>()]; 
            set 
            { 
                _addresses = value?.OfType<IAddress>().Cast<IAddress?>().ToList() ?? [];
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
        public DateTime? JoinDate 
        { 
            get => _joinDate; 
            set 
            { 
                _joinDate = value; 
                DateModified = DateTime.Now; 
            } 
        }

        [Required, JsonPropertyName("emails")]
        public List<IEmail?> Emails 
        { 
            get => _emails; 
            set 
            { 
                _emails = value; 
                DateModified = DateTime.Now; 
            } 
        }

        [Required, JsonPropertyName("phoneNumbers")]
        public List<IPhoneNumber?> PhoneNumbers 
        { 
            get => _phoneNumbers; 
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

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get => _dateCreated; }

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public Person() { }

        [JsonConstructor]
        public Person(
            int id,
            string? firstName,
            string? middleName,
            string? lastName,
            Pronouns? pronouns,
            DateTime? birthDate,
            DateTime? deceasedDate,
            DateTime? joinDate,
            List<IEmail?> emails,
            List<IPhoneNumber?> phoneNumbers,
            List<IAddress?> addresses,
            bool? isActive,
            bool? isDeceased,
            bool? isAdmin,
            bool? isSuperUser,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            _id = id;
            _firstName = firstName;
            _middleName = middleName;
            _lastName = lastName;
            _pronouns = pronouns;
            _birthDate = birthDate;
            _deceasedDate = deceasedDate;
            _joinDate = joinDate;
            _emails = emails ?? [];
            _phoneNumbers = phoneNumbers ?? [];
            _addresses = addresses ?? [];
            _isActive = isActive;
            _isDeceased = isDeceased;
            _isAdmin = isAdmin;
            _isSuperUser = isSuperUser;
            _dateCreated = dateCreated;
            DateModified = dateModified;
        }

        public Person(
            string? firstName,
            string? middleName,
            string? lastName,
            Pronouns? pronouns,
            DateTime? birthDate,
            DateTime? joinDate,
            List<IEmail?> emails,
            List<IPhoneNumber?> phoneNumbers,
            List<IAddress?> addresses,
            bool? isActive,
            bool? isDeceased,
            bool? isAdmin,
            DateTime dateCreated)
        {
            _firstName = firstName;
            _middleName = middleName;
            _lastName = lastName;
            _pronouns = pronouns;
            _birthDate = birthDate;
            _joinDate = joinDate;
            _emails = emails ?? [];
            _phoneNumbers = phoneNumbers ?? [];
            _addresses = addresses ?? [];
            _isActive = isActive;
            _isDeceased = isDeceased;
            _isAdmin = isAdmin;
            _dateCreated = dateCreated;
        }
        #endregion

        #region Methods
        public IDomainEntity Cast<T>() => throw new NotImplementedException();

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id{0}.FullName{1}", _id, FullName);
        #endregion
    }
}
