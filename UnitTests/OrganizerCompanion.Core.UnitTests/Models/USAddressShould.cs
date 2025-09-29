using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Models.Type;

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
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new USAddress();
            var afterCreation = DateTime.Now;

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
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.DateModified, Is.EqualTo(default(DateTime)));
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
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-2);

            // Act
            var address = new USAddress(id, street1, street2, city, state, zipCode, country, type, dateCreated, dateModified);

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
                Assert.That(address.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(address.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void Id_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newId = 456;
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Id = newId;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(newId));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void Street1_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newStreet1 = "456 Oak Ave";
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Street1 = newStreet1;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street1, Is.EqualTo(newStreet1));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void Street2_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newStreet2 = "Suite 100";
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Street2 = newStreet2;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street2, Is.EqualTo(newStreet2));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void City_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newCity = "Springfield";
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.City = newCity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.City, Is.EqualTo(newCity));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void State_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newState = new USState { Name = "Texas", Abbreviation = "TX" };
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.State = newState;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.State, Is.EqualTo(newState));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void StateEnum_WhenSet_ShouldUpdateStateAndDateModified()
        {
            // Arrange
            var stateEnum = USStates.California;
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.StateEnum = stateEnum;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.State, Is.Not.Null);
                Assert.That(_sut.State!.Name, Is.EqualTo("California"));
                Assert.That(_sut.State!.Abbreviation, Is.EqualTo("CA"));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void StateEnum_WhenSetToNull_ShouldSetStateToNullAndUpdateDateModified()
        {
            // Arrange
            _sut.StateEnum = USStates.Florida; // Set initial value
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.StateEnum = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.State, Is.Null);
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
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
        public void ZipCode_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newZipCode = "12345-6789";
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.ZipCode = newZipCode;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.ZipCode, Is.EqualTo(newZipCode));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void Country_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newCountry = "Canada";
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Country = newCountry;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Country, Is.EqualTo(newCountry));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void Type_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newType = OrganizerCompanion.Core.Enums.Types.Work;
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Type = newType;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Type, Is.EqualTo(newType));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
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
                Assert.That(json, Does.Contain("\"type\":0")); // Assuming Types.Home is 0
                Assert.That(json, Does.Contain("\"state\"")); // State object should be serialized
                Assert.That(json, Does.Contain("\"dateCreated\""));
                Assert.That(json, Does.Contain("\"dateModified\""));
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
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Cast<USAddress>());
        }

        [Test, Category("Models")]
        public void IsCast_Getter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.IsCast; });
        }

        [Test, Category("Models")]
        public void IsCast_Setter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.IsCast = true);
        }

        [Test, Category("Models")]
        public void CastId_Getter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastId; });
        }

        [Test, Category("Models")]
        public void CastId_Setter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.CastId = 1);
        }

        [Test, Category("Models")]
        public void CastType_Getter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastType; });
        }

        [Test, Category("Models")]
        public void CastType_Setter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.CastType = "SomeType");
        }

        [Test, Category("Models")]
        public void DateCreated_ShouldBeReadOnly()
        {
            // Arrange
            var originalDateCreated = _sut.DateCreated;

            // Act & Assert - DateCreated should not have a public setter
            var propertyInfo = typeof(USAddress).GetProperty(nameof(USAddress.DateCreated));
            Assert.That(propertyInfo, Is.Not.Null);
            Assert.That(propertyInfo!.CanWrite, Is.False, "DateCreated should be read-only");
            Assert.That(_sut.DateCreated, Is.EqualTo(originalDateCreated));
        }

        [Test, Category("Models")]
        public void DateModified_CanBeSetDirectly()
        {
            // Arrange
            var newDateModified = DateTime.Now.AddDays(-5);

            // Act
            _sut.DateModified = newDateModified;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(newDateModified));
        }

        [Test, Category("Models")]
        public void Properties_WhenSetToSameValue_ShouldStillUpdateDateModified()
        {
            // Arrange
            var value = "Same Value";
            _sut.Street1 = value;
            var firstModifiedDate = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Street1 = value; // Set to same value

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street1, Is.EqualTo(value));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(firstModifiedDate));
                Assert.That(_sut.DateModified, Is.GreaterThan(firstModifiedDate));
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
                DateModified = DateTime.Now.AddMinutes(-30)
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
                Assert.That(json, Does.Contain("\"type\":4")); // Types.Billing enum value
                Assert.That(json, Does.Contain("\"state\"")); // State object should be serialized
                Assert.That(json, Does.Contain("\"dateCreated\""));
                Assert.That(json, Does.Contain("\"dateModified\""));
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

            _sut.DateModified = null;
            Assert.That(_sut.DateModified, Is.Null);
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
        public void Properties_WithMinIntValues_ShouldAcceptMinValues()
        {
            // Arrange & Act
            _sut.Id = int.MinValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MinValue));
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
            _sut.DateModified = null;

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
                
                Assert.That(root.TryGetProperty("dateModified", out var dateModifiedProperty), Is.True);
                Assert.That(dateModifiedProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
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
        public void AllPropertiesUpdate_ShouldUpdateDateModifiedIndependently()
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
                { "StateEnum", () => _sut.StateEnum = USStates.California }
            };

            // Act & Assert
            foreach (var property in properties)
            {
                var originalDateModified = _sut.DateModified;
                Thread.Sleep(10); // Ensure time difference

                property.Value.Invoke();

                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified), 
                    $"Property {property.Key} should update DateModified");
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)), 
                    $"Property {property.Key} should set DateModified to current time");
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
    }
}
