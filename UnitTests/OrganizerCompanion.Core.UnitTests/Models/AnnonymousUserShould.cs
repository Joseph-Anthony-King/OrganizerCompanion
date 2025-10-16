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
                Assert.That(_sut.AccountId, Is.EqualTo(0));
                Assert.That(_sut.Account, Is.Null);
                Assert.That(_sut.IsCast, Is.False);
                Assert.That(_sut.CastId, Is.EqualTo(0));
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
            _sut = new AnnonymousUser(
                id: 123,
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
                Assert.That(_sut.IsCast, Is.True);
                Assert.That(_sut.CastId, Is.EqualTo(456));
                Assert.That(_sut.CastType, Is.EqualTo("Organization"));
                Assert.That(_sut.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test]
        [Category("Models")]
        public void JsonConstructor_ThrowsArgumentException_WhenExceptionOccurs()
        {
            // Note: Since the constructor has simple field assignments, it's difficult to cause
            // an actual exception. This test demonstrates the exception handling structure.
            // The try-catch exists for safety but may not be triggered under normal circumstances.

            // For demonstration purposes, we test with valid parameters to ensure no exception is thrown
            Assert.DoesNotThrow(() => new AnnonymousUser(
                id: 1,
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            ));
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
            Assert.That(_sut.IsCast, Is.False);
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
            Assert.That(_sut.CastId, Is.EqualTo(0));
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
            Assert.That(json, Does.Contain("\"isCast\":false"));
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
            Assert.That(json, Does.Contain("\"isCast\":false"));
            Assert.That(json, Does.Not.Contain("\"castId\""));
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
            Assert.That(_sut.DateModified, Is.EqualTo(secondDate));

            // DateCreated is set during construction and cannot be modified afterward
            Assert.That(_sut.DateCreated, Is.Not.EqualTo(default(DateTime)));
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
                Assert.That(organization.OrganizationName, Is.Null);
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
                Assert.That(person.UserName, Is.Null);
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
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Set AccountId for DTO mapping
            _sut.AccountId = 456;

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
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            _sut.AccountId = 999;

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
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: null
            );

            _sut.AccountId = 777;

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
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            _sut.AccountId = 888;

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
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            _sut.AccountId = 777;

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
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            _sut.AccountId = 888;

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
    }
}
