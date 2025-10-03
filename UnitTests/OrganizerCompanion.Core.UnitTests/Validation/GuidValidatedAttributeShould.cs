using NUnit.Framework;
using OrganizerCompanion.Core.Validation;
using OrganizerCompanion.Core.Validation.Attributes;

namespace OrganizerCompanion.Core.UnitTests.Validation
{
    [TestFixture]
    internal class GuidValidatedAttributeShould
    {
        [Test]
        [Category("Validation")]
        public void Constructor_ShouldSetDefaultErrorMessageAndPattern()
        {
            // Arrange & Act
            var attribute = new GuidValidatedAttribute();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attribute.ErrorMessage, Is.EqualTo("The GUID is not in a valid format."));
                Assert.That(attribute.Pattern, Is.EqualTo(RegexValidators.GuidRegexPattern));
            });
        }

        [Test]
        [Category("Validation")]
        [TestCase("d36ddcfd-5161-4c20-80aa-b312ef161433", true)]
        [TestCase("D36DDCFD-5161-4C20-80AA-B312EF161433", true)]
        [TestCase("00000000-0000-0000-0000-000000000000", true)]
        [TestCase("d36ddcfd-5161-4c20-80aa-b312ef16143", false)] // Invalid: too short
        [TestCase("d36ddcfd-5161-4c20-80aa-b312ef161433a", false)] // Invalid: too long
        [TestCase("d36ddcfd-5161-4c20-80aa-b312ef16143g", false)] // Invalid: non-hex character
        [TestCase("d36ddcfd51614c2080aab312ef161433", false)] // Invalid: no hyphens
        [TestCase("", false)] // Invalid: empty string
        [TestCase(null, true)] // Valid: null is considered valid for optional fields
        public void IsValid_ShouldReturnCorrectValidationResult(object value, bool expected)
        {
            // Arrange
            var attribute = new GuidValidatedAttribute();

            // Act
            var isValid = attribute.IsValid(value);

            // Assert
            Assert.That(isValid, Is.EqualTo(expected));
        }
    }
}
