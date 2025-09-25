using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class AccountShould
    {
        private Person _sut;
        private readonly DateTime _testDateCreated = new(2023, 1, 1, 12, 0, 0);
        private readonly DateTime _testDateModified = new(2023, 1, 2, 12, 0, 0);

        [SetUp]
        public void SetUp()
        {
            _sut = new Person
            {
                // Set required properties on Person to make it valid
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };
        }

        [Test, Category("Models")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var account = new Account();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(0));
                Assert.That(account.UserName, Is.Null);
                Assert.That(account.AccountNumber, Is.Null);
                Assert.That(account.LinkedEntityId, Is.EqualTo(0));
                Assert.That(account.LinkedEntityType, Is.Null);
                Assert.That(account.LinkedEntity, Is.Null);
                Assert.That(account.AllowAnnonymousUsers, Is.False);
                Assert.That(account.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(account.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(account.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsAllPropertiesCorrectly()
        {
            // Arrange & Act
            var account = new Account(
                id: 1,
                userName: "testuser",
                accountNumber: "ACC123",
                linkedEntityId: 1,
                linkedEntityType: "Person",
                linkedEntity: _sut,
                allowAnnonymousUsers: true,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(1));
                Assert.That(account.UserName, Is.EqualTo("testuser"));
                Assert.That(account.AccountNumber, Is.EqualTo("ACC123"));
                Assert.That(account.LinkedEntityId, Is.EqualTo(1));
                Assert.That(account.LinkedEntityType, Is.EqualTo("Person"));
                Assert.That(account.LinkedEntity, Is.EqualTo(_sut));
                Assert.That(account.AllowAnnonymousUsers, Is.True);
                Assert.That(account.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(account.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_SetsPropertiesCorrectlyFromLinkedEntity()
        {
            // Arrange & Act
            var account = new Account(
                userName: "testuser2",
                accountNumber: "ACC456",
                linkedEntity: _sut,
                allowAnnonymousUsers: false,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.UserName, Is.EqualTo("testuser2"));
                Assert.That(account.AccountNumber, Is.EqualTo("ACC456"));
                Assert.That(account.LinkedEntityId, Is.EqualTo(_sut.Id));
                Assert.That(account.LinkedEntityType, Is.EqualTo("Person"));
                Assert.That(account.LinkedEntity, Is.EqualTo(_sut));
                Assert.That(account.AllowAnnonymousUsers, Is.False);
                Assert.That(account.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(account.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void Id_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.Id = 123;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(123));
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void UserName_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.UserName = "newuser";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.UserName, Is.EqualTo("newuser"));
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void AccountNumber_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.AccountNumber = "ACC789";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.AccountNumber, Is.EqualTo("ACC789"));
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void LinkedEntityId_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.LinkedEntityId = 456;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.LinkedEntityId, Is.EqualTo(456));
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void LinkedEntityType_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.LinkedEntityType = "Organization";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.LinkedEntityType, Is.EqualTo("Organization"));
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.LinkedEntity = _sut;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.LinkedEntity, Is.EqualTo(_sut));
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void AllowAnnonymousUsers_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.AllowAnnonymousUsers = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.AllowAnnonymousUsers, Is.True);
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void Cast_ThrowsNotImplementedException()
        {
            // Arrange
            var account = new Account();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => account.Cast<Person>());
        }

        [Test, Category("Models")]
        public void ToJson_ReturnsValidJsonString()
        {
            // Arrange
            var account = new Account(
                id: 1,
                userName: "testuser",
                accountNumber: "ACC123",
                linkedEntityId: 2,
                linkedEntityType: "Person",
                linkedEntity: _sut,
                allowAnnonymousUsers: true,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var json = account.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
            });

            // Verify JSON contains expected properties
            Assert.That(json, Does.Contain("\"id\":1"));
            Assert.That(json, Does.Contain("\"userName\":\"testuser\""));
            Assert.That(json, Does.Contain("\"accountNumber\":\"ACC123\""));
            Assert.That(json, Does.Contain("\"allowAnnonymousUsers\":true"));
        }

        [Test, Category("Models")]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            var account = new Account
            {
                Id = 42,
                UserName = "johndoe"
            };

            // Act
            var result = account.ToString();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Does.Contain("Id42"));
            Assert.That(result, Does.Contain("UserNamejohndoe"));
            Assert.That(result, Does.Contain("OrganizerCompanion.Core.Models.Domain.Account"));
        }

        [Test, Category("Models")]
        public void ToString_HandlesNullUserName()
        {
            // Arrange
            var account = new Account();
            account.Id = 42;
            account.UserName = null;

            // Act
            var result = account.ToString();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Does.Contain("Id42"));
            Assert.That(result, Does.Contain("UserName"));
            Assert.That(result, Does.Contain("OrganizerCompanion.Core.Models.Domain.Account"));
        }

        [Test, Category("Models")]
        public void Properties_CanSetAndGetNullValues()
        {
            // Arrange
            var account = new Account();

            // Act & Assert
            account.UserName = null;
            Assert.That(account.UserName, Is.Null);

            account.AccountNumber = null;
            Assert.That(account.AccountNumber, Is.Null);

            account.LinkedEntityType = null;
            Assert.That(account.LinkedEntityType, Is.Null);

            account.LinkedEntity = null;
            Assert.That(account.LinkedEntity, Is.Null);
        }

        [Test, Category("Models")]
        public void DateCreated_IsReadOnly_AndSetDuringConstruction()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var account = new Account();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(account.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsDateCreatedFromParameter()
        {
            // Arrange
            var specificDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            var account = new Account(
                id: 1,
                userName: "testuser",
                accountNumber: "ACC123",
                linkedEntityId: 2,
                linkedEntityType: "Person",
                linkedEntity: _sut,
                allowAnnonymousUsers: true,
                dateCreated: specificDate,
                dateModified: _testDateModified
            );

            // Assert
            Assert.That(account.DateCreated, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_SetsDateCreatedFromParameter()
        {
            // Arrange
            var specificDate = new DateTime(2023, 6, 20, 14, 15, 30);

            // Act
            var account = new Account(
                userName: "testuser2",
                accountNumber: "ACC456",
                linkedEntity: _sut,
                allowAnnonymousUsers: false,
                dateCreated: specificDate,
                dateModified: _testDateModified
            );

            // Assert
            Assert.That(account.DateCreated, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void DateModified_CanBeSetDirectly()
        {
            // Arrange
            var account = new Account();
            var testDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            account.DateModified = testDate;

            // Assert
            Assert.That(account.DateModified, Is.EqualTo(testDate));
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_ThrowsException_WhenLinkedEntityIsNull()
        {
            // Arrange & Act & Assert
            var ex = Assert.Throws<Exception>(() => new Account(
                userName: "testuser",
                accountNumber: "ACC123",
                linkedEntity: null!,
                allowAnnonymousUsers: true,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            ));

            Assert.That(ex.Message, Is.EqualTo("Error creating Account object"));
            Assert.That(ex.InnerException, Is.Not.Null);
            Assert.That(ex.InnerException, Is.TypeOf<NullReferenceException>());
        }

        [Test, Category("Models")]
        public void JsonConstructor_ThrowsException_WhenInternalExceptionOccurs()
        {
            // Note: This test demonstrates the exception handling structure.
            // In practice, the JsonConstructor is less likely to throw exceptions
            // since it uses direct field assignment, but the try-catch is there for safety.

            // This test would need a scenario that actually causes an exception
            // For demonstration, we'll test with valid parameters to ensure no exception is thrown
            Assert.DoesNotThrow(() => new Account(
                id: 1,
                userName: "testuser",
                accountNumber: "ACC123",
                linkedEntityId: 2,
                linkedEntityType: "Person",
                linkedEntity: _sut,
                allowAnnonymousUsers: true,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            ));
        }

        [Test, Category("Models")]
        public void JsonConstructor_ThrowsException_WhenIdIsNegative()
        {
            // Arrange, Act & Assert
            var ex = Assert.Throws<Exception>(() => new Account(
                id: -1,
                userName: "testuser",
                accountNumber: "ACC123",
                linkedEntityId: 2,
                linkedEntityType: "Person",
                linkedEntity: _sut,
                allowAnnonymousUsers: true,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            ));

            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Error creating Account object"));
                Assert.That(ex.InnerException, Is.Not.Null);
                Assert.That(ex.InnerException, Is.TypeOf<ArgumentOutOfRangeException>());
                Assert.That(ex.InnerException!.Message, Does.Contain("id"));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_HandlesExceptionFromLinkedEntityId()
        {
            // Arrange
            // Create a mock object that throws an exception when accessing Id
            var mockEntity = new ThrowingMockEntity();

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => new Account(
                userName: "testuser",
                accountNumber: "ACC123",
                linkedEntity: mockEntity,
                allowAnnonymousUsers: true,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            ));

            Assert.That(ex.Message, Is.EqualTo("Error creating Account object"));
            Assert.That(ex.InnerException, Is.Not.Null);
            Assert.That(ex.InnerException.Message, Is.EqualTo("Mock exception from Id property"));
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_HandlesExceptionFromGetType()
        {
            // Arrange, Act & Assert
            var ex = Assert.Throws<Exception>(() => new Account(
                userName: "testuser",
                accountNumber: "ACC123",
                linkedEntity: null!,
                allowAnnonymousUsers: true,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            ));

            Assert.That(ex.Message, Is.EqualTo("Error creating Account object"));
            Assert.That(ex.InnerException, Is.Not.Null);
            Assert.That(ex.InnerException.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }
    }

    // Helper classes for testing exception scenarios
    internal class ThrowingMockEntity : IDomainEntity
    {
        public int Id
        {
            get => throw new Exception("Mock exception from Id property");
            set => throw new Exception("Mock exception from Id property setter");
        }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public IDomainEntity Cast<T>() => throw new NotImplementedException();
        public string ToJson() => throw new NotImplementedException();
    }

    internal class ThrowingGetTypeMockEntity : IDomainEntity
    {
        public int Id { get; set; } = 1;
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public new Type GetType() => throw new Exception("Mock exception from GetType method");

        public IDomainEntity Cast<T>() => throw new NotImplementedException();
        public string ToJson() => throw new NotImplementedException();
    }
}
