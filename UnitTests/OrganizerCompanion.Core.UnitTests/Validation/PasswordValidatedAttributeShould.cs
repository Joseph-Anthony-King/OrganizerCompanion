using NUnit.Framework;
using OrganizerCompanion.Core.Validation;
using OrganizerCompanion.Core.Validation.Attributes;

namespace OrganizerCompanion.Core.UnitTests.Validation
{
    [TestFixture]
    internal class PasswordValidatedAttributeShould
    {
        [Test]
        [Category("Validation")]
        public void Constructor_ShouldSetDefaultErrorMessageAndPattern()
        {
            // Arrange & Act
            var attribute = new PasswordValidatorAttribute();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attribute.ErrorMessage, Is.EqualTo("The password must be from 4 and up through 20 characters with at least 1 upper case letter, 1 lower case letter, 1 numeric character, and 1 special character of ! @ # $ % ^ & * + = ? - _ . ,"));
                Assert.That(attribute.Pattern, Is.EqualTo(RegexValidators.PasswordRegexPattern));
            });
        }

        [Test]
        [Category("Validation")]
        [TestCase("ValidPass1!", true)]
        [TestCase("Vp1!", true)] // Minimum length
        [TestCase("ValidPassword123456!", true)] // Maximum length
        [TestCase("Vp1", false)] // Too short
        [TestCase("ThisIsAVeryLongPassword1!", false)] // Too long
        [TestCase("validpass1!", false)] // No uppercase letter
        [TestCase("VALIDPASS1!", false)] // No lowercase letter
        [TestCase("ValidPassword!", false)] // No digit
        [TestCase("ValidPassword1", false)] // No special character
        [TestCase("", false)] // Empty string
        [TestCase(null, true)] // Null is considered valid for optional fields
        public void IsValid_ShouldReturnCorrectValidationResult(object value, bool expected)
        {
            // Arrange
            var attribute = new PasswordValidatorAttribute();

            // Act
            var isValid = attribute.IsValid(value);

            // Assert
            Assert.That(isValid, Is.EqualTo(expected));
        }
    }
}
