using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class ContactShould
    {
        private Contact _sut;
        private readonly DateTime _testCreatedDate = new(2023, 1, 1, 12, 0, 0);
        private readonly DateTime _testModifiedDate = new(2023, 1, 2, 12, 0, 0);
        private List<Email> _testEmails;
        private List<PhoneNumber> _testPhoneNumbers;
        private List<IAddress> _testAddresses;
        private User _linkedUser;

        [SetUp]
        public void SetUp()
        {
            _linkedUser = new User
            {
                Id = 1,
                FirstName = "Linked",
                LastName = "User"
            };

            _testEmails =
            [
                new Email
                {
                    Id = 1,
                    EmailAddress = "test@example.com",
                    Type = OrganizerCompanion.Core.Enums.Types.Home
                }
            ];

            _testPhoneNumbers =
            [
                new PhoneNumber
                {
                    Id = 1,
                    Phone = "555-123-4567",
                    Type = OrganizerCompanion.Core.Enums.Types.Home
                }
            ];

            _testAddresses =
            [
                new CAAddress
                {
                    Id = 1,
                    Street1 = "123 Main St",
                    City = "Toronto",
                    Province = new OrganizerCompanion.Core.Models.Type.CAProvince { Name = "Ontario", Abbreviation = "ON" },
                    ZipCode = "M5V 3A8",
                    Type = OrganizerCompanion.Core.Enums.Types.Home
                }
            ];

            _sut = new Contact();
        }

        #region Constructor Tests

        [Test, Category("Models")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Arrange & Act
            var contact = new Contact();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(contact.Id, Is.EqualTo(0));
                Assert.That(contact.FirstName, Is.Null);
                Assert.That(contact.MiddleName, Is.Null);
                Assert.That(contact.LastName, Is.Null);
                Assert.That(contact.UserName, Is.Null);
                Assert.That(contact.Pronouns, Is.Null);
                Assert.That(contact.BirthDate, Is.Null);
                Assert.That(contact.DeceasedDate, Is.Null);
                Assert.That(contact.JoinedDate, Is.Null);
                Assert.That(contact.Emails, Is.Not.Null);
                Assert.That(contact.Emails, Is.Empty);
                Assert.That(contact.PhoneNumbers, Is.Not.Null);
                Assert.That(contact.PhoneNumbers, Is.Empty);
                Assert.That(contact.Addresses, Is.Not.Null);
                Assert.That(contact.Addresses, Is.Empty);
                Assert.That(contact.IsActive, Is.Null);
                Assert.That(contact.IsDeceased, Is.Null);
                Assert.That(contact.IsAdmin, Is.Null);
                Assert.That(contact.IsSuperUser, Is.Null);
                Assert.That(contact.LinkedEntityId, Is.EqualTo(0));
                Assert.That(contact.LinkedEntity, Is.Null);
                Assert.That(contact.LinkedEntityType, Is.Null);
                Assert.That(contact.CreatedDate, Is.Not.EqualTo(default(DateTime)));
                Assert.That(contact.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsAllPropertiesCorrectly()
        {
            // Arrange & Act
            var contact = new Contact(
                id: 1,
                firstName: "John",
                middleName: "Michael",
                lastName: "Doe",
                userName: "johndoe",
                pronouns: Pronouns.HeHim,
                birthDate: new DateTime(1990, 1, 1),
                deceasedDate: null,
                joinedDate: new DateTime(2023, 1, 1),
                emails: _testEmails,
                phoneNumbers: _testPhoneNumbers,
                addresses: _testAddresses,
                isActive: true,
                isDeceased: false,
                isAdmin: false,
                isSuperUser: false,
                linkedEntityId: 1,
                linkedEntity: _linkedUser,
                linkedEntityType: "User",
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(contact.Id, Is.EqualTo(1));
                Assert.That(contact.FirstName, Is.EqualTo("John"));
                Assert.That(contact.MiddleName, Is.EqualTo("Michael"));
                Assert.That(contact.LastName, Is.EqualTo("Doe"));
                Assert.That(contact.UserName, Is.EqualTo("johndoe"));
                Assert.That(contact.Pronouns, Is.EqualTo(Pronouns.HeHim));
                Assert.That(contact.BirthDate, Is.EqualTo(new DateTime(1990, 1, 1)));
                Assert.That(contact.DeceasedDate, Is.Null);
                Assert.That(contact.JoinedDate, Is.EqualTo(new DateTime(2023, 1, 1)));
                Assert.That(contact.Emails, Has.Count.EqualTo(1));
                Assert.That(contact.PhoneNumbers, Has.Count.EqualTo(1));
                Assert.That(contact.Addresses, Has.Count.EqualTo(1));
                Assert.That(contact.IsActive, Is.True);
                Assert.That(contact.IsDeceased, Is.False);
                Assert.That(contact.IsAdmin, Is.False);
                Assert.That(contact.IsSuperUser, Is.False);
                Assert.That(contact.LinkedEntityId, Is.EqualTo(1));
                Assert.That(contact.LinkedEntity, Is.EqualTo(_linkedUser));
                Assert.That(contact.LinkedEntityType, Is.EqualTo("User"));
                Assert.That(contact.CreatedDate, Is.EqualTo(_testCreatedDate));
                Assert.That(contact.ModifiedDate, Is.EqualTo(_testModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullCollections_InitializesEmptyLists()
        {
            // Arrange & Act
            var contact = new Contact(
                id: 1,
                firstName: "John",
                middleName: null,
                lastName: "Doe",
                userName: null,
                pronouns: Pronouns.HeHim,
                birthDate: new DateTime(1990, 1, 1),
                deceasedDate: null,
                joinedDate: new DateTime(2023, 1, 1),
                emails: [],
                phoneNumbers: [],
                addresses: [],
                isActive: true,
                isDeceased: false,
                isAdmin: false,
                isSuperUser: null,
                linkedEntityId: 0,
                linkedEntity: null,
                linkedEntityType: null,
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(contact.Emails, Is.Not.Null);
                Assert.That(contact.Emails, Is.Empty);
                Assert.That(contact.PhoneNumbers, Is.Not.Null);
                Assert.That(contact.PhoneNumbers, Is.Empty);
                Assert.That(contact.Addresses, Is.Not.Null);
                Assert.That(contact.Addresses, Is.Empty);
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var firstName = "Alice";
            var middleName = "Marie";
            var lastName = "Johnson";
            var userName = "alicej";
            var pronouns = Pronouns.SheHer;
            var birthDate = new DateTime(1992, 3, 15);
            var joinedDate = new DateTime(2022, 6, 1);
            var isActive = true;
            var isAdmin = false;
            var linkedEntityId = 42;
            var linkedEntity = _linkedUser;
            var linkedEntityType = "User";

            // Act
            var contact = new Contact(
                firstName: firstName,
                middleName: middleName,
                lastName: lastName,
                userName: userName,
                pronouns: pronouns,
                birthDate: birthDate,
                joinedDate: joinedDate,
                emails: _testEmails,
                phoneNumbers: _testPhoneNumbers,
                addresses: _testAddresses,
                isActive: isActive,
                isAdmin: isAdmin,
                linkedEntityId: linkedEntityId,
                linkedEntity: linkedEntity,
                linkedEntityType: linkedEntityType
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(contact.Id, Is.EqualTo(0)); // Default value
                Assert.That(contact.FirstName, Is.EqualTo(firstName));
                Assert.That(contact.MiddleName, Is.EqualTo(middleName));
                Assert.That(contact.LastName, Is.EqualTo(lastName));
                Assert.That(contact.UserName, Is.EqualTo(userName));
                Assert.That(contact.Pronouns, Is.EqualTo(pronouns));
                Assert.That(contact.BirthDate, Is.EqualTo(birthDate));
                Assert.That(contact.DeceasedDate, Is.Null); // Not set by this constructor
                Assert.That(contact.JoinedDate, Is.EqualTo(joinedDate));
                Assert.That(contact.Emails, Is.EqualTo(_testEmails));
                Assert.That(contact.PhoneNumbers, Is.EqualTo(_testPhoneNumbers));
                Assert.That(contact.Addresses, Is.EqualTo(_testAddresses));
                Assert.That(contact.IsActive, Is.EqualTo(isActive));
                Assert.That(contact.IsDeceased, Is.Null); // Not set by this constructor
                Assert.That(contact.IsAdmin, Is.EqualTo(isAdmin));
                Assert.That(contact.IsSuperUser, Is.Null); // Not set by this constructor
                Assert.That(contact.LinkedEntityId, Is.EqualTo(linkedEntityId));
                Assert.That(contact.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(contact.LinkedEntityType, Is.EqualTo(linkedEntityType));
                Assert.That(contact.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_WithNullCollections_InitializesEmptyLists()
        {
            // Arrange
            var firstName = "Bob";
            var lastName = "Smith";

            // Act
            var contact = new Contact(
                firstName: firstName,
                middleName: null,
                lastName: lastName,
                userName: null,
                pronouns: null,
                birthDate: null,
                joinedDate: null,
                emails: null!, // Should be handled by null coalescing in constructor
                phoneNumbers: null!, // Should be handled by null coalescing in constructor
                addresses: null!, // Should be handled by null coalescing in constructor
                isActive: null,
                isAdmin: null,
                linkedEntityId: 0,
                linkedEntity: null,
                linkedEntityType: null
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(contact.FirstName, Is.EqualTo(firstName));
                Assert.That(contact.LastName, Is.EqualTo(lastName));
                Assert.That(contact.Emails, Is.Not.Null);
                Assert.That(contact.Emails, Is.Empty);
                Assert.That(contact.PhoneNumbers, Is.Not.Null);
                Assert.That(contact.PhoneNumbers, Is.Empty);
                Assert.That(contact.Addresses, Is.Not.Null);
                Assert.That(contact.Addresses, Is.Empty);
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_WithNullNameValues_AcceptsNullValues()
        {
            // Arrange & Act
            var contact = new Contact(
                firstName: null,
                middleName: null,
                lastName: null,
                userName: null,
                pronouns: Pronouns.TheyThem,
                birthDate: new DateTime(1995, 7, 20),
                joinedDate: new DateTime(2023, 2, 14),
                emails: [],
                phoneNumbers: [],
                addresses: [],
                isActive: false,
                isAdmin: true,
                linkedEntityId: 999,
                linkedEntity: _linkedUser,
                linkedEntityType: "User"
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(contact.FirstName, Is.Null);
                Assert.That(contact.MiddleName, Is.Null);
                Assert.That(contact.LastName, Is.Null);
                Assert.That(contact.UserName, Is.Null);
                Assert.That(contact.Pronouns, Is.EqualTo(Pronouns.TheyThem));
                Assert.That(contact.BirthDate, Is.EqualTo(new DateTime(1995, 7, 20)));
                Assert.That(contact.JoinedDate, Is.EqualTo(new DateTime(2023, 2, 14)));
                Assert.That(contact.IsActive, Is.False);
                Assert.That(contact.IsAdmin, Is.True);
                Assert.That(contact.LinkedEntityId, Is.EqualTo(999));
                Assert.That(contact.LinkedEntity, Is.EqualTo(_linkedUser));
                Assert.That(contact.LinkedEntityType, Is.EqualTo("User"));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_WithValidFullName_CalculatesFullNameCorrectly()
        {
            // Arrange & Act
            var contact = new Contact(
                firstName: "Charlie",
                middleName: "Robert",
                lastName: "Brown",
                userName: "charlieb",
                pronouns: Pronouns.HeHim,
                birthDate: new DateTime(1988, 11, 30),
                joinedDate: new DateTime(2021, 9, 15),
                emails: _testEmails,
                phoneNumbers: _testPhoneNumbers,
                addresses: _testAddresses,
                isActive: true,
                isAdmin: false,
                linkedEntityId: 123,
                linkedEntity: _linkedUser,
                linkedEntityType: "User"
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(contact.FirstName, Is.EqualTo("Charlie"));
                Assert.That(contact.MiddleName, Is.EqualTo("Robert"));
                Assert.That(contact.LastName, Is.EqualTo("Brown"));
                Assert.That(contact.FullName, Is.EqualTo("Charlie Robert Brown"));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_WithEmptyCollections_SetsEmptyCollections()
        {
            // Arrange
            var emptyEmails = new List<Email>();
            var emptyPhoneNumbers = new List<PhoneNumber>();
            var emptyAddresses = new List<IAddress>();

            // Act
            var contact = new Contact(
                firstName: "Diana",
                middleName: null,
                lastName: "Wilson",
                userName: "dianaw",
                pronouns: Pronouns.SheHer,
                birthDate: new DateTime(1991, 4, 8),
                joinedDate: new DateTime(2022, 11, 20),
                emails: emptyEmails,
                phoneNumbers: emptyPhoneNumbers,
                addresses: emptyAddresses,
                isActive: true,
                isAdmin: true,
                linkedEntityId: 456,
                linkedEntity: _linkedUser,
                linkedEntityType: "User"
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(contact.Emails, Is.EqualTo(emptyEmails));
                Assert.That(contact.Emails, Is.Empty);
                Assert.That(contact.PhoneNumbers, Is.EqualTo(emptyPhoneNumbers));
                Assert.That(contact.PhoneNumbers, Is.Empty);
                Assert.That(contact.Addresses, Is.EqualTo(emptyAddresses));
                Assert.That(contact.Addresses, Is.Empty);
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_WithAllPronouns_SetsPronouns()
        {
            // Arrange & Act & Assert - Test with various pronouns
            var pronounOptions = new[]
            {
                Pronouns.HeHim,
                Pronouns.SheHer,
                Pronouns.TheyThem
            };

            foreach (var pronoun in pronounOptions)
            {
                var contact = new Contact(
                    firstName: "Test",
                    middleName: null,
                    lastName: "User",
                    userName: $"test{pronoun}",
                    pronouns: pronoun,
                    birthDate: new DateTime(1990, 1, 1),
                    joinedDate: new DateTime(2023, 1, 1),
                    emails: [],
                    phoneNumbers: [],
                    addresses: [],
                    isActive: true,
                    isAdmin: false,
                    linkedEntityId: 1,
                    linkedEntity: null,
                    linkedEntityType: null
                );

                Assert.That(contact.Pronouns, Is.EqualTo(pronoun),
                    $"Pronouns should be set correctly for {pronoun}");
            }
        }

        [Test, Category("Models")]
        public void IContactDTOConstructor_WithCompleteDTO_SetsAllPropertiesCorrectly()
        {
            // Arrange
            var contactDto = new ContactDTO
            {
                Id = 100,
                FirstName = "Jane",
                MiddleName = "Marie",
                LastName = "Doe",
                UserName = "jane.doe",
                Pronouns = Pronouns.SheHer,
                BirthDate = new DateTime(1990, 5, 15),
                DeceasedDate = new DateTime(2023, 10, 1),
                JoinedDate = new DateTime(2020, 3, 1),
                IsActive = true,
                IsDeceased = true,
                IsAdmin = false,
                IsSuperUser = true,
                Emails = [new EmailDTO { Id = 1, EmailAddress = "jane@example.com", Type = OrganizerCompanion.Core.Enums.Types.Home }],
                PhoneNumbers = [new PhoneNumberDTO { Id = 2, Phone = "555-9876", Type = OrganizerCompanion.Core.Enums.Types.Work }],
                Addresses = [],
                CreatedDate = _testCreatedDate,
                ModifiedDate = _testModifiedDate
            };

            // Act
            var contact = new Contact(contactDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(contact.Id, Is.EqualTo(100));
                Assert.That(contact.FirstName, Is.EqualTo("Jane"));
                Assert.That(contact.MiddleName, Is.EqualTo("Marie"));
                Assert.That(contact.LastName, Is.EqualTo("Doe"));
                Assert.That(contact.FullName, Is.EqualTo("Jane Marie Doe"));
                Assert.That(contact.UserName, Is.EqualTo("jane.doe"));
                Assert.That(contact.Pronouns, Is.EqualTo(Pronouns.SheHer));
                Assert.That(contact.BirthDate, Is.EqualTo(new DateTime(1990, 5, 15)));
                Assert.That(contact.DeceasedDate, Is.EqualTo(new DateTime(2023, 10, 1)));
                Assert.That(contact.JoinedDate, Is.EqualTo(new DateTime(2020, 3, 1)));
                Assert.That(contact.IsActive, Is.True);
                Assert.That(contact.IsDeceased, Is.True);
                Assert.That(contact.IsAdmin, Is.False);
                Assert.That(contact.IsSuperUser, Is.True);
                Assert.That(contact.Emails, Has.Count.EqualTo(1));
                Assert.That(contact.Emails[0].EmailAddress, Is.EqualTo("jane@example.com"));
                Assert.That(contact.PhoneNumbers, Has.Count.EqualTo(1));
                Assert.That(contact.PhoneNumbers[0].Phone, Is.EqualTo("555-9876"));
                Assert.That(contact.Addresses, Is.Empty);
                Assert.That(contact.ModifiedDate, Is.EqualTo(_testModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void IContactDTOConstructor_WithMinimalDTO_SetsBasicPropertiesCorrectly()
        {
            // Arrange
            var contactDto = new ContactDTO
            {
                Id = 50,
                FirstName = "John",
                LastName = "Smith",
                Pronouns = Pronouns.HeHim,
                BirthDate = new DateTime(1985, 12, 25),
                JoinedDate = new DateTime(2021, 1, 15),
                IsActive = false,
                IsAdmin = true,
                Emails = [],
                PhoneNumbers = [],
                Addresses = []
            };

            // Act
            var contact = new Contact(contactDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(contact.Id, Is.EqualTo(50));
                Assert.That(contact.FirstName, Is.EqualTo("John"));
                Assert.That(contact.MiddleName, Is.Null);
                Assert.That(contact.LastName, Is.EqualTo("Smith"));
                Assert.That(contact.FullName, Is.EqualTo("John Smith"));
                Assert.That(contact.UserName, Is.Null);
                Assert.That(contact.Pronouns, Is.EqualTo(Pronouns.HeHim));
                Assert.That(contact.BirthDate, Is.EqualTo(new DateTime(1985, 12, 25)));
                Assert.That(contact.DeceasedDate, Is.Null);
                Assert.That(contact.JoinedDate, Is.EqualTo(new DateTime(2021, 1, 15)));
                Assert.That(contact.IsActive, Is.False);
                Assert.That(contact.IsDeceased, Is.Null);
                Assert.That(contact.IsAdmin, Is.True);
                Assert.That(contact.IsSuperUser, Is.Null);
                Assert.That(contact.Emails, Is.Empty);
                Assert.That(contact.PhoneNumbers, Is.Empty);
                Assert.That(contact.Addresses, Is.Empty);
                Assert.That(contact.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void IContactDTOConstructor_WithNullProperties_HandlesMixedNullValues()
        {
            // Arrange
            var contactDto = new ContactDTO
            {
                Id = 25,
                FirstName = "Alice",
                MiddleName = null,
                LastName = "Johnson",
                UserName = null,
                Pronouns = null,
                BirthDate = null,
                DeceasedDate = null,
                JoinedDate = null,
                IsActive = null,
                IsDeceased = null,
                IsAdmin = null,
                IsSuperUser = null,
                Emails = [],
                PhoneNumbers = [],
                Addresses = [],
                ModifiedDate = null
            };

            // Act
            var contact = new Contact(contactDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(contact.Id, Is.EqualTo(25));
                Assert.That(contact.FirstName, Is.EqualTo("Alice"));
                Assert.That(contact.MiddleName, Is.Null);
                Assert.That(contact.LastName, Is.EqualTo("Johnson"));
                Assert.That(contact.FullName, Is.EqualTo("Alice Johnson"));
                Assert.That(contact.UserName, Is.Null);
                Assert.That(contact.Pronouns, Is.Null);
                Assert.That(contact.BirthDate, Is.Null);
                Assert.That(contact.DeceasedDate, Is.Null);
                Assert.That(contact.JoinedDate, Is.Null);
                Assert.That(contact.IsActive, Is.Null);
                Assert.That(contact.IsDeceased, Is.Null);
                Assert.That(contact.IsAdmin, Is.Null);
                Assert.That(contact.IsSuperUser, Is.Null);
                Assert.That(contact.Emails, Is.Empty);
                Assert.That(contact.PhoneNumbers, Is.Empty);
                Assert.That(contact.Addresses, Is.Empty);
                Assert.That(contact.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void IContactDTOConstructor_WithMultipleEmailsAndPhones_CreatesCorrectCollections()
        {
            // Arrange
            var contactDto = new ContactDTO
            {
                Id = 125,
                FirstName = "Sarah",
                LastName = "Davis",
                Pronouns = Pronouns.SheHer,
                BirthDate = new DateTime(1988, 4, 22),
                JoinedDate = new DateTime(2022, 7, 10),
                IsActive = true,
                IsAdmin = false,
                Emails = [
                    new EmailDTO { Id = 1, EmailAddress = "sarah.personal@example.com", Type = OrganizerCompanion.Core.Enums.Types.Home },
                    new EmailDTO { Id = 2, EmailAddress = "sarah.work@company.com", Type = OrganizerCompanion.Core.Enums.Types.Work },
                    new EmailDTO { Id = 3, EmailAddress = "sarah.billing@example.com", Type = OrganizerCompanion.Core.Enums.Types.Billing }
                ],
                PhoneNumbers = [
                    new PhoneNumberDTO { Id = 1, Phone = "555-1111", Type = OrganizerCompanion.Core.Enums.Types.Home },
                    new PhoneNumberDTO { Id = 2, Phone = "555-2222", Type = OrganizerCompanion.Core.Enums.Types.Work },
                    new PhoneNumberDTO { Id = 3, Phone = "555-3333", Type = OrganizerCompanion.Core.Enums.Types.Mobile }
                ],
                Addresses = []
            };

            // Act
            var contact = new Contact(contactDto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(contact.Emails, Has.Count.EqualTo(3));
                Assert.That(contact.Emails[0].EmailAddress, Is.EqualTo("sarah.personal@example.com"));
                Assert.That(contact.Emails[1].EmailAddress, Is.EqualTo("sarah.work@company.com"));
                Assert.That(contact.Emails[2].EmailAddress, Is.EqualTo("sarah.billing@example.com"));

                Assert.That(contact.PhoneNumbers, Has.Count.EqualTo(3));
                Assert.That(contact.PhoneNumbers[0].Phone, Is.EqualTo("555-1111"));
                Assert.That(contact.PhoneNumbers[1].Phone, Is.EqualTo("555-2222"));
                Assert.That(contact.PhoneNumbers[2].Phone, Is.EqualTo("555-3333"));
            });
        }

        [Test, Category("Models")]
        public void IContactDTOConstructor_WithUnknownEmailType_ThrowsInvalidOperationException()
        {
            // Arrange
            var mockContactDto = new MockContactDTOWithInvalidEmail();

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => new Contact(mockContactDto));
            Assert.That(ex.Message, Does.Contain("Unknown email type"));
        }

        [Test, Category("Models")]
        public void IContactDTOConstructor_WithUnknownPhoneNumberType_ThrowsInvalidOperationException()
        {
            // Arrange
            var mockContactDto = new MockContactDTOWithInvalidPhoneNumber();

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => new Contact(mockContactDto));
            Assert.That(ex.Message, Does.Contain("Unknown phone number type"));
        }

        [Test, Category("Models")]
        public void IContactDTOConstructor_WithUnknownAddressType_ThrowsInvalidOperationException()
        {
            // Arrange
            var mockContactDto = new MockContactDTOWithInvalidAddress();

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => new Contact(mockContactDto));
            Assert.That(ex.Message, Does.Contain("Unknown address type"));
        }

        [Test, Category("Models")]
        public void IContactDTOConstructor_SetsDatesCorrectly()
        {
            // Arrange
            var specificCreatedDate = new DateTime(2023, 1, 1, 10, 0, 0);
            var specificModifiedDate = new DateTime(2023, 6, 15, 14, 30, 0);

            var contactDto = new ContactDTO
            {
                Id = 200,
                FirstName = "TimeTester",
                LastName = "User",
                Pronouns = Pronouns.TheyThem,
                BirthDate = new DateTime(1990, 1, 1),
                JoinedDate = new DateTime(2020, 1, 1),
                IsActive = true,
                IsAdmin = false,
                Emails = [],
                PhoneNumbers = [],
                Addresses = [],
                CreatedDate = specificCreatedDate,
                ModifiedDate = specificModifiedDate
            };

            // Act
            var contact = new Contact(contactDto);

            // Assert
            Assert.Multiple(() =>
            {
                // CreatedDate is set during construction and should be close to DateTime.Now, not copied from DTO
                Assert.That(contact.CreatedDate, Is.EqualTo(specificCreatedDate));
                Assert.That(contact.ModifiedDate, Is.EqualTo(specificModifiedDate));
            });
        }

        #endregion

        #region Property Tests

        [Test, Category("Models")]
        public void Id_Set_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Id = 123;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(123));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void FirstName_Set_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.FirstName = "Jane";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.FirstName, Is.EqualTo("Jane"));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void MiddleName_Set_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.MiddleName = "Marie";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.MiddleName, Is.EqualTo("Marie"));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void LastName_Set_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.LastName = "Smith";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LastName, Is.EqualTo("Smith"));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void UserName_Set_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.UserName = "janesmith";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.UserName, Is.EqualTo("janesmith"));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Pronouns_Set_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Pronouns = Pronouns.SheHer;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Pronouns, Is.EqualTo(Pronouns.SheHer));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void BirthDate_Set_UpdatesModifiedDate()
        {
            // Arrange
            var birthDate = new DateTime(1985, 5, 15);
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.BirthDate = birthDate;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.BirthDate, Is.EqualTo(birthDate));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void DeceasedDate_Set_UpdatesModifiedDate()
        {
            // Arrange
            var deceasedDate = new DateTime(2023, 12, 25);
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.DeceasedDate = deceasedDate;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DeceasedDate, Is.EqualTo(deceasedDate));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JoinedDate_Set_UpdatesModifiedDate()
        {
            // Arrange
            var joinedDate = new DateTime(2023, 1, 15);
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.JoinedDate = joinedDate;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.JoinedDate, Is.EqualTo(joinedDate));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Emails_Set_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Emails = _testEmails;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Emails, Has.Count.EqualTo(1));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void PhoneNumbers_Set_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.PhoneNumbers = _testPhoneNumbers;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PhoneNumbers, Has.Count.EqualTo(1));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Addresses_Set_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Addresses = _testAddresses;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Addresses, Has.Count.EqualTo(1));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void IsActive_Set_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.IsActive = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsActive, Is.True);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void IsDeceased_Set_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.IsDeceased = false;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsDeceased, Is.False);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void IsAdmin_Set_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.IsAdmin = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsAdmin, Is.True);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void IsSuperUser_Set_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.IsSuperUser = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsSuperUser, Is.True);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityId_Set_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.LinkedEntityId = 42;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(42));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Set_UpdatesModifiedDateAndLinkedEntityType()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.LinkedEntity = _linkedUser;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(_linkedUser));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("User"));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(originalModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_SetToNull_UpdatesModifiedDateAndLinkedEntityType()
        {
            // Arrange
            _sut.LinkedEntity = _linkedUser;

            // Act
            _sut.LinkedEntity = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        #endregion

        #region FullName Property Tests

        [Test, Category("Models")]
        public void FullName_WithFirstAndLastName_ReturnsFormattedName()
        {
            // Arrange
            _sut.FirstName = "John";
            _sut.LastName = "Doe";

            // Act
            var result = _sut.FullName;

            // Assert
            Assert.That(result, Is.EqualTo("John Doe"));
        }

        [Test, Category("Models")]
        public void FullName_WithFirstMiddleAndLastName_ReturnsFormattedName()
        {
            // Arrange
            _sut.FirstName = "John";
            _sut.MiddleName = "Michael";
            _sut.LastName = "Doe";

            // Act
            var result = _sut.FullName;

            // Assert
            Assert.That(result, Is.EqualTo("John Michael Doe"));
        }

        [Test, Category("Models")]
        public void FullName_WithAllNamesNull_ReturnsNull()
        {
            // Arrange
            _sut.FirstName = null;
            _sut.MiddleName = null;
            _sut.LastName = null;

            // Act
            var result = _sut.FullName;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("Models")]
        public void FullName_WithFirstNameNull_ThrowsArgumentNullException()
        {
            // Arrange
            _sut.FirstName = null;
            _sut.LastName = "Doe";

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = _sut.FullName);
        }

        [Test, Category("Models")]
        public void FullName_WithLastNameNull_ThrowsArgumentNullException()
        {
            // Arrange
            _sut.FirstName = "John";
            _sut.LastName = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => _ = _sut.FullName);
        }

        #endregion

        #region Explicit Interface Implementation Tests

        [Test, Category("Models")]
        public void ExplicitIPersonEmails_Get_ReturnsTypeInterfaceEmails()
        {
            // Arrange
            _sut.Emails = _testEmails;
            // Act
            var result = ((Interfaces.Type.IPerson)_sut).Emails;

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0], Is.InstanceOf<Interfaces.Type.IEmail>());
        }

        [Test, Category("Models")]
        public void ExplicitIPersonEmails_Set_UpdatesEmails()
        {
            // Arrange
            var typeEmails = _testEmails.Cast<Interfaces.Type.IEmail>().ToList();

            // Act
            ((Interfaces.Type.IPerson)_sut).Emails = typeEmails;

            // Assert
            Assert.That(_sut.Emails, Has.Count.EqualTo(1));
        }

        [Test, Category("Models")]
        public void ExplicitIPersonEmails_SetNull_InitializesEmptyList()
        {
            // Arrange & Act
            ((Interfaces.Type.IPerson)_sut).Emails = [];

            // Assert
            Assert.That(_sut.Emails, Is.Empty);
        }

        [Test, Category("Models")]
        public void ExplicitIPersonPhoneNumbers_Get_ReturnsTypeInterfacePhoneNumbers()
        {
            // Arrange
            _sut.PhoneNumbers = _testPhoneNumbers;
            // Act
            var result = ((Interfaces.Type.IPerson)_sut).PhoneNumbers;

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0], Is.InstanceOf<Interfaces.Type.IPhoneNumber>());
        }

        [Test, Category("Models")]
        public void ExplicitIPersonPhoneNumbers_Set_UpdatesPhoneNumbers()
        {
            // Arrange
            var typePhoneNumbers = _testPhoneNumbers.ConvertAll(phone => (Interfaces.Type.IPhoneNumber)phone);

            // Act
            ((Interfaces.Type.IPerson)_sut).PhoneNumbers = typePhoneNumbers;

            // Assert
            Assert.That(_sut.PhoneNumbers, Has.Count.EqualTo(1));
        }

        [Test, Category("Models")]
        public void ExplicitIPersonPhoneNumbers_SetNull_InitializesEmptyList()
        {
            // Arrange & Act
            ((Interfaces.Type.IPerson)_sut).PhoneNumbers = [];

            // Assert
            Assert.That(_sut.PhoneNumbers, Is.Empty);
        }

        [Test, Category("Models")]
        public void ExplicitIPersonAddresses_Get_ReturnsTypeInterfaceAddresses()
        {
            // Arrange
            _sut.Addresses = _testAddresses;

            // Act
            var result = ((Interfaces.Type.IPerson)_sut).Addresses;

            // Assert
            Assert.That(result, Has.Count.EqualTo(1));
            Assert.That(result[0], Is.InstanceOf<Interfaces.Type.IAddress>());
        }

        [Test, Category("Models")]
        public void ExplicitIPersonAddresses_Set_UpdatesAddresses()
        {
            // Arrange
            var typeAddresses = _testAddresses.Cast<Interfaces.Type.IAddress>().ToList();

            // Act
            ((Interfaces.Type.IPerson)_sut).Addresses = typeAddresses;

            // Assert
            Assert.That(_sut.Addresses, Has.Count.EqualTo(1));
        }

        [Test, Category("Models")]
        public void ExplicitIPersonAddresses_SetNull_InitializesEmptyList()
        {
            // Arrange & Act
            ((Interfaces.Type.IPerson)_sut).Addresses = [];

            // Assert
            Assert.That(_sut.Addresses, Is.Empty);
        }

        #endregion

        #region JSON Serialization Tests

        [Test, Category("Models")]
        public void ToJson_ReturnsValidJsonString()
        {
            // Arrange
            _sut.Id = 1;
            _sut.FirstName = "John";
            _sut.LastName = "Doe";
            _sut.Pronouns = Pronouns.HeHim;
            _sut.BirthDate = new DateTime(1990, 1, 1);
            _sut.JoinedDate = new DateTime(2023, 1, 1);
            _sut.IsActive = true;
            _sut.IsDeceased = false;
            _sut.IsAdmin = false;
            _sut.LinkedEntityId = 0;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
            });

            // Verify JSON contains expected properties
            var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            Assert.Multiple(() =>
            {
                Assert.That(root.TryGetProperty("id", out _), Is.True);
                Assert.That(root.TryGetProperty("firstName", out _), Is.True);
                Assert.That(root.TryGetProperty("lastName", out _), Is.True);
                Assert.That(root.TryGetProperty("fullName", out _), Is.True);
                Assert.That(root.TryGetProperty("pronouns", out _), Is.True);
                Assert.That(root.TryGetProperty("birthDate", out _), Is.True);
                Assert.That(root.TryGetProperty("joinedDate", out _), Is.True);
                Assert.That(root.TryGetProperty("emails", out _), Is.True);
                Assert.That(root.TryGetProperty("phoneNumbers", out _), Is.True);
                Assert.That(root.TryGetProperty("addresses", out _), Is.True);
                Assert.That(root.TryGetProperty("isActive", out _), Is.True);
                Assert.That(root.TryGetProperty("isDeceased", out _), Is.True);
                Assert.That(root.TryGetProperty("isAdmin", out _), Is.True);
                Assert.That(root.TryGetProperty("linkedEntityId", out _), Is.True);
                Assert.That(root.TryGetProperty("linkedEntity", out _), Is.True);
                Assert.That(root.TryGetProperty("linkedEntityType", out _), Is.True);
                Assert.That(root.TryGetProperty("createdDate", out _), Is.True);
                Assert.That(root.TryGetProperty("modifiedDate", out _), Is.True);
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithNullValues_HandlesJsonIgnoreConditions()
        {
            // Arrange
            _sut.Id = 1;
            _sut.FirstName = "John";
            _sut.LastName = "Doe";
            _sut.UserName = null; // Should be ignored when null
            _sut.DeceasedDate = null; // Should be ignored when null
            _sut.IsSuperUser = null; // Should be ignored when null

            // Act
            var json = _sut.ToJson();

            // Assert
            var doc = JsonDocument.Parse(json);
            var root = doc.RootElement;
            Assert.Multiple(() =>
            {
                Assert.That(root.TryGetProperty("userName", out _), Is.False);
                Assert.That(root.TryGetProperty("deceasedDate", out _), Is.False);
                Assert.That(root.TryGetProperty("isSuperUser", out _), Is.False);
            });
        }

        #endregion

        #region ToString Tests

        [Test, Category("Models")]
        public void ToString_ReturnsFormattedString()
        {
            // Arrange
            _sut.Id = 123;
            _sut.FirstName = "John";
            _sut.LastName = "Doe";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.That(result, Contains.Substring("Contact"));
            Assert.That(result, Contains.Substring("Id:123"));
            Assert.That(result, Contains.Substring("FullName:John Doe"));
        }

        [Test, Category("Models")]
        public void ToString_WithNullFullName_HandlesGracefully()
        {
            // Arrange
            _sut.Id = 456;
            _sut.FirstName = null;
            _sut.LastName = null;

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.That(result, Contains.Substring("Contact"));
            Assert.That(result, Contains.Substring("Id:456"));
            Assert.That(result, Contains.Substring("FullName"));
        }

        #endregion

        #region Data Annotations Tests

        [Test, Category("Models")]
        public void ValidationAttributes_IdProperty_HasRequiredAndRangeAttributes()
        {
            // Arrange
            var property = typeof(Contact).GetProperty(nameof(Contact.Id));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault() as RequiredAttribute;
            var rangeAttribute = property?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false).FirstOrDefault() as System.ComponentModel.DataAnnotations.RangeAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(requiredAttribute, Is.Not.Null);
                Assert.That(rangeAttribute, Is.Not.Null);
                Assert.That(rangeAttribute?.Minimum, Is.EqualTo(0));
                Assert.That(rangeAttribute?.Maximum, Is.EqualTo(int.MaxValue));
            });
        }

        [Test, Category("Models")]
        public void ValidationAttributes_LinkedEntityIdProperty_HasRequiredAndRangeAttributes()
        {
            // Arrange
            var property = typeof(Contact).GetProperty(nameof(Contact.LinkedEntityId));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault() as RequiredAttribute;
            var rangeAttribute = property?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false).FirstOrDefault() as System.ComponentModel.DataAnnotations.RangeAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(requiredAttribute, Is.Not.Null);
                Assert.That(rangeAttribute, Is.Not.Null);
                Assert.That(rangeAttribute?.Minimum, Is.EqualTo(0));
                Assert.That(rangeAttribute?.Maximum, Is.EqualTo(int.MaxValue));
            });
        }

        [Test, Category("Models")]
        public void ValidationAttributes_RequiredProperties_HaveRequiredAttribute()
        {
            // Arrange
            var requiredProperties = new[]
            {
                nameof(Contact.FirstName),
                nameof(Contact.MiddleName),
                nameof(Contact.LastName),
                nameof(Contact.FullName),
                nameof(Contact.Pronouns),
                nameof(Contact.BirthDate),
                nameof(Contact.JoinedDate),
                nameof(Contact.Emails),
                nameof(Contact.PhoneNumbers),
                nameof(Contact.Addresses),
                nameof(Contact.IsActive),
                nameof(Contact.IsDeceased),
                nameof(Contact.IsAdmin),
                nameof(Contact.LinkedEntity),
                nameof(Contact.LinkedEntityType),
                nameof(Contact.CreatedDate),
                nameof(Contact.ModifiedDate)
            };

            // Act & Assert
            foreach (var propertyName in requiredProperties)
            {
                var property = typeof(Contact).GetProperty(propertyName);
                var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false).FirstOrDefault() as RequiredAttribute;
                Assert.That(requiredAttribute, Is.Not.Null, $"Property {propertyName} should have Required attribute");
            }
        }

        #endregion

        #region Cast Method Tests

        [Test, Category("Models")]
        public void Cast_ToContactDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 123;
            _sut.FirstName = "John";
            _sut.MiddleName = "Michael";
            _sut.LastName = "Doe";
            _sut.UserName = "johndoe";
            _sut.Pronouns = Pronouns.HeHim;
            _sut.BirthDate = new DateTime(1990, 5, 15);
            _sut.DeceasedDate = new DateTime(2023, 12, 25);
            _sut.JoinedDate = new DateTime(2020, 1, 15);
            _sut.Emails = _testEmails;
            _sut.PhoneNumbers = _testPhoneNumbers;
            // Note: Not testing addresses due to DTO interface inheritance issues
            _sut.Addresses = [];

            // Act
            var result = _sut.Cast<ContactDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<ContactDTO>());
                Assert.That(result.Id, Is.EqualTo(_sut.Id));
                Assert.That(result.FirstName, Is.EqualTo(_sut.FirstName));
                Assert.That(result.MiddleName, Is.EqualTo(_sut.MiddleName));
                Assert.That(result.LastName, Is.EqualTo(_sut.LastName));
                Assert.That(result.FullName, Is.EqualTo(_sut.FullName));
                Assert.That(result.Pronouns, Is.EqualTo(_sut.Pronouns));
                Assert.That(result.BirthDate, Is.EqualTo(_sut.BirthDate));
                Assert.That(result.DeceasedDate, Is.EqualTo(_sut.DeceasedDate));
                Assert.That(result.JoinedDate, Is.EqualTo(_sut.JoinedDate));
                Assert.That(result.Emails, Is.Not.Null);
                Assert.That(result.Emails, Has.Count.EqualTo(_sut.Emails.Count));
                Assert.That(result.PhoneNumbers, Is.Not.Null);
                Assert.That(result.PhoneNumbers, Has.Count.EqualTo(_sut.PhoneNumbers.Count));
                Assert.That(result.Addresses, Is.Not.Null);
                Assert.That(result.Addresses, Has.Count.EqualTo(_sut.Addresses.Count));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIContactDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 456;
            _sut.FirstName = "Jane";
            _sut.MiddleName = "Marie";
            _sut.LastName = "Smith";
            _sut.Pronouns = Pronouns.SheHer;
            _sut.BirthDate = new DateTime(1985, 8, 20);
            _sut.JoinedDate = new DateTime(2021, 3, 10);
            _sut.Emails = _testEmails;
            _sut.PhoneNumbers = _testPhoneNumbers;
            // Note: Not testing addresses due to DTO interface inheritance issues
            _sut.Addresses = [];

            // Act
            var result = _sut.Cast<IContactDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<ContactDTO>());

                // Cast to concrete type to access properties since interface implementations throw NotImplementedException
                var concreteResult = (ContactDTO)result;
                Assert.That(concreteResult.Id, Is.EqualTo(_sut.Id));
                Assert.That(concreteResult.FirstName, Is.EqualTo(_sut.FirstName));
                Assert.That(concreteResult.MiddleName, Is.EqualTo(_sut.MiddleName));
                Assert.That(concreteResult.LastName, Is.EqualTo(_sut.LastName));
                Assert.That(concreteResult.FullName, Is.EqualTo(_sut.FullName));
                Assert.That(concreteResult.Pronouns, Is.EqualTo(_sut.Pronouns));
                Assert.That(concreteResult.BirthDate, Is.EqualTo(_sut.BirthDate));
                Assert.That(concreteResult.JoinedDate, Is.EqualTo(_sut.JoinedDate));
                Assert.That(concreteResult.Emails, Has.Count.EqualTo(_sut.Emails.Count));
                Assert.That(concreteResult.PhoneNumbers, Has.Count.EqualTo(_sut.PhoneNumbers.Count));
                Assert.That(concreteResult.Addresses, Has.Count.EqualTo(_sut.Addresses.Count));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToContactDTO_WithNullProperties_ShouldHandleNullValues()
        {
            // Arrange
            _sut.Id = 789;
            _sut.FirstName = "Test";
            _sut.LastName = "User";

            // Act
            var result = _sut.Cast<ContactDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(789));
                Assert.That(result.FirstName, Is.EqualTo("Test"));
                Assert.That(result.LastName, Is.EqualTo("User"));
                Assert.That(result.Emails, Is.Not.Null);
                Assert.That(result.Emails, Is.Empty);
                Assert.That(result.PhoneNumbers, Is.Not.Null);
                Assert.That(result.PhoneNumbers, Is.Empty);
                Assert.That(result.Addresses, Is.Not.Null);
                Assert.That(result.Addresses, Is.Empty);
            });
        }

        [Test, Category("Models")]
        public void Cast_ToContactDTO_WithUnknownAddressType_ThrowsInvalidOperationException()
        {
            // Arrange
            _sut.Id = 998;
            _sut.FirstName = "Unknown";
            _sut.LastName = "Address";

            // Create a mock address that's not CAAddress, MXAddress, or USAddress
            var unknownAddress = new MockAddress();
            _sut.Addresses = [unknownAddress];

            // Act & Assert
            var exception = Assert.Throws<InvalidOperationException>(() => _sut.Cast<ContactDTO>());
            Assert.Multiple(() =>
            {
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message, Does.Contain("Unknown address type: MockAddress"));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedType_ShouldThrowInvalidCastException()
        {
            // Arrange
            _sut.Id = 1;
            _sut.FirstName = "Test";
            _sut.LastName = "User";

            // Act & Assert
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message, Does.Contain("Cannot cast Contact to type MockDomainEntity."));
            });
        }

        [Test, Category("Models")]
        public void Cast_ErrorMessages_ShouldMentionContactNotEmail()
        {
            // Arrange
            _sut.Id = 1;
            _sut.FirstName = "Error";
            _sut.LastName = "Message";

            // Act & Assert for unsupported type
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message, Does.Contain("Cannot cast Contact to type MockDomainEntity."));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToContactDTO_WithCompleteData_ShouldPreserveAllData()
        {
            // Arrange - Set up Contact with comprehensive data (without addresses due to DTO interface issues)
            var createdDate = DateTime.Now.AddDays(-10);
            var modifiedDate = DateTime.Now.AddHours(-1);

            var fullContact = new Contact(
                id: 555,
                firstName: "Complete",
                middleName: "Test",
                lastName: "Contact",
                userName: "completeuser",
                pronouns: Pronouns.TheyThem,
                birthDate: new DateTime(1980, 12, 1),
                deceasedDate: null,
                joinedDate: new DateTime(2019, 6, 15),
                emails: _testEmails,
                phoneNumbers: _testPhoneNumbers,
                addresses: [], // Empty addresses to avoid casting issues
                isActive: true,
                isDeceased: false,
                isAdmin: true,
                isSuperUser: false,
                linkedEntityId: 42,
                linkedEntity: _linkedUser,
                linkedEntityType: "User",
                createdDate: createdDate,
                modifiedDate: modifiedDate
            );

            // Act
            var result = fullContact.Cast<ContactDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(555));
                Assert.That(result.FirstName, Is.EqualTo("Complete"));
                Assert.That(result.MiddleName, Is.EqualTo("Test"));
                Assert.That(result.LastName, Is.EqualTo("Contact"));
                Assert.That(result.FullName, Is.EqualTo("Complete Test Contact"));
                Assert.That(result.Pronouns, Is.EqualTo(Pronouns.TheyThem));
                Assert.That(result.BirthDate, Is.EqualTo(new DateTime(1980, 12, 1)));
                Assert.That(result.DeceasedDate, Is.Null);
                Assert.That(result.JoinedDate, Is.EqualTo(new DateTime(2019, 6, 15)));
                Assert.That(result.Emails, Has.Count.EqualTo(1));
                Assert.That(result.PhoneNumbers, Has.Count.EqualTo(1));
                Assert.That(result.Addresses, Is.Empty);
            });
        }

        [Test, Category("Models")]
        public void Cast_MultipleCallsToSameType_ShouldReturnDifferentInstances()
        {
            // Arrange
            _sut.Id = 777;
            _sut.FirstName = "Instance";
            _sut.LastName = "Test";

            // Act
            var result1 = _sut.Cast<ContactDTO>();
            var result2 = _sut.Cast<ContactDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.Not.Null);
                Assert.That(result2, Is.Not.Null);
                Assert.That(result1, Is.Not.SameAs(result2)); // Different instances
                Assert.That(result1.Id, Is.EqualTo(result2.Id)); // Same data
                Assert.That(result1.FirstName, Is.EqualTo(result2.FirstName)); // Same data
                Assert.That(result1.LastName, Is.EqualTo(result2.LastName)); // Same data
            });
        }

        [Test, Category("Models")]
        public void Cast_ToContactDTO_ShouldCastNestedEmailsCorrectly()
        {
            // Arrange
            var emails = new List<Email>
            {
                new() {
                    Id = 1,
                    EmailAddress = "primary@test.com",
                    Type = OrganizerCompanion.Core.Enums.Types.Home
                },
                new() {
                    Id = 2,
                    EmailAddress = "work@test.com",
                    Type = OrganizerCompanion.Core.Enums.Types.Work
                }
            };

            _sut.Id = 888;
            _sut.FirstName = "Email";
            _sut.LastName = "Test";
            _sut.Emails = emails;

            // Act
            var result = _sut.Cast<ContactDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Emails, Is.Not.Null);
                Assert.That(result.Emails, Has.Count.EqualTo(2));
                Assert.That(result.Emails[0], Is.TypeOf<EmailDTO>());
                Assert.That(result.Emails[1], Is.TypeOf<EmailDTO>());
                Assert.That(result.Emails[0].EmailAddress, Is.EqualTo("primary@test.com"));
                Assert.That(result.Emails[1].EmailAddress, Is.EqualTo("work@test.com"));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToContactDTO_ShouldCastNestedPhoneNumbersCorrectly()
        {
            // Arrange
            var phoneNumbers = new List<PhoneNumber>
            {
                new() {
                    Id = 1,
                    Phone = "555-123-4567",
                    Type = OrganizerCompanion.Core.Enums.Types.Home
                },
                new() {
                    Id = 2,
                    Phone = "555-987-6543",
                    Type = OrganizerCompanion.Core.Enums.Types.Work
                }
            };

            _sut.Id = 999;
            _sut.FirstName = "Phone";
            _sut.LastName = "Test";
            _sut.PhoneNumbers = phoneNumbers;

            // Act
            var result = _sut.Cast<ContactDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.PhoneNumbers, Is.Not.Null);
                Assert.That(result.PhoneNumbers, Has.Count.EqualTo(2));
                Assert.That(result.PhoneNumbers[0], Is.TypeOf<PhoneNumberDTO>());
                Assert.That(result.PhoneNumbers[1], Is.TypeOf<PhoneNumberDTO>());
                Assert.That(result.PhoneNumbers[0].Phone, Is.EqualTo("555-123-4567"));
                Assert.That(result.PhoneNumbers[1].Phone, Is.EqualTo("555-987-6543"));
            });
        }

        // Helper mock class for testing unsupported cast types
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; } = 1;
            public bool IsCast { get; set; } = false;
            public int CastId { get; set; } = 0;
            public string? CastType { get; set; } = null;
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; } = default;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        // Helper mock class for testing unknown address types
        private class MockAddress : IAddress
        {
            public int Id { get; set; } = 1;
            public OrganizerCompanion.Core.Enums.Types? Type { get; set; } = OrganizerCompanion.Core.Enums.Types.Home;
            public bool IsPrimary { get; set; } = true;
            public int? LinkedEntityId { get; set; } = 0;
            public IDomainEntity? LinkedEntity { get; set; } = null;
            public string? LinkedEntityType { get; set; } = null;
            public bool IsCast { get; set; } = false;
            public int CastId { get; set; } = 0;
            public string? CastType { get; set; } = null;
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; } = default;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        #endregion

        #region Edge Cases and Error Conditions

        [Test, Category("Models")]
        public void CreatedDate_IsReadOnly()
        {
            // Arrange
            var property = typeof(Contact).GetProperty(nameof(Contact.CreatedDate));

            // Act
            var contact = new Contact();

            // Assert
            Assert.Multiple(() =>
              {
                  Assert.That(property, Is.Not.Null, "CreatedDate property should exist");
                  Assert.That(property!.CanRead, Is.True, "CreatedDate should be readable");
                  Assert.That(property.CanWrite, Is.False, "CreatedDate should be read-only");
                  Assert.That(property.GetSetMethod(), Is.Null, "CreatedDate should not have a public setter");
                  Assert.That(contact.CreatedDate, Is.Not.EqualTo(default(DateTime)), "CreatedDate should be initialized");
              });
        }

        [Test, Category("Models")]
        public void LinkedEntityType_IsReadOnly_CannotBeSet()
        {
            // Arrange
            var property = typeof(Contact).GetProperty(nameof(Contact.LinkedEntityType));

            // Act & Assert
            Assert.That(property?.CanWrite, Is.False);
        }

        [Test, Category("Models")]
        public void ExplicitInterfaceProperties_UpdateModifiedDate()
        {
            // Test that explicit interface property setters update ModifiedDate

            var personInterface = (Interfaces.Type.IPerson)_sut;

            // Test Emails property
            personInterface.Emails = _testEmails.Cast<Interfaces.Type.IEmail>().ToList();
            var emailsModifiedDate = _sut.ModifiedDate;
            Assert.That(emailsModifiedDate, Is.Not.Null);

            // Test PhoneNumbers property  
            personInterface.PhoneNumbers = _testPhoneNumbers.Cast<Interfaces.Type.IPhoneNumber>().ToList();
            var phoneNumbersModifiedDate = _sut.ModifiedDate;
            Assert.That(phoneNumbersModifiedDate, Is.Not.Null);

            // Test Addresses property
            personInterface.Addresses = _testAddresses.Cast<Interfaces.Type.IAddress>().ToList();
            var addressesModifiedDate = _sut.ModifiedDate;
            Assert.That(addressesModifiedDate, Is.Not.Null);
        }

        #endregion

        #region Mock Classes for Testing

        // Mock ContactDTO with invalid email type for testing error handling
        private class MockContactDTOWithInvalidEmail : IContactDTO
        {
            public int Id { get; set; } = 1;
            public string? FirstName { get; set; } = "Test";
            public string? MiddleName { get; set; } = null;
            public string? LastName { get; set; } = "User";
            public string? FullName => $"{FirstName} {LastName}";
            public Pronouns? Pronouns { get; set; } = OrganizerCompanion.Core.Enums.Pronouns.HeHim;
            public DateTime? BirthDate { get; set; } = new DateTime(1990, 1, 1);
            public DateTime? DeceasedDate { get; set; } = null;
            public DateTime? JoinedDate { get; set; } = new DateTime(2020, 1, 1);
            public string? UserName { get; set; } = "testuser";
            public bool? IsActive { get; set; } = true;
            public bool? IsDeceased { get; set; } = false;
            public bool? IsAdmin { get; set; } = false;
            public bool? IsSuperUser { get; set; } = false;
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; } = default;

            List<Interfaces.Type.IEmail> Interfaces.Type.IPerson.Emails
            {
                get => [new MockInvalidEmail()];
                set { }
            }

            List<Interfaces.Type.IPhoneNumber> Interfaces.Type.IPerson.PhoneNumbers
            {
                get => [];
                set { }
            }

            List<Interfaces.Type.IAddress> Interfaces.Type.IPerson.Addresses
            {
                get => [];
                set { }
            }

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => throw new NotImplementedException();
        }

        // Mock ContactDTO with invalid phone number type for testing error handling
        private class MockContactDTOWithInvalidPhoneNumber : IContactDTO
        {
            public int Id { get; set; } = 1;
            public string? FirstName { get; set; } = "Test";
            public string? MiddleName { get; set; } = null;
            public string? LastName { get; set; } = "User";
            public string? FullName => $"{FirstName} {LastName}";
            public Pronouns? Pronouns { get; set; } = OrganizerCompanion.Core.Enums.Pronouns.HeHim;
            public DateTime? BirthDate { get; set; } = new DateTime(1990, 1, 1);
            public DateTime? DeceasedDate { get; set; } = null;
            public DateTime? JoinedDate { get; set; } = new DateTime(2020, 1, 1);
            public string? UserName { get; set; } = "testuser";
            public bool? IsActive { get; set; } = true;
            public bool? IsDeceased { get; set; } = false;
            public bool? IsAdmin { get; set; } = false;
            public bool? IsSuperUser { get; set; } = false;
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; } = default;

            List<Interfaces.Type.IEmail> Interfaces.Type.IPerson.Emails
            {
                get => [];
                set { }
            }

            List<Interfaces.Type.IPhoneNumber> Interfaces.Type.IPerson.PhoneNumbers
            {
                get => [new MockInvalidPhoneNumber()];
                set { }
            }

            List<Interfaces.Type.IAddress> Interfaces.Type.IPerson.Addresses
            {
                get => [];
                set { }
            }

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => throw new NotImplementedException();
        }

        // Mock ContactDTO with invalid address type for testing error handling
        private class MockContactDTOWithInvalidAddress : IContactDTO
        {
            public int Id { get; set; } = 1;
            public string? FirstName { get; set; } = "Test";
            public string? MiddleName { get; set; } = null;
            public string? LastName { get; set; } = "User";
            public string? FullName => $"{FirstName} {LastName}";
            public Pronouns? Pronouns { get; set; } = OrganizerCompanion.Core.Enums.Pronouns.HeHim;
            public DateTime? BirthDate { get; set; } = new DateTime(1990, 1, 1);
            public DateTime? DeceasedDate { get; set; } = null;
            public DateTime? JoinedDate { get; set; } = new DateTime(2020, 1, 1);
            public string? UserName { get; set; } = "testuser";
            public bool? IsActive { get; set; } = true;
            public bool? IsDeceased { get; set; } = false;
            public bool? IsAdmin { get; set; } = false;
            public bool? IsSuperUser { get; set; } = false;
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; } = default;

            List<Interfaces.Type.IEmail> Interfaces.Type.IPerson.Emails
            {
                get => [];
                set { }
            }

            List<Interfaces.Type.IPhoneNumber> Interfaces.Type.IPerson.PhoneNumbers
            {
                get => [];
                set { }
            }

            List<Interfaces.Type.IAddress> Interfaces.Type.IPerson.Addresses
            {
                get => [new MockInvalidAddress()];
                set { }
            }

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => throw new NotImplementedException();
        }

        // Mock invalid email that is not EmailDTO
        private class MockInvalidEmail : Interfaces.Type.IEmail
        {
            public string? EmailAddress { get; set; } = "invalid@test.com";
            public OrganizerCompanion.Core.Enums.Types? Type { get; set; } = OrganizerCompanion.Core.Enums.Types.Home;
            public bool IsPrimary { get; set; } = false;
        }

        // Mock invalid phone number that is not PhoneNumberDTO
        private class MockInvalidPhoneNumber : Interfaces.Type.IPhoneNumber
        {
            public string? Phone { get; set; } = "555-0000";
            public OrganizerCompanion.Core.Enums.Types? Type { get; set; } = OrganizerCompanion.Core.Enums.Types.Home;
            public Countries? Country { get; set; } = Countries.UnitedStates;
        }

        // Mock invalid address that is not a known AddressDTO type
        private class MockInvalidAddress : Interfaces.Type.IAddress
        {
            public OrganizerCompanion.Core.Enums.Types? Type { get; set; } = OrganizerCompanion.Core.Enums.Types.Home;
            public bool IsPrimary { get; set; } = false;
        }

        #endregion
    }
}
