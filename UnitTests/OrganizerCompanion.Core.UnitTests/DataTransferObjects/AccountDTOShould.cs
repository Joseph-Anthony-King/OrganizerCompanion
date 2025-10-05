using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Type;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Validation.Attributes;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    [TestFixture]
    internal class AccountDTOShould
    {
        private AccountDTO _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new AccountDTO();
        }

        [Test, Category("DataTransferObjects")]
        public void DefaultConstructor_ShouldCreateAccountDTOWithDefaultValues()
        {
            // Arrange & Act
            _sut = new AccountDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.AccountName, Is.Null);
                Assert.That(_sut.AccountNumber, Is.Null);
                Assert.That(_sut.License, Is.Null);
                Assert.That(_sut.DatabaseConnection, Is.Null);
                Assert.That(_sut.Features, Is.Not.Null);
                Assert.That(_sut.Features, Is.Empty);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldGetAndSetValue()
        {
            // Arrange
            int expectedId = 123;

            // Act
            _sut.Id = expectedId;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(expectedId));
        }

        [Test, Category("DataTransferObjects")]
        public void AccountName_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedAccountName = "Test Account";

            // Act
            _sut.AccountName = expectedAccountName;

            // Assert
            Assert.That(_sut.AccountName, Is.EqualTo(expectedAccountName));
        }

        [Test, Category("DataTransferObjects")]
        public void AccountName_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.AccountName = null;

            // Assert
            Assert.That(_sut.AccountName, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AccountNumber_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedAccountNumber = "ACC-123456";

            // Act
            _sut.AccountNumber = expectedAccountNumber;

            // Assert
            Assert.That(_sut.AccountNumber, Is.EqualTo(expectedAccountNumber));
        }

        [Test, Category("DataTransferObjects")]
        public void AccountNumber_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.AccountNumber = null;

            // Assert
            Assert.That(_sut.AccountNumber, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void License_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedLicense = Guid.NewGuid().ToString();

            // Act
            _sut.License = expectedLicense;

            // Assert
            Assert.That(_sut.License, Is.EqualTo(expectedLicense));
        }

        [Test, Category("DataTransferObjects")]
        public void License_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.License = null;

            // Assert
            Assert.That(_sut.License, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void License_ShouldAcceptValidGuid()
        {
            // Arrange
            string validGuid = "12345678-1234-1234-1234-123456789012";

            // Act
            _sut.License = validGuid;

            // Assert
            Assert.That(_sut.License, Is.EqualTo(validGuid));
        }

        [Test, Category("DataTransferObjects")]
        public void DatabaseConnection_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedConnection = new DatabaseConnection
            {
                ConnectionString = "Server=localhost;Database=testdb;",
                DatabaseType = SupportedDatabases.SQLServer
            };

            // Act
            _sut.DatabaseConnection = expectedConnection;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DatabaseConnection, Is.EqualTo(expectedConnection));
                Assert.That(_sut.DatabaseConnection?.ConnectionString, Is.EqualTo("Server=localhost;Database=testdb;"));
                Assert.That(_sut.DatabaseConnection?.DatabaseType, Is.EqualTo(SupportedDatabases.SQLServer));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DatabaseConnection_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.DatabaseConnection = null;

            // Assert
            Assert.That(_sut.DatabaseConnection, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        [TestCase(SupportedDatabases.SQLServer)]
        [TestCase(SupportedDatabases.SQLite)]
        [TestCase(SupportedDatabases.MySQL)]
        [TestCase(SupportedDatabases.PostgreSQL)]
        public void DatabaseConnection_ShouldAcceptAllDatabaseTypes(SupportedDatabases databaseType)
        {
            // Arrange
            var connection = new DatabaseConnection
            {
                ConnectionString = "test-connection-string",
                DatabaseType = databaseType
            };

            // Act
            _sut.DatabaseConnection = connection;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DatabaseConnection, Is.Not.Null);
                Assert.That(_sut.DatabaseConnection?.DatabaseType, Is.EqualTo(databaseType));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Features_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedFeatures = new List<FeatureDTO>
            {
                new() { Id = 1, FeatureName = "Feature 1", IsEnabled = true },
                new() { Id = 2, FeatureName = "Feature 2", IsEnabled = false }
            };

            // Act
            _sut.Features = expectedFeatures;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Features, Is.EqualTo(expectedFeatures));
                Assert.That(_sut.Features.Count, Is.EqualTo(2));
                Assert.That(_sut.Features[0].Id, Is.EqualTo(1));
                Assert.That(_sut.Features[0].FeatureName, Is.EqualTo("Feature 1"));
                Assert.That(_sut.Features[0].IsEnabled, Is.True);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Features_ShouldAcceptEmptyList()
        {
            // Arrange
            var emptyFeatures = new List<FeatureDTO>();

            // Act
            _sut.Features = emptyFeatures;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Features, Is.Not.Null);
                Assert.That(_sut.Features, Is.Empty);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceFeatures_Get_ShouldConvertFeaturesToIFeatureDTO()
        {
            // Arrange
            var features = new List<FeatureDTO>
            {
                new() { Id = 1, FeatureName = "Test Feature", IsEnabled = true }
            };
            _sut.Features = features;
            var accountInterface = (IAccountDTO)_sut;

            // Act
            var result = accountInterface.Features;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.EqualTo(1));
                Assert.That(result[0], Is.TypeOf<FeatureDTO>());
                Assert.That(result[0].Id, Is.EqualTo(1));
                Assert.That(result[0].FeatureName, Is.EqualTo("Test Feature"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceFeatures_Set_ShouldConvertIFeatureDTOToFeatures()
        {
            // Arrange
            var interfaceFeatures = new List<IFeatureDTO>
            {
                new FeatureDTO { Id = 2, FeatureName = "Interface Feature", IsEnabled = false }
            };
            var accountInterface = (IAccountDTO)_sut;

            // Act
            accountInterface.Features = interfaceFeatures;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Features, Is.Not.Null);
                Assert.That(_sut.Features.Count, Is.EqualTo(1));
                Assert.That(_sut.Features[0].Id, Is.EqualTo(2));
                Assert.That(_sut.Features[0].FeatureName, Is.EqualTo("Interface Feature"));
                Assert.That(_sut.Features[0].IsEnabled, Is.False);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.IsCast; });
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.IsCast = true; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastId; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.CastId = 123; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastType; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.CastType = "TestType"; });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_Get_ShouldReturnValue()
        {
            // Arrange
            var beforeCreation = DateTime.Now.AddSeconds(-1);
            var accountDto = new AccountDTO();
            var afterCreation = DateTime.Now.AddSeconds(1);

            // Act
            var dateCreated = accountDto.DateCreated;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(dateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            _sut.DateCreated = expectedDate;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 6, 20, 14, 15, 30);

            // Act
            _sut.DateModified = expectedDate;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.DateModified = null;

            // Assert
            Assert.That(_sut.DateModified, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_DefaultValue_ShouldBeNull()
        {
            // Arrange & Act
            var accountDto = new AccountDTO();

            // Assert
            Assert.That(accountDto.DateModified, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.Cast<MockDomainEntity>(); });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.ToJson(); });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.Id));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AccountName_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.AccountName));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AccountNumber_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.AccountNumber));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void License_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.License));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void License_ShouldHaveGuidValidatorAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.License));

            // Act
            var guidValidatorAttribute = property?.GetCustomAttribute<GuidValidatorAttribute>();

            // Assert
            Assert.That(guidValidatorAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DatabaseConnection_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.DatabaseConnection));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DatabaseConnection_ShouldHaveDatabaseConnectionValidatorAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.DatabaseConnection));

            // Act
            var validatorAttribute = property?.GetCustomAttribute<DatabaseConnectionValidatorAttribute>();

            // Assert
            Assert.That(validatorAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Features_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.Features));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.DateCreated));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.DateModified));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AccountDTO_ShouldImplementIAccountDTO()
        {
            // Arrange & Act
            var accountDTO = new AccountDTO();

            // Assert
            Assert.That(accountDTO, Is.InstanceOf<IAccountDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void AccountDTO_Properties_ShouldBeSettableInChain()
        {
            // Arrange
            var testGuid = Guid.NewGuid().ToString();
            var testConnection = new DatabaseConnection
            {
                ConnectionString = "Server=localhost;Database=test;",
                DatabaseType = SupportedDatabases.MySQL
            };

            // Act
            var accountDTO = new AccountDTO
            {
                Id = 999,
                AccountName = "Chained Account",
                AccountNumber = "CHAIN-999",
                License = testGuid,
                DatabaseConnection = testConnection,
                Features = new List<FeatureDTO>
                {
                    new() { Id = 1, FeatureName = "Chained Feature", IsEnabled = true }
                }
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountDTO.Id, Is.EqualTo(999));
                Assert.That(accountDTO.AccountName, Is.EqualTo("Chained Account"));
                Assert.That(accountDTO.AccountNumber, Is.EqualTo("CHAIN-999"));
                Assert.That(accountDTO.License, Is.EqualTo(testGuid));
                Assert.That(accountDTO.DatabaseConnection, Is.EqualTo(testConnection));
                Assert.That(accountDTO.DatabaseConnection?.ConnectionString, Is.EqualTo("Server=localhost;Database=test;"));
                Assert.That(accountDTO.DatabaseConnection?.DatabaseType, Is.EqualTo(SupportedDatabases.MySQL));
                Assert.That(accountDTO.Features.Count, Is.EqualTo(1));
                Assert.That(accountDTO.Features[0].FeatureName, Is.EqualTo("Chained Feature"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AccountDTO_WithAllProperties_ShouldMaintainConsistency()
        {
            // Arrange
            var guid = Guid.NewGuid().ToString();
            var connection = new DatabaseConnection
            {
                ConnectionString = "Server=testserver;Database=testdb;User ID=admin;",
                DatabaseType = SupportedDatabases.PostgreSQL
            };
            var features = new List<FeatureDTO>
            {
                new() { Id = 10, FeatureName = "Feature A", IsEnabled = true },
                new() { Id = 20, FeatureName = "Feature B", IsEnabled = false }
            };

            // Act
            _sut.Id = 500;
            _sut.AccountName = "Comprehensive Test";
            _sut.AccountNumber = "COMP-500";
            _sut.License = guid;
            _sut.DatabaseConnection = connection;
            _sut.Features = features;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(500));
                Assert.That(_sut.AccountName, Is.EqualTo("Comprehensive Test"));
                Assert.That(_sut.AccountNumber, Is.EqualTo("COMP-500"));
                Assert.That(_sut.License, Is.EqualTo(guid));
                Assert.That(_sut.DatabaseConnection, Is.Not.Null);
                Assert.That(_sut.DatabaseConnection?.ConnectionString, Is.EqualTo("Server=testserver;Database=testdb;User ID=admin;"));
                Assert.That(_sut.DatabaseConnection?.DatabaseType, Is.EqualTo(SupportedDatabases.PostgreSQL));
                Assert.That(_sut.Features, Has.Count.EqualTo(2));
                Assert.That(_sut.Features[0].FeatureName, Is.EqualTo("Feature A"));
                Assert.That(_sut.Features[1].FeatureName, Is.EqualTo("Feature B"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AccountDTO_ShouldAllowModificationOfDatabaseConnection()
        {
            // Arrange
            var initialConnection = new DatabaseConnection
            {
                ConnectionString = "Initial Connection",
                DatabaseType = SupportedDatabases.SQLServer
            };
            _sut.DatabaseConnection = initialConnection;

            var newConnection = new DatabaseConnection
            {
                ConnectionString = "New Connection",
                DatabaseType = SupportedDatabases.SQLite
            };

            // Act
            _sut.DatabaseConnection = newConnection;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DatabaseConnection, Is.EqualTo(newConnection));
                Assert.That(_sut.DatabaseConnection?.ConnectionString, Is.EqualTo("New Connection"));
                Assert.That(_sut.DatabaseConnection?.DatabaseType, Is.EqualTo(SupportedDatabases.SQLite));
                Assert.That(_sut.DatabaseConnection, Is.Not.EqualTo(initialConnection));
            });
        }

        #region Mock Classes
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime DateCreated { get; }
            public DateTime? DateModified { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => throw new NotImplementedException();
        }
        #endregion
    }
}
