using NUnit.Framework;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Validation;
using OrganizerCompanion.Core.Validation.Attributes;

namespace OrganizerCompanion.Core.UnitTests.Validation
{
    [TestFixture]
    internal class EmailsValidatedAttributeShould
    {
        [Test]
        [Category("Validation")]
        public void Constructor_ShouldSetDefaultErrorMessageAndPattern()
        {
            // Arrange & Act
            var attribute = new EmailsValidatorAttribute();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attribute.ErrorMessage, Is.EqualTo("One or more email addresses are not in a valid format."));
                Assert.That(attribute.Pattern, Is.EqualTo(RegexValidators.EmailRegexPattern));
            });
        }

        [Test]
        [Category("Validation")]
        [TestCase("test@example.com", true)]
        [TestCase("test.name@example.co.uk", true)]
        [TestCase("test", false)]
        [TestCase(null, true)]
        public void IsValid_ShouldReturnCorrectValidationResult_ForSingleString(object value, bool expected)
        {
            // Arrange
            var attribute = new EmailsValidatorAttribute();

            // Act
            var isValid = attribute.IsValid(value);

            // Assert
            Assert.That(isValid, Is.EqualTo(expected));
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForValidListOfEmailDTOs()
        {
            // Arrange
            var attribute = new EmailsValidatorAttribute();
            var emails = new List<EmailDTO>
            {
                new() { EmailAddress = "test1@example.com" },
                new() { EmailAddress = "test2@example.com" }
            };

            // Act
            var isValid = attribute.IsValid(emails);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_WhenListOfEmailDTOsContainsInvalidEmail()
        {
            // Arrange
            var attribute = new EmailsValidatorAttribute();
            var emails = new List<EmailDTO>
            {
                new() { EmailAddress = "test1@example.com" },
                new() { EmailAddress = "invalid-email" }
            };

            // Act
            var isValid = attribute.IsValid(emails);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_WhenListOfEmailDTOsContainsEmptyString()
        {
            // Arrange
            var attribute = new EmailsValidatorAttribute();
            var emails = new List<EmailDTO>
            {
                new() { EmailAddress = "test1@example.com" },
                new() { EmailAddress = "" }
            };

            // Act
            var isValid = attribute.IsValid(emails);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_WhenListOfEmailDTOsContainsNull()
        {
            // Arrange
            var attribute = new EmailsValidatorAttribute();
            var emails = new List<EmailDTO>
            {
                new() { EmailAddress = "test1@example.com" },
                new() { EmailAddress = null }
            };

            // Act
            var isValid = attribute.IsValid(emails);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForEmptyListOfEmailDTOs()
        {
            // Arrange
            var attribute = new EmailsValidatorAttribute();
            var emails = new List<EmailDTO>();

            // Act
            var isValid = attribute.IsValid(emails);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForValidListOfDomainEmails()
        {
            // Arrange
            var attribute = new EmailsValidatorAttribute();
            var emails = new List<Email>
            {
                new() { EmailAddress = "test1@example.com" },
                new() { EmailAddress = "test2@example.com" }
            };

            // Act
            var isValid = attribute.IsValid(emails);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_WhenListOfDomainEmailsContainsInvalidEmail()
        {
            // Arrange
            var attribute = new EmailsValidatorAttribute();
            var emails = new List<Email>
            {
                new() { EmailAddress = "test1@example.com" },
                new() { EmailAddress = "invalid-email" }
            };

            // Act
            var isValid = attribute.IsValid(emails);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForEmptyListOfDomainEmails()
        {
            // Arrange
            var attribute = new EmailsValidatorAttribute();
            var emails = new List<Email>();

            // Act
            var isValid = attribute.IsValid(emails);

            // Assert
            Assert.That(isValid, Is.True);
        }
    }
}
