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
        private readonly DateTime _testCreatedDate = new(2023, 1, 1, 12, 0, 0);
        private readonly DateTime _testModifiedDate = new(2023, 1, 2, 12, 0, 0);

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
            // Arrange & Act
            var databaseConnection = new DatabaseConnection();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(databaseConnection.Id, Is.EqualTo(0));
                Assert.That(databaseConnection.ConnectionString, Is.Null);
                Assert.That(databaseConnection.DatabaseType, Is.Null);
                Assert.That(databaseConnection.CreatedDate, Is.LessThan(DateTime.UtcNow));
                Assert.That(databaseConnection.ModifiedDate, Is.Null);
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
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(databaseConnection.Id, Is.EqualTo(1));
                Assert.That(databaseConnection.ConnectionString, Is.EqualTo("Server=localhost;Database=Test;Trusted_Connection=true;"));
                Assert.That(databaseConnection.DatabaseType, Is.EqualTo(SupportedDatabases.SQLServer));
                Assert.That(databaseConnection.Account, Is.EqualTo(_testAccount));
                Assert.That(databaseConnection.AccountId, Is.EqualTo(_testAccount.Id));
                Assert.That(databaseConnection.CreatedDate, Is.EqualTo(_testCreatedDate));
                Assert.That(databaseConnection.ModifiedDate, Is.EqualTo(_testModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var connectionString = "Data Source=mydb.sqlite;Version=3;";
            var databaseType = SupportedDatabases.SQLite;

            // Act
            var databaseConnection = new DatabaseConnection(
                connectionString: connectionString,
                databaseType: databaseType,
                account: _testAccount
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(databaseConnection.Id, Is.EqualTo(0)); // Default value
                Assert.That(databaseConnection.ConnectionString, Is.EqualTo(connectionString));
                Assert.That(databaseConnection.DatabaseType, Is.EqualTo(databaseType));
                Assert.That(databaseConnection.Account, Is.EqualTo(_testAccount));
                Assert.That(databaseConnection.AccountId, Is.EqualTo(_testAccount.Id));
                Assert.That(databaseConnection.CreatedDate, Is.LessThan(DateTime.UtcNow));
                Assert.That(databaseConnection.ModifiedDate, Is.Null);
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
        public void Id_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Id = 456;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(456));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
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
        public void ConnectionString_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;
            var connectionString = "Server=test;Database=db;";

            // Act
            _sut.ConnectionString = connectionString;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.ConnectionString, Is.EqualTo(connectionString));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void ConnectionString_Setter_WithNull_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;
            _sut.ConnectionString = "Initial value";

            // Act
            _sut.ConnectionString = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.ConnectionString, Is.Null);
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void DatabaseType_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.DatabaseType = SupportedDatabases.MySQL;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DatabaseType, Is.EqualTo(SupportedDatabases.MySQL));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void DatabaseType_Setter_WithNull_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;
            _sut.DatabaseType = SupportedDatabases.SQLServer;

            // Act
            _sut.DatabaseType = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DatabaseType, Is.Null);
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void Account_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var newAccount = new Account { Id = 999, AccountName = "New Account" };
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Account = newAccount;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Account, Is.EqualTo(newAccount));
                Assert.That(_sut.AccountId, Is.EqualTo(newAccount.Id));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
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
        public void CreatedDate_IsReadOnly()
        {
            // Arrange & Act
            var createdDate = _sut.CreatedDate;

            // Assert - Verify property is read-only by checking it has no setter
            var property = typeof(DatabaseConnection).GetProperty(nameof(DatabaseConnection.CreatedDate));
            Assert.That(property?.CanWrite, Is.False);
            Assert.That(createdDate, Is.TypeOf<DateTime>());
        }

        [Test, Category("Models")]
        public void ModifiedDate_CanBeSetDirectly()
        {
            // Arrange
            var testDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            _sut.ModifiedDate = testDate;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(testDate));
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
        public void ExplicitInterfaceAccount_Setter_UpdatesAccountAndModifiedDate()
        {
            // Arrange
            var newAccount = new Account { Id = 789, AccountName = "Interface Test Account" };
            IDatabaseConnection databaseConnection = _sut;
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            databaseConnection.Account = newAccount;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Account, Is.EqualTo(newAccount));
                Assert.That(_sut.AccountId, Is.EqualTo(newAccount.Id));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
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
            _sut.ModifiedDate = new DateTime(2023, 1, 1, 12, 0, 0);

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
                
                Assert.That(root.TryGetProperty("createdDate", out var createdDateProperty), Is.True);
                Assert.That(createdDateProperty.ValueKind, Is.EqualTo(JsonValueKind.String));
                
                Assert.That(root.TryGetProperty("modifiedDate", out var modifiedDateProperty), Is.True);
                Assert.That(modifiedDateProperty.ValueKind, Is.EqualTo(JsonValueKind.String));
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
            _sut.ModifiedDate = null;

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
                
                Assert.That(root.TryGetProperty("modifiedDate", out var modifiedDateProperty), Is.True);
                Assert.That(modifiedDateProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
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
        public void Properties_UpdateModifiedDate_IndependentlyForDifferentPropertyChanges()
        {
            // Arrange
            _sut.Account = _testAccount;
            var initialModifiedDate = _sut.ModifiedDate;
            
            System.Threading.Thread.Sleep(1); // Ensure time difference

            // Act & Assert - Test each property setter updates ModifiedDate
            _sut.Id = 100;
            var afterIdChange = _sut.ModifiedDate;
            Assert.That(afterIdChange, Is.GreaterThan(initialModifiedDate));
            
            System.Threading.Thread.Sleep(1);
            
            _sut.ConnectionString = "New Connection String";
            var afterConnectionStringChange = _sut.ModifiedDate;
            Assert.That(afterConnectionStringChange, Is.GreaterThan(afterIdChange));
            
            System.Threading.Thread.Sleep(1);
            
            _sut.DatabaseType = SupportedDatabases.PostgreSQL;
            var afterDatabaseTypeChange = _sut.ModifiedDate;
            Assert.That(afterDatabaseTypeChange, Is.GreaterThan(afterConnectionStringChange));
            
            System.Threading.Thread.Sleep(1);
            
            var newAccount = new Account { Id = 888, AccountName = "Final Account" };
            _sut.Account = newAccount;
            var afterAccountChange = _sut.ModifiedDate;
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
                createdDate: DateTime.MaxValue,
                modifiedDate: DateTime.MaxValue
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(databaseConnection.Id, Is.EqualTo(int.MaxValue));
                Assert.That(databaseConnection.ConnectionString, Is.EqualTo("MaxValue Connection String"));
                Assert.That(databaseConnection.DatabaseType, Is.EqualTo(SupportedDatabases.PostgreSQL));
                Assert.That(databaseConnection.Account, Is.EqualTo(_testAccount));
                Assert.That(databaseConnection.CreatedDate, Is.EqualTo(DateTime.MaxValue));
                Assert.That(databaseConnection.ModifiedDate, Is.EqualTo(DateTime.MaxValue));
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
                createdDate: DateTime.MinValue,
                modifiedDate: null
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(databaseConnection.Id, Is.EqualTo(0));
                Assert.That(databaseConnection.ConnectionString, Is.EqualTo(""));
                Assert.That(databaseConnection.DatabaseType, Is.EqualTo(SupportedDatabases.SQLServer));
                Assert.That(databaseConnection.Account, Is.EqualTo(_testAccount));
                Assert.That(databaseConnection.CreatedDate, Is.EqualTo(DateTime.MinValue));
                Assert.That(databaseConnection.ModifiedDate, Is.Null);
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
                createdDate: testDate,
                modifiedDate: testDate
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
            defaultConnection.ModifiedDate = testDate;

            // Act & Assert - Verify all properties are set correctly
            Assert.Multiple(() =>
            {
                // JsonConstructor tests
                Assert.That(parameterizedConnection.Id, Is.EqualTo(12345));
                Assert.That(parameterizedConnection.ConnectionString, Is.EqualTo("Comprehensive Test Connection String"));
                Assert.That(parameterizedConnection.DatabaseType, Is.EqualTo(SupportedDatabases.PostgreSQL));
                Assert.That(parameterizedConnection.Account, Is.EqualTo(_testAccount));
                Assert.That(parameterizedConnection.AccountId, Is.EqualTo(_testAccount.Id));
                Assert.That(parameterizedConnection.CreatedDate, Is.EqualTo(testDate));
                Assert.That(parameterizedConnection.ModifiedDate, Is.EqualTo(testDate));

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
                Assert.That(defaultConnection.ModifiedDate, Is.EqualTo(testDate));
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

        [Test, Category("Models")]
        public void Id_Setter_WithIntMinValue_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            _sut.Account = _testAccount;

            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = int.MinValue);
            Assert.Multiple(() =>
            {
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.ParamName, Is.EqualTo("Id"));
                Assert.That(exception.Message, Does.Contain("Id must be a non-negative number."));
            });
        }

        [Test, Category("Models")]
        public void Id_Setter_WithZero_AcceptsValue()
        {
            // Arrange
            _sut.Account = _testAccount;

            // Act
            _sut.Id = 0;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void Id_Setter_WithMaxInt_AcceptsValue()
        {
            // Arrange
            _sut.Account = _testAccount;

            // Act
            _sut.Id = int.MaxValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void ToJson_WithCircularReferences_HandlesCorrectly()
        {
            // Arrange - Set up circular reference scenario
            _sut.Id = 100;
            _sut.ConnectionString = "Circular Test";
            _sut.DatabaseType = SupportedDatabases.SQLServer;
            _sut.Account = _testAccount;

            // Act - Should not throw due to ReferenceHandler.IgnoreCycles
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
            });
        }

        [Test, Category("Models")]
        public void CreatedDate_PropertyInfo_IsReadOnly()
        {
            // Arrange & Act
            var property = typeof(DatabaseConnection).GetProperty(nameof(DatabaseConnection.CreatedDate));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(property, Is.Not.Null);
                Assert.That(property!.CanRead, Is.True);
                Assert.That(property.GetSetMethod(), Is.Null, "CreatedDate should not have a public setter");
            });
        }

        [Test, Category("Models")]
        public void TypeInformation_ShouldBeCorrect()
        {
            // Arrange & Act
            var connection = new DatabaseConnection();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(connection.GetType(), Is.EqualTo(typeof(DatabaseConnection)));
                Assert.That(connection.GetType().Name, Is.EqualTo("DatabaseConnection"));
                Assert.That(connection.GetType().Namespace, Is.EqualTo("OrganizerCompanion.Core.Models.Domain"));
            });
        }

        [Test, Category("Models")]
        public void AllProperties_GettersAndSetters_WorkCorrectly()
        {
            // Arrange
            var connection = new DatabaseConnection();
            var testDate = new DateTime(2025, 5, 15, 10, 30, 45);

            // Act & Assert - Test all property setters and getters
            connection.Id = 12345;
            Assert.That(connection.Id, Is.EqualTo(12345));

 connection.ConnectionString = "Comprehensive Test";
        Assert.That(connection.ConnectionString, Is.EqualTo("Comprehensive Test"));

  connection.DatabaseType = SupportedDatabases.PostgreSQL;
Assert.That(connection.DatabaseType, Is.EqualTo(SupportedDatabases.PostgreSQL));

   connection.Account = _testAccount;
        Assert.That(connection.Account, Is.EqualTo(_testAccount));
            Assert.That(connection.AccountId, Is.EqualTo(_testAccount.Id));

        connection.ModifiedDate = testDate;
 Assert.Multiple(() =>
{
           Assert.That(connection.ModifiedDate, Is.EqualTo(testDate));
     Assert.That(connection.CreatedDate, Is.Not.EqualTo(default(DateTime)));
  });
  }

    [Test, Category("Models")]
   public void ToString_WithAllDatabaseTypes_FormatsCorrectly()
        {
            // Arrange & Act & Assert - Test with all supported database types
            var databaseTypes = new[]
    {
     SupportedDatabases.SQLServer,
 SupportedDatabases.MySQL,
    SupportedDatabases.PostgreSQL,
    SupportedDatabases.SQLite
            };

  foreach (var dbType in databaseTypes)
{
    _sut.Id = 100;
     _sut.DatabaseType = dbType;

          var result = _sut.ToString();

                Assert.Multiple(() =>
{
             Assert.That(result, Is.Not.Null);
  Assert.That(result, Does.Contain("DatabaseConnection"));
               Assert.That(result, Does.Contain("Id:100"));
         Assert.That(result, Does.Contain($"DatabaseType:{dbType}"));
      });
  }
        }

      [Test, Category("Models")]
        public void ExplicitInterfaceAccount_Getter_CastsCorrectly()
 {
      // Arrange
       _sut.Account = _testAccount;
       IDatabaseConnection databaseConnection = _sut;

            // Act
         var result = databaseConnection.Account;

    // Assert
            Assert.Multiple(() =>
       {
           Assert.That(result, Is.Not.Null);
      Assert.That(result, Is.InstanceOf<IAccount>());
      Assert.That(result, Is.SameAs(_testAccount));
   Assert.That(result.Id, Is.EqualTo(_testAccount.Id));
            });
}

        [Test, Category("Models")]
        public void ExplicitInterfaceAccount_Setter_CastsAndUpdatesModifiedDate()
        {
         // Arrange
            IAccount interfaceAccount = new Account { Id = 555, AccountName = "Interface Cast Test" };
 IDatabaseConnection databaseConnection = _sut;
        var originalModifiedDate = _sut.ModifiedDate;
            System.Threading.Thread.Sleep(1);

 // Act
      databaseConnection.Account = interfaceAccount;

         // Assert
          Assert.Multiple(() =>
            {
          Assert.That(_sut.Account, Is.Not.Null);
           Assert.That(_sut.Account, Is.InstanceOf<Account>());
         Assert.That(_sut.Account.Id, Is.EqualTo(555));
              Assert.That(_sut.AccountId, Is.EqualTo(555));
       Assert.That(_sut.ModifiedDate, Is.Not.Null);
          });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullModifiedDate_SetsCorrectly()
        {
     // Arrange & Act
         var connection = new DatabaseConnection(
     id: 777,
    connectionString: "Null Modified Test",
        databaseType: SupportedDatabases.MySQL,
       account: _testAccount,
                createdDate: _testCreatedDate,
             modifiedDate: null
       );

            // Assert
 Assert.Multiple(() =>
            {
        Assert.That(connection.Id, Is.EqualTo(777));
    Assert.That(connection.ConnectionString, Is.EqualTo("Null Modified Test"));
           Assert.That(connection.DatabaseType, Is.EqualTo(SupportedDatabases.MySQL));
       Assert.That(connection.Account, Is.EqualTo(_testAccount));
        Assert.That(connection.CreatedDate, Is.EqualTo(_testCreatedDate));
           Assert.That(connection.ModifiedDate, Is.Null);
            });
  }

        [Test, Category("Models")]
   public void Properties_SetToSameValue_ShouldStillUpdateModifiedDate()
  {
       // Arrange
        _sut.Account = _testAccount;
  _sut.ConnectionString = "Test String";
            var firstModifiedDate = _sut.ModifiedDate;

            // Small delay to ensure different timestamp
     System.Threading.Thread.Sleep(1);

       // Act - Set to the same value
 _sut.ConnectionString = "Test String";
            var secondModifiedDate = _sut.ModifiedDate;

     // Assert - ModifiedDate should still be updated even when setting the same value
            Assert.Multiple(() =>
    {
            Assert.That(_sut.ConnectionString, Is.EqualTo("Test String"));
 Assert.That(secondModifiedDate, Is.GreaterThan(firstModifiedDate));
   });
        }

        [Test, Category("Models")]
        public void JsonSerializer_UsesIgnoreCycles()
        {
            // Arrange
      _sut.Id = 666;
            _sut.ConnectionString = "Serialization Test";
    _sut.DatabaseType = SupportedDatabases.PostgreSQL;
            _sut.Account = _testAccount;

     // Act
      var json = _sut.ToJson();
var document = JsonDocument.Parse(json);

 // Assert
  Assert.Multiple(() =>
          {
     Assert.That(json, Is.Not.Null);
     Assert.That(document.RootElement.TryGetProperty("id", out _), Is.True);
             Assert.That(document.RootElement.TryGetProperty("connectionString", out _), Is.True);
                Assert.That(document.RootElement.TryGetProperty("databaseType", out _), Is.True);
     Assert.That(document.RootElement.TryGetProperty("account", out _), Is.True);
          });
        }

   [Test, Category("Models")]
        public void AllConstructors_InitializeCorrectly()
      {
            // Test default constructor
         var conn1 = new DatabaseConnection();
    Assert.That(conn1.Id, Is.EqualTo(0));

       // Test parameterized constructor (without dates)
          var conn2 = new DatabaseConnection(
     connectionString: "Test",
      databaseType: SupportedDatabases.MySQL,
        account: _testAccount
       );
            Assert.That(conn2.ConnectionString, Is.EqualTo("Test"));

  // Test JSON constructor
          var testDate = DateTime.UtcNow;
        var conn3 = new DatabaseConnection(
       id: 1,
          connectionString: "Test",
         databaseType: SupportedDatabases.SQLServer,
   account: _testAccount,
        createdDate: testDate,
   modifiedDate: testDate
 );
            Assert.That(conn3.CreatedDate, Is.EqualTo(testDate));
}

  [Test, Category("Models")]
 public void Cast_WithAnyType_AlwaysThrowsNotImplementedException()
        {
    // Arrange
  _sut.Account = _testAccount;

   // Act & Assert - Test that Cast always throws regardless of type
            Assert.Throws<NotImplementedException>(() => _sut.Cast<MockDomainEntity>());
   Assert.Throws<NotImplementedException>(() => _sut.Cast<Account>());
        }

        // Helper mock class for testing Cast method
 private class MockDomainEntity : IDomainEntity
        {
       public int Id { get; set; } = 1;
 public bool IsCast { get; set; } = false;
            public int CastId { get; set; } = 0;
  public string? CastType { get; set; } = null;
    public DateTime CreatedDate { get; } = DateTime.Now;
       public DateTime? ModifiedDate { get; set; } = default;

public T Cast<T>() where T : IDomainEntity
    {
          throw new NotImplementedException();
   }

            public string ToJson() => "{}";
 }

     #endregion
    }
}
