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
    internal class CAAddressDTOShould
    {
        private CAAddressDTO _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CAAddressDTO();
        }

        [Test, Category("DataTransferObjects")]
        public void DefaultConstructor_ShouldCreateCAAddressDTOWithDefaultValues()
        {
            // Arrange & Act
            _sut = new CAAddressDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0)); // Default value should be 0 (non-negative)
                Assert.That(_sut.Features, Is.Not.Null);
                Assert.That(_sut.Features, Is.Empty);
                Assert.That(_sut.Street1, Is.Null);
                Assert.That(_sut.Street2, Is.Null);
                Assert.That(_sut.City, Is.Null);
                Assert.That(_sut.Province, Is.Null);
                Assert.That(_sut.ZipCode, Is.Null);
                Assert.That(_sut.Country, Is.Null);
                Assert.That(_sut.Type, Is.Null);
                Assert.That(_sut.IsPrimary, Is.False);
                Assert.That(_sut.CreatedDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
                Assert.That(_sut.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DefaultConstructor_ShouldCreateValidObject()
        {
            // Arrange & Act
            var dto = new CAAddressDTO();
            var validationContext = new ValidationContext(dto);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dto, validationContext, validationResults, true);

            // Assert - The default object should not fail validation for the Id property
            var idValidationErrors = new List<ValidationResult>();
            foreach (var result in validationResults)
            {
                if (result.MemberNames.Contains(nameof(CAAddressDTO.Id)) || 
                    (result.ErrorMessage?.Contains("Id must be a non-negative number.") == true))
                {
                    idValidationErrors.Add(result);
                }
            }

            Assert.That(idValidationErrors, Is.Empty, "Default Id value (0) should be valid");
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
        public void Features_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedFeatures = new List<FeatureDTO>
            {
                new() { Id = 1, FeatureName = "Feature 1", IsEnabled = true },
                new() { Id = 2, FeatureName = "Feature 2", IsEnabled = false }
            };

            // Act
            _sut.Features = expectedFeatures;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Features, Is.EqualTo(expectedFeatures));
                Assert.That(_sut.Features, Has.Count.EqualTo(2));
                Assert.That(_sut.Features[0].Id, Is.EqualTo(1));
                Assert.That(_sut.Features[0].FeatureName, Is.EqualTo("Feature 1"));
                Assert.That(_sut.Features[0].IsEnabled, Is.True);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Features_ShouldAcceptEmptyList()
        {
            // Arrange
            var emptyFeatures = new List<FeatureDTO>();

            // Act
            _sut.Features = emptyFeatures;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Features, Is.Not.Null);
                Assert.That(_sut.Features, Is.Empty);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Street1_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedStreet = "123 Main Street";

            // Act
            _sut.Street1 = expectedStreet;

            // Assert
            Assert.That(_sut.Street1, Is.EqualTo(expectedStreet));
        }

        [Test, Category("DataTransferObjects")]
        public void Street1_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.Street1 = null;

            // Assert
            Assert.That(_sut.Street1, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Street2_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedStreet2 = "Apt 456";

            // Act
            _sut.Street2 = expectedStreet2;

            // Assert
            Assert.That(_sut.Street2, Is.EqualTo(expectedStreet2));
        }

        [Test, Category("DataTransferObjects")]
        public void Street2_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.Street2 = null;

            // Assert
            Assert.That(_sut.Street2, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void City_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedCity = "Toronto";

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
        public void Province_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedProvince = new CAProvince { Name = "Ontario", Abbreviation = "ON" };

            // Act
            _sut.Province = expectedProvince;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Province, Is.EqualTo(expectedProvince));
                Assert.That(_sut.Province.Name, Is.EqualTo("Ontario"));
                Assert.That(_sut.Province.Abbreviation, Is.EqualTo("ON"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Province_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.Province = null;

            // Assert
            Assert.That(_sut.Province, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void ZipCode_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedZipCode = "M5V 3M6";

            // Act
            _sut.ZipCode = expectedZipCode;

            // Assert
            Assert.That(_sut.ZipCode, Is.EqualTo(expectedZipCode));
        }

        [Test, Category("DataTransferObjects")]
        public void ZipCode_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.ZipCode = null;

            // Assert
            Assert.That(_sut.ZipCode, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedCountry = "Canada";

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
                OrganizerCompanion.Core.Enums.Types.Mobile, 
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
        public void IsPrimary_ShouldGetAndSetBooleanValues()
        {
            // Arrange
            var booleanValues = new[] { true, false };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var boolValue in booleanValues)
                {
                    _sut.IsPrimary = boolValue;
                    Assert.That(_sut.IsPrimary, Is.EqualTo(boolValue));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_ShouldHaveDefaultValue()
        {
            // Arrange & Act
            var dto = new CAAddressDTO();

            // Assert
            Assert.That(dto.IsPrimary, Is.False);
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            _sut.CreatedDate = expectedDate;

            // Assert
            Assert.That(_sut.CreatedDate, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldHaveDefaultValue()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var dto = new CAAddressDTO();
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto.CreatedDate, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(dto.CreatedDate, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            _sut.ModifiedDate = expectedDate;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.ModifiedDate = null;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.Null);
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
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.Id));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveRangeAttribute()
        {
            // Arrange
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.Id));

            // Act
            var rangeAttribute = property?.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(rangeAttribute, Is.Not.Null);
                Assert.That(rangeAttribute?.Minimum, Is.EqualTo(0));
                Assert.That(rangeAttribute?.Maximum, Is.EqualTo(int.MaxValue));
                Assert.That(rangeAttribute?.ErrorMessage, Is.EqualTo("Id must be a non-negative number."));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptZero()
        {
            // Arrange & Act
            _sut.Id = 0;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptPositiveNumbers()
        {
            // Arrange
            var positiveNumbers = new[] { 1, 10, 100, 1000, int.MaxValue };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var number in positiveNumbers)
                {
                    _sut.Id = number;
                    Assert.That(_sut.Id, Is.EqualTo(number));
                }
            });
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
        public void Id_ValidationContext_ShouldFailForNegativeNumbers()
        {
            // Arrange
            _sut.Id = -1;
            var validationContext = new ValidationContext(_sut);
            var validationResults = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(_sut, validationContext, validationResults, true);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(isValid, Is.False);
                Assert.That(validationResults, Has.Count.GreaterThan(0));
                Assert.That(validationResults.Any(r => r.ErrorMessage?.Contains("Id must be a non-negative number.") == true), Is.True);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ValidationContext_ShouldPassForNonNegativeNumbers()
        {
            // Arrange
            var validIds = new[] { 0, 1, 100, 1000, int.MaxValue };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var validId in validIds)
                {
                    _sut.Id = validId;
                    var validationContext = new ValidationContext(_sut);
                    var validationResults = new List<ValidationResult>();
                    var isValid = Validator.TryValidateObject(_sut, validationContext, validationResults, true);

                    // Filter out validation errors not related to Id
                    var idValidationErrors = new List<ValidationResult>();
                    foreach (var result in validationResults)
                    {
                        if (result.MemberNames.Contains(nameof(CAAddressDTO.Id)) || 
                            (result.ErrorMessage?.Contains("Id must be a non-negative number.") == true))
                        {
                            idValidationErrors.Add(result);
                        }
                    }

                    Assert.That(idValidationErrors, Is.Empty, $"Id value {validId} should be valid but validation failed");
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ValidationContext_ShouldFailForSpecificNegativeValues()
        {
            // Arrange
            var invalidIds = new[] { -1, -10, -100, int.MinValue };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var invalidId in invalidIds)
                {
                    _sut.Id = invalidId;
                    var validationContext = new ValidationContext(_sut);
                    var validationResults = new List<ValidationResult>();
                    var isValid = Validator.TryValidateObject(_sut, validationContext, validationResults, true);

                    var idValidationErrors = new List<ValidationResult>();
                    foreach (var result in validationResults)
                    {
                        if (result.MemberNames.Contains(nameof(CAAddressDTO.Id)) || 
                            (result.ErrorMessage?.Contains("Id must be a non-negative number.") == true))
                        {
                            idValidationErrors.Add(result);
                        }
                    }

                    Assert.That(idValidationErrors, Has.Count.GreaterThan(0), 
                        $"Id value {invalidId} should be invalid but validation passed");
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Features_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.Features));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Street1_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.Street1));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Street2_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.Street2));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void City_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.City));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Province_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.Province));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void ZipCode_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.ZipCode));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Country_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.Country));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.Type));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.CreatedDate));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.ModifiedDate));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsPrimary_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.IsPrimary));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CAAddressDTO_ShouldImplementICAAddressDTO()
        {
            // Arrange & Act
            var caAddressDTO = new CAAddressDTO();

            // Assert
            Assert.That(caAddressDTO, Is.InstanceOf<ICAAddressDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void CAAddressDTO_ShouldImplementIDomainEntity()
        {
            // Arrange & Act
            var caAddressDTO = new CAAddressDTO();

            // Assert
            Assert.That(caAddressDTO, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void CAAddressDTO_Properties_ShouldBeSettableInChain()
        {
            // Arrange & Act
            var caAddressDTO = new CAAddressDTO
            {
                Id = 999,
                Features = [new() { Id = 1, FeatureName = "Test", IsEnabled = true }],
                Street1 = "123 Test Street",
                Street2 = "Unit 456",
                City = "Toronto",
                Province = new CAProvince { Name = "Ontario", Abbreviation = "ON" },
                ZipCode = "M5V 3M6",
                Country = "Canada",
                Type = OrganizerCompanion.Core.Enums.Types.Work,
                IsPrimary = true,
                CreatedDate = new DateTime(2023, 1, 1, 12, 0, 0),
                ModifiedDate = new DateTime(2023, 1, 2, 12, 0, 0)
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(caAddressDTO.Id, Is.EqualTo(999));
                Assert.That(caAddressDTO.Features, Has.Count.EqualTo(1));
                Assert.That(caAddressDTO.Street1, Is.EqualTo("123 Test Street"));
                Assert.That(caAddressDTO.Street2, Is.EqualTo("Unit 456"));
                Assert.That(caAddressDTO.City, Is.EqualTo("Toronto"));
                Assert.That(caAddressDTO.Province?.Name, Is.EqualTo("Ontario"));
                Assert.That(caAddressDTO.ZipCode, Is.EqualTo("M5V 3M6"));
                Assert.That(caAddressDTO.Country, Is.EqualTo("Canada"));
                Assert.That(caAddressDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(caAddressDTO.IsPrimary, Is.True);
                Assert.That(caAddressDTO.CreatedDate, Is.EqualTo(new DateTime(2023, 1, 1, 12, 0, 0)));
                Assert.That(caAddressDTO.ModifiedDate, Is.EqualTo(new DateTime(2023, 1, 2, 12, 0, 0)));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void JsonPropertyName_Attributes_ShouldBePresent()
        {
            // Arrange
            var properties = new Dictionary<string, string>
            {
                { nameof(CAAddressDTO.Id), "id" },
                { nameof(CAAddressDTO.Features), "features" },
                { nameof(CAAddressDTO.Street1), "street" },
                { nameof(CAAddressDTO.Street2), "street2" },
                { nameof(CAAddressDTO.City), "city" },
                { nameof(CAAddressDTO.Province), "province" },
                { nameof(CAAddressDTO.ZipCode), "zipCode" },
                { nameof(CAAddressDTO.Country), "country" },
                { nameof(CAAddressDTO.Type), "type" },
                { nameof(CAAddressDTO.IsPrimary), "isPrimary" },
                { nameof(CAAddressDTO.CreatedDate), "createdDate" },
                { nameof(CAAddressDTO.ModifiedDate), "modifiedDate" }
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var kvp in properties)
                {
                    var property = typeof(CAAddressDTO).GetProperty(kvp.Key);
                    var jsonAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
                    Assert.That(jsonAttribute, Is.Not.Null, $"Property {kvp.Key} should have JsonPropertyName attribute");
                    Assert.That(jsonAttribute?.Name, Is.EqualTo(kvp.Value), $"Property {kvp.Key} should have JsonPropertyName '{kvp.Value}'");
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Province_ShouldAcceptDifferentINationalSubdivisionImplementations()
        {
            // Arrange
            var mockProvince = new MockNationalSubdivision { Name = "Mock Province", Abbreviation = "MP" };

            // Act
            _sut.Province = mockProvince;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Province, Is.EqualTo(mockProvince));
                Assert.That(_sut.Province, Is.InstanceOf<INationalSubdivision>());
                Assert.That(_sut.Province.Name, Is.EqualTo("Mock Province"));
                Assert.That(_sut.Province.Abbreviation, Is.EqualTo("MP"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_DefaultValue_ShouldBeCloseToCurrentTime()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var dto = new CAAddressDTO();
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto.CreatedDate, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(dto.CreatedDate, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_DefaultValue_ShouldBeNull()
        {
            // Arrange & Act
            var dto = new CAAddressDTO();

            // Assert
            Assert.That(dto.ModifiedDate, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CAAddressDTO_AsInterface_ShouldRetainAllProperties()
        {
            // Arrange
            var testId = 100;
            var testFeatures = new List<FeatureDTO> { new() { Id = 1, FeatureName = "Test", IsEnabled = true } };
            var testStreet1 = "123 Interface St";
            var testStreet2 = "Unit 200";
            var testCity = "Toronto";
            var testProvince = new CAProvince { Name = "Ontario", Abbreviation = "ON" };
            var testZipCode = "M5V 1A1";
            var testCountry = "Canada";
            var testType = OrganizerCompanion.Core.Enums.Types.Home;
            var testCreatedDate = new DateTime(2023, 10, 1, 8, 0, 0);
            var testModifiedDate = new DateTime(2023, 10, 2, 10, 0, 0);

            _sut.Id = testId;
            _sut.Features = testFeatures;
            _sut.Street1 = testStreet1;
            _sut.Street2 = testStreet2;
            _sut.City = testCity;
            _sut.Province = testProvince;
            _sut.ZipCode = testZipCode;
            _sut.Country = testCountry;
            _sut.Type = testType;
            _sut.CreatedDate = testCreatedDate;
            _sut.ModifiedDate = testModifiedDate;

            // Act
            var interfaceInstance = (ICAAddressDTO)_sut;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceInstance.Id, Is.EqualTo(testId));
                Assert.That(interfaceInstance.Street1, Is.EqualTo(testStreet1));
                Assert.That(interfaceInstance.Street2, Is.EqualTo(testStreet2));
                Assert.That(interfaceInstance.City, Is.EqualTo(testCity));
                Assert.That(interfaceInstance.Province, Is.EqualTo(testProvince));
                Assert.That(interfaceInstance.ZipCode, Is.EqualTo(testZipCode));
                Assert.That(interfaceInstance.Country, Is.EqualTo(testCountry));
                Assert.That(interfaceInstance.Type, Is.EqualTo(testType));
                Assert.That(interfaceInstance.CreatedDate, Is.EqualTo(testCreatedDate));
                Assert.That(interfaceInstance.ModifiedDate, Is.EqualTo(testModifiedDate));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CAAddressDTO_InterfaceMethods_ShouldWork()
        {
            // Arrange
            var domainInterface = (IDomainEntity)_sut;
            var caAddressInterface = (ICAAddressDTO)_sut;

            // Act & Assert
            Assert.Multiple(() =>
            {
                // Test IDomainEntity interface methods
                Assert.Throws<NotImplementedException>(() => domainInterface.Cast<MockDomainEntity>());
                Assert.Throws<NotImplementedException>(() => domainInterface.ToJson());

                // Test property access through interface
                Assert.DoesNotThrow(() => caAddressInterface.Id = 500);
                Assert.DoesNotThrow(() => caAddressInterface.Street1 = "Interface Street");
                Assert.DoesNotThrow(() => caAddressInterface.Street2 = "Interface Unit");
                Assert.DoesNotThrow(() => caAddressInterface.City = "Interface City");
                Assert.DoesNotThrow(() => caAddressInterface.ZipCode = "M1M 1M1");
                Assert.DoesNotThrow(() => caAddressInterface.Country = "Interface Country");
                Assert.DoesNotThrow(() => caAddressInterface.Type = OrganizerCompanion.Core.Enums.Types.Work);

                // Verify changes through interface are reflected in concrete type
                Assert.That(_sut.Id, Is.EqualTo(500));
                Assert.That(_sut.Street1, Is.EqualTo("Interface Street"));
                Assert.That(_sut.Street2, Is.EqualTo("Interface Unit"));
                Assert.That(_sut.City, Is.EqualTo("Interface City"));
                Assert.That(_sut.ZipCode, Is.EqualTo("M1M 1M1"));
                Assert.That(_sut.Country, Is.EqualTo("Interface Country"));
                Assert.That(_sut.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Features_ShouldAllowLargeList()
        {
            // Arrange
            var largeFeatureList = new List<FeatureDTO>();
            for (int i = 0; i < 100; i++)
            {
                largeFeatureList.Add(new FeatureDTO { Id = i, FeatureName = $"Feature {i}", IsEnabled = i % 2 == 0 });
            }

            // Act
            _sut.Features = largeFeatureList;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Features, Has.Count.EqualTo(100));
                Assert.That(_sut.Features[0].FeatureName, Is.EqualTo("Feature 0"));
                Assert.That(_sut.Features[99].FeatureName, Is.EqualTo("Feature 99"));
                Assert.That(_sut.Features[50].IsEnabled, Is.True);
                Assert.That(_sut.Features[51].IsEnabled, Is.False);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Features_ShouldAllowNullAssignment()
        {
            // Arrange, Act & Assert
            Assert.DoesNotThrow(() => { _sut.Features = null!; });
        }

        [Test, Category("DataTransferObjects")]
        public void StringProperties_ShouldAcceptEmptyStrings()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _sut.Street1 = "");
                Assert.DoesNotThrow(() => _sut.Street2 = "");
                Assert.DoesNotThrow(() => _sut.City = "");
                Assert.DoesNotThrow(() => _sut.ZipCode = "");
                Assert.DoesNotThrow(() => _sut.Country = "");

                Assert.That(_sut.Street1, Is.EqualTo(""));
                Assert.That(_sut.Street2, Is.EqualTo(""));
                Assert.That(_sut.City, Is.EqualTo(""));
                Assert.That(_sut.ZipCode, Is.EqualTo(""));
                Assert.That(_sut.Country, Is.EqualTo(""));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void StringProperties_ShouldAcceptLongStrings()
        {
            // Arrange
            var longString = new string('A', 1000);

            // Act & Assert
            Assert.Multiple(() =>
            {
                _sut.Street1 = longString;
                Assert.That(_sut.Street1, Is.EqualTo(longString));

                _sut.Street2 = longString;
                Assert.That(_sut.Street2, Is.EqualTo(longString));

                _sut.City = longString;
                Assert.That(_sut.City, Is.EqualTo(longString));

                _sut.ZipCode = longString;
                Assert.That(_sut.ZipCode, Is.EqualTo(longString));

                _sut.Country = longString;
                Assert.That(_sut.Country, Is.EqualTo(longString));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void StringProperties_ShouldAcceptSpecialCharacters()
        {
            // Arrange
            var specialChars = "!@#$%^&*()_+-=[]{}|;':\",./<>?`~";

            // Act & Assert
            Assert.Multiple(() =>
            {
                _sut.Street1 = specialChars;
                Assert.That(_sut.Street1, Is.EqualTo(specialChars));

                _sut.Street2 = specialChars;
                Assert.That(_sut.Street2, Is.EqualTo(specialChars));

                _sut.City = specialChars;
                Assert.That(_sut.City, Is.EqualTo(specialChars));

                _sut.ZipCode = specialChars;
                Assert.That(_sut.ZipCode, Is.EqualTo(specialChars));

                _sut.Country = specialChars;
                Assert.That(_sut.Country, Is.EqualTo(specialChars));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void StringProperties_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var unicodeString = "测试地址 ñáéíóú Müller 🏠🏢🏭";

            // Act & Assert
            Assert.Multiple(() =>
            {
                _sut.Street1 = unicodeString;
                Assert.That(_sut.Street1, Is.EqualTo(unicodeString));

                _sut.Street2 = unicodeString;
                Assert.That(_sut.Street2, Is.EqualTo(unicodeString));

                _sut.City = unicodeString;
                Assert.That(_sut.City, Is.EqualTo(unicodeString));

                _sut.ZipCode = unicodeString;
                Assert.That(_sut.ZipCode, Is.EqualTo(unicodeString));

                _sut.Country = unicodeString;
                Assert.That(_sut.Country, Is.EqualTo(unicodeString));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateTimeProperties_ShouldAcceptBoundaryValues()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Test CreatedDate with boundary values
                _sut.CreatedDate = DateTime.MinValue;
                Assert.That(_sut.CreatedDate, Is.EqualTo(DateTime.MinValue));

                _sut.CreatedDate = DateTime.MaxValue;
                Assert.That(_sut.CreatedDate, Is.EqualTo(DateTime.MaxValue));

                // Test ModifiedDate with boundary values and null
                _sut.ModifiedDate = DateTime.MinValue;
                Assert.That(_sut.ModifiedDate, Is.EqualTo(DateTime.MinValue));

                _sut.ModifiedDate = DateTime.MaxValue;
                Assert.That(_sut.ModifiedDate, Is.EqualTo(DateTime.MaxValue));

                _sut.ModifiedDate = null;
                Assert.That(_sut.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptBoundaryValues()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Test minimum value (0)
                _sut.Id = 0;
                Assert.That(_sut.Id, Is.EqualTo(0));

                // Test maximum value
                _sut.Id = int.MaxValue;
                Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));

                // Test mid-range value
                _sut.Id = 1_000_000;
                Assert.That(_sut.Id, Is.EqualTo(1_000_000));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AllPropertiesSet_ShouldMaintainCorrectTypes()
        {
            // Arrange
            var testDate = new DateTime(2023, 11, 15, 14, 30, 45);

            // Act
            _sut.Id = 12345;
            _sut.Features = [new() { Id = 1, FeatureName = "TypeTest", IsEnabled = true }];
            _sut.Street1 = "Type Test Street";
            _sut.Street2 = "Type Test Unit";
            _sut.City = "Type Test City";
            _sut.Province = new CAProvince { Name = "TypeTestProvince", Abbreviation = "TP" };
            _sut.ZipCode = "T1T 1T1";
            _sut.Country = "Type Test Country";
            _sut.Type = OrganizerCompanion.Core.Enums.Types.Billing;
            _sut.CreatedDate = testDate;
            _sut.ModifiedDate = testDate.AddHours(2);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.TypeOf<int>());
                Assert.That(_sut.Features, Is.TypeOf<List<FeatureDTO>>());
                Assert.That(_sut.Street1, Is.TypeOf<string>());
                Assert.That(_sut.Street2, Is.TypeOf<string>());
                Assert.That(_sut.City, Is.TypeOf<string>());
                Assert.That(_sut.Province, Is.TypeOf<CAProvince>());
                Assert.That(_sut.ZipCode, Is.TypeOf<string>());
                Assert.That(_sut.Country, Is.TypeOf<string>());
                Assert.That(_sut.Type, Is.TypeOf<OrganizerCompanion.Core.Enums.Types>());
                Assert.That(_sut.CreatedDate, Is.TypeOf<DateTime>());
                Assert.That(_sut.ModifiedDate, Is.TypeOf<DateTime>(), "ModifiedDate should be DateTime when set to non-null value");
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CAAddressDTO_DefaultValuesVerification()
        {
            // Arrange & Act
            var freshDto = new CAAddressDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(freshDto.Id, Is.EqualTo(0), "Id should default to 0");
                Assert.That(freshDto.Features, Is.Not.Null, "Features should not be null");
                Assert.That(freshDto.Features, Is.Empty, "Features should be empty list");
                Assert.That(freshDto.Street1, Is.Null, "Street1 should default to null");
                Assert.That(freshDto.Street2, Is.Null, "Street2 should default to null");
                Assert.That(freshDto.City, Is.Null, "City should default to null");
                Assert.That(freshDto.Province, Is.Null, "Province should default to null");
                Assert.That(freshDto.ZipCode, Is.Null, "ZipCode should default to null");
                Assert.That(freshDto.Country, Is.Null, "Country should default to null");
                Assert.That(freshDto.Type, Is.Null, "Type should default to null");
                Assert.That(freshDto.CreatedDate, Is.TypeOf<DateTime>(), "CreatedDate should be DateTime");
                Assert.That(freshDto.ModifiedDate, Is.Null, "ModifiedDate should default to null");
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_WithDifferentTypes_ShouldAlwaysThrowNotImplementedException()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => _sut.Cast<CAAddressDTO>());
                Assert.Throws<NotImplementedException>(() => _sut.Cast<MockDomainEntity>());
                Assert.Throws<NotImplementedException>(() => ((IDomainEntity)_sut).Cast<ICAAddressDTO>());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_RepeatedCalls_ShouldAlwaysThrowNotImplementedException()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => _sut.ToJson());
                Assert.Throws<NotImplementedException>(() => _sut.ToJson());
                Assert.Throws<NotImplementedException>(() => ((IDomainEntity)_sut).ToJson());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CAAddressDTO_PropertyChaining_ShouldWorkCorrectly()
        {
      // Arrange & Act
      var chainedDto = new CAAddressDTO
      {
        Id = 1,
        Street1 = "Chain Street",
        Street2 = "Chain Unit",
        City = "Chain City",
        Type = OrganizerCompanion.Core.Enums.Types.Other
      };

      // Assert
      Assert.Multiple(() =>
            {
                Assert.That(chainedDto.Id, Is.EqualTo(1));
                Assert.That(chainedDto.Street1, Is.EqualTo("Chain Street"));
                Assert.That(chainedDto.Street2, Is.EqualTo("Chain Unit"));
                Assert.That(chainedDto.City, Is.EqualTo("Chain City"));
                Assert.That(chainedDto.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Other));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Type_ShouldAcceptAllEnumValuesByName()
        {
            // Arrange
            var enumNames = Enum.GetNames(typeof(OrganizerCompanion.Core.Enums.Types));

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var enumName in enumNames)
                {
                    var enumValue = Enum.Parse<OrganizerCompanion.Core.Enums.Types>(enumName);
                    _sut.Type = enumValue;
                    Assert.That(_sut.Type, Is.EqualTo(enumValue), $"Enum value {enumName} should be settable");
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Province_ShouldSupportMultipleINationalSubdivisionTypes()
        {
            // Arrange
            var mockSubdivision1 = new MockNationalSubdivision { Name = "Test Province 1", Abbreviation = "TP1" };
            var mockSubdivision2 = new MockNationalSubdivision { Name = "Test Province 2", Abbreviation = "TP2" };
            var caProvince = new CAProvince { Name = "British Columbia", Abbreviation = "BC" };

            // Act & Assert
            Assert.Multiple(() =>
            {
                // Test first mock
                _sut.Province = mockSubdivision1;
                Assert.That(_sut.Province, Is.EqualTo(mockSubdivision1));
                Assert.That(_sut.Province.Name, Is.EqualTo("Test Province 1"));

                // Test second mock
                _sut.Province = mockSubdivision2;
                Assert.That(_sut.Province, Is.EqualTo(mockSubdivision2));
                Assert.That(_sut.Province.Name, Is.EqualTo("Test Province 2"));

                // Test CAProvince
                _sut.Province = caProvince;
                Assert.That(_sut.Province, Is.EqualTo(caProvince));
                Assert.That(_sut.Province.Name, Is.EqualTo("British Columbia"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CAAddressDTO_ComprehensiveAttributeValidation()
        {
            // Arrange
            var type = typeof(CAAddressDTO);

            // Act & Assert - Verify all expected attributes are present
            Assert.Multiple(() =>
            {
                // Verify all properties have Required attributes
                var requiredProperties = new[] { "Id", "Features", "Street1", "Street2", "City", "Province", "ZipCode", "Country", "Type", "CreatedDate", "ModifiedDate" };
                foreach (var propName in requiredProperties)
                {
                    var property = type.GetProperty(propName);
                    Assert.That(property?.GetCustomAttribute<RequiredAttribute>(), Is.Not.Null, $"{propName} should have Required attribute");
                }

                // Verify Id has Range attribute
                var idProperty = type.GetProperty("Id");
                var idRangeAttr = idProperty?.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();
                Assert.That(idRangeAttr?.Minimum, Is.EqualTo(0));
                Assert.That(idRangeAttr?.Maximum, Is.EqualTo(int.MaxValue));
                Assert.That(idRangeAttr?.ErrorMessage, Is.EqualTo("Id must be a non-negative number."));

                // Verify all properties have JsonPropertyName attributes
                var jsonPropertyMappings = new Dictionary<string, string>
                {
                    { "Id", "id" }, { "Features", "features" }, { "Street1", "street" }, { "Street2", "street2" },
                    { "City", "city" }, { "Province", "province" }, { "ZipCode", "zipCode" }, { "Country", "country" },
                    { "Type", "type" }, { "CreatedDate", "createdDate" }, { "ModifiedDate", "modifiedDate" }
                };

                foreach (var mapping in jsonPropertyMappings)
                {
                    var property = type.GetProperty(mapping.Key);
                    var jsonAttr = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
                    Assert.That(jsonAttr?.Name, Is.EqualTo(mapping.Value), $"{mapping.Key} should have JsonPropertyName '{mapping.Value}'");
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Features_ShouldAllowModificationAfterInitialization()
        {
            // Arrange
            var initialFeatures = new List<FeatureDTO>
            {
                new() { Id = 1, FeatureName = "Initial Feature", IsEnabled = true }
            };
            _sut.Features = initialFeatures;

            var newFeatures = new List<FeatureDTO>
            {
                new() { Id = 2, FeatureName = "New Feature A", IsEnabled = false },
                new() { Id = 3, FeatureName = "New Feature B", IsEnabled = true }
            };

            // Act
            _sut.Features = newFeatures;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Features, Is.EqualTo(newFeatures));
                Assert.That(_sut.Features, Has.Count.EqualTo(2));
                Assert.That(_sut.Features[0].FeatureName, Is.EqualTo("New Feature A"));
                Assert.That(_sut.Features[1].FeatureName, Is.EqualTo("New Feature B"));
                Assert.That(_sut.Features, Is.Not.EqualTo(initialFeatures));
            });
        }

        #region Mock Classes
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime CreatedDate { get; }
            public DateTime? ModifiedDate { get; set; } = default;
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
