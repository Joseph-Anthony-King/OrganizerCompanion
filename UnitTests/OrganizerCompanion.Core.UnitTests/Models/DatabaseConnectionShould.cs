using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class DatabaseConnectionShould
    {
        private DatabaseConnection _sut;
        private Account _testAccount;
        private readonly DateTime _testDateCreated = new(2023, 1, 1, 12, 0, 0);
        private readonly DateTime _testDateModified = new(2023, 1, 2, 12, 0, 0);

        [SetUp]
        public void SetUp()
        {
            _testAccount = new Account
            {
                Id = 123,
                AccountName = "Test Account",
                AccountNumber = "ACC-123",
                License = Guid.NewGuid().ToString()
            };

            _sut = new DatabaseConnection();
        }

        #region Constructor Tests

        [Test, Category("Models")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var databaseConnection = new DatabaseConnection();
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(databaseConnection.Id, Is.EqualTo(0));
                Assert.That(databaseConnection.ConnectionString, Is.Null);
                Assert.That(databaseConnection.DatabaseType, Is.Null);
                Assert.That(databaseConnection.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(databaseConnection.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(databaseConnection.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsAllPropertiesCorrectly()
        {
            // Arrange & Act
            var databaseConnection = new DatabaseConnection(
                id: 1,
                connectionString: "Server=localhost;Database=Test;Trusted_Connection=true;",
                databaseType: SupportedDatabases.SQLServer,
                account: _testAccount,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(databaseConnection.Id, Is.EqualTo(1));
                Assert.That(databaseConnection.ConnectionString, Is.EqualTo("Server=localhost;Database=Test;Trusted_Connection=true;"));
                Assert.That(databaseConnection.DatabaseType, Is.EqualTo(SupportedDatabases.SQLServer));
                Assert.That(databaseConnection.Account, Is.EqualTo(_testAccount));
                Assert.That(databaseConnection.AccountId, Is.EqualTo(_testAccount.Id));
                Assert.That(databaseConnection.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(databaseConnection.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var connectionString = "Data Source=mydb.sqlite;Version=3;";
            var databaseType = SupportedDatabases.SQLite;
            var beforeCreation = DateTime.UtcNow;

            // Act
            var databaseConnection = new DatabaseConnection(
                connectionString: connectionString,
                databaseType: databaseType,
                account: _testAccount
            );
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(databaseConnection.Id, Is.EqualTo(0)); // Default value
                Assert.That(databaseConnection.ConnectionString, Is.EqualTo(connectionString));
                Assert.That(databaseConnection.DatabaseType, Is.EqualTo(databaseType));
                Assert.That(databaseConnection.Account, Is.EqualTo(_testAccount));
                Assert.That(databaseConnection.AccountId, Is.EqualTo(_testAccount.Id));
                Assert.That(databaseConnection.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(databaseConnection.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(databaseConnection.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_WithNullValues_AcceptsNullValues()
        {
            // Arrange & Act
            var databaseConnection = new DatabaseConnection(
                connectionString: null,
                databaseType: null,
                account: _testAccount
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(databaseConnection.ConnectionString, Is.Null);
                Assert.That(databaseConnection.DatabaseType, Is.Null);
                Assert.That(databaseConnection.Account, Is.EqualTo(_testAccount));
                Assert.That(databaseConnection.AccountId, Is.EqualTo(_testAccount.Id));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_WithAllSupportedDatabaseTypes_SetsCorrectly()
        {
            // Arrange & Act & Assert - Test with various database types
            var databaseTypes = new[]
            {
                SupportedDatabases.SQLServer,
                SupportedDatabases.MySQL,
                SupportedDatabases.PostgreSQL,
                SupportedDatabases.SQLite
            };

            foreach (var dbType in databaseTypes)
            {
                var databaseConnection = new DatabaseConnection(
                    connectionString: $"TestConnectionString{dbType}",
                    databaseType: dbType,
                    account: _testAccount
                );

                Assert.That(databaseConnection.DatabaseType, Is.EqualTo(dbType),
                    $"DatabaseType should be set correctly for {dbType}");
            }
        }

        #endregion

        #region Property Tests

        [Test, Category("Models")]
        public void Id_Setter_UpdatesDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.Id = 456;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(456));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void Id_Setter_WithNegativeValue_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = -1);
            Assert.Multiple(() =>
            {
                Assert.That(exception.ParamName, Is.EqualTo("Id"));
                Assert.That(exception.Message, Does.Contain("Id must be a non-negative number."));
            });
        }

        [Test, Category("Models")]
        public void ConnectionString_Setter_UpdatesDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;
            var connectionString = "Server=test;Database=db;";

            // Act
            _sut.ConnectionString = connectionString;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.ConnectionString, Is.EqualTo(connectionString));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void ConnectionString_Setter_WithNull_UpdatesDateModified()
        {
            // Arrange
            _sut.ConnectionString = "Initial value";
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.ConnectionString = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.ConnectionString, Is.Null);
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void DatabaseType_Setter_UpdatesDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.DatabaseType = SupportedDatabases.MySQL;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DatabaseType, Is.EqualTo(SupportedDatabases.MySQL));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void DatabaseType_Setter_WithNull_UpdatesDateModified()
        {
            // Arrange
            _sut.DatabaseType = SupportedDatabases.SQLServer;
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.DatabaseType = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DatabaseType, Is.Null);
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void Account_Setter_UpdatesDateModified()
        {
            // Arrange
            var newAccount = new Account { Id = 999, AccountName = "New Account" };
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.Account = newAccount;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Account, Is.EqualTo(newAccount));
                Assert.That(_sut.AccountId, Is.EqualTo(newAccount.Id));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void AccountId_Getter_ReturnsAccountId()
        {
            // Arrange
            _sut.Account = _testAccount;

            // Act
            var result = _sut.AccountId;

            // Assert
            Assert.That(result, Is.EqualTo(_testAccount.Id));
        }

        [Test, Category("Models")]
        public void DateCreated_IsReadOnly()
        {
            // Arrange & Act
            var dateCreated = _sut.DateCreated;

            // Assert - Verify property is read-only by checking it has no setter
            var property = typeof(DatabaseConnection).GetProperty(nameof(DatabaseConnection.DateCreated));
            Assert.That(property?.CanWrite, Is.False);
            Assert.That(dateCreated, Is.TypeOf<DateTime>());
        }

        [Test, Category("Models")]
        public void DateModified_CanBeSetDirectly()
        {
            // Arrange
            var testDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            _sut.DateModified = testDate;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(testDate));
        }

        #endregion

        #region Explicit Interface Implementation Tests

        [Test, Category("Models")]
        public void ExplicitInterfaceAccount_Getter_ReturnsAccount()
        {
            // Arrange
            _sut.Account = _testAccount;
            IDatabaseConnection databaseConnection = _sut;

            // Act
            var result = databaseConnection.Account;

            // Assert
            Assert.That(result, Is.EqualTo(_testAccount));
        }

        [Test, Category("Models")]
        public void ExplicitInterfaceAccount_Setter_UpdatesAccountAndDateModified()
        {
            // Arrange
            var newAccount = new Account { Id = 789, AccountName = "Interface Test Account" };
            IDatabaseConnection databaseConnection = _sut;
            var originalDateModified = _sut.DateModified;

            // Act
            databaseConnection.Account = newAccount;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Account, Is.EqualTo(newAccount));
                Assert.That(_sut.AccountId, Is.EqualTo(newAccount.Id));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        #endregion

        #region Method Tests

        [Test, Category("Models")]
        public void Cast_ThrowsNotImplementedException()
        {
            // Arrange
            _sut.Id = 1;

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Cast<MockDomainEntity>());
        }

        [Test, Category("Models")]
        public void ToJson_ReturnsValidJsonString()
        {
            // Arrange
            _sut.Id = 1;
            _sut.ConnectionString = "Server=localhost;Database=TestDB;";
            _sut.DatabaseType = SupportedDatabases.SQLServer;
            _sut.Account = _testAccount;
            _sut.DateModified = new DateTime(2023, 1, 1, 12, 0, 0);

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
            var jsonDocument = JsonDocument.Parse(json);
            var root = jsonDocument.RootElement;

            Assert.Multiple(() =>
            {
                Assert.That(root.TryGetProperty("id", out var idProperty), Is.True);
                Assert.That(idProperty.GetInt32(), Is.EqualTo(1));
                
                Assert.That(root.TryGetProperty("connectionString", out var connectionStringProperty), Is.True);
                Assert.That(connectionStringProperty.GetString(), Is.EqualTo("Server=localhost;Database=TestDB;"));
                
                Assert.That(root.TryGetProperty("databaseType", out var databaseTypeProperty), Is.True);
                Assert.That(databaseTypeProperty.GetInt32(), Is.EqualTo((int)SupportedDatabases.SQLServer));
                
                Assert.That(root.TryGetProperty("accountId", out var accountIdProperty), Is.True);
                Assert.That(accountIdProperty.GetInt32(), Is.EqualTo(_testAccount.Id));
                
                Assert.That(root.TryGetProperty("account", out var accountProperty), Is.True);
                Assert.That(accountProperty.ValueKind, Is.EqualTo(JsonValueKind.Object));
                
                Assert.That(root.TryGetProperty("dateCreated", out var dateCreatedProperty), Is.True);
                Assert.That(dateCreatedProperty.ValueKind, Is.EqualTo(JsonValueKind.String));
                
                Assert.That(root.TryGetProperty("dateModified", out var dateModifiedProperty), Is.True);
                Assert.That(dateModifiedProperty.ValueKind, Is.EqualTo(JsonValueKind.String));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithNullValues_HandlesNullsCorrectly()
        {
            // Arrange
            _sut.Id = 2;
            _sut.ConnectionString = null;
            _sut.DatabaseType = null;
            _sut.Account = _testAccount;
            _sut.DateModified = null;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
            });

            var jsonDocument = JsonDocument.Parse(json);
            var root = jsonDocument.RootElement;

            Assert.Multiple(() =>
            {
                Assert.That(root.TryGetProperty("id", out var idProperty), Is.True);
                Assert.That(idProperty.GetInt32(), Is.EqualTo(2));
                
                Assert.That(root.TryGetProperty("connectionString", out var connectionStringProperty), Is.True);
                Assert.That(connectionStringProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
                
                Assert.That(root.TryGetProperty("databaseType", out var databaseTypeProperty), Is.True);
                Assert.That(databaseTypeProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
                
                Assert.That(root.TryGetProperty("dateModified", out var dateModifiedProperty), Is.True);
                Assert.That(dateModifiedProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
            });
        }

        [Test, Category("Models")]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            _sut.Id = 42;
            _sut.DatabaseType = SupportedDatabases.PostgreSQL;

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("DatabaseConnection"));
                Assert.That(result, Does.Contain("Id:42"));
                Assert.That(result, Does.Contain("DatabaseType:PostgreSQL"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithNullDatabaseType_HandlesNullCorrectly()
        {
            // Arrange
            _sut.Id = 99;
            _sut.DatabaseType = null;

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("DatabaseConnection"));
                Assert.That(result, Does.Contain("Id:99"));
                Assert.That(result, Does.Contain("DatabaseType:"));
            });
        }

        #endregion

        #region Edge Cases and Comprehensive Tests

        [Test, Category("Models")]
        public void Properties_UpdateDateModified_IndependentlyForDifferentPropertyChanges()
        {
            // Arrange
            _sut.Account = _testAccount;
            var initialDateModified = _sut.DateModified;
            
            System.Threading.Thread.Sleep(1); // Ensure time difference

            // Act & Assert - Test each property setter updates DateModified
            _sut.Id = 100;
            var afterIdChange = _sut.DateModified;
            Assert.That(afterIdChange, Is.GreaterThan(initialDateModified));
            
            System.Threading.Thread.Sleep(1);
            
            _sut.ConnectionString = "New Connection String";
            var afterConnectionStringChange = _sut.DateModified;
            Assert.That(afterConnectionStringChange, Is.GreaterThan(afterIdChange));
            
            System.Threading.Thread.Sleep(1);
            
            _sut.DatabaseType = SupportedDatabases.PostgreSQL;
            var afterDatabaseTypeChange = _sut.DateModified;
            Assert.That(afterDatabaseTypeChange, Is.GreaterThan(afterConnectionStringChange));
            
            System.Threading.Thread.Sleep(1);
            
            var newAccount = new Account { Id = 888, AccountName = "Final Account" };
            _sut.Account = newAccount;
            var afterAccountChange = _sut.DateModified;
            Assert.That(afterAccountChange, Is.GreaterThan(afterDatabaseTypeChange));
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithMaxIntValues_HandlesCorrectly()
        {
            // Arrange & Act
            var databaseConnection = new DatabaseConnection(
                id: int.MaxValue,
                connectionString: "MaxValue Connection String",
                databaseType: SupportedDatabases.PostgreSQL,
                account: _testAccount,
                dateCreated: DateTime.MaxValue,
                dateModified: DateTime.MaxValue
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(databaseConnection.Id, Is.EqualTo(int.MaxValue));
                Assert.That(databaseConnection.ConnectionString, Is.EqualTo("MaxValue Connection String"));
                Assert.That(databaseConnection.DatabaseType, Is.EqualTo(SupportedDatabases.PostgreSQL));
                Assert.That(databaseConnection.Account, Is.EqualTo(_testAccount));
                Assert.That(databaseConnection.DateCreated, Is.EqualTo(DateTime.MaxValue));
                Assert.That(databaseConnection.DateModified, Is.EqualTo(DateTime.MaxValue));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithMinValues_HandlesCorrectly()
        {
            // Arrange & Act
            var databaseConnection = new DatabaseConnection(
                id: 0,
                connectionString: "",
                databaseType: SupportedDatabases.SQLServer,
                account: _testAccount,
                dateCreated: DateTime.MinValue,
                dateModified: null
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(databaseConnection.Id, Is.EqualTo(0));
                Assert.That(databaseConnection.ConnectionString, Is.EqualTo(""));
                Assert.That(databaseConnection.DatabaseType, Is.EqualTo(SupportedDatabases.SQLServer));
                Assert.That(databaseConnection.Account, Is.EqualTo(_testAccount));
                Assert.That(databaseConnection.DateCreated, Is.EqualTo(DateTime.MinValue));
                Assert.That(databaseConnection.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void DatabaseConnection_ImplementsIDatabaseConnectionInterface()
        {
            // Act & Assert
            Assert.That(_sut, Is.InstanceOf<IDatabaseConnection>());
        }

        [Test, Category("Models")]
        public void DatabaseConnection_ImplementsIDomainEntityInterface()
        {
            // Act & Assert
            Assert.That(_sut, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("Models")]
        public void Properties_CanSetAndGetExtremeValues()
        {
            // Arrange & Act & Assert
            Assert.DoesNotThrow(() =>
            {
                _sut.Id = int.MaxValue;
                _sut.ConnectionString = new string('A', 1000); // Very long string
                _sut.DatabaseType = SupportedDatabases.MySQL;
                _sut.Account = _testAccount;
            });

            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
                Assert.That(_sut.ConnectionString, Has.Length.EqualTo(1000));
                Assert.That(_sut.DatabaseType, Is.EqualTo(SupportedDatabases.MySQL));
                Assert.That(_sut.Account, Is.EqualTo(_testAccount));
            });
        }

        [Test, Category("Models")]
        public void ComprehensiveFunctionalityTest()
        {
            // This comprehensive test verifies all major functionality works together correctly

            // Arrange - Test default constructor
            var defaultConnection = new DatabaseConnection();
            Assert.That(defaultConnection, Is.Not.Null);

            // Test JsonConstructor with all parameters
            var testDate = DateTime.UtcNow;
            var parameterizedConnection = new DatabaseConnection(
                id: 12345,
                connectionString: "Comprehensive Test Connection String",
                databaseType: SupportedDatabases.PostgreSQL,
                account: _testAccount,
                dateCreated: testDate,
                dateModified: testDate
            );

            // Test parameterized constructor
            var simpleConnection = new DatabaseConnection(
                connectionString: "Simple Connection String",
                databaseType: SupportedDatabases.SQLite,
                account: _testAccount
            );

            // Test all property setters and getters
            defaultConnection.Id = 54321;
            defaultConnection.ConnectionString = "Setter Test Connection";
            defaultConnection.DatabaseType = SupportedDatabases.MySQL;
            defaultConnection.Account = _testAccount;
            defaultConnection.DateModified = testDate;

            // Act & Assert - Verify all properties are set correctly
            Assert.Multiple(() =>
            {
                // JsonConstructor tests
                Assert.That(parameterizedConnection.Id, Is.EqualTo(12345));
                Assert.That(parameterizedConnection.ConnectionString, Is.EqualTo("Comprehensive Test Connection String"));
                Assert.That(parameterizedConnection.DatabaseType, Is.EqualTo(SupportedDatabases.PostgreSQL));
                Assert.That(parameterizedConnection.Account, Is.EqualTo(_testAccount));
                Assert.That(parameterizedConnection.AccountId, Is.EqualTo(_testAccount.Id));
                Assert.That(parameterizedConnection.DateCreated, Is.EqualTo(testDate));
                Assert.That(parameterizedConnection.DateModified, Is.EqualTo(testDate));

                // Simple constructor tests
                Assert.That(simpleConnection.Id, Is.EqualTo(0));
                Assert.That(simpleConnection.ConnectionString, Is.EqualTo("Simple Connection String"));
                Assert.That(simpleConnection.DatabaseType, Is.EqualTo(SupportedDatabases.SQLite));
                Assert.That(simpleConnection.Account, Is.EqualTo(_testAccount));
                Assert.That(simpleConnection.AccountId, Is.EqualTo(_testAccount.Id));

                // Property setter tests
                Assert.That(defaultConnection.Id, Is.EqualTo(54321));
                Assert.That(defaultConnection.ConnectionString, Is.EqualTo("Setter Test Connection"));
                Assert.That(defaultConnection.DatabaseType, Is.EqualTo(SupportedDatabases.MySQL));
                Assert.That(defaultConnection.Account, Is.EqualTo(_testAccount));
                Assert.That(defaultConnection.AccountId, Is.EqualTo(_testAccount.Id));
                Assert.That(defaultConnection.DateModified, Is.EqualTo(testDate));
            });

            // Test Cast method throws NotImplementedException
            Assert.Throws<NotImplementedException>(() => defaultConnection.Cast<MockDomainEntity>());

            // Test JSON serialization
            var json = defaultConnection.ToJson();
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
            });

            // Test ToString method
            var stringResult = defaultConnection.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(stringResult, Is.Not.Null);
                Assert.That(stringResult, Does.Contain("DatabaseConnection"));
                Assert.That(stringResult, Does.Contain("Id:54321"));
                Assert.That(stringResult, Does.Contain("DatabaseType:MySQL"));
            });

            // Test explicit interface implementation
            IDatabaseConnection interfaceConnection = defaultConnection;
            Assert.That(interfaceConnection.Account, Is.EqualTo(_testAccount));

            var newAccount = new Account { Id = 999, AccountName = "Interface Test" };
            interfaceConnection.Account = newAccount;
            Assert.That(defaultConnection.Account, Is.EqualTo(newAccount));
            Assert.That(defaultConnection.AccountId, Is.EqualTo(newAccount.Id));
        }

        // Helper mock class for testing Cast method
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; } = 1;
            public bool IsCast { get; set; } = false;
            public int CastId { get; set; } = 0;
            public string? CastType { get; set; } = null;
            public DateTime DateCreated { get; } = DateTime.Now;
            public DateTime? DateModified { get; set; } = DateTime.Now;

            public T Cast<T>() where T : IDomainEntity
            {
                throw new NotImplementedException();
            }

            public string ToJson() => "{}";
        }

        #endregion
    }
}
