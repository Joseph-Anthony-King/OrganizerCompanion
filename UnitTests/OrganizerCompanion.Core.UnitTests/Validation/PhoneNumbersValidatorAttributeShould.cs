using NUnit.Framework;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Validation;
using OrganizerCompanion.Core.Validation.Attributes;

namespace OrganizerCompanion.Core.UnitTests.Validation
{
    /// <summary>
    /// Unit tests for PhoneNumbersValidator class to achieve 100% code coverage.
    /// Tests constructor initialization, single phone number validation, list validation,
    /// and various edge cases including NANP phone number formats.
    /// </summary>
    [TestFixture]
    internal class PhoneNumbersValidatorAttributeShould
    {
        #region Constructor Tests

        [Test]
        [Category("Validation")]
        public void Constructor_ShouldSetDefaultErrorMessageAndPattern()
        {
            // Arrange & Act
            var attribute = new PhoneNumbersValidator();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(attribute.ErrorMessage, Is.EqualTo("One or more phone numbers are not in a valid format."));
                Assert.That(attribute.Pattern, Is.EqualTo(RegexValidators.NANPPhoneNumberRegexPattern));
            });
        }

        #endregion

        #region Single String Validation Tests

        [Test]
        [Category("Validation")]
        [TestCase("+1-555-123-4567", true)]
        [TestCase("+1 555-123-4567", true)]
        [TestCase("555-123-4567", true)]
        [TestCase("(555) 123-4567", true)]
        [TestCase("555 123 4567", true)]
        [TestCase("5551234567", true)]
        [TestCase("+1-555-123-4567 ext 123", true)]
        [TestCase("+1-555-123-4567 x123", true)]
        [TestCase("+1-555-123-4567 extension 12345", true)]
        [TestCase("+52-55-1234-5678", true)] // Mexico
        [TestCase("+52 55 1234 5678", true)] // Mexico
        [TestCase("123-4567", true)] // 7 digit number
        [TestCase(null, true)] // null is considered valid for optional fields
        public void IsValid_ShouldReturnTrue_ForValidSinglePhoneNumber(object value, bool expected)
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();

            // Act
            var isValid = attribute.IsValid(value);

            // Assert
            Assert.That(isValid, Is.EqualTo(expected));
        }

        [Test]
        [Category("Validation")]
        [TestCase("invalid-phone", false)]
        [TestCase("123", false)]
        [TestCase("abc-def-ghij", false)]
        [TestCase("12", false)]
        
        [TestCase("+44 20 7946 0958", false)] // UK number (not NANP)
        public void IsValid_ShouldReturnFalse_ForInvalidSinglePhoneNumber(object value, bool expected)
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();

            // Act
            var isValid = attribute.IsValid(value);

            // Assert
            Assert.That(isValid, Is.EqualTo(expected));
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForEmptyStringSingleValue()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();

            // Act
            // Empty string as a single value (not in a list) is considered valid by base RegularExpressionAttribute
            var isValid = attribute.IsValid("");

            // Assert
            Assert.That(isValid, Is.True);
        }

        #endregion

        #region List of Strings Validation Tests

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForValidListOfStrings()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<string>
            {
                "+1-555-123-4567",
                "555-123-4567",
                "(555) 123-4567"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_WhenListOfStringsContainsInvalidPhone()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<string>
            {
                "+1-555-123-4567",
                "invalid-phone"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_WhenListOfStringsContainsEmptyString()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<string>
            {
                "+1-555-123-4567",
                ""
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_WhenListOfStringsContainsNull()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<string>
            {
                "+1-555-123-4567",
                null!
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForEmptyListOfStrings()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<string>();

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForValidListOfMixedFormats()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<string>
            {
                "+1-555-123-4567",
                "555-123-4567",
                "(555) 123-4567",
                "555 123 4567",
                "5551234567",
                "+1-555-123-4567 ext 123",
                "+52-55-1234-5678"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_WhenFirstPhoneInListIsInvalid()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<string>
            {
                "invalid",
                "+1-555-123-4567"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_WhenLastPhoneInListIsInvalid()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<string>
            {
                "+1-555-123-4567",
                "555-987-6543",
                "invalid"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_WhenMiddlePhoneInListIsInvalid()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<string>
            {
                "+1-555-123-4567",
                "invalid",
                "555-987-6543"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        #endregion

        #region List of PhoneNumberDTO Validation Tests

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForListOfPhoneNumberDTOs()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<PhoneNumberDTO>
            {
                new() { Phone = "+1-555-123-4567" },
                new() { Phone = "555-987-6543" }
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_ForListOfPhoneNumberDTOsContainsInvalidPhone()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<PhoneNumberDTO>
            {
                new() { Phone = "+1-555-123-4567" },
                new() { Phone = "invalid-phone" }
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_ForListOfPhoneNumberDTOsContainsEmptyString()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<PhoneNumberDTO>
            {
                new() { Phone = "+1-555-123-4567" },
                new() { Phone = "" }
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_ForListOfPhoneNumberDTOsContainsNull()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<PhoneNumberDTO>
            {
                new() { Phone = "+1-555-123-4567" },
                new() { Phone = null }
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForEmptyListOfPhoneNumberDTOs()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<PhoneNumberDTO>();

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.True);
        }

        #endregion

        #region List of Domain PhoneNumber Validation Tests

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForListOfDomainPhoneNumbers()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<PhoneNumber>
            {
                new() { Phone = "+1-555-123-4567" },
                new() { Phone = "555-987-6543" }
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_ForListOfDomainPhoneNumbersContainsInvalidPhone()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<PhoneNumber>
            {
                new() { Phone = "+1-555-123-4567" },
                new() { Phone = "invalid-phone" }
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForEmptyListOfDomainPhoneNumbers()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<PhoneNumber>();

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.True);
        }

        #endregion

        #region NANP Phone Number Format Tests

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForUSPhoneNumbers()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var usPhoneNumbers = new List<string>
            {
                "+1-555-123-4567",
                "+1 (555) 123-4567",
                "(555) 123-4567",
                "555-123-4567",
                "5551234567"
            };

            // Act & Assert
            foreach (var phoneNumber in usPhoneNumbers)
            {
                var isValid = attribute.IsValid(phoneNumber);
                Assert.That(isValid, Is.True, $"Failed for: {phoneNumber}");
            }
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForCanadianPhoneNumbers()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var canadianPhoneNumbers = new List<string>
            {
                "+1-416-555-1234",
                "+1 (416) 555-1234",
                "416-555-1234",
                "(416) 555-1234"
            };

            // Act & Assert
            foreach (var phoneNumber in canadianPhoneNumbers)
            {
                var isValid = attribute.IsValid(phoneNumber);
                Assert.That(isValid, Is.True, $"Failed for: {phoneNumber}");
            }
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForMexicanPhoneNumbers()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var mexicanPhoneNumbers = new List<string>
            {
                "+52-55-1234-5678",
                "+52 55 1234 5678",
                "+52-33-1234-5678", // 2-digit area code
                "+52 (55) 1234-5678"
            };

            // Act & Assert
            foreach (var phoneNumber in mexicanPhoneNumbers)
            {
                var isValid = attribute.IsValid(phoneNumber);
                Assert.That(isValid, Is.True, $"Failed for: {phoneNumber}");
            }
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForPhoneNumbersWithExtensions()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbersWithExtensions = new List<string>
            {
                "+1-555-123-4567 ext 123",
                "+1-555-123-4567 x123",
                "+1-555-123-4567 extension 12345",
                "555-123-4567 ext 1",
                "555-123-4567 x99999"
            };

            // Act & Assert
            foreach (var phoneNumber in phoneNumbersWithExtensions)
            {
                var isValid = attribute.IsValid(phoneNumber);
                Assert.That(isValid, Is.True, $"Failed for: {phoneNumber}");
            }
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForSevenDigitPhoneNumbers()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var sevenDigitPhoneNumbers = new List<string>
            {
                "123-4567",
                "555-1234",
                "987-6543"
            };

            // Act & Assert
            foreach (var phoneNumber in sevenDigitPhoneNumbers)
            {
                var isValid = attribute.IsValid(phoneNumber);
                Assert.That(isValid, Is.True, $"Failed for: {phoneNumber}");
            }
        }

        #endregion

        #region Edge Case Tests

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_ForNonNANPCountryCodes()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var nonNANPPhoneNumbers = new List<string>
            {
                "+44 20 7946 0958", // UK
                "+33 1 42 86 83 26", // France
                "+81-3-1234-5678", // Japan
                "+86 21 1234 5678", // China
                "+49 30 12345678" // Germany
            };

            // Act & Assert
            foreach (var phoneNumber in nonNANPPhoneNumbers)
            {
                var isValid = attribute.IsValid(phoneNumber);
                Assert.That(isValid, Is.False, $"Should be invalid for: {phoneNumber}");
            }
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForPhoneNumbersWithDifferentSeparators()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbersWithDifferentSeparators = new List<string>
            {
                "555-123-4567",
                "555 123 4567",
                "(555) 123-4567",
                "(555)123-4567",
                "+1 555-123-4567",
                "+1-555-123-4567"
            };

            // Act & Assert
            foreach (var phoneNumber in phoneNumbersWithDifferentSeparators)
            {
                var isValid = attribute.IsValid(phoneNumber);
                Assert.That(isValid, Is.True, $"Failed for: {phoneNumber}");
            }
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_ForPhoneNumbersWithInvalidLength()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var invalidLengthPhoneNumbers = new List<string>
            {
                "123",
                "12",
                "1",
                "12345",
                "123456"
            };

            // Act & Assert
            foreach (var phoneNumber in invalidLengthPhoneNumbers)
            {
                var isValid = attribute.IsValid(phoneNumber);
                Assert.That(isValid, Is.False, $"Should be invalid for: {phoneNumber}");
            }
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_ForPhoneNumbersWithLetters()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbersWithLetters = new List<string>
            {
                "abc-def-ghij",
                "555-ABC-DEFG",
                "call-me-now"
            };

            // Act & Assert
            foreach (var phoneNumber in phoneNumbersWithLetters)
            {
                var isValid = attribute.IsValid(phoneNumber);
                Assert.That(isValid, Is.False, $"Should be invalid for: {phoneNumber}");
            }
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForListWithOnlyValidPhoneNumbers()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<string>
            {
                "+1-555-123-4567",
                "+52-55-1234-5678",
                "555-123-4567",
                "(555) 123-4567 ext 123"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_ForListContainingOnlyInvalidPhoneNumbers()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<string>
            {
                "invalid",
                "123",
                "abc"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForSingleValidPhoneNumberInList()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<string>
            {
                "+1-555-123-4567"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_ForSingleInvalidPhoneNumberInList()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<string>
            {
                "invalid"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldHandleIEnumerableOfStrings()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            IEnumerable<string> phoneNumbers = new[]
            {
                "+1-555-123-4567",
                "555-987-6543"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_ForIEnumerableWithInvalidPhone()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            IEnumerable<string> phoneNumbers = new[]
            {
                "+1-555-123-4567",
                "invalid"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldFallbackToBaseValidation_ForNonStringNonEnumerableValue()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var nonStringValue = 12345;

            // Act
            var isValid = attribute.IsValid(nonStringValue);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForMaximumExtensionLength()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumber = "+1-555-123-4567 ext 99999"; // 5 digit extension

            // Act
            var isValid = attribute.IsValid(phoneNumber);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_ForExtensionExceedingMaxLength()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumber = "+1-555-123-4567 ext 999999"; // 6 digit extension (exceeds max of 5)

            // Act
            var isValid = attribute.IsValid(phoneNumber);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForPhoneNumberWithMinimumLength()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumber = "123-4567"; // 7 digits minimum

            // Act
            var isValid = attribute.IsValid(phoneNumber);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForPhoneNumberWithMaximumLength()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumber = "1234567890"; // 10 digits maximum

            // Act
            var isValid = attribute.IsValid(phoneNumber);

            // Assert
            Assert.That(isValid, Is.True);
        }

        #endregion

        #region Integration Tests

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldValidateComplexScenario_WithMixedValidAndInvalidPhones()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            
            // Test 1: All valid
            var allValidPhones = new List<string>
            {
                "+1-555-123-4567",
                "+52-55-1234-5678",
                "555-123-4567",
                "(555) 123-4567 ext 123"
            };

            // Test 2: One invalid in the middle
            var oneInvalidInMiddle = new List<string>
            {
                "+1-555-123-4567",
                "invalid",
                "555-123-4567"
            };

            // Test 3: All invalid
            var allInvalid = new List<string>
            {
                "invalid",
                "123",
                "abc-def-ghij"
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(attribute.IsValid(allValidPhones), Is.True);
                Assert.That(attribute.IsValid(oneInvalidInMiddle), Is.False);
                Assert.That(attribute.IsValid(allInvalid), Is.False);
            });
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldWorkWithLargeListOfPhoneNumbers()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var largeList = new List<string>();
            
            for (int i = 0; i < 1000; i++)
            {
                // Format as 555-0000 to 555-9999 style which matches the NANP pattern
                largeList.Add($"555-{i:D4}");
            }

            // Act
            var isValid = attribute.IsValid(largeList);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldStopValidationOnFirstInvalidPhone()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            var phoneNumbers = new List<string>
            {
                "invalid", // First one is invalid
                "+1-555-123-4567",
                "555-987-6543"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnTrue_ForEmptyIEnumerableOfStrings()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            IEnumerable<string> emptyEnumerable = Array.Empty<string>();

            // Act
            var isValid = attribute.IsValid(emptyEnumerable);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldHandleArrayOfStrings()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            string[] phoneNumbers = 
            {
                "+1-555-123-4567",
                "555-987-6543"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.True);
        }

        [Test]
        [Category("Validation")]
        public void IsValid_ShouldReturnFalse_ForArrayContainingInvalidPhone()
        {
            // Arrange
            var attribute = new PhoneNumbersValidator();
            string[] phoneNumbers = 
            {
                "+1-555-123-4567",
                "invalid"
            };

            // Act
            var isValid = attribute.IsValid(phoneNumbers);

            // Assert
            Assert.That(isValid, Is.False);
        }

        #endregion
    }
}




