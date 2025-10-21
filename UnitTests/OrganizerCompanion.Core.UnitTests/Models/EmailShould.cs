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
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new Email();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.EmailAddress, Is.Null);
                Assert.That(_sut.Type, Is.Null);
                Assert.That(_sut.IsPrimary, Is.False);
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(0));
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
                Assert.That(_sut.IsConfirmed, Is.False);
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void Constructor_WithEmailAddressAndType_ShouldSetPropertiesAndDateCreated()
        {
            // Arrange
            var emailAddress = "test@example.com";
            var type = OrganizerCompanion.Core.Enums.Types.Work;
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new Email(emailAddress, type, true);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.EmailAddress, Is.EqualTo(emailAddress));
                Assert.That(_sut.Type, Is.EqualTo(type));
                Assert.That(_sut.IsPrimary, Is.True);
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(DateTime.Now));
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
            var linkedEntityId = 123;
            var linkedEntity = new MockDomainEntity();
            var linkedEntityType = "MockDomainEntity";
            var isConfirmed = true;
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now;

            // Act
            _sut = new Email(
                id, 
                emailAddress, 
                type,
                isPrimary,
                linkedEntityId,
                linkedEntity,
                linkedEntityType,
                isConfirmed,
                dateCreated, 
                dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(id));
                Assert.That(_sut.EmailAddress, Is.EqualTo(emailAddress));
                Assert.That(_sut.Type, Is.EqualTo(type));
                Assert.That(_sut.IsPrimary, Is.EqualTo(isPrimary));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(linkedEntityId));
                Assert.That(_sut.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo(linkedEntityType));
                Assert.That(_sut.IsConfirmed, Is.EqualTo(isConfirmed));
                Assert.That(_sut.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void Id_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            _sut = new Email();
            var beforeSet = DateTime.Now;

            // Act
            _sut.Id = 123;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(123));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void EmailAddress_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            _sut = new Email();
            var newEmailAddress = "newemail@example.com";
            var beforeSet = DateTime.Now;

            // Act
            _sut.EmailAddress = newEmailAddress;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.EmailAddress, Is.EqualTo(newEmailAddress));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void EmailAddress_WhenSetToNull_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut = new Email("test@example.com", OrganizerCompanion.Core.Enums.Types.Work, true)
            {
                EmailAddress = null
            };

            // Assert
            Assert.That(_sut.EmailAddress, Is.Null);
        }

        [Test, Category("Models")]
        public void Type_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            _sut = new Email();
            var newType = OrganizerCompanion.Core.Enums.Types.Fax;
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
        public void Type_WhenSetToNull_ShouldAcceptNullValue()
        {
      // Arrange
      _sut = new Email("test@example.com", OrganizerCompanion.Core.Enums.Types.Work, true)
      {
        // Act
        Type = null
      };

      // Assert
      Assert.That(_sut.Type, Is.Null);
        }

        [Test, Category("Models")]
        public void IsPrimary_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            _sut = new Email();
            var beforeSet = DateTime.Now;

            // Act
            _sut.IsPrimary = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsPrimary, Is.True);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void IsPrimary_ShouldGetAndSetCorrectly()
        {
            // Arrange & Act & Assert
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
        public void IsConfirmed_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            _sut = new Email();
            var beforeSet = DateTime.Now;

            // Act
            _sut.IsConfirmed = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsConfirmed, Is.True);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void IsConfirmed_WhenSetToFalse_ShouldUpdateDateModified()
        {
      // Arrange
      _sut = new Email
      {
        IsConfirmed = true // Set to true first
      };
      var beforeSet = DateTime.Now;

            // Act
            _sut.IsConfirmed = false;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsConfirmed, Is.False);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
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
            _sut = new Email("test@example.com", OrganizerCompanion.Core.Enums.Types.Work, true);

            // Assert
            Assert.That(_sut.IsConfirmed, Is.False);
        }

        [Test, Category("Models")]
        public void IsConfirmed_ToggleMultipleTimes_ShouldUpdateDateModifiedEachTime()
    {
      // Arrange
      _sut = new Email();
            var initialTime = DateTime.Now;

            // Act & Assert - Test sequential IsConfirmed changes update DateModified
            System.Threading.Thread.Sleep(1);
            _sut.IsConfirmed = true;
            var firstModified = _sut.DateModified;
      Assert.Multiple(() =>
      {
        Assert.That(firstModified, Is.GreaterThanOrEqualTo(initialTime));
        Assert.That(_sut.IsConfirmed, Is.True);
      });
      System.Threading.Thread.Sleep(1);
            _sut.IsConfirmed = false;
            var secondModified = _sut.DateModified;
      Assert.Multiple(() =>
      {
        Assert.That(secondModified, Is.GreaterThan(firstModified));
        Assert.That(_sut.IsConfirmed, Is.False);
      });
      System.Threading.Thread.Sleep(1);
            _sut.IsConfirmed = true;
            var thirdModified = _sut.DateModified;
      Assert.Multiple(() =>
      {
        Assert.That(thirdModified, Is.GreaterThan(secondModified));
        Assert.That(_sut.IsConfirmed, Is.True);
      });
    }

    [Test, Category("Models")]
        public void DateCreated_IsReadOnly_AndSetDuringConstruction()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new Email();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsDateCreatedFromParameter()
        {
            // Arrange
            var specificDate = DateTime.Now.AddDays(-5);

            // Act
            _sut = new Email(
                1, 
                "test@example.com", 
                OrganizerCompanion.Core.Enums.Types.Work,
                false,
                0,
                null,
                null,
                false,
                specificDate, 
                DateTime.Now);

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void DateModified_CanBeSetAndRetrieved()
        {
            // Arrange
            _sut = new Email();
            var dateModified = DateTime.Now.AddHours(-2);

            // Act
            _sut.DateModified = dateModified;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(dateModified));
        }

        [Test, Category("Models")]
        public void LinkedEntity_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var mockEntity = new MockDomainEntity();
            var originalDateModified = _sut.DateModified;
            System.Threading.Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.LinkedEntity = mockEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(mockEntity));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
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
                0,
                null,
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
                Assert.That(json, Does.Contain("\"linkedEntityId\":0"));
                Assert.That(json, Does.Contain("\"linkedEntity\":null"));
                Assert.That(json, Does.Contain("\"linkedEntityType\":null"));
                Assert.That(json, Does.Contain("\"dateCreated\":"));
                Assert.That(json, Does.Contain("\"dateModified\":"));

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
            // Arrange & Act & Assert for true
            _sut.IsPrimary = true;
            var jsonTrue = _sut.ToJson();
            var documentTrue = JsonDocument.Parse(jsonTrue);
            Assert.That(documentTrue.RootElement.TryGetProperty("isPrimary", out var isPrimaryTrueProperty), Is.True);
            Assert.That(isPrimaryTrueProperty.GetBoolean(), Is.True);

            // Arrange & Act & Assert for false
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
                0,
                null,
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
                0,
                null,
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
                OrganizerCompanion.Core.Enums.Types.Mobil,
                true,
                0,
                null,
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
                Assert.That(json, Does.Contain("\"dateCreated\":"));
                Assert.That(json, Does.Contain("\"dateModified\":"));

                // Verify JSON is well-formed
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void JsonSerialization_WithAllTypesEnum_ShouldProduceValidJson()
        {
            // Test all enum values
            var enumValues = new[] { OrganizerCompanion.Core.Enums.Types.Home, OrganizerCompanion.Core.Enums.Types.Work, OrganizerCompanion.Core.Enums.Types.Mobil, OrganizerCompanion.Core.Enums.Types.Fax, OrganizerCompanion.Core.Enums.Types.Billing, OrganizerCompanion.Core.Enums.Types.Other };

            foreach (var enumValue in enumValues)
            {
                // Arrange
                _sut = new Email($"test{enumValue}@example.com", enumValue, true)
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
                    Assert.That(json, Does.Contain("\"dateCreated\":"), $"Failed for {enumValue}");

                    // Verify JSON is well-formed
                    Assert.DoesNotThrow(() => JsonDocument.Parse(json), $"Failed for {enumValue}");
                });
            }
        }

        [Test, Category("Models")]
        public void Email_WithEmptyEmailAddress_ShouldBeAllowed()
        {
            // Arrange & Act
            _sut = new Email(string.Empty, OrganizerCompanion.Core.Enums.Types.Other, true);

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(string.Empty));
        }

        [Test, Category("Models")]
        public void Email_WithVeryLongEmailAddress_ShouldBeAllowed()
        {
            // Arrange
            var longEmail = new string('a', 1000) + "@example.com";

            // Act
            _sut = new Email(longEmail, OrganizerCompanion.Core.Enums.Types.Work, true);

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(longEmail));
        }

        [Test, Category("Models")]
        public void Email_MultiplePropertyChanges_ShouldUpdateDateModifiedEachTime()
        {
            // Arrange
            _sut = new Email();
            var initialTime = DateTime.Now;

            // Act & Assert
            System.Threading.Thread.Sleep(1); // Ensure time difference
            _sut.Id = 1;
            var firstModified = _sut.DateModified;
            Assert.That(firstModified, Is.GreaterThanOrEqualTo(initialTime));

            System.Threading.Thread.Sleep(1);
            _sut.EmailAddress = "test@example.com";
            var secondModified = _sut.DateModified;
            Assert.That(secondModified, Is.GreaterThan(firstModified));

            System.Threading.Thread.Sleep(1);
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
            var thirdModified = _sut.DateModified;
            Assert.That(thirdModified, Is.GreaterThan(secondModified));

            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntityId = 123;
            var fourthModified = _sut.DateModified;
            Assert.That(fourthModified, Is.GreaterThan(thirdModified));

            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntity = new MockDomainEntity();
            var fifthModified = _sut.DateModified;
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
                0,
                null,
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
                linkedEntityId: 0,
                linkedEntity: null,
                linkedEntityType: null,
                isConfirmed: false,
                dateCreated: DateTime.Now.AddDays(-1),
                dateModified: null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(email.Id, Is.EqualTo(0));
                Assert.That(email.EmailAddress, Is.Null);
                Assert.That(email.Type, Is.Null);
                Assert.That(email.LinkedEntityId, Is.EqualTo(0));
                Assert.That(email.LinkedEntity, Is.Null);
                Assert.That(email.LinkedEntityType, Is.Null);
                Assert.That(email.DateModified, Is.Null);
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
            _sut = new Email(specialEmail, OrganizerCompanion.Core.Enums.Types.Work, true);

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(specialEmail));
        }

        [Test, Category("Models")]
        public void Email_WithUnicodeCharactersInEmailAddress_ShouldBeAllowed()
        {
            // Arrange
            var unicodeEmail = "tést@ëxämplë.com";

            // Act
            _sut = new Email(unicodeEmail, OrganizerCompanion.Core.Enums.Types.Work, true);

            // Assert
            Assert.That(_sut.EmailAddress, Is.EqualTo(unicodeEmail));
        }

        [Test, Category("Models")]
        public void Email_WithNegativeId_ShouldBeAllowed()
        {
            // Arrange & Act
            _sut = new Email { Id = -1 };

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(-1));
        }

        [Test, Category("Models")]
        public void Email_WithNegativeLinkedEntityId_ShouldBeAllowed()
        {
            // Arrange & Act
            _sut = new Email { LinkedEntityId = -1 };

            // Assert
            Assert.That(_sut.LinkedEntityId, Is.EqualTo(-1));
        }

        [Test, Category("Models")]
        public void Email_WithMaxIntLinkedEntityId_ShouldBeAllowed()
        {
            // Arrange & Act
            _sut = new Email { LinkedEntityId = int.MaxValue };

            // Assert
            Assert.That(_sut.LinkedEntityId, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void DateModified_WhenSetToNull_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.DateModified = null;

            // Assert
            Assert.That(_sut.DateModified, Is.Null);
        }

        [Test, Category("Models")]
        public void DateModified_WhenSetToSpecificValue_ShouldRetainValue()
        {
            // Arrange
            var specificDate = DateTime.Now.AddDays(-3);

            // Act
            _sut.DateModified = specificDate;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(specificDate));
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
                456,
                mockEntity,
                "MockDomainEntity",
                false,
                DateTime.Now.AddDays(-1),
                DateTime.Now);

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"linkedEntityId\":456"));
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
            // Arrange & Act - Testing that the JsonConstructor sets _linkedEntityType directly
            var email = new Email(
                id: 1,
                emailAddress: "test@example.com",
                type: OrganizerCompanion.Core.Enums.Types.Work,
                isPrimary: false,
                linkedEntityId: 123,
                linkedEntity: new MockDomainEntity(),
                linkedEntityType: "CustomEntityType", // Different from actual type name
                isConfirmed: true,
                dateCreated: DateTime.Now.AddDays(-1),
                dateModified: DateTime.Now);

            // Assert - LinkedEntityType should be what was passed in, not derived from LinkedEntity
            Assert.That(email.LinkedEntityType, Is.EqualTo("CustomEntityType"));
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
        public void DateCreated_FromDefaultConstructor_ShouldBeSetToCurrentTime()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var email = new Email();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(email.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(email.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                
                // DateCreated should be read-only
                var property = typeof(Email).GetProperty("DateCreated");
                Assert.That(property?.SetMethod, Is.Null, "DateCreated should be read-only");
            });
        }

        [Test, Category("Models")]
        public void AllPropertySetters_ShouldUpdateDateModified()
        {
            // Arrange
            _sut = new Email();
            var originalDateModified = _sut.DateModified;

            // Act & Assert - Test each property that should update DateModified
            System.Threading.Thread.Sleep(1);
            _sut.Id = 100;
            Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified), "Id setter should update DateModified");

            var idModified = _sut.DateModified;
            System.Threading.Thread.Sleep(1);
            _sut.EmailAddress = "new@test.com";
            Assert.That(_sut.DateModified, Is.GreaterThan(idModified), "EmailAddress setter should update DateModified");

            var emailModified = _sut.DateModified;
            System.Threading.Thread.Sleep(1);
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Fax;
            Assert.That(_sut.DateModified, Is.GreaterThan(emailModified), "Type setter should update DateModified");

            var typeModified = _sut.DateModified;
            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntityId = 999;
            Assert.That(_sut.DateModified, Is.GreaterThan(typeModified), "LinkedEntityId setter should update DateModified");

            var linkedIdModified = _sut.DateModified;
            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntity = new MockDomainEntity();
            Assert.That(_sut.DateModified, Is.GreaterThan(linkedIdModified), "LinkedEntity setter should update DateModified");

            var linkedEntityModified = _sut.DateModified;
            System.Threading.Thread.Sleep(1);
            _sut.IsConfirmed = true;
            Assert.That(_sut.DateModified, Is.GreaterThan(linkedEntityModified), "IsConfirmed setter should update DateModified");
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
            var dateCreatedProperty = type.GetProperty("DateCreated");
            var dateModifiedProperty = type.GetProperty("DateModified");

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
                Assert.That(dateCreatedProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false), 
                    Is.Not.Empty, "DateCreated should have JsonPropertyName attribute");
                Assert.That(dateModifiedProperty?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false), 
                    Is.Not.Empty, "DateModified should have JsonPropertyName attribute");
            });
        }

        [Test, Category("Models")]
        public void Constructor_SimpleEmailAndType_ShouldNotSetLinkedProperties()
        {
            // Arrange & Act
            var email = new Email("simple@test.com", OrganizerCompanion.Core.Enums.Types.Home, true);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(email.EmailAddress, Is.EqualTo("simple@test.com"));
                Assert.That(email.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
                Assert.That(email.Id, Is.EqualTo(0));
                Assert.That(email.LinkedEntityId, Is.EqualTo(0));
                Assert.That(email.LinkedEntity, Is.Null);
                Assert.That(email.LinkedEntityType, Is.Null);
                Assert.That(email.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void Email_WithMinIntValues_ShouldBeAllowed()
        {
            // Arrange & Act
            _sut = new Email
            {
                Id = int.MinValue,
                LinkedEntityId = int.MinValue
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(int.MinValue));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(int.MinValue));
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
            var dateCreatedProperty = type.GetProperty("DateCreated");
            var dateModifiedProperty = type.GetProperty("DateModified");

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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToEmailDTO_WithAllTypesEnum_ShouldPreserveTypeCorrectly()
        {
            // Test each enum value
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
                    Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                    Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
            var dateCreated = DateTime.Now.AddDays(-5);
            var dateModified = DateTime.Now.AddHours(-2);

            var fullEmail = new Email(
                id: 999,
                emailAddress: "complete@test.com",
                type: OrganizerCompanion.Core.Enums.Types.Mobil,
                isPrimary: false,
                linkedEntityId: 42,
                linkedEntity: new MockDomainEntity { Id = 42 },
                linkedEntityType: "MockDomainEntity",
                isConfirmed: true,
                dateCreated: dateCreated,
                dateModified: dateModified
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
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Mobil));
                Assert.That(result.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(result.DateModified, Is.EqualTo(dateModified));
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

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.Not.Null);
                Assert.That(result2, Is.Not.Null);
                Assert.That(result1, Is.Not.SameAs(result2), "Each cast should return a new instance");
                Assert.That(result1.Id, Is.EqualTo(result2.Id));
                Assert.That(result1.EmailAddress, Is.EqualTo(result2.EmailAddress));
                Assert.That(result1.Type, Is.EqualTo(result2.Type));
                Assert.That(result1.DateCreated, Is.EqualTo(result2.DateCreated));
                Assert.That(result1.DateModified, Is.EqualTo(result2.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToEmailDTO_WithMaxIntId_ShouldHandleLargeIds()
        {
            // Arrange
            _sut.Id = int.MaxValue;
            _sut.EmailAddress = "maxint@test.com";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Mobil;

            // Act
            var result = _sut.Cast<EmailDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(int.MaxValue));
                Assert.That(result.EmailAddress, Is.EqualTo("maxint@test.com"));
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToEmailDTO_WithNegativeId_ShouldAllowNegativeIds()
        {
            // Arrange
            _sut.Id = -100;
            _sut.EmailAddress = "negative@test.com";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Act
            var result = _sut.Cast<EmailDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(-100));
                Assert.That(result.EmailAddress, Is.EqualTo("negative@test.com"));
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                linkedEntityId: 456,
                linkedEntity: new MockDomainEntity(),
                linkedEntityType: "CustomType", // This parameter should be respected in JsonConstructor
                isConfirmed: true,
                dateCreated: testDate.AddDays(-1),
                dateModified: testDate
            );

            // Assert - Verify that all properties are set correctly
            Assert.Multiple(() =>
            {
                Assert.That(email.Id, Is.EqualTo(999));
                Assert.That(email.EmailAddress, Is.EqualTo("comprehensive@test.com"));
                Assert.That(email.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(email.LinkedEntityId, Is.EqualTo(456));
                Assert.That(email.LinkedEntity, Is.Not.Null);
                Assert.That(email.LinkedEntityType, Is.EqualTo("CustomType")); // Should be from parameter, not derived
                Assert.That(email.IsConfirmed, Is.True);
                Assert.That(email.DateCreated, Is.EqualTo(testDate.AddDays(-1)));
                Assert.That(email.DateModified, Is.EqualTo(testDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ExceptionHandling_RethrowsCorrectly()
        {
            // This test verifies that the catch block in the Cast method properly rethrows exceptions
            
            // Arrange
            _sut.Id = 1;
            _sut.EmailAddress = "test@example.com";

            // Act & Assert - Test that InvalidCastException is thrown and rethrown correctly
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Email to type MockDomainEntity"));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_WithComplexEntityTypeChange_ShouldUpdateLinkedEntityTypeCorrectly()
        {
            // Test comprehensive LinkedEntity behavior with type changes
            
            // Arrange
            var entity1 = new MockDomainEntity();
            var entity2 = new AnotherMockEntity();
            var originalDateModified = _sut.DateModified;

            // Act & Assert - Test multiple entity changes
            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntity = entity1;
            var firstModified = _sut.DateModified;
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(entity1));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(firstModified, Is.GreaterThan(originalDateModified));
            });

            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntity = entity2;
            var secondModified = _sut.DateModified;
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(entity2));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("AnotherMockEntity"));
                Assert.That(secondModified, Is.GreaterThan(firstModified));
            });

            System.Threading.Thread.Sleep(1);
            _sut.LinkedEntity = null;
            var thirdModified = _sut.DateModified;
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
                Assert.That(thirdModified, Is.GreaterThan(secondModified));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithExtremeValues_ShouldHandleAllScenarios()
        {
            // Test ToString with various extreme value combinations
            
            // Case 1: Maximum values
            _sut.Id = int.MaxValue;
            _sut.EmailAddress = new string('a', 1000) + "@verylongdomain.com";
            var maxResult = _sut.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(maxResult, Is.Not.Null);
                Assert.That(maxResult, Does.Contain(int.MaxValue.ToString()));
                Assert.That(maxResult, Does.Contain("Id:" + int.MaxValue));
                Assert.That(maxResult, Does.Contain(_sut.EmailAddress));
            });

            // Case 2: Minimum/Zero values
            _sut.Id = 0;
            _sut.EmailAddress = "";
            var minResult = _sut.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(minResult, Is.Not.Null);
                Assert.That(minResult, Does.Contain("Id:0"));
                Assert.That(minResult, Does.Contain("EmailAddress"));
            });

            // Case 3: Null email address
            _sut.Id = 42;
            _sut.EmailAddress = null;
            var nullResult = _sut.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(nullResult, Is.Not.Null);
                Assert.That(nullResult, Does.Contain("Id:42"));
                Assert.That(nullResult, Does.Contain("EmailAddress"));
            });
        }

        [Test, Category("Models")]
        public void SerializerOptions_CyclicalReferenceHandling_ComprehensiveTest()
        {
            // Test that the serialization options handle complex scenarios correctly
            
            // Arrange - Create email with LinkedEntity that could cause cycles
            var mockEntity = new MockDomainEntity { Id = 123 };
            _sut = new Email(
                id: 1,
                emailAddress: "cycle@test.com",
                type: OrganizerCompanion.Core.Enums.Types.Work,
                isPrimary: true,
                linkedEntityId: 123,
                linkedEntity: mockEntity,
                linkedEntityType: "MockDomainEntity",
                isConfirmed: true,
                dateCreated: DateTime.Now.AddHours(-1),
                dateModified: DateTime.Now
            );

            // Act & Assert - Multiple serialization calls should work consistently
            string json1, json2, json3;
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => json1 = _sut.ToJson());
                Assert.DoesNotThrow(() => json2 = _sut.ToJson());
                Assert.DoesNotThrow(() => json3 = _sut.ToJson());
            });

            // All serializations should produce valid JSON
            json1 = _sut.ToJson();
            json2 = _sut.ToJson();
            json3 = _sut.ToJson();
            
            Assert.Multiple(() =>
            {
                Assert.That(json1, Is.Not.Null.And.Not.Empty);
                Assert.That(json2, Is.Not.Null.And.Not.Empty);
                Assert.That(json3, Is.Not.Null.And.Not.Empty);
                Assert.That(json1, Is.EqualTo(json2));
                Assert.That(json2, Is.EqualTo(json3));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToMultipleDTOTypes_ShouldCreateIndependentInstances()
        {
            // Test that multiple Cast calls create independent DTO instances
            
            // Arrange
            _sut = new Email(
                id: 100,
                emailAddress: "multi@cast.com",
                type: OrganizerCompanion.Core.Enums.Types.Mobil,
                isPrimary: false,
                linkedEntityId: 0,
                linkedEntity: null,
                linkedEntityType: null,
                isConfirmed: false,
                dateCreated: DateTime.Now.AddDays(-1),
                dateModified: DateTime.Now
            );

            // Act - Cast to different supported types
            var emailDto1 = _sut.Cast<EmailDTO>();
            var emailDto2 = _sut.Cast<EmailDTO>();
            var iEmailDto = _sut.Cast<IEmailDTO>();

            // Assert - All should be different instances but with same data
            Assert.Multiple(() =>
            {
                Assert.That(emailDto1, Is.Not.SameAs(emailDto2));
                Assert.That(emailDto1, Is.Not.SameAs(iEmailDto));
                Assert.That(emailDto2, Is.Not.SameAs(iEmailDto));
                
                // All should have same data
                Assert.That(emailDto1.Id, Is.EqualTo(100));
                Assert.That(emailDto2.Id, Is.EqualTo(100));
                Assert.That(iEmailDto.Id, Is.EqualTo(100));
                
                Assert.That(emailDto1.EmailAddress, Is.EqualTo("multi@cast.com"));
                Assert.That(emailDto2.EmailAddress, Is.EqualTo("multi@cast.com"));
                Assert.That(iEmailDto.EmailAddress, Is.EqualTo("multi@cast.com"));
            });
        }

        [Test, Category("Models")]
        public void AllEnumTypes_ComprehensiveCastAndSerializationTest()
        {
            // Test all enum values with Cast and serialization operations
            
            var enumValues = Enum.GetValues<OrganizerCompanion.Core.Enums.Types>();
            
            foreach (var enumValue in enumValues)
            {
                // Arrange
                _sut = new Email(
                    id: (int)enumValue,
                    emailAddress: $"test{enumValue}@example.com",
                    type: enumValue,
                    isPrimary: false,
                    linkedEntityId: 0,
                    linkedEntity: null,
                    linkedEntityType: null,
                    isConfirmed: (int)enumValue % 2 == 0,
                    dateCreated: DateTime.Now.AddDays(-(int)enumValue),
                    dateModified: DateTime.Now
                );

                // Act & Assert - Cast should work for all enum types
                Assert.DoesNotThrow(() =>
                {
                    var dto = _sut.Cast<EmailDTO>();
                    Assert.That(dto.Type, Is.EqualTo(enumValue));
                });

                // Act & Assert - Serialization should work for all enum types
                Assert.DoesNotThrow(() =>
                {
                    var json = _sut.ToJson();
                    Assert.That(json, Does.Contain(enumValue.ToString()));
                });
            }
        }

        [Test, Category("Models")]
        public void DateModified_PropertyChangeCombinations_ShouldUpdateInSequence()
        {
            // Test that rapid sequential property changes all update DateModified correctly
            
            // Arrange
            _sut = new Email();
            var timestamps = new List<DateTime?>
            {
              _sut.DateModified // Initial state
            };

            // Act & Assert - Test sequential property changes
            System.Threading.Thread.Sleep(2);
            _sut.Id = 1;
            timestamps.Add(_sut.DateModified);

            System.Threading.Thread.Sleep(2);
            _sut.EmailAddress = "sequence@test.com";
            timestamps.Add(_sut.DateModified);

            System.Threading.Thread.Sleep(2);
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
            timestamps.Add(_sut.DateModified);

            System.Threading.Thread.Sleep(2);
            _sut.LinkedEntityId = 999;
            timestamps.Add(_sut.DateModified);

            System.Threading.Thread.Sleep(2);
            _sut.LinkedEntity = new MockDomainEntity();
            timestamps.Add(_sut.DateModified);

            System.Threading.Thread.Sleep(2);
            _sut.IsConfirmed = true;
            timestamps.Add(_sut.DateModified);

            // Assert - Each timestamp should be greater than the previous
            for (int i = 1; i < timestamps.Count; i++)
            {
                Assert.That(timestamps[i], Is.GreaterThan(timestamps[i - 1]), 
                    $"Timestamp at index {i} should be greater than timestamp at index {i - 1}");
            }
        }

        [Test, Category("Models")]
        public void Email_ComprehensiveFunctionalityIntegrationTest()
        {
            // Comprehensive test that exercises all major functionality together
            
            // Test default constructor
            var defaultEmail = new Email();
            Assert.That(defaultEmail, Is.Not.Null);
            Assert.That(defaultEmail.DateCreated, Is.Not.EqualTo(default(DateTime)));
            
            // Test simple constructor
            var simpleEmail = new Email("simple@test.com", OrganizerCompanion.Core.Enums.Types.Work, true);
            Assert.Multiple(() =>
            {
                Assert.That(simpleEmail.EmailAddress, Is.EqualTo("simple@test.com"));
                Assert.That(simpleEmail.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(simpleEmail.IsConfirmed, Is.False);
            });
            
            // Test JsonConstructor with comprehensive data
            var testDate = DateTime.Now;
            var linkedEntity = new MockDomainEntity { Id = 456 };
            var comprehensiveEmail = new Email(
                id: 12345,
                emailAddress: "comprehensive@test.com",
                type: OrganizerCompanion.Core.Enums.Types.Mobil,
                isPrimary: false,
                linkedEntityId: 456,
                linkedEntity: linkedEntity,
                linkedEntityType: "MockDomainEntity",
                isConfirmed: true,
                dateCreated: testDate.AddDays(-1),
                dateModified: testDate
            );
            
            // Verify comprehensive properties
            Assert.Multiple(() =>
            {
                Assert.That(comprehensiveEmail.Id, Is.EqualTo(12345));
                Assert.That(comprehensiveEmail.EmailAddress, Is.EqualTo("comprehensive@test.com"));
                Assert.That(comprehensiveEmail.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Mobil));
                Assert.That(comprehensiveEmail.LinkedEntityId, Is.EqualTo(456));
                Assert.That(comprehensiveEmail.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(comprehensiveEmail.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(comprehensiveEmail.IsConfirmed, Is.True);
                Assert.That(comprehensiveEmail.DateCreated, Is.EqualTo(testDate.AddDays(-1)));
                Assert.That(comprehensiveEmail.DateModified, Is.EqualTo(testDate));
            });
            
            // Test all property setters
            defaultEmail.Id = 54321;
            defaultEmail.EmailAddress = "updated@test.com";
            defaultEmail.Type = OrganizerCompanion.Core.Enums.Types.Fax;
            defaultEmail.LinkedEntityId = 789;
            defaultEmail.LinkedEntity = new AnotherMockEntity();
            defaultEmail.IsConfirmed = true;
            
            Assert.Multiple(() =>
            {
                Assert.That(defaultEmail.Id, Is.EqualTo(54321));
                Assert.That(defaultEmail.EmailAddress, Is.EqualTo("updated@test.com"));
                Assert.That(defaultEmail.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Fax));
                Assert.That(defaultEmail.LinkedEntityId, Is.EqualTo(789));
                Assert.That(defaultEmail.LinkedEntityType, Is.EqualTo("AnotherMockEntity"));
                Assert.That(defaultEmail.IsConfirmed, Is.True);
            });
            
            // Test Cast functionality
            var emailDto = defaultEmail.Cast<EmailDTO>();
            var iEmailDto = defaultEmail.Cast<IEmailDTO>();
            
            Assert.Multiple(() =>
            {
                Assert.That(emailDto, Is.InstanceOf<EmailDTO>());
                Assert.That(iEmailDto, Is.InstanceOf<EmailDTO>());
                Assert.That(emailDto.Id, Is.EqualTo(defaultEmail.Id));
                Assert.That(iEmailDto.Id, Is.EqualTo(defaultEmail.Id));
            });
            
            // Test JSON serialization
            var json = defaultEmail.ToJson();
            Assert.That(json, Is.Not.Null.And.Not.Empty);
            
            // Test ToString functionality
            var stringResult = defaultEmail.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(stringResult, Is.Not.Null.And.Not.Empty);
                Assert.That(stringResult, Does.Contain("updated@test.com"));
                Assert.That(stringResult, Does.Contain("54321"));
            });
            
            // Test exception scenarios
            Assert.Throws<InvalidCastException>(() => defaultEmail.Cast<MockDomainEntity>());
        }

        [Test, Category("Models")]
        public void LinkedEntityType_ReadOnlyProperty_ComprehensiveBehaviorTest()
        {
            // Comprehensive test of LinkedEntityType read-only behavior
            
            // Verify property is read-only
            var property = typeof(Email).GetProperty("LinkedEntityType");
            Assert.That(property?.SetMethod, Is.Null, "LinkedEntityType should be read-only");
            
            // Test behavior with null LinkedEntity
            _sut.LinkedEntity = null;
            Assert.That(_sut.LinkedEntityType, Is.Null);
            
            // Test behavior with various entity types
            var mockEntity = new MockDomainEntity();
            _sut.LinkedEntity = mockEntity;
            Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
            
            var anotherEntity = new AnotherMockEntity();
            _sut.LinkedEntity = anotherEntity;
            Assert.That(_sut.LinkedEntityType, Is.EqualTo("AnotherMockEntity"));
            
            // Test that setting back to null clears the type
            _sut.LinkedEntity = null;
            Assert.That(_sut.LinkedEntityType, Is.Null);
        }

        [Test, Category("Models")]
        public void Cast_ExceptionRethrowing_ShouldPreserveOriginalException()
        {
            // Test that the catch block in Cast method properly rethrows exceptions
            
            // Arrange
            _sut = new Email("test@exception.com", OrganizerCompanion.Core.Enums.Types.Work, true);
            
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

        #endregion

        // Helper mock class for testing IDomainEntity
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; } = 1;
            public bool IsCast { get; set; } = false;
            public int CastId { get; set; } = 0;
            public string? CastType { get; set; } = null;
            public DateTime DateCreated { get; } = DateTime.Now;
            public DateTime? DateModified { get; set; } = DateTime.Now;

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
            public DateTime DateCreated { get; } = DateTime.Now;
            public DateTime? DateModified { get; set; } = DateTime.Now;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }
    }
}
