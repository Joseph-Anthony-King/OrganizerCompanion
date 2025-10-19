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
    /// Unit tests for UserDTO class to achieve 100% code coverage.
    /// Tests constructor initialization, property getters/setters, interface implementations,
    /// explicit interface methods, JSON serialization attributes, data annotations, and edge cases.
    /// </summary>
    [TestFixture]
    public class UserDTOShould
    {
        private UserDTO _userDTO;

        [SetUp]
        public void SetUp()
        {
            _userDTO = new UserDTO();
        }

        #region Constructor Tests

        [Test, Category("DataTransferObjects")]
        public void Constructor_ShouldInitializeWithDefaultValues()
        {
            // Arrange & Act
            var userDTO = new UserDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(userDTO.Id, Is.EqualTo(0));
                Assert.That(userDTO.FirstName, Is.Null);
                Assert.That(userDTO.MiddleName, Is.Null);
                Assert.That(userDTO.LastName, Is.Null);
                Assert.That(userDTO.FullName, Is.Null);
                Assert.That(userDTO.UserName, Is.Null);
                Assert.That(userDTO.Emails, Is.Not.Null);
                Assert.That(userDTO.Emails, Is.Empty);
                Assert.That(userDTO.PhoneNumbers, Is.Not.Null);
                Assert.That(userDTO.PhoneNumbers, Is.Empty);
                Assert.That(userDTO.Addresses, Is.Not.Null);
                Assert.That(userDTO.Addresses, Is.Empty);
                Assert.That(userDTO.Pronouns, Is.Null);
                Assert.That(userDTO.BirthDate, Is.Null);
                Assert.That(userDTO.DeceasedDate, Is.Null);
                Assert.That(userDTO.JoinedDate, Is.Null);
                Assert.That(userDTO.IsActive, Is.Null);
                Assert.That(userDTO.IsDeceased, Is.Null);
                Assert.That(userDTO.IsAdmin, Is.Null);
                Assert.That(userDTO.IsSuperUser, Is.Null);
                Assert.That(userDTO.DateModified, Is.Null);
                Assert.That(userDTO.DateCreated, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));
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
            _userDTO.Id = expectedId;

            // Assert
            Assert.That(_userDTO.Id, Is.EqualTo(expectedId));
        }

        [Test, Category("DataTransferObjects")]
        public void FirstName_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const string expectedFirstName = "John";

            // Act
            _userDTO.FirstName = expectedFirstName;

            // Assert
            Assert.That(_userDTO.FirstName, Is.EqualTo(expectedFirstName));
        }

        [Test, Category("DataTransferObjects")]
        public void FirstName_ShouldAcceptNull()
        {
            // Act
            _userDTO.FirstName = null;

            // Assert
            Assert.That(_userDTO.FirstName, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void MiddleName_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const string expectedMiddleName = "Michael";

            // Act
            _userDTO.MiddleName = expectedMiddleName;

            // Assert
            Assert.That(_userDTO.MiddleName, Is.EqualTo(expectedMiddleName));
        }

        [Test, Category("DataTransferObjects")]
        public void MiddleName_ShouldAcceptNull()
        {
            // Act
            _userDTO.MiddleName = null;

            // Assert
            Assert.That(_userDTO.MiddleName, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void LastName_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const string expectedLastName = "Doe";

            // Act
            _userDTO.LastName = expectedLastName;

            // Assert
            Assert.That(_userDTO.LastName, Is.EqualTo(expectedLastName));
        }

        [Test, Category("DataTransferObjects")]
        public void LastName_ShouldAcceptNull()
        {
            // Act
            _userDTO.LastName = null;

            // Assert
            Assert.That(_userDTO.LastName, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void FullName_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const string expectedFullName = "John Michael Doe";

            // Act
            _userDTO.FullName = expectedFullName;

            // Assert
            Assert.That(_userDTO.FullName, Is.EqualTo(expectedFullName));
        }

        [Test, Category("DataTransferObjects")]
        public void FullName_ShouldAcceptNull()
        {
            // Act
            _userDTO.FullName = null;

            // Assert
            Assert.That(_userDTO.FullName, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const string expectedUserName = "johndoe123";

            // Act
            _userDTO.UserName = expectedUserName;

            // Assert
            Assert.That(_userDTO.UserName, Is.EqualTo(expectedUserName));
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldAcceptNull()
        {
            // Act
            _userDTO.UserName = null;

            // Assert
            Assert.That(_userDTO.UserName, Is.Null);
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
            _userDTO.Emails = expectedEmails;

            // Assert
            Assert.That(_userDTO.Emails, Is.EqualTo(expectedEmails));
            Assert.That(_userDTO.Emails, Has.Count.EqualTo(2));
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
            _userDTO.PhoneNumbers = expectedPhoneNumbers;

            // Assert
            Assert.That(_userDTO.PhoneNumbers, Is.EqualTo(expectedPhoneNumbers));
            Assert.That(_userDTO.PhoneNumbers, Has.Count.EqualTo(2));
        }

        [Test, Category("DataTransferObjects")]
        public void Addresses_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var mockAddress1 = new MockAddressDTO { Id = 1 };
            var mockAddress2 = new MockAddressDTO { Id = 2 };
            var expectedAddresses = new List<IAddressDTO> { mockAddress1, mockAddress2 };

            // Act
            _userDTO.Addresses = expectedAddresses;

            // Assert
            Assert.That(_userDTO.Addresses, Is.EqualTo(expectedAddresses));
            Assert.That(_userDTO.Addresses, Has.Count.EqualTo(2));
        }

        [Test, Category("DataTransferObjects")]
        public void Pronouns_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const OrganizerCompanion.Core.Enums.Pronouns expectedPronouns = OrganizerCompanion.Core.Enums.Pronouns.TheyThem;

            // Act
            _userDTO.Pronouns = expectedPronouns;

            // Assert
            Assert.That(_userDTO.Pronouns, Is.EqualTo(expectedPronouns));
        }

        [Test, Category("DataTransferObjects")]
        public void Pronouns_ShouldAcceptNull()
        {
            // Act
            _userDTO.Pronouns = null;

            // Assert
            Assert.That(_userDTO.Pronouns, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Pronouns_ShouldAcceptAllValidEnumValues()
        {
            // Arrange & Act & Assert
            foreach (OrganizerCompanion.Core.Enums.Pronouns pronoun in Enum.GetValues<OrganizerCompanion.Core.Enums.Pronouns>())
            {
                _userDTO.Pronouns = pronoun;
                Assert.That(_userDTO.Pronouns, Is.EqualTo(pronoun));
            }
        }

        [Test, Category("DataTransferObjects")]
        public void BirthDate_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedBirthDate = new DateTime(1990, 5, 15);

            // Act
            _userDTO.BirthDate = expectedBirthDate;

            // Assert
            Assert.That(_userDTO.BirthDate, Is.EqualTo(expectedBirthDate));
        }

        [Test, Category("DataTransferObjects")]
        public void BirthDate_ShouldAcceptNull()
        {
            // Act
            _userDTO.BirthDate = null;

            // Assert
            Assert.That(_userDTO.BirthDate, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DeceasedDate_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedDeceasedDate = new DateTime(2023, 12, 25);

            // Act
            _userDTO.DeceasedDate = expectedDeceasedDate;

            // Assert
            Assert.That(_userDTO.DeceasedDate, Is.EqualTo(expectedDeceasedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void DeceasedDate_ShouldAcceptNull()
        {
            // Act
            _userDTO.DeceasedDate = null;

            // Assert
            Assert.That(_userDTO.DeceasedDate, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void JoinedDate_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedJoinedDate = new DateTime(2020, 1, 1);

            // Act
            _userDTO.JoinedDate = expectedJoinedDate;

            // Assert
            Assert.That(_userDTO.JoinedDate, Is.EqualTo(expectedJoinedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void JoinedDate_ShouldAcceptNull()
        {
            // Act
            _userDTO.JoinedDate = null;

            // Assert
            Assert.That(_userDTO.JoinedDate, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsActive_ShouldGetAndSetCorrectly()
        {
            // Act & Assert
            _userDTO.IsActive = true;
            Assert.That(_userDTO.IsActive, Is.True);

            _userDTO.IsActive = false;
            Assert.That(_userDTO.IsActive, Is.False);
        }

        [Test, Category("DataTransferObjects")]
        public void IsActive_ShouldAcceptNull()
        {
            // Act
            _userDTO.IsActive = null;

            // Assert
            Assert.That(_userDTO.IsActive, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsDeceased_ShouldGetAndSetCorrectly()
        {
            // Act & Assert
            _userDTO.IsDeceased = true;
            Assert.That(_userDTO.IsDeceased, Is.True);

            _userDTO.IsDeceased = false;
            Assert.That(_userDTO.IsDeceased, Is.False);
        }

        [Test, Category("DataTransferObjects")]
        public void IsDeceased_ShouldAcceptNull()
        {
            // Act
            _userDTO.IsDeceased = null;

            // Assert
            Assert.That(_userDTO.IsDeceased, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsAdmin_ShouldGetAndSetCorrectly()
        {
            // Act & Assert
            _userDTO.IsAdmin = true;
            Assert.That(_userDTO.IsAdmin, Is.True);

            _userDTO.IsAdmin = false;
            Assert.That(_userDTO.IsAdmin, Is.False);
        }

        [Test, Category("DataTransferObjects")]
        public void IsAdmin_ShouldAcceptNull()
        {
            // Act
            _userDTO.IsAdmin = null;

            // Assert
            Assert.That(_userDTO.IsAdmin, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsSuperUser_ShouldGetAndSetCorrectly()
        {
            // Act & Assert
            _userDTO.IsSuperUser = true;
            Assert.That(_userDTO.IsSuperUser, Is.True);

            _userDTO.IsSuperUser = false;
            Assert.That(_userDTO.IsSuperUser, Is.False);
        }

        [Test, Category("DataTransferObjects")]
        public void IsSuperUser_ShouldAcceptNull()
        {
            // Act
            _userDTO.IsSuperUser = null;

            // Assert
            Assert.That(_userDTO.IsSuperUser, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 6, 20, 14, 15, 30);

            // Act
            _userDTO.DateModified = expectedDate;

            // Assert
            Assert.That(_userDTO.DateModified, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptNull()
        {
            // Act
            _userDTO.DateModified = null;

            // Assert
            Assert.That(_userDTO.DateModified, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 1, 1, 12, 0, 0);

            // Act
            _userDTO.DateCreated = expectedDate;

            // Assert
            Assert.That(_userDTO.DateCreated, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldUsePrivateFieldCorrectly()
        {
            // Arrange
            var firstDate = new DateTime(2023, 1, 1);
            var secondDate = new DateTime(2023, 12, 31);

            // Act
            _userDTO.DateCreated = firstDate;
            var retrievedFirst = _userDTO.DateCreated;
            
            _userDTO.DateCreated = secondDate;
            var retrievedSecond = _userDTO.DateCreated;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(retrievedFirst, Is.EqualTo(firstDate));
                Assert.That(retrievedSecond, Is.EqualTo(secondDate));
                Assert.That(retrievedFirst, Is.Not.EqualTo(retrievedSecond));
            });
        }

        #endregion

        #region Interface Implementation Tests

        [Test, Category("DataTransferObjects")]
        public void UserDTO_ShouldImplementIUserDTO()
        {
            Assert.That(_userDTO, Is.InstanceOf<IUserDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void UserDTO_ShouldImplementIDomainEntity()
        {
            Assert.That(_userDTO, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void UserDTO_ShouldImplementIPerson()
        {
            Assert.That(_userDTO, Is.InstanceOf<OrganizerCompanion.Core.Interfaces.Type.IPerson>());
        }

        #endregion

        #region Explicit Interface Implementation Tests - IDomainEntity

        [Test, Category("DataTransferObjects")]
        public void IDomainEntity_Id_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            IDomainEntity domainEntity = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = domainEntity.Id; });
        }

        [Test, Category("DataTransferObjects")]
        public void IDomainEntity_Id_ShouldThrowNotImplementedException_OnSet()
        {
            // Arrange
            IDomainEntity domainEntity = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => domainEntity.Id = 123);
        }

        [Test, Category("DataTransferObjects")]
        public void IDomainEntity_IsCast_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            IDomainEntity domainEntity = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = domainEntity.IsCast; });
        }

        [Test, Category("DataTransferObjects")]
        public void IDomainEntity_IsCast_ShouldThrowNotImplementedException_OnSet()
        {
            // Arrange
            IDomainEntity domainEntity = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => domainEntity.IsCast = true);
        }

        [Test, Category("DataTransferObjects")]
        public void IDomainEntity_CastId_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            IDomainEntity domainEntity = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = domainEntity.CastId; });
        }

        [Test, Category("DataTransferObjects")]
        public void IDomainEntity_CastId_ShouldThrowNotImplementedException_OnSet()
        {
            // Arrange
            IDomainEntity domainEntity = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => domainEntity.CastId = 123);
        }

        [Test, Category("DataTransferObjects")]
        public void IDomainEntity_CastType_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            IDomainEntity domainEntity = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = domainEntity.CastType; });
        }

        [Test, Category("DataTransferObjects")]
        public void IDomainEntity_CastType_ShouldThrowNotImplementedException_OnSet()
        {
            // Arrange
            IDomainEntity domainEntity = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => domainEntity.CastType = "TestType");
        }

        [Test, Category("DataTransferObjects")]
        public void IDomainEntity_Cast_ShouldThrowNotImplementedException()
        {
            // Arrange
            IDomainEntity domainEntity = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => domainEntity.Cast<MockDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void IDomainEntity_ToJson_ShouldThrowNotImplementedException()
        {
            // Arrange
            IDomainEntity domainEntity = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => domainEntity.ToJson());
        }

        #endregion

        #region Explicit Interface Implementation Tests - IPerson

        [Test, Category("DataTransferObjects")]
        public void IPerson_FirstName_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = person.FirstName; });
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_FirstName_ShouldThrowNotImplementedException_OnSet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => person.FirstName = "Test");
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_MiddleName_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = person.MiddleName; });
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_MiddleName_ShouldThrowNotImplementedException_OnSet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => person.MiddleName = "Test");
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_LastName_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = person.LastName; });
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_LastName_ShouldThrowNotImplementedException_OnSet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => person.LastName = "Test");
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_FullName_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = person.FullName; });
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_Pronouns_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = person.Pronouns; });
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_Pronouns_ShouldThrowNotImplementedException_OnSet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => person.Pronouns = OrganizerCompanion.Core.Enums.Pronouns.HeHim);
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_BirthDate_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = person.BirthDate; });
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_BirthDate_ShouldThrowNotImplementedException_OnSet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => person.BirthDate = DateTime.Now);
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_DeceasedDate_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = person.DeceasedDate; });
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_DeceasedDate_ShouldThrowNotImplementedException_OnSet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => person.DeceasedDate = DateTime.Now);
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_JoinedDate_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = person.JoinedDate; });
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_JoinedDate_ShouldThrowNotImplementedException_OnSet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => person.JoinedDate = DateTime.Now);
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_Emails_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = person.Emails; });
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_Emails_ShouldThrowNotImplementedException_OnSet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => person.Emails = []);
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_PhoneNumbers_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = person.PhoneNumbers; });
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_PhoneNumbers_ShouldThrowNotImplementedException_OnSet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => person.PhoneNumbers = []);
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_Addresses_ShouldThrowNotImplementedException_OnGet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = person.Addresses; });
        }

        [Test, Category("DataTransferObjects")]
        public void IPerson_Addresses_ShouldThrowNotImplementedException_OnSet()
        {
            // Arrange
            OrganizerCompanion.Core.Interfaces.Type.IPerson person = _userDTO;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => person.Addresses = []);
        }

        #endregion

        #region Public Interface Implementation Tests

        [Test, Category("DataTransferObjects")]
        public void PublicIsCast_ShouldThrowNotImplementedException_OnGet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _userDTO.IsCast; });
        }

        [Test, Category("DataTransferObjects")]
        public void PublicIsCast_ShouldThrowNotImplementedException_OnSet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _userDTO.IsCast = true);
        }

        [Test, Category("DataTransferObjects")]
        public void PublicCastId_ShouldThrowNotImplementedException_OnGet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _userDTO.CastId; });
        }

        [Test, Category("DataTransferObjects")]
        public void PublicCastId_ShouldThrowNotImplementedException_OnSet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _userDTO.CastId = 123);
        }

        [Test, Category("DataTransferObjects")]
        public void PublicCastType_ShouldThrowNotImplementedException_OnGet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _userDTO.CastType; });
        }

        [Test, Category("DataTransferObjects")]
        public void PublicCastType_ShouldThrowNotImplementedException_OnSet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _userDTO.CastType = "TestType");
        }

        [Test, Category("DataTransferObjects")]
        public void PublicCast_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _userDTO.Cast<MockDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void PublicToJson_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _userDTO.ToJson());
        }

        #endregion

        #region JSON Serialization Attribute Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(UserDTO).GetProperty(nameof(UserDTO.Id));

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
        public void FirstName_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(UserDTO).GetProperty(nameof(UserDTO.FirstName));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("firstName"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void MiddleName_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(UserDTO).GetProperty(nameof(UserDTO.MiddleName));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("middleName"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void LastName_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(UserDTO).GetProperty(nameof(UserDTO.LastName));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("lastName"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void FullName_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(UserDTO).GetProperty(nameof(UserDTO.FullName));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("fullName"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(UserDTO).GetProperty(nameof(UserDTO.UserName));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("userName"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DeceasedDate_ShouldHaveJsonIgnoreWhenWritingNullAttribute()
        {
            // Arrange
            var property = typeof(UserDTO).GetProperty(nameof(UserDTO.DeceasedDate));

            // Act
            var jsonIgnoreAttributes = property?.GetCustomAttributes(typeof(JsonIgnoreAttribute), false)
                .Cast<JsonIgnoreAttribute>().ToList();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonIgnoreAttributes, Is.Not.Null);
                Assert.That(jsonIgnoreAttributes!, Is.Not.Empty);
                Assert.That(jsonIgnoreAttributes!.Any(attr => attr.Condition == JsonIgnoreCondition.WhenWritingNull), Is.True);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IsSuperUser_ShouldHaveJsonIgnoreWhenWritingNullAttribute()
        {
            // Arrange
            var property = typeof(UserDTO).GetProperty(nameof(UserDTO.IsSuperUser));

            // Act
            var jsonIgnoreAttributes = property?.GetCustomAttributes(typeof(JsonIgnoreAttribute), false)
                .Cast<JsonIgnoreAttribute>().ToList();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonIgnoreAttributes, Is.Not.Null);
                Assert.That(jsonIgnoreAttributes!, Is.Not.Empty);
                Assert.That(jsonIgnoreAttributes!.Any(attr => attr.Condition == JsonIgnoreCondition.WhenWritingNull), Is.True);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(UserDTO).GetProperty(nameof(UserDTO.DateCreated));

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
            var property = typeof(UserDTO).GetProperty(nameof(UserDTO.DateModified));

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
        public void RequiredProperties_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var requiredProperties = new[]
            {
                nameof(UserDTO.Id),
                nameof(UserDTO.FirstName),
                nameof(UserDTO.LastName),
                nameof(UserDTO.FullName),
                nameof(UserDTO.UserName),
                nameof(UserDTO.Emails),
                nameof(UserDTO.PhoneNumbers),
                nameof(UserDTO.Addresses),
                nameof(UserDTO.Pronouns),
                nameof(UserDTO.BirthDate),
                nameof(UserDTO.JoinedDate),
                nameof(UserDTO.IsActive),
                nameof(UserDTO.IsDeceased),
                nameof(UserDTO.IsAdmin),
                nameof(UserDTO.DateModified),
                nameof(UserDTO.DateCreated)
            };

            // Act & Assert
            foreach (var propertyName in requiredProperties)
            {
                var property = typeof(UserDTO).GetProperty(propertyName);
                var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false)
                    .FirstOrDefault() as RequiredAttribute;

                Assert.That(requiredAttribute, Is.Not.Null, $"Property {propertyName} should have Required attribute");
            }
        }

        [Test, Category("DataTransferObjects")]
        public void OptionalProperties_ShouldNotHaveRequiredAttribute()
        {
            // Arrange
            var optionalProperties = new[]
            {
                nameof(UserDTO.MiddleName),
                nameof(UserDTO.DeceasedDate),
                nameof(UserDTO.IsSuperUser)
            };

            // Act & Assert
            foreach (var propertyName in optionalProperties)
            {
                var property = typeof(UserDTO).GetProperty(propertyName);
                var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false)
                    .FirstOrDefault() as RequiredAttribute;

                Assert.That(requiredAttribute, Is.Null, $"Property {propertyName} should not have Required attribute");
            }
        }

        #endregion

        #region Edge Case Tests

        [Test, Category("DataTransferObjects")]
        public void Names_ShouldAcceptSpecialCharacters()
        {
            // Arrange & Act & Assert
            var namesWithSpecialChars = new[]
            {
                "O'Connor",
                "José María",
                "Anne-Marie",
                "McDowell",
                "de la Cruz"
            };

            foreach (var name in namesWithSpecialChars)
            {
                _userDTO.FirstName = name;
                Assert.That(_userDTO.FirstName, Is.EqualTo(name));

                _userDTO.LastName = name;
                Assert.That(_userDTO.LastName, Is.EqualTo(name));
            }
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldAcceptVariousFormats()
        {
            // Arrange & Act & Assert
            var userNameFormats = new[]
            {
                "john.doe",
                "john_doe",
                "johndoe123",
                "john-doe",
                "user@domain.com",
                "UPPERCASE_USER"
            };

            foreach (var userName in userNameFormats)
            {
                _userDTO.UserName = userName;
                Assert.That(_userDTO.UserName, Is.EqualTo(userName));
            }
        }

        [Test, Category("DataTransferObjects")]
        public void Collections_ShouldAcceptEmptyLists()
        {
            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _userDTO.Emails = []);
                Assert.DoesNotThrow(() => _userDTO.PhoneNumbers = []);
                Assert.DoesNotThrow(() => _userDTO.Addresses = []);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void BooleanProperties_ShouldAcceptAllStates()
        {
            // Arrange & Act & Assert
            var booleanProperties = new[]
            {
                (Action<bool?>)(value => _userDTO.IsActive = value),
                (Action<bool?>)(value => _userDTO.IsDeceased = value),
                (Action<bool?>)(value => _userDTO.IsAdmin = value),
                (Action<bool?>)(value => _userDTO.IsSuperUser = value)
            };

            foreach (var setter in booleanProperties)
            {
                Assert.DoesNotThrow(() => setter(true));
                Assert.DoesNotThrow(() => setter(false));
                Assert.DoesNotThrow(() => setter(null));
            }
        }

        [Test, Category("DataTransferObjects")]
        public void DateProperties_ShouldAcceptValidDateRanges()
        {
            // Arrange & Act & Assert
            var dates = new[]
            {
                DateTime.MinValue,
                new DateTime(1900, 1, 1),
                new DateTime(2000, 1, 1),
                DateTime.Now,
                DateTime.MaxValue
            };

            foreach (var date in dates)
            {
                Assert.DoesNotThrow(() => _userDTO.BirthDate = date);
                Assert.DoesNotThrow(() => _userDTO.DeceasedDate = date);
                Assert.DoesNotThrow(() => _userDTO.JoinedDate = date);
                Assert.DoesNotThrow(() => _userDTO.DateModified = date);
                Assert.DoesNotThrow(() => _userDTO.DateCreated = date);
            }
        }

        #endregion

        #region JSON Serialization Tests

        [Test, Category("DataTransferObjects")]
        public void UserDTO_ShouldSerializeWithCorrectPropertyNames()
        {
            // Arrange
            _userDTO.Id = 123;
            _userDTO.FirstName = "John";
            _userDTO.LastName = "Doe";
            _userDTO.UserName = "johndoe";
            _userDTO.IsActive = true;

            // Act
            var json = JsonSerializer.Serialize(_userDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(json, Contains.Substring("\"id\":123"));
                Assert.That(json, Contains.Substring("\"firstName\":\"John\""));
                Assert.That(json, Contains.Substring("\"lastName\":\"Doe\""));
                Assert.That(json, Contains.Substring("\"userName\":\"johndoe\""));
                Assert.That(json, Contains.Substring("\"isActive\":true"));
                Assert.That(json, Contains.Substring("\"dateCreated\":"));
                Assert.That(json, Contains.Substring("\"dateModified\":"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void UserDTO_ShouldSerializeWithNullValues()
        {
            // Arrange
            _userDTO.Id = 456;
            _userDTO.FirstName = null;
            _userDTO.MiddleName = null;
            _userDTO.LastName = null;
            _userDTO.DeceasedDate = null;
            _userDTO.IsSuperUser = null;

            // Act
            var json = JsonSerializer.Serialize(_userDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Contains.Substring("\"id\":456"));
                Assert.That(json, Contains.Substring("\"firstName\":null"));
                Assert.That(json, Contains.Substring("\"lastName\":null"));
                // DeceasedDate and IsSuperUser should be omitted when null due to JsonIgnoreCondition.WhenWritingNull
                Assert.That(json, Does.Not.Contain("\"deceasedDate\""));
                Assert.That(json, Does.Not.Contain("\"superUser\""));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void UserDTO_ShouldDeserializeCorrectly()
        {
            // Arrange
            const string json = "{\"id\":789,\"firstName\":\"Jane\",\"lastName\":\"Smith\",\"userName\":\"janesmith\",\"emails\":[],\"phoneNumbers\":[],\"addresses\":[],\"pronouns\":2,\"birthDate\":\"1990-05-15T00:00:00\",\"joinedDate\":\"2020-01-01T00:00:00\",\"isActive\":true,\"isDeceased\":false,\"isAdmin\":false,\"dateModified\":null,\"dateCreated\":\"2023-01-01T12:00:00\"}";

            // Act
            var userDTO = JsonSerializer.Deserialize<UserDTO>(json);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(userDTO, Is.Not.Null);
                Assert.That(userDTO!.Id, Is.EqualTo(789));
                Assert.That(userDTO.FirstName, Is.EqualTo("Jane"));
                Assert.That(userDTO.LastName, Is.EqualTo("Smith"));
                Assert.That(userDTO.UserName, Is.EqualTo("janesmith"));
                Assert.That(userDTO.IsActive, Is.True);
                Assert.That(userDTO.IsDeceased, Is.False);
                Assert.That(userDTO.IsAdmin, Is.False);
            });
        }

        #endregion

        #region Integration Tests

        [Test, Category("DataTransferObjects")]
        public void UserDTO_ShouldWorkWithComplexScenarios()
        {
            // Arrange
            _userDTO.Id = 999;
            _userDTO.FirstName = "Complex";
            _userDTO.MiddleName = "Test";
            _userDTO.LastName = "User";
            _userDTO.FullName = "Complex Test User";
            _userDTO.UserName = "complex.test.user";
            _userDTO.Pronouns = OrganizerCompanion.Core.Enums.Pronouns.TheyThem;
            _userDTO.BirthDate = new DateTime(1985, 6, 15);
            _userDTO.JoinedDate = new DateTime(2020, 3, 1);
            _userDTO.IsActive = true;
            _userDTO.IsAdmin = true;
            _userDTO.IsSuperUser = false;
            _userDTO.Emails =
            [
                new EmailDTO { Id = 1, EmailAddress = "complex@test.com" }
            ];

            // Act
            var json = JsonSerializer.Serialize(_userDTO);
            var deserializedUser = JsonSerializer.Deserialize<UserDTO>(json);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(deserializedUser, Is.Not.Null);
                Assert.That(deserializedUser!.Id, Is.EqualTo(999));
                Assert.That(deserializedUser.FullName, Is.EqualTo("Complex Test User"));
                Assert.That(deserializedUser.UserName, Is.EqualTo("complex.test.user"));
                Assert.That(deserializedUser.Pronouns, Is.EqualTo(OrganizerCompanion.Core.Enums.Pronouns.TheyThem));
                Assert.That(deserializedUser.IsAdmin, Is.True);
                Assert.That(deserializedUser.IsSuperUser, Is.False);
                Assert.That(deserializedUser.Emails, Has.Count.EqualTo(1));
            });
        }

        #endregion

        #region Mock Classes for Testing

        /// <summary>
        /// Mock implementation of IDomainEntity for testing purposes.
        /// </summary>
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; }
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
        /// Mock implementation of IAddressDTO for testing purposes.
        /// </summary>
        private class MockAddressDTO : IAddressDTO
        {
            public int Id { get; set; }
            public OrganizerCompanion.Core.Enums.Types? Type { get; set; }
            public int LinkedEntityId { get; set; }
            public IDomainEntity? LinkedEntity { get; set; }
            public string? LinkedEntityType { get; set; }
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

        #endregion
    }
}
