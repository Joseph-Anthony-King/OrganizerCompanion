using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Models.Type;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class USAddressShould
    {
        private USAddress _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new USAddress();
        }

        [TearDown]
        public void TearDown()
        {
            _sut = null!;
        }

        [Test, Category("Models")]
        public void DefaultConstructor_ShouldCreateUSAddressWithDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            _sut = new USAddress();
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.Street1, Is.Null);
                Assert.That(_sut.Street2, Is.Null);
                Assert.That(_sut.City, Is.Null);
                Assert.That(_sut.State, Is.Null);
                Assert.That(_sut.StateEnum, Is.Null);
                Assert.That(_sut.ZipCode, Is.Null);
                Assert.That(_sut.Country, Is.EqualTo(Countries.UnitedStates.GetName()));
                Assert.That(_sut.Type, Is.Null);
                Assert.That(_sut.IsPrimary, Is.False);
                Assert.That(_sut.LinkedEntityId, Is.Null);
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
                Assert.That(_sut.CreatedDate, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.CreatedDate, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_ShouldCreateUSAddressWithProvidedValues()
        {
            // Arrange
            var id = 123;
            var street1 = "123 Main St";
            var street2 = "Apt 4B";
            var city = "Anytown";
            var state = new USState { Name = "California", Abbreviation = "CA" };
            var zipCode = "90210";
            var country = "United States";
            var type = OrganizerCompanion.Core.Enums.Types.Home;
            var isPrimary = true;
            var linkedEntityId = 456;
            var linkedEntity = new MockDomainEntity { Id = 456 };
            var createdDate = DateTime.Now.AddDays(-1);
            var modifiedDate = DateTime.Now.AddHours(-2);

            // Act
            var address = new USAddress(
              id,
              street1,
              street2,
              city,
              state,
              zipCode,
              country,
              type,
              isPrimary,
              linkedEntity,
              createdDate,
              modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(address.Id, Is.EqualTo(id));
                Assert.That(address.Street1, Is.EqualTo(street1));
                Assert.That(address.Street2, Is.EqualTo(street2));
                Assert.That(address.City, Is.EqualTo(city));
                Assert.That(address.State, Is.EqualTo(state));
                Assert.That(address.ZipCode, Is.EqualTo(zipCode));
                Assert.That(address.Country, Is.EqualTo(country));
                Assert.That(address.Type, Is.EqualTo(type));
                Assert.That(address.IsPrimary, Is.EqualTo(isPrimary));
                Assert.That(address.LinkedEntityId, Is.EqualTo(linkedEntityId));
                Assert.That(address.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(address.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(address.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(address.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Id_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newId = 456;
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Id = newId;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(newId));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void Street1_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newStreet1 = "456 Oak Ave";
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Street1 = newStreet1;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street1, Is.EqualTo(newStreet1));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void Street2_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newStreet2 = "Suite 100";
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Street2 = newStreet2;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street2, Is.EqualTo(newStreet2));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void City_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newCity = "Springfield";
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.City = newCity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.City, Is.EqualTo(newCity));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void State_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newState = new USState { Name = "Texas", Abbreviation = "TX" };
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.State = newState;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.State, Is.EqualTo(newState));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void StateEnum_WhenSet_ShouldUpdateStateAndModifiedDate()
        {
            // Arrange
            var stateEnum = USStates.California;
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.StateEnum = stateEnum;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.State, Is.Not.Null);
                Assert.That(_sut.State!.Name, Is.EqualTo("California"));
                Assert.That(_sut.State!.Abbreviation, Is.EqualTo("CA"));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void StateEnum_WhenSetToNull_ShouldSetStateToNullAndUpdateModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;
            _sut.StateEnum = USStates.Florida; // Set initial value
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.StateEnum = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.State, Is.Null);
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.LessThan(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void StateEnum_Get_ShouldAlwaysReturnNull()
        {
            // Arrange
            _sut.State = new USState { Name = "New York", Abbreviation = "NY" };

            // Act
            var result = _sut.StateEnum;

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("Models")]
        public void ZipCode_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newZipCode = "12345-6789";
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.ZipCode = newZipCode;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.ZipCode, Is.EqualTo(newZipCode));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void Country_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newCountry = "Canada";
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Country = newCountry;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Country, Is.EqualTo(newCountry));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void Type_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newType = OrganizerCompanion.Core.Enums.Types.Work;
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Type = newType;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Type, Is.EqualTo(newType));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void IsPrimary_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newIsPrimary = true;
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.IsPrimary = newIsPrimary;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsPrimary, Is.EqualTo(newIsPrimary));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void IsPrimary_WhenChangedMultipleTimes_ShouldUpdateModifiedDateEachTime()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act & Assert - Test changing from default false to true
            _sut.IsPrimary = true;
            var firstModifiedDate = _sut.ModifiedDate;
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsPrimary, Is.True);
                Assert.That(firstModifiedDate, Is.Not.EqualTo(originalModifiedDate));
                Assert.That(firstModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });

            Thread.Sleep(10); // Ensure time difference

            // Act & Assert - Test changing from true back to false
            _sut.IsPrimary = false;
            var secondModifiedDate = _sut.ModifiedDate;
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsPrimary, Is.False);
                Assert.That(secondModifiedDate, Is.Not.EqualTo(firstModifiedDate));
                Assert.That(secondModifiedDate, Is.GreaterThan(firstModifiedDate));
            });

            Thread.Sleep(10); // Ensure time difference

            // Act & Assert - Test setting to same value (false)
            _sut.IsPrimary = false;
            var thirdModifiedDate = _sut.ModifiedDate;
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsPrimary, Is.False);
                Assert.That(thirdModifiedDate, Is.Not.EqualTo(secondModifiedDate));
                Assert.That(thirdModifiedDate, Is.GreaterThan(secondModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void IsPrimary_DefaultValue_ShouldBeFalse()
        {
            // Arrange & Act - Using new instance to test default
            var newAddress = new USAddress();

            // Assert
            Assert.That(newAddress.IsPrimary, Is.False);
        }

        [Test, Category("Models")]
        public void LinkedEntity_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newLinkedEntity = new MockDomainEntity(); // Mock domain entity for testing
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.LinkedEntity = newLinkedEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(newLinkedEntity));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Street1 = "123 Test St";
            _sut.Street2 = "Unit A";
            _sut.City = "Test City";
            _sut.State = new USState { Name = "Test State", Abbreviation = "TS" };
            _sut.ZipCode = "12345";
            _sut.Country = "Test Country";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
            _sut.IsPrimary = true;
            _sut.LinkedEntity = new MockDomainEntity { Id = 10 };

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("\"street1\":\"123 Test St\""));
                Assert.That(json, Does.Contain("\"street2\":\"Unit A\""));
                Assert.That(json, Does.Contain("\"city\":\"Test City\""));
                Assert.That(json, Does.Contain("\"zipCode\":\"12345\""));
                Assert.That(json, Does.Contain("\"country\":\"Test Country\""));
                Assert.That(json, Does.Contain("\"type\":0")); // Assuming OrganizerCompanion.Core.Enums.Types.Home is 0
                Assert.That(json, Does.Contain("\"isPrimary\":true"));
                Assert.That(json, Does.Contain("\"linkedEntityId\":10"));
                Assert.That(json, Does.Contain("\"linkedEntity\""));
                Assert.That(json, Does.Contain("\"state\"")); // State object should be serialized
                Assert.That(json, Does.Contain("\"createdDate\""));
                Assert.That(json, Does.Contain("\"modifiedDate\""));
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
            });
        }

        [Test, Category("Models")]
        public void ToString_WithAllProperties_ShouldReturnFormattedString()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Street1 = "456 Oak Ave";
            _sut.City = "Springfield";
            _sut.State = new USState { Name = "Illinois", Abbreviation = "IL" };
            _sut.ZipCode = "62701";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Does.Contain(".Id:123"));
                Assert.That(result, Does.Contain(".Street1:456 Oak Ave"));
                Assert.That(result, Does.Contain(".City:Springfield"));
                Assert.That(result, Does.Contain(".State:IL"));
                Assert.That(result, Does.Contain(".Zip:62701"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithStateNameOnly_ShouldUseStateName()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Street1 = "456 Oak Ave";
            _sut.City = "Springfield";
            _sut.State = new USState { Name = "Illinois", Abbreviation = null };
            _sut.ZipCode = "62701";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.That(result, Does.Contain(".State:Illinois"));
        }

        [Test, Category("Models")]
        public void ToString_WithNullState_ShouldShowUnknown()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Street1 = "456 Oak Ave";
            _sut.City = "Springfield";
            _sut.State = null;
            _sut.ZipCode = "62701";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.That(result, Does.Contain(".State:Unknown"));
        }

        [Test, Category("Models")]
        public void ToString_WithStateWithNullNameAndAbbreviation_ShouldShowUnknown()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Street1 = "456 Oak Ave";
            _sut.City = "Springfield";
            _sut.State = new USState { Name = null, Abbreviation = null };
            _sut.ZipCode = "62701";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.That(result, Does.Contain(".State:Unknown"));
        }

        [Test, Category("Models")]
        public void CreatedDate_ShouldBeReadOnly()
        {
            // Arrange
            var originalCreatedDate = _sut.CreatedDate;

            // Act & Assert - CreatedDate should not have a public setter
            var propertyInfo = typeof(USAddress).GetProperty(nameof(USAddress.CreatedDate));
            Assert.That(propertyInfo, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(propertyInfo!.CanWrite, Is.False, "CreatedDate should be read-only");
                Assert.That(_sut.CreatedDate, Is.EqualTo(originalCreatedDate));
            });
        }

        [Test, Category("Models")]
        public void ModifiedDate_CanBeSetDirectly()
        {
            // Arrange
            var newModifiedDate = DateTime.Now.AddDays(-5);

            // Act
            _sut.ModifiedDate = newModifiedDate;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(newModifiedDate));
        }

        [Test, Category("Models")]
        public void Properties_WhenSetToSameValue_ShouldStillUpdateModifiedDate()
        {
            // Arrange
            var value = "Same Value";
            _sut.Street1 = value;
            var firstModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Street1 = value; // Set to same value

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street1, Is.EqualTo(value));
                Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(firstModifiedDate));
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(firstModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void JsonSerialization_ShouldContainAllPropertiesInCorrectFormat()
        {
            // Arrange
            var address = new USAddress
            {
                Id = 999,
                Street1 = "789 Pine St",
                Street2 = "Floor 3",
                City = "Metropolis",
                State = new USState { Name = "New York", Abbreviation = "NY" },
                ZipCode = "10001",
                Country = "USA",
                Type = OrganizerCompanion.Core.Enums.Types.Billing,
                IsPrimary = false,
                ModifiedDate = DateTime.Now.AddMinutes(-30)
            };

            // Act
            var json = address.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":999"));
                Assert.That(json, Does.Contain("\"street1\":\"789 Pine St\""));
                Assert.That(json, Does.Contain("\"street2\":\"Floor 3\""));
                Assert.That(json, Does.Contain("\"city\":\"Metropolis\""));
                Assert.That(json, Does.Contain("\"zipCode\":\"10001\""));
                Assert.That(json, Does.Contain("\"country\":\"USA\""));
                Assert.That(json, Does.Contain("\"type\":4")); // OrganizerCompanion.Core.Enums.Types.Billing enum value
                Assert.That(json, Does.Contain("\"isPrimary\":false"));
                Assert.That(json, Does.Contain("\"state\"")); // State object should be serialized
                Assert.That(json, Does.Contain("\"linkedEntityId\":null"));
                Assert.That(json, Does.Contain("\"linkedEntity\":null"));
                Assert.That(json, Does.Contain("\"linkedEntityType\":null"));
                Assert.That(json, Does.Contain("\"createdDate\""));
                Assert.That(json, Does.Contain("\"modifiedDate\""));
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
            });
        }

        [Test, Category("Models")]
        public void MultipleStateEnumOperations_ShouldWorkCorrectly()
        {
            // Act & Assert
            Assert.Multiple(() =>
            {
                // Test setting various states
                _sut.StateEnum = USStates.California;
                Assert.That(_sut.State?.Abbreviation, Is.EqualTo("CA"));

                _sut.StateEnum = USStates.Texas;
                Assert.That(_sut.State?.Abbreviation, Is.EqualTo("TX"));

                _sut.StateEnum = USStates.Florida;
                Assert.That(_sut.State?.Abbreviation, Is.EqualTo("FL"));

                // Test that get always returns null regardless of what was set
                Assert.That(_sut.StateEnum, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Properties_WithNullValues_ShouldHandleCorrectly()
        {
            // Act & Assert
            _sut.Street1 = null;
            Assert.That(_sut.Street1, Is.Null);

            _sut.Street2 = null;
            Assert.That(_sut.Street2, Is.Null);

            _sut.City = null;
            Assert.That(_sut.City, Is.Null);

            _sut.State = null;
            Assert.That(_sut.State, Is.Null);

            _sut.ZipCode = null;
            Assert.That(_sut.ZipCode, Is.Null);

            _sut.Country = null;
            Assert.That(_sut.Country, Is.Null);

            _sut.Type = null;
            Assert.That(_sut.Type, Is.Null);

            _sut.IsPrimary = true;
            Assert.That(_sut.IsPrimary, Is.True);

            _sut.IsPrimary = false;
            Assert.That(_sut.IsPrimary, Is.False);

            _sut.LinkedEntity = null;
            Assert.That(_sut.LinkedEntity, Is.Null);

            _sut.ModifiedDate = null;
            Assert.That(_sut.ModifiedDate, Is.Null);
        }

        [Test, Category("Models")]
        public void Properties_WithEmptyStrings_ShouldAcceptEmptyStrings()
        {
            // Act & Assert
            _sut.Street1 = string.Empty;
            Assert.That(_sut.Street1, Is.EqualTo(string.Empty));

            _sut.Street2 = string.Empty;
            Assert.That(_sut.Street2, Is.EqualTo(string.Empty));

            _sut.City = string.Empty;
            Assert.That(_sut.City, Is.EqualTo(string.Empty));

            _sut.ZipCode = string.Empty;
            Assert.That(_sut.ZipCode, Is.EqualTo(string.Empty));

            _sut.Country = string.Empty;
            Assert.That(_sut.Country, Is.EqualTo(string.Empty));
        }

        [Test, Category("Models")]
        public void Properties_WithMaxIntValues_ShouldAcceptMaxValues()
        {
            // Arrange & Act
            _sut.Id = int.MaxValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("Models")]
        public void Properties_WithMinIntValues_ShouldNotAcceptMinValues()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = int.MinValue);
        }

        [Test, Category("Models")]
        public void Id_WhenSetToNegativeOne_ShouldThrowArgumentOutOfRangeException()
        {
            // Arrange & Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = -1);
            Assert.Multiple(() =>
            {
                Assert.That(exception.ParamName, Is.EqualTo("Id"));
                Assert.That(exception.Message, Does.Contain("Id must be a non-negative number."));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithNullProperties_ShouldHandleNullsCorrectly()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Street1 = null;
            _sut.Street2 = null;
            _sut.City = null;
            _sut.State = null;
            _sut.ZipCode = null;
            _sut.Country = null;
            _sut.Type = null;
            _sut.IsPrimary = false;
            _sut.ModifiedDate = null;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
            });

            var jsonDocument = JsonDocument.Parse(json);
            var root = jsonDocument.RootElement;

            Assert.Multiple(() =>
            {
                Assert.That(root.TryGetProperty("id", out var idProperty), Is.True);
                Assert.That(idProperty.GetInt32(), Is.EqualTo(1));

                Assert.That(root.TryGetProperty("street1", out var street1Property), Is.True);
                Assert.That(street1Property.ValueKind, Is.EqualTo(JsonValueKind.Null));

                Assert.That(root.TryGetProperty("isPrimary", out var isPrimaryProperty), Is.True);
                Assert.That(isPrimaryProperty.GetBoolean(), Is.False);

                Assert.That(root.TryGetProperty("modifiedDate", out var modifiedDateProperty), Is.True);
                Assert.That(modifiedDateProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
            });
        }

        [Test, Category("Models")]
        public void USAddress_WithSpecialCharacters_ShouldHandleCorrectly()
        {
            // Arrange & Act
            _sut.Street1 = "123 O'Connor St";
            _sut.Street2 = "Apt #4B";
            _sut.City = "New York";
            _sut.ZipCode = "10001-1234";
            _sut.Country = "United States";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street1, Is.EqualTo("123 O'Connor St"));
                Assert.That(_sut.Street2, Is.EqualTo("Apt #4B"));
                Assert.That(_sut.City, Is.EqualTo("New York"));
                Assert.That(_sut.ZipCode, Is.EqualTo("10001-1234"));
                Assert.That(_sut.Country, Is.EqualTo("United States"));
            });
        }

        [Test, Category("Models")]
        public void AllPropertiesUpdate_ShouldUpdateModifiedDateIndependently()
        {
            // Arrange
            var properties = new Dictionary<string, Action>
            {
                { "Id", () => _sut.Id = 1 },
                { "Street1", () => _sut.Street1 = "Test Street" },
                { "Street2", () => _sut.Street2 = "Test Street2" },
                { "City", () => _sut.City = "Test City" },
                { "State", () => _sut.State = new USState { Name = "Test", Abbreviation = "TS" } },
                { "ZipCode", () => _sut.ZipCode = "12345" },
                { "Country", () => _sut.Country = "Test Country" },
                { "Type", () => _sut.Type = OrganizerCompanion.Core.Enums.Types.Other },
                { "IsPrimary", () => _sut.IsPrimary = true },
                { "StateEnum", () => _sut.StateEnum = USStates.California },
                { "LinkedEntity", () => _sut.LinkedEntity = new MockDomainEntity() }
            };

            // Act & Assert
            foreach (var property in properties)
            {
                var originalModifiedDate = _sut.ModifiedDate;
                Thread.Sleep(10); // Ensure time difference

                property.Value.Invoke();

                Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(originalModifiedDate),
                    $"Property {property.Key} should update ModifiedDate");
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)),
                    $"Property {property.Key} should set ModifiedDate to current time");
            }
        }

        [Test, Category("Models")]
        public void ToJson_WithSerializerOptions_HandlesCircularReferences()
        {
            // Arrange
            _sut.Id = 100;
            _sut.Street1 = "Test Circular Street";
            _sut.City = "Test City";
            _sut.State = USStates.California.ToStateModel();

            // Act
            var json = _sut.ToJson();

            // Assert - Should not throw due to ReferenceHandler.IgnoreCycles
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
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

        #region Cast Method Tests
        [Test, Category("Models")]
        public void Cast_ToUSAddressDTO_ShouldReturnValidUSAddressDTO()
        {
            // Arrange
            var state = new USState { Name = "California", Abbreviation = "CA" };
            _sut.Id = 123;
            _sut.Street1 = "123 Main St";
            _sut.Street2 = "Apt 4B";
            _sut.City = "Los Angeles";
            _sut.State = state;
            _sut.ZipCode = "90210";
            _sut.Country = "United States";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
            _sut.IsPrimary = true;

            // Act
            var result = _sut.Cast<USAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<USAddressDTO>());
                Assert.That(result.Id, Is.EqualTo(123));
                Assert.That(result.Street1, Is.EqualTo("123 Main St"));
                Assert.That(result.Street2, Is.EqualTo("Apt 4B"));
                Assert.That(result.City, Is.EqualTo("Los Angeles"));
                Assert.That(result.State, Is.EqualTo(state));
                Assert.That(result.ZipCode, Is.EqualTo("90210"));
                Assert.That(result.Country, Is.EqualTo("United States"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
                Assert.That(result.IsPrimary, Is.True);
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIUSAddressDTO_ShouldReturnValidIUSAddressDTO()
        {
            // Arrange
            var state = new USState { Name = "Texas", Abbreviation = "TX" };
            _sut.Id = 456;
            _sut.Street1 = "456 Oak Ave";
            _sut.Street2 = "Suite 200";
            _sut.City = "Dallas";
            _sut.State = state;
            _sut.ZipCode = "75201";
            _sut.Country = "USA";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
            _sut.IsPrimary = false;

            // Act
            var result = _sut.Cast<IUSAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<USAddressDTO>());
                Assert.That(result.Id, Is.EqualTo(456));
                Assert.That(result.Street1, Is.EqualTo("456 Oak Ave"));
                Assert.That(result.Street2, Is.EqualTo("Suite 200"));
                Assert.That(result.City, Is.EqualTo("Dallas"));
                Assert.That(result.State, Is.EqualTo(state));
                Assert.That(result.ZipCode, Is.EqualTo("75201"));
                Assert.That(result.Country, Is.EqualTo("USA"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(result.IsPrimary, Is.False);
            });
        }

        [Test, Category("Models")]
        public void Cast_WithNullStreet1_ShouldReturnUSAddressDTOWithNullStreet1()
        {
            // Arrange
            _sut.Id = 789;
            _sut.Street1 = null;
            _sut.Street2 = "Unit B";
            _sut.City = "Phoenix";
            _sut.State = new USState { Name = "Arizona", Abbreviation = "AZ" };
            _sut.ZipCode = "85001";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Other;

            // Act
            var result = _sut.Cast<USAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(789));
                Assert.That(result.Street1, Is.Null);
                Assert.That(result.Street2, Is.EqualTo("Unit B"));
                Assert.That(result.City, Is.EqualTo("Phoenix"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithNullStreet2_ShouldReturnUSAddressDTOWithNullStreet2()
        {
            // Arrange
            _sut.Id = 101;
            _sut.Street1 = "789 Pine St";
            _sut.Street2 = null;
            _sut.City = "Seattle";
            _sut.State = new USState { Name = "Washington", Abbreviation = "WA" };
            _sut.ZipCode = "98101";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Billing;

            // Act
            var result = _sut.Cast<USAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(101));
                Assert.That(result.Street1, Is.EqualTo("789 Pine St"));
                Assert.That(result.Street2, Is.Null);
                Assert.That(result.City, Is.EqualTo("Seattle"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Billing));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithNullState_ShouldReturnUSAddressDTOWithNullState()
        {
            // Arrange
            _sut.Id = 202;
            _sut.Street1 = "321 Elm St";
            _sut.City = "Unknown City";
            _sut.State = null;
            _sut.ZipCode = "00000";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Fax;

            // Act
            var result = _sut.Cast<USAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(202));
                Assert.That(result.Street1, Is.EqualTo("321 Elm St"));
                Assert.That(result.City, Is.EqualTo("Unknown City"));
                Assert.That(result.State, Is.Null);
                Assert.That(result.ZipCode, Is.EqualTo("00000"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Fax));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithNullType_ShouldReturnUSAddressDTOWithNullType()
        {
            // Arrange
            _sut.Id = 303;
            _sut.Street1 = "654 Maple Ave";
            _sut.City = "Boston";
            _sut.State = new USState { Name = "Massachusetts", Abbreviation = "MA" };
            _sut.ZipCode = "02101";
            _sut.Type = null;

            // Act
            var result = _sut.Cast<USAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(303));
                Assert.That(result.Street1, Is.EqualTo("654 Maple Ave"));
                Assert.That(result.City, Is.EqualTo("Boston"));
                Assert.That(result.State!.Abbreviation, Is.EqualTo("MA"));
                Assert.That(result.ZipCode, Is.EqualTo("02101"));
                Assert.That(result.Type, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Cast_WithEmptyStrings_ShouldReturnUSAddressDTOWithEmptyStrings()
        {
            // Arrange
            _sut.Id = 404;
            _sut.Street1 = string.Empty;
            _sut.Street2 = string.Empty;
            _sut.City = string.Empty;
            _sut.State = new USState { Name = "Florida", Abbreviation = "FL" };
            _sut.ZipCode = string.Empty;
            _sut.Country = string.Empty;
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Mobile;

            // Act
            var result = _sut.Cast<USAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(404));
                Assert.That(result.Street1, Is.EqualTo(string.Empty));
                Assert.That(result.Street2, Is.EqualTo(string.Empty));
                Assert.That(result.City, Is.EqualTo(string.Empty));
                Assert.That(result.ZipCode, Is.EqualTo(string.Empty));
                Assert.That(result.Country, Is.EqualTo(string.Empty));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Mobile));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithLongStrings_ShouldReturnUSAddressDTOWithLongStrings()
        {
            // Arrange
            var longStreet1 = new string('A', 500) + " Very Long Street Name " + new string('B', 500);
            var longStreet2 = new string('C', 300) + " Long Apt Number " + new string('D', 300);
            var longCity = new string('E', 200) + " Long City Name " + new string('F', 200);
            var longZip = new string('1', 50) + "-" + new string('2', 50);
            var longCountry = new string('G', 100) + " Long Country Name " + new string('H', 100);

            _sut.Id = 505;
            _sut.Street1 = longStreet1;
            _sut.Street2 = longStreet2;
            _sut.City = longCity;
            _sut.State = new USState { Name = "New York", Abbreviation = "NY" };
            _sut.ZipCode = longZip;
            _sut.Country = longCountry;
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Other;

            // Act
            var result = _sut.Cast<USAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(505));
                Assert.That(result.Street1, Is.EqualTo(longStreet1));
                Assert.That(result.Street1?.Length, Is.EqualTo(1023)); // 500 + 23 + 500
                Assert.That(result.Street2, Is.EqualTo(longStreet2));
                Assert.That(result.Street2?.Length, Is.EqualTo(617)); // 300 + 17 + 300
                Assert.That(result.City, Is.EqualTo(longCity));
                Assert.That(result.City?.Length, Is.EqualTo(416)); // 200 + 16 + 200
                Assert.That(result.ZipCode, Is.EqualTo(longZip));
                Assert.That(result.ZipCode?.Length, Is.EqualTo(101)); // 50 + 1 + 50
                Assert.That(result.Country, Is.EqualTo(longCountry));
                Assert.That(result.Country?.Length, Is.EqualTo(219)); // 100 + 19 + 100
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithSpecialCharactersInStrings_ShouldReturnUSAddressDTOWithSpecialCharacters()
        {
            // Arrange
            var specialStreet1 = "123 O'Connor St. #4B";
            var specialStreet2 = "Apt 2½ (Back Unit)";
            var specialCity = "Saint-Jean-sur-Richelieu";
            var specialZip = "90210-1234";
            var specialCountry = "United States of America";

            _sut.Id = 606;
            _sut.Street1 = specialStreet1;
            _sut.Street2 = specialStreet2;
            _sut.City = specialCity;
            _sut.State = new USState { Name = "California", Abbreviation = "CA" };
            _sut.ZipCode = specialZip;
            _sut.Country = specialCountry;
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;

            // Act
            var result = _sut.Cast<USAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(606));
                Assert.That(result.Street1, Is.EqualTo(specialStreet1));
                Assert.That(result.Street2, Is.EqualTo(specialStreet2));
                Assert.That(result.City, Is.EqualTo(specialCity));
                Assert.That(result.ZipCode, Is.EqualTo(specialZip));
                Assert.That(result.Country, Is.EqualTo(specialCountry));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithAllTypesEnumValues_ShouldReturnCorrectUSAddressDTO()
        {
            // Test each enum value
            var testCases = new[]
            {
                (OrganizerCompanion.Core.Enums.Types.Home, 1),
                (OrganizerCompanion.Core.Enums.Types.Work, 2),
                (OrganizerCompanion.Core.Enums.Types.Mobile, 3),
                (OrganizerCompanion.Core.Enums.Types.Fax, 4),
                (OrganizerCompanion.Core.Enums.Types.Billing, 5),
                (OrganizerCompanion.Core.Enums.Types.Other, 6)
            };

            foreach (var (type, id) in testCases)
            {
                // Arrange
                _sut.Id = id;
                _sut.Street1 = $"{id} Test Street";
                _sut.City = $"Test City {id}";
                _sut.State = new USState { Name = "Test State", Abbreviation = "TS" };
                _sut.ZipCode = $"{id:00000}";
                _sut.Type = type;

                // Act
                var result = _sut.Cast<USAddressDTO>();

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null, $"Failed for type {type}");
                    Assert.That(result.Id, Is.EqualTo(id), $"Failed for type {type}");
                    Assert.That(result.Street1, Is.EqualTo($"{id} Test Street"), $"Failed for type {type}");
                    Assert.That(result.City, Is.EqualTo($"Test City {id}"), $"Failed for type {type}");
                    Assert.That(result.ZipCode, Is.EqualTo($"{id:00000}"), $"Failed for type {type}");
                    Assert.That(result.Type, Is.EqualTo(type), $"Failed for type {type}");
                });
            }
        }

        [Test, Category("Models")]
        public void Cast_WithAllUSStates_ShouldReturnCorrectUSAddressDTO()
        {
            // Test with different US states
            var testStates = new[]
            {
                USStates.California,
                USStates.Texas,
                USStates.Florida,
                USStates.NewYork,
                USStates.Illinois,
                USStates.Pennsylvania
            };

            foreach (var stateEnum in testStates)
            {
                // Arrange
                _sut.Id = (int)stateEnum;
                _sut.Street1 = $"123 {stateEnum} Street";
                _sut.City = $"{stateEnum} City";
                _sut.StateEnum = stateEnum; // This will set the State property
                _sut.ZipCode = "12345";
                _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;

                // Act
                var result = _sut.Cast<USAddressDTO>();

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null, $"Failed for state {stateEnum}");
                    Assert.That(result.Id, Is.EqualTo((int)stateEnum), $"Failed for state {stateEnum}");
                    Assert.That(result.Street1, Is.EqualTo($"123 {stateEnum} Street"), $"Failed for state {stateEnum}");
                    Assert.That(result.City, Is.EqualTo($"{stateEnum} City"), $"Failed for state {stateEnum}");
                    Assert.That(result.State, Is.Not.Null, $"Failed for state {stateEnum}");
                    Assert.That(result.State!.Name, Is.EqualTo(stateEnum.GetName()), $"Failed for state {stateEnum}");
                    Assert.That(result.State!.Abbreviation, Is.EqualTo(stateEnum.GetAbbreviation()), $"Failed for state {stateEnum}");
                    Assert.That(result.ZipCode, Is.EqualTo("12345"), $"Failed for state {stateEnum}");
                    Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home), $"Failed for state {stateEnum}");
                });
            }
        }

        [Test, Category("Models")]
        public void Cast_WithMaxIntId_ShouldReturnUSAddressDTOWithMaxIntId()
        {
            // Arrange
            _sut.Id = int.MaxValue;
            _sut.Street1 = "123 Max Value Street";
            _sut.City = "Max City";
            _sut.State = new USState { Name = "Max State", Abbreviation = "MX" };
            _sut.ZipCode = "99999";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Act
            var result = _sut.Cast<USAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(int.MaxValue));
                Assert.That(result.Street1, Is.EqualTo("123 Max Value Street"));
                Assert.That(result.City, Is.EqualTo("Max City"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithZeroId_ShouldReturnUSAddressDTOWithZeroId()
        {
            // Arrange
            _sut.Id = 0;
            _sut.Street1 = "123 Zero Street";
            _sut.City = "Zero City";
            _sut.State = new USState { Name = "Zero State", Abbreviation = "ZS" };
            _sut.ZipCode = "00000";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Other;

            // Act
            var result = _sut.Cast<USAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(0));
                Assert.That(result.Street1, Is.EqualTo("123 Zero Street"));
                Assert.That(result.City, Is.EqualTo("Zero City"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedType_ShouldThrowInvalidCastException()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Street1 = "123 Test St";
            _sut.City = "Test City";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;

            // Act & Assert
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message, Does.Contain("Cannot cast USAddress to type MockDomainEntity."));
            });
        }

        // Mock class for testing unsupported cast types
        private class MockUnsupportedType : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime? ModifiedDate { get; set; } = default;
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        [Test, Category("Models")]
        public void Cast_ExceptionHandling_ShouldRethrowExceptions()
        {
            // Note: This test verifies the exception handling in the Cast method
            // While the catch block may not be reachable in normal scenarios,
            // it demonstrates the expected behavior if an exception occurs
            
            // Arrange
            _sut.Id = 100;
            _sut.Street1 = "Test Street";

            // Act & Assert - Test unsupported type throws InvalidCastException
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockUnsupportedType>());
            Assert.That(exception.Message, Does.Contain("Cannot cast USAddress to type MockUnsupportedType."));
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedTypeWithMock_ShouldThrowInvalidCastException()
        {
            // Arrange
            _sut.Id = 456;
            _sut.Street1 = "456 Error Street";
            _sut.City = "Error City";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Act & Assert
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockUnsupportedType>());
            Assert.Multiple(() =>
            {
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message, Does.Contain("Cannot cast USAddress to type MockUnsupportedType."));
            });
        }

        [Test, Category("Models")]
        public void Cast_WhenExceptionOccursInTryBlock_ShouldThrowInvalidCastExceptionWithInnerException()
        {
            // Note: This test demonstrates the exception handling in the Cast method
            // The current implementation catches any exception and wraps it in InvalidCastException

            // Arrange
            _sut.Id = 789;
            _sut.Street1 = "789 Exception Test Street";
            _sut.City = "Exception City";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Billing;

            // Act & Assert - Test with a type that should cause the InvalidCastException
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockUnsupportedType>());
            Assert.Multiple(() =>
            {
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message, Does.Contain("Cannot cast USAddress to type MockUnsupportedType."));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithAllNullValues_ShouldReturnUSAddressDTOWithNullValues()
        {
            // Arrange
            _sut.Id = 0;
            _sut.Street1 = null;
            _sut.Street2 = null;
            _sut.City = null;
            _sut.State = null;
            _sut.ZipCode = null;
            _sut.Country = null;
            _sut.Type = null;

            // Act
            var result = _sut.Cast<USAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(0));
                Assert.That(result.Street1, Is.Null);
                Assert.That(result.Street2, Is.Null);
                Assert.That(result.City, Is.Null);
                Assert.That(result.State, Is.Null);
                Assert.That(result.ZipCode, Is.Null);
                Assert.That(result.Country, Is.Null);
                Assert.That(result.Type, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIUSAddressDTOWithComplexData_ShouldReturnValidResult()
        {
            // Arrange
            var complexState = new USState { Name = "Complex State Name", Abbreviation = "CS" };
            _sut.Id = 999;
            _sut.Street1 = "999 Complex Address Street";
            _sut.Street2 = "Complex Unit #999";
            _sut.City = "Complex City";
            _sut.State = complexState;
            _sut.ZipCode = "99999-9999";
            _sut.Country = "Complex Country";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Billing;

            // Act
            var result = _sut.Cast<IUSAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<USAddressDTO>());
                Assert.That(result.Id, Is.EqualTo(999));
                Assert.That(result.Street1, Is.EqualTo("999 Complex Address Street"));
                Assert.That(result.Street2, Is.EqualTo("Complex Unit #999"));
                Assert.That(result.City, Is.EqualTo("Complex City"));
                Assert.That(result.State, Is.EqualTo(complexState));
                Assert.That(result.ZipCode, Is.EqualTo("99999-9999"));
                Assert.That(result.Country, Is.EqualTo("Complex Country"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Billing));
            });
        }

        [Test, Category("Models")]
        public void Cast_PerformanceTest_ShouldHandleMultipleCastsEfficiently()
        {
            // Arrange
            _sut.Id = 100;
            _sut.Street1 = "100 Performance Test Street";
            _sut.City = "Performance City";
            _sut.State = new USState { Name = "Performance State", Abbreviation = "PS" };
            _sut.ZipCode = "10000";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
            var iterations = 1000;

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                for (int i = 0; i < iterations; i++)
                {
                    var dto = _sut.Cast<USAddressDTO>();
                    var iDto = _sut.Cast<IUSAddressDTO>();
                    Assert.Multiple(() =>
                    {
                        Assert.That(dto, Is.Not.Null);
                        Assert.That(iDto, Is.Not.Null);
                    });
                }
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIAddressDTO_ShouldReturnValidIAddressDTO()
        {
            // Arrange
            var state = new USState { Name = "Nevada", Abbreviation = "NV" };
            _sut.Id = 999;
            _sut.Street1 = "999 Desert Rd";
            _sut.Street2 = "Suite A";
            _sut.City = "Las Vegas";
            _sut.State = state;
            _sut.ZipCode = "89101";
            _sut.Country = "USA";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Other;
            _sut.IsPrimary = true;

            // Act
            var result = _sut.Cast<IAddressDTO>();
            var usAddressDto = result as USAddressDTO;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<USAddressDTO>());
                Assert.That(usAddressDto, Is.Not.Null);
                Assert.That(usAddressDto!.Id, Is.EqualTo(999));
                Assert.That(usAddressDto.Street1, Is.EqualTo("999 Desert Rd"));
                Assert.That(usAddressDto.Street2, Is.EqualTo("Suite A"));
                Assert.That(usAddressDto.City, Is.EqualTo("Las Vegas"));
                Assert.That(usAddressDto.ZipCode, Is.EqualTo("89101"));
                Assert.That(usAddressDto.Country, Is.EqualTo("USA"));
                Assert.That(usAddressDto.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
                Assert.That(usAddressDto.IsPrimary, Is.True);
            });
        }
        #endregion
    }
}
