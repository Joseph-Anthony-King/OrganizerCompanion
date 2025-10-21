using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    /// <summary>
    /// Unit tests for OrganizationDTO class to achieve 100% code coverage.
    /// Tests constructor initialization, property getters/setters, interface implementations,
    /// explicit interface methods, JSON serialization attributes, data annotations, and domain entity functionality.
    /// </summary>
    [TestFixture]
    public class OrganizationDTOShould
    {
        private OrganizationDTO _organizationDTO;

        [SetUp]
        public void SetUp()
        {
            _organizationDTO = new OrganizationDTO();
        }

        #region Constructor Tests

        [Test, Category("DataTransferObjects")]
        public void Constructor_ShouldInitializeWithDefaultValues()
        {
            // Arrange & Act
            var organizationDTO = new OrganizationDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organizationDTO.Id, Is.EqualTo(0));
                Assert.That(organizationDTO.OrganizationName, Is.Null);
                Assert.That(organizationDTO.Emails, Is.Not.Null);
                Assert.That(organizationDTO.Emails, Is.Empty);
                Assert.That(organizationDTO.PhoneNumbers, Is.Not.Null);
                Assert.That(organizationDTO.PhoneNumbers, Is.Empty);
                Assert.That(organizationDTO.Addresses, Is.Not.Null);
                Assert.That(organizationDTO.Addresses, Is.Empty);
                Assert.That(organizationDTO.Members, Is.Not.Null);
                Assert.That(organizationDTO.Members, Is.Empty);
                Assert.That(organizationDTO.Contacts, Is.Not.Null);
                Assert.That(organizationDTO.Contacts, Is.Empty);
                Assert.That(organizationDTO.Accounts, Is.Not.Null);
                Assert.That(organizationDTO.Accounts, Is.Empty);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Constructor_ShouldInitializeIDomainEntityProperties()
        {
            // Arrange & Act
            var organizationDTO = new OrganizationDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organizationDTO.DateCreated, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));
                Assert.That(organizationDTO.DateModified, Is.Null);
            });
        }

        #endregion

        #region Property Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const int expectedId = 12345;

            // Act
            _organizationDTO.Id = expectedId;

            // Assert
            Assert.That(_organizationDTO.Id, Is.EqualTo(expectedId));
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationName_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const string expectedOrganizationName = "Test Organization";

            // Act
            _organizationDTO.OrganizationName = expectedOrganizationName;

            // Assert
            Assert.That(_organizationDTO.OrganizationName, Is.EqualTo(expectedOrganizationName));
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationName_ShouldAcceptNull()
        {
            // Act
            _organizationDTO.OrganizationName = null;

            // Assert
            Assert.That(_organizationDTO.OrganizationName, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationName_ShouldAcceptEmptyString()
        {
            // Act
            _organizationDTO.OrganizationName = string.Empty;

            // Assert
            Assert.That(_organizationDTO.OrganizationName, Is.EqualTo(string.Empty));
        }

        [Test, Category("DataTransferObjects")]
        public void Emails_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedEmails = new List<EmailDTO>
            {
                new() { Id = 1, EmailAddress = "test1@example.com" },
                new() { Id = 2, EmailAddress = "test2@example.com" }
            };

            // Act
            _organizationDTO.Emails = expectedEmails;

            // Assert
            Assert.That(_organizationDTO.Emails, Is.EqualTo(expectedEmails));
            Assert.That(_organizationDTO.Emails, Has.Count.EqualTo(2));
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumbers_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedPhoneNumbers = new List<PhoneNumberDTO>
            {
                new() { Id = 1, Phone = "555-0123" },
                new() { Id = 2, Phone = "555-0456" }
            };

            // Act
            _organizationDTO.PhoneNumbers = expectedPhoneNumbers;

            // Assert
            Assert.That(_organizationDTO.PhoneNumbers, Is.EqualTo(expectedPhoneNumbers));
            Assert.That(_organizationDTO.PhoneNumbers, Has.Count.EqualTo(2));
        }

        [Test, Category("DataTransferObjects")]
        public void Addresses_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var mockAddress1 = new MockAddressDTO { Id = 1 };
            var mockAddress2 = new MockAddressDTO { Id = 2 };
            var expectedAddresses = new List<IAddressDTO> { mockAddress1, mockAddress2 };

            // Act
            _organizationDTO.Addresses = expectedAddresses;

            // Assert
            Assert.That(_organizationDTO.Addresses, Is.EqualTo(expectedAddresses));
            Assert.That(_organizationDTO.Addresses, Has.Count.EqualTo(2));
        }

        [Test, Category("DataTransferObjects")]
        public void Members_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedMembers = new List<ContactDTO>
            {
                new() { Id = 1, FirstName = "John", LastName = "Doe" },
                new() { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };

            // Act
            _organizationDTO.Members = expectedMembers;

            // Assert
            Assert.That(_organizationDTO.Members, Is.EqualTo(expectedMembers));
            Assert.That(_organizationDTO.Members, Has.Count.EqualTo(2));
        }

        [Test, Category("DataTransferObjects")]
        public void Contacts_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedContacts = new List<ContactDTO>
            {
                new() { Id = 3, FirstName = "Alice", LastName = "Johnson" },
                new() { Id = 4, FirstName = "Bob", LastName = "Wilson" }
            };

            // Act
            _organizationDTO.Contacts = expectedContacts;

            // Assert
            Assert.That(_organizationDTO.Contacts, Is.EqualTo(expectedContacts));
            Assert.That(_organizationDTO.Contacts, Has.Count.EqualTo(2));
        }

        [Test, Category("DataTransferObjects")]
        public void Accounts_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedAccounts = new List<AccountDTO>
            {
                new() { Id = 1, AccountName = "user1" },
                new() { Id = 2, AccountName = "user2" }
            };

            // Act
            _organizationDTO.Accounts = expectedAccounts;

            // Assert
            Assert.That(_organizationDTO.Accounts, Is.EqualTo(expectedAccounts));
            Assert.That(_organizationDTO.Accounts, Has.Count.EqualTo(2));
        }

        #endregion

        #region IDomainEntity Property Tests

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            _organizationDTO.DateCreated = expectedDate;

            // Assert
            Assert.That(_organizationDTO.DateCreated, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 6, 20, 14, 15, 30);

            // Act
            _organizationDTO.DateModified = expectedDate;

            // Assert
            Assert.That(_organizationDTO.DateModified, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptNull()
        {
            // Act
            _organizationDTO.DateModified = null;

            // Assert
            Assert.That(_organizationDTO.DateModified, Is.Null);
        }

        #endregion

        #region Explicit Interface Implementation Tests

        [Test, Category("DataTransferObjects")]
        public void IOrganizationDTO_Emails_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var emailDTOs = new List<EmailDTO>
            {
                new() { Id = 1, EmailAddress = "test1@example.com" },
                new() { Id = 2, EmailAddress = "test2@example.com" }
            };
            var interfaceEmails = emailDTOs.ConvertAll(email => (IEmailDTO)email);
            IOrganizationDTO interfaceOrganization = _organizationDTO;

            // Act
            interfaceOrganization.Emails = interfaceEmails;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceOrganization.Emails, Is.Not.Null);
                Assert.That(interfaceOrganization.Emails, Has.Count.EqualTo(2));
                Assert.That(interfaceOrganization.Emails[0], Is.InstanceOf<IEmailDTO>());
                Assert.That(interfaceOrganization.Emails[1], Is.InstanceOf<IEmailDTO>());
                Assert.That(_organizationDTO.Emails, Has.Count.EqualTo(2));
                Assert.That(_organizationDTO.Emails[0], Is.InstanceOf<EmailDTO>());
                Assert.That(_organizationDTO.Emails[1], Is.InstanceOf<EmailDTO>());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IOrganizationDTO_PhoneNumbers_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var phoneNumberDTOs = new List<PhoneNumberDTO>
            {
                new() { Id = 1, Phone = "555-0123" },
                new() { Id = 2, Phone = "555-0456" }
            };
            var interfacePhoneNumbers = phoneNumberDTOs.ConvertAll(phone => (IPhoneNumberDTO)phone);
            IOrganizationDTO interfaceOrganization = _organizationDTO;

            // Act
            interfaceOrganization.PhoneNumbers = interfacePhoneNumbers;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceOrganization.PhoneNumbers, Is.Not.Null);
                Assert.That(interfaceOrganization.PhoneNumbers, Has.Count.EqualTo(2));
                Assert.That(interfaceOrganization.PhoneNumbers[0], Is.InstanceOf<IPhoneNumberDTO>());
                Assert.That(interfaceOrganization.PhoneNumbers[1], Is.InstanceOf<IPhoneNumberDTO>());
                Assert.That(_organizationDTO.PhoneNumbers, Has.Count.EqualTo(2));
                Assert.That(_organizationDTO.PhoneNumbers[0], Is.InstanceOf<PhoneNumberDTO>());
                Assert.That(_organizationDTO.PhoneNumbers[1], Is.InstanceOf<PhoneNumberDTO>());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IOrganizationDTO_Members_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var contactDTOs = new List<ContactDTO>
            {
                new() { Id = 1, FirstName = "John", LastName = "Doe" },
                new() { Id = 2, FirstName = "Jane", LastName = "Smith" }
            };
            var interfaceMembers = contactDTOs.ConvertAll(member => (IContactDTO)member);
            IOrganizationDTO interfaceOrganization = _organizationDTO;

            // Act
            interfaceOrganization.Members = interfaceMembers;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceOrganization.Members, Is.Not.Null);
                Assert.That(interfaceOrganization.Members, Has.Count.EqualTo(2));
                Assert.That(interfaceOrganization.Members[0], Is.InstanceOf<IContactDTO>());
                Assert.That(interfaceOrganization.Members[1], Is.InstanceOf<IContactDTO>());
                Assert.That(_organizationDTO.Members, Has.Count.EqualTo(2));
                Assert.That(_organizationDTO.Members[0], Is.InstanceOf<ContactDTO>());
                Assert.That(_organizationDTO.Members[1], Is.InstanceOf<ContactDTO>());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IOrganizationDTO_Contacts_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var contactDTOs = new List<ContactDTO>
            {
                new() { Id = 3, FirstName = "Alice", LastName = "Johnson" },
                new() { Id = 4, FirstName = "Bob", LastName = "Wilson" }
            };
            var interfaceContacts = contactDTOs.ConvertAll(contact => (IContactDTO)contact);
            IOrganizationDTO interfaceOrganization = _organizationDTO;

            // Act
            interfaceOrganization.Contacts = interfaceContacts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceOrganization.Contacts, Is.Not.Null);
                Assert.That(interfaceOrganization.Contacts, Has.Count.EqualTo(2));
                Assert.That(interfaceOrganization.Contacts[0], Is.InstanceOf<IContactDTO>());
                Assert.That(interfaceOrganization.Contacts[1], Is.InstanceOf<IContactDTO>());
                Assert.That(_organizationDTO.Contacts, Has.Count.EqualTo(2));
                Assert.That(_organizationDTO.Contacts[0], Is.InstanceOf<ContactDTO>());
                Assert.That(_organizationDTO.Contacts[1], Is.InstanceOf<ContactDTO>());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IOrganizationDTO_Accounts_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var accountDTOs = new List<AccountDTO>
            {
                new() { Id = 1, AccountName = "user1" },
                new() { Id = 2, AccountName = "user2" }
            };
            var interfaceAccounts = accountDTOs.ConvertAll(account => (IAccountDTO)account);
            IOrganizationDTO interfaceOrganization = _organizationDTO;

            // Act
            interfaceOrganization.Accounts = interfaceAccounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceOrganization.Accounts, Is.Not.Null);
                Assert.That(interfaceOrganization.Accounts, Has.Count.EqualTo(2));
                Assert.That(interfaceOrganization.Accounts[0], Is.InstanceOf<IAccountDTO>());
                Assert.That(interfaceOrganization.Accounts[1], Is.InstanceOf<IAccountDTO>());
                Assert.That(_organizationDTO.Accounts, Has.Count.EqualTo(2));
                Assert.That(_organizationDTO.Accounts[0], Is.InstanceOf<AccountDTO>());
                Assert.That(_organizationDTO.Accounts[1], Is.InstanceOf<AccountDTO>());
            });
        }

        #endregion

        #region Interface Implementation Tests

        [Test, Category("DataTransferObjects")]
        public void OrganizationDTO_ShouldImplementIOrganizationDTO()
        {
            Assert.That(_organizationDTO, Is.InstanceOf<IOrganizationDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationDTO_ShouldImplementIDomainEntity()
        {
            Assert.That(_organizationDTO, Is.InstanceOf<IDomainEntity>());
        }

        #endregion

        #region IDomainEntity Method Tests

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _organizationDTO.Cast<MockDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldReturnSerializedJson()
        {
            // Arrange
            _organizationDTO.Id = 123;
            _organizationDTO.OrganizationName = "Test Organization";

            // Act
            var result = _organizationDTO.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.Not.Empty);
                Assert.That(result, Contains.Substring("\"id\":123"));
                Assert.That(result, Contains.Substring("\"organizationName\":\"Test Organization\""));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldSerializeAllCollections()
        {
            // Arrange
            _organizationDTO.Id = 1;
            _organizationDTO.OrganizationName = "Test Org";
            _organizationDTO.Emails = [new() { Id = 1, EmailAddress = "test@example.com" }];
            _organizationDTO.PhoneNumbers = [new() { Id = 1, Phone = "555-0123" }];
            _organizationDTO.Members = [new() { Id = 1, FirstName = "John", LastName = "Doe" }];
            _organizationDTO.Contacts = [new() { Id = 2, FirstName = "Jane", LastName = "Smith" }];
            _organizationDTO.Accounts = [new() { Id = 1, AccountName = "testuser" }];

            // Act
            var result = _organizationDTO.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Contains.Substring("\"emails\""));
                Assert.That(result, Contains.Substring("\"phoneNumbers\""));
                Assert.That(result, Contains.Substring("\"addresses\""));
                Assert.That(result, Contains.Substring("\"members\""));
                Assert.That(result, Contains.Substring("\"contacts\""));
                Assert.That(result, Contains.Substring("\"accounts\""));
                Assert.That(result, Contains.Substring("\"dateCreated\""));
                Assert.That(result, Contains.Substring("\"dateModified\""));
            });
        }

        #endregion

        #region JSON Serialization Attribute Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.Id));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("id"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationName_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.OrganizationName));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("organizationName"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Emails_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.Emails));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("emails"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumbers_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.PhoneNumbers));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("phoneNumbers"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Addresses_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.Addresses));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("addresses"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Members_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.Members));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("members"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Contacts_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.Contacts));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("contacts"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Accounts_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.Accounts));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("accounts"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.DateCreated));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("dateCreated"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.DateModified));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("dateModified"));
            });
        }



        #endregion

        #region Data Annotation Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.Id));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false)
                .FirstOrDefault() as RequiredAttribute;

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationName_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.OrganizationName));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false)
                .FirstOrDefault() as RequiredAttribute;

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AllCollectionProperties_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var emailsProperty = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.Emails));
            var phoneNumbersProperty = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.PhoneNumbers));
            var addressesProperty = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.Addresses));
            var membersProperty = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.Members));
            var contactsProperty = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.Contacts));
            var accountsProperty = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.Accounts));

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(emailsProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(phoneNumbersProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(addressesProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(membersProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(contactsProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(accountsProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.DateCreated));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false)
                .FirstOrDefault() as RequiredAttribute;

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(OrganizationDTO).GetProperty(nameof(OrganizationDTO.DateModified));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false)
                .FirstOrDefault() as RequiredAttribute;

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        #endregion

        #region Edge Case Tests

        [Test, Category("DataTransferObjects")]
        public void Collections_ShouldAcceptEmptyLists()
        {
            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _organizationDTO.Emails = []);
                Assert.DoesNotThrow(() => _organizationDTO.PhoneNumbers = []);
                Assert.DoesNotThrow(() => _organizationDTO.Addresses = []);
                Assert.DoesNotThrow(() => _organizationDTO.Members = []);
                Assert.DoesNotThrow(() => _organizationDTO.Contacts = []);
                Assert.DoesNotThrow(() => _organizationDTO.Accounts = []);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Collections_ShouldAcceptLargeCollections()
        {
            // Arrange
            var largeEmailList = Enumerable.Range(1, 1000)
                .Select(i => new EmailDTO { Id = i, EmailAddress = $"email{i}@example.com" })
                .ToList();

            // Act & Assert
            Assert.DoesNotThrow(() => _organizationDTO.Emails = largeEmailList);
            Assert.That(_organizationDTO.Emails, Has.Count.EqualTo(1000));
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationName_ShouldAcceptSpecialCharacters()
        {
            // Arrange
            const string specialCharacterName = "Test-Org & Co. (2023) #1";

            // Act
            _organizationDTO.OrganizationName = specialCharacterName;

            // Assert
            Assert.That(_organizationDTO.OrganizationName, Is.EqualTo(specialCharacterName));
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationName_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            const string unicodeName = "Tëst Örganizatiön 测试机构";

            // Act
            _organizationDTO.OrganizationName = unicodeName;

            // Assert
            Assert.That(_organizationDTO.OrganizationName, Is.EqualTo(unicodeName));
        }

        #endregion

        #region Integration Tests

        [Test, Category("DataTransferObjects")]
        public void OrganizationDTO_ShouldWorkWithJsonSerialization()
        {
            // Arrange
            _organizationDTO.Id = 42;
            _organizationDTO.OrganizationName = "Integration Test Org";
            _organizationDTO.Emails = [new() { Id = 1, EmailAddress = "test@integration.com" }];

            // Act
            var json = JsonSerializer.Serialize(_organizationDTO);
            var deserializedOrganization = JsonSerializer.Deserialize<OrganizationDTO>(json);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(deserializedOrganization, Is.Not.Null);
                Assert.That(deserializedOrganization!.Id, Is.EqualTo(42));
                Assert.That(deserializedOrganization.OrganizationName, Is.EqualTo("Integration Test Org"));
                Assert.That(deserializedOrganization.Emails, Is.Not.Null);
                Assert.That(deserializedOrganization.Emails, Has.Count.EqualTo(1));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationDTO_ShouldMaintainObjectIdentityThroughInterfaceCasting()
        {
            // Arrange
            var originalEmails = new List<EmailDTO> { new() { Id = 1, EmailAddress = "test@example.com" } };
            _organizationDTO.Emails = originalEmails;

            // Act
            IOrganizationDTO interfaceOrganization = _organizationDTO;
            var interfaceEmails = interfaceOrganization.Emails;
            interfaceOrganization.Emails = interfaceEmails;

            // Assert
            Assert.That(_organizationDTO.Emails[0].EmailAddress, Is.EqualTo("test@example.com"));
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationDTO_ShouldHandleComplexObjectGraph()
        {
            // Arrange
            _organizationDTO.Id = 1;
            _organizationDTO.OrganizationName = "Complex Org";
            _organizationDTO.Emails =
            [
                new() { Id = 1, EmailAddress = "email1@complex.com" },
                new() { Id = 2, EmailAddress = "email2@complex.com" }
            ];
            _organizationDTO.Members =
            [
                new() { Id = 1, FirstName = "Member1", LastName = "LastName1" },
                new() { Id = 2, FirstName = "Member2", LastName = "LastName2" }
            ];
            _organizationDTO.Contacts =
            [
                new() { Id = 3, FirstName = "Contact1", LastName = "ContactLast1" }
            ];

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(_organizationDTO.Emails, Has.Count.EqualTo(2));
                Assert.That(_organizationDTO.Members, Has.Count.EqualTo(2));
                Assert.That(_organizationDTO.Contacts, Has.Count.EqualTo(1));
                Assert.That(_organizationDTO.PhoneNumbers.Count, Is.EqualTo(0));
                Assert.That(_organizationDTO.Addresses.Count, Is.EqualTo(0));
                Assert.That(_organizationDTO.Accounts.Count, Is.EqualTo(0));
            });
        }

        #endregion

        #region Additional Edge Case Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _organizationDTO.Id = int.MinValue;

            // Assert
            Assert.That(_organizationDTO.Id, Is.EqualTo(int.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _organizationDTO.Id = int.MaxValue;

            // Assert
            Assert.That(_organizationDTO.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldSupportSequentialAssignments()
        {
            // Arrange
            var ids = new[] { 0, 1, -1, int.MaxValue, int.MinValue, 42, 999 };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var id in ids)
                {
                    _organizationDTO.Id = id;
                    Assert.That(_organizationDTO.Id, Is.EqualTo(id));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _organizationDTO.DateCreated = DateTime.MinValue;

            // Assert
            Assert.That(_organizationDTO.DateCreated, Is.EqualTo(DateTime.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _organizationDTO.DateCreated = DateTime.MaxValue;

            // Assert
            Assert.That(_organizationDTO.DateCreated, Is.EqualTo(DateTime.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _organizationDTO.DateModified = DateTime.MinValue;

            // Assert
            Assert.That(_organizationDTO.DateModified, Is.EqualTo(DateTime.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _organizationDTO.DateModified = DateTime.MaxValue;

            // Assert
            Assert.That(_organizationDTO.DateModified, Is.EqualTo(DateTime.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldMaintainPrecision()
        {
            // Arrange
            var preciseDate = new DateTime(2023, 12, 25, 14, 30, 45, 123);

            // Act
            _organizationDTO.DateCreated = preciseDate;

            // Assert
            Assert.That(_organizationDTO.DateCreated, Is.EqualTo(preciseDate));
            Assert.That(_organizationDTO.DateCreated.Millisecond, Is.EqualTo(123));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldMaintainPrecision()
        {
            // Arrange
            var preciseDate = new DateTime(2023, 11, 15, 9, 45, 30, 456);

            // Act
            _organizationDTO.DateModified = preciseDate;

            // Assert
            Assert.That(_organizationDTO.DateModified, Is.EqualTo(preciseDate));
            Assert.That(_organizationDTO.DateModified?.Millisecond, Is.EqualTo(456));
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationName_ShouldAcceptVeryLongString()
        {
            // Arrange
            var longName = new string('O', 10000) + " Organization";

            // Act
            _organizationDTO.OrganizationName = longName;

            // Assert
            Assert.That(_organizationDTO.OrganizationName, Is.EqualTo(longName));
            Assert.That(_organizationDTO.OrganizationName?.Length, Is.EqualTo(10013));
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationName_ShouldAcceptWhitespace()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                _organizationDTO.OrganizationName = "   ";
                Assert.That(_organizationDTO.OrganizationName, Is.EqualTo("   "));

                _organizationDTO.OrganizationName = " Organization Name ";
                Assert.That(_organizationDTO.OrganizationName, Is.EqualTo(" Organization Name "));

                _organizationDTO.OrganizationName = "\t\n\r";
                Assert.That(_organizationDTO.OrganizationName, Is.EqualTo("\t\n\r"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationName_ShouldHandleConsecutiveAssignments()
        {
            // Arrange
            var names = new[] { "Org 1", null, "", "Org 2", "   ", "Final Org" };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var name in names)
                {
                    _organizationDTO.OrganizationName = name;
                    Assert.That(_organizationDTO.OrganizationName, Is.EqualTo(name));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Collections_ShouldHandleNullElements()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Test Emails with null elements
                var emailsWithNull = new List<EmailDTO> { new() { Id = 1 }, null!, new() { Id = 2 } };
                _organizationDTO.Emails = emailsWithNull;
                Assert.That(_organizationDTO.Emails, Is.EqualTo(emailsWithNull));
                Assert.That(_organizationDTO.Emails[1], Is.Null);

                // Test PhoneNumbers with null elements
                var phonesWithNull = new List<PhoneNumberDTO> { new() { Id = 1 }, null!, new() { Id = 2 } };
                _organizationDTO.PhoneNumbers = phonesWithNull;
                Assert.That(_organizationDTO.PhoneNumbers, Is.EqualTo(phonesWithNull));
                Assert.That(_organizationDTO.PhoneNumbers[1], Is.Null);

                // Test Members with null elements  
                var membersWithNull = new List<ContactDTO> { new() { Id = 1 }, null!, new() { Id = 2 } };
                _organizationDTO.Members = membersWithNull;
                Assert.That(_organizationDTO.Members, Is.EqualTo(membersWithNull));
                Assert.That(_organizationDTO.Members[1], Is.Null);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Collections_ShouldSupportConsecutiveAssignments()
        {
            // Arrange
            var emptyEmails = new List<EmailDTO>();
            var singleEmail = new List<EmailDTO> { new() { Id = 1, EmailAddress = "test@example.com" } };
            var multipleEmails = new List<EmailDTO> 
            { 
                new() { Id = 1, EmailAddress = "test1@example.com" },
                new() { Id = 2, EmailAddress = "test2@example.com" }
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                // Empty -> Single -> Multiple -> Empty
                _organizationDTO.Emails = emptyEmails;
                Assert.That(_organizationDTO.Emails, Is.EqualTo(emptyEmails));

                _organizationDTO.Emails = singleEmail;
                Assert.That(_organizationDTO.Emails, Is.EqualTo(singleEmail));

                _organizationDTO.Emails = multipleEmails;
                Assert.That(_organizationDTO.Emails, Is.EqualTo(multipleEmails));

                _organizationDTO.Emails = emptyEmails;
                Assert.That(_organizationDTO.Emails, Is.EqualTo(emptyEmails));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IOrganizationDTO_Collections_ShouldHandleEmptyToNonEmptyTransitions()
        {
            // Arrange
            IOrganizationDTO interfaceOrg = _organizationDTO;
            var emptyEmails = new List<IEmailDTO>();
            var nonEmptyEmails = new List<IEmailDTO> { new EmailDTO { Id = 1, EmailAddress = "test@example.com" } };

            // Act & Assert
            Assert.Multiple(() =>
            {
                // Start with empty
                interfaceOrg.Emails = emptyEmails;
                Assert.That(_organizationDTO.Emails.Count, Is.EqualTo(0));

                // Add items
                interfaceOrg.Emails = nonEmptyEmails;
                Assert.That(_organizationDTO.Emails.Count, Is.EqualTo(1));

                // Back to empty
                interfaceOrg.Emails = emptyEmails;
                Assert.That(_organizationDTO.Emails.Count, Is.EqualTo(0));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHandleNullToDateTransitions()
        {
            // Arrange
            var testDates = new DateTime[]
            {
                DateTime.Now,
                DateTime.MinValue,
                DateTime.MaxValue,
                new DateTime(2020, 1, 1),
                new DateTime(2030, 12, 31, 23, 59, 59)
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var date in testDates)
                {
                    // Start with null
                    _organizationDTO.DateModified = null;
                    Assert.That(_organizationDTO.DateModified, Is.Null);

                    // Assign date
                    _organizationDTO.DateModified = date;
                    Assert.That(_organizationDTO.DateModified, Is.EqualTo(date));

                    // Back to null
                    _organizationDTO.DateModified = null;
                    Assert.That(_organizationDTO.DateModified, Is.Null);
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationDTO_PropertiesShouldBeIndependent()
        {
            // Arrange & Act
            _organizationDTO.Id = 999;
            _organizationDTO.OrganizationName = "Independent Org";
            _organizationDTO.Emails = [new() { Id = 1 }];
            _organizationDTO.PhoneNumbers = [new() { Id = 1 }];
            _organizationDTO.Addresses = [new MockAddressDTO { Id = 1 }];
            _organizationDTO.Members = [new() { Id = 1 }];
            _organizationDTO.Contacts = [new() { Id = 1 }];
            _organizationDTO.Accounts = [new() { Id = 1 }];
            var testDate = DateTime.Now.AddDays(-5);
            var testModified = DateTime.Now.AddHours(-3);
            _organizationDTO.DateCreated = testDate;
            _organizationDTO.DateModified = testModified;

            // Store original values
            var originalId = _organizationDTO.Id;
            var originalName = _organizationDTO.OrganizationName;
            var originalEmails = _organizationDTO.Emails;
            var originalPhones = _organizationDTO.PhoneNumbers;
            var originalCreated = _organizationDTO.DateCreated;
            var originalModified = _organizationDTO.DateModified;

            // Assert
            Assert.Multiple(() =>
            {
                // Change Id, verify others unchanged
                _organizationDTO.Id = 1000;
                Assert.That(_organizationDTO.OrganizationName, Is.EqualTo(originalName));
                Assert.That(_organizationDTO.Emails, Is.EqualTo(originalEmails));
                Assert.That(_organizationDTO.PhoneNumbers, Is.EqualTo(originalPhones));
                Assert.That(_organizationDTO.DateCreated, Is.EqualTo(originalCreated));
                Assert.That(_organizationDTO.DateModified, Is.EqualTo(originalModified));

                // Change OrganizationName, verify others unchanged
                _organizationDTO.OrganizationName = "Changed Name";
                Assert.That(_organizationDTO.Id, Is.EqualTo(1000)); // New value
                Assert.That(_organizationDTO.Emails, Is.EqualTo(originalEmails));
                Assert.That(_organizationDTO.PhoneNumbers, Is.EqualTo(originalPhones));
                Assert.That(_organizationDTO.DateCreated, Is.EqualTo(originalCreated));
                Assert.That(_organizationDTO.DateModified, Is.EqualTo(originalModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationDTO_ShouldSupportObjectInitializerSyntax()
        {
            // Arrange
            var testCreated = DateTime.Now.AddDays(-7);
            var testModified = DateTime.Now.AddHours(-1);
            var testEmails = new List<EmailDTO> { new() { Id = 1, EmailAddress = "init@example.com" } };

            // Act
            var orgDto = new OrganizationDTO
            {
                Id = 555,
                OrganizationName = "Initializer Organization",
                Emails = testEmails,
                PhoneNumbers = [new() { Id = 1, Phone = "555-0123" }],
                Addresses = [new MockAddressDTO { Id = 1 }],
                Members = [new() { Id = 1, FirstName = "John" }],
                Contacts = [new() { Id = 2, FirstName = "Jane" }],
                Accounts = [new() { Id = 1, AccountName = "testuser" }],
                DateCreated = testCreated,
                DateModified = testModified
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(orgDto.Id, Is.EqualTo(555));
                Assert.That(orgDto.OrganizationName, Is.EqualTo("Initializer Organization"));
                Assert.That(orgDto.Emails, Is.EqualTo(testEmails));
                Assert.That(orgDto.PhoneNumbers.Count, Is.EqualTo(1));
                Assert.That(orgDto.Addresses.Count, Is.EqualTo(1));
                Assert.That(orgDto.Members.Count, Is.EqualTo(1));
                Assert.That(orgDto.Contacts.Count, Is.EqualTo(1));
                Assert.That(orgDto.Accounts.Count, Is.EqualTo(1));
                Assert.That(orgDto.DateCreated, Is.EqualTo(testCreated));
                Assert.That(orgDto.DateModified, Is.EqualTo(testModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException_WithDifferentGenericTypes()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => _organizationDTO.Cast<MockDomainEntity>());
                Assert.Throws<NotImplementedException>(() => _organizationDTO.Cast<IDomainEntity>());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowWithSpecificMessage()
        {
            // Arrange & Act
            var ex = Assert.Throws<NotImplementedException>(() => _organizationDTO.Cast<MockDomainEntity>());

            // Assert
            Assert.That(ex.Message, Is.EqualTo("Cast method not implemented for OrganizationDTO."));
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldHandleEmptyCollections()
        {
            // Arrange
            _organizationDTO.Id = 1;
            _organizationDTO.OrganizationName = "Empty Collections Org";
            _organizationDTO.Emails = [];
            _organizationDTO.PhoneNumbers = [];
            _organizationDTO.Addresses = [];
            _organizationDTO.Members = [];
            _organizationDTO.Contacts = [];
            _organizationDTO.Accounts = [];

            // Act
            var json = _organizationDTO.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Contains.Substring("\"emails\":[]"));
                Assert.That(json, Contains.Substring("\"phoneNumbers\":[]"));
                Assert.That(json, Contains.Substring("\"addresses\":[]"));
                Assert.That(json, Contains.Substring("\"members\":[]"));
                Assert.That(json, Contains.Substring("\"contacts\":[]"));
                Assert.That(json, Contains.Substring("\"accounts\":[]"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldHandleNullValues()
        {
            // Arrange
            _organizationDTO.Id = 1;
            _organizationDTO.OrganizationName = null;
            _organizationDTO.DateModified = null;

            // Act
            var json = _organizationDTO.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Contains.Substring("\"organizationName\":null"));
                Assert.That(json, Contains.Substring("\"dateModified\":null"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldProduceValidJson()
        {
            // Arrange
            _organizationDTO.Id = 42;
            _organizationDTO.OrganizationName = "JSON Test Org";
            _organizationDTO.Emails = [new() { Id = 1, EmailAddress = "json@test.com" }];

            // Act
            var json = _organizationDTO.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
                var document = JsonDocument.Parse(json);
                Assert.That(document.RootElement.GetProperty("id").GetInt32(), Is.EqualTo(42));
                Assert.That(document.RootElement.GetProperty("organizationName").GetString(), Is.EqualTo("JSON Test Org"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationDTO_ShouldMaintainStateAcrossMultipleOperations()
        {
            // Arrange
            var operations = new[]
            {
                new { Id = 1, Name = (string?)"Org One" },
                new { Id = 2, Name = (string?)null },
                new { Id = 3, Name = (string?)"" },
                new { Id = 4, Name = (string?)"Org Four" }
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var op in operations)
                {
                    _organizationDTO.Id = op.Id;
                    _organizationDTO.OrganizationName = op.Name;

                    Assert.That(_organizationDTO.Id, Is.EqualTo(op.Id));
                    Assert.That(_organizationDTO.OrganizationName, Is.EqualTo(op.Name));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IOrganizationDTO_Collections_ShouldPreserveCastingBehavior()
        {
            // Arrange
            var emails = new List<EmailDTO> { new() { Id = 1, EmailAddress = "casting@test.com" } };
            _organizationDTO.Emails = emails;
            IOrganizationDTO interfaceOrg = _organizationDTO;

            // Act
            var interfaceEmails = interfaceOrg.Emails;
            var backToEmails = interfaceEmails.ConvertAll(e => (EmailDTO)e);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceEmails.Count, Is.EqualTo(emails.Count));
                Assert.That(backToEmails.Count, Is.EqualTo(emails.Count));
                Assert.That(interfaceEmails[0].Id, Is.EqualTo(emails[0].Id));
                Assert.That(backToEmails[0].Id, Is.EqualTo(emails[0].Id));
                Assert.That(interfaceEmails[0].EmailAddress, Is.EqualTo(emails[0].EmailAddress));
                Assert.That(backToEmails[0].EmailAddress, Is.EqualTo(emails[0].EmailAddress));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void OrganizationDTO_ShouldHandleComplexScenarios()
        {
            // Arrange & Act
            _organizationDTO.Id = int.MaxValue;
            _organizationDTO.OrganizationName = "Complex Organization with 特殊字符 and emojis 🏢🏛️";
            _organizationDTO.DateCreated = DateTime.MaxValue.AddMilliseconds(-1);
            _organizationDTO.DateModified = DateTime.MinValue.AddMilliseconds(1);

            // Create complex collections
            var complexEmails = new List<EmailDTO>();
            for (int i = 1; i <= 100; i++)
            {
                complexEmails.Add(new EmailDTO
                {
                    Id = i,
                    EmailAddress = $"complex{i}_测试@example.com"
                });
            }
            _organizationDTO.Emails = complexEmails;

            var complexMembers = new List<ContactDTO>();
            for (int i = 1; i <= 50; i++)
            {
                complexMembers.Add(new ContactDTO
                {
                    Id = i,
                    FirstName = $"Member_{i}_测试",
                    LastName = $"LastName_{i}_🚀"
                });
            }
            _organizationDTO.Members = complexMembers;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_organizationDTO.Id, Is.EqualTo(int.MaxValue));
                Assert.That(_organizationDTO.OrganizationName, Contains.Substring("Complex Organization"));
                Assert.That(_organizationDTO.OrganizationName, Contains.Substring("特殊字符"));
                Assert.That(_organizationDTO.OrganizationName, Contains.Substring("🏢🏛️"));
                Assert.That(_organizationDTO.Emails.Count, Is.EqualTo(100));
                Assert.That(_organizationDTO.Members.Count, Is.EqualTo(50));
                Assert.That(_organizationDTO.DateCreated, Is.LessThan(DateTime.MaxValue));
                Assert.That(_organizationDTO.DateModified, Is.GreaterThan(DateTime.MinValue));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IOrganizationDTO_AllCollections_ShouldWorkWithInterfaceCasting()
        {
            // Arrange
            IOrganizationDTO interfaceOrg = _organizationDTO;
            var emails = new List<IEmailDTO> { new EmailDTO { Id = 1, EmailAddress = "interface@test.com" } };
            var phones = new List<IPhoneNumberDTO> { new PhoneNumberDTO { Id = 1, Phone = "555-0123" } };
            var members = new List<IContactDTO> { new ContactDTO { Id = 1, FirstName = "Member" } };
            var contacts = new List<IContactDTO> { new ContactDTO { Id = 2, FirstName = "Contact" } };
            var accounts = new List<IAccountDTO> { new AccountDTO { Id = 1, AccountName = "account" } };

            // Act
            interfaceOrg.Emails = emails;
            interfaceOrg.PhoneNumbers = phones;
            interfaceOrg.Members = members;
            interfaceOrg.Contacts = contacts;
            interfaceOrg.Accounts = accounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceOrg.Emails.Count, Is.EqualTo(1));
                Assert.That(interfaceOrg.PhoneNumbers.Count, Is.EqualTo(1));
                Assert.That(interfaceOrg.Members.Count, Is.EqualTo(1));
                Assert.That(interfaceOrg.Contacts.Count, Is.EqualTo(1));
                Assert.That(interfaceOrg.Accounts.Count, Is.EqualTo(1));

                Assert.That(_organizationDTO.Emails.Count, Is.EqualTo(1));
                Assert.That(_organizationDTO.PhoneNumbers.Count, Is.EqualTo(1));
                Assert.That(_organizationDTO.Members.Count, Is.EqualTo(1));
                Assert.That(_organizationDTO.Contacts.Count, Is.EqualTo(1));
                Assert.That(_organizationDTO.Accounts.Count, Is.EqualTo(1));
            });
        }

        #endregion
    }

    #region Mock Classes for Testing

    /// <summary>
    /// Mock implementation of IAddressDTO for testing purposes.
    /// </summary>
    internal class MockAddressDTO : IAddressDTO
    {
        public int Id { get; set; }
        public OrganizerCompanion.Core.Enums.Types? Type { get; set; }
        public bool IsPrimary { get; set; }
        public IDomainEntity? LinkedEntity { get; set; }
        public int? LinkedEntityId => LinkedEntity?.Id;
        public string? LinkedEntityType => LinkedEntity?.GetType().Name;
        public bool IsCast { get; set; }
        public int CastId { get; set; }
        public string? CastType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public T Cast<T>() where T : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public string ToJson()
        {
            throw new NotImplementedException();
        }
    }

    /// <summary>
    /// Mock implementation of IDomainEntity for testing purposes.
    /// </summary>
    internal class MockDomainEntity : IDomainEntity
    {
        public int Id { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public T Cast<T>() where T : IDomainEntity
        {
            throw new NotImplementedException();
        }

        public string ToJson()
        {
            throw new NotImplementedException();
        }
    }

    #endregion
}
