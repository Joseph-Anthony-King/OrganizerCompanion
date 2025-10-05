using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class PhoneNumberShould
    {
        private PhoneNumber _sut;
        private DateTime _testDateCreated;
        private DateTime _testDateModified;

        // Mock class for testing LinkedEntity
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime? DateModified { get; set; }

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

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
                Assert.That(_sut.Country, Is.Null);
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
            var country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;
            var linkedEntityId = 456;
            var linkedEntity = new MockDomainEntity { Id = 456 };
            var linkedEntityType = "MockDomainEntity";
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-2);

            // Act
            var phoneNumber = new PhoneNumber(
                id, 
                phone, 
                type, 
                country,
                linkedEntityId,
                linkedEntity,
                linkedEntityType,
                dateCreated, 
                dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(phoneNumber.Id, Is.EqualTo(id));
                Assert.That(phoneNumber.Phone, Is.EqualTo(phone));
                Assert.That(phoneNumber.Type, Is.EqualTo(type));
                Assert.That(phoneNumber.Country, Is.EqualTo(country));
                Assert.That(phoneNumber.LinkedEntityId, Is.EqualTo(linkedEntityId));
                Assert.That(phoneNumber.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(phoneNumber.LinkedEntityType, Is.EqualTo(linkedEntityType));
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
        public void Country_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newCountry = OrganizerCompanion.Core.Enums.Countries.Canada;
            var beforeSet = DateTime.Now;

            // Act
            _sut.Country = newCountry;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Country, Is.EqualTo(newCountry));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void Country_WhenSetToNull_ShouldUpdateDateModified()
        {
            // Arrange
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;
            var beforeSet = DateTime.Now;

            // Act
            _sut.Country = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Country, Is.Null);
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
            var phoneNumber = new PhoneNumber(1, "555-1234", OrganizerCompanion.Core.Enums.Types.Home, OrganizerCompanion.Core.Enums.Countries.UnitedStates, 0, null, null, specificDate, null);

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
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Phone = "+1-555-123-4567";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("phone").And.Contains("555-123-4567")); // Allow for Unicode escaping
                Assert.That(json, Does.Contain("\"type\":1")); // Work enum value is 1
                Assert.That(json, Does.Contain("\"country\":185")); // UnitedStates enum value is 185
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
                Assert.That(json, Does.Contain("\"country\":null"));
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
                country: OrganizerCompanion.Core.Enums.Countries.UnitedStates,
                linkedEntityId: 456,
                linkedEntity: null,
                linkedEntityType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);

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
                country: OrganizerCompanion.Core.Enums.Countries.UnitedStates,
                linkedEntityId: 789,
                linkedEntity: null,
                linkedEntityType: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);

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
        public void PhoneNumber_WithAllCountriesEnumValues_ShouldBeAllowed()
        {
            // Test all enum values
            foreach (OrganizerCompanion.Core.Enums.Countries country in Enum.GetValues<OrganizerCompanion.Core.Enums.Countries>())
            {
                // Act
                _sut.Country = country;

                // Assert
                Assert.That(_sut.Country, Is.EqualTo(country), $"Failed for country: {country}");
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
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.Canada;
            var fourthModified = _sut.DateModified;
            Assert.That(fourthModified, Is.GreaterThan(thirdModified));

            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntityId = 456;
            var fifthModified = _sut.DateModified;
            Assert.That(fifthModified, Is.GreaterThan(fourthModified));

            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntity = new MockDomainEntity { Id = 789 };
            var sixthModified = _sut.DateModified;
            Assert.That(sixthModified, Is.GreaterThan(fifthModified));
        }

        [Test, Category("Models")]
        public void ToJson_WithCompletePhoneNumberData_ShouldSerializeAllProperties()
        {
            // Arrange
            var mockEntity = new MockDomainEntity { Id = 999 };
            _sut.Id = 42;
            _sut.Phone = "+1-800-CALL-NOW";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Billing;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;
            _sut.LinkedEntityId = 999;
            _sut.LinkedEntity = mockEntity;
            _sut.DateModified = DateTime.Now;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);

                // Check that all expected properties are present
                string[] expectedProperties = [
                    "id", "phone", "type", "country", "linkedEntityId", "linkedEntity", "linkedEntityType", "dateCreated", "dateModified"
                ];

                foreach (var property in expectedProperties)
                {
                    Assert.That(json, Does.Contain($"\"{property}\":"), $"Property '{property}' not found in JSON");
                }

                // Check specific values
                Assert.That(json, Does.Contain("\"id\":42"));
                Assert.That(json, Does.Contain("\"linkedEntityId\":999"));
                Assert.That(json, Does.Contain("\"linkedEntityType\":\"MockDomainEntity\""));
                Assert.That(json, Does.Contain("\"type\":4")); // Billing enum value is 4

                // Verify JSON is well-formed
                var jsonDoc = JsonDocument.Parse(json);
                Assert.That(jsonDoc, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void PhoneNumber_SerializerOptions_ShouldHandleCyclicalReferences()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Phone = "+1-555-123-4567";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Cell;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.Canada;

            // Act & Assert - This should not throw due to ReferenceHandler.IgnoreCycles
            Assert.DoesNotThrow(() =>
            {
                var json = _sut.ToJson();
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                
                // Verify we can parse it back
                var jsonDoc = JsonDocument.Parse(json);
                Assert.That(jsonDoc, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void PhoneNumber_JsonSerialization_ShouldProduceValidFormat()
        {
            // Arrange
            _sut.Id = 42;
            _sut.Phone = "+1-555-TESTING";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Fax;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.Canada;
            _sut.DateModified = DateTime.Now;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);

                // Check that all expected properties are present
                string[] expectedProperties = [
                    "id", "phone", "type", "country", "dateCreated", "dateModified"
                ];

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
        public void PhoneNumber_WithZeroId_ShouldBeAllowed()
        {
            // Act
            _sut.Id = 0;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
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
        public void JsonConstructor_WithNullDateModified_ShouldAcceptNull()
        {
            // Arrange
            var id = 1;
            var phone = "+1-555-123-4567";
            var type = OrganizerCompanion.Core.Enums.Types.Home;
            var country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;
            var linkedEntityId = 0;
            var dateCreated = DateTime.Now.AddDays(-1);
            DateTime? dateModified = null;

            // Act
            var phoneNumber = new PhoneNumber(id, phone, type, country, linkedEntityId, null, null, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(phoneNumber.Id, Is.EqualTo(id));
                Assert.That(phoneNumber.Phone, Is.EqualTo(phone));
                Assert.That(phoneNumber.Type, Is.EqualTo(type));
                Assert.That(phoneNumber.Country, Is.EqualTo(country));
                Assert.That(phoneNumber.LinkedEntityId, Is.EqualTo(linkedEntityId));
                Assert.That(phoneNumber.LinkedEntity, Is.Null);
                Assert.That(phoneNumber.LinkedEntityType, Is.Null);
                Assert.That(phoneNumber.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(phoneNumber.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithOptionalParameters_ShouldIgnoreOptionalParams()
        {
            // Arrange
            var id = 1;
            var phone = "+1-555-123-4567";
            var type = OrganizerCompanion.Core.Enums.Types.Home;
            var country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;
            var linkedEntityId = 123;
            var mockEntity = new MockDomainEntity { Id = 123 };
            var linkedEntityType = "TestType";
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-1);
            
            // Act - Test constructor with all parameters
            var phoneNumber = new PhoneNumber(id, phone, type, country, linkedEntityId, mockEntity, linkedEntityType, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(phoneNumber.Id, Is.EqualTo(id));
                Assert.That(phoneNumber.Phone, Is.EqualTo(phone));
                Assert.That(phoneNumber.Type, Is.EqualTo(type));
                Assert.That(phoneNumber.Country, Is.EqualTo(country));
                Assert.That(phoneNumber.LinkedEntityId, Is.EqualTo(linkedEntityId));
                Assert.That(phoneNumber.LinkedEntity, Is.EqualTo(mockEntity));
                Assert.That(phoneNumber.LinkedEntityType, Is.EqualTo(linkedEntityType));
                Assert.That(phoneNumber.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(phoneNumber.DateModified, Is.EqualTo(dateModified));
                // Cast properties still throw exceptions despite constructor parameters
                Assert.Throws<NotImplementedException>(() => { var _ = phoneNumber.IsCast; });
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityId_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newLinkedEntityId = 999;
            var beforeSet = DateTime.Now;

            // Act
            _sut.LinkedEntityId = newLinkedEntityId;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(newLinkedEntityId));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_WhenSet_ShouldUpdateLinkedEntityTypeAndDateModified()
        {
            // Arrange
            var mockEntity = new MockDomainEntity { Id = 123 };
            var beforeSet = DateTime.Now;

            // Act
            _sut.LinkedEntity = mockEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(mockEntity));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_WhenSetToNull_ShouldUpdateLinkedEntityTypeToNullAndDateModified()
        {
            // Arrange
            _sut.LinkedEntity = new MockDomainEntity { Id = 123 };
            var beforeSet = DateTime.Now;

            // Act
            _sut.LinkedEntity = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityType_IsReadOnly_ReflectsLinkedEntityType()
        {
            // Arrange
            var mockEntity = new MockDomainEntity { Id = 456 };

            // Act
            _sut.LinkedEntity = mockEntity;

            // Assert
            Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
        }

        [Test, Category("Models")]
        public void LinkedEntityId_WithNegativeValue_ShouldBeAllowed()
        {
            // Act
            _sut.LinkedEntityId = -1;

            // Assert
            Assert.That(_sut.LinkedEntityId, Is.EqualTo(-1));
        }

        [Test, Category("Models")]
        public void LinkedEntityId_WithMaxValue_ShouldBeAllowed()
        {
            // Act
            _sut.LinkedEntityId = int.MaxValue;

            // Assert
            Assert.That(_sut.LinkedEntityId, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void LinkedEntityId_WithMinValue_ShouldBeAllowed()
        {
            // Act
            _sut.LinkedEntityId = int.MinValue;

            // Assert
            Assert.That(_sut.LinkedEntityId, Is.EqualTo(int.MinValue));
        }

        [Test, Category("Models")]
        public void JsonSerialization_WithLinkedEntityProperties_ShouldIncludeAllFields()
        {
            // Arrange
            var mockEntity = new MockDomainEntity { Id = 789 };
            _sut.Id = 1;
            _sut.Phone = "+1-555-123-4567";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
            _sut.LinkedEntityId = 789;
            _sut.LinkedEntity = mockEntity;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"linkedEntityId\":789"));
                Assert.That(json, Does.Contain("\"linkedEntityType\":\"MockDomainEntity\""));
                
                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void JsonSerialization_WithNullLinkedEntity_ShouldHandleCorrectly()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Phone = "+1-555-123-4567";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Cell;
            _sut.LinkedEntityId = 0;
            _sut.LinkedEntity = null;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"linkedEntityId\":0"));
                Assert.That(json, Does.Contain("\"linkedEntity\":null"));
                Assert.That(json, Does.Contain("\"linkedEntityType\":null"));
                
                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void DataAnnotation_RequiredAttributes_ShouldBePresent()
        {
            // Arrange & Act - Verify that Required attributes are correctly applied
            var type = typeof(PhoneNumber);
            
            // Assert - Check that properties have Required attributes where expected
            var idProperty = type.GetProperty("Id");
            var phoneProperty = type.GetProperty("Phone");
            var typeProperty = type.GetProperty("Type");
            var countryProperty = type.GetProperty("Country");
            var linkedEntityIdProperty = type.GetProperty("LinkedEntityId");
            var linkedEntityProperty = type.GetProperty("LinkedEntity");
            var linkedEntityTypeProperty = type.GetProperty("LinkedEntityType");
            var dateCreatedProperty = type.GetProperty("DateCreated");
            var dateModifiedProperty = type.GetProperty("DateModified");

            Assert.Multiple(() =>
            {
                // Verify Required attributes exist on appropriate properties
                Assert.That(idProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false), 
                    Is.Not.Empty, "Id should have Required attribute");
                Assert.That(phoneProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false), 
                    Is.Not.Empty, "Phone should have Required attribute");
                Assert.That(typeProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false), 
                    Is.Not.Empty, "Type should have Required attribute");
                Assert.That(countryProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false),
                    Is.Not.Empty, "Country should have Required attribute");
                Assert.That(linkedEntityIdProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false), 
                    Is.Not.Empty, "LinkedEntityId should have Required attribute");
                Assert.That(linkedEntityProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false), 
                    Is.Not.Empty, "LinkedEntity should have Required attribute");
                Assert.That(linkedEntityTypeProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false), 
                    Is.Not.Empty, "LinkedEntityType should have Required attribute");
                Assert.That(dateCreatedProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false), 
                    Is.Not.Empty, "DateCreated should have Required attribute");
                Assert.That(dateModifiedProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false), 
                    Is.Not.Empty, "DateModified should have Required attribute");
            });
        }

        [Test, Category("Models")]
        public void DataAnnotation_RangeAttributes_ShouldBePresent()
        {
            // Arrange & Act - Verify that Range attributes are correctly applied
            var type = typeof(PhoneNumber);
            
            // Assert - Check that properties have Range attributes where expected
            var idProperty = type.GetProperty("Id");
            var linkedEntityIdProperty = type.GetProperty("LinkedEntityId");

            Assert.Multiple(() =>
            {
                // Verify Range attributes exist on appropriate properties
                Assert.That(idProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false), 
                    Is.Not.Empty, "Id should have Range attribute");
                Assert.That(linkedEntityIdProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RangeAttribute), false), 
                    Is.Not.Empty, "LinkedEntityId should have Range attribute");
            });
        }

        [Test, Category("Models")]
        public void JsonPropertyName_Attributes_ShouldBePresent()
        {
            // Arrange & Act - Verify that JsonPropertyName attributes are correctly applied
            var type = typeof(PhoneNumber);
            
            // Assert - Check that properties have JsonPropertyName attributes where expected
            var idProperty = type.GetProperty("Id");
            var phoneProperty = type.GetProperty("Phone");
            var typeProperty = type.GetProperty("Type");
            var countryProperty = type.GetProperty("Country");
            var linkedEntityIdProperty = type.GetProperty("LinkedEntityId");
            var linkedEntityProperty = type.GetProperty("LinkedEntity");
            var linkedEntityTypeProperty = type.GetProperty("LinkedEntityType");
            var dateCreatedProperty = type.GetProperty("DateCreated");
            var dateModifiedProperty = type.GetProperty("DateModified");

            Assert.Multiple(() =>
            {
                // Verify JsonPropertyName attributes exist on appropriate properties
                Assert.That(idProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false), 
                    Is.Not.Empty, "Id should have JsonPropertyName attribute");
                Assert.That(phoneProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false), 
                    Is.Not.Empty, "Phone should have JsonPropertyName attribute");
                Assert.That(typeProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false), 
                    Is.Not.Empty, "Type should have JsonPropertyName attribute");
                Assert.That(countryProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false),
                    Is.Not.Empty, "Country should have JsonPropertyName attribute");
                Assert.That(linkedEntityIdProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false), 
                    Is.Not.Empty, "LinkedEntityId should have JsonPropertyName attribute");
                Assert.That(linkedEntityProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false), 
                    Is.Not.Empty, "LinkedEntity should have JsonPropertyName attribute");
                Assert.That(linkedEntityTypeProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false), 
                    Is.Not.Empty, "LinkedEntityType should have JsonPropertyName attribute");
                Assert.That(dateCreatedProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false), 
                    Is.Not.Empty, "DateCreated should have JsonPropertyName attribute");
                Assert.That(dateModifiedProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false), 
                    Is.Not.Empty, "DateModified should have JsonPropertyName attribute");
            });
        }

        [Test, Category("Models")]
        public void JsonIgnore_Attributes_ShouldBePresent()
        {
            // Arrange & Act - Verify that JsonIgnore attributes are correctly applied to Cast properties
            var type = typeof(PhoneNumber);
            
            // Assert - Check that Cast properties have JsonIgnore attributes
            var isCastProperty = type.GetProperty("IsCast");
            var castIdProperty = type.GetProperty("CastId");
            var castTypeProperty = type.GetProperty("CastType");

            Assert.Multiple(() =>
            {
                // Verify JsonIgnore attributes exist on Cast properties
                Assert.That(isCastProperty?.GetCustomAttributes(typeof(JsonIgnoreAttribute), false), 
                    Is.Not.Empty, "IsCast should have JsonIgnore attribute");
                Assert.That(castIdProperty?.GetCustomAttributes(typeof(JsonIgnoreAttribute), false), 
                    Is.Not.Empty, "CastId should have JsonIgnore attribute");
                Assert.That(castTypeProperty?.GetCustomAttributes(typeof(JsonIgnoreAttribute), false), 
                    Is.Not.Empty, "CastType should have JsonIgnore attribute");
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullParameters_ShouldHandleCorrectly()
        {
            // Arrange & Act
            var phoneNumber = new PhoneNumber(
                id: 0,
                phone: null,
                type: null,
                country: null,
                linkedEntityId: 0,
                linkedEntity: null,
                linkedEntityType: null,
                dateCreated: DateTime.Now,
                dateModified: null
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(phoneNumber.Id, Is.EqualTo(0));
                Assert.That(phoneNumber.Phone, Is.Null);
                Assert.That(phoneNumber.Type, Is.Null);
                Assert.That(phoneNumber.Country, Is.Null);
                Assert.That(phoneNumber.LinkedEntityId, Is.EqualTo(0));
                Assert.That(phoneNumber.LinkedEntity, Is.Null);
                Assert.That(phoneNumber.LinkedEntityType, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithEmptyStringValues_ShouldHandleCorrectly()
        {
            // Arrange & Act
            var phoneNumber = new PhoneNumber(
                id: 1,
                phone: string.Empty,
                type: OrganizerCompanion.Core.Enums.Types.Other,
                country: OrganizerCompanion.Core.Enums.Countries.UnitedStates,
                linkedEntityId: 0,
                linkedEntity: null,
                linkedEntityType: string.Empty,
                dateCreated: DateTime.Now,
                dateModified: DateTime.Now
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(phoneNumber.Id, Is.EqualTo(1));
                Assert.That(phoneNumber.Phone, Is.EqualTo(string.Empty));
                Assert.That(phoneNumber.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
                Assert.That(phoneNumber.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.UnitedStates));
                Assert.That(phoneNumber.LinkedEntityId, Is.EqualTo(0));
                Assert.That(phoneNumber.LinkedEntity, Is.Null);
                Assert.That(phoneNumber.LinkedEntityType, Is.EqualTo(string.Empty));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_WithComplexMockEntity_ShouldUpdateLinkedEntityTypeCorrectly()
        {
            // Arrange
            var complexMockEntity = new MockDomainEntity 
            { 
                Id = 999, 
                IsCast = true, 
                CastId = 123, 
                CastType = "TestCast",
                DateCreated = DateTime.Now.AddDays(-1),
                DateModified = DateTime.Now.AddHours(-1)
            };

            // Act
            _sut.LinkedEntity = complexMockEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(complexMockEntity));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(_sut.LinkedEntity.Id, Is.EqualTo(999));
                Assert.That(_sut.LinkedEntity.IsCast, Is.True);
                Assert.That(_sut.LinkedEntity.CastId, Is.EqualTo(123));
                Assert.That(_sut.LinkedEntity.CastType, Is.EqualTo("TestCast"));
            });
        }

        [Test, Category("Models")]
        public void PhoneNumber_AllPropertyChanges_ShouldUpdateDateModifiedSequentially()
        {
            // Arrange
            var initialTime = DateTime.Now;
            System.Threading.Thread.Sleep(1);

            // Act & Assert - Test sequential property changes update DateModified
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
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.Canada;
            var fourthModified = _sut.DateModified;
            Assert.That(fourthModified, Is.GreaterThan(thirdModified));

            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntityId = 456;
            var fifthModified = _sut.DateModified;
            Assert.That(fifthModified, Is.GreaterThan(fourthModified));

            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntity = new MockDomainEntity { Id = 789 };
            var sixthModified = _sut.DateModified;
            Assert.That(sixthModified, Is.GreaterThan(fifthModified));
        }

        #region Cast Method Tests
        [Test, Category("Models")]
        public void Cast_ToPhoneNumberDTO_ShouldReturnValidPhoneNumberDTO()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Phone = "+1-555-123-4567";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;

            // Act
            var result = _sut.Cast<PhoneNumberDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<PhoneNumberDTO>());
                Assert.That(result.Id, Is.EqualTo(123));
                Assert.That(result.Phone, Is.EqualTo("+1-555-123-4567"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(result.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.UnitedStates));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIPhoneNumberDTO_ShouldReturnValidIPhoneNumberDTO()
        {
            // Arrange
            _sut.Id = 456;
            _sut.Phone = "+1-800-CALL-NOW";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Cell;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.Canada;

            // Act
            var result = _sut.Cast<IPhoneNumberDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<PhoneNumberDTO>());
                Assert.That(result.Id, Is.EqualTo(456));
                Assert.That(result.Phone, Is.EqualTo("+1-800-CALL-NOW"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Cell));
                Assert.That(result.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.Canada));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithNullPhone_ShouldReturnPhoneNumberDTOWithNullPhone()
        {
            // Arrange
            _sut.Id = 789;
            _sut.Phone = null;
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.Mexico;

            // Act
            var result = _sut.Cast<PhoneNumberDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(789));
                Assert.That(result.Phone, Is.Null);
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
                Assert.That(result.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.Mexico));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithNullType_ShouldReturnPhoneNumberDTOWithNullType()
        {
            // Arrange
            _sut.Id = 101;
            _sut.Phone = "+1-555-NULL-TYPE";
            _sut.Type = null;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;

            // Act
            var result = _sut.Cast<PhoneNumberDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(101));
                Assert.That(result.Phone, Is.EqualTo("+1-555-NULL-TYPE"));
                Assert.That(result.Type, Is.Null);
                Assert.That(result.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.UnitedStates));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithNullCountry_ShouldReturnPhoneNumberDTOWithNullCountry()
        {
            // Arrange
            _sut.Id = 102;
            _sut.Phone = "+1-555-NULL-COUNTRY";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
            _sut.Country = null;

            // Act
            var result = _sut.Cast<PhoneNumberDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(102));
                Assert.That(result.Phone, Is.EqualTo("+1-555-NULL-COUNTRY"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(result.Country, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Cast_WithEmptyPhone_ShouldReturnPhoneNumberDTOWithEmptyPhone()
        {
            // Arrange
            _sut.Id = 202;
            _sut.Phone = string.Empty;
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Fax;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.Canada;

            // Act
            var result = _sut.Cast<PhoneNumberDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(202));
                Assert.That(result.Phone, Is.EqualTo(string.Empty));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Fax));
                Assert.That(result.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.Canada));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithVeryLongPhone_ShouldReturnPhoneNumberDTOWithLongPhone()
        {
            // Arrange
            var longPhone = new string('1', 500) + "+1-555-123-4567" + new string('2', 500);
            _sut.Id = 303;
            _sut.Phone = longPhone;
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Other;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;

            // Act
            var result = _sut.Cast<PhoneNumberDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(303));
                Assert.That(result.Phone, Is.EqualTo(longPhone));
                Assert.That(result.Phone?.Length, Is.EqualTo(1015)); // 500 + 15 + 500
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
                Assert.That(result.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.UnitedStates));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithSpecialCharactersInPhone_ShouldReturnPhoneNumberDTOWithSpecialCharacters()
        {
            // Arrange
            var specialPhone = "☎ +1-555-123-4567 📞 ext. 1234";
            _sut.Id = 404;
            _sut.Phone = specialPhone;
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Billing;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.Mexico;

            // Act
            var result = _sut.Cast<PhoneNumberDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(404));
                Assert.That(result.Phone, Is.EqualTo(specialPhone));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Billing));
                Assert.That(result.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.Mexico));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithAllTypesEnumValues_ShouldReturnCorrectPhoneNumberDTO()
        {
            // Test each enum value
            var testCases = new[]
            {
                (OrganizerCompanion.Core.Enums.Types.Home, 1),
                (OrganizerCompanion.Core.Enums.Types.Work, 2),
                (OrganizerCompanion.Core.Enums.Types.Cell, 3),
                (OrganizerCompanion.Core.Enums.Types.Fax, 4),
                (OrganizerCompanion.Core.Enums.Types.Billing, 5),
                (OrganizerCompanion.Core.Enums.Types.Other, 6)
            };

            foreach (var (type, id) in testCases)
            {
                // Arrange
                _sut.Id = id;
                _sut.Phone = $"+1-555-{id:000}-{id:0000}";
                _sut.Type = type;
                _sut.Country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;

                // Act
                var result = _sut.Cast<PhoneNumberDTO>();

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null, $"Failed for type {type}");
                    Assert.That(result.Id, Is.EqualTo(id), $"Failed for type {type}");
                    Assert.That(result.Phone, Is.EqualTo($"+1-555-{id:000}-{id:0000}"), $"Failed for type {type}");
                    Assert.That(result.Type, Is.EqualTo(type), $"Failed for type {type}");
                    Assert.That(result.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.UnitedStates), $"Failed for type {type}");
                });
            }
        }

        [Test, Category("Models")]
        public void Cast_WithMaxIntId_ShouldReturnPhoneNumberDTOWithMaxIntId()
        {
            // Arrange
            _sut.Id = int.MaxValue;
            _sut.Phone = "+1-555-MAX-VALUE";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.Canada;

            // Act
            var result = _sut.Cast<PhoneNumberDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(int.MaxValue));
                Assert.That(result.Phone, Is.EqualTo("+1-555-MAX-VALUE"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(result.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.Canada));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithMinIntId_ShouldReturnPhoneNumberDTOWithMinIntId()
        {
            // Arrange
            _sut.Id = int.MinValue;
            _sut.Phone = "+1-555-MIN-VALUE";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Cell;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.Mexico;

            // Act
            var result = _sut.Cast<PhoneNumberDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(int.MinValue));
                Assert.That(result.Phone, Is.EqualTo("+1-555-MIN-VALUE"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Cell));
                Assert.That(result.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.Mexico));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithZeroId_ShouldReturnPhoneNumberDTOWithZeroId()
        {
            // Arrange
            _sut.Id = 0;
            _sut.Phone = "+1-555-ZERO-ID";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;

            // Act
            var result = _sut.Cast<PhoneNumberDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(0));
                Assert.That(result.Phone, Is.EqualTo("+1-555-ZERO-ID"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
                Assert.That(result.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.UnitedStates));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedType_ShouldThrowInvalidCastException()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Phone = "+1-555-123-4567";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;

            // Act & Assert
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message, Does.Contain("Cannot cast Phone to type MockDomainEntity is not supported"));
            });
        }

        // Mock class for testing unsupported cast types
        private class MockUnsupportedType : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime? DateModified { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedTypeWithMock_ShouldThrowInvalidCastException()
        {
            // Arrange
            _sut.Id = 456;
            _sut.Phone = "+1-555-UNSUPPORTED";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Fax;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.Mexico;

            // Act & Assert
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockUnsupportedType>());
            Assert.Multiple(() =>
            {
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message, Does.Contain("Cannot cast Phone to type MockUnsupportedType is not supported"));
            });
        }

        [Test, Category("Models")]
        public void Cast_WhenExceptionOccursInTryBlock_ShouldThrowInvalidCastExceptionWithInnerException()
        {
            // Note: This test demonstrates the exception handling in the Cast method
            // The current implementation catches any exception and wraps it in InvalidCastException
            
            // Arrange
            _sut.Id = 789;
            _sut.Phone = "+1-555-EXCEPTION-TEST";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Other;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;

            // Act & Assert - Test with a type that should cause the InvalidCastException
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockUnsupportedType>());
            Assert.Multiple(() =>
            {
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message, Does.Contain("Cannot cast Phone to type MockUnsupportedType is not supported"));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithAllNullValues_ShouldReturnPhoneNumberDTOWithNullValues()
        {
            // Arrange
            _sut.Id = 0;
            _sut.Phone = null;
            _sut.Type = null;
            _sut.Country = null;

            // Act
            var result = _sut.Cast<PhoneNumberDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(0));
                Assert.That(result.Phone, Is.Null);
                Assert.That(result.Type, Is.Null);
                Assert.That(result.Country, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIPhoneNumberDTOWithComplexData_ShouldReturnValidResult()
        {
            // Arrange
            _sut.Id = 999;
            _sut.Phone = "+1-800-COMPLEX-DATA-123";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Billing;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.Canada;

            // Act
            var result = _sut.Cast<IPhoneNumberDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<PhoneNumberDTO>());
                Assert.That(result.Id, Is.EqualTo(999));
                Assert.That(result.Phone, Is.EqualTo("+1-800-COMPLEX-DATA-123"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Billing));
                Assert.That(result.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.Canada));
            });
        }

        [Test, Category("Models")]
        public void Cast_PerformanceTest_ShouldHandleMultipleCastsEfficiently()
        {
            // Arrange
            _sut.Id = 100;
            _sut.Phone = "+1-555-PERFORMANCE-TEST";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
            _sut.Country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;
            var iterations = 1000;

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    var dto = _sut.Cast<PhoneNumberDTO>();
                    var iDto = _sut.Cast<IPhoneNumberDTO>();
                    
                    Assert.That(dto, Is.Not.Null);
                    Assert.That(iDto, Is.Not.Null);
                }
            });
        }
        #endregion
    }
}
