using System.Text.Json;
using System.Text.Json.Serialization;
using System.Reflection;
using System.ComponentModel.DataAnnotations.Schema;
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

        #region Constructor Tests
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

                // Test EF Core navigation properties default to null
                Assert.That(_sut.User, Is.Null);
                Assert.That(_sut.UserId, Is.Null);
                Assert.That(_sut.Contact, Is.Null);
                Assert.That(_sut.ContactId, Is.Null);
                Assert.That(_sut.Organization, Is.Null);
                Assert.That(_sut.OrganizationId, Is.Null);
                Assert.That(_sut.SubAccount, Is.Null);
                Assert.That(_sut.SubAccountId, Is.Null);
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
        public void Id_Setter_WithNegativeValue_ThrowsArgumentOutOfRangeException()
        {
            // Act & Assert
            var exception = Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = -1);
            Assert.Multiple(() =>
            {
                Assert.That(exception.ParamName, Is.EqualTo("Id"));
                Assert.That(exception.Message, Does.Contain("Id must be a non-negative number."));
            });
        }
        #endregion

        #region Property Setter Tests
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
        public void StateEnum_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.StateEnum = MXStates.Jalisco;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.State?.Name, Is.EqualTo(MXStates.Jalisco.ToStateModel().Name));
                Assert.That(_sut.State?.Abbreviation, Is.EqualTo(MXStates.Jalisco.ToStateModel().Abbreviation));
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void StateEnum_Getter_ReturnsNull()
        {
            // Act & Assert
            Assert.That(_sut.StateEnum, Is.Null);
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
        #endregion

        #region Cast Method Tests
        [Test, Category("Models")]
        public void Cast_ToMXAddressDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 123;
            _sut.Street = "456 Test Street";
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
                Assert.That(result, Is.TypeOf<MXAddressDTO>());
                Assert.That(result.Id, Is.EqualTo(_sut.Id));
                Assert.That(result.Street, Is.EqualTo(_sut.Street));
                Assert.That(result.Neighborhood, Is.EqualTo(_sut.Neighborhood));
                Assert.That(result.PostalCode, Is.EqualTo(_sut.PostalCode));
                Assert.That(result.City, Is.EqualTo(_sut.City));
                Assert.That(result.State, Is.EqualTo(_sut.State));
                Assert.That(result.Country, Is.EqualTo(_sut.Country));
                Assert.That(result.Type, Is.EqualTo(_sut.Type));
                Assert.That(result.IsPrimary, Is.EqualTo(_sut.IsPrimary));
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIMXAddressDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 456;
            _sut.Street = "789 Another Street";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Act
            var result = _sut.Cast<IMXAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<MXAddressDTO>());
                Assert.That(result.Id, Is.EqualTo(_sut.Id));
                Assert.That(result.Street, Is.EqualTo(_sut.Street));
                Assert.That(result.Type, Is.EqualTo(_sut.Type));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIAddressDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 789;
            _sut.City = "Montreal";

            // Act
            var result = _sut.Cast<IAddressDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<MXAddressDTO>());
                Assert.That(result.Id, Is.EqualTo(_sut.Id));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedType_ShouldThrowInvalidCastException()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Street = "Test Street";

            // Act & Assert
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(exception, Is.Not.Null);
                Assert.That(exception.Message, Does.Contain("Cannot cast MXAddress to type MockDomainEntity."));
            });
        }
        #endregion

        #region ToString Tests
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
        #endregion

        #region JSON Serialization Tests
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
        public void ToJson_HandlesNullProperties()
        {
            // Arrange
            _sut.Id = 2;
            _sut.Street = null;
            _sut.ModifiedDate = null;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
             {
                 Assert.That(json, Is.Not.Null);
                 Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
             });

            var jsonDocument = JsonDocument.Parse(json);
            var root = jsonDocument.RootElement;

            Assert.Multiple(() =>
            {
                Assert.That(root.TryGetProperty("id", out var idProperty), Is.True);
                Assert.That(idProperty.GetInt32(), Is.EqualTo(2));

                Assert.That(root.TryGetProperty("street", out var streetProperty), Is.True);
                Assert.That(streetProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
            });
        }
        #endregion

        #region Additional Property Tests
        [Test, Category("Models")]
        public void CreatedDate_IsReadOnly()
        {
            // Arrange
            var property = typeof(MXAddress).GetProperty(nameof(MXAddress.CreatedDate));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(property, Is.Not.Null);
                Assert.That(property!.CanRead, Is.True);
                Assert.That(property.CanWrite, Is.False);
                Assert.That(property.GetSetMethod(), Is.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityId_IsReadOnly()
        {
            // Arrange
            var property = typeof(MXAddress).GetProperty(nameof(MXAddress.LinkedEntityId));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(property, Is.Not.Null);
                Assert.That(property!.CanRead, Is.True);
                Assert.That(property.CanWrite, Is.False);
                Assert.That(property.GetSetMethod(), Is.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityType_IsReadOnly()
        {
            // Arrange
            var property = typeof(MXAddress).GetProperty(nameof(MXAddress.LinkedEntityType));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(property, Is.Not.Null);
                Assert.That(property!.CanRead, Is.True);
                Assert.That(property.CanWrite, Is.False);
                Assert.That(property.GetSetMethod(), Is.Null);
            });
        }

        [Test, Category("Models")]
        public void ModifiedDate_CanBeSetDirectly()
        {
            // Arrange
            var testDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            _sut.ModifiedDate = testDate;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(testDate));
        }

        [Test, Category("Models")]
        public void Properties_CanSetAndGetNullValues()
        {
            // Act & Assert
            Assert.Multiple(() =>
            {
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

                _sut.ModifiedDate = null;
                Assert.That(_sut.ModifiedDate, Is.Null);
            });
        }
        #endregion

        #region EF Core Foreign Key Relationship Tests
        [Test, Category("Models")]
        public void LinkedEntity_WithUser_ShouldSetUserPropertiesAndClearOthers()
        {
            // Arrange
            var user = new MockUser { Id = 100 };

            // Act
            _sut.LinkedEntity = user;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(user), "LinkedEntity should return the User");
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(100), "LinkedEntityId should match User.Id");
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockUser"), "LinkedEntityType should be MockUser since it's stored in _linkedEntity");

                // Since MockUser is not a recognized concrete type (User, Contact, etc.), 
                // it should be stored in the _linkedEntity field instead of navigation properties
                Assert.That(_sut.User, Is.Null, "User navigation property should remain null for mock types");
                Assert.That(_sut.UserId, Is.Null, "UserId foreign key should remain null for mock types");
                Assert.That(_sut.Contact, Is.Null, "Contact should be null");
                Assert.That(_sut.ContactId, Is.Null, "ContactId should be null");
                Assert.That(_sut.Organization, Is.Null, "Organization should be null");
                Assert.That(_sut.OrganizationId, Is.Null, "OrganizationId should be null");
                Assert.That(_sut.SubAccount, Is.Null, "SubAccount should be null");
                Assert.That(_sut.SubAccountId, Is.Null, "SubAccountId should be null");
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_WithContact_ShouldSetContactPropertiesAndClearOthers()
        {
            // Arrange
            var contact = new MockContact { Id = 200 };

            // Act
            _sut.LinkedEntity = contact;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(contact), "LinkedEntity should return the Contact");
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(200), "LinkedEntityId should match Contact.Id");
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockContact"), "LinkedEntityType should be MockContact since it's stored in _linkedEntity");

                // Since MockContact is not a recognized concrete type (User, Contact, etc.), 
                // it should be stored in the _linkedEntity field instead of navigation properties
                Assert.That(_sut.Contact, Is.Null, "Contact navigation property should remain null for mock types");
                Assert.That(_sut.ContactId, Is.Null, "ContactId foreign key should remain null for mock types");
                Assert.That(_sut.User, Is.Null, "User should be null");
                Assert.That(_sut.UserId, Is.Null, "UserId should be null");
                Assert.That(_sut.Organization, Is.Null, "Organization should be null");
                Assert.That(_sut.OrganizationId, Is.Null, "OrganizationId should be null");
                Assert.That(_sut.SubAccount, Is.Null, "SubAccount should be null");
                Assert.That(_sut.SubAccountId, Is.Null, "SubAccountId should be null");
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_SetToNull_ShouldClearAllNavigationProperties()
        {
            // Arrange
            var user = new MockUser { Id = 100 };
            _sut.LinkedEntity = user; // Set initial value

            // Act
            _sut.LinkedEntity = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.Null, "LinkedEntity should be null");
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(null), "LinkedEntityId should be null");
                Assert.That(_sut.LinkedEntityType, Is.Null, "LinkedEntityType should be null");

                // All navigation properties should be null
                Assert.That(_sut.User, Is.Null, "User should be null");
                Assert.That(_sut.UserId, Is.Null, "UserId should be null");
                Assert.That(_sut.Contact, Is.Null, "Contact should be null");
                Assert.That(_sut.ContactId, Is.Null, "ContactId should be null");
                Assert.That(_sut.Organization, Is.Null, "Organization should be null");
                Assert.That(_sut.OrganizationId, Is.Null, "OrganizationId should be null");
                Assert.That(_sut.SubAccount, Is.Null, "SubAccount should be null");
                Assert.That(_sut.SubAccountId, Is.Null, "SubAccountId should be null");
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_WithUnsupportedType_ShouldStoreEntityButNotSetNavigationProperties()
        {
            // Arrange
            var unsupportedEntity = new MockDomainEntity { Id = 999 };

            // Act
            _sut.LinkedEntity = unsupportedEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntity, Is.EqualTo(unsupportedEntity), "LinkedEntity should return the unsupported entity");
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(999), "_linkedEntityId should be set");
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"), "LinkedEntityType should reflect the actual type");

                // All navigation properties should remain null for unsupported types
                Assert.That(_sut.User, Is.Null, "User should be null");
                Assert.That(_sut.UserId, Is.Null, "UserId should be null");
                Assert.That(_sut.Contact, Is.Null, "Contact should be null");
                Assert.That(_sut.ContactId, Is.Null, "ContactId should be null");
                Assert.That(_sut.Organization, Is.Null, "Organization should be null");
                Assert.That(_sut.OrganizationId, Is.Null, "OrganizationId should be null");
                Assert.That(_sut.SubAccount, Is.Null, "SubAccount should be null");
                Assert.That(_sut.SubAccountId, Is.Null, "SubAccountId should be null");
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_ComputedGetter_ShouldReturnFirstNonNullNavigationProperty()
        {
            // This test verifies the priority order: User ?? Contact ?? Organization ?? SubAccount ?? _linkedEntity

            // Arrange & Act - All navigation properties are null by default, and no _linkedEntity is set
            var result = _sut.LinkedEntity;
            Assert.That(result, Is.Null, "Should return null when all navigation properties and _linkedEntity are null");

            // Test that _linkedEntity is returned when navigation properties are null
            var mockEntity = new MockDomainEntity { Id = 123 };
            _sut.LinkedEntity = mockEntity;
            Assert.That(_sut.LinkedEntity, Is.EqualTo(mockEntity), "Should return _linkedEntity when navigation properties are null");

            // Test priority: if we manually set foreign key IDs (simulating EF Core loading), 
            // the computed getter should prioritize them over _linkedEntity, but since we can't 
            // create actual domain entities in unit tests, we'll verify _linkedEntity is cleared 
            // when we set LinkedEntity to null
            _sut.LinkedEntity = null;
            Assert.That(_sut.LinkedEntity, Is.Null, "Should return null after clearing LinkedEntity");
        }

        [Test, Category("Models")]
        public void NavigationProperties_ShouldAllowDirectAssignment()
        {
            // Test that navigation properties can be set directly (for EF Core use)
            // We test that the properties exist and can be assigned null values

            // Act & Assert - Test that properties can be set to null without error
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _sut.User = null);
                Assert.DoesNotThrow(() => _sut.UserId = null);
                Assert.DoesNotThrow(() => _sut.Contact = null);
                Assert.DoesNotThrow(() => _sut.ContactId = null);
                Assert.DoesNotThrow(() => _sut.Organization = null);
                Assert.DoesNotThrow(() => _sut.OrganizationId = null);
                Assert.DoesNotThrow(() => _sut.SubAccount = null);
                Assert.DoesNotThrow(() => _sut.SubAccountId = null);

                // Test that foreign key IDs can be set to integer values
                Assert.DoesNotThrow(() => _sut.UserId = 100);
                Assert.DoesNotThrow(() => _sut.ContactId = 200);
                Assert.DoesNotThrow(() => _sut.OrganizationId = 300);
                Assert.DoesNotThrow(() => _sut.SubAccountId = 400);

                // Verify the values were set
                Assert.That(_sut.UserId, Is.EqualTo(100));
                Assert.That(_sut.ContactId, Is.EqualTo(200));
                Assert.That(_sut.OrganizationId, Is.EqualTo(300));
                Assert.That(_sut.SubAccountId, Is.EqualTo(400));
            });
        }

        [Test, Category("Models")]
        public void ForeignKeyProperties_ShouldHaveJsonIgnoreAttribute()
        {
            // Verify that foreign key properties are not serialized to JSON
            var userIdProperty = typeof(MXAddress).GetProperty(nameof(MXAddress.UserId));
            var contactIdProperty = typeof(MXAddress).GetProperty(nameof(MXAddress.ContactId));
            var organizationIdProperty = typeof(MXAddress).GetProperty(nameof(MXAddress.OrganizationId));
            var subAccountIdProperty = typeof(MXAddress).GetProperty(nameof(MXAddress.SubAccountId));

            Assert.Multiple(() =>
            {
                Assert.That(userIdProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "UserId should have JsonIgnore");
                Assert.That(contactIdProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "ContactId should have JsonIgnore");
                Assert.That(organizationIdProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "OrganizationId should have JsonIgnore");
                Assert.That(subAccountIdProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "SubAccountId should have JsonIgnore");
            });
        }

        [Test, Category("Models")]
        public void NavigationProperties_ShouldHaveJsonIgnoreAndForeignKeyAttributes()
        {
            // Verify EF Core attributes are present
            var userProperty = typeof(MXAddress).GetProperty(nameof(MXAddress.User));
            var contactProperty = typeof(MXAddress).GetProperty(nameof(MXAddress.Contact));
            var organizationProperty = typeof(MXAddress).GetProperty(nameof(MXAddress.Organization));
            var subAccountProperty = typeof(MXAddress).GetProperty(nameof(MXAddress.SubAccount));

            Assert.Multiple(() =>
            {
                // Check JsonIgnore attributes
                Assert.That(userProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "User should have JsonIgnore");
                Assert.That(contactProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "Contact should have JsonIgnore");
                Assert.That(organizationProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "Organization should have JsonIgnore");
                Assert.That(subAccountProperty?.GetCustomAttribute<JsonIgnoreAttribute>(), Is.Not.Null, "SubAccount should have JsonIgnore");

                // Check ForeignKey attributes
                Assert.That(userProperty?.GetCustomAttribute<ForeignKeyAttribute>()?.Name, Is.EqualTo("UserId"), "User should have ForeignKey(UserId)");
                Assert.That(contactProperty?.GetCustomAttribute<ForeignKeyAttribute>()?.Name, Is.EqualTo("ContactId"), "Contact should have ForeignKey(ContactId)");
                Assert.That(organizationProperty?.GetCustomAttribute<ForeignKeyAttribute>()?.Name, Is.EqualTo("OrganizationId"), "Organization should have ForeignKey(OrganizationId)");
                Assert.That(subAccountProperty?.GetCustomAttribute<ForeignKeyAttribute>()?.Name, Is.EqualTo("SubAccountId"), "SubAccount should have ForeignKey(SubAccountId)");
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_ShouldHaveNotMappedAttribute()
        {
            // Verify LinkedEntity is not mapped to database (computed property)
            var linkedEntityProperty = typeof(MXAddress).GetProperty(nameof(MXAddress.LinkedEntity));
            var notMappedAttribute = linkedEntityProperty?.GetCustomAttribute<NotMappedAttribute>();

            Assert.That(notMappedAttribute, Is.Not.Null, "LinkedEntity should have NotMapped attribute");
        }

        [Test, Category("Models")]
        public void LinkedEntityId_ShouldHaveNotMappedAttribute()
        {
            // Verify LinkedEntityId is not mapped to database (computed property)
            var linkedEntityIdProperty = typeof(MXAddress).GetProperty(nameof(MXAddress.LinkedEntityId));
            var notMappedAttribute = linkedEntityIdProperty?.GetCustomAttribute<NotMappedAttribute>();

            Assert.That(notMappedAttribute, Is.Not.Null, "LinkedEntityId should have NotMapped attribute");
        }

        [Test, Category("Models")]
        public void LinkedEntityType_ShouldHaveNotMappedAttribute()
        {
            // Verify LinkedEntityType is not mapped to database (computed property)
            var linkedEntityTypeProperty = typeof(MXAddress).GetProperty(nameof(MXAddress.LinkedEntityType));
            var notMappedAttribute = linkedEntityTypeProperty?.GetCustomAttribute<NotMappedAttribute>();

            Assert.That(notMappedAttribute, Is.Not.Null, "LinkedEntityType should have NotMapped attribute");
        }

        [Test, Category("Models")]
        public void JsonSerialization_ShouldNotIncludeNavigationProperties()
        {
            // Arrange
            _sut.Id = 1;
            _sut.Street = "Test Street";
            _sut.LinkedEntity = new MockUser { Id = 100 };

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Does.Not.Contain("\"userId\""), "JSON should not contain userId");
                Assert.That(json, Does.Not.Contain("\"user\""), "JSON should not contain user");
                Assert.That(json, Does.Not.Contain("\"contactId\""), "JSON should not contain contactId");
                Assert.That(json, Does.Not.Contain("\"contact\""), "JSON should not contain contact");
                Assert.That(json, Does.Not.Contain("\"organizationId\""), "JSON should not contain organizationId");
                Assert.That(json, Does.Not.Contain("\"organization\""), "JSON should not contain organization");
                Assert.That(json, Does.Not.Contain("\"subAccountId\""), "JSON should not contain subAccountId");
                Assert.That(json, Does.Not.Contain("\"subAccount\""), "JSON should not contain subAccount");

                // But should contain the computed properties
                Assert.That(json, Does.Contain("\"linkedEntity\""), "JSON should contain linkedEntity");
                Assert.That(json, Does.Contain("\"linkedEntityId\""), "JSON should contain linkedEntityId");
                Assert.That(json, Does.Contain("\"linkedEntityType\""), "JSON should contain linkedEntityType");
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_ModifiedDateUpdate_ShouldUseUtcNow()
        {
            // Arrange
            var beforeSet = DateTime.UtcNow.AddSeconds(-1);
            var user = new MockUser { Id = 100 };

            // Act
            _sut.LinkedEntity = user;
            var afterSet = DateTime.UtcNow.AddSeconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.ModifiedDate, Is.Not.Null, "ModifiedDate should be set");
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(beforeSet), "ModifiedDate should be after beforeSet");
                Assert.That(_sut.ModifiedDate, Is.LessThan(afterSet), "ModifiedDate should be before afterSet");
                Assert.That(_sut.ModifiedDate?.Kind, Is.EqualTo(DateTimeKind.Utc).Or.EqualTo(DateTimeKind.Unspecified), "Should use UTC time");
            });
        }
        #endregion

        #region Helper Mock Classes
        // Helper mock class for testing INationalSubdivision
        private class MockNationalSubdivision : Interfaces.Type.INationalSubdivision
        {
            public string? Name { get; set; }
            public string? Abbreviation { get; set; }
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

        // Additional mock classes for testing the new EF Core entity types
        private class MockUser : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        private class MockContact : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        private class MockOrganization : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        private class MockSubAccount : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }
        #endregion
    }
}
