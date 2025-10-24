using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Extensions;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
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
            var beforeCreation = DateTime.UtcNow;

            // Act
            _sut = new MXAddress();
            var afterCreation = DateTime.UtcNow;

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
                Assert.That(_sut.LinkedEntityId, Is.Null);
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
                Assert.That(_sut.IsPrimary, Is.False);
                Assert.That(_sut.CreatedDate, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.CreatedDate, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.ModifiedDate, Is.Null);
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
            var isPrimary = true;
            var linkedEntityId = 42;
            var linkedEntity = new MockDomainEntity { Id = linkedEntityId };
            var createdDate = DateTime.Now.AddDays(-1);
            var modifiedDate = DateTime.Now.AddHours(-2);

            // Act
            var address = new MXAddress(
              id,
              street,
              neighborhood,
              postalCode,
              city,
              state,
              country,
              type,
              isPrimary,
              linkedEntityId,
              linkedEntity.GetType().Name,
              linkedEntity,
              createdDate,
              modifiedDate);

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
                Assert.That(address.LinkedEntityId, Is.EqualTo(42));
                Assert.That(address.LinkedEntityType, Is.EqualTo(linkedEntity.GetType().Name));
                Assert.That(address.LinkedEntity, Is.Not.Null);
                Assert.That(address.IsPrimary, Is.EqualTo(isPrimary));
                Assert.That(address.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(address.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        #region IMXAddressDTO Constructor Tests

        [Test, Category("Models")]
        public void DTOConstructor_WithCompleteDTO_ShouldSetAllProperties()
        {
            // Arrange
            var createdDate = DateTime.Now.AddDays(-5);

            var dto = new MXAddressDTO
            {
                Id = 123,
                Street = "Calle Principal 456",
                Neighborhood = "Centro Histórico",
                PostalCode = "06000",
                City = "Ciudad de México",
                State = new MXState { Name = "Ciudad de México", Abbreviation = "CDMX" },
                Country = "México",
                Type = OrganizerCompanion.Core.Enums.Types.Home,
                IsPrimary = true,
                CreatedDate = createdDate,
                ModifiedDate = DateTime.Now.AddDays(-1)
            };
            var linkedEntity = new MockDomainEntity { Id = 42 };

            // Act
            _sut = new MXAddress(dto, linkedEntity);
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.Street, Is.EqualTo(dto.Street));
                Assert.That(_sut.Neighborhood, Is.EqualTo(dto.Neighborhood));
                Assert.That(_sut.PostalCode, Is.EqualTo(dto.PostalCode));
                Assert.That(_sut.City, Is.EqualTo(dto.City));
                Assert.That(_sut.State, Is.EqualTo(dto.State));
                Assert.That(_sut.Country, Is.EqualTo(dto.Country));
                Assert.That(_sut.Type, Is.EqualTo(dto.Type));
                Assert.That(_sut.IsPrimary, Is.EqualTo(dto.IsPrimary));
                Assert.That(_sut.LinkedEntity, Is.EqualTo(linkedEntity));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(linkedEntity.Id));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                // CreatedDate should be set to current time, not DTO's CreatedDate (readonly field behavior)
                Assert.That(_sut.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(_sut.CreatedDate, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithMinimalDTO_ShouldSetBasicProperties()
        {
            // Arrange
            var dto = new MXAddressDTO
            {
                Street = "Calle Básica",
                City = "Puebla"
            };

            // Act
            _sut = new MXAddress(dto);
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.Street, Is.EqualTo(dto.Street));
                Assert.That(_sut.Neighborhood, Is.EqualTo(dto.Neighborhood));
                Assert.That(_sut.PostalCode, Is.EqualTo(dto.PostalCode));
                Assert.That(_sut.City, Is.EqualTo(dto.City));
                Assert.That(_sut.State, Is.EqualTo(dto.State));
                Assert.That(_sut.Country, Is.EqualTo(dto.Country));
                Assert.That(_sut.Type, Is.EqualTo(dto.Type));
                Assert.That(_sut.IsPrimary, Is.EqualTo(dto.IsPrimary));
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityId, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
                Assert.That(_sut.CreatedDate, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullLinkedEntity_ShouldHandleGracefully()
        {
            // Arrange
            var dto = new MXAddressDTO
            {
                Id = 789,
                Street = "Avenida Sin Entidad",
                City = "Monterrey",
                Type = OrganizerCompanion.Core.Enums.Types.Work,
                IsPrimary = false
            };

            // Act
            _sut = new MXAddress(dto, null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.Street, Is.EqualTo(dto.Street));
                Assert.That(_sut.City, Is.EqualTo(dto.City));
                Assert.That(_sut.Type, Is.EqualTo(dto.Type));
                Assert.That(_sut.IsPrimary, Is.EqualTo(dto.IsPrimary));
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityId, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithDifferentLinkedEntityTypes_ShouldSetCorrectEntityType()
        {
            // Arrange
            var dto = new MXAddressDTO
            {
                Street = "Calle Entidad Diferente",
                City = "Guadalajara"
            };
            var anotherEntity = new AnotherMockEntity { Id = 99 };

            // Act
            _sut = new MXAddress(dto, anotherEntity);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(anotherEntity));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(anotherEntity.Id));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("AnotherMockEntity"));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullStreetAndNeighborhood_ShouldAcceptNullValues()
        {
            // Arrange
            var dto = new MXAddressDTO
            {
                Id = 456,
                Street = null,
                Neighborhood = null,
                PostalCode = "12345",
                City = "Tijuana",
                Country = "México"
            };

            // Act
            _sut = new MXAddress(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.Street, Is.Null);
                Assert.That(_sut.Neighborhood, Is.Null);
                Assert.That(_sut.PostalCode, Is.EqualTo(dto.PostalCode));
                Assert.That(_sut.City, Is.EqualTo(dto.City));
                Assert.That(_sut.Country, Is.EqualTo(dto.Country));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithEmptyStrings_ShouldAcceptEmptyStrings()
        {
            // Arrange
            var dto = new MXAddressDTO
            {
                Street = string.Empty,
                Neighborhood = string.Empty,
                PostalCode = string.Empty,
                City = string.Empty,
                Country = string.Empty
            };

            // Act
            _sut = new MXAddress(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street, Is.EqualTo(string.Empty));
                Assert.That(_sut.Neighborhood, Is.EqualTo(string.Empty));
                Assert.That(_sut.PostalCode, Is.EqualTo(string.Empty));
                Assert.That(_sut.City, Is.EqualTo(string.Empty));
                Assert.That(_sut.Country, Is.EqualTo(string.Empty));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithSpecialCharactersAndUnicode_ShouldAcceptSpecialCharacters()
        {
            // Arrange
            var dto = new MXAddressDTO
            {
                Street = "Calle José María Vigil #123, Apto. 4B",
                Neighborhood = "Colonia Américas Unidas",
                PostalCode = "44160",
                City = "Guadalajara, Jalisco",
                Country = "México 🇲🇽"
            };

            // Act
            _sut = new MXAddress(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street, Is.EqualTo(dto.Street));
                Assert.That(_sut.Neighborhood, Is.EqualTo(dto.Neighborhood));
                Assert.That(_sut.PostalCode, Is.EqualTo(dto.PostalCode));
                Assert.That(_sut.City, Is.EqualTo(dto.City));
                Assert.That(_sut.Country, Is.EqualTo(dto.Country));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithLongStrings_ShouldAcceptLongStrings()
        {
            // Arrange
            var longStreet = new string('A', 500);
            var longNeighborhood = new string('B', 300);
            var longCity = new string('C', 200);
            var dto = new MXAddressDTO
            {
                Street = longStreet,
                Neighborhood = longNeighborhood,
                City = longCity
            };

            // Act
            _sut = new MXAddress(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street, Is.EqualTo(longStreet));
                Assert.That(_sut.Neighborhood, Is.EqualTo(longNeighborhood));
                Assert.That(_sut.City, Is.EqualTo(longCity));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithAllMXStates_ShouldAcceptAllValidStates()
        {
            // Arrange
            var testStates = new[]
            {
                new MXState { Name = "Aguascalientes", Abbreviation = "AGU" },
                new MXState { Name = "Baja California", Abbreviation = "BC" },
                new MXState { Name = "Chihuahua", Abbreviation = "CHIH" },
                new MXState { Name = "Jalisco", Abbreviation = "JAL" },
                new MXState { Name = "Yucatán", Abbreviation = "YUC" }
            };

            // Act & Assert
            foreach (var state in testStates)
            {
                var dto = new MXAddressDTO
                {
                    Street = "Calle Test",
                    City = "Ciudad Test",
                    State = state
                };
                
                _sut = new MXAddress(dto);
                
                Assert.Multiple(() =>
                {
                    Assert.That(_sut.State, Is.EqualTo(state));
                    Assert.That(_sut.State?.Name, Is.EqualTo(state.Name));
                    Assert.That(_sut.State?.Abbreviation, Is.EqualTo(state.Abbreviation));
                });
            }
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithAllAddressTypes_ShouldAcceptAllValidTypes()
        {
            // Arrange
            var testTypes = new[]
            {
                OrganizerCompanion.Core.Enums.Types.Home,
                OrganizerCompanion.Core.Enums.Types.Work,
                OrganizerCompanion.Core.Enums.Types.Mobil,
                OrganizerCompanion.Core.Enums.Types.Fax,
            };

            // Act & Assert
            foreach (var type in testTypes)
            {
                var dto = new MXAddressDTO
                {
                    Street = "Calle Test",
                    City = "Ciudad Test",
                    Type = type
                };
                
                _sut = new MXAddress(dto);
                
                Assert.That(_sut.Type, Is.EqualTo(type));
            }
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullType_ShouldAcceptNullType()
        {
            // Arrange
            var dto = new MXAddressDTO
            {
                Street = "Calle Sin Tipo",
                City = "Ciudad Test",
                Type = null
            };

            // Act
            _sut = new MXAddress(dto);

            // Assert
            Assert.That(_sut.Type, Is.Null);
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithIsPrimaryTrue_ShouldSetIsPrimaryCorrectly()
        {
            // Arrange
            var dto = new MXAddressDTO
            {
                Street = "Dirección Principal",
                City = "Ciudad Test",
                IsPrimary = true
            };

            // Act
            _sut = new MXAddress(dto);

            // Assert
            Assert.That(_sut.IsPrimary, Is.True);
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithIsPrimaryFalse_ShouldSetIsPrimaryCorrectly()
        {
            // Arrange
            var dto = new MXAddressDTO
            {
                Street = "Dirección Secundaria",
                City = "Ciudad Test",
                IsPrimary = false
            };

            // Act
            _sut = new MXAddress(dto);

            // Assert
            Assert.That(_sut.IsPrimary, Is.False);
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithZeroId_ShouldAcceptZeroValue()
        {
            // Arrange
            var dto = new MXAddressDTO
            {
                Id = 0,
                Street = "Calle Cero",
                City = "Ciudad Test"
            };

            // Act
            _sut = new MXAddress(dto);

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithMaxIntId_ShouldAcceptMaxValue()
        {
            // Arrange
            var dto = new MXAddressDTO
            {
                Id = int.MaxValue,
                Street = "Calle Máxima",
                City = "Ciudad Test"
            };

            // Act
            _sut = new MXAddress(dto);

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        #endregion

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
        public void Street_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newStreet = "Avenida Reforma 456";
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Street = newStreet;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street, Is.EqualTo(newStreet));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void Neighborhood_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newNeighborhood = "Roma Norte";
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Neighborhood = newNeighborhood;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Neighborhood, Is.EqualTo(newNeighborhood));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void PostalCode_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newPostalCode = "06700";
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.PostalCode = newPostalCode;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.PostalCode, Is.EqualTo(newPostalCode));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
            });
        }

        [Test, Category("Models")]
        public void City_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newCity = "Guadalajara";
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
            var newState = new MXState { Name = "Jalisco", Abbreviation = "JA" };
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
        public void Country_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newCountry = "Mexico";
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
        public void LinkedEntity_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var mockEntity = new MockDomainEntity(); // Create a mock domain entity
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

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
        public void IsPrimary_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.IsPrimary = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsPrimary, Is.True);
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
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
        public void IsPrimary_WhenTrue_ShouldSerializeToJsonCorrectly()
        {
            // Arrange
            _sut.IsPrimary = true;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Does.Contain("\"isPrimary\":true"));
                Assert.That(() => JsonSerializer.Deserialize<object>(json), Throws.Nothing);
            });
        }

        [Test, Category("Models")]
        public void IsPrimary_WhenFalse_ShouldSerializeToJsonCorrectly()
        {
            // Arrange
            _sut.IsPrimary = false;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Does.Contain("\"isPrimary\":false"));
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
        public void CreatedDate_ShouldBeReadOnly()
        {
            // Arrange
            var originalCreatedDate = _sut.CreatedDate;

            // Act & Assert - CreatedDate should not have a public setter
            var propertyInfo = typeof(MXAddress).GetProperty(nameof(MXAddress.CreatedDate));
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
            _sut.Street = value;
            var firstModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.Street = value; // Set to same value

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Street, Is.EqualTo(value));
                Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(firstModifiedDate));
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(firstModifiedDate));
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
                ModifiedDate = DateTime.Now.AddMinutes(-30)
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
                Assert.That(json, Does.Contain("\"linkedEntityId\":null"));
                Assert.That(json, Does.Contain("\"linkedEntity\":null"));
                Assert.That(json, Does.Contain("\"linkedEntityType\":null"));
                Assert.That(json, Does.Contain("\"createdDate\""));
                Assert.That(json, Does.Contain("\"modifiedDate\""));
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
            var isPrimary = false;
            var linkedEntityId = 42;
            var linkedEntity = new MockDomainEntity { Id = linkedEntityId };
            var createdDate = DateTime.Now.AddDays(-2);
            var modifiedDate = DateTime.Now.AddHours(-1);

            // Act
            var address = new MXAddress(
                id, 
                street, 
                neighborhood, 
                postalCode, 
                city, 
                state, 
                country, 
                type, 
                isPrimary,
                linkedEntityId,
                linkedEntity.GetType().Name,
                linkedEntity,
                createdDate, 
                modifiedDate);

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
                Assert.That(address.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(address.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("Models")]
        public void State_WithMXStateExtension_ShouldWorkCorrectly()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;
            Thread.Sleep(10); // Ensure time difference

            // Act
            _sut.State = MXStates.CiudadDeMéxico.ToStateModel();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.State, Is.Not.Null);
                Assert.That(_sut.State!.Name, Is.EqualTo("Ciudad de México"));
                Assert.That(_sut.State!.Abbreviation, Is.EqualTo("DF"));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.Now.AddSeconds(-1)));
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
        public void AllPropertiesUpdate_ShouldUpdateModifiedDateIndependently()
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

            _sut.ModifiedDate = null;
            Assert.That(_sut.ModifiedDate, Is.Null);
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

                Assert.That(root.TryGetProperty("street", out var streetProperty), Is.True);
                Assert.That(streetProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));

                Assert.That(root.TryGetProperty("modifiedDate", out var modifiedDateProperty), Is.True);
                Assert.That(modifiedDateProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
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
        public void Properties_WithMinIntValues_ShouldNotAcceptMinValues()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = int.MinValue);
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

        #region Cast Method Tests

        [Test, Category("Models")]
        public void Cast_ToMXAddressDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Street = "Calle Principal 456";
            _sut.Neighborhood = "Centro";
            _sut.PostalCode = "01000";
            _sut.City = "Ciudad de México";
            _sut.State = new MXState { Name = "Ciudad de México", Abbreviation = "DF" };
            _sut.Country = "México";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
            _sut.IsPrimary = true;

            // Act
            var result = _sut.Cast<MXAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<MXAddressDTO>());
                Assert.That(result.Id, Is.EqualTo(123));
                Assert.That(result.Street, Is.EqualTo("Calle Principal 456"));
                Assert.That(result.Neighborhood, Is.EqualTo("Centro"));
                Assert.That(result.PostalCode, Is.EqualTo("01000"));
                Assert.That(result.City, Is.EqualTo("Ciudad de México"));
                Assert.That(result.State, Is.EqualTo(_sut.State));
                Assert.That(result.Country, Is.EqualTo("México"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
                Assert.That(result.IsPrimary, Is.True);
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIMXAddressDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 456;
            _sut.Street = "Avenida Reforma 789";
            _sut.Neighborhood = "Juárez";
            _sut.PostalCode = "06600";
            _sut.City = "Ciudad de México";
            _sut.State = MXStates.Jalisco.ToStateModel();
            _sut.Country = "México";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
            _sut.IsPrimary = false;

            // Act
            var result = _sut.Cast<IMXAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<MXAddressDTO>());
                Assert.That(result.Id, Is.EqualTo(456));
                Assert.That(result.Street, Is.EqualTo("Avenida Reforma 789"));
                Assert.That(result.Neighborhood, Is.EqualTo("Juárez"));
                Assert.That(result.PostalCode, Is.EqualTo("06600"));
                Assert.That(result.City, Is.EqualTo("Ciudad de México"));
                Assert.That(result.State?.Name, Is.EqualTo("Jalisco"));
                Assert.That(result.State?.Abbreviation, Is.EqualTo("JA"));
                Assert.That(result.Country, Is.EqualTo("México"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(result.IsPrimary, Is.False);
            });
        }

        [Test, Category("Models")]
        public void Cast_ToMXAddressDTO_WithNullValues_ShouldHandleNullValues()
        {
            // Arrange
            _sut.Id = 789;
            _sut.Street = null;
            _sut.Neighborhood = null;
            _sut.PostalCode = null;
            _sut.City = null;
            _sut.State = null;
            _sut.Country = null;
            _sut.Type = null;

            // Act
            var result = _sut.Cast<MXAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<MXAddressDTO>());
                Assert.That(result.Id, Is.EqualTo(789));
                Assert.That(result.Street, Is.Null);
                Assert.That(result.Neighborhood, Is.Null);
                Assert.That(result.PostalCode, Is.Null);
                Assert.That(result.City, Is.Null);
                Assert.That(result.State, Is.Null);
                Assert.That(result.Country, Is.Null);
                Assert.That(result.Type, Is.Null);
                Assert.That(result.IsPrimary, Is.False);
            });
        }

        [Test, Category("Models")]
        public void Cast_ToMXAddressDTO_WithEmptyStrings_ShouldHandleEmptyStrings()
        {
            // Arrange
            _sut.Id = 101;
            _sut.Street = string.Empty;
            _sut.Neighborhood = string.Empty;
            _sut.PostalCode = string.Empty;
            _sut.City = string.Empty;
            _sut.Country = string.Empty;
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Other;

            // Act
            var result = _sut.Cast<MXAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Street, Is.EqualTo(string.Empty));
                Assert.That(result.Neighborhood, Is.EqualTo(string.Empty));
                Assert.That(result.PostalCode, Is.EqualTo(string.Empty));
                Assert.That(result.City, Is.EqualTo(string.Empty));
                Assert.That(result.Country, Is.EqualTo(string.Empty));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedType_ShouldThrowInvalidCastException()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Street = "Test Street";
            _sut.City = "Test City";

            // Act & Assert
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Contains.Substring("Cannot cast MXAddress to type MockDomainEntity."));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToMXAddressDTO_WithCompleteData_ShouldPreserveAllData()
        {
            // Arrange - Set up MXAddress with comprehensive data
            var linkedEntityId = 42;
            var linkedEntity = new MockDomainEntity { Id = linkedEntityId };
            var createdDate = DateTime.Now.AddDays(-7);
            var modifiedDate = DateTime.Now.AddHours(-2);

            var fullAddress = new MXAddress(
                id: 999,
                street: "Paseo de la Reforma 1234",
                neighborhood: "Polanco",
                postalCode: "11560",
                city: "Ciudad de México",
                state: MXStates.CiudadDeMéxico.ToStateModel(),
                country: "México",
                type: OrganizerCompanion.Core.Enums.Types.Billing,
                isPrimary: true,
                linkedEntityId: linkedEntityId,
                linkedEntityType: linkedEntity.GetType().Name,
                linkedEntity: linkedEntity,
                createdDate: createdDate,
                modifiedDate: modifiedDate
            );

            // Act
            var result = fullAddress.Cast<MXAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<MXAddressDTO>());
                Assert.That(result.Id, Is.EqualTo(999));
                Assert.That(result.Street, Is.EqualTo("Paseo de la Reforma 1234"));
                Assert.That(result.Neighborhood, Is.EqualTo("Polanco"));
                Assert.That(result.PostalCode, Is.EqualTo("11560"));
                Assert.That(result.City, Is.EqualTo("Ciudad de México"));
                Assert.That(result.State?.Name, Is.EqualTo("Ciudad de México"));
                Assert.That(result.State?.Abbreviation, Is.EqualTo("DF"));
                Assert.That(result.Country, Is.EqualTo("México"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Billing));
                Assert.That(result.IsPrimary, Is.True);
                // Note: LinkedEntity, LinkedEntityId, etc. are not part of MXAddressDTO
                // This is by design as MXAddressDTO is a simplified representation
            });
        }

        [Test, Category("Models")]
        public void Cast_MultipleCallsToSameType_ShouldReturnDifferentInstances()
        {
            // Arrange
            _sut.Id = 777;
            _sut.Street = "Calle Independencia 321";
            _sut.City = "Guadalajara";
            _sut.State = MXStates.Jalisco.ToStateModel();
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;

            // Act
            var result1 = _sut.Cast<MXAddressDTO>();
            var result2 = _sut.Cast<MXAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.Not.Null);
                Assert.That(result2, Is.Not.Null);
                Assert.That(result1, Is.Not.SameAs(result2), "Each cast should return a new instance");
                Assert.That(result1.Id, Is.EqualTo(result2.Id));
                Assert.That(result1.Street, Is.EqualTo(result2.Street));
                Assert.That(result1.City, Is.EqualTo(result2.City));
                Assert.That(result1.State?.Name, Is.EqualTo(result2.State?.Name));
                Assert.That(result1.Type, Is.EqualTo(result2.Type));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToMXAddressDTO_WithSpecialCharacters_ShouldPreserveCharacters()
        {
            // Arrange
            var specialStreet = "Calle José María Morelos y Pavón #123";
            var specialNeighborhood = "Colonia Américas Unidas";
            var specialCity = "Mérida";
            _sut.Id = 888;
            _sut.Street = specialStreet;
            _sut.Neighborhood = specialNeighborhood;
            _sut.City = specialCity;
            _sut.State = MXStates.Yucatán.ToStateModel();
            _sut.Country = "México";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Fax;

            // Act
            var result = _sut.Cast<MXAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Street, Is.EqualTo(specialStreet));
                Assert.That(result.Neighborhood, Is.EqualTo(specialNeighborhood));
                Assert.That(result.City, Is.EqualTo(specialCity));
                Assert.That(result.State?.Name, Is.EqualTo("Yucatán"));
                Assert.That(result.Country, Is.EqualTo("México"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Fax));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToMXAddressDTO_WithAllTypesEnum_ShouldPreserveTypeCorrectly()
        {
            // Test each enum value
            var enumValues = Enum.GetValues<OrganizerCompanion.Core.Enums.Types>();

            foreach (var enumValue in enumValues)
            {
                // Arrange
                _sut.Id = 100;
                _sut.Street = $"Calle {enumValue} 123";
                _sut.City = "Puebla";
                _sut.State = MXStates.Puebla.ToStateModel();
                _sut.Type = enumValue;

                // Act
                var result = _sut.Cast<MXAddressDTO>();

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.Type, Is.EqualTo(enumValue), $"Type {enumValue} should be preserved");
                    Assert.That(result.Street, Is.EqualTo($"Calle {enumValue} 123"));
                });
            }
        }

        [Test, Category("Models")]
        public void Cast_WithExceptionInCasting_ShouldWrapInInvalidCastException()
        {
            // This test verifies the exception handling in the Cast method
            // Since the current implementation doesn't have scenarios that cause inner exceptions,
            // this test documents the expected behavior when such scenarios arise

            // Arrange
            _sut.Id = 1;
            _sut.Street = "Test Street";

            // Act & Assert - Test unsupported type casting
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<AnotherMockEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Contains.Substring("Cannot cast MXAddress to type AnotherMockEntity."));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToMXAddressDTO_WithZeroId_ShouldAllowZeroId()
        {
            // Arrange
            _sut.Id = 0;
            _sut.Street = "Calle Zero";
            _sut.City = "Test City";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;

            // Act
            var result = _sut.Cast<MXAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(0));
                Assert.That(result.Street, Is.EqualTo("Calle Zero"));
                Assert.That(result.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToMXAddressDTO_WithMaxIntId_ShouldHandleLargeIds()
        {
            // Arrange
            _sut.Id = int.MaxValue;
            _sut.Street = "Calle MaxInt";
            _sut.City = "Large City";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Act
            var result = _sut.Cast<MXAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(int.MaxValue));
                Assert.That(result.Street, Is.EqualTo("Calle MaxInt"));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToMXAddressDTO_WithNegativeId_ShouldNotAllowNegativeIds()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _sut.Id = -1; // Setting negative Id should throw
            });
        }

        [Test, Category("Models")]
        public void Cast_ToMXAddressDTO_WithLongStrings_ShouldHandleLongStrings()
        {
            // Arrange
            var longStreet = new string('A', 500) + " Muy Larga";
            var longNeighborhood = new string('B', 300) + " Colonia";
            _sut.Id = 555;
            _sut.Street = longStreet;
            _sut.Neighborhood = longNeighborhood;
            _sut.City = "Test City";

            // Act
            var result = _sut.Cast<MXAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Street, Is.EqualTo(longStreet));
                Assert.That(result.Neighborhood, Is.EqualTo(longNeighborhood));
                Assert.That(result.Street!.Length, Is.EqualTo(510)); // 500 + " Muy Larga".Length (10)
                Assert.That(result.Neighborhood!.Length, Is.EqualTo(308)); // 300 + " Colonia".Length (8)
            });
        }

        [Test, Category("Models")]
        public void Cast_ToMXAddressDTO_WithAllMXStates_ShouldPreserveStateCorrectly()
        {
            // Test several Mexican states
            var testStates = new[]
            {
                MXStates.Jalisco.ToStateModel(),
                MXStates.CiudadDeMéxico.ToStateModel(),
                MXStates.Yucatán.ToStateModel(),
                MXStates.NuevoLeón.ToStateModel(),
                MXStates.Querétaro.ToStateModel()
            };

            foreach (var state in testStates)
            {
                // Arrange
                _sut.Id = 200;
                _sut.Street = $"Calle {state.Name} 123";
                _sut.City = "Test City";
                _sut.State = state;
                _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;

                // Act
                var result = _sut.Cast<MXAddressDTO>();

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(result.State, Is.Not.Null);
                    Assert.That(result.State!.Name, Is.EqualTo(state.Name), $"State name {state.Name} should be preserved");
                    Assert.That(result.State!.Abbreviation, Is.EqualTo(state.Abbreviation), $"State abbreviation {state.Abbreviation} should be preserved");
                });
            }
        }

        [Test, Category("Models")]
        public void Cast_ToMXAddressDTO_WithPostalCodeFormats_ShouldHandleVariousFormats()
        {
            // Test different postal code formats that are valid in Mexico
            var postalCodes = new[] { "01000", "12345", "99999", "00001", "77500" };

            foreach (var postalCode in postalCodes)
            {
                // Arrange
                _sut.Id = 300;
                _sut.Street = "Test Street";
                _sut.PostalCode = postalCode;
                _sut.City = "Test City";

                // Act
                var result = _sut.Cast<MXAddressDTO>();

                // Assert
                Assert.That(result.PostalCode, Is.EqualTo(postalCode), $"Postal code {postalCode} should be preserved");
            }
        }

        #endregion

        #region Additional Comprehensive Coverage Tests

        [Test, Category("Models")]
        public void JsonConstructor_WithUnusedParameters_ShouldIgnoreThemAndSetPropertiesCorrectly()
        {
            // Test that JsonConstructor handles unused parameters (isCast, castId, castType) correctly

            // Arrange & Act
            var linkedEntityId = 42;
            var linkedEntity = new MockDomainEntity { Id = linkedEntityId };
            var testDate = DateTime.Now;
            var state = MXStates.Jalisco.ToStateModel();
            var address = new MXAddress(
                id: 999,
                street: "Comprehensive Test Street 123",
                neighborhood: "Test Neighborhood",
                postalCode: "12345",
                city: "Test City",
                state: state,
                country: "México",
                type: OrganizerCompanion.Core.Enums.Types.Home,
                isPrimary: false,
                linkedEntityId: linkedEntityId,
                linkedEntityType: linkedEntity.GetType().Name,
                linkedEntity: linkedEntity,
                createdDate: testDate.AddDays(-1),
                modifiedDate: testDate
            );

            // Assert - Verify that the object is created correctly and unused parameters don't affect it
            Assert.Multiple(() =>
            {
                Assert.That(address.Id, Is.EqualTo(999));
                Assert.That(address.Street, Is.EqualTo("Comprehensive Test Street 123"));
                Assert.That(address.Neighborhood, Is.EqualTo("Test Neighborhood"));
                Assert.That(address.PostalCode, Is.EqualTo("12345"));
                Assert.That(address.City, Is.EqualTo("Test City"));
                Assert.That(address.State, Is.EqualTo(state));
                Assert.That(address.Country, Is.EqualTo("México"));
                Assert.That(address.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
                Assert.That(address.CreatedDate, Is.EqualTo(testDate.AddDays(-1)));
                Assert.That(address.ModifiedDate, Is.EqualTo(testDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ExceptionHandling_RethrowsCorrectly()
        {
            // This test verifies that the catch block in the Cast method properly rethrows exceptions
            
            // Arrange
            _sut.Id = 1;
            _sut.Street = "Test Street";
            _sut.City = "Test City";

            // Act & Assert - Test that InvalidCastException is thrown and rethrown correctly
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast MXAddress to type MockDomainEntity"));
            });
        }

        [Test, Category("Models")]
        public void DefaultConstructor_ShouldInitializeCountryToMexico()
        {
            // Test that default constructor sets Country to Mexico using extension method
            
            // Arrange & Act
            var address = new MXAddress();

            // Assert
            Assert.That(address.Country, Is.EqualTo(Countries.Mexico.GetName()));
        }

        [Test, Category("Models")]
        public void ToString_StateDisplayLogic_ComprehensiveTest()
        {
            // Test all ToString state display logic branches
            
            // Case 1: State with abbreviation (should use abbreviation)
            _sut.Id = 1;
            _sut.Street = "Test Street";
            _sut.City = "Test City";
            _sut.PostalCode = "12345";
            _sut.State = new MXState { Name = "Jalisco", Abbreviation = "JA" };
            
            var result1 = _sut.ToString();
            Assert.That(result1, Does.Contain(".State:JA"));
            
            // Case 2: State with name but null abbreviation (should use name)
            _sut.State = new MXState { Name = "Yucatán", Abbreviation = null };
            
            var result2 = _sut.ToString();
            Assert.That(result2, Does.Contain(".State:Yucatán"));
            
            // Case 3: State with both null (should use "Unknown")
            _sut.State = new MXState { Name = null, Abbreviation = null };
            
            var result3 = _sut.ToString();
            Assert.That(result3, Does.Contain(".State:Unknown"));
            
            // Case 4: Null state (should use "Unknown")
            _sut.State = null;
            
            var result4 = _sut.ToString();
            Assert.That(result4, Does.Contain(".State:Unknown"));
        }

        [Test, Category("Models")]
        public void SerializerOptions_CyclicalReferenceHandling_ComprehensiveTest()
        {
            // Test that the serialization options handle complex scenarios correctly

            // Arrange - Create address with complex data
            var linkedEntityId = 42;
            var linkedEntity = new MockDomainEntity { Id = linkedEntityId };

            _sut = new MXAddress(
                id: 1,
                street: "Serialization Test Street",
                neighborhood: "Test Neighborhood",
                postalCode: "01000",
                city: "Ciudad de México",
                state: MXStates.CiudadDeMéxico.ToStateModel(),
                country: "México",
                type: OrganizerCompanion.Core.Enums.Types.Work,
                isPrimary: true,
                linkedEntityId: linkedEntityId,
                linkedEntityType: linkedEntity.GetType().Name,
                linkedEntity: linkedEntity,
                createdDate: DateTime.Now.AddHours(-1),
                modifiedDate: DateTime.Now
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
            var linkedEntityId = 42;
            var linkedEntity = new MockDomainEntity { Id = linkedEntityId };

            // Arrange
            _sut = new MXAddress(
                id: 100,
                street: "Multi Cast Street",
                neighborhood: "Multi Neighborhood",
                postalCode: "12345",
                city: "Multi City",
                state: MXStates.Jalisco.ToStateModel(),
                country: "México",
                type: OrganizerCompanion.Core.Enums.Types.Mobil,
                isPrimary: true,
                linkedEntityId: linkedEntityId,
                linkedEntityType: linkedEntity.GetType().Name,
                linkedEntity: linkedEntity,
                createdDate: DateTime.Now.AddDays(-1),
                modifiedDate: DateTime.Now
            );

            // Act - Cast to different supported types
            var addressDto1 = _sut.Cast<MXAddressDTO>();
            var addressDto2 = _sut.Cast<MXAddressDTO>();
            var iAddressDto = _sut.Cast<IMXAddressDTO>();

            // Assert - All should be different instances but with same data
            Assert.Multiple(() =>
            {
                Assert.That(addressDto1, Is.Not.SameAs(addressDto2));
                Assert.That(addressDto1, Is.Not.SameAs(iAddressDto));
                Assert.That(addressDto2, Is.Not.SameAs(iAddressDto));
                
                // All should have same data
                Assert.That(addressDto1.Id, Is.EqualTo(100));
                Assert.That(addressDto2.Id, Is.EqualTo(100));
                Assert.That(iAddressDto.Id, Is.EqualTo(100));
                
                Assert.That(addressDto1.Street, Is.EqualTo("Multi Cast Street"));
                Assert.That(addressDto2.Street, Is.EqualTo("Multi Cast Street"));
                Assert.That(iAddressDto.Street, Is.EqualTo("Multi Cast Street"));
                
                Assert.That(addressDto1.Neighborhood, Is.EqualTo("Multi Neighborhood"));
                Assert.That(addressDto2.Neighborhood, Is.EqualTo("Multi Neighborhood"));
                Assert.That(iAddressDto.Neighborhood, Is.EqualTo("Multi Neighborhood"));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_ComplexTypeOperations_ShouldUpdateLinkedEntityTypeCorrectly()
        {
            // Test comprehensive LinkedEntity behavior with type changes

            // Arrange
            _sut.Id = 1;
            var entity1 = new MockDomainEntity();
            var entity2 = new AnotherMockEntity();
            var originalModifiedDate = _sut.ModifiedDate;

            // Act & Assert - Test multiple entity changes
            System.Threading.Thread.Sleep(10);
            _sut.LinkedEntity = entity1;
            var firstModified = _sut.ModifiedDate;
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(entity1));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(firstModified, Is.GreaterThan(originalModifiedDate));
            });

            System.Threading.Thread.Sleep(10);
            _sut.LinkedEntity = entity2;
            var secondModified = _sut.ModifiedDate;
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(entity2));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("AnotherMockEntity"));
                Assert.That(secondModified, Is.GreaterThan(firstModified));
            });

            System.Threading.Thread.Sleep(10);
            _sut.LinkedEntity = null;
            var thirdModified = _sut.ModifiedDate;
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.LinkedEntityType, Is.Null);
                Assert.That(thirdModified, Is.GreaterThan(secondModified));
            });
        }

        [Test, Category("Models")]
        public void ModifiedDate_PropertyChangeCombinations_ShouldUpdateInSequence()
        {
            // Test that rapid sequential property changes all update ModifiedDate correctly

            // Arrange
            _sut = new MXAddress
            {
                Id = 1
            };
            var timestamps = new List<DateTime?>
            {
              _sut.ModifiedDate // Initial state
            };

            // Act & Assert - Test sequential property changes
            System.Threading.Thread.Sleep(2);
            _sut.Id = 1;
            timestamps.Add(_sut.ModifiedDate);

            System.Threading.Thread.Sleep(2);
            _sut.Street = "Sequential Street";
            timestamps.Add(_sut.ModifiedDate);

            System.Threading.Thread.Sleep(2);
            _sut.Neighborhood = "Sequential Neighborhood";
            timestamps.Add(_sut.ModifiedDate);

            System.Threading.Thread.Sleep(2);
            _sut.PostalCode = "12345";
            timestamps.Add(_sut.ModifiedDate);

            System.Threading.Thread.Sleep(2);
            _sut.City = "Sequential City";
            timestamps.Add(_sut.ModifiedDate);

            System.Threading.Thread.Sleep(2);
            _sut.State = MXStates.Jalisco.ToStateModel();
            timestamps.Add(_sut.ModifiedDate);

            System.Threading.Thread.Sleep(2);
            _sut.Country = "Updated México";
            timestamps.Add(_sut.ModifiedDate);

            System.Threading.Thread.Sleep(2);
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
            timestamps.Add(_sut.ModifiedDate);

            System.Threading.Thread.Sleep(2);
            _sut.LinkedEntity = new MockDomainEntity();
            timestamps.Add(_sut.ModifiedDate);

            // Assert - Each timestamp should be greater than the previous
            for (int i = 1; i < timestamps.Count; i++)
            {
                Assert.That(timestamps[i], Is.GreaterThan(timestamps[i - 1]), 
                    $"Timestamp at index {i} should be greater than timestamp at index {i - 1}");
            }
        }

        [Test, Category("Models")]
        public void JsonSerialization_WithSpecialCharactersAndUnicode_ShouldSerializeCorrectly()
        {
            // Test JSON serialization with special characters and Unicode
            
            // Arrange
            _sut.Id = 888;
            _sut.Street = "Calle José María Morelos y Pavón #123 (Esquina)";
            _sut.Neighborhood = "Colonia Américas Unidas & Desarrollo";
            _sut.PostalCode = "12345";
            _sut.City = "Mérida de Yucatán";
            _sut.State = MXStates.Yucatán.ToStateModel();
            _sut.Country = "México 🇲🇽";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":888"));
                
                // Verify JSON is valid by attempting to parse
                Assert.DoesNotThrow(() =>
                {
                    var document = System.Text.Json.JsonDocument.Parse(json);
                    Assert.That(document.RootElement.ValueKind, Is.EqualTo(System.Text.Json.JsonValueKind.Object));
                    document.Dispose();
                });
            });
        }

        [Test, Category("Models")]
        public void AllMXStatesExtensions_ComprehensiveTest()
        {
            // Test all MX states from the enum work correctly with ToStateModel extension
            
            var allMXStates = Enum.GetValues<MXStates>();
            
            foreach (var state in allMXStates)
            {
                // Arrange
                var stateModel = state.ToStateModel();
                _sut.State = stateModel;
                
                // Act & Assert
                Assert.Multiple(() =>
                {
                    Assert.That(_sut.State, Is.Not.Null);
                    Assert.That(_sut.State.Name, Is.Not.Null.And.Not.Empty);
                    Assert.That(_sut.State.Abbreviation, Is.Not.Null.And.Not.Empty);
                });
                
                // Test ToString with each state
                _sut.Id = (int)state;
                _sut.Street = $"Test Street for {state}";
                _sut.City = "Test City";
                _sut.PostalCode = "12345";
                
                var toStringResult = _sut.ToString();
                Assert.That(toStringResult, Does.Contain($".State:{stateModel.Abbreviation}"));
            }
        }

        [Test, Category("Models")]
        public void MXAddress_ComprehensiveFunctionalityIntegrationTest()
        {
            // Comprehensive test that exercises all major functionality together
            
            // Test default constructor
            var defaultAddress = new MXAddress();
            Assert.Multiple(() =>
            {
                Assert.That(defaultAddress, Is.Not.Null);
                Assert.That(defaultAddress.CreatedDate, Is.Not.EqualTo(default(DateTime)));
                Assert.That(defaultAddress.Country, Is.EqualTo(Countries.Mexico.GetName()));
            });

            // Test JsonConstructor with comprehensive data
            var linkedEntityId = 42;
            var linkedEntity = new MockDomainEntity { Id = linkedEntityId };
            var testDate = DateTime.Now;
            var state = MXStates.CiudadDeMéxico.ToStateModel();
            var comprehensiveAddress = new MXAddress(
                id: 12345,
                street: "Comprehensive Test Address",
                neighborhood: "Test Neighborhood",
                postalCode: "01000",
                city: "Ciudad de México",
                state: state,
                country: "México",
                type: OrganizerCompanion.Core.Enums.Types.Work,
                isPrimary: false,
                linkedEntityId: linkedEntityId,
                linkedEntityType: linkedEntity.GetType().Name,
                linkedEntity: linkedEntity,
                createdDate: testDate.AddDays(-1),
                modifiedDate: testDate
            );
            
            // Verify comprehensive properties
            Assert.Multiple(() =>
            {
                Assert.That(comprehensiveAddress.Id, Is.EqualTo(12345));
                Assert.That(comprehensiveAddress.Street, Is.EqualTo("Comprehensive Test Address"));
                Assert.That(comprehensiveAddress.Neighborhood, Is.EqualTo("Test Neighborhood"));
                Assert.That(comprehensiveAddress.PostalCode, Is.EqualTo("01000"));
                Assert.That(comprehensiveAddress.City, Is.EqualTo("Ciudad de México"));
                Assert.That(comprehensiveAddress.State, Is.EqualTo(state));
                Assert.That(comprehensiveAddress.Country, Is.EqualTo("México"));
                Assert.That(comprehensiveAddress.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(comprehensiveAddress.CreatedDate, Is.EqualTo(testDate.AddDays(-1)));
                Assert.That(comprehensiveAddress.ModifiedDate, Is.EqualTo(testDate));
            });
            
            // Test all property setters
            defaultAddress.Id = 54321;
            defaultAddress.Street = "Updated Street";
            defaultAddress.Neighborhood = "Updated Neighborhood";
            defaultAddress.PostalCode = "54321";
            defaultAddress.City = "Updated City";
            defaultAddress.State = MXStates.Jalisco.ToStateModel();
            defaultAddress.Country = "Updated México";
            defaultAddress.Type = OrganizerCompanion.Core.Enums.Types.Billing;
            defaultAddress.LinkedEntity = new MockDomainEntity() { Id = 999 };
            
            Assert.Multiple(() =>
            {
                Assert.That(defaultAddress.Id, Is.EqualTo(54321));
                Assert.That(defaultAddress.Street, Is.EqualTo("Updated Street"));
                Assert.That(defaultAddress.Neighborhood, Is.EqualTo("Updated Neighborhood"));
                Assert.That(defaultAddress.PostalCode, Is.EqualTo("54321"));
                Assert.That(defaultAddress.City, Is.EqualTo("Updated City"));
                Assert.That(defaultAddress.Country, Is.EqualTo("Updated México"));
                Assert.That(defaultAddress.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Billing));
                Assert.That(defaultAddress.LinkedEntityId, Is.EqualTo(999));
                Assert.That(defaultAddress.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(defaultAddress.CreatedDate, Is.Not.EqualTo(default(DateTime)));
            });
            
            // Test Cast functionality
            var addressDto = defaultAddress.Cast<MXAddressDTO>();
            var iAddressDto = defaultAddress.Cast<IMXAddressDTO>();
            
            Assert.Multiple(() =>
            {
                Assert.That(addressDto, Is.InstanceOf<MXAddressDTO>());
                Assert.That(iAddressDto, Is.InstanceOf<MXAddressDTO>());
                Assert.That(addressDto.Id, Is.EqualTo(defaultAddress.Id));
                Assert.That(iAddressDto.Id, Is.EqualTo(defaultAddress.Id));
            });
            
            // Test JSON serialization
            var json = defaultAddress.ToJson();
            Assert.That(json, Is.Not.Null.And.Not.Empty);
            
            // Test ToString functionality
            var stringResult = defaultAddress.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(stringResult, Is.Not.Null.And.Not.Empty);
                Assert.That(stringResult, Does.Contain("Updated Street"));
                Assert.That(stringResult, Does.Contain("54321"));
                Assert.That(stringResult, Does.Contain("JA")); // Jalisco abbreviation
            });
            
            // Test exception scenarios
            Assert.Throws<InvalidCastException>(() => defaultAddress.Cast<MockDomainEntity>());
        }

        [Test, Category("Models")]
        public void LinkedEntityType_ReadOnlyProperty_ComprehensiveBehaviorTest()
        {
            // Comprehensive test of LinkedEntityType read-only behavior
            
            // Verify property is read-only
            var property = typeof(MXAddress).GetProperty("LinkedEntityType");
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
            _sut = new MXAddress
            {
                Id = 1,
                Street = "Exception Test Street",
                City = "Exception City"
            };
            
            // Act & Assert - Verify that InvalidCastException is thrown for unsupported types
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast MXAddress to type MockDomainEntity"));
                Assert.That(ex.InnerException, Is.Null); // Should be the original exception, not wrapped
            });
            
            // Test multiple unsupported types
            Assert.Throws<InvalidCastException>(() => _sut.Cast<AnotherMockEntity>());
        }

        [Test, Category("Models")]
        public void CreatedDate_ReadOnlyBehavior_ComprehensiveTest()
        {
            // Comprehensive test of CreatedDate read-only behavior
            
            // Verify property is read-only
            var property = typeof(MXAddress).GetProperty("CreatedDate");
            Assert.That(property?.SetMethod, Is.Null, "CreatedDate should be read-only");
            
            // Test that CreatedDate is set during construction and doesn't change
            var beforeCreation = DateTime.UtcNow;
            var address1 = new MXAddress();
            var afterCreation = DateTime.UtcNow;
            
            Assert.Multiple(() =>
            {
                Assert.That(address1.CreatedDate, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(address1.CreatedDate, Is.LessThanOrEqualTo(afterCreation));
            });
            
            // Test that different instances have different CreatedDate values
            System.Threading.Thread.Sleep(1);
            var address2 = new MXAddress();
            Assert.That(address2.CreatedDate, Is.GreaterThanOrEqualTo(address1.CreatedDate));

            // Test JsonConstructor with specific CreatedDate
            var linkedEntityId = 42;
            var linkedEntity = new MockDomainEntity { Id = linkedEntityId };
            var specificDate = DateTime.Now.AddDays(-5);
            var address3 = new MXAddress(
                id: 1,
                street: "Date Test Street",
                neighborhood: "Date Test",
                postalCode: "12345",
                city: "Date City",
                state: MXStates.Jalisco.ToStateModel(),
                country: "México",
                type: OrganizerCompanion.Core.Enums.Types.Home,
                isPrimary: true,
                linkedEntityId: linkedEntityId,
                linkedEntityType: linkedEntity.GetType().Name,
                linkedEntity: linkedEntity,
                createdDate: specificDate,
                modifiedDate: DateTime.Now
            );
            
            Assert.That(address3.CreatedDate, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void ToString_WithExtremeValues_ShouldHandleAllScenarios()
        {
            // Test ToString with various extreme value combinations
            
            // Case 1: Maximum values
            _sut.Id = int.MaxValue;
            _sut.Street = new string('A', 1000);
            _sut.City = new string('B', 500);
            _sut.PostalCode = new string('1', 10);
            _sut.State = new MXState { Name = "VeryLongStateName", Abbreviation = "VLSN" };
            
            var maxResult = _sut.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(maxResult, Is.Not.Null);
                Assert.That(maxResult, Does.Contain(int.MaxValue.ToString()));
                Assert.That(maxResult, Does.Contain(".Id:" + int.MaxValue));
                Assert.That(maxResult, Does.Contain(".State:VLSN"));
            });

            // Case 2: Minimum/Zero values with empty strings
            _sut.Id = 0;
            _sut.Street = "";
            _sut.City = "";
            _sut.PostalCode = "";
            _sut.State = new MXState { Name = "", Abbreviation = "" };
            
            var minResult = _sut.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(minResult, Is.Not.Null);
                Assert.That(minResult, Does.Contain(".Id:0"));
                Assert.That(minResult, Does.Contain(".State:")); // Empty abbreviation will be used, resulting in empty state display
            });

            // Case 3: Negative ID with null values
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _sut.Id = -42;
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
    }
}
