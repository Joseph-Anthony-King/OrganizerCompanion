using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class PhoneNumberShould
    {
        private PhoneNumber _sut;
        private DateTime _testDateCreated;
        private DateTime _testDateModified;

        [SetUp]
        public void SetUp()
        {
            _sut = new PhoneNumber();
            _testDateCreated = new DateTime(2023, 1, 1, 12, 0, 0);
            _testDateModified = new DateTime(2023, 1, 2, 12, 0, 0);
        }

        [TearDown]
        public void TearDown()
        {
            _sut = null!;
        }

        [Test, Category("Models")]
        public void DefaultConstructor_ShouldCreatePhoneNumberWithDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new PhoneNumber();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.Phone, Is.Null);
                Assert.That(_sut.Type, Is.Null);
                Assert.That(_sut.IsCast, Is.False);
                Assert.That(_sut.CastId, Is.EqualTo(0));
                Assert.That(_sut.CastType, Is.Null);
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_ShouldCreatePhoneNumberWithProvidedValues()
        {
            // Arrange
            var id = 123;
            var phone = "+1-555-123-4567";
            var type = OrganizerCompanion.Core.Enums.Types.Work;
            var isCast = true;
            var castId = 456;
            var castType = "Organization";
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-2);

            // Act
            var phoneNumber = new PhoneNumber(
                id, 
                phone, 
                type, 
                dateCreated, 
                dateModified, 
                isCast, 
                castId, 
                castType);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(phoneNumber.Id, Is.EqualTo(id));
                Assert.That(phoneNumber.Phone, Is.EqualTo(phone));
                Assert.That(phoneNumber.Type, Is.EqualTo(type));
                Assert.That(phoneNumber.IsCast, Is.EqualTo(isCast));
                Assert.That(phoneNumber.CastId, Is.EqualTo(castId));
                Assert.That(phoneNumber.CastType, Is.EqualTo(castType));
                Assert.That(phoneNumber.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(phoneNumber.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void Id_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newId = 999;
            var beforeSet = DateTime.Now;

            // Act
            _sut.Id = newId;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(newId));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void Phone_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newPhone = "+1-555-987-6543";
            var beforeSet = DateTime.Now;

            // Act
            _sut.Phone = newPhone;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Phone, Is.EqualTo(newPhone));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void Phone_WhenSetToNull_ShouldUpdateDateModified()
        {
            // Arrange
            _sut.Phone = "+1-555-123-4567";
            var beforeSet = DateTime.Now;

            // Act
            _sut.Phone = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Phone, Is.Null);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void Type_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newType = OrganizerCompanion.Core.Enums.Types.Cell;
            var beforeSet = DateTime.Now;

            // Act
            _sut.Type = newType;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Type, Is.EqualTo(newType));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void Type_WhenSetToNull_ShouldUpdateDateModified()
        {
            // Arrange
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
            var beforeSet = DateTime.Now;

            // Act
            _sut.Type = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Type, Is.Null);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void IsCast_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.IsCast = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsCast, Is.True);
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void CastId_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newCastId = 789;
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.CastId = newCastId;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.CastId, Is.EqualTo(newCastId));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void CastType_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newCastType = "Organization";
            var originalDateModified = _sut.DateModified;

            // Act
            _sut.CastType = newCastType;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.CastType, Is.EqualTo(newCastType));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void CastType_WhenSetToNull_ShouldUpdateDateModified()
        {
            // Arrange
            _sut.CastType = "Person";
            var beforeSet = DateTime.Now;

            // Act
            _sut.CastType = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.CastType, Is.Null);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void DateCreated_IsReadOnly_AndSetDuringConstruction()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var phoneNumber = new PhoneNumber();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(phoneNumber.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(phoneNumber.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsDateCreatedFromParameter()
        {
            // Arrange
            var specificDate = DateTime.Now.AddDays(-10);

            // Act
            var phoneNumber = new PhoneNumber(1, "555-1234", OrganizerCompanion.Core.Enums.Types.Home, specificDate, null);

            // Assert
            Assert.That(phoneNumber.DateCreated, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void DateModified_CanBeSetAndRetrieved()
        {
            // Arrange
            var dateModified = DateTime.Now.AddHours(-2);

            // Act
            _sut.DateModified = dateModified;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(dateModified));
        }

        [Test, Category("Models")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Cast<PhoneNumber>());
        }

        [Test, Category("Models")]
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Phone = "+1-555-123-4567";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("phone").And.Contains("555-123-4567")); // Allow for Unicode escaping
                Assert.That(json, Does.Contain("\"type\":1")); // Work enum value is 1
                Assert.That(json, Does.Contain("\"isCast\":false"));
                Assert.That(json, Does.Contain("\"dateCreated\":"));
                Assert.That(json, Does.Contain("\"dateModified\":"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithNullValues_ShouldReturnValidJsonString()
        {
            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":0"));
                Assert.That(json, Does.Contain("\"phone\":null"));
                Assert.That(json, Does.Contain("\"type\":null"));
                Assert.That(json, Does.Contain("\"isCast\":false"));
                Assert.That(json, Does.Contain("\"dateCreated\":"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithCastProperties_ShouldIncludeFields()
        {
            // Arrange
            _sut = new PhoneNumber(
                id: 123,
                phone: "+1-555-123-4567",
                type: OrganizerCompanion.Core.Enums.Types.Work,
                isCast: true,
                castId: 456,
                castType: "Organization",
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"isCast\":true"));
                Assert.That(json, Does.Contain("\"castId\":456"));
                Assert.That(json, Does.Contain("\"castType\":\"Organization\""));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithDefaultCastProperties_ShouldHandleCorrectly()
        {
            // Arrange
            _sut = new PhoneNumber(
                id: 123,
                phone: "+1-555-123-4567",
                type: OrganizerCompanion.Core.Enums.Types.Work,
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"isCast\":false"));
                // CastId should be omitted when 0 due to JsonIgnore condition
                Assert.That(json, Does.Not.Contain("\"castId\""));
                // CastType should be omitted when null due to JsonIgnore condition
                Assert.That(json, Does.Not.Contain("\"castType\""));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Phone = "+1-555-123-4567";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("123"));
                Assert.That(result, Does.Contain("+1-555-123-4567"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithNullPhone_ShouldHandleNullValues()
        {
            // Arrange
            _sut.Id = 456;

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("456"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithDefaultValues_ShouldReturnFormattedString()
        {
            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("0"));
            });
        }

        [Test, Category("Models")]
        public void PhoneNumber_WithAllTypesEnumValues_ShouldBeAllowed()
        {
            // Test all enum values
            foreach (OrganizerCompanion.Core.Enums.Types type in Enum.GetValues<OrganizerCompanion.Core.Enums.Types>())
            {
                // Act
                _sut.Type = type;

                // Assert
                Assert.That(_sut.Type, Is.EqualTo(type), $"Failed for type: {type}");
            }
        }

        [Test, Category("Models")]
        public void PhoneNumber_WithEmptyPhone_ShouldBeAllowed()
        {
            // Act
            _sut.Phone = string.Empty;

            // Assert
            Assert.That(_sut.Phone, Is.EqualTo(string.Empty));
        }

        [Test, Category("Models")]
        public void PhoneNumber_WithVeryLongPhone_ShouldBeAllowed()
        {
            // Arrange
            var longPhone = new string('1', 1000);

            // Act
            _sut.Phone = longPhone;

            // Assert
            Assert.That(_sut.Phone, Is.EqualTo(longPhone));
        }

        [Test, Category("Models")]
        public void PhoneNumber_MultiplePropertyChanges_ShouldUpdateDateModifiedEachTime()
        {
            // Arrange
            var initialTime = DateTime.Now;

            // Act & Assert
            System.Threading.Thread.Sleep(1); // Ensure time difference
            _sut.Id = 1;
            var firstModified = _sut.DateModified;
            Assert.That(firstModified, Is.GreaterThanOrEqualTo(initialTime));

            System.Threading.Thread.Sleep(1);
            _sut.Phone = "+1-555-123-4567";
            var secondModified = _sut.DateModified;
            Assert.That(secondModified, Is.GreaterThan(firstModified));

            System.Threading.Thread.Sleep(1);
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
            var thirdModified = _sut.DateModified;
            Assert.That(thirdModified, Is.GreaterThan(secondModified));

            System.Threading.Thread.Sleep(1);
            _sut.IsCast = true;
            var fourthModified = _sut.DateModified;
            Assert.That(fourthModified, Is.GreaterThan(thirdModified));

            System.Threading.Thread.Sleep(1);
            _sut.CastId = 123;
            var fifthModified = _sut.DateModified;
            Assert.That(fifthModified, Is.GreaterThan(fourthModified));

            System.Threading.Thread.Sleep(1);
            _sut.CastType = "Person";
            var sixthModified = _sut.DateModified;
            Assert.That(sixthModified, Is.GreaterThan(fifthModified));
        }

        [Test, Category("Models")]
        public void PhoneNumber_WithZeroId_ShouldBeAllowed()
        {
            // Act
            _sut.Id = 0;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void PhoneNumber_WithMaxIntId_ShouldBeAllowed()
        {
            // Act
            _sut.Id = int.MaxValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void PhoneNumber_WithNegativeId_ShouldBeAllowed()
        {
            // Act
            _sut.Id = -1;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(-1));
        }

        [Test, Category("Models")]
        public void PhoneNumber_WithInternationalFormats_ShouldBeAllowed()
        {
            // Test various international phone formats
            var phoneFormats = new[]
            {
                "+1-555-123-4567",      // US format
                "+44 20 7946 0958",     // UK format
                "+49 30 12345678",      // German format
                "+81-3-1234-5678",      // Japanese format
                "+86 138 0013 8000",    // Chinese format
                "555-1234",             // Simple format
                "(555) 123-4567",       // US with parentheses
                "555.123.4567",         // Dot notation
                "5551234567"            // No formatting
            };

            foreach (var phoneFormat in phoneFormats)
            {
                // Act
                _sut.Phone = phoneFormat;

                // Assert
                Assert.That(_sut.Phone, Is.EqualTo(phoneFormat), $"Failed for phone format: {phoneFormat}");
            }
        }

        [Test, Category("Models")]
        public void PhoneNumber_WithSpecialCharacters_ShouldBeAllowed()
        {
            // Arrange
            var specialPhone = "+1-(555)-123-4567 ext. 1234";

            // Act
            _sut.Phone = specialPhone;

            // Assert
            Assert.That(_sut.Phone, Is.EqualTo(specialPhone));
        }

        [Test, Category("Models")]
        public void PhoneNumber_WithWhitespace_ShouldBeAllowed()
        {
            // Arrange
            var phoneWithSpaces = " +1 555 123 4567 ";

            // Act
            _sut.Phone = phoneWithSpaces;

            // Assert
            Assert.That(_sut.Phone, Is.EqualTo(phoneWithSpaces));
        }

        [Test, Category("Models")]
        public void PhoneNumber_SerializerOptions_ShouldHandleCyclicalReferences()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Phone = "+1-555-123-4567";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Cell;

            // Act & Assert - This should not throw due to ReferenceHandler.IgnoreCycles
            Assert.DoesNotThrow(() =>
            {
                var json = _sut.ToJson();
                Assert.That(json, Is.Not.Null.And.Not.Empty);
            });
        }

        [Test, Category("Models")]
        public void PhoneNumber_JsonSerialization_ShouldProduceValidFormat()
        {
            // Arrange
            _sut.Id = 42;
            _sut.Phone = "+1-555-TESTING";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Fax;
            _sut.DateModified = DateTime.Now;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);

                // Check that all expected properties are present
                var expectedProperties = new[] {
                    "id", "phone", "type", "isCast", "dateCreated", "dateModified"
                };

                foreach (var property in expectedProperties)
                {
                    Assert.That(json, Does.Contain($"\"{property}\":"), $"Property '{property}' not found in JSON");
                }

                // Verify JSON is well-formed
                var jsonDoc = JsonDocument.Parse(json);
                Assert.That(jsonDoc, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void PhoneNumber_WithNonStandardCharacters_ShouldBeAllowed()
        {
            // Arrange
            var phoneWithUnicode = "☎ +1-555-123-4567 📞";

            // Act
            _sut.Phone = phoneWithUnicode;

            // Assert
            Assert.That(_sut.Phone, Is.EqualTo(phoneWithUnicode));
        }

        [Test, Category("Models")]
        public void PhoneNumber_WithOnlyNumbers_ShouldBeAllowed()
        {
            // Arrange
            var numericPhone = "15551234567";

            // Act
            _sut.Phone = numericPhone;

            // Assert
            Assert.That(_sut.Phone, Is.EqualTo(numericPhone));
        }

        [Test, Category("Models")]
        public void PhoneNumber_WithExtension_ShouldBeAllowed()
        {
            // Arrange
            var phoneWithExt = "+1-555-123-4567 x1234";

            // Act
            _sut.Phone = phoneWithExt;

            // Assert
            Assert.That(_sut.Phone, Is.EqualTo(phoneWithExt));
        }

        [Test, Category("Models")]
        public void PhoneNumber_TypeEnumValues_ShouldMatchExpectedValues()
        {
            // Verify the Types enum values are as expected
            var expectedTypes = new[]
            {
                (OrganizerCompanion.Core.Enums.Types.Home, 0),
                (OrganizerCompanion.Core.Enums.Types.Work, 1),
                (OrganizerCompanion.Core.Enums.Types.Cell, 2),
                (OrganizerCompanion.Core.Enums.Types.Fax, 3),
                (OrganizerCompanion.Core.Enums.Types.Billing, 4),
                (OrganizerCompanion.Core.Enums.Types.Other, 5)
            };

            foreach (var (type, expectedValue) in expectedTypes)
            {
                Assert.That((int)type, Is.EqualTo(expectedValue), $"Enum value mismatch for {type}");
            }
        }

        [Test, Category("Models")]
        public void CastProperties_CanBeSetMultipleTimes()
        {
            // Arrange & Act - IsCast
            _sut.IsCast = true;
            Assert.That(_sut.IsCast, Is.True);

            _sut.IsCast = false;
            Assert.That(_sut.IsCast, Is.False);

            // Act - CastId
            _sut.CastId = 100;
            Assert.That(_sut.CastId, Is.EqualTo(100));

            _sut.CastId = 200;
            Assert.That(_sut.CastId, Is.EqualTo(200));

            // Act - CastType
            _sut.CastType = "Organization";
            Assert.That(_sut.CastType, Is.EqualTo("Organization"));

            _sut.CastType = "Person";
            Assert.That(_sut.CastType, Is.EqualTo("Person"));
        }

        [Test, Category("Models")]
        public void CastId_WithNegativeValue_ShouldBeAllowed()
        {
            // Act
            _sut.CastId = -1;

            // Assert
            Assert.That(_sut.CastId, Is.EqualTo(-1));
        }

        [Test, Category("Models")]
        public void CastId_WithMaxValue_ShouldBeAllowed()
        {
            // Act
            _sut.CastId = int.MaxValue;

            // Assert
            Assert.That(_sut.CastId, Is.EqualTo(int.MaxValue));
        }
    }
}
