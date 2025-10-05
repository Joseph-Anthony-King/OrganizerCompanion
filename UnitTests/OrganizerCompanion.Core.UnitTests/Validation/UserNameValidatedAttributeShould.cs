using NUnit.Framework;
using OrganizerCompanion.Core.Validation;
using OrganizerCompanion.Core.Validation.Attributes;

namespace OrganizerCompanion.Core.UnitTests.Validation
{
    [TestFixture]
    internal class UserNameValidatedAttributeShould
    {
        [Test]
        [Category("Validation")]
        public void Constructor_ShouldSetDefaultErrorMessageAndPattern()
        {
            // Arrange & Act
            var attribute = new UserNameValidatorAttribute();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attribute.ErrorMessage, Is.EqualTo("The username is not in a valid format."));
                Assert.That(attribute.Pattern, Is.EqualTo(RegexValidators.UserNameRegexPattern));
            });
        }

        [Test]
        [Category("Validation")]
        [TestCase("user", true)]
        [TestCase("user-name", true)]
        [TestCase("user_123", true)]
        [TestCase("user.name", true)]
        [TestCase("usr", false)] // Invalid: too short
        [TestCase("user name", false)] // Invalid: contains a space
        [TestCase("", false)] // Invalid: empty string
        [TestCase(null, true)] // Valid: null is considered valid for optional fields
        public void IsValid_ShouldReturnCorrectValidationResult(object value, bool expected)
        {
            // Arrange
            var attribute = new UserNameValidatorAttribute();

            // Act
            var isValid = attribute.IsValid(value);

            // Assert
            Assert.That(isValid, Is.EqualTo(expected));
        }
    }
}
