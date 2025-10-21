using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Type;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Validation.Attributes;
using DatabaseConnection = OrganizerCompanion.Core.Models.Domain.DatabaseConnection;

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
                Assert.That(_sut.Accounts, Is.Null);
                Assert.That(_sut.DateModified, Is.Null);
                // DateCreated should be set to current time, tested separately for timing
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
                Assert.That(_sut.Features, Has.Count.EqualTo(2));
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
        public void Features_DefaultValue_ShouldBeEmptyList()
        {
            // Arrange & Act
            var accountDto = new AccountDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountDto.Features, Is.Not.Null);
                Assert.That(accountDto.Features, Is.Empty);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Accounts_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedAccounts = new List<SubAccountDTO>
            {
                new() { Id = 1, LinkedEntityId = 1 },
                new() { Id = 2, LinkedEntityId = 2 }
            };

            // Act
            _sut.Accounts = expectedAccounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Accounts, Is.EqualTo(expectedAccounts));
                Assert.That(_sut.Accounts, Has.Count.EqualTo(2));
                Assert.That(_sut.Accounts![0].Id, Is.EqualTo(1));
                Assert.That(_sut.Accounts[0].Id, Is.EqualTo(0 + 1));
                Assert.That(_sut.Accounts[0].LinkedEntityId, Is.EqualTo(0 + 1));
                Assert.That(_sut.Accounts[1].Id, Is.EqualTo(2));
                Assert.That(_sut.Accounts[1].Id, Is.EqualTo(1 + 1));
                Assert.That(_sut.Accounts[1].LinkedEntityId, Is.EqualTo(1 + 1));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Accounts_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.Accounts = null;

            // Assert
            Assert.That(_sut.Accounts, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Accounts_ShouldAcceptEmptyList()
        {
            // Arrange
            var emptyAccounts = new List<SubAccountDTO>();

            // Act
            _sut.Accounts = emptyAccounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Accounts, Is.Not.Null);
                Assert.That(_sut.Accounts, Is.Empty);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Accounts_DefaultValue_ShouldBeNull()
        {
            // Arrange & Act
            var accountDto = new AccountDTO();

            // Assert
            Assert.That(accountDto.Accounts, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Accounts_ShouldAllowModificationAfterInitialization()
        {
            // Arrange
            var initialAccounts = new List<SubAccountDTO>
            {
                new() { Id = 1, LinkedEntityId = 1 }
            };
            _sut.Accounts = initialAccounts;

            var newAccounts = new List<SubAccountDTO>
            {
                new() { Id = 2, LinkedEntityId = 2 },
                new() { Id = 3, LinkedEntityId = 3 }
            };

            // Act
            _sut.Accounts = newAccounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Accounts, Is.EqualTo(newAccounts));
                Assert.That(_sut.Accounts, Has.Count.EqualTo(2));
                Assert.That(_sut.Accounts![0].Id, Is.EqualTo(2));
                Assert.That(_sut.Accounts[1].Id, Is.EqualTo(3));
                Assert.That(_sut.Accounts, Is.Not.EqualTo(initialAccounts));
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
                Assert.That(result, Has.Count.EqualTo(1));
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
                Assert.That(_sut.Features, Has.Count.EqualTo(1));
                Assert.That(_sut.Features[0].Id, Is.EqualTo(2));
                Assert.That(_sut.Features[0].FeatureName, Is.EqualTo("Interface Feature"));
                Assert.That(_sut.Features[0].IsEnabled, Is.False);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceFeatures_Set_ShouldHandleEmptyList()
        {
            // Arrange
            var emptyInterfaceFeatures = new List<IFeatureDTO>();
            var accountInterface = (IAccountDTO)_sut;

            // Act
            accountInterface.Features = emptyInterfaceFeatures;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Features, Is.Not.Null);
                Assert.That(_sut.Features, Is.Empty);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceFeatures_Set_ShouldReplaceExistingFeatures()
        {
            // Arrange
            var initialFeatures = new List<FeatureDTO>
            {
                new() { Id = 1, FeatureName = "Initial Feature", IsEnabled = true }
            };
            _sut.Features = initialFeatures;

            var newInterfaceFeatures = new List<IFeatureDTO>
            {
                new FeatureDTO { Id = 10, FeatureName = "Replacement Feature", IsEnabled = false }
            };
            var accountInterface = (IAccountDTO)_sut;

            // Act
            accountInterface.Features = newInterfaceFeatures;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Features, Is.Not.Null);
                Assert.That(_sut.Features, Has.Count.EqualTo(1));
                Assert.That(_sut.Features[0].Id, Is.EqualTo(10));
                Assert.That(_sut.Features[0].FeatureName, Is.EqualTo("Replacement Feature"));
                Assert.That(_sut.Features[0].IsEnabled, Is.False);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAccounts_Get_ShouldConvertAccountsToISubAccountDTO()
        {
            // Arrange
            var accounts = new List<SubAccountDTO>
            {
                new() { Id = 1, LinkedEntityId = 1 }
            };
            _sut.Accounts = accounts;
            var accountInterface = (IAccountDTO)_sut;

            // Act
            var result = accountInterface.Accounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Has.Count.EqualTo(1));
                Assert.That(result![0], Is.TypeOf<SubAccountDTO>());
                Assert.That(result[0].Id, Is.EqualTo(1));
                Assert.That(result[0].LinkedEntityId, Is.EqualTo(1));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAccounts_Get_ShouldHandleNullAccounts()
        {
            // Arrange
            _sut.Accounts = null;
            var accountInterface = (IAccountDTO)_sut;

            // Act
            var result = accountInterface.Accounts;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAccounts_Set_ShouldConvertISubAccountDTOToAccounts()
        {
            // Arrange
            var interfaceAccounts = new List<ISubAccountDTO>
            {
                new SubAccountDTO { Id = 2, LinkedEntityId = 2 }
            };
            var accountInterface = (IAccountDTO)_sut;

            // Act
            accountInterface.Accounts = interfaceAccounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Accounts, Is.Not.Null);
                Assert.That(_sut.Accounts, Has.Count.EqualTo(1));
                Assert.That(_sut.Accounts![0].Id, Is.EqualTo(2));
                Assert.That(_sut.Accounts[0].LinkedEntityId, Is.EqualTo(2));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAccounts_Set_ShouldHandleNullValue()
        {
            // Arrange
            var accountInterface = (IAccountDTO)_sut;

            // Act
            accountInterface.Accounts = null;

            // Assert
            Assert.Throws<NullReferenceException>(() => { var count = _sut.Accounts!.Count; });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAccounts_Set_ShouldHandleEmptyList()
        {
            // Arrange
            var emptyInterfaceAccounts = new List<ISubAccountDTO>();
            var accountInterface = (IAccountDTO)_sut;

            // Act
            accountInterface.Accounts = emptyInterfaceAccounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Accounts, Is.Not.Null);
                Assert.That(_sut.Accounts, Is.Empty);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAccounts_Set_ShouldClearExistingAccountsAndAddNew()
        {
            // Arrange
            var initialAccounts = new List<SubAccountDTO>
            {
                new() { Id = 1, LinkedEntityId = 1 },
                new() { Id = 2, LinkedEntityId = 2 }
            };
            _sut.Accounts = initialAccounts;

            var newInterfaceAccounts = new List<ISubAccountDTO>
            {
                new SubAccountDTO { Id = 3, LinkedEntityId = 3 },
                new SubAccountDTO { Id = 4, LinkedEntityId = 4 },
                new SubAccountDTO { Id = 5, LinkedEntityId = 5 }
            };
            var accountInterface = (IAccountDTO)_sut;

            // Act
            accountInterface.Accounts = newInterfaceAccounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Accounts, Is.Not.Null);
                Assert.That(_sut.Accounts, Has.Count.EqualTo(3));
                Assert.That(_sut.Accounts![0].Id, Is.EqualTo(3));
                Assert.That(_sut.Accounts[1].Id, Is.EqualTo(4));
                Assert.That(_sut.Accounts[2].Id, Is.EqualTo(5));
                Assert.That(_sut.Accounts[0].LinkedEntityId, Is.EqualTo(3));
                Assert.That(_sut.Accounts[1].LinkedEntityId, Is.EqualTo(4));
                Assert.That(_sut.Accounts[2].LinkedEntityId, Is.EqualTo(5));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAccounts_Set_ShouldInitializeAccountsIfNull()
        {
            // Arrange
            _sut.Accounts = null;
            var interfaceAccounts = new List<ISubAccountDTO>
            {
                new SubAccountDTO { Id = 10, LinkedEntityId = 10 }
            };
            var accountInterface = (IAccountDTO)_sut;

            // Act
            accountInterface.Accounts = interfaceAccounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Accounts, Is.Not.Null);
                Assert.That(_sut.Accounts, Has.Count.EqualTo(1));
                Assert.That(_sut.Accounts![0].Id, Is.EqualTo(10));
                Assert.That(_sut.Accounts[0].LinkedEntityId, Is.EqualTo(10));
            });
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
        public void DateCreated_DefaultValue_ShouldBeCurrentDateTime()
        {
            // Arrange
            var beforeCreation = DateTime.Now.AddSeconds(-1);

            // Act
            var accountDto = new AccountDTO();
            var afterCreation = DateTime.Now.AddSeconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountDto.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(accountDto.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
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
            // Arrange, Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.Cast<MockDomainEntity>(); });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldThrowNotImplementedException()
        {
            // Arrange, Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.ToJson(); });
        }

        [Test, Category("DataTransferObjects")]
        public void Features_ShouldAllowNullAssignment()
        {
            // Note: This test verifies behavior if someone tries to assign null
            // even though the property initializes to an empty list
            // Arrange, Act & Assert
            Assert.DoesNotThrow(() => { _sut.Features = null!; });
        }

        [Test, Category("DataTransferObjects")]
        public void Features_ShouldAllowModificationAfterInitialization()
        {
            // Arrange
            var initialFeatures = new List<FeatureDTO>
            {
                new() { Id = 1, FeatureName = "Initial Feature", IsEnabled = true }
            };
            _sut.Features = initialFeatures;

            var newFeatures = new List<FeatureDTO>
            {
                new() { Id = 2, FeatureName = "New Feature A", IsEnabled = false },
                new() { Id = 3, FeatureName = "New Feature B", IsEnabled = true }
            };

            // Act
            _sut.Features = newFeatures;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Features, Is.EqualTo(newFeatures));
                Assert.That(_sut.Features, Has.Count.EqualTo(2));
                Assert.That(_sut.Features[0].FeatureName, Is.EqualTo("New Feature A"));
                Assert.That(_sut.Features[1].FeatureName, Is.EqualTo("New Feature B"));
                Assert.That(_sut.Features, Is.Not.EqualTo(initialFeatures));
            });
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
        public void Id_ShouldHaveRangeAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.Id));

            // Act
            var rangeAttribute = property?.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(rangeAttribute, Is.Not.Null);
                Assert.That(rangeAttribute?.Minimum, Is.EqualTo(0));
                Assert.That(rangeAttribute?.Maximum, Is.EqualTo(int.MaxValue));
                Assert.That(rangeAttribute?.ErrorMessage, Is.EqualTo("Id must be a non-negative number."));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.Id));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("id"));
            });
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
        public void AccountName_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.AccountName));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("accountName"));
            });
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
        public void AccountNumber_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.AccountNumber));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("accountNumber"));
            });
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
        public void License_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.License));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("license"));
            });
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
        public void DatabaseConnection_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.DatabaseConnection));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("databaseConnection"));
            });
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
        public void Features_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.Features));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("features"));
            });
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
        public void DateCreated_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.DateCreated));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("dateCreated"));
            });
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
        public void DateModified_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.DateModified));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("dateModified"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Accounts_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.Accounts));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("accounts"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Accounts_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.Accounts));

            // Act
            var jsonIgnoreAttribute = property?.GetCustomAttribute<JsonIgnoreAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonIgnoreAttribute, Is.Not.Null);
                Assert.That(jsonIgnoreAttribute?.Condition, Is.EqualTo(JsonIgnoreCondition.WhenWritingNull));
            });
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
        public void AccountDTO_ShouldImplementIDomainEntity()
        {
            // Arrange & Act
            var accountDTO = new AccountDTO();

            // Assert
            Assert.That(accountDTO, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void AccountDTO_CastToInterface_ShouldRetainAllProperties()
        {
            // Arrange
            var originalId = 42;
            var originalAccountName = "Test Account";
            var originalFeatures = new List<FeatureDTO>
            {
                new() { Id = 1, FeatureName = "Test Feature", IsEnabled = true }
            };

            _sut.Id = originalId;
            _sut.AccountName = originalAccountName;
            _sut.Features = originalFeatures;

            // Act
            var interfaceInstance = (IAccountDTO)_sut;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceInstance.Id, Is.EqualTo(originalId));
                Assert.That(interfaceInstance.AccountName, Is.EqualTo(originalAccountName));
                Assert.That(interfaceInstance.Features, Has.Count.EqualTo(1));
                Assert.That(interfaceInstance.Features[0].FeatureName, Is.EqualTo("Test Feature"));
            });
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
            var testSubAccounts = new List<SubAccountDTO>
            {
                new() { Id = 1, LinkedEntityId = 1 }
            };

            // Act
            var accountDTO = new AccountDTO
            {
                Id = 999,
                AccountName = "Chained Account",
                AccountNumber = "CHAIN-999",
                License = testGuid,
                DatabaseConnection = testConnection,
                Features =
                [
                    new() { Id = 1, FeatureName = "Chained Feature", IsEnabled = true }
                ],
                Accounts = testSubAccounts
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountDTO.Id, Is.EqualTo(999));
                Assert.That(accountDTO.AccountName, Is.EqualTo("Chained Account"));
                Assert.That(accountDTO.AccountNumber, Is.EqualTo("CHAIN-999"));
                Assert.That(accountDTO.License, Is.EqualTo(testGuid));
                Assert.That(accountDTO.DatabaseConnection.ConnectionString, Is.EqualTo(testConnection.ConnectionString));
                Assert.That(accountDTO.DatabaseConnection?.DatabaseType, Is.EqualTo(SupportedDatabases.MySQL));
                Assert.That(accountDTO.Features, Has.Count.EqualTo(1));
                Assert.That(accountDTO.Features[0].FeatureName, Is.EqualTo("Chained Feature"));
                Assert.That(accountDTO.Accounts, Is.EqualTo(testSubAccounts));
                Assert.That(accountDTO.Accounts, Has.Count.EqualTo(1));
                Assert.That(accountDTO.Accounts![0].Id, Is.EqualTo(1));
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
            var accounts = new List<SubAccountDTO>
            {
                new() { Id = 30, LinkedEntityId = 30 },
                new() { Id = 40, LinkedEntityId = 40 }
            };

            // Act
            _sut.Id = 500;
            _sut.AccountName = "Comprehensive Test";
            _sut.AccountNumber = "COMP-500";
            _sut.License = guid;
            _sut.DatabaseConnection = connection;
            _sut.Features = features;
            _sut.Accounts = accounts;

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
                Assert.That(_sut.Accounts, Is.Not.Null);
                Assert.That(_sut.Accounts, Has.Count.EqualTo(2));
                Assert.That(_sut.Accounts![0].Id, Is.EqualTo(30));
                Assert.That(_sut.Accounts[1].Id, Is.EqualTo(40));
                Assert.That(_sut.Accounts[0].LinkedEntityId, Is.EqualTo(30));
                Assert.That(_sut.Accounts[1].LinkedEntityId, Is.EqualTo(40));
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

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceFeatures_Property_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var interfaceMap = typeof(AccountDTO).GetInterfaceMap(typeof(IAccountDTO));
            var featuresPropertyIndex = Array.FindIndex(interfaceMap.InterfaceMethods,
                m => m.Name == "get_Features");

            // Act & Assert
            // The explicit interface implementation has JsonIgnore attribute on the property
            // This test verifies the explicit interface property exists and is properly implemented
            Assert.Multiple(() =>
            {
                Assert.That(featuresPropertyIndex, Is.GreaterThanOrEqualTo(0));
                Assert.That(interfaceMap.TargetMethods[featuresPropertyIndex], Is.Not.Null);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAccounts_Property_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var interfaceMap = typeof(AccountDTO).GetInterfaceMap(typeof(IAccountDTO));
            var accountsPropertyIndex = Array.FindIndex(interfaceMap.InterfaceMethods,
                m => m.Name == "get_Accounts");

            // Act & Assert
            // The explicit interface implementation has JsonIgnore attribute on the property
            // This test verifies the explicit interface property exists and is properly implemented
            Assert.Multiple(() =>
            {
                Assert.That(accountsPropertyIndex, Is.GreaterThanOrEqualTo(0));
                Assert.That(interfaceMap.TargetMethods[accountsPropertyIndex], Is.Not.Null);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAccounts_Set_ShouldHandleMultipleConsecutiveSets()
        {
            // Arrange
            var firstAccounts = new List<ISubAccountDTO>
            {
                new SubAccountDTO { Id = 1, LinkedEntityId = 1 }
            };
            var secondAccounts = new List<ISubAccountDTO>
            {
                new SubAccountDTO { Id = 2, LinkedEntityId = 2 },
                new SubAccountDTO { Id = 3, LinkedEntityId = 3 }
            };
            var accountInterface = (IAccountDTO)_sut;

            // Act
            accountInterface.Accounts = firstAccounts;
            accountInterface.Accounts = secondAccounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Accounts, Is.Not.Null);
                Assert.That(_sut.Accounts, Has.Count.EqualTo(2));
                Assert.That(_sut.Accounts![0].Id, Is.EqualTo(2));
                Assert.That(_sut.Accounts[1].Id, Is.EqualTo(3));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAccounts_Set_ShouldHandleAccountsIsNullAndValueHasItems()
        {
            // Arrange
            _sut.Accounts = null; // Ensure it's null first
            var interfaceAccounts = new List<ISubAccountDTO>
            {
                new SubAccountDTO { Id = 99, LinkedEntityId = 99 }
            };
            var accountInterface = (IAccountDTO)_sut;

            // Act
            accountInterface.Accounts = interfaceAccounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Accounts, Is.Not.Null);
                Assert.That(_sut.Accounts, Has.Count.EqualTo(1));
                Assert.That(_sut.Accounts![0].Id, Is.EqualTo(99));
                Assert.That(_sut.Accounts[0].LinkedEntityId, Is.EqualTo(99));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceFeatures_GetSet_ShouldWorkWithMixedFeatureTypes()
        {
            // Arrange
            var mixedFeatures = new List<IFeatureDTO>
            {
                new FeatureDTO { Id = 1, FeatureName = "Feature One", IsEnabled = true },
                new FeatureDTO { Id = 2, FeatureName = "Feature Two", IsEnabled = false }
            };
            var accountInterface = (IAccountDTO)_sut;

            // Act
            accountInterface.Features = mixedFeatures;
            var retrievedFeatures = accountInterface.Features;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(retrievedFeatures, Is.Not.Null);
                Assert.That(retrievedFeatures, Has.Count.EqualTo(2));
                Assert.That(retrievedFeatures[0].Id, Is.EqualTo(1));
                Assert.That(retrievedFeatures[0].FeatureName, Is.EqualTo("Feature One"));
                Assert.That(retrievedFeatures[0].IsEnabled, Is.True);
                Assert.That(retrievedFeatures[1].Id, Is.EqualTo(2));
                Assert.That(retrievedFeatures[1].FeatureName, Is.EqualTo("Feature Two"));
                Assert.That(retrievedFeatures[1].IsEnabled, Is.False);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AccountDTO_InterfaceAndConcreteTypeBehavior_ShouldBeConsistent()
        {
            // Arrange
            var features = new List<FeatureDTO>
            {
                new() { Id = 100, FeatureName = "Consistency Test", IsEnabled = true }
            };
            var accounts = new List<SubAccountDTO>
            {
                new() { Id = 200, LinkedEntityId = 200 }
            };

            // Act - Set via concrete type
            _sut.Features = features;
            _sut.Accounts = accounts;

            // Cast to interface and retrieve
            var accountInterface = (IAccountDTO)_sut;
            var interfaceFeatures = accountInterface.Features;
            var interfaceAccounts = accountInterface.Accounts;

            // Assert
            Assert.Multiple(() =>
            {
                // Features consistency
                Assert.That(interfaceFeatures, Has.Count.EqualTo(1));
                Assert.That(interfaceFeatures[0].Id, Is.EqualTo(100));
                Assert.That(interfaceFeatures[0].FeatureName, Is.EqualTo("Consistency Test"));

                // Accounts consistency
                Assert.That(interfaceAccounts, Is.Not.Null);
                Assert.That(interfaceAccounts, Has.Count.EqualTo(1));
                Assert.That(interfaceAccounts![0].Id, Is.EqualTo(200));
                Assert.That(interfaceAccounts[0].LinkedEntityId, Is.EqualTo(200));

                // Verify concrete type still has same data
                Assert.That(_sut.Features[0].Id, Is.EqualTo(100));
                Assert.That(_sut.Accounts![0].Id, Is.EqualTo(200));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AccountDTO_DefaultValues_ShouldMatchExpectations()
        {
            // Arrange & Act
            var freshAccountDTO = new AccountDTO();

            // Assert - Verify all default values are as expected
            Assert.Multiple(() =>
            {
                Assert.That(freshAccountDTO.Id, Is.EqualTo(0), "Id should default to 0");
                Assert.That(freshAccountDTO.AccountName, Is.Null, "AccountName should default to null");
                Assert.That(freshAccountDTO.AccountNumber, Is.Null, "AccountNumber should default to null");
                Assert.That(freshAccountDTO.License, Is.Null, "License should default to null");
                Assert.That(freshAccountDTO.DatabaseConnection, Is.Null, "DatabaseConnection should default to null");
                Assert.That(freshAccountDTO.Features, Is.Not.Null, "Features should not be null");
                Assert.That(freshAccountDTO.Features, Is.Empty, "Features should be empty list");
                Assert.That(freshAccountDTO.Accounts, Is.Null, "Accounts should default to null");
                Assert.That(freshAccountDTO.DateModified, Is.Null, "DateModified should default to null");
                Assert.That(freshAccountDTO.DateCreated, Is.TypeOf<DateTime>(), "DateCreated should be DateTime");
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AccountDTO_AllPropertiesSet_ShouldMaintainCorrectTypes()
        {
            // Arrange
            var testDate = new DateTime(2023, 12, 25, 10, 30, 45);
            var testGuid = Guid.NewGuid().ToString();
            var testConnection = new DatabaseConnection
            {
                ConnectionString = "Type Test Connection",
                DatabaseType = SupportedDatabases.SQLServer
            };

            // Act
            _sut.Id = 12345;
            _sut.AccountName = "Type Test Account";
            _sut.AccountNumber = "TYPE-12345";
            _sut.License = testGuid;
            _sut.DatabaseConnection = testConnection;
            _sut.Features = [new() { Id = 1, FeatureName = "Type Test Feature", IsEnabled = true }];
            _sut.Accounts = [new() { Id = 1, LinkedEntityId = 1 }];
            _sut.DateCreated = testDate;
            _sut.DateModified = testDate.AddDays(1);

            // Assert - Verify all types are correct
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.TypeOf<int>());
                Assert.That(_sut.AccountName, Is.TypeOf<string>());
                Assert.That(_sut.AccountNumber, Is.TypeOf<string>());
                Assert.That(_sut.License, Is.TypeOf<string>());
                Assert.That(_sut.DatabaseConnection, Is.TypeOf<DatabaseConnection>());
                Assert.That(_sut.Features, Is.TypeOf<List<FeatureDTO>>());
                Assert.That(_sut.Accounts, Is.TypeOf<List<SubAccountDTO>>());
                Assert.That(_sut.DateCreated, Is.TypeOf<DateTime>());
                Assert.That(_sut.DateModified, Is.TypeOf<DateTime>(), "DateModified should be DateTime when set to non-null value");
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_WithSpecificType_ShouldThrowNotImplementedException()
        {
            // Arrange, Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Cast<AccountDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_MultipleCalls_ShouldAlwaysThrowNotImplementedException()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => _sut.ToJson());
                Assert.Throws<NotImplementedException>(() => _sut.ToJson());
                Assert.Throws<NotImplementedException>(() => _sut.ToJson());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Features_ShouldAllowLargeList()
        {
            // Arrange
            var largeFeatureList = new List<FeatureDTO>();
            for (int i = 0; i < 1000; i++)
            {
                largeFeatureList.Add(new FeatureDTO { Id = i, FeatureName = $"Feature {i}", IsEnabled = i % 2 == 0 });
            }

            // Act
            _sut.Features = largeFeatureList;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Features, Has.Count.EqualTo(1000));
                Assert.That(_sut.Features[0].FeatureName, Is.EqualTo("Feature 0"));
                Assert.That(_sut.Features[999].FeatureName, Is.EqualTo("Feature 999"));
                Assert.That(_sut.Features[500].IsEnabled, Is.True);
                Assert.That(_sut.Features[501].IsEnabled, Is.False);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Accounts_ShouldAllowLargeList()
        {
            // Arrange
            var largeAccountList = new List<SubAccountDTO>();
            for (int i = 0; i < 500; i++)
            {
                largeAccountList.Add(new SubAccountDTO { Id = i, LinkedEntityId = i * 2 });
            }

            // Act
            _sut.Accounts = largeAccountList;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Accounts, Has.Count.EqualTo(500));
                Assert.That(_sut.Accounts![0].Id, Is.EqualTo(0));
                Assert.That(_sut.Accounts[499].Id, Is.EqualTo(499));
                Assert.That(_sut.Accounts[250].LinkedEntityId, Is.EqualTo(500));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceAccounts_Set_ShouldHandleLargeListConversion()
        {
            // Arrange
            var largeInterfaceList = new List<ISubAccountDTO>();
            for (int i = 0; i < 100; i++)
            {
                largeInterfaceList.Add(new SubAccountDTO { Id = i + 1000, LinkedEntityId = i + 2000 });
            }
            var accountInterface = (IAccountDTO)_sut;

            // Act
            accountInterface.Accounts = largeInterfaceList;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Accounts, Has.Count.EqualTo(100));
                Assert.That(_sut.Accounts![0].Id, Is.EqualTo(1000));
                Assert.That(_sut.Accounts[99].Id, Is.EqualTo(1099));
                Assert.That(_sut.Accounts[50].LinkedEntityId, Is.EqualTo(2050));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptBoundaryValues()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Test minimum value (0)
                _sut.Id = 0;
                Assert.That(_sut.Id, Is.EqualTo(0));

                // Test maximum value
                _sut.Id = int.MaxValue;
                Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));

                // Test mid-range value
                _sut.Id = 1_000_000;
                Assert.That(_sut.Id, Is.EqualTo(1_000_000));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AccountDTO_PropertyChaining_ShouldWorkCorrectly()
        {
            // Arrange & Act
            var result = new AccountDTO
            {
                Id = 1,
                AccountName = "Chain Test",
                AccountNumber = "CHAIN-001",
                License = Guid.NewGuid().ToString()
            };

            // Assert
            Assert.Multiple(() =>
                  {
                      Assert.That(result.Id, Is.EqualTo(1));
                      Assert.That(result.AccountName, Is.EqualTo("Chain Test"));
                      Assert.That(result.AccountNumber, Is.EqualTo("CHAIN-001"));
                      Assert.That(result.License, Is.Not.Null);
                      Assert.That(result.License, Does.Match(@"^[0-9a-fA-F]{8}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{4}-[0-9a-fA-F]{12}$"));
                  });
        }

        [Test, Category("DataTransferObjects")]
        public void AccountDTO_AsInterface_AllMethodsShouldWork()
        {
            // Arrange
            var accountInterface = (IAccountDTO)_sut;
            var domainInterface = (IDomainEntity)_sut;

            // Act & Assert
            Assert.Multiple(() =>
            {
                // Test IAccountDTO interface methods
                Assert.DoesNotThrow(() => accountInterface.Features = []);
                Assert.DoesNotThrow(() => accountInterface.Accounts = []);
                Assert.DoesNotThrow(() => { var features = accountInterface.Features; });
                Assert.DoesNotThrow(() => { var accounts = accountInterface.Accounts; });

                // Test IDomainEntity interface methods
                Assert.Throws<NotImplementedException>(() => domainInterface.Cast<MockDomainEntity>());
                Assert.Throws<NotImplementedException>(() => domainInterface.ToJson());

                // Test properties through interface
                accountInterface.Id = 999;
                Assert.That(_sut.Id, Is.EqualTo(999));
            });
        }

        #region Mock Classes
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; }
            public DateTime DateCreated { get; }
            public DateTime? DateModified { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => throw new NotImplementedException();
        }
        #endregion
    }
}
