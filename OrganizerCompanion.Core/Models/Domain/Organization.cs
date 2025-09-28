using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Models.Domain
{
    internal class Organization : IOrganization
    {
        #region Fields
        private readonly JsonSerializerOptions _serializerOptions = new()
        {
            ReferenceHandler = ReferenceHandler.IgnoreCycles
        };

        private int _id = 0;
        private string? _organizationName = null;
        private List<IAddress?> _address = [];
        private List<IPhoneNumber?> _phoneNumbers = [];
        private List<IPerson?> _members = [];
        private List<IPerson?> _contacts = [];
        private List<IAccount?> _accounts = [];
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

        [Required, JsonPropertyName("organizationName")]
        public string? OrganizationName
        {
            get => _organizationName;
            set
            {
                _organizationName = value;
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("addresses")]
        public List<IAddress?> Addresses
        {
            get => _address;
            set
            {
                _address = value ?? [];
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("phoneNumbers")]
        public List<IPhoneNumber?> PhoneNumbers
        {
            get => _phoneNumbers;
            set
            {
                _phoneNumbers = value ?? [];
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("members")]
        public List<IPerson?> Members
        {
            get => _members;
            set
            {
                _members = value ?? [];
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("contacts")]
        public List<IPerson?> Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value ?? [];
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("accounts")]
        public List<IAccount?> Accounts
        {
            get => _accounts;
            set
            {
                _accounts = value ?? [];
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated { get => _dateCreated; }

        [Required, JsonPropertyName("dateModified")]
        public DateTime? DateModified { get; set; } = default(DateTime);
        #endregion

        #region Constructors
        public Organization() { }

        [JsonConstructor]
        public Organization(
            int id,
            string? organizationName,
            List<IAddress?> addresses,
            List<IPhoneNumber?> phoneNumbers,
            List<IPerson?> members,
            List<IPerson?> contacts,
            List<IAccount?> accounts,
            DateTime dateCreated,
            DateTime? dateModified)
        {
            try
            {
                ArgumentOutOfRangeException.ThrowIfNegative(id, nameof(id));
                ArgumentNullException.ThrowIfNull(organizationName, nameof(organizationName));
                ArgumentNullException.ThrowIfNull(dateCreated, nameof(dateCreated));
                
                _id = id;
                _organizationName = organizationName;
                _address = addresses ?? [];
                _phoneNumbers = phoneNumbers ?? [];
                _members = members ?? [];
                _contacts = contacts ?? [];
                _accounts = accounts ?? [];
                _dateCreated = dateCreated;
                DateModified = dateModified;
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error creating Organization object.", ex);
            }
        }
        #endregion

        #region Methods
        public IDomainEntity Cast<T>() => throw new NotImplementedException();

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id{0}.OrganizationName{1}", _id, OrganizationName);
        #endregion
    }
}
