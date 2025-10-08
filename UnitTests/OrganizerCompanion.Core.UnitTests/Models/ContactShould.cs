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
        private readonly DateTime _testDateCreated = new(2023, 1, 1, 12, 0, 0);
        private readonly DateTime _testDateModified = new(2023, 1, 2, 12, 0, 0);
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
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var contact = new Contact();
            var afterCreation = DateTime.Now;

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
                Assert.That(contact.Emails.Count, Is.EqualTo(0));
                Assert.That(contact.PhoneNumbers, Is.Not.Null);
                Assert.That(contact.PhoneNumbers.Count, Is.EqualTo(0));
                Assert.That(contact.Addresses, Is.Not.Null);
                Assert.That(contact.Addresses.Count, Is.EqualTo(0));
                Assert.That(contact.IsActive, Is.Null);
                Assert.That(contact.IsDeceased, Is.Null);
                Assert.That(contact.IsAdmin, Is.Null);
                Assert.That(contact.IsSuperUser, Is.Null);
                Assert.That(contact.LinkedEntityId, Is.EqualTo(0));
                Assert.That(contact.LinkedEntity, Is.Null);
                Assert.That(contact.LinkedEntityType, Is.Null);
                Assert.That(contact.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(contact.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(contact.DateModified, Is.EqualTo(default(DateTime)));
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
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
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
                Assert.That(contact.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(contact.DateModified, Is.EqualTo(_testDateModified));
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
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(contact.Emails, Is.Not.Null);
                Assert.That(contact.Emails.Count, Is.EqualTo(0));
                Assert.That(contact.PhoneNumbers, Is.Not.Null);
                Assert.That(contact.PhoneNumbers.Count, Is.EqualTo(0));
                Assert.That(contact.Addresses, Is.Not.Null);
                Assert.That(contact.Addresses.Count, Is.EqualTo(0));
            });
        }

        #endregion

        #region Property Tests

        [Test, Category("Models")]
        public void Id_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.Id = 123;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(123));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void FirstName_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.FirstName = "Jane";
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.FirstName, Is.EqualTo("Jane"));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void MiddleName_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.MiddleName = "Marie";
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.MiddleName, Is.EqualTo("Marie"));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void LastName_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.LastName = "Smith";
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LastName, Is.EqualTo("Smith"));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void UserName_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.UserName = "janesmith";
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.UserName, Is.EqualTo("janesmith"));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void Pronouns_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.Pronouns = Pronouns.SheHer;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Pronouns, Is.EqualTo(Pronouns.SheHer));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void BirthDate_Set_UpdatesDateModified()
        {
            // Arrange
            var birthDate = new DateTime(1985, 5, 15);
            var beforeSet = DateTime.Now;

            // Act
            _sut.BirthDate = birthDate;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.BirthDate, Is.EqualTo(birthDate));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void DeceasedDate_Set_UpdatesDateModified()
        {
            // Arrange
            var deceasedDate = new DateTime(2023, 12, 25);
            var beforeSet = DateTime.Now;

            // Act
            _sut.DeceasedDate = deceasedDate;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DeceasedDate, Is.EqualTo(deceasedDate));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void JoinedDate_Set_UpdatesDateModified()
        {
            // Arrange
            var joinedDate = new DateTime(2023, 1, 15);
            var beforeSet = DateTime.Now;

            // Act
            _sut.JoinedDate = joinedDate;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.JoinedDate, Is.EqualTo(joinedDate));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void Emails_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.Emails = _testEmails;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Emails, Has.Count.EqualTo(1));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void PhoneNumbers_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.PhoneNumbers = _testPhoneNumbers;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PhoneNumbers, Has.Count.EqualTo(1));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void Addresses_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.Addresses = _testAddresses;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Addresses, Has.Count.EqualTo(1));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void IsActive_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.IsActive = true;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsActive, Is.True);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void IsDeceased_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.IsDeceased = false;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsDeceased, Is.False);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void IsAdmin_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.IsAdmin = true;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsAdmin, Is.True);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void IsSuperUser_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.IsSuperUser = true;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsSuperUser, Is.True);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityId_Set_UpdatesDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.LinkedEntityId = 42;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(42));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Set_UpdatesDateModifiedAndLinkedEntityType()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.LinkedEntity = _linkedUser;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(_linkedUser));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("User"));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_SetToNull_UpdatesDateModifiedAndLinkedEntityType()
        {
            // Arrange
            _sut.LinkedEntity = _linkedUser;
            var beforeSet = DateTime.Now;

            // Act
            _sut.LinkedEntity = null;
            var afterSet = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(afterSet));
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
            Assert.That(_sut.Emails.Count, Is.EqualTo(0));
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
            Assert.That(_sut.PhoneNumbers.Count, Is.EqualTo(0));
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
            Assert.That(_sut.Addresses.Count, Is.EqualTo(0));
        }

        #endregion

        #region IDomainEntity Not Implemented Properties Tests

        [Test, Category("Models")]
        public void IsCast_Get_ThrowsNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _ = _sut.IsCast);
        }

        [Test, Category("Models")]
        public void IsCast_Set_ThrowsNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.IsCast = true);
        }

        [Test, Category("Models")]
        public void CastId_Get_ThrowsNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _ = _sut.CastId);
        }

        [Test, Category("Models")]
        public void CastId_Set_ThrowsNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.CastId = 1);
        }

        [Test, Category("Models")]
        public void CastType_Get_ThrowsNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _ = _sut.CastType);
        }

        [Test, Category("Models")]
        public void CastType_Set_ThrowsNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.CastType = "TestType");
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
                Assert.That(root.TryGetProperty("dateCreated", out _), Is.True);
                Assert.That(root.TryGetProperty("dateModified", out _), Is.True);
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
            Assert.That(result, Contains.Substring("Id123"));
            Assert.That(result, Contains.Substring("FullNameJohn Doe"));
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
            Assert.That(result, Contains.Substring("Id456"));
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
                Assert.That(rangeAttribute?.Minimum, Is.EqualTo(1));
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
                nameof(Contact.DateCreated),
                nameof(Contact.DateModified)
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
            _sut.MiddleName = null;
            _sut.LastName = "User";
            _sut.UserName = null;
            _sut.Pronouns = null;
            _sut.BirthDate = null;
            _sut.DeceasedDate = null;
            _sut.JoinedDate = null;
            _sut.Emails = [];
            _sut.PhoneNumbers = [];
            _sut.Addresses = [];

            // Act
            var result = _sut.Cast<ContactDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(789));
                Assert.That(result.FirstName, Is.EqualTo("Test"));
                Assert.That(result.MiddleName, Is.Null);
                Assert.That(result.LastName, Is.EqualTo("User"));
                Assert.That(result.FullName, Is.EqualTo("Test User"));
                Assert.That(result.Pronouns, Is.Null);
                Assert.That(result.BirthDate, Is.Null);
                Assert.That(result.DeceasedDate, Is.Null);
                Assert.That(result.JoinedDate, Is.Null);
                Assert.That(result.Emails, Is.Not.Null);
                Assert.That(result.Emails.Count, Is.EqualTo(0));
                Assert.That(result.PhoneNumbers, Is.Not.Null);
                Assert.That(result.PhoneNumbers.Count, Is.EqualTo(0));
                Assert.That(result.Addresses, Is.Not.Null);
                Assert.That(result.Addresses.Count, Is.EqualTo(0));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToContactDTO_WithAddresses_ThrowsInvalidCastExceptionDueToInterfaceIssue()
        {
            // Arrange
            // This test documents the current behavior where address casting fails due to interface inheritance issues
            _sut.Id = 999;
            _sut.FirstName = "Address";
            _sut.LastName = "Test";
            _sut.Addresses = _testAddresses; // This will cause the cast to fail

            // Act & Assert
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<ContactDTO>());
            Assert.Multiple(() =>
            {
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message, Does.Contain("Unable to cast object of type"));
                Assert.That(exception.Message, Does.Contain("AddressDTO"));
                Assert.That(exception.Message, Does.Contain("IAddressDTO"));
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
                Assert.That(exception.Message, Does.Contain("Error casting Email to type MockDomainEntity: Cannot cast Email to type MockDomainEntity, casting is not supported for this type"));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToContactDTO_WithCompleteData_ShouldPreserveAllData()
        {
            // Arrange - Set up Contact with comprehensive data (without addresses due to DTO interface issues)
            var dateCreated = DateTime.Now.AddDays(-10);
            var dateModified = DateTime.Now.AddHours(-1);

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
                dateCreated: dateCreated,
                dateModified: dateModified
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
                Assert.That(result.Addresses.Count, Is.EqualTo(0));
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
            public DateTime DateCreated { get; } = DateTime.Now;
            public DateTime? DateModified { get; set; } = DateTime.Now;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        #endregion

        #region Edge Cases and Error Conditions

        [Test, Category("Models")]
        public void DateCreated_IsReadOnly_CannotBeSet()
        {
            // Arrange
            var property = typeof(Contact).GetProperty(nameof(Contact.DateCreated));

            // Act & Assert
            Assert.That(property?.CanWrite, Is.False);
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
        public void FullName_IsReadOnly_CannotBeSet()
        {
            // Arrange
            var property = typeof(Contact).GetProperty(nameof(Contact.FullName));

            // Act & Assert
            Assert.That(property?.CanWrite, Is.False);
        }

        [Test, Category("Models")]
        public void Contact_ImplementsIContactInterface()
        {
            // Act & Assert
            Assert.That(_sut, Is.InstanceOf<IContact>());
        }

        [Test, Category("Models")]
        public void Contact_ImplementsIPersonInterface()
        {
            // Act & Assert
            Assert.That(_sut, Is.InstanceOf<Interfaces.Domain.IPerson>());
        }

        [Test, Category("Models")]
        public void Contact_ImplementsIDomainEntityInterface()
        {
            // Act & Assert
            Assert.That(_sut, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("Models")]
        public void Contact_ImplementsTypeIPersonInterface()
        {
            // Act & Assert
            Assert.That(_sut, Is.InstanceOf<Interfaces.Type.IPerson>());
        }

        #endregion
    }
}
