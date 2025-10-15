using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NUnit.Framework;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Interfaces.Type;
using OrganizerCompanion.Core.Models.Type;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    [TestFixture]
    internal class MXAddressDTOShould
    {
        private MXAddressDTO _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new MXAddressDTO();
        }

        [Test, Category("DataTransferObjects")]
        public void DefaultConstructor_ShouldCreateMXAddressDTOWithDefaultValues()
        {
            // Arrange & Act
            _sut = new MXAddressDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.Street, Is.Null);
                Assert.That(_sut.Neighborhood, Is.Null);
                Assert.That(_sut.PostalCode, Is.Null);
                Assert.That(_sut.City, Is.Null);
                Assert.That(_sut.State, Is.Null);
                Assert.That(_sut.Country, Is.Null);
                Assert.That(_sut.Type, Is.Null);
                Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));
                Assert.That(_sut.DateModified, Is.Null);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldGetAndSetValue()
        {
            // Arrange
            int expectedId = 123;

            // Act
            _sut.Id = expectedId;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(expectedId));
        }

        [Test, Category("DataTransferObjects")]
        public void Street_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedStreet = "Av. Reforma 123";

            // Act
            _sut.Street = expectedStreet;

            // Assert
            Assert.That(_sut.Street, Is.EqualTo(expectedStreet));
        }

        [Test, Category("DataTransferObjects")]
        public void Street_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.Street = null;

            // Assert
            Assert.That(_sut.Street, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Street_ShouldAcceptVariousStreetFormats()
        {
            // Arrange
            var streetFormats = new[]
            {
                "Calle Madero 45",
                "Av. Insurgentes Sur 1234",
                "Blvd. Manuel Ávila Camacho 567",
                "Calzada de Tlalpan 890",
                "Periférico Sur 123-A",
                "Eje Central Lázaro Cárdenas 456"
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var street in streetFormats)
                {
                    _sut.Street = street;
                    Assert.That(_sut.Street, Is.EqualTo(street));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Neighborhood_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedNeighborhood = "Polanco";

            // Act
            _sut.Neighborhood = expectedNeighborhood;

            // Assert
            Assert.That(_sut.Neighborhood, Is.EqualTo(expectedNeighborhood));
        }

        [Test, Category("DataTransferObjects")]
        public void Neighborhood_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.Neighborhood = null;

            // Assert
            Assert.That(_sut.Neighborhood, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Neighborhood_ShouldAcceptVariousNeighborhoodFormats()
        {
            // Arrange
            var neighborhoods = new[]
            {
                "Roma Norte",
                "Condesa",
                "Santa María la Ribera",
                "Del Valle Centro",
                "Doctores",
                "Nápoles"
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var neighborhood in neighborhoods)
                {
                    _sut.Neighborhood = neighborhood;
                    Assert.That(_sut.Neighborhood, Is.EqualTo(neighborhood));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void PostalCode_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedPostalCode = "11560";

            // Act
            _sut.PostalCode = expectedPostalCode;

            // Assert
            Assert.That(_sut.PostalCode, Is.EqualTo(expectedPostalCode));
        }

        [Test, Category("DataTransferObjects")]
        public void PostalCode_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.PostalCode = null;

            // Assert
            Assert.That(_sut.PostalCode, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void PostalCode_ShouldAcceptVariousPostalCodeFormats()
        {
            // Arrange
            var postalCodes = new[]
            {
                "01000",
                "11560",
                "06700",
                "03100",
                "08500",
                "04510"
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var postalCode in postalCodes)
                {
                    _sut.PostalCode = postalCode;
                    Assert.That(_sut.PostalCode, Is.EqualTo(postalCode));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void City_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedCity = "Ciudad de México";

            // Act
            _sut.City = expectedCity;

            // Assert
            Assert.That(_sut.City, Is.EqualTo(expectedCity));
        }

        [Test, Category("DataTransferObjects")]
        public void City_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.City = null;

            // Assert
            Assert.That(_sut.City, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void City_ShouldAcceptVariousMexicanCities()
        {
            // Arrange
            var cities = new[]
            {
                "Ciudad de México",
                "Guadalajara",
                "Monterrey",
                "Puebla",
                "Tijuana",
                "León"
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var city in cities)
                {
                    _sut.City = city;
                    Assert.That(_sut.City, Is.EqualTo(city));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void State_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedState = new MXState { Name = "Distrito Federal", Abbreviation = "DF" };

            // Act
            _sut.State = expectedState;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.State, Is.EqualTo(expectedState));
                Assert.That(_sut.State.Name, Is.EqualTo("Distrito Federal"));
                Assert.That(_sut.State.Abbreviation, Is.EqualTo("DF"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void State_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.State = null;

            // Assert
            Assert.That(_sut.State, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedCountry = "México";

            // Act
            _sut.Country = expectedCountry;

            // Assert
            Assert.That(_sut.Country, Is.EqualTo(expectedCountry));
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.Country = null;

            // Assert
            Assert.That(_sut.Country, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldGetAndSetValue()
        {
            // Arrange
            OrganizerCompanion.Core.Enums.Types expectedType = OrganizerCompanion.Core.Enums.Types.Home;

            // Act
            _sut.Type = expectedType;

            // Assert
            Assert.That(_sut.Type, Is.EqualTo(expectedType));
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.Type = null;

            // Assert
            Assert.That(_sut.Type, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldAcceptAllEnumValues()
        {
            // Arrange
            var enumValues = new[] { 
                OrganizerCompanion.Core.Enums.Types.Home,
                OrganizerCompanion.Core.Enums.Types.Work,
                OrganizerCompanion.Core.Enums.Types.Cell,
                OrganizerCompanion.Core.Enums.Types.Fax,
                OrganizerCompanion.Core.Enums.Types.Billing,
                OrganizerCompanion.Core.Enums.Types.Other
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var enumValue in enumValues)
                {
                    _sut.Type = enumValue;
                    Assert.That(_sut.Type, Is.EqualTo(enumValue));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.IsCast; });
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.IsCast = true; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastId; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.CastId = 123; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastType; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.CastType = "TestType"; });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldGetAndSetValue()
        {
            // Arrange
            DateTime expectedDateCreated = new DateTime(2023, 10, 15, 14, 30, 0);

            // Act
            _sut.DateCreated = expectedDateCreated;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(expectedDateCreated));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldGetAndSetValue()
        {
            // Arrange
            DateTime expectedDateModified = new DateTime(2023, 10, 16, 10, 15, 0);

            // Act
            _sut.DateModified = expectedDateModified;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(expectedDateModified));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.DateModified = null;

            // Assert
            Assert.That(_sut.DateModified, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.Cast<MockDomainEntity>(); });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.ToJson(); });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(MXAddressDTO).GetProperty(nameof(MXAddressDTO.Id));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Street_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(MXAddressDTO).GetProperty(nameof(MXAddressDTO.Street));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Neighborhood_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(MXAddressDTO).GetProperty(nameof(MXAddressDTO.Neighborhood));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void PostalCode_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(MXAddressDTO).GetProperty(nameof(MXAddressDTO.PostalCode));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void City_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(MXAddressDTO).GetProperty(nameof(MXAddressDTO.City));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void State_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(MXAddressDTO).GetProperty(nameof(MXAddressDTO.State));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(MXAddressDTO).GetProperty(nameof(MXAddressDTO.Country));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(MXAddressDTO).GetProperty(nameof(MXAddressDTO.Type));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(MXAddressDTO).GetProperty(nameof(MXAddressDTO.DateCreated));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(MXAddressDTO).GetProperty(nameof(MXAddressDTO.DateModified));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void MXAddressDTO_ShouldImplementIMXAddressDTO()
        {
            // Arrange & Act
            var mxAddressDTO = new MXAddressDTO();

            // Assert
            Assert.That(mxAddressDTO, Is.InstanceOf<IMXAddressDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void MXAddressDTO_ShouldImplementIDomainEntity()
        {
            // Arrange & Act
            var mxAddressDTO = new MXAddressDTO();

            // Assert
            Assert.That(mxAddressDTO, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void MXAddressDTO_Properties_ShouldBeSettableInChain()
        {
            // Arrange
            var testDateCreated = new DateTime(2023, 10, 15, 14, 30, 0);
            var testDateModified = new DateTime(2023, 10, 16, 10, 15, 0);

            // Act
            var mxAddressDTO = new MXAddressDTO
            {
                Id = 999,
                Street = "Av. Insurgentes Sur 1234",
                Neighborhood = "Roma Norte",
                PostalCode = "06700",
                City = "Ciudad de México",
                State = new MXState { Name = "Distrito Federal", Abbreviation = "DF" },
                Country = "México",
                Type = OrganizerCompanion.Core.Enums.Types.Work,
                DateCreated = testDateCreated,
                DateModified = testDateModified
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(mxAddressDTO.Id, Is.EqualTo(999));
                Assert.That(mxAddressDTO.Street, Is.EqualTo("Av. Insurgentes Sur 1234"));
                Assert.That(mxAddressDTO.Neighborhood, Is.EqualTo("Roma Norte"));
                Assert.That(mxAddressDTO.PostalCode, Is.EqualTo("06700"));
                Assert.That(mxAddressDTO.City, Is.EqualTo("Ciudad de México"));
                Assert.That(mxAddressDTO.State?.Name, Is.EqualTo("Distrito Federal"));
                Assert.That(mxAddressDTO.Country, Is.EqualTo("México"));
                Assert.That(mxAddressDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(mxAddressDTO.DateCreated, Is.EqualTo(testDateCreated));
                Assert.That(mxAddressDTO.DateModified, Is.EqualTo(testDateModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void JsonPropertyName_Attributes_ShouldBePresent()
        {
            // Arrange
            var properties = new Dictionary<string, string>
            {
                { nameof(MXAddressDTO.Id), "id" },
                { nameof(MXAddressDTO.Street), "street" },
                { nameof(MXAddressDTO.Neighborhood), "neighborhood" },
                { nameof(MXAddressDTO.PostalCode), "postalCode" },
                { nameof(MXAddressDTO.City), "city" },
                { nameof(MXAddressDTO.State), "state" },
                { nameof(MXAddressDTO.Country), "country" },
                { nameof(MXAddressDTO.Type), "type" },
                { nameof(MXAddressDTO.DateCreated), "dateCreated" },
                { nameof(MXAddressDTO.DateModified), "dateModified" }
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var kvp in properties)
                {
                    var property = typeof(MXAddressDTO).GetProperty(kvp.Key);
                    var jsonAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
                    Assert.That(jsonAttribute, Is.Not.Null, $"Property {kvp.Key} should have JsonPropertyName attribute");
                    Assert.That(jsonAttribute?.Name, Is.EqualTo(kvp.Value), $"Property {kvp.Key} should have JsonPropertyName '{kvp.Value}'");
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Interface_Properties_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var properties = new[]
            {
                nameof(MXAddressDTO.IsCast),
                nameof(MXAddressDTO.CastId),
                nameof(MXAddressDTO.CastType)
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var propertyName in properties)
                {
                    var property = typeof(MXAddressDTO).GetProperty(propertyName);
                    var jsonIgnoreAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();
                    Assert.That(jsonIgnoreAttribute, Is.Not.Null, $"Property {propertyName} should have JsonIgnore attribute");
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void State_ShouldAcceptDifferentINationalSubdivisionImplementations()
        {
            // Arrange
            var mockState = new MockNationalSubdivision { Name = "Mock State", Abbreviation = "MS" };

            // Act
            _sut.State = mockState;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.State, Is.EqualTo(mockState));
                Assert.That(_sut.State, Is.InstanceOf<INationalSubdivision>());
                Assert.That(_sut.State.Name, Is.EqualTo("Mock State"));
                Assert.That(_sut.State.Abbreviation, Is.EqualTo("MS"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void StringProperties_ShouldAcceptEmptyStrings()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                _sut.Street = "";
                Assert.That(_sut.Street, Is.EqualTo(""));

                _sut.Neighborhood = "";
                Assert.That(_sut.Neighborhood, Is.EqualTo(""));

                _sut.PostalCode = "";
                Assert.That(_sut.PostalCode, Is.EqualTo(""));

                _sut.City = "";
                Assert.That(_sut.City, Is.EqualTo(""));

                _sut.Country = "";
                Assert.That(_sut.Country, Is.EqualTo(""));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void StringProperties_ShouldAcceptWhitespace()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                _sut.Street = " Calle con espacios ";
                Assert.That(_sut.Street, Is.EqualTo(" Calle con espacios "));

                _sut.Neighborhood = "  Colonia  ";
                Assert.That(_sut.Neighborhood, Is.EqualTo("  Colonia  "));

                _sut.PostalCode = " 12345 ";
                Assert.That(_sut.PostalCode, Is.EqualTo(" 12345 "));

                _sut.City = "  Ciudad  ";
                Assert.That(_sut.City, Is.EqualTo("  Ciudad  "));

                _sut.Country = "  País  ";
                Assert.That(_sut.Country, Is.EqualTo("  País  "));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void StringProperties_ShouldAcceptSpecialCharacters()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                _sut.Street = "Calle José María Morelos y Pavón #123-B";
                Assert.That(_sut.Street, Is.EqualTo("Calle José María Morelos y Pavón #123-B"));

                _sut.Neighborhood = "San José de los Ñáñez";
                Assert.That(_sut.Neighborhood, Is.EqualTo("San José de los Ñáñez"));

                _sut.City = "San Luis Potosí";
                Assert.That(_sut.City, Is.EqualTo("San Luis Potosí"));

                _sut.Country = "México";
                Assert.That(_sut.Country, Is.EqualTo("México"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Properties_ShouldAllowMultipleReassignments()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                // Test Street reassignments
                _sut.Street = "First Street";
                Assert.That(_sut.Street, Is.EqualTo("First Street"));
                _sut.Street = "Second Street";
                Assert.That(_sut.Street, Is.EqualTo("Second Street"));

                // Test Neighborhood reassignments
                _sut.Neighborhood = "First Neighborhood";
                Assert.That(_sut.Neighborhood, Is.EqualTo("First Neighborhood"));
                _sut.Neighborhood = "Second Neighborhood";
                Assert.That(_sut.Neighborhood, Is.EqualTo("Second Neighborhood"));

                // Test Type reassignments
                _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
                Assert.That(_sut.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
                _sut.Type = OrganizerCompanion.Core.Enums.Types.Work;
                Assert.That(_sut.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
            });
        }

        #region Mock Classes
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime DateCreated { get; }
            public DateTime? DateModified { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => throw new NotImplementedException();
        }

        private class MockNationalSubdivision : INationalSubdivision
        {
            public string? Name { get; set; }
            public string? Abbreviation { get; set; }
        }
        #endregion
    }
}
