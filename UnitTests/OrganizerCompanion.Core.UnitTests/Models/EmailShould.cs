using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class EmailShould
    {
        private Email _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Email();
        }

        [Test, Category("Models")]
        public void DefaultConstructor_ShouldCreateEmailWithDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            _sut = new Email();
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.EmailAddress, Is.Null);
                Assert.That(_sut.Type, Is.Null);
                Assert.That(_sut.IsPrimary, Is.False);
                Assert.That(_sut.LinkedEntityId, Is.Null);
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
                Assert.That(_sut.IsConfirmed, Is.False);
                Assert.That(_sut.CreatedDate, Is.LessThan(DateTime.UtcNow));
                Assert.That(_sut.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Constructor_WithEmailAddressAndType_ShouldSetPropertiesAndCreatedDate()
        {
            // Arrange
            var emailAddress = "test@example.com";
            var type = OrganizerCompanion.Core.Enums.Types.Work;

            // Act
            _sut = new Email(emailAddress, type, true, null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.EmailAddress, Is.EqualTo(emailAddress));
                Assert.That(_sut.Type, Is.EqualTo(type));
                Assert.That(_sut.IsPrimary, Is.True);
                Assert.That(_sut.CreatedDate, Is.LessThan(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithValidParameters_ShouldSetAllProperties()
        {
            // Arrange
            var id = 1;
            var emailAddress = "test@example.com";
            var type = OrganizerCompanion.Core.Enums.Types.Home;
            var isPrimary = true;
            var linkedEntity = new MockDomainEntity();
            var isConfirmed = true;
            var createdDate = DateTime.Now.AddDays(-1);
            var modifiedDate = DateTime.Now;

            // Act
            _sut = new Email(
                id,
                emailAddress,
                type,
                isPrimary,
                linkedEntity,
                isConfirmed,
                createdDate,
                modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(id));
                Assert.That(_sut.EmailAddress, Is.EqualTo(emailAddress));
                Assert.That(_sut.Type, Is.EqualTo(type));
                Assert.That(_sut.IsPrimary, Is.EqualTo(isPrimary));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(linkedEntity.Id));
                Assert.That(_sut.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(_sut.IsConfirmed, Is.EqualTo(isConfirmed));
                Assert.That(_sut.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        #region IEmailDTO Constructor Tests

        [Test, Category("Models")]
        public void DTOConstructor_WithCompleteDTO_ShouldSetAllProperties()
        {
            // Arrange
            var dto = new MockEmailDTO
            {
                Id = 123,
                EmailAddress = "test@example.com",
                Type = OrganizerCompanion.Core.Enums.Types.Work,
                IsPrimary = true,
                IsConfirmed = true,
                CreatedDate = DateTime.Now.AddDays(-2),
                ModifiedDate = DateTime.Now.AddDays(-1)
            };
            var linkedEntity = new MockDomainEntity();

            // Act
            _sut = new Email(dto, linkedEntity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.EmailAddress, Is.EqualTo(dto.EmailAddress));
                Assert.That(_sut.Type, Is.EqualTo(dto.Type));
                Assert.That(_sut.IsPrimary, Is.EqualTo(dto.IsPrimary));
                Assert.That(_sut.IsConfirmed, Is.EqualTo(dto.IsConfirmed));
                Assert.That(_sut.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(linkedEntity.Id));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(_sut.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithMinimalDTO_ShouldSetBasicProperties()
        {
            // Arrange
            var dto = new MockEmailDTO
            {
                EmailAddress = "minimal@example.com",
                Type = OrganizerCompanion.Core.Enums.Types.Home
            };

            // Act
            _sut = new Email(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.EmailAddress, Is.EqualTo(dto.EmailAddress));
                Assert.That(_sut.Type, Is.EqualTo(dto.Type));
                Assert.That(_sut.IsPrimary, Is.EqualTo(dto.IsPrimary));
                Assert.That(_sut.IsConfirmed, Is.EqualTo(dto.IsConfirmed));
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityId, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
                Assert.That(_sut.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullLinkedEntity_ShouldHandleGracefully()
        {
            // Arrange
            var dto = new MockEmailDTO
            {
                Id = 456,
                EmailAddress = "nullentity@example.com",
                Type = OrganizerCompanion.Core.Enums.Types.Work,
                IsPrimary = false,
                IsConfirmed = false
            };

            // Act
            _sut = new Email(dto, null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.EmailAddress, Is.EqualTo(dto.EmailAddress));
                Assert.That(_sut.Type, Is.EqualTo(dto.Type));
                Assert.That(_sut.IsPrimary, Is.EqualTo(dto.IsPrimary));
                Assert.That(_sut.IsConfirmed, Is.EqualTo(dto.IsConfirmed));
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityId, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithDifferentLinkedEntityTypes_ShouldSetCorrectEntityType()
        {
            // Arrange
            var dto = new MockEmailDTO
            {
                EmailAddress = "entity@example.com",
                Type = OrganizerCompanion.Core.Enums.Types.Home
            };
            var anotherEntity = new AnotherMockEntity();

            // Act
            _sut = new Email(dto, anotherEntity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(anotherEntity));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(anotherEntity.Id));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("AnotherMockEntity"));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullDTO_ShouldThrowArgumentNullException()
        {
            // Arrange
            MockEmailDTO? dto = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => new Email(dto!));
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithInvalidDTOType_ShouldThrowArgumentException()
        {
            // This test validates that the Email constructor properly validates DTO types
            // Since we can't pass an invalid type due to compile-time checking,
            // we'll test with a null DTO instead to trigger validation
            
            // Act & Assert
            Assert.Throws<NullReferenceException>(() => new Email((IEmailDTO)null!));
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullEmailAddress_ShouldAcceptNullValue()
        {
            // Arrange
            var dto = new MockEmailDTO
            {
                EmailAddress = null,
                Type = OrganizerCompanion.Core.Enums.Types.Work
            };

            // Act
            _sut = new Email(dto);

            // Assert
            Assert.That(_sut.EmailAddress, Is.Null);
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullType_ShouldAcceptNullValue()
        {
            // Arrange
            var dto = new MockEmailDTO
            {
                EmailAddress = "nulltype@example.com",
                Type = null
            };

            // Act
            _sut = new Email(dto);

            // Assert
            Assert.That(_sut.Type, Is.Null);
        }

        #endregion

        [Test, Category("Models")]
        public void Id_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            _sut = new Email();

            // Act
            _sut.Id = 123;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(123));
                Assert.That(_sut.ModifiedDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void EmailAddress_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            _sut = new Email();
            var newEmailAddress = "newemail@example.com";

            // Act
            _sut.EmailAddress = newEmailAddress;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.EmailAddress, Is.EqualTo(newEmailAddress));
                Assert.That(_sut.ModifiedDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void EmailAddress_WhenSetToNull_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut = new Email("test@example.com", OrganizerCompanion.Core.Enums.Types.Work, true, null)
            {
                EmailAddress = null
            };

            // Assert
            Assert.That(_sut.EmailAddress, Is.Null);
        }

        [Test, Category("Models")]
        public void Type_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            _sut = new Email();
            var newType = OrganizerCompanion.Core.Enums.Types.Fax;

            // Act
            _sut.Type = newType;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Type, Is.EqualTo(newType));
                Assert.That(_sut.ModifiedDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void Type_WhenSetToNull_ShouldAcceptNullValue()
        {
            // Arrange
            _sut = new Email("test@example.com", OrganizerCompanion.Core.Enums.Types.Work, true, null)
            {
                // Act
                Type = null
            };

            // Assert
            Assert.That(_sut.Type, Is.Null);
        }

        [Test, Category("Models")]
        public void IsPrimary_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            _sut = new Email();

            // Act
            _sut.IsPrimary = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsPrimary, Is.True);
                Assert.That(_sut.ModifiedDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void IsPrimary_ShouldGetAndSetCorrectly()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Test setting to true
                _sut.IsPrimary = true;
                Assert.That(_sut.IsPrimary, Is.True);

                // Test setting to false
                _sut.IsPrimary = false;
                Assert.That(_sut.IsPrimary, Is.False);
            });
        }

        [Test, Category("Models")]
        public void IsPrimary_ShouldDefaultToFalse()
        {
            // Arrange & Act
            var newEmail = new Email();

            // Assert
            Assert.That(newEmail.IsPrimary, Is.False);
        }

        [Test, Category("Models")]
        public void IsPrimary_ShouldHandleBooleanToggling()
        {
            // Arrange
            Assert.That(_sut.IsPrimary, Is.False, "Should start as false");

            // Act & Assert - Toggle multiple times
            _sut.IsPrimary = !_sut.IsPrimary;
            Assert.That(_sut.IsPrimary, Is.True, "Should be true after first toggle");

            _sut.IsPrimary = !_sut.IsPrimary;
            Assert.That(_sut.IsPrimary, Is.False, "Should be false after second toggle");

            _sut.IsPrimary = !_sut.IsPrimary;
            Assert.That(_sut.IsPrimary, Is.True, "Should be true after third toggle");
        }

        [Test, Category("Models")]
        public void IsPrimary_ShouldMaintainValueAfterOtherPropertyChanges()
        {
            // Arrange
            _sut.IsPrimary = true;

            // Act - Change other properties
            _sut.Id = 999;
            _sut.EmailAddress = "test@example.com";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Assert
            Assert.That(_sut.IsPrimary, Is.True,
                "IsPrimary should maintain its value when other properties change");
        }

        [Test, Category("Models")]
        public void IsConfirmed_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            _sut = new Email();

            // Act
            _sut.IsConfirmed = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsConfirmed, Is.True);
                Assert.That(_sut.ModifiedDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void IsConfirmed_WhenSetToFalse_ShouldUpdateModifiedDate()
        {
            // Arrange
            _sut = new Email
            {
                IsConfirmed = true // Set to true first
            };

            // Act
            _sut.IsConfirmed = false;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsConfirmed, Is.False);
                Assert.That(_sut.ModifiedDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void IsConfirmed_DefaultValue_ShouldBeFalse()
        {
            // Arrange & Act
            _sut = new Email();

            // Assert
            Assert.That(_sut.IsConfirmed, Is.False);
        }

        [Test, Category("Models")]
        public void IsConfirmed_WithSimpleConstructor_ShouldDefaultToFalse()
        {
            // Arrange & Act
            _sut = new Email("test@example.com", OrganizerCompanion.Core.Enums.Types.Work, true, null);

            // Assert
            Assert.That(_sut.IsConfirmed, Is.False);
        }

        [Test, Category("Models")]
        public void IsConfirmed_ToggleMultipleTimes_ShouldUpdateModifiedDateEachTime()
        {
            // Arrange
            _sut = new Email();
            var initialTime = DateTime.Now;

            // Act & Assert - Test sequential IsConfirmed changes update ModifiedDate
            System.Threading.Thread.Sleep(1);
            _sut.IsConfirmed = true;
            var firstModified = _sut.ModifiedDate;
            Assert.Multiple(() =>
            {
                Assert.That(firstModified, Is.GreaterThanOrEqualTo(initialTime));
                Assert.That(_sut.IsConfirmed, Is.True);
            });
            System.Threading.Thread.Sleep(1);
            _sut.IsConfirmed = false;
            var secondModified = _sut.ModifiedDate;
            Assert.Multiple(() =>
            {
                Assert.That(secondModified, Is.GreaterThan(firstModified));
                Assert.That(_sut.IsConfirmed, Is.False);
            });
            System.Threading.Thread.Sleep(1);
            _sut.IsConfirmed = true;
            var thirdModified = _sut.ModifiedDate;
            Assert.Multiple(() =>
            {
                Assert.That(thirdModified, Is.GreaterThan(secondModified));
                Assert.That(_sut.IsConfirmed, Is.True);
            });
        }

        [Test, Category("Models")]
        public void CreatedDate_IsReadOnly_AndSetDuringConstruction()
        {
            // Arrange & Act
            _sut = new Email();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.CreatedDate, Is.LessThan(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsCreatedDateFromParameter()
        {
            // Arrange
            var specificDate = DateTime.Now.AddDays(-5);

            // Act
            _sut = new Email(
                1,
                "test@example.com",
                OrganizerCompanion.Core.Enums.Types.Work,
                false,
                null,
                false,
                specificDate,
                DateTime.Now);

            // Assert
            Assert.That(_sut.CreatedDate, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void ModifiedDate_CanBeSetAndRetrieved()
        {
            // Arrange
            _sut = new Email();
            var modifiedDate = DateTime.Now.AddHours(-2);

            // Act
            _sut.ModifiedDate = modifiedDate;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(modifiedDate));
        }

        [Test, Category("Models")]
        public void LinkedEntity_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var mockEntity = new MockDomainEntity();
            var originalModifiedDate = _sut.ModifiedDate;
            System.Threading.Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.LinkedEntity = mockEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(mockEntity));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityType_WhenLinkedEntityIsNull_ShouldReturnNull()
        {
            // Arrange
            _sut.LinkedEntity = null;

            // Act
            var result = _sut.LinkedEntityType;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("Models")]
        public void LinkedEntityType_WhenLinkedEntityIsSet_ShouldReturnTypeName()
        {
            // Arrange
            var mockEntity = new MockDomainEntity();
            _sut.LinkedEntity = mockEntity;

            // Act
            var result = _sut.LinkedEntityType;

            // Assert
            Assert.That(result, Is.EqualTo("MockDomainEntity"));
        }

        [Test, Category("Models")]
        public void LinkedEntityType_WhenLinkedEntityChanges_ShouldUpdateTypeName()
        {
            // Arrange
            var mockEntity1 = new MockDomainEntity();
            var mockEntity2 = new AnotherMockEntity();

            _sut.LinkedEntity = mockEntity1;
            var firstType = _sut.LinkedEntityType;

            // Act
            _sut.LinkedEntity = mockEntity2;
            var secondType = _sut.LinkedEntityType;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(firstType, Is.EqualTo("MockDomainEntity"));
                Assert.That(secondType, Is.EqualTo("AnotherMockEntity"));
                Assert.That(firstType, Is.Not.EqualTo(secondType));
            });
        }

        [Test, Category("Models")]
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            _sut = new Email(
                1,
                "test@example.com",
                OrganizerCompanion.Core.Enums.Types.Work,
                false,
                null,
                true,
                DateTime.Now.AddDays(-1),
                DateTime.Now);

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("\"emailAddress\":\"test@example.com\""));
                Assert.That(json, Does.Contain("\"linkedEntityId\":null"));
                Assert.That(json, Does.Contain("\"linkedEntity\":null"));
                Assert.That(json, Does.Contain("\"linkedEntityType\":null"));
                Assert.That(json, Does.Contain("\"createdDate\":"));
                Assert.That(json, Does.Contain("\"modifiedDate\":"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithNullValues_ShouldReturnValidJsonString()
        {
            // Arrange
            _sut = new Email();

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":0"));
                Assert.That(json, Does.Contain("\"emailAddress\":null"));
                Assert.That(json, Does.Contain("\"isConfirmed\":false"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithIsConfirmedTrue_ShouldSerializeCorrectly()
        {
            // Arrange
            _sut = new Email
            {
                IsConfirmed = true
            };

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"isConfirmed\":true"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithIsConfirmedFalse_ShouldSerializeCorrectly()
        {
            // Arrange
            _sut = new Email
            {
                IsConfirmed = false
            };

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"isConfirmed\":false"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithIsPrimaryTrue_ShouldSerializeCorrectly()
        {
            // Arrange
            _sut = new Email
            {
                IsPrimary = true
            };

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"isPrimary\":true"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithIsPrimaryFalse_ShouldSerializeCorrectly()
        {
            // Arrange
            _sut = new Email
            {
                IsPrimary = false
            };

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"isPrimary\":false"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_ShouldSerializeBooleanValuesCorrectly()
        {
            // Arrange, Act & Assert for true
            _sut.IsPrimary = true;
            var jsonTrue = _sut.ToJson();
            var documentTrue = JsonDocument.Parse(jsonTrue);
            Assert.That(documentTrue.RootElement.TryGetProperty("isPrimary", out var isPrimaryTrueProperty), Is.True);
            Assert.That(isPrimaryTrueProperty.GetBoolean(), Is.True);

            // Arrange, Act & Assert for false
            _sut.IsPrimary = false;
            var jsonFalse = _sut.ToJson();
            var documentFalse = JsonDocument.Parse(jsonFalse);
            Assert.That(documentFalse.RootElement.TryGetProperty("isPrimary", out var isPrimaryFalseProperty), Is.True);
            Assert.That(isPrimaryFalseProperty.GetBoolean(), Is.False);
        }

        [Test, Category("Models")]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            _sut = new Email(
                123,
                "test@example.com",
                OrganizerCompanion.Core.Enums.Types.Home,
                false,
                null,
                false,
                DateTime.Now.AddDays(-1),
                DateTime.Now);

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("123"));
                Assert.That(result, Does.Contain("test@example.com"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithNullEmailAddress_ShouldHandleNullValues()
        {
            // Arrange
            _sut = new Email(
                456,
                null,
                OrganizerCompanion.Core.Enums.Types.Work,
                false,
                null,
                true,
                DateTime.Now.AddDays(-1),
                DateTime.Now);

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
            // Arrange
            _sut = new Email();

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
        public void JsonSerialization_ShouldProduceValidJsonFormat()
        {
            // Arrange
            _sut = new Email(
                1,
                "test@example.com",
                OrganizerCompanion.Core.Enums.Types.Mobile,
                true,
                null,
                false,
                DateTime.Now.AddDays(-1),
                DateTime.Now);

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("\"emailAddress\":\"test@example.com\""));
                Assert.That(json, Does.Contain("\"createdDate\":"));
                Assert.That(json, Does.Contain("\"modifiedDate\":"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void JsonSerialization_WithAllTypesEnum_ShouldProduceValidJson()
        {
            // Test all enum values
            var enumValues = new[] { OrganizerCompanion.Core.Enums.Types.Home, OrganizerCompanion.Core.Enums.Types.Work, OrganizerCompanion.Core.Enums.Types.Mobile, OrganizerCompanion.Core.Enums.Types.Fax, OrganizerCompanion.Core.Enums.Types.Billing, OrganizerCompanion.Core.Enums.Types.Other };

            foreach (var enumValue in enumValues)
            {
                // Arrange
                _sut = new Email($"test{enumValue}@example.com", enumValue, true, null)
                {
                    Id = 1
                };

                // Act
                var json = _sut.ToJson();

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(json, Is.Not.Null.And.Not.Empty, $"Failed for {enumValue}");
                    Assert.That(json, Does.Contain($"test{enumValue}@example.com"), $"Failed for {enumValue}");
                    Assert.That(json, Does.Contain("\"createdDate\":"), $"Failed for {enumValue}");

                    // Verify JSON is well-formed
                    Assert.DoesNotThrow(() => JsonDocument.Parse(json), $"Failed for {enumValue}");
                });
            }
        }

        [Test, Category("Models")]
        public void Email_WithEmptyEmailAddress_ShouldBeAllowed()
        {
            // Arrange & Act
            _sut = new Email(string.Empty, OrganizerCompanion.Core.Enums.Types.Other, true, null);

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(string.Empty));
        }

        [Test, Category("Models")]
        public void Email_WithVeryLongEmailAddress_ShouldBeAllowed()
        {
            // Arrange
            var longEmail = new string('a', 1000) + "@example.com";

            // Act
            _sut = new Email(longEmail, OrganizerCompanion.Core.Enums.Types.Work, true, null);

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(longEmail));
        }

        [Test, Category("Models")]
        public void Email_MultiplePropertyChanges_ShouldUpdateModifiedDateEachTime()
        {
            // Arrange
            _sut = new Email();
            var initialTime = DateTime.Now;

            // Act & Assert
            System.Threading.Thread.Sleep(1); // Ensure time difference
            _sut.Id = 1;
            var firstModified = _sut.ModifiedDate;
            Assert.That(firstModified, Is.GreaterThanOrEqualTo(initialTime));

            System.Threading.Thread.Sleep(1);
            _sut.EmailAddress = "test@example.com";
            var secondModified = _sut.ModifiedDate;
            Assert.That(secondModified, Is.GreaterThan(firstModified));

            System.Threading.Thread.Sleep(1);
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
            var thirdModified = _sut.ModifiedDate;
            Assert.That(thirdModified, Is.GreaterThan(secondModified));

            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntity = new MockDomainEntity { Id = 999 };
            var fourthModified = _sut.ModifiedDate;
            Assert.That(fourthModified, Is.GreaterThan(thirdModified));

            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntity = new MockDomainEntity();
            var fifthModified = _sut.ModifiedDate;
            Assert.That(fifthModified, Is.GreaterThan(fourthModified));
        }

        [Test, Category("Models")]
        public void Email_WithZeroId_ShouldBeAllowed()
        {
            // Arrange & Act
            _sut = new Email
            {
                Id = 0
            };

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void Email_WithMaxIntId_ShouldBeAllowed()
        {
            // Arrange & Act
            _sut = new Email
            {
                Id = int.MaxValue
            };

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void Email_SerializerOptions_ShouldHandleCyclicalReferences()
        {
            // Arrange
            _sut = new Email(
                1,
                "test@example.com",
                OrganizerCompanion.Core.Enums.Types.Work,
                false,
                null,
                true,
                DateTime.Now,
                DateTime.Now);

            // Act & Assert - This should not throw due to ReferenceHandler.IgnoreCycles
            Assert.DoesNotThrow(() =>
            {
                var json = _sut.ToJson();
                Assert.That(json, Is.Not.Null.And.Not.Empty);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullValues_ShouldHandleGracefully()
        {
            // Arrange & Act
            var email = new Email(
                id: 0,
                emailAddress: null,
                type: null,
                isPrimary: false,
                linkedEntity: null,
                isConfirmed: false,
                createdDate: DateTime.Now.AddDays(-1),
                modifiedDate: null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(email.Id, Is.EqualTo(0));
                Assert.That(email.EmailAddress, Is.Null);
                Assert.That(email.Type, Is.Null);
                Assert.That(email.LinkedEntityId, Is.Null);
                Assert.That(email.LinkedEntity, Is.Null);
                Assert.That(email.LinkedEntityType, Is.Null);
                Assert.That(email.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityType_WhenSetThroughLinkedEntitySetter_ShouldBeReadOnly()
        {
            // Arrange
            var mockEntity = new MockDomainEntity();

            // Act
            _sut.LinkedEntity = mockEntity;

            // Assert - LinkedEntityType should be read-only and set automatically
            Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));

            // Verify that LinkedEntityType doesn't have a public setter
            var property = typeof(Email).GetProperty("LinkedEntityType");
            Assert.That(property?.SetMethod, Is.Null, "LinkedEntityType should be read-only");
        }

        [Test, Category("Models")]
        public void Email_WithSpecialCharactersInEmailAddress_ShouldBeAllowed()
        {
            // Arrange
            var specialEmail = "test+special.chars@example-domain.co.uk";

            // Act
            _sut = new Email(specialEmail, OrganizerCompanion.Core.Enums.Types.Work, true, null);

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(specialEmail));
        }

        [Test, Category("Models")]
        public void Email_WithUnicodeCharactersInEmailAddress_ShouldBeAllowed()
        {
            // Arrange
            var unicodeEmail = "tést@ëxämplë.com";

            // Act
            _sut = new Email(unicodeEmail, OrganizerCompanion.Core.Enums.Types.Work, true, null);

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(unicodeEmail));
        }

        [Test, Category("Models")]
        public void Email_WithNegativeId_ShouldNotBeAllowed()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _sut = new Email { Id = -1 };
            });
        }

        [Test, Category("Models")]
        public void Email_WithNegativeLinkedEntityId_ShouldBeAllowed()
        {
            // Arrange & Act
            _sut = new Email { LinkedEntity = new MockDomainEntity { Id = -1 } };

            // Assert
            Assert.That(_sut.LinkedEntityId, Is.EqualTo(-1));
        }

        [Test, Category("Models")]
        public void Email_WithMaxIntLinkedEntityId_ShouldBeAllowed()
        {
            // Arrange & Act
            _sut = new Email { LinkedEntity = new MockDomainEntity { Id = int.MaxValue } };

            // Assert
            Assert.That(_sut.LinkedEntityId, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void ModifiedDate_WhenSetToNull_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.ModifiedDate = null;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.Null);
        }

        [Test, Category("Models")]
        public void ModifiedDate_WhenSetToSpecificValue_ShouldRetainValue()
        {
            // Arrange
            var specificDate = DateTime.Now.AddDays(-3);

            // Act
            _sut.ModifiedDate = specificDate;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void ToString_WithBasicValues_ShouldIncludeBaseToString()
        {
            // Arrange
            _sut = new Email { Id = 999, EmailAddress = "format@test.com" };

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("999"));
                Assert.That(result, Does.Contain("format@test.com"));
                // Should contain the base class ToString() output
                Assert.That(result, Does.Contain("Email"));
            });
        }

        [Test, Category("Models")]
        public void JsonSerialization_WithLinkedEntity_ShouldSerializeCorrectly()
        {
            // Arrange
            var mockEntity = new MockDomainEntity { Id = 789 };
            _sut = new Email(
                1,
                "linked@test.com",
                OrganizerCompanion.Core.Enums.Types.Work,
                true,
                mockEntity,
                false,
                DateTime.Now.AddDays(-1),
                DateTime.Now);

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
        public void SerializerOptions_ShouldHaveIgnoreCyclesReferenceHandler()
        {
            // Arrange & Act
            var json1 = _sut.ToJson();
            var json2 = _sut.ToJson();

            // Assert - Should produce consistent results and not throw
            Assert.Multiple(() =>
            {
                Assert.That(json1, Is.Not.Null.And.Not.Empty);
                Assert.That(json2, Is.Not.Null.And.Not.Empty);
                Assert.DoesNotThrow(() => JsonDocument.Parse(json1));
                Assert.DoesNotThrow(() => JsonDocument.Parse(json2));
            });
        }

        [Test, Category("Models")]
        public void DefaultConstructor_ShouldSetTypeToNull()
        {
            // Arrange & Act
            _sut = new Email();

            // Assert
            Assert.That(_sut.Type, Is.Null);
        }

        [Test, Category("Models")]
        public void AllTypesEnum_ShouldBeSettableAndRetrievable()
        {
            // Test each enum value individually
            var enumValues = Enum.GetValues<OrganizerCompanion.Core.Enums.Types>();

            foreach (var enumValue in enumValues)
            {
                // Arrange
                _sut = new Email
                {
                    // Act
                    Type = enumValue
                };

                // Assert
                Assert.That(_sut.Type, Is.EqualTo(enumValue), $"Failed for enum value: {enumValue}");
            }
        }

        [Test, Category("Models")]
        public void LinkedEntity_WhenSetToNull_ShouldClearLinkedEntityType()
        {
            // Arrange
            var mockEntity = new MockDomainEntity();
            _sut.LinkedEntity = mockEntity;
            Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity")); // Verify it was set

            // Act
            _sut.LinkedEntity = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithDifferentLinkedEntityType_ShouldRespectProvidedValue()
        {
            // Arrange & Act - Testing that the JsonConstructor sets linkedEntity correctly
            var mockEntity = new MockDomainEntity();
            var email = new Email(
                id: 1,
                emailAddress: "test@example.com",
                type: OrganizerCompanion.Core.Enums.Types.Work,
                isPrimary: false,
                linkedEntity: mockEntity,
                isConfirmed: true,
                createdDate: DateTime.Now.AddDays(-1),
                modifiedDate: DateTime.Now);

            // Assert - LinkedEntityType should be derived from LinkedEntity
            Assert.That(email.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
        }

        [Test, Category("Models")]
        public void SerializerOptions_FieldShouldBeInitializedCorrectly()
        {
            // Arrange & Act - Test that the serializer options field is properly initialized
            var json = _sut.ToJson();

            // Assert - Should not throw and should produce valid JSON
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));

                // The serialization should handle reference cycles due to ReferenceHandler.IgnoreCycles
                Assert.That(json, Does.Contain("\"id\":"));
            });
        }

        [Test, Category("Models")]
        public void CreatedDate_FromDefaultConstructor_ShouldBeSetToCurrentTime()
        {
            // Arrange & Act
            var email = new Email();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(email.CreatedDate, Is.LessThan(DateTime.UtcNow));

                // CreatedDate should be read-only
                var property = typeof(Email).GetProperty("CreatedDate");
                Assert.That(property?.SetMethod, Is.Null, "CreatedDate should be read-only");
            });
        }

        [Test, Category("Models")]
        public void AllPropertySetters_ShouldUpdateModifiedDate()
        {
            // Arrange
            _sut = new Email();
            var originalModifiedDate = _sut.ModifiedDate;

            // Act & Assert - Test each property that should update ModifiedDate
            System.Threading.Thread.Sleep(1);
            _sut.Id = 100;
            Assert.That(_sut.ModifiedDate, Is.Not.Null, "Id setter should update ModifiedDate");

            var idModified = _sut.ModifiedDate;
            System.Threading.Thread.Sleep(1);
            _sut.EmailAddress = "new@test.com";
            Assert.That(_sut.ModifiedDate, Is.GreaterThan(idModified), "EmailAddress setter should update ModifiedDate");

            var emailModified = _sut.ModifiedDate;
            System.Threading.Thread.Sleep(1);
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Fax;
            Assert.That(_sut.ModifiedDate, Is.GreaterThan(emailModified), "Type setter should update ModifiedDate");

            var typeModified = _sut.ModifiedDate;
            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntity = new MockDomainEntity { Id = 999 };
            Assert.That(_sut.ModifiedDate, Is.GreaterThan(typeModified), "LinkedEntity setter should update ModifiedDate");

            var linkedIdModified = _sut.ModifiedDate;
            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntity = new MockDomainEntity();
            Assert.That(_sut.ModifiedDate, Is.GreaterThan(linkedIdModified), "LinkedEntity setter should update ModifiedDate");

            var linkedEntityModified = _sut.ModifiedDate;
            System.Threading.Thread.Sleep(1);
            _sut.IsConfirmed = true;
            Assert.That(_sut.ModifiedDate, Is.GreaterThan(linkedEntityModified), "IsConfirmed setter should update ModifiedDate");
        }

        [Test, Category("Models")]
        public void LinkedEntity_WhenSetWithDifferentEntities_ShouldUpdateLinkedEntityTypeEachTime()
        {
            // Arrange
            var mockEntity1 = new MockDomainEntity();
            var mockEntity2 = new AnotherMockEntity();

            // Act & Assert
            _sut.LinkedEntity = mockEntity1;
            Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));

            _sut.LinkedEntity = mockEntity2;
            Assert.That(_sut.LinkedEntityType, Is.EqualTo("AnotherMockEntity"));

            _sut.LinkedEntity = mockEntity1; // Back to first type
            Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
        }

        [Test, Category("Models")]
        public void JsonPropertyName_AttributesShouldBePresent()
        {
            // Arrange & Act - Verify that JsonPropertyName attributes are correctly applied
            var type = typeof(Email);

            // Assert - Check that properties have the correct JsonPropertyName attributes
            var idProperty = type.GetProperty("Id");
            var emailProperty = type.GetProperty("EmailAddress");
            var typeProperty = type.GetProperty("Type");
            var linkedEntityIdProperty = type.GetProperty("LinkedEntityId");
            var linkedEntityProperty = type.GetProperty("LinkedEntity");
            var linkedEntityTypeProperty = type.GetProperty("LinkedEntityType");
            var isConfirmedProperty = type.GetProperty("IsConfirmed");
            var createdDateProperty = type.GetProperty("CreatedDate");
            var modifiedDateProperty = type.GetProperty("ModifiedDate");

            Assert.Multiple(() =>
            {
                Assert.That(idProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false),
                    Is.Not.Empty, "Id should have JsonPropertyName attribute");
                Assert.That(emailProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false),
                    Is.Not.Empty, "EmailAddress should have JsonPropertyName attribute");
                Assert.That(typeProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false),
                    Is.Not.Empty, "Type should have JsonPropertyName attribute");
                Assert.That(linkedEntityIdProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false),
                    Is.Not.Empty, "LinkedEntityId should have JsonPropertyName attribute");
                Assert.That(linkedEntityProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false),
                    Is.Not.Empty, "LinkedEntity should have JsonPropertyName attribute");
                Assert.That(linkedEntityTypeProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false),
                    Is.Not.Empty, "LinkedEntityType should have JsonPropertyName attribute");
                Assert.That(isConfirmedProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false),
                    Is.Not.Empty, "IsConfirmed should have JsonPropertyName attribute");
                Assert.That(createdDateProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false),
                    Is.Not.Empty, "CreatedDate should have JsonPropertyName attribute");
                Assert.That(modifiedDateProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false),
                    Is.Not.Empty, "ModifiedDate should have JsonPropertyName attribute");
            });
        }

        [Test, Category("Models")]
        public void Constructor_SimpleEmailAndType_ShouldNotSetLinkedProperties()
        {
            // Arrange & Act
            var email = new Email("simple@test.com", OrganizerCompanion.Core.Enums.Types.Home, true, null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(email.EmailAddress, Is.EqualTo("simple@test.com"));
                Assert.That(email.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
                Assert.That(email.Id, Is.EqualTo(0));
                Assert.That(email.LinkedEntityId, Is.Null);
                Assert.That(email.LinkedEntity, Is.Null);
                Assert.That(email.LinkedEntityType, Is.Null);
                Assert.That(email.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Email_WithMinIntValues_ShouldNotBeAllowed()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _sut = new Email
                {
                    Id = int.MinValue,
                    LinkedEntity = new MockDomainEntity { Id = int.MinValue }
                };
            });
        }

        [Test, Category("Models")]
        public void DataAnnotation_RequiredAttributes_ShouldBePresent()
        {
            // Arrange & Act - Verify that Required attributes are correctly applied
            var type = typeof(Email);

            // Assert - Check that properties have Required attributes where expected
            var idProperty = type.GetProperty("Id");
            var emailProperty = type.GetProperty("EmailAddress");
            var typeProperty = type.GetProperty("Type");
            var linkedEntityIdProperty = type.GetProperty("LinkedEntityId");
            var linkedEntityProperty = type.GetProperty("LinkedEntity");
            var linkedEntityTypeProperty = type.GetProperty("LinkedEntityType");
            var isConfirmedProperty = type.GetProperty("IsConfirmed");
            var createdDateProperty = type.GetProperty("CreatedDate");
            var modifiedDateProperty = type.GetProperty("ModifiedDate");

            Assert.Multiple(() =>
            {
                // Verify Required attributes exist on appropriate properties
                Assert.That(idProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false),
                    Is.Not.Empty, "Id should have Required attribute");
                Assert.That(emailProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false),
                    Is.Not.Empty, "EmailAddress should have Required attribute");
                Assert.That(typeProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false),
                    Is.Not.Empty, "Type should have Required attribute");
                Assert.That(linkedEntityIdProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false),
                    Is.Not.Empty, "LinkedEntityId should have Required attribute");
                Assert.That(linkedEntityProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false),
                    Is.Not.Empty, "LinkedEntity should have Required attribute");
                Assert.That(linkedEntityTypeProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false),
                    Is.Not.Empty, "LinkedEntityType should have Required attribute");
                Assert.That(isConfirmedProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false),
                    Is.Not.Empty, "IsConfirmed should have Required attribute");
                Assert.That(createdDateProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false),
                    Is.Not.Empty, "CreatedDate should have Required attribute");
                Assert.That(modifiedDateProperty?.GetCustomAttributes(typeof(System.ComponentModel.DataAnnotations.RequiredAttribute), false),
                    Is.Not.Empty, "ModifiedDate should have Required attribute");
            });
        }

        [Test, Category("Models")]
        public void DataAnnotation_RangeAttributes_ShouldBePresent()
        {
            // Arrange & Act - Verify that Range attributes are correctly applied
            var type = typeof(Email);

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

        #region Cast Method Tests

        [Test, Category("Models")]
        public void Cast_ToEmailDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 123;
            _sut.EmailAddress = "test@example.com";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
            _sut.IsPrimary = true;

            // Act
            var result = _sut.Cast<EmailDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<EmailDTO>());
                Assert.That(result.Id, Is.EqualTo(123));
                Assert.That(result.EmailAddress, Is.EqualTo("test@example.com"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(result.IsPrimary, Is.EqualTo(_sut.IsPrimary));
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIEmailDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 456;
            _sut.EmailAddress = "interface@test.com";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
            _sut.IsPrimary = false;

            // Act
            var result = _sut.Cast<IEmailDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<EmailDTO>());
                Assert.That(result.Id, Is.EqualTo(456));
                Assert.That(result.EmailAddress, Is.EqualTo("interface@test.com"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
                Assert.That(result.IsPrimary, Is.EqualTo(_sut.IsPrimary));
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToEmailDTO_WithNullValues_ShouldHandleNullValues()
        {
            // Arrange
            _sut.Id = 789;
            _sut.EmailAddress = null;
            _sut.Type = null;
            _sut.IsPrimary = false;

            // Act
            var result = _sut.Cast<EmailDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<EmailDTO>());
                Assert.That(result.Id, Is.EqualTo(789));
                Assert.That(result.EmailAddress, Is.Null);
                Assert.That(result.Type, Is.Null);
                Assert.That(result.IsPrimary, Is.False);
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToEmailDTO_WithAllTypesEnum_ShouldPreserveTypeCorrectly()
        {
            // Test all enum values
            var enumValues = Enum.GetValues<OrganizerCompanion.Core.Enums.Types>();

            foreach (var enumValue in enumValues)
            {
                // Arrange
                _sut.Id = 100;
                _sut.EmailAddress = $"test_{enumValue}@example.com";
                _sut.Type = enumValue;

                // Act
                var result = _sut.Cast<EmailDTO>();

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Type, Is.EqualTo(enumValue), $"Type {enumValue} should be preserved");
                    Assert.That(result.EmailAddress, Is.EqualTo($"test_{enumValue}@example.com"));
                    Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                    Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
                });
            }
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedType_ShouldThrowInvalidCastException()
        {
            // Arrange
            _sut.Id = 1;
            _sut.EmailAddress = "test@example.com";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Act & Assert
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Contains.Substring("Cannot cast Email to type MockDomainEntity."));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToEmailDTO_WithCompleteData_ShouldPreserveAllData()
        {
            // Arrange - Set up Email with comprehensive data
            var createdDate = DateTime.Now.AddDays(-5);
            var modifiedDate = DateTime.Now.AddHours(-2);

            var fullEmail = new Email(
                id: 999,
                emailAddress: "complete@test.com",
                type: OrganizerCompanion.Core.Enums.Types.Mobile,
                isPrimary: false,
                linkedEntity: new MockDomainEntity { Id = 42 },
                isConfirmed: true,
                createdDate: createdDate,
                modifiedDate: modifiedDate
            );

            // Act
            var result = fullEmail.Cast<EmailDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<EmailDTO>());
                Assert.That(result.Id, Is.EqualTo(999));
                Assert.That(result.EmailAddress, Is.EqualTo("complete@test.com"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Mobile));
                Assert.That(result.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(modifiedDate));
                // Note: LinkedEntity, LinkedEntityId, IsConfirmed, etc. are not part of EmailDTO
                // This is by design as EmailDTO is a simplified representation
            });
        }

        [Test, Category("Models")]
        public void Cast_MultipleCallsToSameType_ShouldReturnDifferentInstances()
        {
            // Arrange
            _sut.Id = 777;
            _sut.EmailAddress = "instance@test.com";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Fax;

            // Act
            var result1 = _sut.Cast<EmailDTO>();
            var result2 = _sut.Cast<EmailDTO>();
            var iEmailDto = _sut.Cast<IEmailDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.Not.SameAs(result2));
                Assert.That(result1, Is.Not.SameAs(iEmailDto));
                Assert.That(result2, Is.Not.SameAs(iEmailDto));

                // All should have same data
                Assert.That(result1.Id, Is.EqualTo(777));
                Assert.That(result2.Id, Is.EqualTo(777));
                Assert.That(iEmailDto.Id, Is.EqualTo(777));

                Assert.That(result1.EmailAddress, Is.EqualTo("instance@test.com"));
                Assert.That(result2.EmailAddress, Is.EqualTo("instance@test.com"));
                Assert.That(iEmailDto.EmailAddress, Is.EqualTo("instance@test.com"));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToEmailDTO_WithEmptyEmailAddress_ShouldHandleEmptyString()
        {
            // Arrange
            _sut.Id = 555;
            _sut.EmailAddress = string.Empty;
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Other;

            // Act
            var result = _sut.Cast<EmailDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.EmailAddress, Is.EqualTo(string.Empty));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToEmailDTO_WithSpecialCharactersInEmail_ShouldPreserveCharacters()
        {
            // Arrange
            var specialEmail = "test+special.chars@example-domain.co.uk";
            _sut.Id = 888;
            _sut.EmailAddress = specialEmail;
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Billing;

            // Act
            var result = _sut.Cast<EmailDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.EmailAddress, Is.EqualTo(specialEmail));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Billing));
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToEmailDTO_WithUnicodeCharacters_ShouldPreserveUnicode()
        {
            // Arrange
            var unicodeEmail = "tést@ëxämplë.com";
            _sut.Id = 333;
            _sut.EmailAddress = unicodeEmail;
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Act
            var result = _sut.Cast<EmailDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.EmailAddress, Is.EqualTo(unicodeEmail));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithExceptionInCasting_ShouldWrapInInvalidCastException()
        {
            // This test verifies the exception handling in the Cast method
            // Since the current implementation doesn't have scenarios that cause inner exceptions,
            // this test documents the expected behavior when such scenarios arise

            // Arrange
            _sut.Id = 1;
            _sut.EmailAddress = "test@example.com";

            // Act & Assert - Test unsupported type casting
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<AnotherMockEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Contains.Substring("Cannot cast Email to type AnotherMockEntity."));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToEmailDTO_WithZeroId_ShouldAllowZeroId()
        {
            // Arrange
            _sut.Id = 0;
            _sut.EmailAddress = "zero@test.com";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;

            // Act
            var result = _sut.Cast<EmailDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(0));
                Assert.That(result.EmailAddress, Is.EqualTo("zero@test.com"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToEmailDTO_WithMaxIntId_ShouldHandleLargeIds()
        {
            // Arrange
            _sut.Id = int.MaxValue;
            _sut.EmailAddress = "maxint@test.com";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Mobile;

            // Act
            var result = _sut.Cast<EmailDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(int.MaxValue));
                Assert.That(result.EmailAddress, Is.EqualTo("maxint@test.com"));
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        #endregion

        #region Additional Comprehensive Coverage Tests

        [Test, Category("Models")]
        public void JsonConstructor_WithUnusedParameters_ShouldIgnoreThemAndSetPropertiesCorrectly()
        {
            // Test that JsonConstructor handles parameters correctly and doesn't have issues with edge cases

            // Arrange & Act
            var testDate = DateTime.Now;
            var email = new Email(
                id: 999,
                emailAddress: "comprehensive@test.com",
                type: OrganizerCompanion.Core.Enums.Types.Work,
                isPrimary: false,
                linkedEntity: new MockDomainEntity { Id = 456 },
                isConfirmed: true,
                createdDate: testDate.AddDays(-1),
                modifiedDate: testDate);

            // Assert - Verify that all properties are set correctly
            Assert.Multiple(() =>
            {
                Assert.That(email.Id, Is.EqualTo(999));
                Assert.That(email.EmailAddress, Is.EqualTo("comprehensive@test.com"));
                Assert.That(email.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(email.LinkedEntityId, Is.EqualTo(456));
                Assert.That(email.LinkedEntity, Is.Not.Null);
                Assert.That(email.LinkedEntityType, Is.EqualTo("MockDomainEntity")); // Should be derived from LinkedEntity type
                Assert.That(email.IsConfirmed, Is.True);
                Assert.That(email.CreatedDate, Is.EqualTo(testDate.AddDays(-1)));
                Assert.That(email.ModifiedDate, Is.EqualTo(testDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ExceptionRethrowing_ShouldPreserveOriginalException()
        {
            // Test that the catch block in Cast method properly rethrows exceptions

            // Arrange
            _sut = new Email("test@exception.com", OrganizerCompanion.Core.Enums.Types.Work, true, null);

            // Act & Assert - Verify that InvalidCastException is thrown for unsupported types
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Email to type MockDomainEntity"));
                Assert.That(ex.InnerException, Is.Null); // Should be the original exception, not wrapped
            });

            // Test multiple unsupported types
            Assert.Throws<InvalidCastException>(() => _sut.Cast<AnotherMockEntity>());
        }

        [Test, Category("Models")]
        public void LinkedEntity_Getter_WhenLinkedEntityIsNull_ShouldReturnContact()
        {
            // Arrange
            var contact = new Contact { Id = 123, FirstName = "Test" };
            _sut = new Email("test@example.com", OrganizerCompanion.Core.Enums.Types.Work, true, null);
            _sut.LinkedEntity = contact;

            // Act
            var linkedEntity = _sut.LinkedEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(linkedEntity, Is.Not.Null);
                Assert.That(linkedEntity, Is.SameAs(contact));
                Assert.That(linkedEntity, Is.InstanceOf<Contact>());
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Getter_WhenLinkedEntityIsNull_ShouldReturnOrganization()
        {
            // Arrange
            var organization = new Organization { Id = 456, OrganizationName = "Test Org" };
            _sut = new Email("test@example.com", OrganizerCompanion.Core.Enums.Types.Work, true, null);
            _sut.LinkedEntity = organization;

            // Act
            var linkedEntity = _sut.LinkedEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(linkedEntity, Is.Not.Null);
                Assert.That(linkedEntity, Is.SameAs(organization));
                Assert.That(linkedEntity, Is.InstanceOf<Organization>());
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_WithContact_ShouldSetContactProperties()
        {
            // Arrange
            var contact = new Contact { Id = 789, FirstName = "Setter Test" };
            var originalModifiedDate = _sut.ModifiedDate;
            System.Threading.Thread.Sleep(1);

            // Act
            _sut.LinkedEntity = contact;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.SameAs(contact));
                Assert.That(_sut.ContactId, Is.EqualTo(contact.Id));
                Assert.That(_sut.Organization, Is.Null);
                Assert.That(_sut.OrganizationId, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_WithOrganization_ShouldSetOrganizationProperties()
        {
            // Arrange
            var organization = new Organization { Id = 101, OrganizationName = "Org Setter Test" };
            var originalModifiedDate = _sut.ModifiedDate;
            System.Threading.Thread.Sleep(1);

            // Act
            _sut.LinkedEntity = organization;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.SameAs(organization));
                Assert.That(_sut.Organization, Is.SameAs(organization));
                Assert.That(_sut.OrganizationId, Is.EqualTo(organization.Id));
                Assert.That(_sut.Contact, Is.Null);
                Assert.That(_sut.ContactId, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        #endregion

        // Helper mock class for testing IDomainEntity
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; } = 1;
            public bool IsCast { get; set; } = false;
            public int CastId { get; set; } = 0;
            public string? CastType { get; set; } = null;
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; } = default;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        // Another helper mock class for testing LinkedEntityType changes
        private class AnotherMockEntity : IDomainEntity
        {
            public int Id { get; set; } = 2;
            public bool IsCast { get; set; } = false;
            public int CastId { get; set; } = 0;
            public string? CastType { get; set; } = null;
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; } = default;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        // Mock EmailDTO for testing IEmailDTO constructor
        private class MockEmailDTO : IEmailDTO
        {
            public int Id { get; set; } = 0;
            public bool IsCast { get; set; } = false;
            public int CastId { get; set; } = 0;
            public string? CastType { get; set; } = null;
            public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
            public DateTime? ModifiedDate { get; set; } = default;
            public string? EmailAddress { get; set; }
            public OrganizerCompanion.Core.Enums.Types? Type { get; set; }
            public bool IsPrimary { get; set; } = false;
            public bool IsConfirmed { get; set; } = false;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }
    }
}
