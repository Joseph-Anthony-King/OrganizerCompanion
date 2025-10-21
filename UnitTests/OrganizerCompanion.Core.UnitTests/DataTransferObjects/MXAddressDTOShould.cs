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
                Assert.That(_sut.IsPrimary, Is.False);
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
                OrganizerCompanion.Core.Enums.Types.Mobil,
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
        public void IsPrimary_ShouldGetAndSetValue()
        {
            // Arrange
            bool expectedIsPrimary = true;

            // Act
            _sut.IsPrimary = expectedIsPrimary;

            // Assert
            Assert.That(_sut.IsPrimary, Is.EqualTo(expectedIsPrimary));
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_ShouldAcceptTrueValue()
        {
            // Arrange & Act
            _sut.IsPrimary = true;

            // Assert
            Assert.That(_sut.IsPrimary, Is.True);
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_ShouldAcceptFalseValue()
        {
            // Arrange & Act
            _sut.IsPrimary = false;

            // Assert
            Assert.That(_sut.IsPrimary, Is.False);
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_DefaultValue_ShouldBeFalse()
        {
            // Arrange & Act
            var mxAddressDTO = new MXAddressDTO();

            // Assert
            Assert.That(mxAddressDTO.IsPrimary, Is.False);
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_ShouldSupportMultipleReassignments()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                _sut.IsPrimary = true;
                Assert.That(_sut.IsPrimary, Is.True);
                
                _sut.IsPrimary = false;
                Assert.That(_sut.IsPrimary, Is.False);
                
                _sut.IsPrimary = true;
                Assert.That(_sut.IsPrimary, Is.True);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldGetAndSetValue()
        {
            // Arrange
            DateTime expectedDateCreated = new(2023, 10, 15, 14, 30, 0);

            // Act
            _sut.DateCreated = expectedDateCreated;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(expectedDateCreated));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldGetAndSetValue()
        {
            // Arrange
            DateTime expectedDateModified = new(2023, 10, 16, 10, 15, 0);

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
            // Arrange, Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.Cast<MockDomainEntity>(); });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldThrowNotImplementedException()
        {
            // Arrange, Act & Assert
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
        public void IsPrimary_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(MXAddressDTO).GetProperty(nameof(MXAddressDTO.IsPrimary));

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
                IsPrimary = true,
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
                Assert.That(mxAddressDTO.IsPrimary, Is.True);
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
                { nameof(MXAddressDTO.IsPrimary), "isPrimary" },
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
            // Arrange, Act & Assert
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
            // Arrange, Act & Assert
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
            // Arrange, Act & Assert
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
            // Arrange, Act & Assert
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

                // Test IsPrimary reassignments
                _sut.IsPrimary = true;
                Assert.That(_sut.IsPrimary, Is.True);
                _sut.IsPrimary = false;
                Assert.That(_sut.IsPrimary, Is.False);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _sut.Id = int.MinValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _sut.Id = int.MaxValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

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
                    _sut.Id = id;
                    Assert.That(_sut.Id, Is.EqualTo(id));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _sut.DateCreated = DateTime.MinValue;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _sut.DateCreated = DateTime.MaxValue;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _sut.DateModified = DateTime.MinValue;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _sut.DateModified = DateTime.MaxValue;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldMaintainPrecision()
        {
            // Arrange
            var preciseDate = new DateTime(2023, 12, 25, 14, 30, 45, 123);

            // Act
            _sut.DateCreated = preciseDate;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(preciseDate));
            Assert.That(_sut.DateCreated.Millisecond, Is.EqualTo(123));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldMaintainPrecision()
        {
            // Arrange
            var preciseDate = new DateTime(2023, 11, 15, 9, 45, 30, 456);

            // Act
            _sut.DateModified = preciseDate;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(preciseDate));
            Assert.That(_sut.DateModified?.Millisecond, Is.EqualTo(456));
        }

        [Test, Category("DataTransferObjects")]
        public void Street_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var unicodeStreet = "Calle 测试街 (Test Street) - Улица Тест 🏠";

            // Act
            _sut.Street = unicodeStreet;

            // Assert
            Assert.That(_sut.Street, Is.EqualTo(unicodeStreet));
        }

        [Test, Category("DataTransferObjects")]
        public void Street_ShouldAcceptVeryLongString()
        {
            // Arrange
            var longStreet = new string('S', 10000) + " Street";

            // Act
            _sut.Street = longStreet;

            // Assert
            Assert.That(_sut.Street, Is.EqualTo(longStreet));
            Assert.That(_sut.Street?.Length, Is.EqualTo(10007));
        }

        [Test, Category("DataTransferObjects")]
        public void Neighborhood_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var unicodeNeighborhood = "Colonia 测试区 (Test Neighborhood) - Район Тест 🏘️";

            // Act
            _sut.Neighborhood = unicodeNeighborhood;

            // Assert
            Assert.That(_sut.Neighborhood, Is.EqualTo(unicodeNeighborhood));
        }

        [Test, Category("DataTransferObjects")]
        public void PostalCode_ShouldAcceptAlphanumericFormats()
        {
            // Arrange
            var alphanumericCodes = new[]
            {
                "12345",
                "ABCDE",
                "A1B2C",
                "12A34",
                "AB123"
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var code in alphanumericCodes)
                {
                    _sut.PostalCode = code;
                    Assert.That(_sut.PostalCode, Is.EqualTo(code));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void City_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var unicodeCity = "Ciudad 测试市 (Test City) - Город Тест 🌆";

            // Act
            _sut.City = unicodeCity;

            // Assert
            Assert.That(_sut.City, Is.EqualTo(unicodeCity));
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var unicodeCountry = "País 测试国 (Test Country) - Страна Тест 🇲🇽";

            // Act
            _sut.Country = unicodeCountry;

            // Assert
            Assert.That(_sut.Country, Is.EqualTo(unicodeCountry));
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldSupportCastingFromInt()
        {
            // Arrange
            var enumValue = (OrganizerCompanion.Core.Enums.Types)0;

            // Act
            _sut.Type = enumValue;

            // Assert
            Assert.That(_sut.Type, Is.EqualTo(enumValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldSupportAllDefinedValues()
        {
            // Arrange
            var allTypes = Enum.GetValues<OrganizerCompanion.Core.Enums.Types>();

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var typeValue in allTypes)
                {
                    _sut.Type = typeValue;
                    Assert.That(_sut.Type, Is.EqualTo(typeValue));
                    Assert.That(_sut.Type.HasValue, Is.True);
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldHandleNullToValueTransitions()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Start with null
                _sut.Type = null;
                Assert.That(_sut.Type, Is.Null);

                // Assign each enum value
                foreach (var enumValue in Enum.GetValues<OrganizerCompanion.Core.Enums.Types>())
                {
                    _sut.Type = enumValue;
                    Assert.That(_sut.Type, Is.EqualTo(enumValue));
                    Assert.That(_sut.Type.HasValue, Is.True);
                    
                    // Back to null
                    _sut.Type = null;
                    Assert.That(_sut.Type, Is.Null);
                    Assert.That(_sut.Type.HasValue, Is.False);
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IMXAddressDTO_InterfaceConsistency_ShouldExposeAllProperties()
        {
            // Arrange
            IMXAddressDTO interfaceDto = new MXAddressDTO();
            var testModified = DateTime.Now.AddHours(-2);
            var testState = new MXState { Name = "Test State", Abbreviation = "TS" };

            // Act
            interfaceDto.Id = 100;
            interfaceDto.Street = "Interface Street";
            interfaceDto.Neighborhood = "Interface Neighborhood";
            interfaceDto.PostalCode = "12345";
            interfaceDto.City = "Interface City";
            interfaceDto.State = testState;
            interfaceDto.Country = "Interface Country";
            interfaceDto.Type = OrganizerCompanion.Core.Enums.Types.Work;
            interfaceDto.IsPrimary = true;
            interfaceDto.DateModified = testModified;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceDto.Id, Is.EqualTo(100));
                Assert.That(interfaceDto.Street, Is.EqualTo("Interface Street"));
                Assert.That(interfaceDto.Neighborhood, Is.EqualTo("Interface Neighborhood"));
                Assert.That(interfaceDto.PostalCode, Is.EqualTo("12345"));
                Assert.That(interfaceDto.City, Is.EqualTo("Interface City"));
                Assert.That(interfaceDto.State, Is.EqualTo(testState));
                Assert.That(interfaceDto.Country, Is.EqualTo("Interface Country"));
                Assert.That(interfaceDto.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(interfaceDto.IsPrimary, Is.True);
                Assert.That(interfaceDto.DateCreated, Is.Not.EqualTo(default(DateTime))); // DateCreated is read-only, check it has a value
                Assert.That(interfaceDto.DateModified, Is.EqualTo(testModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void State_ShouldHandleNullToStateTransitions()
        {
            // Arrange
            var testStates = new INationalSubdivision[]
            {
                new MXState { Name = "State 1", Abbreviation = "S1" },
                new MockNationalSubdivision { Name = "State 2", Abbreviation = "S2" },
                new MXState { Name = "State 3", Abbreviation = "S3" }
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var state in testStates)
                {
                    // Start with null
                    _sut.State = null;
                    Assert.That(_sut.State, Is.Null);

                    // Assign state
                    _sut.State = state;
                    Assert.That(_sut.State, Is.EqualTo(state));
                    Assert.That(_sut.State.Name, Is.EqualTo(state.Name));
                    Assert.That(_sut.State.Abbreviation, Is.EqualTo(state.Abbreviation));

                    // Back to null
                    _sut.State = null;
                    Assert.That(_sut.State, Is.Null);
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHandleNullToDateTransitions()
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
                    _sut.DateModified = null;
                    Assert.That(_sut.DateModified, Is.Null);

                    // Assign date
                    _sut.DateModified = date;
                    Assert.That(_sut.DateModified, Is.EqualTo(date));

                    // Back to null
                    _sut.DateModified = null;
                    Assert.That(_sut.DateModified, Is.Null);
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void MXAddressDTO_ShouldMaintainStateAcrossMultipleOperations()
        {
            // Arrange
            var operations = new[]
            {
                new { Id = 1, Street = (string?)"Street 1", Neighborhood = (string?)"Neighborhood 1", City = (string?)"City 1", IsPrimary = true },
                new { Id = 2, Street = (string?)null, Neighborhood = (string?)"Neighborhood 2", City = (string?)null, IsPrimary = false },
                new { Id = 3, Street = (string?)"", Neighborhood = (string?)null, City = (string?)"City 3", IsPrimary = true },
                new { Id = 4, Street = (string?)"Street 4", Neighborhood = (string?)"", City = (string?)"", IsPrimary = false }
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var op in operations)
                {
                    _sut.Id = op.Id;
                    _sut.Street = op.Street;
                    _sut.Neighborhood = op.Neighborhood;
                    _sut.City = op.City;
                    _sut.IsPrimary = op.IsPrimary;

                    Assert.That(_sut.Id, Is.EqualTo(op.Id));
                    Assert.That(_sut.Street, Is.EqualTo(op.Street));
                    Assert.That(_sut.Neighborhood, Is.EqualTo(op.Neighborhood));
                    Assert.That(_sut.City, Is.EqualTo(op.City));
                    Assert.That(_sut.IsPrimary, Is.EqualTo(op.IsPrimary));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void MXAddressDTO_PropertiesShouldBeIndependent()
        {
            // Arrange & Act
            _sut.Id = 999;
            _sut.Street = "Independent Street";
            _sut.Neighborhood = "Independent Neighborhood";
            _sut.PostalCode = "12345";
            _sut.City = "Independent City";
            _sut.State = new MXState { Name = "Independent State", Abbreviation = "IS" };
            _sut.Country = "Independent Country";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Home;
            _sut.IsPrimary = true;
            var testDate = DateTime.Now.AddDays(-5);
            var testModified = DateTime.Now.AddHours(-3);
            _sut.DateCreated = testDate;
            _sut.DateModified = testModified;

            // Store original values
            var originalId = _sut.Id;
            var originalStreet = _sut.Street;
            var originalNeighborhood = _sut.Neighborhood;
            var originalPostalCode = _sut.PostalCode;
            var originalCity = _sut.City;
            var originalState = _sut.State;
            var originalCountry = _sut.Country;
            var originalType = _sut.Type;
            var originalIsPrimary = _sut.IsPrimary;
            var originalCreated = _sut.DateCreated;
            var originalModified = _sut.DateModified;

            // Assert
            Assert.Multiple(() =>
            {
                // Change Id, verify others unchanged
                _sut.Id = 1000;
                Assert.That(_sut.Street, Is.EqualTo(originalStreet));
                Assert.That(_sut.Neighborhood, Is.EqualTo(originalNeighborhood));
                Assert.That(_sut.PostalCode, Is.EqualTo(originalPostalCode));
                Assert.That(_sut.City, Is.EqualTo(originalCity));
                Assert.That(_sut.State, Is.EqualTo(originalState));
                Assert.That(_sut.Country, Is.EqualTo(originalCountry));
                Assert.That(_sut.Type, Is.EqualTo(originalType));
                Assert.That(_sut.DateCreated, Is.EqualTo(originalCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(originalModified));

                // Change Street, verify others unchanged
                _sut.Street = "Changed Street";
                Assert.That(_sut.Id, Is.EqualTo(1000)); // New value
                Assert.That(_sut.Neighborhood, Is.EqualTo(originalNeighborhood));
                Assert.That(_sut.PostalCode, Is.EqualTo(originalPostalCode));
                Assert.That(_sut.City, Is.EqualTo(originalCity));
                Assert.That(_sut.State, Is.EqualTo(originalState));
                Assert.That(_sut.Country, Is.EqualTo(originalCountry));
                Assert.That(_sut.Type, Is.EqualTo(originalType));
                Assert.That(_sut.DateCreated, Is.EqualTo(originalCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(originalModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void MXAddressDTO_ShouldSupportObjectInitializerSyntax()
        {
            // Arrange
            var testCreated = DateTime.Now.AddDays(-7);
            var testModified = DateTime.Now.AddHours(-1);
            var testState = new MXState { Name = "Initializer State", Abbreviation = "IN" };

            // Act
            var mxAddressDto = new MXAddressDTO
            {
                Id = 555,
                Street = "Initializer Street 123",
                Neighborhood = "Initializer Neighborhood",
                PostalCode = "54321",
                City = "Initializer City",
                State = testState,
                Country = "Initializer Country",
                Type = OrganizerCompanion.Core.Enums.Types.Billing,
                IsPrimary = true,
                DateCreated = testCreated,
                DateModified = testModified
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(mxAddressDto.Id, Is.EqualTo(555));
                Assert.That(mxAddressDto.Street, Is.EqualTo("Initializer Street 123"));
                Assert.That(mxAddressDto.Neighborhood, Is.EqualTo("Initializer Neighborhood"));
                Assert.That(mxAddressDto.PostalCode, Is.EqualTo("54321"));
                Assert.That(mxAddressDto.City, Is.EqualTo("Initializer City"));
                Assert.That(mxAddressDto.State, Is.EqualTo(testState));
                Assert.That(mxAddressDto.Country, Is.EqualTo("Initializer Country"));
                Assert.That(mxAddressDto.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Billing));
                Assert.That(mxAddressDto.IsPrimary, Is.True);
                Assert.That(mxAddressDto.DateCreated, Is.EqualTo(testCreated));
                Assert.That(mxAddressDto.DateModified, Is.EqualTo(testModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException_WithDifferentGenericTypes()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => _sut.Cast<MockDomainEntity>());
                Assert.Throws<NotImplementedException>(() => _sut.Cast<IMXAddressDTO>());
                Assert.Throws<NotImplementedException>(() => _sut.Cast<IDomainEntity>());
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
                    var ex = Assert.Throws<NotImplementedException>(() => _sut.ToJson());
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
        public void StringProperties_ShouldSupportVeryLongStrings()
        {
            // Arrange
            var longString = new string('A', 10000);

            // Act & Assert
            Assert.Multiple(() =>
            {
                _sut.Street = longString;
                Assert.That(_sut.Street?.Length, Is.EqualTo(10000));

                _sut.Neighborhood = longString;
                Assert.That(_sut.Neighborhood?.Length, Is.EqualTo(10000));

                _sut.PostalCode = longString;
                Assert.That(_sut.PostalCode?.Length, Is.EqualTo(10000));

                _sut.City = longString;
                Assert.That(_sut.City?.Length, Is.EqualTo(10000));

                _sut.Country = longString;
                Assert.That(_sut.Country?.Length, Is.EqualTo(10000));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void StringProperties_ShouldHandleConsecutiveAssignments()
        {
            // Arrange
            var streetValues = new[] { "Street 1", null, "", "Street 2", "   ", "Final Street" };
            var neighborhoodValues = new[] { "Neighborhood 1", "", null, "Neighborhood 2" };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var street in streetValues)
                {
                    _sut.Street = street;
                    Assert.That(_sut.Street, Is.EqualTo(street));
                }

                foreach (var neighborhood in neighborhoodValues)
                {
                    _sut.Neighborhood = neighborhood;
                    Assert.That(_sut.Neighborhood, Is.EqualTo(neighborhood));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void MXAddressDTO_ShouldHandleComplexScenarios()
        {
            // Arrange & Act
            _sut.Id = int.MaxValue;
            _sut.Street = "Complex Street with 特殊字符 and emojis 🏠🏘️";
            _sut.Neighborhood = "Neighborhood with\nmultiple\tlines";
            _sut.PostalCode = "12345-ABCDE";
            _sut.City = "Ciudad 测试市 🌆";
            _sut.State = new MockNationalSubdivision { Name = "Complex State 🏛️", Abbreviation = "CS" };
            _sut.Country = "País 测试国 🇲🇽";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Other;
            _sut.DateCreated = DateTime.MaxValue.AddMilliseconds(-1);
            _sut.DateModified = DateTime.MinValue.AddMilliseconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
                Assert.That(_sut.Street, Contains.Substring("Complex Street"));
                Assert.That(_sut.Street, Contains.Substring("特殊字符"));
                Assert.That(_sut.Street, Contains.Substring("🏠🏘️"));
                Assert.That(_sut.Neighborhood, Contains.Substring("multiple"));
                Assert.That(_sut.PostalCode, Contains.Substring("12345"));
                Assert.That(_sut.PostalCode, Contains.Substring("ABCDE"));
                Assert.That(_sut.City, Contains.Substring("Ciudad"));
                Assert.That(_sut.City, Contains.Substring("测试市"));
                Assert.That(_sut.State?.Name, Contains.Substring("Complex State"));
                Assert.That(_sut.Country, Contains.Substring("País"));
                Assert.That(_sut.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
                Assert.That(_sut.DateCreated, Is.LessThan(DateTime.MaxValue));
                Assert.That(_sut.DateModified, Is.GreaterThan(DateTime.MinValue));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void StringProperties_ShouldAcceptMexicanSpecificFormats()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Test Mexican street formats
                var mexicanStreets = new[]
                {
                    "Calle José María Morelos y Pavón No. 123",
                    "Av. 16 de Septiembre Pte. 456-B",
                    "Blvd. Adolfo López Mateos Norte S/N",
                    "Calzada de los Misterios Km. 7.5"
                };

                foreach (var street in mexicanStreets)
                {
                    _sut.Street = street;
                    Assert.That(_sut.Street, Is.EqualTo(street));
                }

                // Test Mexican postal codes
                var mexicanPostalCodes = new[] { "01000", "11560", "06700", "03100", "64000", "20000" };
                foreach (var code in mexicanPostalCodes)
                {
                    _sut.PostalCode = code;
                    Assert.That(_sut.PostalCode, Is.EqualTo(code));
                }

                // Test Mexican cities with accents
                var mexicanCities = new[]
                {
                    "Mérida",
                    "León",
                    "San Luis Potosí",
                    "Querétaro",
                    "Torreón",
                    "Tuxtla Gutiérrez"
                };

                foreach (var city in mexicanCities)
                {
                    _sut.City = city;
                    Assert.That(_sut.City, Is.EqualTo(city));
                }
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
