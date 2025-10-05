using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Models.Type;
using System.Text.Json;

namespace OrganizerCompanion.Core.UnitTests.Models.Type
{
    /// <summary>
    /// Unit tests for DatabaseConnection class.
    /// Tests serialization, deserialization, and property validation.
    /// </summary>
    [TestFixture]
    internal class DatabaseConnectionShould
    {
        #region Constructor Tests

        [Test]
        [Category("Models")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Arrange & Act
            var dbConnection = new DatabaseConnection();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dbConnection.ConnectionString, Is.Null);
                Assert.That(dbConnection.DatabaseType, Is.Null);
            });
        }

        #endregion

        #region Property Tests

        [Test]
        [Category("Models")]
        public void Properties_CanSetAndGetValues()
        {
            // Arrange
            var dbConnection = new DatabaseConnection();
            var connectionString = "Server=localhost;Database=testdb;Integrated Security=true;";
            var databaseType = SupportedDatabases.SQLServer;

            // Act
            dbConnection.ConnectionString = connectionString;
            dbConnection.DatabaseType = databaseType;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dbConnection.ConnectionString, Is.EqualTo(connectionString));
                Assert.That(dbConnection.DatabaseType, Is.EqualTo(databaseType));
            });
        }

        [Test]
        [Category("Models")]
        public void Properties_CanSetNullValues()
        {
            // Arrange
            var dbConnection = new DatabaseConnection
            {
                ConnectionString = "Server=localhost;Database=test;",
                DatabaseType = SupportedDatabases.MySQL
            };

            // Act
            dbConnection.ConnectionString = null;
            dbConnection.DatabaseType = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dbConnection.ConnectionString, Is.Null);
                Assert.That(dbConnection.DatabaseType, Is.Null);
            });
        }

        #endregion

        #region ToJson Tests

        [Test]
        [Category("Models")]
        public void ToJson_SerializesObjectCorrectly()
        {
            // Arrange
            var dbConnection = new DatabaseConnection
            {
                ConnectionString = "Server=localhost;Database=testdb;User ID=sa;Password=pass123;",
                DatabaseType = SupportedDatabases.SQLServer
            };

            // Act
            var json = dbConnection.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Does.Contain("connectionString"));
            Assert.That(json, Does.Contain("databaseType"));
            Assert.That(json, Does.Contain("Server=localhost"));
            Assert.That(json, Does.Contain("SQLServer"));
        }

        [Test]
        [Category("Models")]
        public void ToJson_HandlesNullValues()
        {
            // Arrange
            var dbConnection = new DatabaseConnection();

            // Act
            var json = dbConnection.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Does.Contain("connectionString"));
            Assert.That(json, Does.Contain("databaseType"));
        }

        [Test]
        [Category("Models")]
        [TestCase(SupportedDatabases.SQLServer)]
        [TestCase(SupportedDatabases.SQLite)]
        [TestCase(SupportedDatabases.MySQL)]
        [TestCase(SupportedDatabases.PostgreSQL)]
        public void ToJson_SerializesAllDatabaseTypes(SupportedDatabases databaseType)
        {
            // Arrange
            var dbConnection = new DatabaseConnection
            {
                ConnectionString = "Server=localhost;Database=test;",
                DatabaseType = databaseType
            };

            // Act
            var json = dbConnection.ToJson();

            // Assert
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Does.Contain(databaseType.ToString()));
        }

        #endregion

        #region FromJson Tests

        [Test]
        [Category("Models")]
        public void FromJson_DeserializesObjectCorrectly()
        {
            // Arrange
            var json = @"{""connectionString"":""Server=localhost;Database=testdb;Integrated Security=true;"",""databaseType"":""SQLServer""}";

            // Act
            var dbConnection = DatabaseConnection.FromJson(json);

            // Assert
            Assert.That(dbConnection, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(dbConnection.ConnectionString, Is.EqualTo("Server=localhost;Database=testdb;Integrated Security=true;"));
                Assert.That(dbConnection.DatabaseType, Is.EqualTo(SupportedDatabases.SQLServer));
            });
        }

        [Test]
        [Category("Models")]
        public void FromJson_HandlesNullValues()
        {
            // Arrange
            var json = @"{""connectionString"":null,""databaseType"":null}";

            // Act
            var dbConnection = DatabaseConnection.FromJson(json);

            // Assert
            Assert.That(dbConnection, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(dbConnection.ConnectionString, Is.Null);
                Assert.That(dbConnection.DatabaseType, Is.Null);
            });
        }

        [Test]
        [Category("Models")]
        [TestCase(SupportedDatabases.SQLServer, "Server=localhost;Database=test;Integrated Security=true;")]
        [TestCase(SupportedDatabases.SQLite, "Data Source=mydb.db;Version=3;")]
        [TestCase(SupportedDatabases.MySQL, "Server=localhost;Database=test;User ID=root;Password=pass;")]
        [TestCase(SupportedDatabases.PostgreSQL, "Host=localhost;Database=test;Username=postgres;Password=pass;")]
        public void FromJson_DeserializesAllDatabaseTypes(SupportedDatabases databaseType, string connectionString)
        {
            // Arrange
            var json = $@"{{""connectionString"":""{connectionString}"",""databaseType"":""{databaseType}""}}";

            // Act
            var dbConnection = DatabaseConnection.FromJson(json);

            // Assert
            Assert.That(dbConnection, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(dbConnection.ConnectionString, Is.EqualTo(connectionString));
                Assert.That(dbConnection.DatabaseType, Is.EqualTo(databaseType));
            });
        }

        [Test]
        [Category("Models")]
        public void FromJson_ReturnsNull_ForInvalidJson()
        {
            // Arrange
            var invalidJson = "This is not valid JSON";

            // Act & Assert
            Assert.Throws<JsonException>(() => DatabaseConnection.FromJson(invalidJson));
        }

        [Test]
        [Category("Models")]
        public void FromJson_ReturnsNull_ForEmptyString()
        {
            // Arrange
            var emptyJson = "";

            // Act & Assert
            Assert.Throws<JsonException>(() => DatabaseConnection.FromJson(emptyJson));
        }

        #endregion

        #region Serialization Round-Trip Tests

        [Test]
        [Category("Models")]
        public void SerializationRoundTrip_PreservesData()
        {
            // Arrange
            var original = new DatabaseConnection
            {
                ConnectionString = "Server=localhost;Database=testdb;User ID=admin;Password=secret;",
                DatabaseType = SupportedDatabases.SQLServer
            };

            // Act
            var json = original.ToJson();
            var deserialized = DatabaseConnection.FromJson(json);

            // Assert
            Assert.That(deserialized, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(deserialized.ConnectionString, Is.EqualTo(original.ConnectionString));
                Assert.That(deserialized.DatabaseType, Is.EqualTo(original.DatabaseType));
            });
        }

        [Test]
        [Category("Models")]
        [TestCase(SupportedDatabases.SQLServer)]
        [TestCase(SupportedDatabases.SQLite)]
        [TestCase(SupportedDatabases.MySQL)]
        [TestCase(SupportedDatabases.PostgreSQL)]
        public void SerializationRoundTrip_WorksForAllDatabaseTypes(SupportedDatabases databaseType)
        {
            // Arrange
            var original = new DatabaseConnection
            {
                ConnectionString = "Server=test;Database=db;",
                DatabaseType = databaseType
            };

            // Act
            var json = original.ToJson();
            var deserialized = DatabaseConnection.FromJson(json);

            // Assert
            Assert.That(deserialized, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(deserialized.ConnectionString, Is.EqualTo(original.ConnectionString));
                Assert.That(deserialized.DatabaseType, Is.EqualTo(original.DatabaseType));
            });
        }

        [Test]
        [Category("Models")]
        public void SerializationRoundTrip_PreservesNullValues()
        {
            // Arrange
            var original = new DatabaseConnection
            {
                ConnectionString = null,
                DatabaseType = null
            };

            // Act
            var json = original.ToJson();
            var deserialized = DatabaseConnection.FromJson(json);

            // Assert
            Assert.That(deserialized, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(deserialized.ConnectionString, Is.Null);
                Assert.That(deserialized.DatabaseType, Is.Null);
            });
        }

        [Test]
        [Category("Models")]
        public void SerializationRoundTrip_HandlesComplexConnectionStrings()
        {
            // Arrange
            var original = new DatabaseConnection
            {
                ConnectionString = "Server=tcp:myserver.database.windows.net,1433;Database=mydb;User ID=user@myserver;Password=P@ssw0rd!;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;",
                DatabaseType = SupportedDatabases.SQLServer
            };

            // Act
            var json = original.ToJson();
            var deserialized = DatabaseConnection.FromJson(json);

            // Assert
            Assert.That(deserialized, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(deserialized.ConnectionString, Is.EqualTo(original.ConnectionString));
                Assert.That(deserialized.DatabaseType, Is.EqualTo(original.DatabaseType));
            });
        }

        #endregion

        #region Edge Case Tests

        [Test]
        [Category("Models")]
        public void ToJson_HandlesSpecialCharactersInConnectionString()
        {
            // Arrange
            var dbConnection = new DatabaseConnection
            {
                ConnectionString = @"Server=localhost;Database=test;Password=""P@ss;w0rd"";",
                DatabaseType = SupportedDatabases.SQLServer
            };

            // Act
            var json = dbConnection.ToJson();
            var deserialized = DatabaseConnection.FromJson(json);

            // Assert
            Assert.That(deserialized, Is.Not.Null);
            Assert.That(deserialized.ConnectionString, Is.EqualTo(dbConnection.ConnectionString));
        }

        [Test]
        [Category("Models")]
        public void FromJson_HandlesExtraProperties()
        {
            // Arrange
            var json = @"{""connectionString"":""Server=localhost;"",""databaseType"":""SQLServer"",""extraProperty"":""ignored""}";

            // Act
            var dbConnection = DatabaseConnection.FromJson(json);

            // Assert
            Assert.That(dbConnection, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(dbConnection.ConnectionString, Is.EqualTo("Server=localhost;"));
                Assert.That(dbConnection.DatabaseType, Is.EqualTo(SupportedDatabases.SQLServer));
            });
        }

        [Test]
        [Category("Models")]
        public void FromJson_HandlesMissingProperties()
        {
            // Arrange
            var json = @"{""connectionString"":""Server=localhost;""}";

            // Act
            var dbConnection = DatabaseConnection.FromJson(json);

            // Assert
            Assert.That(dbConnection, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(dbConnection.ConnectionString, Is.EqualTo("Server=localhost;"));
                Assert.That(dbConnection.DatabaseType, Is.Null);
            });
        }

        #endregion
    }
}
