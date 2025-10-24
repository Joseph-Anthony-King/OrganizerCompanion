using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Validation.Attributes;

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
        private List<Email> _emails = [];
        private List<PhoneNumber> _phoneNumbers = [];
        private List<IAddress> _addresses = [];
        private bool? _isActive = null;
        private bool? _isDeceased = null;
        private bool? _isAdmin = null;
        private bool? _isSuperUser = null;
        private int _linkedEntityId = 0;
        private IDomainEntity? _linkedEntity = null;
        private string? _linkedEntityType = null;
        private DateTime _createdDate = DateTime.UtcNow;
        #endregion

        #region Properties
        #region Explicit Interface Properties
        [JsonIgnore]
        List<Interfaces.Type.IEmail> Interfaces.Type.IPerson.Emails
        {
            get => [.. _emails];
            set
            {
                _emails = value?.ConvertAll(email => (Email)email) ?? [];
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [JsonIgnore]
        List<Interfaces.Type.IPhoneNumber> Interfaces.Type.IPerson.PhoneNumbers
        {
            get => [.. _phoneNumbers];
            set
            {
                _phoneNumbers = value.ConvertAll(phone => (PhoneNumber)phone) ?? [];
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [JsonIgnore]
        List<Interfaces.Type.IAddress> Interfaces.Type.IPerson.Addresses
        {
            get => [.. _addresses.Cast<Interfaces.Type.IAddress>()];
            set
            {
                _addresses = value.ConvertAll(address => (IAddress)address) ?? [];
                ModifiedDate = DateTime.UtcNow;
            }
        }
        #endregion

        [Key]
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

        [Required, JsonPropertyName("firstName")]
        public string? FirstName
        {
            get => _firstName;
            set
            {
                _firstName = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("middleName")]
        public string? MiddleName
        {
            get => _middleName;
            set
            {
                _middleName = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("lastName")]
        public string? LastName
        {
            get => _lastName;
            set
            {
                _lastName = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("fullName")]
        public string? FullName => _firstName == null && _middleName == null && _lastName == null ? null :
            _firstName == null || _lastName == null ?
            throw new ArgumentNullException("FirstName and/or LastName properties cannot be null.") :
            _middleName == null ?
            $"{_firstName} {_lastName}" : $"{_firstName} {_middleName} {_lastName}";

        [JsonPropertyName("userName"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull), UserNameValidator]
        public string? UserName
        {
            get => _userName;
            set
            {
                _userName = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("pronouns")]
        public Pronouns? Pronouns
        {
            get => _pronouns;
            set
            {
                _pronouns = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("birthDate")]
        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [JsonPropertyName("deceasedDate"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public DateTime? DeceasedDate
        {
            get => _deceasedDate;
            set
            {
                _deceasedDate = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("joinedDate")]
        public DateTime? JoinedDate
        {
            get => _joinedDate;
            set
            {
                _joinedDate = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("emails"), EmailsValidator]
        public List<Email> Emails
        {
            get => _emails;
            set
            {
                _emails = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("phoneNumbers"), PhoneNumbersValidator]
        public List<PhoneNumber> PhoneNumbers
        {
            get => _phoneNumbers;
            set
            {
                _phoneNumbers = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("addresses")]
        public List<IAddress> Addresses
        {
            get => _addresses;
            set
            {
                _addresses = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("isActive")]
        public bool? IsActive
        {
            get => _isActive;
            set
            {
                _isActive = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("isDeceased")]
        public bool? IsDeceased
        {
            get => _isDeceased;
            set
            {
                _isDeceased = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("isAdmin")]
        public bool? IsAdmin
        {
            get => _isAdmin;
            set
            {
                _isAdmin = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [JsonPropertyName("isSuperUser"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public bool? IsSuperUser
        {
            get => _isSuperUser;
            set
            {
                _isSuperUser = value;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [Required, JsonPropertyName("linkedEntityId"), Range(0, int.MaxValue, ErrorMessage = "Linked Entity Id must be a non-negative number.")]
        public int LinkedEntityId
        {
            get => _linkedEntityId;
            set
            {
                _linkedEntityId = value;
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
                _linkedEntityType = value?.GetType().Name;
                ModifiedDate = DateTime.UtcNow;
            }
        }

        [NotMapped]
        [Required, JsonPropertyName("linkedEntityType")]
        public string? LinkedEntityType => _linkedEntityType;

        [Required, JsonPropertyName("createdDate")]
        public DateTime CreatedDate => _createdDate;

        [Required, JsonPropertyName("modifiedDate")]
        public DateTime? ModifiedDate { get; set; } = null;
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
            List<Email> emails,
            List<PhoneNumber> phoneNumbers,
            List<IAddress> addresses,
            bool? isActive,
            bool? isDeceased,
            bool? isAdmin,
            bool? isSuperUser,
            int linkedEntityId,
            IDomainEntity? linkedEntity,
            string? linkedEntityType,
            DateTime createdDate,
            DateTime? modifiedDate)
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
            _createdDate = createdDate;
            ModifiedDate = modifiedDate;
        }

        public Contact(
            string? firstName,
            string? middleName,
            string? lastName,
            string? userName,
            Pronouns? pronouns,
            DateTime? birthDate,
            DateTime? joinedDate,
            List<Email> emails,
            List<PhoneNumber> phoneNumbers,
            List<IAddress> addresses,
            bool? isActive,
            bool? isAdmin,
            int linkedEntityId,
            IDomainEntity? linkedEntity,
            string? linkedEntityType)
        {
            _firstName = firstName;
            _middleName = middleName;
            _lastName = lastName;
            _userName = userName;
            _pronouns = pronouns;
            _birthDate = birthDate;
            _joinedDate = joinedDate;
            _emails = emails ?? [];
            _phoneNumbers = phoneNumbers ?? [];
            _addresses = addresses ?? [];
            _isActive = isActive;
            _isAdmin = isAdmin;
            _linkedEntityId = linkedEntityId;
            _linkedEntity = linkedEntity;
            _linkedEntityType = linkedEntityType;
        }

        internal Contact(IContactDTO dto)
        {
            _id = dto.Id;
            _firstName = dto.FirstName;
            _middleName = dto.MiddleName;
            _lastName = dto.LastName;
            _userName = dto.UserName;
            _pronouns = dto.Pronouns;
            _birthDate = dto.BirthDate;
            _deceasedDate = dto.DeceasedDate;
            _joinedDate = dto.JoinedDate;
            _isActive = dto.IsActive;
            _isDeceased = dto.IsDeceased;
            _isAdmin = dto.IsAdmin;
            _isSuperUser = dto.IsSuperUser;

            // Access collections through the interface properties
            var emails = ((Interfaces.Type.IPerson)dto).Emails;
            var phoneNumbers = ((Interfaces.Type.IPerson)dto).PhoneNumbers;
            var addresses = ((Interfaces.Type.IPerson)dto).Addresses;

            _emails = emails?.ConvertAll(emailInterface => emailInterface switch
            {
                EmailDTO emailDto => new Email(emailDto),
                _ => throw new InvalidOperationException($"Unknown email type: {emailInterface.GetType().Name}.")
            }) ?? [];

            _phoneNumbers = phoneNumbers?.ConvertAll(phoneInterface => phoneInterface switch
            {
                PhoneNumberDTO phoneDto => new PhoneNumber(phoneDto),
                _ => throw new InvalidOperationException($"Unknown phone number type: {phoneInterface.GetType().Name}.")
            }) ?? [];

            _addresses = addresses?.ConvertAll<IAddress>(addressInterface => addressInterface switch
            {
                CAAddressDTO caAddressDto => new CAAddress(caAddressDto),
                MXAddressDTO mxAddressDto => new MXAddress(mxAddressDto),
                USAddressDTO usAddressDto => new USAddress(usAddressDto),
                _ => throw new InvalidOperationException($"Unknown address type: {addressInterface.GetType().Name}.")
            }) ?? [];

            _createdDate = dto.CreatedDate;
            ModifiedDate = dto.ModifiedDate;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            try
            {
                if (typeof(T) == typeof(ContactDTO) || typeof(T) == typeof(IContactDTO))
                {
                    object dto = new ContactDTO()
                    {
                        Id = Id,
                        FirstName = FirstName,
                        MiddleName = MiddleName,
                        LastName = LastName,
                        FullName = FullName,
                        Pronouns = Pronouns,
                        BirthDate = BirthDate,
                        DeceasedDate = DeceasedDate,
                        UserName = UserName,
                        IsActive = IsActive,
                        IsDeceased = IsDeceased,
                        IsAdmin = IsAdmin,
                        IsSuperUser = IsSuperUser,
                        JoinedDate = JoinedDate,
                        Emails = Emails.ConvertAll(email => email.Cast<EmailDTO>()),
                        PhoneNumbers = PhoneNumbers.ConvertAll(phone => phone.Cast<PhoneNumberDTO>()),
                        Addresses = Addresses.ConvertAll(address => (IAddressDTO)CastAddressByType(address)),
                        CreatedDate = CreatedDate,
                        ModifiedDate = ModifiedDate,
                    };
                    return (T)dto;
                }
                else
                {
                    throw new InvalidCastException($"Cannot cast Contact to type {typeof(T).Name}.");
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id:{0}.FullName:{1}", _id, FullName);

        private static IDomainEntity CastAddressByType(IAddress address)
        {
            return address switch
            {
                CAAddress caAddress => caAddress.Cast<CAAddressDTO>(),
                MXAddress mxAddress => mxAddress.Cast<MXAddressDTO>(),
                USAddress usAddress => usAddress.Cast<USAddressDTO>(),
                _ => throw new InvalidOperationException($"Unknown address type: {address.GetType().Name}.")
            };
        }
        #endregion
    }
}
