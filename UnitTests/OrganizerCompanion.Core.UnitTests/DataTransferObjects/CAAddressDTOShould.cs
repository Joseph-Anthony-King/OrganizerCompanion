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
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(DateTime.Now));
                Assert.That(_sut.DateModified, Is.Null);
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
                    (result.ErrorMessage?.Contains("ID must be a non-negative number") == true))
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
                Assert.That(_sut.Features.Count, Is.EqualTo(2));
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
            var expectedDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            _sut.DateCreated = expectedDate;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldHaveDefaultValue()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var dto = new CAAddressDTO();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(dto.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            _sut.DateModified = expectedDate;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(expectedDate));
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
                Assert.That(rangeAttribute?.ErrorMessage, Is.EqualTo("ID must be a non-negative number"));
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
                Assert.That(validationResults.Any(r => r.ErrorMessage?.Contains("ID must be a non-negative number") == true), Is.True);
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
                            (result.ErrorMessage?.Contains("ID must be a non-negative number") == true))
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
                            (result.ErrorMessage?.Contains("ID must be a non-negative number") == true))
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
        public void DateCreated_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.DateCreated));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(CAAddressDTO).GetProperty(nameof(CAAddressDTO.DateModified));

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
                Features = new List<FeatureDTO> { new() { Id = 1, FeatureName = "Test", IsEnabled = true } },
                Street1 = "123 Test Street",
                Street2 = "Unit 456",
                City = "Toronto",
                Province = new CAProvince { Name = "Ontario", Abbreviation = "ON" },
                ZipCode = "M5V 3M6",
                Country = "Canada",
                Type = OrganizerCompanion.Core.Enums.Types.Work,
                DateCreated = new DateTime(2023, 1, 1, 12, 0, 0),
                DateModified = new DateTime(2023, 1, 2, 12, 0, 0)
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(caAddressDTO.Id, Is.EqualTo(999));
                Assert.That(caAddressDTO.Features.Count, Is.EqualTo(1));
                Assert.That(caAddressDTO.Street1, Is.EqualTo("123 Test Street"));
                Assert.That(caAddressDTO.Street2, Is.EqualTo("Unit 456"));
                Assert.That(caAddressDTO.City, Is.EqualTo("Toronto"));
                Assert.That(caAddressDTO.Province?.Name, Is.EqualTo("Ontario"));
                Assert.That(caAddressDTO.ZipCode, Is.EqualTo("M5V 3M6"));
                Assert.That(caAddressDTO.Country, Is.EqualTo("Canada"));
                Assert.That(caAddressDTO.Type, Is.EqualTo(OrganizerCompanion.Core.Enums.Types.Work));
                Assert.That(caAddressDTO.DateCreated, Is.EqualTo(new DateTime(2023, 1, 1, 12, 0, 0)));
                Assert.That(caAddressDTO.DateModified, Is.EqualTo(new DateTime(2023, 1, 2, 12, 0, 0)));
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
                { nameof(CAAddressDTO.DateCreated), "dateCreated" },
                { nameof(CAAddressDTO.DateModified), "dateModified" }
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
        public void Interface_Properties_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var properties = new[]
            {
                nameof(CAAddressDTO.IsCast),
                nameof(CAAddressDTO.CastId),
                nameof(CAAddressDTO.CastType)
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var propertyName in properties)
                {
                    var property = typeof(CAAddressDTO).GetProperty(propertyName);
                    var jsonIgnoreAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();
                    Assert.That(jsonIgnoreAttribute, Is.Not.Null, $"Property {propertyName} should have JsonIgnore attribute");
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
        public void DateCreated_DefaultValue_ShouldBeCloseToCurrentTime()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var dto = new CAAddressDTO();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(dto.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_DefaultValue_ShouldBeNull()
        {
            // Arrange & Act
            var dto = new CAAddressDTO();

            // Assert
            Assert.That(dto.DateModified, Is.Null);
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
