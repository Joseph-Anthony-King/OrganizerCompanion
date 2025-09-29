using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Models.Type;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class MXAddressShould
    {
        private MXAddress _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new MXAddress();
        }

        [TearDown]
        public void TearDown()
        {
            _sut = null!;
        }

        [Test, Category("Models")]
        public void DefaultConstructor_ShouldCreateMXAddressWithDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new MXAddress();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.Street, Is.Null);
                Assert.That(_sut.Neighborhood, Is.Null);
                Assert.That(_sut.PostalCode, Is.Null);
                Assert.That(_sut.City, Is.Null);
                Assert.That(_sut.State, Is.Null);
                Assert.That(_sut.Country, Is.EqualTo(Countries.Mexico.GetName()));
                Assert.That(_sut.Type, Is.Null);
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(0));
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_ShouldCreateMXAddressWithProvidedValues()
        {
            // Arrange
            var id = 123;
            var street = "Calle Principal 123";
            var neighborhood = "Centro";
            var postalCode = "01000";
            var city = "Ciudad de México";
            var state = new MXState { Name = "Ciudad de México", Abbreviation = "DF" };
            var country = "México";
            var type = OrganizerCompanion.Core.Enums.Types.Home;
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-2);

            // Act
            var address = new MXAddress(id, street, neighborhood, postalCode, city, state, country, type, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(address.Id, Is.EqualTo(id));
                Assert.That(address.Street, Is.EqualTo(street));
                Assert.That(address.Neighborhood, Is.EqualTo(neighborhood));
                Assert.That(address.PostalCode, Is.EqualTo(postalCode));
                Assert.That(address.City, Is.EqualTo(city));
                Assert.That(address.State, Is.EqualTo(state));
                Assert.That(address.Country, Is.EqualTo(country));
                Assert.That(address.Type, Is.EqualTo(type));
                Assert.That(address.LinkedEntityId, Is.EqualTo(0));
                Assert.That(address.LinkedEntity, Is.Null);
                Assert.That(address.LinkedEntityType, Is.Null);
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
        public void Street_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newStreet = "Avenida Reforma 456";
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Street = newStreet;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street, Is.EqualTo(newStreet));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void Neighborhood_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newNeighborhood = "Roma Norte";
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Neighborhood = newNeighborhood;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Neighborhood, Is.EqualTo(newNeighborhood));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void PostalCode_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newPostalCode = "06700";
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.PostalCode = newPostalCode;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PostalCode, Is.EqualTo(newPostalCode));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void City_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newCity = "Guadalajara";
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
            var newState = new MXState { Name = "Jalisco", Abbreviation = "JA" };
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
        public void Country_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newCountry = "Mexico";
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
        public void LinkedEntityId_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newLinkedEntityId = 456;
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.LinkedEntityId = newLinkedEntityId;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(newLinkedEntityId));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var mockEntity = new MockDomainEntity(); // Create a mock domain entity
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.LinkedEntity = mockEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(mockEntity));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Street = "Calle de Prueba 123";
            _sut.Neighborhood = "Colonia Centro";
            _sut.PostalCode = "12345";
            _sut.City = "Ciudad de Prueba";
            _sut.State = new MXState { Name = "Estado de Prueba", Abbreviation = "EP" };
            _sut.Country = "México";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("\"street\":\"Calle de Prueba 123\""));
                Assert.That(json, Does.Contain("\"neighborhood\":\"Colonia Centro\""));
                Assert.That(json, Does.Contain("\"postalCode\":\"12345\""));
                Assert.That(json, Does.Contain("\"city\":\"Ciudad de Prueba\""));
                Assert.That(json, Does.Contain("\"country\":\"M\\u00E9xico\""));
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
            });
        }

        [Test, Category("Models")]
        public void ToString_WithAllProperties_ShouldReturnFormattedString()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Street = "Avenida Insurgentes 456";
            _sut.City = "Ciudad de México";
            _sut.State = new MXState { Name = "Ciudad de México", Abbreviation = "DF" };
            _sut.PostalCode = "03100";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Does.Contain(".Id:123"));
                Assert.That(result, Does.Contain(".Street1:Avenida Insurgentes 456"));
                Assert.That(result, Does.Contain(".City:Ciudad de México"));
                Assert.That(result, Does.Contain(".State:DF"));
                Assert.That(result, Does.Contain(".Zip:03100"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithStateNameOnly_ShouldUseStateName()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Street = "Calle Morelos 789";
            _sut.City = "Guadalajara";
            _sut.State = new MXState { Name = "Jalisco", Abbreviation = null };
            _sut.PostalCode = "44100";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.That(result, Does.Contain(".State:Jalisco"));
        }

        [Test, Category("Models")]
        public void ToString_WithNullState_ShouldShowUnknown()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Street = "Calle Principal 321";
            _sut.City = "Monterrey";
            _sut.State = null;
            _sut.PostalCode = "64000";

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
            _sut.Street = "Boulevard Kukulcán 654";
            _sut.City = "Cancún";
            _sut.State = new MXState { Name = null, Abbreviation = null };
            _sut.PostalCode = "77500";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.That(result, Does.Contain(".State:Unknown"));
        }

        [Test, Category("Models")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Cast<MXAddress>());
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
            var propertyInfo = typeof(MXAddress).GetProperty(nameof(MXAddress.DateCreated));
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
            _sut.Street = value;
            var firstModifiedDate = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Street = value; // Set to same value

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street, Is.EqualTo(value));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(firstModifiedDate));
                Assert.That(_sut.DateModified, Is.GreaterThan(firstModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void JsonSerialization_ShouldContainAllPropertiesInCorrectFormat()
        {
            // Arrange
            var address = new MXAddress
            {
                Id = 999,
                Street = "Paseo de la Reforma 123",
                Neighborhood = "Juárez",
                PostalCode = "06600",
                City = "Ciudad de México",
                State = new MXState { Name = "Ciudad de México", Abbreviation = "DF" },
                Country = "México",
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
                Assert.That(json, Does.Contain("\"street\":\"Paseo de la Reforma 123\""));
                Assert.That(json, Does.Contain("\"neighborhood\":\"Ju\\u00E1rez\""));
                Assert.That(json, Does.Contain("\"postalCode\":\"06600\""));
                Assert.That(json, Does.Contain("\"city\":\"Ciudad de M\\u00E9xico\""));
                Assert.That(json, Does.Contain("\"country\":\"M\\u00E9xico\""));
                Assert.That(json, Does.Contain("\"type\":4")); // Types.Billing enum value
                Assert.That(json, Does.Contain("\"state\"")); // State object should be serialized
                Assert.That(json, Does.Contain("\"linkedEntityId\":0"));
                Assert.That(json, Does.Contain("\"linkedEntity\":null"));
                Assert.That(json, Does.Contain("\"linkedEntityType\":null"));
                Assert.That(json, Does.Contain("\"dateCreated\""));
                Assert.That(json, Does.Contain("\"dateModified\""));
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithMXStateExtensions_ShouldWorkCorrectly()
        {
            // Arrange
            var id = 456;
            var street = "Calle Hidalgo 789";
            var neighborhood = "Centro Histórico";
            var postalCode = "44100";
            var city = "Guadalajara";
            var state = MXStates.Jalisco.ToStateModel();
            var country = "México";
            var type = OrganizerCompanion.Core.Enums.Types.Work;
            var dateCreated = DateTime.Now.AddDays(-2);
            var dateModified = DateTime.Now.AddHours(-1);

            // Act
            var address = new MXAddress(id, street, neighborhood, postalCode, city, state, country, type, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(address.Id, Is.EqualTo(id));
                Assert.That(address.Street, Is.EqualTo(street));
                Assert.That(address.Neighborhood, Is.EqualTo(neighborhood));
                Assert.That(address.PostalCode, Is.EqualTo(postalCode));
                Assert.That(address.City, Is.EqualTo(city));
                Assert.That(address.State?.Name, Is.EqualTo("Jalisco"));
                Assert.That(address.State?.Abbreviation, Is.EqualTo("JA"));
                Assert.That(address.Country, Is.EqualTo(country));
                Assert.That(address.Type, Is.EqualTo(type));
                Assert.That(address.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(address.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void State_WithMXStateExtension_ShouldWorkCorrectly()
        {
            // Arrange
            var originalDateModified = _sut.DateModified;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.State = MXStates.CiudadDeMéxico.ToStateModel();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.State, Is.Not.Null);
                Assert.That(_sut.State!.Name, Is.EqualTo("Ciudad de México"));
                Assert.That(_sut.State!.Abbreviation, Is.EqualTo("DF"));
                Assert.That(_sut.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void MXAddress_WithSpecialCharacters_ShouldHandleCorrectly()
        {
            // Arrange & Act
            _sut.Street = "Calle de la Constitución";
            _sut.Neighborhood = "Colonia México";
            _sut.City = "Mérida";
            _sut.State = MXStates.Yucatán.ToStateModel();
            _sut.Country = "México";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street, Is.EqualTo("Calle de la Constitución"));
                Assert.That(_sut.Neighborhood, Is.EqualTo("Colonia México"));
                Assert.That(_sut.City, Is.EqualTo("Mérida"));
                Assert.That(_sut.State?.Name, Is.EqualTo("Yucatán"));
                Assert.That(_sut.State?.Abbreviation, Is.EqualTo("YU"));
                Assert.That(_sut.Country, Is.EqualTo("México"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithSpecialCharactersInState_ShouldHandleCorrectly()
        {
            // Arrange
            _sut.Id = 789;
            _sut.Street = "Avenida Américas 123";
            _sut.City = "Querétaro";
            _sut.State = MXStates.Querétaro.ToStateModel();
            _sut.PostalCode = "76000";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Does.Contain(".Id:789"));
                Assert.That(result, Does.Contain(".Street1:Avenida Américas 123"));
                Assert.That(result, Does.Contain(".City:Querétaro"));
                Assert.That(result, Does.Contain(".State:QT"));
                Assert.That(result, Does.Contain(".Zip:76000"));
            });
        }

        [Test, Category("Models")]
        public void AllPropertiesUpdate_ShouldUpdateDateModifiedIndependently()
        {
            // Arrange
            var properties = new Dictionary<string, Action>
            {
                { "Id", () => _sut.Id = 1 },
                { "Street", () => _sut.Street = "Test Street" },
                { "Neighborhood", () => _sut.Neighborhood = "Test Neighborhood" },
                { "PostalCode", () => _sut.PostalCode = "12345" },
                { "City", () => _sut.City = "Test City" },
                { "State", () => _sut.State = new MXState { Name = "Test", Abbreviation = "TS" } },
                { "Country", () => _sut.Country = "Test Country" },
                { "Type", () => _sut.Type = OrganizerCompanion.Core.Enums.Types.Other },
                { "LinkedEntityId", () => _sut.LinkedEntityId = 123 },
                { "LinkedEntity", () => _sut.LinkedEntity = new MockDomainEntity() }
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
        public void JsonDeserialization_WithCompleteObject_ShouldDeserializeCorrectly()
        {
            // Arrange
            var originalAddress = new MXAddress
            {
                Id = 555,
                Street = "Calle Revolución 999",
                Neighborhood = "Del Valle",
                PostalCode = "03100",
                City = "Ciudad de México",
                State = new MXState { Name = "Ciudad de México", Abbreviation = "DF" },
                Country = "México",
                Type = OrganizerCompanion.Core.Enums.Types.Home
            };

            var json = originalAddress.ToJson();

            // Act & Assert - Verify JSON is valid and contains expected structure
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
                
                // Verify key JSON properties exist
                Assert.That(json, Does.Contain("\"id\":555"));
                Assert.That(json, Does.Contain("\"street\":\"Calle Revoluci\\u00F3n 999\""));
                Assert.That(json, Does.Contain("\"neighborhood\":\"Del Valle\""));
                Assert.That(json, Does.Contain("\"postalCode\":\"03100\""));
                Assert.That(json, Does.Contain("\"city\":\"Ciudad de M\\u00E9xico\""));
                Assert.That(json, Does.Contain("\"country\":\"M\\u00E9xico\""));
            });
        }

        [Test, Category("Models")]
        public void Properties_WithNullValues_ShouldHandleCorrectly()
        {
            // Act & Assert
            _sut.Street = null;
            Assert.That(_sut.Street, Is.Null);

            _sut.Neighborhood = null;
            Assert.That(_sut.Neighborhood, Is.Null);

            _sut.PostalCode = null;
            Assert.That(_sut.PostalCode, Is.Null);

            _sut.City = null;
            Assert.That(_sut.City, Is.Null);

            _sut.State = null;
            Assert.That(_sut.State, Is.Null);

            _sut.Country = null;
            Assert.That(_sut.Country, Is.Null);

            _sut.Type = null;
            Assert.That(_sut.Type, Is.Null);

            _sut.LinkedEntity = null;
            Assert.That(_sut.LinkedEntity, Is.Null);

            _sut.DateModified = null;
            Assert.That(_sut.DateModified, Is.Null);
        }

        [Test, Category("Models")]
        public void ToJson_WithNullProperties_ShouldHandleNullsCorrectly()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Street = null;
            _sut.Neighborhood = null;
            _sut.PostalCode = null;
            _sut.City = null;
            _sut.State = null;
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
                
                Assert.That(root.TryGetProperty("street", out var streetProperty), Is.True);
                Assert.That(streetProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
                
                Assert.That(root.TryGetProperty("dateModified", out var dateModifiedProperty), Is.True);
                Assert.That(dateModifiedProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
            });
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
        public void Properties_WithEmptyStrings_ShouldAcceptEmptyStrings()
        {
            // Act & Assert
            _sut.Street = string.Empty;
            Assert.That(_sut.Street, Is.EqualTo(string.Empty));

            _sut.Neighborhood = string.Empty;
            Assert.That(_sut.Neighborhood, Is.EqualTo(string.Empty));

            _sut.PostalCode = string.Empty;
            Assert.That(_sut.PostalCode, Is.EqualTo(string.Empty));

            _sut.City = string.Empty;
            Assert.That(_sut.City, Is.EqualTo(string.Empty));

            _sut.Country = string.Empty;
            Assert.That(_sut.Country, Is.EqualTo(string.Empty));
        }

        [Test, Category("Models")]
        public void ToJson_WithSerializerOptions_HandlesCircularReferences()
        {
            // Arrange
            _sut.Id = 100;
            _sut.Street = "Test Circular Street";
            _sut.City = "Test City";
            _sut.State = MXStates.Jalisco.ToStateModel();

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
