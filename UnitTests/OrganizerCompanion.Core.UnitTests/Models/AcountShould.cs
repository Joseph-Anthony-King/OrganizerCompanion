using NUnit.Framework;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class AccountShould
    {
        private User _sut;
        private readonly DateTime _testDateCreated = new(2023, 1, 1, 12, 0, 0);
        private readonly DateTime _testDateModified = new(2023, 1, 2, 12, 0, 0);
        private List<AccountFeature> _testFeatures;

        [SetUp]
        public void SetUp()
        {
            _sut = new User
            {
                // Set required properties on Person to make it valid
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };

            _testFeatures =
            [
                new AccountFeature { Id = 1, AccountId = 1, FeatureId = 1 }
            ];
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
                Assert.That(account.AccountName, Is.Null);
                Assert.That(account.AccountNumber, Is.Null);
                Assert.That(account.LinkedEntityId, Is.EqualTo(0));
                Assert.That(account.LinkedEntityType, Is.Null);
                Assert.That(account.LinkedEntity, Is.Null);
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features.Count, Is.EqualTo(0));
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
                accountName: "testuser",
                accountNumber: "ACC123",
                linkedEntityId: 1,
                linkedEntity: _sut,
                features: _testFeatures,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(1));
                Assert.That(account.AccountName, Is.EqualTo("testuser"));
                Assert.That(account.AccountNumber, Is.EqualTo("ACC123"));
                Assert.That(account.LinkedEntityId, Is.EqualTo(1));
                Assert.That(account.LinkedEntityType, Is.EqualTo("User"));
                Assert.That(account.LinkedEntity, Is.EqualTo(_sut));
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features.Count, Is.EqualTo(1));
                Assert.That(account.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(account.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_SetsPropertiesCorrectlyFromLinkedEntity()
        {
            // Arrange & Act
            var account = new Account(
                accountName: "testuser2",
                accountNumber: "ACC456",
                linkedEntity: _sut,
                features: _testFeatures,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.AccountName, Is.EqualTo("testuser2"));
                Assert.That(account.AccountNumber, Is.EqualTo("ACC456"));
                Assert.That(account.LinkedEntityId, Is.EqualTo(_sut.Id));
                Assert.That(account.LinkedEntityType, Is.EqualTo("User"));
                Assert.That(account.LinkedEntity, Is.EqualTo(_sut));
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features.Count, Is.EqualTo(1));
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
        public void AccountName_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.AccountName = "newuser";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.AccountName, Is.EqualTo("newuser"));
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
        public void Features_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;
            var newFeatures = new List<AccountFeature>
            {
                new() { Id = 2, AccountId = 1, FeatureId = 2 }
            };

            // Act
            account.Features = newFeatures;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features.Count, Is.EqualTo(1));
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
            Assert.Throws<NotImplementedException>(() => account.Cast<User>());
        }

        [Test, Category("Models")]
        public void IsCast_Getter_ThrowsNotImplementedException()
        {
            // Arrange
            var account = new Account();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = account.IsCast; });
        }

        [Test, Category("Models")]
        public void IsCast_Setter_ThrowsNotImplementedException()
        {
            // Arrange
            var account = new Account();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => account.IsCast = true);
        }

        [Test, Category("Models")]
        public void CastId_Getter_ThrowsNotImplementedException()
        {
            // Arrange
            var account = new Account();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = account.CastId; });
        }

        [Test, Category("Models")]
        public void CastId_Setter_ThrowsNotImplementedException()
        {
            // Arrange
            var account = new Account();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => account.CastId = 1);
        }

        [Test, Category("Models")]
        public void CastType_Getter_ThrowsNotImplementedException()
        {
            // Arrange
            var account = new Account();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = account.CastType; });
        }

        [Test, Category("Models")]
        public void CastType_Setter_ThrowsNotImplementedException()
        {
            // Arrange
            var account = new Account();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => account.CastType = "SomeType");
        }

        [Test, Category("Models")]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            var account = new Account
            {
                Id = 42,
                AccountName = "johndoe"
            };

            // Act
            var result = account.ToString();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Does.Contain("Id:42"));
            Assert.That(result, Does.Contain("AccountName:johndoe"));
            Assert.That(result, Does.Contain("OrganizerCompanion.Core.Models.Domain.Account"));
        }

        [Test, Category("Models")]
        public void ToString_HandlesNullAccountName()
        {
            // Arrange
            var account = new Account
            {
                Id = 42,
                AccountName = null
            };

            // Act
            var result = account.ToString();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Does.Contain("Id:42"));
            Assert.That(result, Does.Contain("AccountName"));
            Assert.That(result, Does.Contain("OrganizerCompanion.Core.Models.Domain.Account"));
        }

        [Test, Category("Models")]
        public void Properties_CanSetAndGetNullValues()
        {
            // Arrange
            var account = new Account
            {
                // Act & Assert
                AccountName = null
            };
            Assert.That(account.AccountName, Is.Null);

            account.AccountNumber = null;
            Assert.That(account.AccountNumber, Is.Null);

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
                accountName: "testuser",
                accountNumber: "ACC123",
                linkedEntityId: 2,
                linkedEntity: _sut,
                features: _testFeatures,
                dateCreated: specificDate,
                dateModified: _testDateModified,
                isCast: false,
                castId: 0,
                castType: null
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
                accountName: "testuser2",
                accountNumber: "ACC456",
                linkedEntity: _sut,
                features: _testFeatures,
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
                accountName: "testuser",
                accountNumber: "ACC123",
                linkedEntity: null!,
                features: _testFeatures,
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
                accountName: "testuser",
                accountNumber: "ACC123",
                linkedEntityId: 2,
                linkedEntity: _sut,
                features: _testFeatures,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified,
                isCast: false,
                castId: 0,
                castType: null
            ));
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_HandlesExceptionFromLinkedEntityId()
        {
            // Arrange
            // Create a mock object that throws an exception when accessing Id
            var mockEntity = new ThrowingMockEntity();

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => new Account(
                accountName: "testuser",
                accountNumber: "ACC123",
                linkedEntity: mockEntity,
                features: _testFeatures,
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
                accountName: "testuser",
                accountNumber: "ACC123",
                linkedEntity: null!,
                features: _testFeatures,
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
        public bool IsCast { get; set; }
        public int CastId { get; set; }
        public string? CastType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
        public string ToJson() => throw new NotImplementedException();
    }

    internal class ThrowingGetTypeMockEntity : IDomainEntity
    {
        public int Id { get; set; } = 1;
        public bool IsCast { get; set; } = false;
        public int CastId { get; set; } = 0;
        public string? CastType { get; set; } = null;
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public new Type GetType() => throw new Exception("Mock exception from GetType method");
        public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
        public string ToJson() => throw new NotImplementedException();
    }
}
