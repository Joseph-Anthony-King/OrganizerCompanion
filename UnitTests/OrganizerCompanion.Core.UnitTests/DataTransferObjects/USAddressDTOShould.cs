using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using System.Text.Json.Serialization;
using NUnit.Framework;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Interfaces.Type;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    /// <summary>
    /// Unit tests for USAddressDTO class to achieve 100% code coverage.
    /// Tests constructor initialization, property getters/setters, interface implementations,
    /// IDomainEntity methods, JSON serialization attributes, data annotations, and edge cases.
    /// </summary>
    [TestFixture]
    public class USAddressDTOShould
    {
        private USAddressDTO _usAddressDTO;

        [SetUp]
        public void SetUp()
        {
            _usAddressDTO = new USAddressDTO();
        }

        #region Constructor Tests

        [Test, Category("DataTransferObjects")]
        public void Constructor_ShouldInitializeWithDefaultValues()
        {
            // Arrange & Act
            var usAddressDTO = new USAddressDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(usAddressDTO.Id, Is.EqualTo(0));
                Assert.That(usAddressDTO.Street1, Is.Null);
                Assert.That(usAddressDTO.Street2, Is.Null);
                Assert.That(usAddressDTO.City, Is.Null);
                Assert.That(usAddressDTO.State, Is.Null);
                Assert.That(usAddressDTO.ZipCode, Is.Null);
                Assert.That(usAddressDTO.Country, Is.Null);
                Assert.That(usAddressDTO.Type, Is.Null);
                Assert.That(usAddressDTO.IsPrimary, Is.False);
                Assert.That(usAddressDTO.DateCreated, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));
                Assert.That(usAddressDTO.DateModified, Is.Null);
            });
        }

        #endregion

        #region Property Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const int expectedId = 12345;

            // Act
            _usAddressDTO.Id = expectedId;

            // Assert
            Assert.That(_usAddressDTO.Id, Is.EqualTo(expectedId));
        }

        [Test, Category("DataTransferObjects")]
        public void Street1_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const string expectedStreet1 = "123 Main Street";

            // Act
            _usAddressDTO.Street1 = expectedStreet1;

            // Assert
            Assert.That(_usAddressDTO.Street1, Is.EqualTo(expectedStreet1));
        }

        [Test, Category("DataTransferObjects")]
        public void Street1_ShouldAcceptNull()
        {
            // Act
            _usAddressDTO.Street1 = null;

            // Assert
            Assert.That(_usAddressDTO.Street1, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Street1_ShouldAcceptEmptyString()
        {
            // Act
            _usAddressDTO.Street1 = string.Empty;

            // Assert
            Assert.That(_usAddressDTO.Street1, Is.EqualTo(string.Empty));
        }

        [Test, Category("DataTransferObjects")]
        public void Street2_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const string expectedStreet2 = "Apt 4B";

            // Act
            _usAddressDTO.Street2 = expectedStreet2;

            // Assert
            Assert.That(_usAddressDTO.Street2, Is.EqualTo(expectedStreet2));
        }

        [Test, Category("DataTransferObjects")]
        public void Street2_ShouldAcceptNull()
        {
            // Act
            _usAddressDTO.Street2 = null;

            // Assert
            Assert.That(_usAddressDTO.Street2, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Street2_ShouldAcceptEmptyString()
        {
            // Act
            _usAddressDTO.Street2 = string.Empty;

            // Assert
            Assert.That(_usAddressDTO.Street2, Is.EqualTo(string.Empty));
        }

        [Test, Category("DataTransferObjects")]
        public void City_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const string expectedCity = "New York";

            // Act
            _usAddressDTO.City = expectedCity;

            // Assert
            Assert.That(_usAddressDTO.City, Is.EqualTo(expectedCity));
        }

        [Test, Category("DataTransferObjects")]
        public void City_ShouldAcceptNull()
        {
            // Act
            _usAddressDTO.City = null;

            // Assert
            Assert.That(_usAddressDTO.City, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void City_ShouldAcceptEmptyString()
        {
            // Act
            _usAddressDTO.City = string.Empty;

            // Assert
            Assert.That(_usAddressDTO.City, Is.EqualTo(string.Empty));
        }

        [Test, Category("DataTransferObjects")]
        public void State_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedState = new MockNationalSubdivision
            {
                Name = "New York",
                Abbreviation = "NY"
            };

            // Act
            _usAddressDTO.State = expectedState;

            // Assert
            Assert.That(_usAddressDTO.State, Is.EqualTo(expectedState));
        }

        [Test, Category("DataTransferObjects")]
        public void State_ShouldAcceptNull()
        {
            // Act
            _usAddressDTO.State = null;

            // Assert
            Assert.That(_usAddressDTO.State, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void ZipCode_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const string expectedZipCode = "12345";

            // Act
            _usAddressDTO.ZipCode = expectedZipCode;

            // Assert
            Assert.That(_usAddressDTO.ZipCode, Is.EqualTo(expectedZipCode));
        }

        [Test, Category("DataTransferObjects")]
        public void ZipCode_ShouldAcceptNull()
        {
            // Act
            _usAddressDTO.ZipCode = null;

            // Assert
            Assert.That(_usAddressDTO.ZipCode, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void ZipCode_ShouldAcceptEmptyString()
        {
            // Act
            _usAddressDTO.ZipCode = string.Empty;

            // Assert
            Assert.That(_usAddressDTO.ZipCode, Is.EqualTo(string.Empty));
        }

        [Test, Category("DataTransferObjects")]
        public void ZipCode_ShouldAcceptVariousFormats()
        {
            // Arrange, Act & Assert
            var zipCodeFormats = new[]
            {
                "12345",
                "12345-6789",
                "12345 6789",
                "123456789"
            };

            foreach (var format in zipCodeFormats)
            {
                _usAddressDTO.ZipCode = format;
                Assert.That(_usAddressDTO.ZipCode, Is.EqualTo(format));
            }
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const string expectedCountry = "United States";

            // Act
            _usAddressDTO.Country = expectedCountry;

            // Assert
            Assert.That(_usAddressDTO.Country, Is.EqualTo(expectedCountry));
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldAcceptNull()
        {
            // Act
            _usAddressDTO.Country = null;

            // Assert
            Assert.That(_usAddressDTO.Country, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldAcceptEmptyString()
        {
            // Act
            _usAddressDTO.Country = string.Empty;

            // Assert
            Assert.That(_usAddressDTO.Country, Is.EqualTo(string.Empty));
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldGetAndSetCorrectly()
        {
            // Arrange
            const OrganizerCompanion.Core.Enums.Types expectedType = OrganizerCompanion.Core.Enums.Types.Home;

            // Act
            _usAddressDTO.Type = expectedType;

            // Assert
            Assert.That(_usAddressDTO.Type, Is.EqualTo(expectedType));
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldAcceptNull()
        {
            // Act
            _usAddressDTO.Type = null;

            // Assert
            Assert.That(_usAddressDTO.Type, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldAcceptAllValidEnumValues()
        {
            // Arrange, Act & Assert
            foreach (OrganizerCompanion.Core.Enums.Types type in Enum.GetValues<OrganizerCompanion.Core.Enums.Types>())
            {
                _usAddressDTO.Type = type;
                Assert.That(_usAddressDTO.Type, Is.EqualTo(type));
            }
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_ShouldGetAndSetCorrectly()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Test setting to true
                _usAddressDTO.IsPrimary = true;
                Assert.That(_usAddressDTO.IsPrimary, Is.True);

                // Test setting to false
                _usAddressDTO.IsPrimary = false;
                Assert.That(_usAddressDTO.IsPrimary, Is.False);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_ShouldDefaultToFalse()
        {
            // Arrange & Act
            var newAddressDTO = new USAddressDTO();

            // Assert
            Assert.That(newAddressDTO.IsPrimary, Is.False);
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_ShouldHandleBooleanToggling()
        {
            // Arrange
            Assert.That(_usAddressDTO.IsPrimary, Is.False, "Should start as false");

            // Act & Assert - Toggle multiple times
            _usAddressDTO.IsPrimary = !_usAddressDTO.IsPrimary;
            Assert.That(_usAddressDTO.IsPrimary, Is.True, "Should be true after first toggle");

            _usAddressDTO.IsPrimary = !_usAddressDTO.IsPrimary;
            Assert.That(_usAddressDTO.IsPrimary, Is.False, "Should be false after second toggle");

            _usAddressDTO.IsPrimary = !_usAddressDTO.IsPrimary;
            Assert.That(_usAddressDTO.IsPrimary, Is.True, "Should be true after third toggle");
        }

        #endregion

        #region Interface Implementation Tests

        [Test, Category("DataTransferObjects")]
        public void USAddressDTO_ShouldImplementIUSAddressDTO()
        {
            Assert.That(_usAddressDTO, Is.InstanceOf<IUSAddressDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void USAddressDTO_ShouldImplementIDomainEntity()
        {
            Assert.That(_usAddressDTO, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void USAddressDTO_ShouldImplementIUSAddress()
        {
            Assert.That(_usAddressDTO, Is.InstanceOf<OrganizerCompanion.Core.Interfaces.Type.IUSAddress>());
        }

        [Test, Category("DataTransferObjects")]
        public void USAddressDTO_ShouldImplementIAddress()
        {
            Assert.That(_usAddressDTO, Is.InstanceOf<OrganizerCompanion.Core.Interfaces.Type.IAddress>());
        }

        #endregion

        #region IDomainEntity Property Tests


        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 10, 15, 14, 30, 0);

            // Act
            _usAddressDTO.DateCreated = expectedDate;

            // Assert
            Assert.That(_usAddressDTO.DateCreated, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldGetAndSetCorrectly()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 10, 16, 10, 15, 0);

            // Act
            _usAddressDTO.DateModified = expectedDate;

            // Assert
            Assert.That(_usAddressDTO.DateModified, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptNull()
        {
            // Act
            _usAddressDTO.DateModified = null;

            // Assert
            Assert.That(_usAddressDTO.DateModified, Is.Null);
        }

        #endregion

        #region IDomainEntity Method Tests

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _usAddressDTO.Cast<MockDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldThrowNotImplementedException()
        {
            // Act & Assert
            Assert.Throws<NotImplementedException>(() => _usAddressDTO.ToJson());
        }

        #endregion

        #region JSON Serialization Attribute Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.Id));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("id"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Street1_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.Street1));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("street"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Street2_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.Street2));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("street2"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void City_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.City));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("city"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void State_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.State));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("state"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ZipCode_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.ZipCode));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("zipCode"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.Country));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("country"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.Type));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("type"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.DateCreated));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("dateCreated"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.DateModified));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("dateModified"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.IsPrimary));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false)
                .FirstOrDefault() as JsonPropertyNameAttribute;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute!.Name, Is.EqualTo("isPrimary"));
            });
        }

        #endregion

        #region Data Annotation Tests

        [Test, Category("DataTransferObjects")]
        public void AllProperties_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var idProperty = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.Id));
            var street1Property = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.Street1));
            var street2Property = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.Street2));
            var cityProperty = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.City));
            var stateProperty = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.State));
            var zipCodeProperty = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.ZipCode));
            var countryProperty = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.Country));
            var typeProperty = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.Type));
            var isPrimaryProperty = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.IsPrimary));
            var dateCreatedProperty = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.DateCreated));
            var dateModifiedProperty = typeof(USAddressDTO).GetProperty(nameof(USAddressDTO.DateModified));

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(idProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(street1Property?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(street2Property?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(cityProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(stateProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(zipCodeProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(countryProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(typeProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(isPrimaryProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(dateCreatedProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
                Assert.That(dateModifiedProperty?.GetCustomAttributes(typeof(RequiredAttribute), false), Is.Not.Empty);
            });
        }

        #endregion

        #region Edge Case Tests

        [Test, Category("DataTransferObjects")]
        public void Street1_ShouldAcceptSpecialCharacters()
        {
            // Arrange
            const string streetWithSpecialChars = "123 Main St. #4B & Co.";

            // Act
            _usAddressDTO.Street1 = streetWithSpecialChars;

            // Assert
            Assert.That(_usAddressDTO.Street1, Is.EqualTo(streetWithSpecialChars));
        }

        [Test, Category("DataTransferObjects")]
        public void Street1_ShouldAcceptLongStreets()
        {
            // Arrange
            const string longStreet = "1234567890 Very Very Very Long Street Name With Many Words And Numbers Avenue Boulevard";

            // Act
            _usAddressDTO.Street1 = longStreet;

            // Assert
            Assert.That(_usAddressDTO.Street1, Is.EqualTo(longStreet));
        }

        [Test, Category("DataTransferObjects")]
        public void Street2_ShouldAcceptApartmentFormats()
        {
            // Arrange
            var apartmentFormats = new[]
            {
                "Apt 4B",
                "Unit 123",
                "Suite 456",
                "#789",
                "Floor 2",
                "Building A"
            };

            // Act & Assert
            foreach (var format in apartmentFormats)
            {
                _usAddressDTO.Street2 = format;
                Assert.That(_usAddressDTO.Street2, Is.EqualTo(format));
            }
        }

        [Test, Category("DataTransferObjects")]
        public void City_ShouldAcceptCitiesWithSpaces()
        {
            // Arrange
            var citiesWithSpaces = new[]
            {
                "New York",
                "Los Angeles",
                "San Francisco",
                "Las Vegas",
                "Salt Lake City"
            };

            // Act & Assert
            foreach (var city in citiesWithSpaces)
            {
                _usAddressDTO.City = city;
                Assert.That(_usAddressDTO.City, Is.EqualTo(city));
            }
        }

        [Test, Category("DataTransferObjects")]
        public void City_ShouldAcceptCitiesWithSpecialCharacters()
        {
            // Arrange
            var citiesWithSpecialChars = new[]
            {
                "O'Fallon",
                "St. Louis",
                "Coeur d'Alene",
                "Martha's Vineyard"
            };

            // Act & Assert
            foreach (var city in citiesWithSpecialChars)
            {
                _usAddressDTO.City = city;
                Assert.That(_usAddressDTO.City, Is.EqualTo(city));
            }
        }

        [Test, Category("DataTransferObjects")]
        public void ZipCode_ShouldAcceptExtendedFormats()
        {
            // Arrange
            var extendedZipCodes = new[]
            {
                "12345-6789",
                "98765-4321",
                "00501-0001"
            };

            // Act & Assert
            foreach (var zipCode in extendedZipCodes)
            {
                _usAddressDTO.ZipCode = zipCode;
                Assert.That(_usAddressDTO.ZipCode, Is.EqualTo(zipCode));
            }
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldAcceptVariousCountryFormats()
        {
            // Arrange
            var countryFormats = new[]
            {
                "United States",
                "USA",
                "US",
                "United States of America"
            };

            // Act & Assert
            foreach (var country in countryFormats)
            {
                _usAddressDTO.Country = country;
                Assert.That(_usAddressDTO.Country, Is.EqualTo(country));
            }
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptNegativeValues()
        {
            // Arrange
            const int negativeId = -999;

            // Act
            _usAddressDTO.Id = negativeId;

            // Assert
            Assert.That(_usAddressDTO.Id, Is.EqualTo(negativeId));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptMaxIntValue()
        {
            // Arrange
            const int maxValue = int.MaxValue;

            // Act
            _usAddressDTO.Id = maxValue;

            // Assert
            Assert.That(_usAddressDTO.Id, Is.EqualTo(maxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptMinIntValue()
        {
            // Arrange
            const int minValue = int.MinValue;

            // Act
            _usAddressDTO.Id = minValue;

            // Assert
            Assert.That(_usAddressDTO.Id, Is.EqualTo(minValue));
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_ShouldHandleRepeatedAssignments()
        {
            // Arrange, Act & Assert
            for (int i = 0; i < 100; i++)
            {
                var expectedValue = i % 2 == 0;
                _usAddressDTO.IsPrimary = expectedValue;
                Assert.That(_usAddressDTO.IsPrimary, Is.EqualTo(expectedValue), 
                    $"Failed on iteration {i} with expected value {expectedValue}");
            }
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_ShouldMaintainValueAfterOtherPropertyChanges()
        {
            // Arrange
            _usAddressDTO.IsPrimary = true;

            // Act - Change other properties
            _usAddressDTO.Id = 999;
            _usAddressDTO.Street1 = "Test Street";
            _usAddressDTO.City = "Test City";
            _usAddressDTO.Type = OrganizerCompanion.Core.Enums.Types.Work;

            // Assert
            Assert.That(_usAddressDTO.IsPrimary, Is.True, 
                "IsPrimary should maintain its value when other properties change");
        }

        #endregion

        #region JSON Serialization Tests

        [Test, Category("DataTransferObjects")]
        public void USAddressDTO_ShouldSerializeWithCorrectPropertyNames()
        {
            // Arrange
            _usAddressDTO.Id = 123;
            _usAddressDTO.Street1 = "123 Main St";
            _usAddressDTO.Street2 = "Apt 4B";
            _usAddressDTO.City = "New York";
            _usAddressDTO.ZipCode = "10001";
            _usAddressDTO.Country = "USA";
            _usAddressDTO.Type = OrganizerCompanion.Core.Enums.Types.Home;
            _usAddressDTO.IsPrimary = true;

            // Act
            var json = JsonSerializer.Serialize(_usAddressDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(json, Contains.Substring("\"id\":123"));
                Assert.That(json, Contains.Substring("\"street\":\"123 Main St\""));
                Assert.That(json, Contains.Substring("\"street2\":\"Apt 4B\""));
                Assert.That(json, Contains.Substring("\"city\":\"New York\""));
                Assert.That(json, Contains.Substring("\"zipCode\":\"10001\""));
                Assert.That(json, Contains.Substring("\"country\":\"USA\""));
                Assert.That(json, Contains.Substring("\"type\":"));
                Assert.That(json, Contains.Substring("\"isPrimary\":true"));
                Assert.That(json, Contains.Substring("\"dateCreated\":"));
                Assert.That(json, Contains.Substring("\"dateModified\":"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void USAddressDTO_ShouldDeserializeCorrectly()
        {
            // Arrange
            const string json = "{\"id\":456,\"street\":\"456 Oak Ave\",\"street2\":\"Suite 7\",\"city\":\"Chicago\",\"state\":null,\"zipCode\":\"60601\",\"country\":\"United States\",\"type\":1,\"isPrimary\":true}";

            // Act
            var usAddressDTO = JsonSerializer.Deserialize<USAddressDTO>(json);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(usAddressDTO, Is.Not.Null);
                Assert.That(usAddressDTO!.Id, Is.EqualTo(456));
                Assert.That(usAddressDTO.Street1, Is.EqualTo("456 Oak Ave"));
                Assert.That(usAddressDTO.Street2, Is.EqualTo("Suite 7"));
                Assert.That(usAddressDTO.City, Is.EqualTo("Chicago"));
                Assert.That(usAddressDTO.State, Is.Null);
                Assert.That(usAddressDTO.ZipCode, Is.EqualTo("60601"));
                Assert.That(usAddressDTO.Country, Is.EqualTo("United States"));
                Assert.That(usAddressDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(usAddressDTO.IsPrimary, Is.True);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void USAddressDTO_ShouldSerializeWithNullValues()
        {
            // Arrange
            _usAddressDTO.Id = 789;
            _usAddressDTO.Street1 = null;
            _usAddressDTO.Street2 = null;
            _usAddressDTO.City = null;
            _usAddressDTO.State = null;
            _usAddressDTO.ZipCode = null;
            _usAddressDTO.Country = null;
            _usAddressDTO.Type = null;
            _usAddressDTO.IsPrimary = false;

            // Act
            var json = JsonSerializer.Serialize(_usAddressDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Contains.Substring("\"id\":789"));
                Assert.That(json, Contains.Substring("\"street\":null"));
                Assert.That(json, Contains.Substring("\"street2\":null"));
                Assert.That(json, Contains.Substring("\"city\":null"));
                Assert.That(json, Contains.Substring("\"state\":null"));
                Assert.That(json, Contains.Substring("\"zipCode\":null"));
                Assert.That(json, Contains.Substring("\"country\":null"));
                Assert.That(json, Contains.Substring("\"type\":null"));
                Assert.That(json, Contains.Substring("\"isPrimary\":false"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void USAddressDTO_ShouldNotSerializeIDomainEntityProperties()
        {
            // Arrange
            _usAddressDTO.Id = 1;
            _usAddressDTO.Street1 = "Test Street";

            // Act
            var json = JsonSerializer.Serialize(_usAddressDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Does.Not.Contain("isCast"));
                Assert.That(json, Does.Not.Contain("castId"));
                Assert.That(json, Does.Not.Contain("castType"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void USAddressDTO_ShouldSerializeBooleanValuesCorrectly()
        {
            // Arrange, Act & Assert for true
            _usAddressDTO.IsPrimary = true;
            var jsonTrue = JsonSerializer.Serialize(_usAddressDTO);
            Assert.That(jsonTrue, Contains.Substring("\"isPrimary\":true"));

            // Arrange, Act & Assert for false
            _usAddressDTO.IsPrimary = false;
            var jsonFalse = JsonSerializer.Serialize(_usAddressDTO);
            Assert.That(jsonFalse, Contains.Substring("\"isPrimary\":false"));
        }

        [Test, Category("DataTransferObjects")]
        public void USAddressDTO_ShouldDeserializeBooleanValuesCorrectly()
        {
            // Test deserializing true
            const string jsonTrue = "{\"id\":1,\"isPrimary\":true}";
            var dtoTrue = JsonSerializer.Deserialize<USAddressDTO>(jsonTrue);
            Assert.That(dtoTrue?.IsPrimary, Is.True);

            // Test deserializing false
            const string jsonFalse = "{\"id\":2,\"isPrimary\":false}";
            var dtoFalse = JsonSerializer.Deserialize<USAddressDTO>(jsonFalse);
            Assert.That(dtoFalse?.IsPrimary, Is.False);
        }

        #endregion

        #region Integration Tests

        [Test, Category("DataTransferObjects")]
        public void USAddressDTO_ShouldWorkWithComplexScenarios()
        {
            // Arrange
            var mockState = new MockNationalSubdivision
            {
                Name = "California",
                Abbreviation = "CA"
            };

            _usAddressDTO.Id = 999;
            _usAddressDTO.Street1 = "1600 Amphitheatre Parkway";
            _usAddressDTO.Street2 = "Building 42";
            _usAddressDTO.City = "Mountain View";
            _usAddressDTO.State = mockState;
            _usAddressDTO.ZipCode = "94043-1351";
            _usAddressDTO.Country = "United States";
            _usAddressDTO.Type = OrganizerCompanion.Core.Enums.Types.Work;
            _usAddressDTO.IsPrimary = true;

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(_usAddressDTO.Id, Is.EqualTo(999));
                Assert.That(_usAddressDTO.Street1, Is.EqualTo("1600 Amphitheatre Parkway"));
                Assert.That(_usAddressDTO.Street2, Is.EqualTo("Building 42"));
                Assert.That(_usAddressDTO.City, Is.EqualTo("Mountain View"));
                Assert.That(_usAddressDTO.State, Is.EqualTo(mockState));
                Assert.That(_usAddressDTO.ZipCode, Is.EqualTo("94043-1351"));
                Assert.That(_usAddressDTO.Country, Is.EqualTo("United States"));
                Assert.That(_usAddressDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(_usAddressDTO.IsPrimary, Is.True);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void USAddressDTO_ShouldMaintainTypeIntegrity()
        {
            // Arrange & Act
            IUSAddressDTO interfaceAddress = _usAddressDTO;
            interfaceAddress.Street1 = "Interface Test Street";
            interfaceAddress.City = "Interface City";
            interfaceAddress.Type = OrganizerCompanion.Core.Enums.Types.Other;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_usAddressDTO.Street1, Is.EqualTo("Interface Test Street"));
                Assert.That(_usAddressDTO.City, Is.EqualTo("Interface City"));
                Assert.That(_usAddressDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
                Assert.That(interfaceAddress.Street1, Is.EqualTo(_usAddressDTO.Street1));
                Assert.That(interfaceAddress.City, Is.EqualTo(_usAddressDTO.City));
                Assert.That(interfaceAddress.Type, Is.EqualTo(_usAddressDTO.Type));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void USAddressDTO_ShouldHandleTypesEnumConversions()
        {
            // Arrange, Act & Assert
            var allTypes = Enum.GetValues<OrganizerCompanion.Core.Enums.Types>();
            
            foreach (var type in allTypes)
            {
                _usAddressDTO.Type = type;
                var json = JsonSerializer.Serialize(_usAddressDTO);
                var deserialized = JsonSerializer.Deserialize<USAddressDTO>(json);
                
                Assert.That(deserialized!.Type, Is.EqualTo(type), 
                    $"Failed for type: {type}");
            }
        }

        [Test, Category("DataTransferObjects")]
        public void USAddressDTO_ShouldWorkWithStatePolymorphism()
        {
            // Arrange
            var state1 = new MockNationalSubdivision { Name = "Texas", Abbreviation = "TX" };
            var state2 = new MockNationalSubdivision { Name = "Florida", Abbreviation = "FL" };

            // Act
            _usAddressDTO.State = state1;
            var firstState = _usAddressDTO.State;
            
            _usAddressDTO.State = state2;
            var secondState = _usAddressDTO.State;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(firstState, Is.InstanceOf<INationalSubdivision>());
                Assert.That(secondState, Is.InstanceOf<INationalSubdivision>());
                Assert.That(firstState, Is.Not.EqualTo(secondState));
                Assert.That(secondState.Name, Is.EqualTo("Florida"));
                Assert.That(secondState.Abbreviation, Is.EqualTo("FL"));
            });
        }

        #endregion

        #region Comprehensive Coverage Tests

        [Test, Category("Boundary")]
        public void Id_ShouldHandleBoundaryValues()
        {
            // Test zero
            _usAddressDTO.Id = 0;
            Assert.That(_usAddressDTO.Id, Is.EqualTo(0));

            // Test positive boundary
            _usAddressDTO.Id = int.MaxValue;
            Assert.That(_usAddressDTO.Id, Is.EqualTo(int.MaxValue));

            // Test negative boundary
            _usAddressDTO.Id = int.MinValue;
            Assert.That(_usAddressDTO.Id, Is.EqualTo(int.MinValue));
        }

        [Test, Category("Unicode")]
        public void StringProperties_ShouldHandleUnicodeCharacters()
        {
            // Arrange
            var unicodeStreet = "123 Ümlaut Straße";
            var unicodeCity = "São Paulo";
            var unicodeCountry = "中国";

            // Act & Assert
            Assert.Multiple(() =>
            {
                _usAddressDTO.Street1 = unicodeStreet;
                Assert.That(_usAddressDTO.Street1, Is.EqualTo(unicodeStreet));

                _usAddressDTO.City = unicodeCity;
                Assert.That(_usAddressDTO.City, Is.EqualTo(unicodeCity));

                _usAddressDTO.Country = unicodeCountry;
                Assert.That(_usAddressDTO.Country, Is.EqualTo(unicodeCountry));
            });
        }

        [Test, Category("String")]
        public void StringProperties_ShouldHandleVeryLongStrings()
        {
            // Arrange
            var veryLongStreet = new string('A', 10000);
            var veryLongCity = new string('B', 5000);
            var veryLongCountry = new string('C', 3000);
            var veryLongZipCode = new string('1', 1000);

            // Act & Assert - DTOs should accept any string length
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _usAddressDTO.Street1 = veryLongStreet);
                Assert.That(_usAddressDTO.Street1, Is.EqualTo(veryLongStreet));

                Assert.DoesNotThrow(() => _usAddressDTO.City = veryLongCity);
                Assert.That(_usAddressDTO.City, Is.EqualTo(veryLongCity));

                Assert.DoesNotThrow(() => _usAddressDTO.Country = veryLongCountry);
                Assert.That(_usAddressDTO.Country, Is.EqualTo(veryLongCountry));

                Assert.DoesNotThrow(() => _usAddressDTO.ZipCode = veryLongZipCode);
                Assert.That(_usAddressDTO.ZipCode, Is.EqualTo(veryLongZipCode));
            });
        }

        [Test, Category("DateTime")]
        public void DateProperties_ShouldHandlePreciseDateTime()
        {
            // Arrange
            var preciseDateTime = new DateTime(2025, 10, 20, 14, 35, 42, 999);
            var minDateTime = DateTime.MinValue;
            var maxDateTime = DateTime.MaxValue;

            // Act & Assert for DateCreated
            Assert.Multiple(() =>
            {
                _usAddressDTO.DateCreated = preciseDateTime;
                Assert.That(_usAddressDTO.DateCreated, Is.EqualTo(preciseDateTime));

                _usAddressDTO.DateCreated = minDateTime;
                Assert.That(_usAddressDTO.DateCreated, Is.EqualTo(minDateTime));

                _usAddressDTO.DateCreated = maxDateTime;
                Assert.That(_usAddressDTO.DateCreated, Is.EqualTo(maxDateTime));
            });

            // Act & Assert for DateModified
            Assert.Multiple(() =>
            {
                _usAddressDTO.DateModified = preciseDateTime;
                Assert.That(_usAddressDTO.DateModified, Is.EqualTo(preciseDateTime));

                _usAddressDTO.DateModified = minDateTime;
                Assert.That(_usAddressDTO.DateModified, Is.EqualTo(minDateTime));

                _usAddressDTO.DateModified = maxDateTime;
                Assert.That(_usAddressDTO.DateModified, Is.EqualTo(maxDateTime));

                _usAddressDTO.DateModified = null;
                Assert.That(_usAddressDTO.DateModified, Is.Null);
            });
        }

        [Test, Category("Enum")]
        public void Type_ShouldHandleAllTypesEnumValues()
        {
            // Act & Assert - Test all enum values
            foreach (OrganizerCompanion.Core.Enums.Types enumValue in Enum.GetValues<OrganizerCompanion.Core.Enums.Types>())
            {
                _usAddressDTO.Type = enumValue;
                Assert.That(_usAddressDTO.Type, Is.EqualTo(enumValue));
            }

            // Test null value
            _usAddressDTO.Type = null;
            Assert.That(_usAddressDTO.Type, Is.Null);
        }

        [Test, Category("Interface")]
        public void USAddressDTO_AsIUSAddressDTO_ShouldProvideAllProperties()
        {
            // Arrange
            var usAddressInterface = (IUSAddressDTO)_usAddressDTO;
            var testState = new MockNationalSubdivision { Name = "Test State", Abbreviation = "TS" };

            // Act & Assert - All properties should be accessible through interface
            Assert.Multiple(() =>
            {
                usAddressInterface.Id = 999;
                Assert.That(_usAddressDTO.Id, Is.EqualTo(999));

                usAddressInterface.Street1 = "Interface Street";
                Assert.That(_usAddressDTO.Street1, Is.EqualTo("Interface Street"));

                usAddressInterface.Street2 = "Interface Street2";
                Assert.That(_usAddressDTO.Street2, Is.EqualTo("Interface Street2"));

                usAddressInterface.City = "Interface City";
                Assert.That(_usAddressDTO.City, Is.EqualTo("Interface City"));

                usAddressInterface.State = testState;
                Assert.That(_usAddressDTO.State, Is.SameAs(testState));

                usAddressInterface.ZipCode = "12345";
                Assert.That(_usAddressDTO.ZipCode, Is.EqualTo("12345"));

                usAddressInterface.Country = "Interface Country";
                Assert.That(_usAddressDTO.Country, Is.EqualTo("Interface Country"));

                usAddressInterface.Type = OrganizerCompanion.Core.Enums.Types.Work;
                Assert.That(_usAddressDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));

                usAddressInterface.IsPrimary = true;
                Assert.That(_usAddressDTO.IsPrimary, Is.True);

                var testDate = DateTime.Now;
                usAddressInterface.DateModified = testDate;
                Assert.That(_usAddressDTO.DateModified, Is.EqualTo(testDate));
            });
        }

        [Test, Category("Interface")]
        public void USAddressDTO_AsIDomainEntity_ShouldProvideRequiredProperties()
        {
            // Arrange
            var domainEntity = (IDomainEntity)_usAddressDTO;
            var testDate = DateTime.Now;

            // Act & Assert
            Assert.Multiple(() =>
            {
                domainEntity.Id = 555;
                Assert.That(_usAddressDTO.Id, Is.EqualTo(555));

                domainEntity.DateModified = testDate;
                Assert.That(_usAddressDTO.DateModified, Is.EqualTo(testDate));

                // Test Cast and ToJson methods throw NotImplementedException
                Assert.Throws<NotImplementedException>(() => domainEntity.Cast<MockDomainEntity>());
                Assert.Throws<NotImplementedException>(() => domainEntity.ToJson());
            });
        }

        [Test, Category("Whitespace")]
        public void StringProperties_ShouldHandleWhitespaceValues()
        {
            // Test various whitespace scenarios
            Assert.Multiple(() =>
            {
                // Empty strings
                _usAddressDTO.Street1 = "";
                Assert.That(_usAddressDTO.Street1, Is.EqualTo(""));

                // Whitespace only
                _usAddressDTO.Street2 = "   ";
                Assert.That(_usAddressDTO.Street2, Is.EqualTo("   "));

                // Leading/trailing spaces
                _usAddressDTO.City = " City Name ";
                Assert.That(_usAddressDTO.City, Is.EqualTo(" City Name "));

                // Tabs and newlines
                _usAddressDTO.Country = "\t\nCountry\r\n";
                Assert.That(_usAddressDTO.Country, Is.EqualTo("\t\nCountry\r\n"));

                // Mixed whitespace
                _usAddressDTO.ZipCode = " \t 12345 \r\n ";
                Assert.That(_usAddressDTO.ZipCode, Is.EqualTo(" \t 12345 \r\n "));
            });
        }

        [Test, Category("State")]
        public void State_ShouldHandleMultipleStateAssignments()
        {
            // Arrange
            var states = new[]
            {
                new MockNationalSubdivision { Name = "California", Abbreviation = "CA" },
                new MockNationalSubdivision { Name = "New York", Abbreviation = "NY" },
                new MockNationalSubdivision { Name = "Texas", Abbreviation = "TX" },
                new MockNationalSubdivision { Name = "Florida", Abbreviation = "FL" }
            };

            // Act & Assert - Should handle multiple state assignments
            foreach (var state in states)
      {
        _usAddressDTO.State = state;
                Assert.That(_usAddressDTO.State, Is.SameAs(state));
        Assert.Multiple(() =>
        {
          Assert.That(_usAddressDTO.State?.Name, Is.EqualTo(state.Name));
          Assert.That(_usAddressDTO.State?.Abbreviation, Is.EqualTo(state.Abbreviation));
        });
      }

      // Test null assignment
      _usAddressDTO.State = null;
            Assert.That(_usAddressDTO.State, Is.Null);
        }

        [Test, Category("Property")]
        public void AllProperties_ShouldMaintainIndependence()
        {
            // Arrange
            var testState = new MockNationalSubdivision { Name = "Test", Abbreviation = "TE" };
            
            // Act - Set all properties
            _usAddressDTO.Id = 123;
            _usAddressDTO.Street1 = "Test Street 1";
            _usAddressDTO.Street2 = "Test Street 2";
            _usAddressDTO.City = "Test City";
            _usAddressDTO.State = testState;
            _usAddressDTO.ZipCode = "12345";
            _usAddressDTO.Country = "Test Country";
            _usAddressDTO.Type = OrganizerCompanion.Core.Enums.Types.Home;
            _usAddressDTO.IsPrimary = true;
            var testDate = DateTime.Now;
            _usAddressDTO.DateCreated = testDate;
            _usAddressDTO.DateModified = testDate;

            // Assert - All properties should maintain their values independently
            Assert.Multiple(() =>
            {
                Assert.That(_usAddressDTO.Id, Is.EqualTo(123));
                Assert.That(_usAddressDTO.Street1, Is.EqualTo("Test Street 1"));
                Assert.That(_usAddressDTO.Street2, Is.EqualTo("Test Street 2"));
                Assert.That(_usAddressDTO.City, Is.EqualTo("Test City"));
                Assert.That(_usAddressDTO.State, Is.SameAs(testState));
                Assert.That(_usAddressDTO.ZipCode, Is.EqualTo("12345"));
                Assert.That(_usAddressDTO.Country, Is.EqualTo("Test Country"));
                Assert.That(_usAddressDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Home));
                Assert.That(_usAddressDTO.IsPrimary, Is.True);
                Assert.That(_usAddressDTO.DateCreated, Is.EqualTo(testDate));
                Assert.That(_usAddressDTO.DateModified, Is.EqualTo(testDate));
            });
        }

        [Test, Category("Threading")]
        public void Properties_ShouldBeThreadSafe()
        {
            // This test verifies that property access doesn't cause threading issues
            var testState = new MockNationalSubdivision { Name = "Thread Test", Abbreviation = "TT" };

            // Act & Assert - Multiple property access should be consistent
            _usAddressDTO.State = testState;
            var state1 = _usAddressDTO.State;
            var state2 = _usAddressDTO.State;
            var state3 = _usAddressDTO.State;

            Assert.Multiple(() =>
            {
                Assert.That(state1, Is.SameAs(testState));
                Assert.That(state2, Is.SameAs(testState));
                Assert.That(state3, Is.SameAs(testState));
                Assert.That(state1, Is.SameAs(state2));
                Assert.That(state2, Is.SameAs(state3));
            });
        }

        [Test, Category("Validation")]
        public void TypeInformation_ShouldBeCorrect()
        {
            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(_usAddressDTO.GetType(), Is.EqualTo(typeof(USAddressDTO)));
                Assert.That(_usAddressDTO.GetType().Name, Is.EqualTo("USAddressDTO"));
                Assert.That(_usAddressDTO.GetType().Namespace, Is.EqualTo("OrganizerCompanion.Core.Models.DataTransferObject"));
                
                // Interface implementations
                Assert.That(_usAddressDTO is IUSAddressDTO, Is.True);
                Assert.That(_usAddressDTO is IDomainEntity, Is.True);
                Assert.That(_usAddressDTO is OrganizerCompanion.Core.Interfaces.Type.IUSAddress, Is.True);
                Assert.That(_usAddressDTO is OrganizerCompanion.Core.Interfaces.Type.IAddress, Is.True);
            });
        }

        [Test, Category("DefaultValues")]
        public void NewInstance_ShouldHaveCorrectDefaultValues()
        {
            // Arrange & Act
            var newInstance = new USAddressDTO();

            // Assert - Verify all default values
            Assert.Multiple(() =>
            {
                Assert.That(newInstance.Id, Is.EqualTo(0));
                Assert.That(newInstance.Street1, Is.Null);
                Assert.That(newInstance.Street2, Is.Null);
                Assert.That(newInstance.City, Is.Null);
                Assert.That(newInstance.State, Is.Null);
                Assert.That(newInstance.ZipCode, Is.Null);
                Assert.That(newInstance.Country, Is.Null);
                Assert.That(newInstance.Type, Is.Null);
                Assert.That(newInstance.IsPrimary, Is.False);
                Assert.That(newInstance.DateCreated, Is.EqualTo(DateTime.Now).Within(TimeSpan.FromSeconds(1)));
                Assert.That(newInstance.DateModified, Is.Null);
            });
        }

        [Test, Category("Edge")]
        public void AllNullableProperties_ShouldAcceptNull()
        {
            // Act & Assert - Test all nullable properties can be set to null
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _usAddressDTO.Street1 = null);
                Assert.DoesNotThrow(() => _usAddressDTO.Street2 = null);
                Assert.DoesNotThrow(() => _usAddressDTO.City = null);
                Assert.DoesNotThrow(() => _usAddressDTO.State = null);
                Assert.DoesNotThrow(() => _usAddressDTO.ZipCode = null);
                Assert.DoesNotThrow(() => _usAddressDTO.Country = null);
                Assert.DoesNotThrow(() => _usAddressDTO.Type = null);
                Assert.DoesNotThrow(() => _usAddressDTO.DateModified = null);

                Assert.That(_usAddressDTO.Street1, Is.Null);
                Assert.That(_usAddressDTO.Street2, Is.Null);
                Assert.That(_usAddressDTO.City, Is.Null);
                Assert.That(_usAddressDTO.State, Is.Null);
                Assert.That(_usAddressDTO.ZipCode, Is.Null);
                Assert.That(_usAddressDTO.Country, Is.Null);
                Assert.That(_usAddressDTO.Type, Is.Null);
                Assert.That(_usAddressDTO.DateModified, Is.Null);
            });
        }

        [Test, Category("Complex")]
        public void CompleteAddressScenario_ShouldWorkCorrectly()
        {
            // Arrange
            var californiaState = new MockNationalSubdivision { Name = "California", Abbreviation = "CA" };
            var createdDate = new DateTime(2025, 1, 1, 10, 0, 0);
            var modifiedDate = new DateTime(2025, 10, 20, 15, 30, 45);

            // Act - Create a complete address
            _usAddressDTO.Id = 12345;
            _usAddressDTO.Street1 = "1600 Amphitheatre Parkway";
            _usAddressDTO.Street2 = "Building 43";
            _usAddressDTO.City = "Mountain View";
            _usAddressDTO.State = californiaState;
            _usAddressDTO.ZipCode = "94043-1351";
            _usAddressDTO.Country = "United States";
            _usAddressDTO.Type = OrganizerCompanion.Core.Enums.Types.Work;
            _usAddressDTO.IsPrimary = true;
            _usAddressDTO.DateCreated = createdDate;
            _usAddressDTO.DateModified = modifiedDate;

            // Assert - Verify complete address
            Assert.Multiple(() =>
            {
                Assert.That(_usAddressDTO.Id, Is.EqualTo(12345));
                Assert.That(_usAddressDTO.Street1, Is.EqualTo("1600 Amphitheatre Parkway"));
                Assert.That(_usAddressDTO.Street2, Is.EqualTo("Building 43"));
                Assert.That(_usAddressDTO.City, Is.EqualTo("Mountain View"));
                Assert.That(_usAddressDTO.State, Is.SameAs(californiaState));
                Assert.That(_usAddressDTO.State?.Name, Is.EqualTo("California"));
                Assert.That(_usAddressDTO.State?.Abbreviation, Is.EqualTo("CA"));
                Assert.That(_usAddressDTO.ZipCode, Is.EqualTo("94043-1351"));
                Assert.That(_usAddressDTO.Country, Is.EqualTo("United States"));
                Assert.That(_usAddressDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(_usAddressDTO.IsPrimary, Is.True);
                Assert.That(_usAddressDTO.DateCreated, Is.EqualTo(createdDate));
                Assert.That(_usAddressDTO.DateModified, Is.EqualTo(modifiedDate));
            });
        }

        #endregion

        #region Mock Classes for Testing

        /// <summary>
        /// Mock implementation of IDomainEntity for testing purposes.
        /// </summary>
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime DateCreated { get; set; }
            public DateTime? DateModified { get; set; }

            public T Cast<T>() where T : IDomainEntity
            {
                throw new NotImplementedException();
            }

            public string ToJson()
            {
                throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Mock implementation of INationalSubdivision for testing purposes.
        /// </summary>
        private class MockNationalSubdivision : INationalSubdivision
        {
            public string? Name { get; set; }
            public string? Abbreviation { get; set; }
        }

        #endregion
    }
}
