using NUnit.Framework;
using OrganizerCompanion.Core.Validation;
using System.Text.RegularExpressions;

namespace OrganizerCompanion.Core.UnitTests.Validation
{
    [TestFixture]
    internal class RegexValidatorsShould
    {
        [Test]
        [Category("Validation")]
        [TestCase("test@example.com", true)]
        [TestCase("test.name@example.co.uk", true)]
        [TestCase("test-name@example.com", true)]
        [TestCase("test_name@example.com", true)]
        [TestCase("test@example.io", true)]
        [TestCase("test@sub.domain.com", true)]
        [TestCase("test@.com", false)]
        [TestCase("test", false)]
        [TestCase("@example.com", false)]
        [TestCase("test@example", false)]
        [TestCase("test@example..com", false)]
        [TestCase("test@.example.com", false)]
        public void ValidateEmailRegex(string email, bool expected)
        {
            // Act
            var isValid = Regex.IsMatch(email, RegexValidators.EmailRegexPattern);

            // Assert
            Assert.That(isValid, Is.EqualTo(expected));
        }

        [Test]
        [Category("Validation")]
        [TestCase("d36ddcfd-5161-4c20-80aa-b312ef161433", true)]
        [TestCase("D36DDCFD-5161-4C20-80AA-B312EF161433", true)]
        [TestCase("00000000-0000-0000-0000-000000000000", true)]
        [TestCase("d36ddcfd-5161-4c20-80aa-b312ef16143", false)] // Too short
        [TestCase("d36ddcfd-5161-4c20-80aa-b312ef161433a", false)] // Too long
        [TestCase("d36ddcfd-5161-4c20-80aa-b312ef16143g", false)] // Invalid character
        [TestCase("d36ddcfd_5161_4c20_80aa_b312ef161433", false)] // Wrong separator
        [TestCase("d36ddcfd51614c2080aab312ef161433", false)] // No separators
        public void ValidateGuidRegex(string guid, bool expected)
        {
            // Act
            var isValid = Regex.IsMatch(guid, RegexValidators.GuidRegexPattern);

            // Assert
            Assert.That(isValid, Is.EqualTo(expected));
        }

        [Test]
        [Category("Validation")]
        [TestCase("Pass1!", true)]
        [TestCase("ValidPass123$", true)]
        [TestCase("Another-Good-Pass1", true)]
        [TestCase("p1!", false)] // Too short
        [TestCase("ThisPasswordIsWayTooLong1!", false)] // Too long
        [TestCase("password123!", false)] // No uppercase
        [TestCase("PASSWORD123!", false)] // No lowercase
        [TestCase("Password!", false)] // No number
        [TestCase("Password123", false)] // No special character
        public void ValidatePasswordRegex(string password, bool expected)
        {
            // Act
            var isValid = Regex.IsMatch(password, RegexValidators.PasswordRegexPattern);

            // Assert
            Assert.That(isValid, Is.EqualTo(expected));
        }

        [Test]
        [Category("Validation")]
        [TestCase("user", true)]
        [TestCase("user-name", true)]
        [TestCase("user_name", true)]
        [TestCase("user.name", true)]
        [TestCase("user123", true)]
        [TestCase("user!@#", true)]
        [TestCase("usr", false)] // Too short
        public void ValidateUserNameRegex(string username, bool expected)
        {
            // Act
            var isValid = Regex.IsMatch(username, RegexValidators.UserNameRegexPattern);

            // Assert
            Assert.That(isValid, Is.EqualTo(expected));
        }

        [Test]
        [Category("Validation")]
        [TestCase("http://example.com", true)]
        [TestCase("https://example.com", true)]
        [TestCase("https://www.example.com", true)]
        [TestCase("http://example.com/path", true)]
        [TestCase("https://example.com/path?query=1", true)]
        [TestCase("ftp://example.com", true)]
        [TestCase("ftps://example.com", true)]
        [TestCase("http://127.0.0.1", true)]
        [TestCase("https://localhost:8080", true)]
        [TestCase("example.com", false)] // No protocol
        [TestCase("htp://example.com", false)] // Misspelled protocol
        [TestCase("http//example.com", false)] // Missing colon
        [TestCase("http:/example.com", false)] // Missing slash
        public void ValidateUrlRegex(string url, bool expected)
        {
            // Act
            var isValid = Regex.IsMatch(url, RegexValidators.UrlRegexPattern);

            // Assert
            Assert.That(isValid, Is.EqualTo(expected));
        }

        [Test]
        [Category("Validation")]
        [TestCase("Server=localhost;Database=mydb;Integrated Security=true;", true)]
        [TestCase("Server=localhost;Database=mydb;User ID=sa;Password=Pass123;", true)]
        [TestCase("Data Source=.\\SQLEXPRESS;Initial Catalog=TestDB;Integrated Security=SSPI;", true)]
        [TestCase("Server=tcp:myserver.database.windows.net,1433;Database=mydb;User ID=user@myserver;Password=Pass123;Encrypt=True;TrustServerCertificate=False;", true)]
        [TestCase("Server=(localdb)\\MSSQLLocalDB;Database=MyApp;Trusted_Connection=True;", true)]
        [TestCase("Data Source=localhost;Initial Catalog=mydb;UID=sa;PWD=Pass123;", true)]
        [TestCase("Server=myserver;Database=mydb;Integrated Security=true;Connection Timeout=30;", true)]
        [TestCase("Server=localhost;Database=mydb;User ID=sa;Password=Pass123;MultipleActiveResultSets=true;", true)]
        [TestCase("Server=localhost;Database=mydb;Integrated Security=true;Application Name=MyApp;", true)]
        [TestCase("Server=localhost;Database=mydb;Integrated Security=true;Pooling=true;Min Pool Size=5;Max Pool Size=100;", true)]
        [TestCase("Server=localhost;Database=mydb;Integrated Security=true;Encrypt=true;TrustServerCertificate=true;", true)]
        [TestCase("Server=localhost;Database=mydb", true)] // No trailing semicolon
        [TestCase("Server=localhost", true)] // Minimal connection string
        [TestCase("", false)] // Empty string
        [TestCase("=localhost;Database=mydb;", false)] // Missing key
        [TestCase("Server=;Database=mydb;", true)] // Empty value (still valid format)
        [TestCase("123Server=localhost;Database=mydb;", false)] // Key starts with number
        [TestCase("Server", false)] // No equals sign
        [TestCase(";Server=localhost;Database=mydb;", false)] // Starts with semicolon
        public void ValidateSQLServerConnectionStringRegex(string connectionString, bool expected)
        {
            // Act
            var isValid = Regex.IsMatch(connectionString, RegexValidators.SQLServerDbConnectionStringRegexPattern);

            // Assert
            Assert.That(isValid, Is.EqualTo(expected), $"Connection string validation failed for: {connectionString}");
        }
    }
}
