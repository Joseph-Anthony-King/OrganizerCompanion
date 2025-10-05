using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Models.Type;
using OrganizerCompanion.Core.Validation.Attributes;
using System.ComponentModel.DataAnnotations;

namespace OrganizerCompanion.Core.UnitTests.Validation
{
    /// <summary>
    /// Unit tests for DatabaseConnectionStringValidatorAttribute class.
    /// Tests validation of connection strings for different database types.
    /// </summary>
    [TestFixture]
    internal class DatabaseConnectionStringValidatorAttributeShould
    {
        #region SQL Server Connection String Tests

        [Test]
        [Category("Validation")]
        public void ValidateSQLServerConnectionString_WhenValid_ReturnsSuccess()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = SupportedDatabases.SQLServer,
                ConnectionString = "Server=localhost;Database=mydb;Integrated Security=true;"
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        [Category("Validation")]
        public void ValidateSQLServerConnectionString_WithMultipleParameters_ReturnsSuccess()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = SupportedDatabases.SQLServer,
                ConnectionString = "Server=tcp:myserver.database.windows.net,1433;Database=mydb;User ID=user@myserver;Password=Pass123;Encrypt=True;TrustServerCertificate=False;"
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        [Category("Validation")]
        public void ValidateSQLServerConnectionString_WhenInvalid_ReturnsError()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = SupportedDatabases.SQLServer,
                ConnectionString = "=localhost;Database=mydb;" // Missing key
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Does.Contain("SQLServer"));
        }

        #endregion

        #region SQLite Connection String Tests

        [Test]
        [Category("Validation")]
        public void ValidateSQLiteConnectionString_WhenValid_ReturnsSuccess()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = SupportedDatabases.SQLite,
                ConnectionString = "Data Source=mydb.db"
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        [Category("Validation")]
        public void ValidateSQLiteConnectionString_WithOptionalParameters_ReturnsSuccess()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = SupportedDatabases.SQLite,
                ConnectionString = "Data Source=mydb.db;Version=3;Cache=Shared;Mode=ReadWriteCreate"
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        [Category("Validation")]
        public void ValidateSQLiteConnectionString_WhenInvalid_ReturnsError()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = SupportedDatabases.SQLite,
                ConnectionString = "Server=localhost;Database=mydb;" // Wrong format for SQLite
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Does.Contain("SQLite"));
        }

        #endregion

        #region MySQL Connection String Tests

        [Test]
        [Category("Validation")]
        public void ValidateMySQLConnectionString_WhenValid_ReturnsSuccess()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = SupportedDatabases.MySQL,
                ConnectionString = "Server=localhost;Database=mydb;User ID=root;Password=Pass123;"
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        [Category("Validation")]
        public void ValidateMySQLConnectionString_WithPort_ReturnsSuccess()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = SupportedDatabases.MySQL,
                ConnectionString = "Server=localhost;Port=3306;Database=mydb;User ID=root;Password=Pass123;Pooling=True;"
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        #endregion

        #region PostgreSQL Connection String Tests

        [Test]
        [Category("Validation")]
        public void ValidatePostgreSQLConnectionString_WhenValid_ReturnsSuccess()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = SupportedDatabases.PostgreSQL,
                ConnectionString = "Host=localhost;Database=mydb;User ID=postgres;Password=Pass123;"
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        [Category("Validation")]
        public void ValidatePostgreSQLConnectionString_WithPort_ReturnsSuccess()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = SupportedDatabases.PostgreSQL,
                ConnectionString = "Host=localhost;Port=5432;Database=mydb;Username=postgres;Password=Pass123;"
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        #endregion

        #region Null and Empty Tests

        [Test]
        [Category("Validation")]
        public void ValidateConnectionString_WhenNull_ReturnsError()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var context = new ValidationContext(new object());

            // Act
            var result = validator.GetValidationResult(null, context);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Does.Contain("cannot be null"));
        }

        [Test]
        [Category("Validation")]
        public void ValidateConnectionString_WhenDatabaseTypeIsNull_ReturnsError()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = null,
                ConnectionString = "Server=localhost;Database=mydb;"
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Does.Contain("Database type must be specified"));
        }

        [Test]
        [Category("Validation")]
        public void ValidateConnectionString_WhenConnectionStringIsNull_ReturnsError()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = SupportedDatabases.SQLServer,
                ConnectionString = null
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Does.Contain("Connection string cannot be null or empty"));
        }

        [Test]
        [Category("Validation")]
        public void ValidateConnectionString_WhenConnectionStringIsEmpty_ReturnsError()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = SupportedDatabases.SQLServer,
                ConnectionString = ""
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Does.Contain("Connection string cannot be null or empty"));
        }

        [Test]
        [Category("Validation")]
        public void ValidateConnectionString_WhenConnectionStringIsWhitespace_ReturnsError()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = SupportedDatabases.SQLServer,
                ConnectionString = "   "
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Does.Contain("Connection string cannot be null or empty"));
        }

        #endregion

        #region String Fallback Tests

        [Test]
        [Category("Validation")]
        public void ValidateConnectionString_WhenValidStringProvided_ReturnsSuccess()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var connectionString = "Server=localhost;Database=mydb;Integrated Security=true;";
            var context = new ValidationContext(new object());

            // Act
            var result = validator.GetValidationResult(connectionString, context);

            // Assert
            Assert.That(result, Is.EqualTo(ValidationResult.Success));
        }

        [Test]
        [Category("Validation")]
        public void ValidateConnectionString_WhenInvalidStringProvided_ReturnsError()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var connectionString = "InvalidConnectionString";
            var context = new ValidationContext(new object());

            // Act
            var result = validator.GetValidationResult(connectionString, context);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Does.Contain("not in a valid format"));
        }

        [Test]
        [Category("Validation")]
        public void ValidateConnectionString_WhenEmptyStringProvided_ReturnsError()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var connectionString = "";
            var context = new ValidationContext(new object());

            // Act
            var result = validator.GetValidationResult(connectionString, context);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Does.Contain("Connection string cannot be null or empty"));
        }

        #endregion

        #region Wrong Type Tests

        [Test]
        [Category("Validation")]
        public void ValidateConnectionString_WhenWrongTypeProvided_ReturnsError()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var wrongType = 12345;
            var context = new ValidationContext(new object());

            // Act
            var result = validator.GetValidationResult(wrongType, context);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Does.Contain("Invalid value type"));
        }

        #endregion

        #region Cross-Database Validation Tests

        [Test]
        [Category("Validation")]
        public void ValidateConnectionString_WhenSQLServerStringUsedForSQLite_ReturnsError()
        {
            // Arrange
            var validator = new DatabaseConnectionValidatorAttribute();
            var dbConnection = new DatabaseConnection
            {
                DatabaseType = SupportedDatabases.SQLite,
                ConnectionString = "Server=localhost;Database=mydb;Integrated Security=true;"
            };
            var context = new ValidationContext(dbConnection);

            // Act
            var result = validator.GetValidationResult(dbConnection, context);

            // Assert
            Assert.That(result, Is.Not.EqualTo(ValidationResult.Success));
            Assert.That(result?.ErrorMessage, Does.Contain("SQLite"));
        }

        #endregion
    }
}
