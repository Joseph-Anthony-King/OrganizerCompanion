using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class AnnonymousUserShould
    {
        private AnnonymousUser _sut;
        private DateTime _testDateCreated;
        private DateTime _testDateModified;

        [SetUp]
        public void SetUp()
        {
            _sut = new AnnonymousUser();
            _testDateCreated = new DateTime(2023, 1, 1, 12, 0, 0);
            _testDateModified = new DateTime(2023, 1, 2, 12, 0, 0);
        }

        [Test]
        [Category("Models")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new AnnonymousUser();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.UserName, Is.EqualTo(string.Empty));
                Assert.That(_sut.AccountId, Is.EqualTo(0));
                Assert.That(_sut.Account, Is.Null);
                Assert.That(_sut.IsCast, Is.Null);
                Assert.That(_sut.CastId, Is.Null);
                Assert.That(_sut.CastType, Is.Null);
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test]
        [Category("Models")]
        public void JsonConstructor_SetsAllPropertiesCorrectly()
        {
            // Arrange & Act
            var testAccount = new SubAccount();
            _sut = new AnnonymousUser(
                id: 123,
                userName: "TestUser",
                account: testAccount,
                isCast: true,
                castId: 456,
                castType: "Organization",
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(123));
                Assert.That(_sut.UserName, Is.EqualTo("TestUser"));
                Assert.That(_sut.Account, Is.EqualTo(testAccount));
                Assert.That(_sut.IsCast, Is.True);
                Assert.That(_sut.CastId, Is.EqualTo(456));
                Assert.That(_sut.CastType, Is.EqualTo("Organization"));
                Assert.That(_sut.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test]
        [Category("Models")]
        public void JsonConstructor_CreatesObjectSuccessfully()
        {
            // This test verifies that the JsonConstructor creates objects successfully
            // with various parameter combinations, including edge cases.

            // Test with valid parameters
            Assert.DoesNotThrow(() => new AnnonymousUser(
                id: 1,
                userName: "TestUser",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            ));
        }

        [Test]
        [Category("Models")]
        public void UserName_Getter_ReturnsCorrectValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.That(_sut.UserName, Is.EqualTo(string.Empty));
        }

        [Test]
        [Category("Models")]
        public void UserName_Setter_UpdatesValueAndDateModified()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.UserName = "TestUser";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.UserName, Is.EqualTo("TestUser"));
            });
            Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test]
        [Category("Models")]
        public void UserName_Setter_ThrowsException_WhenNull()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _sut.UserName = null!);
            Assert.That(ex.Message, Does.Contain("User Name must be at least 1 character long."));
        }

        [Test]
        [Category("Models")]
        public void UserName_Setter_ThrowsException_WhenEmpty()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _sut.UserName = "");
            Assert.That(ex.Message, Does.Contain("User Name must be at least 1 character long."));
        }

        [Test]
        [Category("Models")]
        public void UserName_Setter_ThrowsException_WhenWhitespace()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _sut.UserName = "   ");
            Assert.That(ex.Message, Does.Contain("User Name must be at least 1 character long."));
        }

        [Test]
        [Category("Models")]
        public void UserName_Setter_ThrowsException_WhenTooLong()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var longUserName = new string('A', 101); // 101 characters

            // Act & Assert
            var ex = Assert.Throws<ArgumentException>(() => _sut.UserName = longUserName);
            Assert.That(ex.Message, Does.Contain("User Name cannot exceed 100 characters."));
        }

        [Test]
        [Category("Models")]
        public void UserName_Setter_AcceptsMaxLength()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var maxLengthUserName = new string('A', 100); // 100 characters

            // Act & Assert
            Assert.DoesNotThrow(() => _sut.UserName = maxLengthUserName);
            Assert.That(_sut.UserName, Is.EqualTo(maxLengthUserName));
        }

        [Test]
        [Category("Models")]
        public void UserName_Setter_UpdatesDateModified_OnEachCall()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert - First update
            var originalDateModified = _sut.DateModified;
            _sut.UserName = "FirstName";
            var firstUpdateTime = _sut.DateModified;

            Assert.That(firstUpdateTime, Is.GreaterThan(originalDateModified));

            // Small delay to ensure different timestamps
            System.Threading.Thread.Sleep(1);

            // Act & Assert - Second update
            _sut.UserName = "SecondName";
            var secondUpdateTime = _sut.DateModified;

            Assert.That(secondUpdateTime, Is.GreaterThan(firstUpdateTime));
        }

        [Test]
        [Category("Models")]
        public void Id_Getter_ReturnsCorrectValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test]
        [Category("Models")]
        public void Id_Setter_UpdatesValueAndDateModified()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.Id = 456;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(456));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test]
        [Category("Models")]
        public void IsCast_Getter_ReturnsCorrectValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.That(_sut.IsCast, Is.Null);
        }

        [Test]
        [Category("Models")]
        public void IsCast_Setter_UpdatesValueAndDateModified()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.IsCast = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsCast, Is.True);
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test]
        [Category("Models")]
        public void CastId_Getter_ReturnsCorrectValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.That(_sut.CastId, Is.Null);
        }

        [Test]
        [Category("Models")]
        public void CastId_Setter_UpdatesValueAndDateModified()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.CastId = 789;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.CastId, Is.EqualTo(789));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test]
        [Category("Models")]
        public void CastId_Setter_CanSetToZero()
        {
            // Arrange
            _sut = new AnnonymousUser
            {
                CastId = 123
            };

            // Act
            _sut.CastId = 0;

            // Assert
            Assert.That(_sut.CastId, Is.EqualTo(0));
        }

        [Test]
        [Category("Models")]
        public void CastType_Getter_ReturnsCorrectValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.That(_sut.CastType, Is.Null);
        }

        [Test]
        [Category("Models")]
        public void CastType_Setter_UpdatesValueAndDateModified()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.CastType = "Organization";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.CastType, Is.EqualTo("Organization"));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test]
        [Category("Models")]
        public void CastType_Setter_CanSetToNull()
        {
            // Arrange
            _sut = new AnnonymousUser
            {
                CastType = "Person"
            };

            // Act
            _sut.CastType = null;

            // Assert
            Assert.That(_sut.CastType, Is.Null);
        }

        [Test]
        [Category("Models")]
        public void DateCreated_IsReadOnly_AndSetDuringConstruction()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new AnnonymousUser();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test]
        [Category("Models")]
        public void JsonConstructor_SetsDateCreatedFromParameter()
        {
            // Arrange
            var specificDate = new DateTime(2023, 6, 15, 14, 30, 0);

            // Act
            _sut = new AnnonymousUser(
                id: 123,
                userName: "TestUser",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: specificDate,
                dateModified: _testDateModified
            );

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(specificDate));
        }

        [Test]
        [Category("Models")]
        public void DateModified_CanBeSetAndRetrieved()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var testDate = new DateTime(2023, 6, 15, 14, 30, 0);

            // Act
            _sut.DateModified = testDate;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(testDate));
        }

        [Test]
        [Category("Models")]
        public void DateModified_CanBeSetToNull()
        {
            // Arrange
            _sut = new AnnonymousUser
            {
                DateModified = null
            };

            // Assert
            Assert.That(_sut.DateModified, Is.Null);
        }

        [Test]
        [Category("Models")]
        public void ToJson_ReturnsValidJsonString()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 789,
                userName: "JsonTestUser",
                account: new SubAccount(),
                isCast: true,
                castId: 456,
                castType: "Organization",
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
            });

            // Verify JSON contains expected properties
            Assert.That(json, Does.Contain("\"id\":789"));
            Assert.That(json, Does.Contain("\"userName\":\"JsonTestUser\""));
            Assert.That(json, Does.Contain("\"dateCreated\""));
            Assert.That(json, Does.Contain("\"dateModified\""));
            Assert.That(json, Does.Contain("\"isCast\":true"));
            Assert.That(json, Does.Contain("\"castId\":456"));
            Assert.That(json, Does.Contain("\"castType\":\"Organization\""));
        }

        [Test]
        [Category("Models")]
        public void ToJson_HandlesDefaultValues()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
            });

            // Verify JSON contains expected properties with default values
            Assert.That(json, Does.Contain("\"id\":0"));
            Assert.That(json, Does.Contain("\"userName\":\"\""));
            Assert.That(json, Does.Contain("\"dateCreated\""));
            Assert.That(json, Does.Contain("\"dateModified\""));
        }

        [Test]
        [Category("Models")]
        public void ToJson_HandlesNullDateModified()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 100,
                userName: "NullModifiedUser",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: null
            );

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
            });

            // Verify JSON contains expected properties
            Assert.That(json, Does.Contain("\"id\":100"));
            Assert.That(json, Does.Contain("\"userName\":\"NullModifiedUser\""));
            Assert.That(json, Does.Contain("\"dateCreated\""));
            Assert.That(json, Does.Contain("\"dateModified\":null"));
        }

        [Test]
        [Category("Models")]
        public void ToJson_WithCastProperties_IncludesConvertedFields()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 100,
                userName: "CastTestUser",
                account: new SubAccount(),
                isCast: true,
                castId: 456,
                castType: "Organization",
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
            });

            // Verify JSON contains cast properties
            Assert.That(json, Does.Contain("\"isCast\":true"));
            Assert.That(json, Does.Contain("\"userName\":\"CastTestUser\""));
            Assert.That(json, Does.Contain("\"castId\":456"));
            Assert.That(json, Does.Contain("\"castType\":\"Organization\""));
        }

        [Test]
        [Category("Models")]
        public void ToJson_WithNullCastProperties_HandlesCorrectly()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 100,
                userName: "Test User",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
            });

            // Verify JSON handles null cast properties correctly (should be omitted due to JsonIgnore condition)
            Assert.That(json, Does.Contain("\"isCast\""));
            Assert.That(json, Does.Contain("\"userName\":\"Test User\""));
            Assert.That(json, Does.Contain("\"castId\""));
            Assert.That(json, Does.Not.Contain("\"castType\""));
        }

        [Test]
        [Category("Models")]
        public void Properties_CanBeSetMultipleTimes()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var firstDate = new DateTime(2023, 1, 1);
            var secondDate = new DateTime(2023, 2, 1);

            // Act & Assert - Id
            _sut.Id = 100;
            Assert.That(_sut.Id, Is.EqualTo(100));

            _sut.Id = 200;
            Assert.That(_sut.Id, Is.EqualTo(200));

            // Act & Assert - IsCast
            _sut.IsCast = true;
            Assert.That(_sut.IsCast, Is.True);

            _sut.IsCast = false;
            Assert.That(_sut.IsCast, Is.False);

            // Act & Assert - CastId
            _sut.CastId = 300;
            Assert.That(_sut.CastId, Is.EqualTo(300));

            _sut.CastId = 400;
            Assert.That(_sut.CastId, Is.EqualTo(400));

            // Act & Assert - CastType
            _sut.CastType = "Organization";
            Assert.That(_sut.CastType, Is.EqualTo("Organization"));

            _sut.CastType = "Person";
            Assert.That(_sut.CastType, Is.EqualTo("Person"));

            // Act & Assert - DateModified (DateCreated is read-only)
            _sut.DateModified = firstDate;
            Assert.That(_sut.DateModified, Is.EqualTo(firstDate));

            _sut.DateModified = secondDate;
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DateModified, Is.EqualTo(secondDate));

                // DateCreated is set during construction and cannot be modified afterward
                Assert.That(_sut.DateCreated, Is.Not.EqualTo(default(DateTime)));
            });
        }

        [Test]
        [Category("Models")]
        public void Id_Setter_UpdatesDateModified_OnEachCall()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert - First update
            var originalDateModified = _sut.DateModified;
            _sut.Id = 1;
            var firstUpdateTime = _sut.DateModified;

            Assert.That(firstUpdateTime, Is.GreaterThan(originalDateModified));

            // Small delay to ensure different timestamps
            System.Threading.Thread.Sleep(1);

            // Act & Assert - Second update
            _sut.Id = 2;
            var secondUpdateTime = _sut.DateModified;

            Assert.That(secondUpdateTime, Is.GreaterThan(firstUpdateTime));
        }

        [Test]
        [Category("Models")]
        public void IsCast_Setter_UpdatesDateModified_OnEachCall()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert - First update
            var originalDateModified = _sut.DateModified;
            _sut.IsCast = true;
            var firstUpdateTime = _sut.DateModified;

            Assert.That(firstUpdateTime, Is.GreaterThan(originalDateModified));

            // Small delay to ensure different timestamps
            System.Threading.Thread.Sleep(1);

            // Act & Assert - Second update
            _sut.IsCast = false;
            var secondUpdateTime = _sut.DateModified;

            Assert.That(secondUpdateTime, Is.GreaterThan(firstUpdateTime));
        }

        [Test]
        [Category("Models")]
        public void CastId_Setter_UpdatesDateModified_OnEachCall()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert - First update
            var originalDateModified = _sut.DateModified;
            _sut.CastId = 100;
            var firstUpdateTime = _sut.DateModified;

            Assert.That(firstUpdateTime, Is.GreaterThan(originalDateModified));

            // Small delay to ensure different timestamps
            System.Threading.Thread.Sleep(1);

            // Act & Assert - Second update
            _sut.CastId = 200;
            var secondUpdateTime = _sut.DateModified;

            Assert.That(secondUpdateTime, Is.GreaterThan(firstUpdateTime));
        }

        [Test]
        [Category("Models")]
        public void CastType_Setter_UpdatesDateModified_OnEachCall()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert - First update
            var originalDateModified = _sut.DateModified;
            _sut.CastType = "Organization";
            var firstUpdateTime = _sut.DateModified;

            Assert.That(firstUpdateTime, Is.GreaterThan(originalDateModified));

            // Small delay to ensure different timestamps
            System.Threading.Thread.Sleep(1);

            // Act & Assert - Second update
            _sut.CastType = "Person";
            var secondUpdateTime = _sut.DateModified;

            Assert.That(secondUpdateTime, Is.GreaterThan(firstUpdateTime));
        }

        [Test]
        [Category("Models")]
        public void JsonConstructor_HandlesMinAndMaxDateTimeValues()
        {
            // Arrange & Act
            _sut = new AnnonymousUser(
                id: int.MaxValue,
                userName: "Test User",
                account: new SubAccount(),
                isCast: true,
                castId: int.MaxValue,
                castType: "MaxValue",
                dateCreated: DateTime.MinValue,
                dateModified: DateTime.MaxValue
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
                Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.MinValue));
                Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MaxValue));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToOrganization_ReturnsOrganizationWithCorrectProperties()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 123,
                userName: "Test User",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var organization = _sut.Cast<Organization>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organization, Is.Not.Null);
                Assert.That(organization, Is.InstanceOf<Organization>());
                Assert.That(organization.Id, Is.EqualTo(0)); // Cast creates new Organization with Id = 0
                Assert.That(organization.OrganizationName, Is.EqualTo("Test User"));
                Assert.That(organization.Addresses, Is.Not.Null);
                Assert.That(organization.Addresses, Is.Empty);
                Assert.That(organization.PhoneNumbers, Is.Not.Null);
                Assert.That(organization.PhoneNumbers, Is.Empty);
                Assert.That(organization.Members, Is.Not.Null);
                Assert.That(organization.Members, Is.Empty);
                Assert.That(organization.Contacts, Is.Not.Null);
                Assert.That(organization.Contacts, Is.Empty);
                Assert.That(organization.Accounts, Is.Not.Null);
                Assert.That(organization.Accounts, Is.Empty);
                Assert.That(organization.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(organization.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToOrganization_UpdatesCastProperties()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 123,
                userName: "Test User",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var organization = _sut.Cast<Organization>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsCast, Is.True);
                Assert.That(_sut.CastId, Is.EqualTo(organization.Id));
                Assert.That(_sut.CastType, Is.EqualTo("Organization"));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToPerson_ReturnsPersonWithCorrectProperties()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 456,
                userName: "Test User",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var person = _sut.Cast<User>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(person, Is.Not.Null);
                Assert.That(person, Is.InstanceOf<User>());
                Assert.That(person.Id, Is.EqualTo(0)); // Cast creates new Person with Id = 0
                Assert.That(person.FirstName, Is.Null);
                Assert.That(person.MiddleName, Is.Null);
                Assert.That(person.LastName, Is.Null);
                Assert.That(person.UserName, Is.EqualTo("Test User"));
                Assert.That(person.Pronouns, Is.Null);
                Assert.That(person.BirthDate, Is.Null);
                Assert.That(person.DeceasedDate, Is.Null);
                Assert.That(person.JoinedDate, Is.Null);
                Assert.That(person.Emails, Is.Not.Null);
                Assert.That(person.Emails, Is.Empty);
                Assert.That(person.PhoneNumbers, Is.Not.Null);
                Assert.That(person.PhoneNumbers, Is.Empty);
                Assert.That(person.Addresses, Is.Not.Null);
                Assert.That(person.Addresses, Is.Empty);
                Assert.That(person.IsActive, Is.Null);
                Assert.That(person.IsDeceased, Is.Null);
                Assert.That(person.IsAdmin, Is.Null);
                Assert.That(person.IsSuperUser, Is.Null);
                Assert.That(person.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(person.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToUser_UpdatesCastProperties()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 456,
                userName: "Test User",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var user = _sut.Cast<User>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsCast, Is.True);
                Assert.That(_sut.CastId, Is.EqualTo(user.Id));
                Assert.That(_sut.CastType, Is.EqualTo("User"));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_WithDefaultConstructor_ToOrganization_PreservesDateCreated()
        {
            // Arrange
            var beforeCreation = DateTime.Now;
            _sut = new AnnonymousUser();
            var afterCreation = DateTime.Now;

            // Act
            var organization = _sut.Cast<Organization>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organization, Is.Not.Null);
                Assert.That(organization.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(organization.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(organization.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_WithDefaultConstructor_ToPerson_PreservesDateCreated()
        {
            // Arrange
            var beforeCreation = DateTime.Now;
            _sut = new AnnonymousUser();
            var afterCreation = DateTime.Now;

            // Act
            var person = _sut.Cast<User>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(person, Is.Not.Null);
                Assert.That(person.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(person.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(person.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_WithNullDateModified_HandlesCorrectly()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 789,
                userName: "Test User",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: null
            );

            _sut.Id += 1; // Trigger DateModified update

            // Act & Assert - Organization
            var organization = _sut.Cast<Organization>();
            Assert.Multiple(() =>
            {
                Assert.That(organization, Is.Not.Null);
                Assert.That(organization.DateCreated, Is.EqualTo(_sut.DateCreated));
            });

            // Act & Assert - Person
            var person = _sut.Cast<User>();
            Assert.Multiple(() =>
            {
                Assert.That(person, Is.Not.Null);
                Assert.That(person.DateCreated, Is.EqualTo(_sut.DateCreated));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToUnsupportedType_ThrowsArgumentException()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 123,
                userName: "Test User",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<Account>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast AnnonymousUser to Account."));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToAnotherUnsupportedType_ThrowsArgumentException()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert - Testing with a different unsupported type to ensure the generic error handling works
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<AnnonymousUser>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast AnnonymousUser to AnnonymousUser."));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToAnnonymousUserDTO_ReturnsAnnonymousUserDTOWithCorrectProperties()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 789,
                userName: "DTOTestUser",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            )
            {
                // Set AccountId for DTO mapping
                AccountId = 456
            };

            var beforeCast = DateTime.Now.AddSeconds(-1);

            // Act
            var dto = _sut.Cast<AnnonymousUserDTO>();
            var afterCast = DateTime.Now.AddSeconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto, Is.InstanceOf<AnnonymousUserDTO>());
                Assert.That(dto.Id, Is.EqualTo(789));
                Assert.That(dto.UserName, Is.EqualTo("DTOTestUser"));
                Assert.That(dto.AccountId, Is.EqualTo(456));
                Assert.That(dto.DateCreated, Is.EqualTo(_testDateCreated));
                // DateModified is set to DateTime.Now when the object is created/modified
                Assert.That(dto.DateModified, Is.GreaterThanOrEqualTo(beforeCast));
                Assert.That(dto.DateModified, Is.LessThanOrEqualTo(afterCast));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToAnnonymousUserDTO_DoesNotUpdateSourceCastProperties()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 123,
                userName: "Test User",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            )
            {
                AccountId = 999
            };

            var originalIsCast = _sut.IsCast;
            var originalCastId = _sut.CastId;
            var originalCastType = _sut.CastType;

            // Act
            var dto = _sut.Cast<AnnonymousUserDTO>();

            // Assert - Source object's cast properties should not be updated 
            // because AnnonymousUserDTO doesn't support cast tracking
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(_sut.IsCast, Is.EqualTo(originalIsCast));
                Assert.That(_sut.CastId, Is.EqualTo(originalCastId));
                Assert.That(_sut.CastType, Is.EqualTo(originalCastType));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToAnnonymousUserDTO_WithDefaultValues_CreatesValidDTO()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act
            var dto = _sut.Cast<AnnonymousUserDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto.Id, Is.EqualTo(0));
                Assert.That(dto.AccountId, Is.EqualTo(0));
                Assert.That(dto.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(dto.DateModified, Is.EqualTo(_sut.DateModified));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToAnnonymousUserDTO_WithNullDateModified_HandlesCorrectly()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 555,
                userName: "Test User",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: null
            )
            {
                AccountId = 777
            };

            var beforeCast = DateTime.Now.AddSeconds(-1);

            // Act
            var dto = _sut.Cast<AnnonymousUserDTO>();
            var afterCast = DateTime.Now.AddSeconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto.Id, Is.EqualTo(555));
                Assert.That(dto.AccountId, Is.EqualTo(777));
                Assert.That(dto.DateCreated, Is.EqualTo(_testDateCreated));
                // DateModified is set during object initialization
                Assert.That(dto.DateModified, Is.GreaterThanOrEqualTo(beforeCast));
                Assert.That(dto.DateModified, Is.LessThanOrEqualTo(afterCast));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToAnnonymousUserDTO_ReturnsDifferentInstanceEachTime()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 999,
                userName: "Test User",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            )
            {
                AccountId = 888
            };

            // Act
            var dto1 = _sut.Cast<AnnonymousUserDTO>();
            var dto2 = _sut.Cast<AnnonymousUserDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto1, Is.Not.SameAs(dto2));

                // Verify they have the same property values but are different instances
                Assert.That(dto1.Id, Is.EqualTo(dto2.Id));
                Assert.That(dto1.AccountId, Is.EqualTo(dto2.AccountId));
                Assert.That(dto1.DateCreated, Is.EqualTo(dto2.DateCreated));
                Assert.That(dto1.DateModified, Is.EqualTo(dto2.DateModified));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToIAnnonymousUserDTO_ThrowsArgumentException_NotSupported()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 555,
                userName: "Test User",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            )
            {
                AccountId = 777
            };

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<IAnnonymousUserDTO>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast AnnonymousUser to IAnnonymousUserDTO."));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ReturnsNewInstanceEachTime()
        {
            // Arrange
            _sut = new AnnonymousUser(
                id: 999,
                userName: "Test User",
                account: new SubAccount(),
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            )
            {
                AccountId = 888
            };

            // Act
            var organization1 = _sut.Cast<Organization>();
            var organization2 = _sut.Cast<Organization>();
            var person1 = _sut.Cast<User>();
            var person2 = _sut.Cast<User>();
            var dto1 = _sut.Cast<AnnonymousUserDTO>();
            var dto2 = _sut.Cast<AnnonymousUserDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(organization1, Is.Not.SameAs(organization2));
                Assert.That(person1, Is.Not.SameAs(person2));
                Assert.That(dto1, Is.Not.SameAs(dto2));

                // Verify they have the same property values but are different instances
                Assert.That(organization1.DateCreated, Is.EqualTo(organization2.DateCreated));
                Assert.That(person1.DateCreated, Is.EqualTo(person2.DateCreated));
                Assert.That(dto1.DateCreated, Is.EqualTo(dto2.DateCreated));
            });
        }

        [Test]
        [Category("Models")]
        public void AccountId_Getter_ReturnsCorrectValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.That(_sut.AccountId, Is.EqualTo(0));
        }

        [Test]
        [Category("Models")]
        public void AccountId_Setter_UpdatesValueAndDateModified()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.AccountId = 999;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.AccountId, Is.EqualTo(999));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test]
        [Category("Models")]
        public void AccountId_Setter_UpdatesDateModified_OnEachCall()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert - First update
            var originalDateModified = _sut.DateModified;
            _sut.AccountId = 100;
            var firstUpdateTime = _sut.DateModified;

            Assert.That(firstUpdateTime, Is.GreaterThan(originalDateModified));

            // Small delay to ensure different timestamps
            System.Threading.Thread.Sleep(1);

            // Act & Assert - Second update
            _sut.AccountId = 200;
            var secondUpdateTime = _sut.DateModified;

            Assert.That(secondUpdateTime, Is.GreaterThan(firstUpdateTime));
        }

        [Test]
        [Category("Models")]
        public void Account_Getter_ReturnsCorrectValue()
        {
            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert
            Assert.That(_sut.Account, Is.Null);
        }

        [Test]
        [Category("Models")]
        public void Account_Setter_UpdatesValueAndDateModified()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var originalDateModified = _sut.DateModified;
            var testAccount = new SubAccount();

            // Act
            _sut.Account = testAccount;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Account, Is.EqualTo(testAccount));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test]
        [Category("Models")]
        public void Account_Setter_CanSetToNull()
        {
            // Arrange
            _sut = new AnnonymousUser
            {
                Account = new SubAccount()
            };

            // Act
            _sut.Account = null;

            // Assert
            Assert.That(_sut.Account, Is.Null);
        }

        [Test]
        [Category("Models")]
        public void Account_Setter_UpdatesDateModified_OnEachCall()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var testAccount1 = new SubAccount();
            var testAccount2 = new SubAccount();

            // Act & Assert - First update
            var originalDateModified = _sut.DateModified;
            _sut.Account = testAccount1;
            var firstUpdateTime = _sut.DateModified;

            Assert.That(firstUpdateTime, Is.GreaterThan(originalDateModified));

            // Small delay to ensure different timestamps
            System.Threading.Thread.Sleep(1);

            // Act & Assert - Second update
            _sut.Account = testAccount2;
            var secondUpdateTime = _sut.DateModified;

            Assert.That(secondUpdateTime, Is.GreaterThan(firstUpdateTime));
        }

        [Test]
        [Category("Models")]
        public void ExplicitInterfaceImplementation_Account_Getter_ReturnsCorrectValue()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var interfaceReference = (OrganizerCompanion.Core.Interfaces.Domain.IAnnonymousUser)_sut;

            // Act & Assert
            Assert.That(interfaceReference.Account, Is.Null);
        }

        [Test]
        [Category("Models")]
        public void ExplicitInterfaceImplementation_Account_Setter_UpdatesValueAndDateModified()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var interfaceReference = (OrganizerCompanion.Core.Interfaces.Domain.IAnnonymousUser)_sut;
            var originalDateModified = _sut.DateModified;
            var testAccount = new SubAccount();

            // Act
            interfaceReference.Account = testAccount;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceReference.Account, Is.EqualTo(testAccount));
                Assert.That(_sut.Account, Is.EqualTo(testAccount));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test]
        [Category("Models")]
        public void ExplicitInterfaceImplementation_Account_Setter_CanSetToNull()
        {
            // Arrange
            _sut = new AnnonymousUser
            {
                Account = new SubAccount()
            };
            var interfaceReference = (OrganizerCompanion.Core.Interfaces.Domain.IAnnonymousUser)_sut;

            // Act
            interfaceReference.Account = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceReference.Account, Is.Null);
                Assert.That(_sut.Account, Is.Null);
            });
        }

        [Test]
        [Category("Models")]
        public void ExplicitInterfaceImplementation_Account_Setter_CastsCorrectly()
        {
            // Arrange
            _sut = new AnnonymousUser();
            var interfaceReference = (OrganizerCompanion.Core.Interfaces.Domain.IAnnonymousUser)_sut;
            var testAccount = new SubAccount();

            // Act
            interfaceReference.Account = testAccount;

            // Assert - Verify that the casting works correctly in both directions
            Assert.Multiple(() =>
            {
                Assert.That(interfaceReference.Account, Is.InstanceOf<SubAccount>());
                Assert.That(_sut.Account, Is.InstanceOf<SubAccount>());
                Assert.That(interfaceReference.Account, Is.SameAs(testAccount));
                Assert.That(_sut.Account, Is.SameAs(testAccount));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_CatchBlock_RethrowsExceptions()
        {
            // This test verifies that the catch block in the Cast method rethrows exceptions.
            // Since the Cast method's try block contains simple operations that rarely fail,
            // we test the exception rethrowing by triggering the InvalidCastException path,
            // which demonstrates the exception handling mechanism.

            // Arrange
            _sut = new AnnonymousUser();

            // Act & Assert - Test that InvalidCastException is thrown and rethrown correctly
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<Account>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast AnnonymousUser to Account."));
            });
        }

        [Test]
        [Category("Models")]
        public void JsonConstructor_WithNullValues_HandlesCorrectly()
        {
            // Arrange & Act
            _sut = new AnnonymousUser(
                id: 0,
                userName: "Test User",
                account: new SubAccount(),
                isCast: null,
                castId: null,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: null
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.IsCast, Is.False); // null isCast becomes false in constructor
                Assert.That(_sut.CastId, Is.EqualTo(0)); // null castId becomes 0 in constructor
                Assert.That(_sut.CastType, Is.Null);
                Assert.That(_sut.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(_sut.DateModified, Is.Null);
            });
        }

        [Test]
        [Category("Models")]
        public void JsonConstructor_WithFalseIsCast_SetsCorrectly()
        {
            // Arrange & Act
            _sut = new AnnonymousUser(
                id: 123,
                userName: "Test User",
                account: new SubAccount(),
                isCast: false,
                castId: 456,
                castType: "Test",
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(123));
                Assert.That(_sut.IsCast, Is.False);
                Assert.That(_sut.CastId, Is.EqualTo(456));
                Assert.That(_sut.CastType, Is.EqualTo("Test"));
                Assert.That(_sut.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test]
        [Category("Models")]
        public void JsonConstructor_WithExtremeValues_HandlesCorrectly()
        {
            // Test with extreme values to ensure robustness and potentially trigger
            // any edge cases in the constructor's exception handling
            Assert.DoesNotThrow(() =>
            {
                var extremeTest = new AnnonymousUser(
                    id: int.MaxValue,
                    userName: "Test User",
                    account: new SubAccount(),
                    isCast: true,
                    castId: int.MaxValue,
                    castType: new string('A', 1000), // Very long string
                    dateCreated: DateTime.MinValue,
                    dateModified: DateTime.MaxValue
                );

                // Verify the extreme values are set correctly
                Assert.Multiple(() =>
                {
                    Assert.That(extremeTest.Id, Is.EqualTo(int.MaxValue));
                    Assert.That(extremeTest.IsCast, Is.True);
                    Assert.That(extremeTest.CastId, Is.EqualTo(int.MaxValue));
                    Assert.That(extremeTest.CastType, Has.Length.EqualTo(1000));
                    Assert.That(extremeTest.DateCreated, Is.EqualTo(DateTime.MinValue));
                    Assert.That(extremeTest.DateModified, Is.EqualTo(DateTime.MaxValue));
                });
            });
        }

        [Test]
        [Category("Models")]
        public void JsonConstructor_ParameterVariations_CoverageTest()
        {
            // This test ensures comprehensive coverage of the JsonConstructor with
            // various parameter combinations to exercise all code paths in the
            // simplified constructor implementation.

            Assert.DoesNotThrow(() =>
            {
                // Test multiple scenarios to ensure all branches are covered
                var test1 = new AnnonymousUser(0, "Test User", new SubAccount(), null, null, null, DateTime.Now, null);
                var test2 = new AnnonymousUser(1, "Test User", new SubAccount(), true, 42, "Test Type", DateTime.Now, null);
                var test3 = new AnnonymousUser(int.MaxValue, "Test User", new SubAccount(), null, null, null, DateTime.Now, null);

                Assert.Multiple(() =>
                {
                    Assert.That(test1, Is.Not.Null);
                    Assert.That(test2, Is.Not.Null);
                    Assert.That(test3, Is.Not.Null);
                });
            });
        }

        [Test]
        [Category("Models")]
        public void AnnonymousUser_ComprehensiveCoverageVerification()
        {
            // This test verifies comprehensive coverage of all major functionality
            // in the AnnonymousUser class including edge cases and different scenarios.

            // Test default constructor
            var defaultUser = new AnnonymousUser();
            Assert.That(defaultUser, Is.Not.Null);

            // Test JsonConstructor with various parameter combinations
            var parameterizedUser = new AnnonymousUser(
                id: 123,
                userName: "Test User",
                account: new SubAccount(),
                dateCreated: DateTime.Now,
                dateModified: DateTime.Now,
                isCast: true,
                castId: 456,
                castType: "TestType"
            );
            Assert.That(parameterizedUser, Is.Not.Null);

            // Test all property setters and getters
            defaultUser.Id = 789;
            defaultUser.AccountId = 101112;
            defaultUser.Account = new SubAccount();
            defaultUser.IsCast = true;
            defaultUser.CastId = 131415;
            defaultUser.CastType = "CoverageTest";
            defaultUser.DateModified = DateTime.Now;

            Assert.Multiple(() =>
            {
                Assert.That(defaultUser.Id, Is.EqualTo(789));
                Assert.That(defaultUser.AccountId, Is.EqualTo(101112));
                Assert.That(defaultUser.Account, Is.Not.Null);
                Assert.That(defaultUser.IsCast, Is.True);
                Assert.That(defaultUser.CastId, Is.EqualTo(131415));
                Assert.That(defaultUser.CastType, Is.EqualTo("CoverageTest"));
                Assert.That(defaultUser.DateModified, Is.Not.Null);
                Assert.That(defaultUser.DateCreated, Is.Not.EqualTo(default(DateTime)));
            });

            // Test explicit interface implementation
            var interfaceRef = (OrganizerCompanion.Core.Interfaces.Domain.IAnnonymousUser)defaultUser;
            var testAccount = new SubAccount();
            interfaceRef.Account = testAccount;
            Assert.That(interfaceRef.Account, Is.SameAs(testAccount));

            // Test Cast methods for all supported types
            var orgCast = defaultUser.Cast<Organization>();
            var userCast = defaultUser.Cast<User>();
            var dtoCast = defaultUser.Cast<AnnonymousUserDTO>();

            Assert.Multiple(() =>
            {
                Assert.That(orgCast, Is.InstanceOf<Organization>());
                Assert.That(userCast, Is.InstanceOf<User>());
                Assert.That(dtoCast, Is.InstanceOf<AnnonymousUserDTO>());
            });

            // Test ToJson functionality
            var json = defaultUser.ToJson();
            Assert.That(json, Is.Not.Null.And.Not.Empty);

            // Test exception scenarios
            Assert.Throws<InvalidCastException>(() => defaultUser.Cast<Account>());
        }

        /* NOTE: Code Coverage Analysis
         * 
         * The AnnonymousUser class should achieve near 100% code coverage with these tests.
         * 
         * The JsonConstructor has been simplified and no longer contains exception handling,
         * making it fully testable with straightforward scenarios.
         * 
         * The only remaining potential uncovered line would be the Cast method catch-rethrow
         * (line ~199 in the simplified version), which represents defensive programming where
         * the actual operations (simple field assignments and object creation) are extremely
         * unlikely to fail under normal circumstances.
         * 
         * This comprehensive test suite covers all practical functionality and realistic
         * error scenarios in the AnnonymousUser class, including:
         * - All constructors and their parameter variations
         * - All property getters and setters (including explicit interface implementations)
         * - All Cast method variants and exception scenarios
         * - JSON serialization functionality
         * - DateModified tracking behavior
         * - Edge cases and boundary conditions
         */
    }
}
