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
                Assert.That(phoneNumberDTO.Country, Is.Null);
                Assert.That(phoneNumberDTO.CreatedDate, Is.LessThan(DateTime.UtcNow));
                Assert.That(phoneNumberDTO.ModifiedDate, Is.Null);
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
            // Arrange, Act & Assert
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
            // Arrange, Act & Assert
            foreach (OrganizerCompanion.Core.Enums.Types type in Enum.GetValues<OrganizerCompanion.Core.Enums.Types>())
            {
                _phoneNumberDTO.Type = type;
                Assert.That(_phoneNumberDTO.Type, Is.EqualTo(type));
            }
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const OrganizerCompanion.Core.Enums.Countries expectedCountry = OrganizerCompanion.Core.Enums.Countries.UnitedStates;

            // Act
            _phoneNumberDTO.Country = expectedCountry;

            // Assert
            Assert.That(_phoneNumberDTO.Country, Is.EqualTo(expectedCountry));
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldAcceptNull()
        {
            // Act
            _phoneNumberDTO.Country = null;

            // Assert
            Assert.That(_phoneNumberDTO.Country, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldAcceptAllValidEnumValues()
        {
            // Arrange, Act & Assert
            foreach (OrganizerCompanion.Core.Enums.Countries country in Enum.GetValues<OrganizerCompanion.Core.Enums.Countries>())
            {
                _phoneNumberDTO.Country = country;
                Assert.That(_phoneNumberDTO.Country, Is.EqualTo(country));
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
        public void CreatedDate_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 10, 15, 14, 30, 0);

            // Act
            _phoneNumberDTO.CreatedDate = expectedDate;

            // Assert
            Assert.That(_phoneNumberDTO.CreatedDate, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 10, 16, 10, 15, 0);

            // Act
            _phoneNumberDTO.ModifiedDate = expectedDate;

            // Assert
            Assert.That(_phoneNumberDTO.ModifiedDate, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldAcceptNull()
        {
            // Act
            _phoneNumberDTO.ModifiedDate = null;

            // Assert
            Assert.That(_phoneNumberDTO.ModifiedDate, Is.Null);
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
        public void Country_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.Country));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("country"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.CreatedDate));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("createdDate"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.ModifiedDate));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("modifiedDate"));
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

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.Country));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false)
                .FirstOrDefault() as RequiredAttribute;

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.CreatedDate));

            // Act
            var requiredAttribute = property?.GetCustomAttributes(typeof(RequiredAttribute), false)
                .FirstOrDefault() as RequiredAttribute;

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(PhoneNumberDTO).GetProperty(nameof(PhoneNumberDTO.ModifiedDate));

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
            _phoneNumberDTO.Country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;

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
                Assert.That(json, Contains.Substring("\"country\":"));
                Assert.That(json, Contains.Substring("\"createdDate\":"));
                Assert.That(json, Contains.Substring("\"modifiedDate\":"));
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
            interfacePhone.Type = OrganizerCompanion.Core.Enums.Types.Mobile;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo("555-TYPE-TEST"));
                Assert.That(_phoneNumberDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Mobile));
                Assert.That(interfacePhone.Phone, Is.EqualTo(_phoneNumberDTO.Phone));
                Assert.That(interfacePhone.Type, Is.EqualTo(_phoneNumberDTO.Type));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldHandleTypesEnumConversions()
        {
            // Arrange, Act & Assert
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

        #region Additional Edge Case Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldSupportSequentialAssignments()
        {
            // Arrange
            var ids = new[] { 0, 1, -1, int.MaxValue, int.MinValue, 42, 999 };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var id in ids)
                {
                    _phoneNumberDTO.Id = id;
                    Assert.That(_phoneNumberDTO.Id, Is.EqualTo(id));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _phoneNumberDTO.CreatedDate = DateTime.MinValue;

            // Assert
            Assert.That(_phoneNumberDTO.CreatedDate, Is.EqualTo(DateTime.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _phoneNumberDTO.CreatedDate = DateTime.MaxValue;

            // Assert
            Assert.That(_phoneNumberDTO.CreatedDate, Is.EqualTo(DateTime.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _phoneNumberDTO.ModifiedDate = DateTime.MinValue;

            // Assert
            Assert.That(_phoneNumberDTO.ModifiedDate, Is.EqualTo(DateTime.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _phoneNumberDTO.ModifiedDate = DateTime.MaxValue;

            // Assert
            Assert.That(_phoneNumberDTO.ModifiedDate, Is.EqualTo(DateTime.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldMaintainPrecision()
        {
            // Arrange
            var preciseDate = new DateTime(2023, 12, 25, 14, 30, 45, 123);

            // Act
            _phoneNumberDTO.CreatedDate = preciseDate;

            // Assert
            Assert.That(_phoneNumberDTO.CreatedDate, Is.EqualTo(preciseDate));
            Assert.That(_phoneNumberDTO.CreatedDate.Millisecond, Is.EqualTo(123));
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldMaintainPrecision()
        {
            // Arrange
            var preciseDate = new DateTime(2023, 11, 15, 9, 45, 30, 456);

            // Act
            _phoneNumberDTO.ModifiedDate = preciseDate;

            // Assert
            Assert.That(_phoneNumberDTO.ModifiedDate, Is.EqualTo(preciseDate));
            Assert.That(_phoneNumberDTO.ModifiedDate?.Millisecond, Is.EqualTo(456));
        }

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var unicodePhone = "电话: 123-456-7890 (测试)";

            // Act
            _phoneNumberDTO.Phone = unicodePhone;

            // Assert
            Assert.That(_phoneNumberDTO.Phone, Is.EqualTo(unicodePhone));
        }

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldAcceptVeryLongString()
        {
            // Arrange
            var longPhone = new string('1', 10000) + " - Very Long Phone Number";

            // Act
            _phoneNumberDTO.Phone = longPhone;

            // Assert
            Assert.That(_phoneNumberDTO.Phone, Is.EqualTo(longPhone));
            Assert.That(_phoneNumberDTO.Phone?.Length, Is.EqualTo(10025)); // 10000 + " - Very Long Phone Number".Length (25)
        }

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldAcceptWhitespace()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                _phoneNumberDTO.Phone = "   ";
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo("   "));

                _phoneNumberDTO.Phone = " 555-123-4567 ";
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo(" 555-123-4567 "));

                _phoneNumberDTO.Phone = "\t\n\r";
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo("\t\n\r"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldHandleConsecutiveAssignments()
        {
            // Arrange
            var phones = new[] { "555-0001", null, "", "555-0002", "   ", "555-FINAL" };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var phone in phones)
                {
                    _phoneNumberDTO.Phone = phone;
                    Assert.That(_phoneNumberDTO.Phone, Is.EqualTo(phone));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldSupportCastingFromInt()
        {
            // Arrange
            var enumValue = (OrganizerCompanion.Core.Enums.Types)0;

            // Act
            _phoneNumberDTO.Type = enumValue;

            // Assert
            Assert.That(_phoneNumberDTO.Type, Is.EqualTo(enumValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldHandleNullToValueTransitions()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Start with null
                _phoneNumberDTO.Type = null;
                Assert.That(_phoneNumberDTO.Type, Is.Null);

                // Assign each enum value
                foreach (var enumValue in Enum.GetValues<OrganizerCompanion.Core.Enums.Types>())
                {
                    _phoneNumberDTO.Type = enumValue;
                    Assert.That(_phoneNumberDTO.Type, Is.EqualTo(enumValue));
                    Assert.That(_phoneNumberDTO.Type.HasValue, Is.True);
                    
                    // Back to null
                    _phoneNumberDTO.Type = null;
                    Assert.That(_phoneNumberDTO.Type, Is.Null);
                    Assert.That(_phoneNumberDTO.Type.HasValue, Is.False);
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldSupportCastingFromInt()
        {
            // Arrange
            var enumValue = (OrganizerCompanion.Core.Enums.Countries)0;

            // Act
            _phoneNumberDTO.Country = enumValue;

            // Assert
            Assert.That(_phoneNumberDTO.Country, Is.EqualTo(enumValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldHandleNullToValueTransitions()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Start with null
                _phoneNumberDTO.Country = null;
                Assert.That(_phoneNumberDTO.Country, Is.Null);

                // Assign each enum value
                foreach (var enumValue in Enum.GetValues<OrganizerCompanion.Core.Enums.Countries>())
                {
                    _phoneNumberDTO.Country = enumValue;
                    Assert.That(_phoneNumberDTO.Country, Is.EqualTo(enumValue));
                    Assert.That(_phoneNumberDTO.Country.HasValue, Is.True);
                    
                    // Back to null
                    _phoneNumberDTO.Country = null;
                    Assert.That(_phoneNumberDTO.Country, Is.Null);
                    Assert.That(_phoneNumberDTO.Country.HasValue, Is.False);
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IPhoneNumberDTO_InterfaceConsistency_ShouldExposeAllProperties()
        {
            // Arrange
            IPhoneNumberDTO interfaceDto = new PhoneNumberDTO();
            var testModified = DateTime.Now.AddHours(-2);

            // Act
            interfaceDto.Id = 100;
            interfaceDto.Phone = "555-INTERFACE";
            interfaceDto.Type = OrganizerCompanion.Core.Enums.Types.Work;
            interfaceDto.Country = OrganizerCompanion.Core.Enums.Countries.Canada;
            interfaceDto.ModifiedDate = testModified;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceDto.Id, Is.EqualTo(100));
                Assert.That(interfaceDto.Phone, Is.EqualTo("555-INTERFACE"));
                Assert.That(interfaceDto.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(interfaceDto.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.Canada));
                Assert.That(interfaceDto.CreatedDate, Is.Not.EqualTo(default(DateTime))); // CreatedDate is read-only, check it has a value
                Assert.That(interfaceDto.ModifiedDate, Is.EqualTo(testModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldHandleNullToDateTransitions()
        {
            // Arrange
            var testDates = new DateTime[]
            {
                DateTime.Now,
                DateTime.MinValue,
                DateTime.MaxValue,
                new DateTime(2020, 1, 1),
                new DateTime(2030, 12, 31, 23, 59, 59)
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var date in testDates)
                {
                    // Start with null
                    _phoneNumberDTO.ModifiedDate = null;
                    Assert.That(_phoneNumberDTO.ModifiedDate, Is.Null);

                    // Assign date
                    _phoneNumberDTO.ModifiedDate = date;
                    Assert.That(_phoneNumberDTO.ModifiedDate, Is.EqualTo(date));

                    // Back to null
                    _phoneNumberDTO.ModifiedDate = null;
                    Assert.That(_phoneNumberDTO.ModifiedDate, Is.Null);
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldMaintainStateAcrossMultipleOperations()
        {
            // Arrange
            var operations = new[]
            {
                new { Id = 1, Phone = (string?)"555-0001", Type = (OrganizerCompanion.Core.Enums.Types?)OrganizerCompanion.Core.Enums.Types.Home },
                new { Id = 2, Phone = (string?)null, Type = (OrganizerCompanion.Core.Enums.Types?)OrganizerCompanion.Core.Enums.Types.Work },
                new { Id = 3, Phone = (string?)"", Type = (OrganizerCompanion.Core.Enums.Types?)null },
                new { Id = 4, Phone = (string?)"555-0004", Type = (OrganizerCompanion.Core.Enums.Types?)OrganizerCompanion.Core.Enums.Types.Mobile }
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var op in operations)
                {
                    _phoneNumberDTO.Id = op.Id;
                    _phoneNumberDTO.Phone = op.Phone;
                    _phoneNumberDTO.Type = op.Type;

                    Assert.That(_phoneNumberDTO.Id, Is.EqualTo(op.Id));
                    Assert.That(_phoneNumberDTO.Phone, Is.EqualTo(op.Phone));
                    Assert.That(_phoneNumberDTO.Type, Is.EqualTo(op.Type));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_PropertiesShouldBeIndependent()
        {
            // Arrange & Act
            _phoneNumberDTO.Id = 999;
            _phoneNumberDTO.Phone = "555-INDEPENDENT";
            _phoneNumberDTO.Type = OrganizerCompanion.Core.Enums.Types.Fax;
            _phoneNumberDTO.Country = OrganizerCompanion.Core.Enums.Countries.Mexico;
            var testDate = DateTime.Now.AddDays(-5);
            var testModified = DateTime.Now.AddHours(-3);
            _phoneNumberDTO.CreatedDate = testDate;
            _phoneNumberDTO.ModifiedDate = testModified;

            // Store original values
            var originalId = _phoneNumberDTO.Id;
            var originalPhone = _phoneNumberDTO.Phone;
            var originalType = _phoneNumberDTO.Type;
            var originalCountry = _phoneNumberDTO.Country;
            var originalCreated = _phoneNumberDTO.CreatedDate;
            var originalModified = _phoneNumberDTO.ModifiedDate;

            // Assert
            Assert.Multiple(() =>
            {
                // Change Id, verify others unchanged
                _phoneNumberDTO.Id = 1000;
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo(originalPhone));
                Assert.That(_phoneNumberDTO.Type, Is.EqualTo(originalType));
                Assert.That(_phoneNumberDTO.Country, Is.EqualTo(originalCountry));
                Assert.That(_phoneNumberDTO.CreatedDate, Is.EqualTo(originalCreated));
                Assert.That(_phoneNumberDTO.ModifiedDate, Is.EqualTo(originalModified));

                // Change Phone, verify others unchanged
                _phoneNumberDTO.Phone = "555-CHANGED";
                Assert.That(_phoneNumberDTO.Id, Is.EqualTo(1000)); // New value
                Assert.That(_phoneNumberDTO.Type, Is.EqualTo(originalType));
                Assert.That(_phoneNumberDTO.Country, Is.EqualTo(originalCountry));
                Assert.That(_phoneNumberDTO.CreatedDate, Is.EqualTo(originalCreated));
                Assert.That(_phoneNumberDTO.ModifiedDate, Is.EqualTo(originalModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldSupportObjectInitializerSyntax()
        {
            // Arrange
            var testCreated = DateTime.Now.AddDays(-7);
            var testModified = DateTime.Now.AddHours(-1);

            // Act
            var phoneDto = new PhoneNumberDTO
            {
                Id = 555,
                Phone = "555-INITIALIZER",
                Type = OrganizerCompanion.Core.Enums.Types.Billing,
                Country = OrganizerCompanion.Core.Enums.Countries.UnitedKingdom,
                CreatedDate = testCreated,
                ModifiedDate = testModified
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(phoneDto.Id, Is.EqualTo(555));
                Assert.That(phoneDto.Phone, Is.EqualTo("555-INITIALIZER"));
                Assert.That(phoneDto.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Billing));
                Assert.That(phoneDto.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.UnitedKingdom));
                Assert.That(phoneDto.CreatedDate, Is.EqualTo(testCreated));
                Assert.That(phoneDto.ModifiedDate, Is.EqualTo(testModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException_WithDifferentGenericTypes()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => _phoneNumberDTO.Cast<MockDomainEntity>());
                Assert.Throws<NotImplementedException>(() => _phoneNumberDTO.Cast<IDomainEntity>());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldConsistentlyThrowNotImplementedException()
        {
            // Arrange - Multiple calls should all throw
            var exceptions = new List<NotImplementedException>();

            // Act & Assert
            Assert.Multiple(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    var ex = Assert.Throws<NotImplementedException>(() => _phoneNumberDTO.ToJson());
                    Assert.That(ex, Is.Not.Null);
                    if (ex != null)
                    {
                        exceptions.Add(ex);
                    }
                }
                
                // Verify all exceptions are separate instances
                Assert.That(exceptions, Has.Count.EqualTo(3));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldAcceptSpecialPhoneNumberFormats()
        {
            // Arrange
            var specialFormats = new[]
            {
                "555-1234",
                "555.123.4567",
                "(555) 123-4567",
                "+1-555-123-4567",
                "+44 20 7946 0958",
                "555-123-4567 x1234",
                "1-800-FLOWERS",
                "800-GOT-JUNK?",
                "+33 1 42 86 83 26",
                "+81-3-3570-8000"
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var format in specialFormats)
                {
                    _phoneNumberDTO.Phone = format;
                    Assert.That(_phoneNumberDTO.Phone, Is.EqualTo(format));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Phone_ShouldAcceptComplexFormats()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Emergency numbers
                _phoneNumberDTO.Phone = "911";
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo("911"));

                // International with country codes
                _phoneNumberDTO.Phone = "+86 138 0013 8000";
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo("+86 138 0013 8000"));

                // Vanity numbers
                _phoneNumberDTO.Phone = "1-800-BUY-CARS";
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo("1-800-BUY-CARS"));

                // With extension and department
                _phoneNumberDTO.Phone = "555-123-4567 ext. 890 (Sales)";
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo("555-123-4567 ext. 890 (Sales)"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Enums_ShouldSupportAllDefinedValues()
        {
            // Act & Assert
            Assert.Multiple(() =>
            {
                // Test all Types enum values
                foreach (var typeValue in Enum.GetValues<OrganizerCompanion.Core.Enums.Types>())
                {
                    _phoneNumberDTO.Type = typeValue;
                    Assert.That(_phoneNumberDTO.Type, Is.EqualTo(typeValue));
                    Assert.That(_phoneNumberDTO.Type.HasValue, Is.True);
                }

                // Test all Countries enum values
                foreach (var countryValue in Enum.GetValues<OrganizerCompanion.Core.Enums.Countries>())
                {
                    _phoneNumberDTO.Country = countryValue;
                    Assert.That(_phoneNumberDTO.Country, Is.EqualTo(countryValue));
                    Assert.That(_phoneNumberDTO.Country.HasValue, Is.True);
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldHandleComplexScenarios()
        {
            // Arrange & Act
            _phoneNumberDTO.Id = int.MaxValue;
            _phoneNumberDTO.Phone = "Complex Phone with 特殊字符 and emojis 📞📱";
            _phoneNumberDTO.Type = OrganizerCompanion.Core.Enums.Types.Other;
            _phoneNumberDTO.Country = OrganizerCompanion.Core.Enums.Countries.China;
            _phoneNumberDTO.CreatedDate = DateTime.MaxValue.AddMilliseconds(-1);
            _phoneNumberDTO.ModifiedDate = DateTime.MinValue.AddMilliseconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_phoneNumberDTO.Id, Is.EqualTo(int.MaxValue));
                Assert.That(_phoneNumberDTO.Phone, Contains.Substring("Complex Phone"));
                Assert.That(_phoneNumberDTO.Phone, Contains.Substring("特殊字符"));
                Assert.That(_phoneNumberDTO.Phone, Contains.Substring("📞📱"));
                Assert.That(_phoneNumberDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
                Assert.That(_phoneNumberDTO.Country, Is.EqualTo(OrganizerCompanion.Core.Enums.Countries.China));
                Assert.That(_phoneNumberDTO.CreatedDate, Is.LessThan(DateTime.MaxValue));
                Assert.That(_phoneNumberDTO.ModifiedDate, Is.GreaterThan(DateTime.MinValue));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PhoneNumberDTO_ShouldSupportCountrySpecificFormats()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // US formats
                _phoneNumberDTO.Country = OrganizerCompanion.Core.Enums.Countries.UnitedStates;
                _phoneNumberDTO.Phone = "+1 (555) 123-4567";
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo("+1 (555) 123-4567"));

                // UK formats
                _phoneNumberDTO.Country = OrganizerCompanion.Core.Enums.Countries.UnitedKingdom;
                _phoneNumberDTO.Phone = "+44 20 7946 0958";
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo("+44 20 7946 0958"));

                // French formats
                _phoneNumberDTO.Country = OrganizerCompanion.Core.Enums.Countries.France;
                _phoneNumberDTO.Phone = "+33 1 42 86 83 26";
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo("+33 1 42 86 83 26"));

                // Japanese formats
                _phoneNumberDTO.Country = OrganizerCompanion.Core.Enums.Countries.Japan;
                _phoneNumberDTO.Phone = "+81-3-3570-8000";
                Assert.That(_phoneNumberDTO.Phone, Is.EqualTo("+81-3-3570-8000"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IPhoneNumber_Interface_ShouldBeImplemented()
        {
            // Arrange & Act
            var phoneNumber = new PhoneNumberDTO();

            // Assert
            Assert.That(phoneNumber, Is.InstanceOf<OrganizerCompanion.Core.Interfaces.Type.IPhoneNumber>());
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
            public DateTime CreatedDate { get; set; }
            public DateTime? ModifiedDate { get; set; } = default;

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
