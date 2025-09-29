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
            var type = Types.Home;
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
            var newType = Types.Work;
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
            _sut.Type = Types.Home;

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
                Type = Types.Billing,
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
    }
}
