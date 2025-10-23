using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Validation.Attributes;

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
        [JsonIgnore]
        List<IEmail> IOrganization.Emails
        {
            get => _emails.ConvertAll(email => (IEmail)email);
            set
            {
                _emails = value.ConvertAll(email => (Email)email) ?? [];
                DateModified = DateTime.Now;
            }
        }

        [JsonIgnore]
        List<IPhoneNumber> IOrganization.PhoneNumbers
        {
            get => _phoneNumbers.ConvertAll(phone => (IPhoneNumber)phone!);
            set
            {
                _phoneNumbers = value.ConvertAll(phone => (PhoneNumber)phone) ?? [];
                DateModified = DateTime.Now;
            }
        }

        [JsonIgnore]
        List<IPerson> IOrganization.Members
        {
            get => _members.ConvertAll(member => (IPerson)member);
            set
            {
                _members = value.ConvertAll(member => (Contact)member) ?? [];
                DateModified = DateTime.Now;
            }
        }

        [JsonIgnore]
        List<IPerson> IOrganization.Contacts
        {
            get => _contacts.ConvertAll(contact => (IPerson)contact);
            set
            {
                _contacts = value.ConvertAll(contact => (Contact)contact) ?? [];
                DateModified = DateTime.Now;
            }
        }

        [JsonIgnore]
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

        [Required, JsonPropertyName("id"), Range(0, int.MaxValue, ErrorMessage = "Id must be a non-negative number.")]
        public int Id
        {
            get => _id;
            set
            {
                if (value < 0)
                    throw new ArgumentOutOfRangeException(nameof(Id), "Id must be a non-negative number.");
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

        [Required, JsonPropertyName("emails"), EmailsValidator]
        public List<Email> Emails
        {
            get => _emails;
            set
            {
                _emails = value ?? [];
                DateModified = DateTime.Now;
            }
        }

        [Required, JsonPropertyName("phoneNumbers"), PhoneNumbersValidator]
        public List<PhoneNumber> PhoneNumbers
        {
            get => _phoneNumbers;
            set
            {
                _phoneNumbers = value ?? [];
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

        [Required, JsonPropertyName("dateCreated")]
        public DateTime DateCreated => _dateCreated;

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

        public Organization(IOrganizationDTO dto)
        {
            _id = dto.Id;
            _organizationName = dto.OrganizationName;
            _emails = dto.Emails.ConvertAll(emailDto => new Email(emailDto));
            _phoneNumbers = dto.PhoneNumbers.ConvertAll(phoneDto => new PhoneNumber(phoneDto));
            _address = dto.Addresses.ConvertAll<IAddress>(addressDto =>
            {
                return addressDto switch
                {
                    CAAddressDTO caAddressDto => new CAAddress(caAddressDto),
                    MXAddressDTO mxAddressDto => new MXAddress(mxAddressDto),
                    USAddressDTO usAddressDto => new USAddress(usAddressDto),
                    _ => throw new InvalidCastException("Unknown address DTO type.")
                };
            });
            _members = dto.Members.ConvertAll(memberDto => new Contact(memberDto));
            _contacts = dto.Contacts.ConvertAll(contactDto => new Contact(contactDto));
            _accounts = dto.Accounts.ConvertAll(accountDto => new Account(accountDto));
            _dateCreated = dto.DateCreated;
            DateModified = dto.DateModified;
        }
        #endregion

        #region Methods
        public T Cast<T>() where T : IDomainEntity
        {
            try
            {
                if (typeof(T) == typeof(OrganizationDTO) || typeof(T) == typeof(IOrganizationDTO))
                {
                    List<IAddressDTO> addressesDto = [];
                    foreach (var item in this.Addresses)
                    {
                        if (!string.IsNullOrEmpty(item.LinkedEntityType))
                        {
                            System.Type? type = System.Type.GetType(item.LinkedEntityType);
                            if (type != null)
                            {
                                if (Activator.CreateInstance(type) is IAddressDTO addressDto)
                                {
                                    if (item is CAAddress caAddress && addressDto is CAAddressDTO caAddressDto)
                                    {
                                        caAddressDto.Id = caAddress.Id;
                                        caAddressDto.Street1 = caAddress.Street1;
                                        caAddressDto.Street2 = caAddress.Street2;
                                        caAddressDto.City = caAddress.City;
                                        caAddressDto.Province = caAddress.Province;
                                        caAddressDto.ZipCode = caAddress.ZipCode;
                                        caAddressDto.Country = caAddress.Country;
                                        caAddressDto.Type = caAddress.Type;
                                        caAddressDto.DateCreated = caAddress.DateCreated;
                                        caAddressDto.DateModified = caAddress.DateModified;
                                    }
                                    else if (item is MXAddress mxAddress && addressDto is MXAddressDTO mxAddressDto)
                                    {
                                        mxAddressDto.Id = mxAddress.Id;
                                        mxAddressDto.Street = mxAddress.Street;
                                        mxAddressDto.Neighborhood = mxAddress.Neighborhood;
                                        mxAddressDto.PostalCode = mxAddress.PostalCode;
                                        mxAddressDto.City = mxAddress.City;
                                        mxAddressDto.State = mxAddress.State;
                                        mxAddressDto.Country = mxAddress.Country;
                                        mxAddressDto.Type = mxAddress.Type;
                                        mxAddressDto.DateCreated = mxAddress.DateCreated;
                                        mxAddressDto.DateModified = mxAddress.DateModified;
                                    }
                                    else if (item is USAddress usAddress && addressDto is USAddressDTO usAddressDto)
                                    {
                                        usAddressDto.Id = usAddress.Id;
                                        usAddressDto.Street1 = usAddress.Street1;
                                        usAddressDto.Street2 = usAddress.Street2;
                                        usAddressDto.City = usAddress.City;
                                        usAddressDto.State = usAddress.State;
                                        usAddressDto.ZipCode = usAddress.ZipCode;
                                        usAddressDto.Country = usAddress.Country;
                                        usAddressDto.Type = usAddress.Type;
                                        usAddressDto.DateCreated = usAddress.DateCreated;
                                        usAddressDto.DateModified = usAddress.DateModified;
                                    }
                                    addressesDto.Add(addressDto);
                                }
                            }
                        }
                    }
                    var dto = new OrganizationDTO
                    {
                        Id = this.Id,
                        OrganizationName = this.OrganizationName,
                        Emails = this.Emails.ConvertAll(email => new EmailDTO
                        {
                            Id = email.Id,
                            EmailAddress = email.EmailAddress,
                            Type = email.Type,
                            DateCreated = email.DateCreated,
                            DateModified = email.DateModified
                        }),
                        PhoneNumbers = this.PhoneNumbers.ConvertAll(phone => new PhoneNumberDTO
                        {
                            Id = phone.Id,
                            Phone = phone.Phone,
                            Type = phone.Type,
                            DateCreated = phone.DateCreated,
                            DateModified = phone.DateModified
                        }),
                        Addresses = addressesDto,
                        Members = this.Members.ConvertAll(member => new ContactDTO
                        {
                            Id = member.Id,
                            FirstName = member.FirstName,
                            LastName = member.LastName,
                            MiddleName = member.MiddleName,
                            DateCreated = member.DateCreated,
                            DateModified = member.DateModified
                        }),
                        Contacts = this.Contacts.ConvertAll(contact => new ContactDTO
                        {
                            Id = contact.Id,
                            FirstName = contact.FirstName,
                            LastName = contact.LastName,
                            MiddleName = contact.MiddleName,
                            DateCreated = contact.DateCreated,
                            DateModified = contact.DateModified
                        }),
                        Accounts = this.Accounts.ConvertAll(account => new AccountDTO
                        {
                            Id = account.Id,
                            AccountName = account.AccountName,
                            AccountNumber = account.AccountNumber,
                            Features = [],
                            DateCreated = account.DateCreated,
                            DateModified = account.DateModified
                        }),
                        DateCreated = this.DateCreated,
                        DateModified = this.DateModified
                    };
                    return (T)(IDomainEntity)dto;
                }
                else throw new InvalidCastException($"Cannot cast Organization to type {typeof(T).Name}.");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public string ToJson() => JsonSerializer.Serialize(this, _serializerOptions);

        public override string? ToString() => string.Format(base.ToString() + ".Id{0}.OrganizationName{1}", _id, OrganizationName);
        #endregion
    }
}
