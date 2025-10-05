using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;

namespace OrganizerCompanion.Core.UnitTests.Extensions
{
    [TestFixture]
    internal class SupportedDatabaseExtensionsShould
    {
        [Test]
        [Category("Extensions")]
        [TestCase(SupportedDatabases.SQLite, "SQLite")]
        [TestCase(SupportedDatabases.SQLServer, "Microsoft SQL Server")]
        [TestCase(SupportedDatabases.MySQL, "MySQL")]
        [TestCase(SupportedDatabases.PostgreSQL, "PostgreSQL")]
        public void GetName_ReturnsCorrectName_ForSupportedDatabase(SupportedDatabases database, string expectedName)
        {
            // Act
            var result = database.GetName();

            // Assert
            Assert.That(result, Is.EqualTo(expectedName));
        }

        [Test]
        [Category("Extensions")]
        public void GetName_ReturnsStringRepresentation_ForUndefinedDatabase()
        {
            // Arrange
            var undefinedDatabase = (SupportedDatabases)999;

            // Act
            var result = undefinedDatabase.GetName();

            // Assert
            Assert.That(result, Is.EqualTo("999"));
        }

        [Test]
        [Category("Extensions")]
        [TestCase(SupportedDatabases.SQLite, null)]
        [TestCase(SupportedDatabases.SQLServer, 1433)]
        [TestCase(SupportedDatabases.MySQL, 3306)]
        [TestCase(SupportedDatabases.PostgreSQL, 5432)]
        public void GetDefaultPort_ReturnsCorrectPort_ForSupportedDatabase(SupportedDatabases database, int? expectedPort)
        {
            // Act
            var result = database.GetDefaultPort();

            // Assert
            Assert.That(result, Is.EqualTo(expectedPort));
        }

        [Test]
        [Category("Extensions")]
        public void GetDefaultPort_ReturnsNull_ForUndefinedDatabase()
        {
            // Arrange
            var undefinedDatabase = (SupportedDatabases)999;

            // Act
            var result = undefinedDatabase.GetDefaultPort();

            // Assert
            Assert.That(result, Is.Null);
        }
    }
}
