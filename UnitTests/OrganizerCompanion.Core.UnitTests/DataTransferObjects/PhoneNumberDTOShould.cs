using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    /// <summary>
    /// Unit tests for PhoneNumberDTO class to achieve 100% code coverage.
    /// Tests constructor initialization, property getters/setters, interface implementations,
    /// IDomainEntity methods, JSON serialization attributes, data annotations, and edge cases.
    /// </summary>
    [TestFixture]
    public class PhoneNumberDTOShould
    {
        private PhoneNumberDTO _phoneNumberDTO;

        [SetUp]
        public void SetUp()
        {
            _phoneNumberDTO = new PhoneNumberDTO();
        }

        #region Constructor Tests

        [Test, Category("DataTransferObjects")]
        public void Constructor_ShouldInitializeWithDefaultValues()
        {
            // Arrange & Act
            var phoneNumberDTO = new PhoneNumberDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(phoneNumberDTO.Id, Is.EqualTo(0));
                Assert.That(phoneNumberDTO.Phone, Is.Null);
                Assert.That(phoneNumberDTO.Type, Is.Null);
            });
        }

        #endregion

        #region Property Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const int expectedId = 12345;

            // Act
            _phoneNumberDTO.Id = expectedId;

            // Assert
            Assert.That(_phoneNumberDTO.Id, Is.EqualTo(expectedId));
        }

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const string expectedPhone = "555-123-4567";

            // Act
            _phoneNumberDTO.Phone = expectedPhone;

            // Assert
            Assert.That(_phoneNumberDTO.Phone, Is.EqualTo(expectedPhone));
        }

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldAcceptNull()
        {
            // Act
            _phoneNumberDTO.Phone = null;

            // Assert
            Assert.That(_phoneNumberDTO.Phone, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldAcceptEmptyString()
        {
            // Act
            _phoneNumberDTO.Phone = string.Empty;

            // Assert
            Assert.That(_phoneNumberDTO.Phone, Is.EqualTo(string.Empty));
        }

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldAcceptVariousFormats()
        {
            // Arrange & Act & Assert
            var phoneFormats = new[]
            {
                "555-123-4567",
                "(555) 123-4567",
                "555.123.4567",
                "5551234567",
                "+1-555-123-4567",
                "+1 (555) 123-4567",
                "1-555-123-4567"
            };

            foreach (var format in phoneFormats)
            {
                _phoneNumberDTO.Phone = format;
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo(format));
            }
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const OrganizerCompanion.Core.Enums.Types expectedType = OrganizerCompanion.Core.Enums.Types.Home;

            // Act
            _phoneNumberDTO.Type = expectedType;

            // Assert
            Assert.That(_phoneNumberDTO.Type, Is.EqualTo(expectedType));
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldAcceptNull()
        {
            // Act
            _phoneNumberDTO.Type = null;

            // Assert
            Assert.That(_phoneNumberDTO.Type, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldAcceptAllValidEnumValues()
        {
            // Arrange & Act & Assert
            foreach (OrganizerCompanion.Core.Enums.Types type in Enum.GetValues<OrganizerCompanion.Core.Enums.Types>())
            {
                _phoneNumberDTO.Type = type;
                Assert.That(_phoneNumberDTO.Type, Is.EqualTo(type));
            }
        }

        #endregion

        #region Interface Implementation Tests

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldImplementIPhoneNumberDTO()
        {
            Assert.That(_phoneNumberDTO, Is.InstanceOf<IPhoneNumberDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldImplementIDomainEntity()
        {
            Assert.That(_phoneNumberDTO, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldImplementIPhoneNumber()
        {
            Assert.That(_phoneNumberDTO, Is.InstanceOf<OrganizerCompanion.Core.Interfaces.Type.IPhoneNumber>());
        }

        #endregion

        #region IDomainEntity Property Tests

        [Test, Category("DataTransferObjects")]
        public void IsCast_ShouldThrowNotImplementedException_OnGet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _phoneNumberDTO.IsCast; });
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_ShouldThrowNotImplementedException_OnSet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _phoneNumberDTO.IsCast = true);
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_ShouldThrowNotImplementedException_OnGet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _phoneNumberDTO.CastId; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_ShouldThrowNotImplementedException_OnSet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _phoneNumberDTO.CastId = 123);
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_ShouldThrowNotImplementedException_OnGet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _phoneNumberDTO.CastType; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_ShouldThrowNotImplementedException_OnSet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _phoneNumberDTO.CastType = "TestType");
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldThrowNotImplementedException_OnGet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _phoneNumberDTO.DateCreated; });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldThrowNotImplementedException_OnGet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _phoneNumberDTO.DateModified; });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldThrowNotImplementedException_OnSet()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _phoneNumberDTO.DateModified = DateTime.Now);
        }

        #endregion

        #region IDomainEntity Method Tests

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _phoneNumberDTO.Cast<MockDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _phoneNumberDTO.ToJson());
        }

        #endregion

        #region JSON Serialization Attribute Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.Id));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("id"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.Phone));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("phone"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.Type));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("type"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IDomainEntityProperties_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var isCastProperty = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.IsCast));
            var castIdProperty = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.CastId));
            var castTypeProperty = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.CastType));
            var dateCreatedProperty = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.DateCreated));
            var dateModifiedProperty = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.DateModified));

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(isCastProperty?.GetCustomAttributes(typeof(JsonIgnoreAttribute), false), Is.Not.Empty);
                Assert.That(castIdProperty?.GetCustomAttributes(typeof(JsonIgnoreAttribute), false), Is.Not.Empty);
                Assert.That(castTypeProperty?.GetCustomAttributes(typeof(JsonIgnoreAttribute), false), Is.Not.Empty);
                Assert.That(dateCreatedProperty?.GetCustomAttributes(typeof(JsonIgnoreAttribute), false), Is.Not.Empty);
                Assert.That(dateModifiedProperty?.GetCustomAttributes(typeof(JsonIgnoreAttribute), false), Is.Not.Empty);
            });
        }

        #endregion

        #region Data Annotation Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.Id));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false)
                .FirstOrDefault() as RequiredAttribute;

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.Phone));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false)
                .FirstOrDefault() as RequiredAttribute;

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.Type));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false)
                .FirstOrDefault() as RequiredAttribute;

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        #endregion

        #region Edge Case Tests

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldAcceptSpecialCharacters()
        {
            // Arrange
            const string phoneWithSpecialChars = "555-123-4567 ext. 1234";

            // Act
            _phoneNumberDTO.Phone = phoneWithSpecialChars;

            // Assert
            Assert.That(_phoneNumberDTO.Phone, Is.EqualTo(phoneWithSpecialChars));
        }

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldAcceptInternationalFormats()
        {
            // Arrange
            var internationalFormats = new[]
            {
                "+44 20 7946 0958",
                "+33 1 42 86 83 26",
                "+81-3-3570-8000",
                "+86 21 1234 5678"
            };

            // Act & Assert
            foreach (var format in internationalFormats)
            {
                _phoneNumberDTO.Phone = format;
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo(format));
            }
        }

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldAcceptVeryLongNumbers()
        {
            // Arrange
            const string longPhoneNumber = "+1-555-123-4567-extension-12345-department-sales";

            // Act
            _phoneNumberDTO.Phone = longPhoneNumber;

            // Assert
            Assert.That(_phoneNumberDTO.Phone, Is.EqualTo(longPhoneNumber));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptNegativeValues()
        {
            // Arrange
            const int negativeId = -999;

            // Act
            _phoneNumberDTO.Id = negativeId;

            // Assert
            Assert.That(_phoneNumberDTO.Id, Is.EqualTo(negativeId));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptMaxIntValue()
        {
            // Arrange
            const int maxValue = int.MaxValue;

            // Act
            _phoneNumberDTO.Id = maxValue;

            // Assert
            Assert.That(_phoneNumberDTO.Id, Is.EqualTo(maxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptMinIntValue()
        {
            // Arrange
            const int minValue = int.MinValue;

            // Act
            _phoneNumberDTO.Id = minValue;

            // Assert
            Assert.That(_phoneNumberDTO.Id, Is.EqualTo(minValue));
        }

        #endregion

        #region JSON Serialization Tests

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldSerializeWithCorrectPropertyNames()
        {
            // Arrange
            _phoneNumberDTO.Id = 123;
            _phoneNumberDTO.Phone = "555-123-4567";
            _phoneNumberDTO.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Act
            var json = JsonSerializer.Serialize(_phoneNumberDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(json, Contains.Substring("\"id\":123"));
                Assert.That(json, Contains.Substring("\"phone\":\"555-123-4567\""));
                Assert.That(json, Contains.Substring("\"type\":"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldDeserializeCorrectly()
        {
            // Arrange
            const string json = "{\"id\":456,\"phone\":\"555-987-6543\",\"type\":1}";

            // Act
            var phoneNumberDTO = JsonSerializer.Deserialize<PhoneNumberDTO>(json);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(phoneNumberDTO, Is.Not.Null);
                Assert.That(phoneNumberDTO!.Id, Is.EqualTo(456));
                Assert.That(phoneNumberDTO.Phone, Is.EqualTo("555-987-6543"));
                Assert.That(phoneNumberDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldSerializeWithNullValues()
        {
            // Arrange
            _phoneNumberDTO.Id = 789;
            _phoneNumberDTO.Phone = null;
            _phoneNumberDTO.Type = null;

            // Act
            var json = JsonSerializer.Serialize(_phoneNumberDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Contains.Substring("\"id\":789"));
                Assert.That(json, Contains.Substring("\"phone\":null"));
                Assert.That(json, Contains.Substring("\"type\":null"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldNotSerializeIDomainEntityProperties()
        {
            // Arrange
            _phoneNumberDTO.Id = 1;
            _phoneNumberDTO.Phone = "555-0000";

            // Act
            var json = JsonSerializer.Serialize(_phoneNumberDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Does.Not.Contain("isCast"));
                Assert.That(json, Does.Not.Contain("castId"));
                Assert.That(json, Does.Not.Contain("castType"));
                Assert.That(json, Does.Not.Contain("dateCreated"));
                Assert.That(json, Does.Not.Contain("dateModified"));
            });
        }

        #endregion

        #region Integration Tests

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldWorkWithComplexScenarios()
        {
            // Arrange
            _phoneNumberDTO.Id = 999;
            _phoneNumberDTO.Phone = "+1 (555) 123-4567 ext. 890";
            _phoneNumberDTO.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Act
            var json = JsonSerializer.Serialize(_phoneNumberDTO);
            var deserializedPhoneNumber = JsonSerializer.Deserialize<PhoneNumberDTO>(json);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(deserializedPhoneNumber, Is.Not.Null);
                Assert.That(deserializedPhoneNumber!.Id, Is.EqualTo(999));
                Assert.That(deserializedPhoneNumber.Phone, Is.EqualTo("+1 (555) 123-4567 ext. 890"));
                Assert.That(deserializedPhoneNumber.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldMaintainTypeIntegrity()
        {
            // Arrange & Act
            IPhoneNumberDTO interfacePhone = _phoneNumberDTO;
            interfacePhone.Phone = "555-TYPE-TEST";
            interfacePhone.Type = OrganizerCompanion.Core.Enums.Types.Cell;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo("555-TYPE-TEST"));
                Assert.That(_phoneNumberDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Cell));
                Assert.That(interfacePhone.Phone, Is.EqualTo(_phoneNumberDTO.Phone));
                Assert.That(interfacePhone.Type, Is.EqualTo(_phoneNumberDTO.Type));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldHandleTypesEnumConversions()
        {
            // Arrange & Act & Assert
            var allTypes = Enum.GetValues<OrganizerCompanion.Core.Enums.Types>();
            
            foreach (var type in allTypes)
            {
                _phoneNumberDTO.Type = type;
                var json = JsonSerializer.Serialize(_phoneNumberDTO);
                var deserialized = JsonSerializer.Deserialize<PhoneNumberDTO>(json);
                
                Assert.That(deserialized!.Type, Is.EqualTo(type), 
                    $"Failed for type: {type}");
            }
        }

        #endregion

        #region Mock Classes for Testing

        /// <summary>
        /// Mock implementation of IDomainEntity for testing purposes.
        /// </summary>
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime? DateModified { get; set; }

            public T Cast<T>() where T : IDomainEntity
            {
                throw new NotImplementedException();
            }

            public string ToJson()
            {
                throw new NotImplementedException();
            }
        }

        #endregion
    }
}
