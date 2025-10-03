using NUnit.Framework;
using OrganizerCompanion.Core.Validation;
using OrganizerCompanion.Core.Validation.Attributes;

namespace OrganizerCompanion.Core.UnitTests.Validation
{
    [TestFixture]
    internal class EmailValidatedAttributeShould
    {
        [Test]
        [Category("Validation")]
        public void Constructor_ShouldSetDefaultErrorMessageAndPattern()
        {
            // Arrange & Act
            var attribute = new EmailValidatedAttribute();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attribute.ErrorMessage, Is.EqualTo("The email address is not in a valid format."));
                Assert.That(attribute.Pattern, Is.EqualTo(RegexValidators.EmailRegexPattern));
            });
        }

        [Test]
        [Category("Validation")]
        [TestCase("test@example.com", true)]
        [TestCase("test.name@example.co.uk", true)]
        [TestCase("test-name@example.com", true)]
        [TestCase("test@sub.domain.com", true)]
        [TestCase("test", false)]
        [TestCase("@example.com", false)]
        [TestCase("test@example", false)]
        [TestCase("test@example..com", false)]
        [TestCase("", false)] // Empty string is not a valid email
        [TestCase(null, true)] // Null is considered valid; RequiredAttribute should handle null checks
        public void IsValid_ShouldReturnCorrectValidationResult(object value, bool expected)
        {
            // Arrange
            var attribute = new EmailValidatedAttribute();

            // Act
            var isValid = attribute.IsValid(value);

            // Assert
            Assert.That(isValid, Is.EqualTo(expected));
        }
    }
}
