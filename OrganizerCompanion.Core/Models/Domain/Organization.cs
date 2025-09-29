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
        private List<Email> _emails = [];
        private List<PhoneNumber> _phoneNumbers = [];
        private List<IAddress> _address = [];
        private List<Contact> _members = [];
        private List<Contact> _contacts = [];
        private List<Account> _accounts = [];
        private readonly DateTime _dateCreated = DateTime.Now;
        #endregion

        #region Properties
        #region Explicit Interface Implementations
        List<IEmail> IOrganization.Emails
        {
            get => _emails.ConvertAll(email => (IEmail)email);
            set
            {
                _emails = value.ConvertAll(email => (Email)email) ?? [];
                DateModified = DateTime.Now;
            }
        }

        List<IPhoneNumber> IOrganization.PhoneNumbers
        {
            get => _phoneNumbers.ConvertAll(phone => (IPhoneNumber)phone!);
            set
            {
                _phoneNumbers = value.ConvertAll(phone => (PhoneNumber)phone) ?? [];
                DateModified = DateTime.Now;
            }
        }

        List<IPerson> IOrganization.Members
        {
            get => _members.ConvertAll(member => (IPerson)member);
            set
            {
                _members = value.ConvertAll(member => (Contact)member) ?? [];
                DateModified = DateTime.Now;
            }
        }

        List<IPerson> IOrganization.Contacts
        {
            get => _contacts.ConvertAll(contact => (IPerson)contact);
            set
            {
                _contacts = value.ConvertAll(contact => (Contact)contact) ?? [];
                DateModified = DateTime.Now;
            }
        }

        List<IAccount> IOrganization.Accounts
        {
            get => _accounts.ConvertAll(account => (IAccount)account);
            set
            {
                _accounts = value.ConvertAll(account => (Account)account) ?? [];
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

        [Required, JsonPropertyName("emails")]
        public List<Email> Emails
        {
            get => _emails;
            set
            {
                _emails = value ?? [];
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("addresses")]
        public List<IAddress> Addresses
        {
            get => _address;
            set
            {
                _address = value ?? [];
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("phoneNumbers")]
        public List<PhoneNumber> PhoneNumbers
        {
            get => _phoneNumbers;
            set
            {
                _phoneNumbers = value ?? [];
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("members")]
        public List<Contact> Members
        {
            get => _members;
            set
            {
                _members = value ?? [];
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("contacts")]
        public List<Contact> Contacts
        {
            get => _contacts;
            set
            {
                _contacts = value ?? [];
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("accounts")]
        public List<Account> Accounts
        {
            get => _accounts;
            set
            {
                _accounts = value ?? [];
                DateModified = DateTime.Now;
            }
        }

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
        public Organization() { }

        [JsonConstructor]
        public Organization(
            int id,
            string? organizationName,
            List<Email> emails,
            List<PhoneNumber> phoneNumbers,
            List<IAddress> addresses,
            List<Contact> members,
            List<Contact> contacts,
            List<Account> accounts,
            DateTime dateCreated,
            DateTime? dateModified,
            bool? isCast = null,
            int? castId = null,
            string? castType = null)
        {
            try
            {
                _id = id;
                _organizationName = organizationName;
                _emails = emails ?? [];
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
        public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id{0}.OrganizationName{1}", _id, OrganizationName);
        #endregion
    }
}
