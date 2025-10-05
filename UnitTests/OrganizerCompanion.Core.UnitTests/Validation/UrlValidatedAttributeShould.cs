using NUnit.Framework;
using OrganizerCompanion.Core.Validation;
using OrganizerCompanion.Core.Validation.Attributes;

namespace OrganizerCompanion.Core.UnitTests.Validation
{
    [TestFixture]
    internal class UrlValidatedAttributeShould
    {
        [Test]
        [Category("Validation")]
        public void Constructor_ShouldSetDefaultErrorMessageAndPattern()
        {
            // Arrange & Act
            var attribute = new UrlValidatorAttribute();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attribute.ErrorMessage, Is.EqualTo("The URL is not in a valid format."));
                Assert.That(attribute.Pattern, Is.EqualTo(RegexValidators.UrlRegexPattern));
            });
        }

        [Test]
        [Category("Validation")]
        [TestCase("http://example.com", true)]
        [TestCase("https://www.example.com", true)]
        [TestCase("ftp://example.com/path", true)]
        [TestCase("ftps://example.com/path?query=value", true)]
        [TestCase("http://127.0.0.1:8080", true)]
        [TestCase("example.com", false)] // Invalid: no protocol
        [TestCase("htp://example.com", false)] // Invalid: misspelled protocol
        [TestCase("http//example.com", false)] // Invalid: missing colon
        [TestCase("", false)] // Invalid: empty string
        [TestCase(null, true)] // Valid: null is considered valid for optional fields
        public void IsValid_ShouldReturnCorrectValidationResult(object value, bool expected)
        {
            // Arrange
            var attribute = new UrlValidatorAttribute();

            // Act
            var isValid = attribute.IsValid(value);

            // Assert
            Assert.That(isValid, Is.EqualTo(expected));
        }
    }
}