using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NUnit.Framework;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    [TestFixture]
    internal class AnnonymousUserDTOShould
    {
        private AnnonymousUserDTO _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new AnnonymousUserDTO();
        }

        [Test, Category("DataTransferObjects")]
        public void DefaultConstructor_ShouldCreateAnnonymousUserDTOWithDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now.AddSeconds(-1);

            // Act
            _sut = new AnnonymousUserDTO();
            var afterCreation = DateTime.Now.AddSeconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.UserName, Is.EqualTo(string.Empty));
                Assert.That(_sut.AccountId, Is.EqualTo(0));
                Assert.That(_sut.IsCast, Is.EqualTo(false));
                Assert.That(_sut.CastId, Is.EqualTo(0));
                Assert.That(_sut.CastType, Is.Null);
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.DateModified, Is.Null);
                Assert.That(_sut, Is.Not.Null);
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
        public void Id_ShouldAcceptZeroValue()
        {
            // Arrange & Act
            _sut.Id = 0;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptPositiveValues()
        {
            // Arrange
            int[] testValues = { 1, 100, 999, int.MaxValue };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (int value in testValues)
                {
                    _sut.Id = value;
                    Assert.That(_sut.Id, Is.EqualTo(value));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_ShouldGetAndSetValue()
        {
            // Arrange
            int expectedAccountId = 123;

            // Act
            _sut.AccountId = expectedAccountId;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(expectedAccountId));
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_ShouldAcceptZeroValue()
        {
            // Arrange & Act
            _sut.AccountId = 0;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(0));
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_ShouldAcceptPositiveValues()
        {
            // Arrange
            int[] testValues = { 1, 100, 999, int.MaxValue };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (int value in testValues)
                {
                    _sut.AccountId = value;
                    Assert.That(_sut.AccountId, Is.EqualTo(value));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_ShouldAcceptNegativeValues()
        {
            // Arrange
            int negativeValue = -123;

            // Act
            _sut.AccountId = negativeValue;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(negativeValue));
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedUserName = "TestUser";

            // Act
            _sut.UserName = expectedUserName;

            // Assert
            Assert.That(_sut.UserName, Is.EqualTo(expectedUserName));
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_DefaultValue_ShouldBeEmptyString()
        {
            // Arrange & Act
            var dto = new AnnonymousUserDTO();

            // Assert
            Assert.That(dto.UserName, Is.EqualTo(string.Empty));
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldAcceptEmptyString()
        {
            // Arrange & Act
            _sut.UserName = "";

            // Assert
            Assert.That(_sut.UserName, Is.EqualTo(""));
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldAcceptMaxLengthString()
        {
            // Arrange
            var maxLengthUserName = new string('A', 100); // 100 characters

            // Act
            _sut.UserName = maxLengthUserName;

            // Assert
            Assert.That(_sut.UserName, Is.EqualTo(maxLengthUserName));
            Assert.That(_sut.UserName.Length, Is.EqualTo(100));
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldAcceptLongString()
        {
            // Arrange
            var longUserName = new string('B', 150); // 150 characters (exceeds validation but property should still accept it)

            // Act
            _sut.UserName = longUserName;

            // Assert
            Assert.That(_sut.UserName, Is.EqualTo(longUserName));
            Assert.That(_sut.UserName.Length, Is.EqualTo(150));
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldAcceptSpecialCharacters()
        {
            // Arrange
            var specialCharsUserName = "user@domain.com_123-test!";

            // Act
            _sut.UserName = specialCharsUserName;

            // Assert
            Assert.That(_sut.UserName, Is.EqualTo(specialCharsUserName));
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var unicodeUserName = "用户名_ñáéíóú_🌟";

            // Act
            _sut.UserName = unicodeUserName;

            // Assert
            Assert.That(_sut.UserName, Is.EqualTo(unicodeUserName));
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.UserName));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldHaveMinLengthAttribute()
        {
            // Arrange
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.UserName));

            // Act
            var minLengthAttribute = property?.GetCustomAttribute<System.ComponentModel.DataAnnotations.MinLengthAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(minLengthAttribute, Is.Not.Null);
                Assert.That(minLengthAttribute?.Length, Is.EqualTo(1));
                Assert.That(minLengthAttribute?.ErrorMessage, Is.EqualTo("User Name must be at least 1 character long."));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldHaveMaxLengthAttribute()
        {
            // Arrange
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.UserName));

            // Act
            var maxLengthAttribute = property?.GetCustomAttribute<System.ComponentModel.DataAnnotations.MaxLengthAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(maxLengthAttribute, Is.Not.Null);
                Assert.That(maxLengthAttribute?.Length, Is.EqualTo(100));
                Assert.That(maxLengthAttribute?.ErrorMessage, Is.EqualTo("User Name cannot exceed 100 characters."));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void UserName_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.UserName));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("userName"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_Get_ShouldReturnValue()
        {
            // Arrange
            var beforeCreation = DateTime.Now.AddSeconds(-1);
            var dto = new AnnonymousUserDTO();
            var afterCreation = DateTime.Now.AddSeconds(1);

            // Act
            var dateCreated = dto.DateCreated;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(dateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
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
        public void DateModified_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 6, 20, 14, 15, 30);

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
        public void DateModified_DefaultValue_ShouldBeNull()
        {
            // Arrange & Act
            var dto = new AnnonymousUserDTO();

            // Assert
            Assert.That(dto.DateModified, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_ShouldGetAndSetValue()
        {
            // Arrange
            bool expectedValue = true;

            // Act
            _sut.IsCast = expectedValue;

            // Assert
            Assert.That(_sut.IsCast, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_DefaultValue_ShouldBeFalse()
        {
            // Arrange & Act
            var dto = new AnnonymousUserDTO();

            // Assert
            Assert.That(dto.IsCast, Is.EqualTo(false));
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_ShouldGetAndSetValue()
        {
            // Arrange
            int expectedValue = 123;

            // Act
            _sut.CastId = expectedValue;

            // Assert
            Assert.That(_sut.CastId, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_DefaultValue_ShouldBeZero()
        {
            // Arrange & Act
            var dto = new AnnonymousUserDTO();

            // Assert
            Assert.That(dto.CastId, Is.EqualTo(0));
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedValue = "TestType";

            // Act
            _sut.CastType = expectedValue;

            // Assert
            Assert.That(_sut.CastType, Is.EqualTo(expectedValue));
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_DefaultValue_ShouldBeNull()
        {
            // Arrange & Act
            var dto = new AnnonymousUserDTO();

            // Assert
            Assert.That(dto.CastType, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.CastType = null;

            // Assert
            Assert.That(_sut.CastType, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_ShouldAcceptNegativeValues()
        {
            // Arrange
            int negativeValue = -456;

            // Act
            _sut.CastId = negativeValue;

            // Assert
            Assert.That(_sut.CastId, Is.EqualTo(negativeValue));
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_ShouldSupportMinAndMaxValues()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                _sut.CastId = int.MinValue;
                Assert.That(_sut.CastId, Is.EqualTo(int.MinValue));
                
                _sut.CastId = int.MaxValue;
                Assert.That(_sut.CastId, Is.EqualTo(int.MaxValue));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_ShouldToggleCorrectly()
        {
            // Arrange
            var initialValue = _sut.IsCast;

            // Act
            _sut.IsCast = !initialValue;
            var toggledValue = _sut.IsCast;
            _sut.IsCast = !toggledValue;
            var reToggedValue = _sut.IsCast;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(toggledValue, Is.Not.EqualTo(initialValue));
                Assert.That(reToggedValue, Is.EqualTo(initialValue));
            });
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
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.Id));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveRangeAttribute()
        {
            // Arrange
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.Id));

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
        public void AccountId_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.AccountId));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_ShouldHaveRangeAttribute()
        {
            // Arrange
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.AccountId));

            // Act
            var rangeAttribute = property?.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(rangeAttribute, Is.Not.Null);
                Assert.That(rangeAttribute?.Minimum, Is.EqualTo(0));
                Assert.That(rangeAttribute?.Maximum, Is.EqualTo(int.MaxValue));
                Assert.That(rangeAttribute?.ErrorMessage, Is.EqualTo("Account Id must be a non-negative number."));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.DateCreated));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.DateModified));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AnnonymousUserDTO_ShouldImplementIAnnonymousUserDTO()
        {
            // Arrange & Act
            var anonymousUserDTO = new AnnonymousUserDTO();

            // Assert
            Assert.That(anonymousUserDTO, Is.InstanceOf<IAnnonymousUserDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void AnnonymousUserDTO_ShouldImplementIDomainEntity()
        {
            // Arrange & Act
            var anonymousUserDTO = new AnnonymousUserDTO();

            // Assert
            Assert.That(anonymousUserDTO, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void PropertyInitializers_ShouldWork()
        {
            // Arrange
            var testDate = new DateTime(2023, 7, 10, 12, 30, 0);

            // Act
            var anonymousUserDTO = new AnnonymousUserDTO
            {
                Id = 789,
                UserName = "InitializerTestUser",
                AccountId = 456,
                IsCast = true,
                CastId = 321,
                CastType = "TestCastType",
                DateCreated = testDate,
                DateModified = testDate.AddDays(1)
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(anonymousUserDTO.Id, Is.EqualTo(789));
                Assert.That(anonymousUserDTO.UserName, Is.EqualTo("InitializerTestUser"));
                Assert.That(anonymousUserDTO.AccountId, Is.EqualTo(456));
                Assert.That(anonymousUserDTO.IsCast, Is.EqualTo(true));
                Assert.That(anonymousUserDTO.CastId, Is.EqualTo(321));
                Assert.That(anonymousUserDTO.CastType, Is.EqualTo("TestCastType"));
                Assert.That(anonymousUserDTO.DateCreated, Is.EqualTo(testDate));
                Assert.That(anonymousUserDTO.DateModified, Is.EqualTo(testDate.AddDays(1)));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_ShouldSupportMinValue()
        {
            // Arrange & Act
            _sut.AccountId = int.MinValue;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(int.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_ShouldSupportMaxValue()
        {
            // Arrange & Act
            _sut.AccountId = int.MaxValue;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldSupportMinValue()
        {
            // Arrange & Act
            _sut.Id = int.MinValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldSupportMaxValue()
        {
            // Arrange & Act
            _sut.Id = int.MaxValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void JsonPropertyName_Attributes_ShouldBePresent()
        {
            // Arrange
            var idProperty = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.Id));
            var userNameProperty = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.UserName));
            var accountIdProperty = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.AccountId));
            var isCastProperty = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.IsCast));
            var castIdProperty = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.CastId));
            var dateCreatedProperty = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.DateCreated));
            var dateModifiedProperty = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.DateModified));

            // Act
            var idJsonAttribute = idProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
            var userNameJsonAttribute = userNameProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
            var accountIdJsonAttribute = accountIdProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
            var isCastJsonAttribute = isCastProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
            var castIdJsonAttribute = castIdProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
            var dateCreatedJsonAttribute = dateCreatedProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
            var dateModifiedJsonAttribute = dateModifiedProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(idJsonAttribute, Is.Not.Null);
                Assert.That(idJsonAttribute?.Name, Is.EqualTo("id"));
                Assert.That(userNameJsonAttribute, Is.Not.Null);
                Assert.That(userNameJsonAttribute?.Name, Is.EqualTo("userName"));
                Assert.That(accountIdJsonAttribute, Is.Not.Null);
                Assert.That(accountIdJsonAttribute?.Name, Is.EqualTo("accountId"));
                Assert.That(isCastJsonAttribute, Is.Not.Null);
                Assert.That(isCastJsonAttribute?.Name, Is.EqualTo("isCast"));
                Assert.That(castIdJsonAttribute, Is.Not.Null);
                Assert.That(castIdJsonAttribute?.Name, Is.EqualTo("castId"));
                Assert.That(dateCreatedJsonAttribute, Is.Not.Null);
                Assert.That(dateCreatedJsonAttribute?.Name, Is.EqualTo("dateCreated"));
                Assert.That(dateModifiedJsonAttribute, Is.Not.Null);
                Assert.That(dateModifiedJsonAttribute?.Name, Is.EqualTo("dateModified"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.CastType));

            // Act
            var jsonIgnoreAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();

            // Assert
            Assert.That(jsonIgnoreAttribute, Is.Not.Null, "CastType property should have JsonIgnore attribute");
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_ShouldHaveJsonIgnoreCondition()
        {
            // Arrange
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.IsCast));

            // Act
            var jsonIgnoreAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonIgnoreAttribute, Is.Not.Null, "IsCast property should have JsonIgnore attribute");
                Assert.That(jsonIgnoreAttribute?.Condition, Is.EqualTo(System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_ShouldHaveJsonIgnoreCondition()
        {
            // Arrange
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.CastId));

            // Act
            var jsonIgnoreAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonIgnoreAttribute, Is.Not.Null, "CastId property should have JsonIgnore attribute");
                Assert.That(jsonIgnoreAttribute?.Condition, Is.EqualTo(System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AllProperties_ShouldMaintainConsistency()
        {
            // Arrange
            var testId = 999;
            var testUserName = "ConsistencyTestUser";
            var testAccountId = 888;
            var testIsCast = true;
            var testCastId = 777;
            var testCastType = "ConsistencyTestType";
            var testDateCreated = new DateTime(2023, 8, 15, 10, 0, 0);
            var testDateModified = new DateTime(2023, 8, 16, 12, 30, 0);

            // Act
            _sut.Id = testId;
            _sut.UserName = testUserName;
            _sut.AccountId = testAccountId;
            _sut.IsCast = testIsCast;
            _sut.CastId = testCastId;
            _sut.CastType = testCastType;
            _sut.DateCreated = testDateCreated;
            _sut.DateModified = testDateModified;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(testId));
                Assert.That(_sut.UserName, Is.EqualTo(testUserName));
                Assert.That(_sut.AccountId, Is.EqualTo(testAccountId));
                Assert.That(_sut.IsCast, Is.EqualTo(testIsCast));
                Assert.That(_sut.CastId, Is.EqualTo(testCastId));
                Assert.That(_sut.CastType, Is.EqualTo(testCastType));
                Assert.That(_sut.DateCreated, Is.EqualTo(testDateCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(testDateModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AnnonymousUserDTO_AsInterface_ShouldRetainAllProperties()
        {
            // Arrange
            var testId = 100;
            var testUserName = "InterfaceTestUser";
            var testAccountId = 200;
            var testIsCast = true;
            var testCastId = 300;
            var testCastType = "InterfaceTest";
            var testDateCreated = new DateTime(2023, 9, 1, 8, 0, 0);
            var testDateModified = new DateTime(2023, 9, 2, 10, 0, 0);

            _sut.Id = testId;
            _sut.UserName = testUserName;
            _sut.AccountId = testAccountId;
            _sut.IsCast = testIsCast;
            _sut.CastId = testCastId;
            _sut.CastType = testCastType;
            _sut.DateCreated = testDateCreated;
            _sut.DateModified = testDateModified;

            // Act
            var interfaceInstance = (IAnnonymousUserDTO)_sut;

            // Assert - Only test properties that exist in the interface
            Assert.Multiple(() =>
            {
                Assert.That(interfaceInstance.Id, Is.EqualTo(testId));
                Assert.That(interfaceInstance.UserName, Is.EqualTo(testUserName));
                Assert.That(interfaceInstance.AccountId, Is.EqualTo(testAccountId));
                Assert.That(interfaceInstance.DateCreated, Is.EqualTo(testDateCreated));
                Assert.That(interfaceInstance.DateModified, Is.EqualTo(testDateModified));
                
                // Verify concrete type still has all properties
                Assert.That(_sut.IsCast, Is.EqualTo(testIsCast));
                Assert.That(_sut.CastId, Is.EqualTo(testCastId));
                Assert.That(_sut.CastType, Is.EqualTo(testCastType));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AnnonymousUserDTO_InterfaceMethods_ShouldWork()
        {
            // Arrange
            var domainInterface = (IDomainEntity)_sut;
            var anonymousUserInterface = (IAnnonymousUserDTO)_sut;

            // Act & Assert
            Assert.Multiple(() =>
            {
                // Test IDomainEntity interface methods
                Assert.Throws<NotImplementedException>(() => domainInterface.Cast<MockDomainEntity>());
                Assert.Throws<NotImplementedException>(() => domainInterface.ToJson());

                // Test property access through interface (only properties that exist in interface)
                Assert.DoesNotThrow(() => anonymousUserInterface.Id = 500);
                Assert.DoesNotThrow(() => anonymousUserInterface.UserName = "InterfaceTestUser");
                Assert.DoesNotThrow(() => anonymousUserInterface.AccountId = 600);

                // Verify changes through interface are reflected in concrete type
                Assert.That(_sut.Id, Is.EqualTo(500));
                Assert.That(_sut.UserName, Is.EqualTo("InterfaceTestUser"));
                Assert.That(_sut.AccountId, Is.EqualTo(600));
                
                // Test concrete type properties directly
                Assert.DoesNotThrow(() => _sut.IsCast = true);
                Assert.DoesNotThrow(() => _sut.CastId = 700);
                Assert.DoesNotThrow(() => _sut.CastType = "TestType");
                
                Assert.That(_sut.IsCast, Is.True);
                Assert.That(_sut.CastId, Is.EqualTo(700));
                Assert.That(_sut.CastType, Is.EqualTo("TestType"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_ShouldAcceptEmptyString()
        {
            // Arrange & Act
            _sut.CastType = "";

            // Assert
            Assert.That(_sut.CastType, Is.EqualTo(""));
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_ShouldAcceptLongString()
        {
            // Arrange
            var longString = new string('A', 1000);

            // Act
            _sut.CastType = longString;

            // Assert
            Assert.That(_sut.CastType, Is.EqualTo(longString));
            Assert.That(_sut.CastType?.Length, Is.EqualTo(1000));
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_ShouldAcceptSpecialCharacters()
        {
            // Arrange
            var specialChars = "!@#$%^&*()_+-=[]{}|;':\",./<>?`~";

            // Act
            _sut.CastType = specialChars;

            // Assert
            Assert.That(_sut.CastType, Is.EqualTo(specialChars));
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var unicodeString = "测试字符串 ñáéíóú 🌟🎯🚀";

            // Act
            _sut.CastType = unicodeString;

            // Assert
            Assert.That(_sut.CastType, Is.EqualTo(unicodeString));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldAcceptMinDateTime()
        {
            // Arrange & Act
            _sut.DateCreated = DateTime.MinValue;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldAcceptMaxDateTime()
        {
            // Arrange & Act
            _sut.DateCreated = DateTime.MaxValue;

            // Assert
            Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptMinDateTime()
        {
            // Arrange & Act
            _sut.DateModified = DateTime.MinValue;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldAcceptMaxDateTime()
        {
            // Arrange & Act
            _sut.DateModified = DateTime.MaxValue;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.CastType));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("castType"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_ShouldHaveJsonIgnoreCondition()
        {
            // Arrange
            var property = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.CastType));

            // Act
            var jsonIgnoreAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonIgnoreAttribute, Is.Not.Null);
                Assert.That(jsonIgnoreAttribute?.Condition, Is.EqualTo(System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AllPropertiesSet_ShouldMaintainCorrectTypes()
        {
            // Arrange
            var testDate = new DateTime(2023, 10, 15, 14, 30, 45);

            // Act
            _sut.Id = 12345;
            _sut.AccountId = 67890;
            _sut.IsCast = true;
            _sut.CastId = 54321;
            _sut.CastType = "TypeTest";
            _sut.DateCreated = testDate;
            _sut.DateModified = testDate.AddHours(2);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.TypeOf<int>());
                Assert.That(_sut.AccountId, Is.TypeOf<int>());
                Assert.That(_sut.IsCast, Is.TypeOf<bool>());
                Assert.That(_sut.CastId, Is.TypeOf<int>());
                Assert.That(_sut.CastType, Is.TypeOf<string>());
                Assert.That(_sut.DateCreated, Is.TypeOf<DateTime>());
                Assert.That(_sut.DateModified, Is.TypeOf<DateTime>(), "DateModified should be DateTime when set to non-null value");
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AnnonymousUserDTO_DefaultValuesVerification()
        {
            // Arrange & Act
            var freshDto = new AnnonymousUserDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(freshDto.Id, Is.EqualTo(0), "Id should default to 0");
                Assert.That(freshDto.UserName, Is.EqualTo(string.Empty), "UserName should default to empty string");
                Assert.That(freshDto.AccountId, Is.EqualTo(0), "AccountId should default to 0");
                Assert.That(freshDto.IsCast, Is.EqualTo(false), "IsCast should default to false");
                Assert.That(freshDto.CastId, Is.EqualTo(0), "CastId should default to 0");
                Assert.That(freshDto.CastType, Is.Null, "CastType should default to null");
                Assert.That(freshDto.DateCreated, Is.TypeOf<DateTime>(), "DateCreated should be DateTime");
                Assert.That(freshDto.DateModified, Is.Null, "DateModified should default to null");
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_WithDifferentTypes_ShouldAlwaysThrowNotImplementedException()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => _sut.Cast<AnnonymousUserDTO>());
                Assert.Throws<NotImplementedException>(() => _sut.Cast<MockDomainEntity>());
                Assert.Throws<NotImplementedException>(() => ((IDomainEntity)_sut).Cast<IAnnonymousUserDTO>());
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
        public void AnnonymousUserDTO_PropertyChaining_ShouldWorkCorrectly()
        {
      // Arrange & Act
      var chainedDto = new AnnonymousUserDTO
      {
        Id = 1,
        UserName = "ChainTestUser",
        AccountId = 2,
        IsCast = true,
        CastId = 3,
        CastType = "ChainTest"
      };

      // Assert
      Assert.Multiple(() =>
            {
                Assert.That(chainedDto.Id, Is.EqualTo(1));
                Assert.That(chainedDto.UserName, Is.EqualTo("ChainTestUser"));
                Assert.That(chainedDto.AccountId, Is.EqualTo(2));
                Assert.That(chainedDto.IsCast, Is.True);
                Assert.That(chainedDto.CastId, Is.EqualTo(3));
                Assert.That(chainedDto.CastType, Is.EqualTo("ChainTest"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AllIntegerProperties_ShouldAcceptFullRange()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Test Id with full range
                _sut.Id = int.MinValue;
                Assert.That(_sut.Id, Is.EqualTo(int.MinValue));
                _sut.Id = 0;
                Assert.That(_sut.Id, Is.EqualTo(0));
                _sut.Id = int.MaxValue;
                Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));

                // Test AccountId with full range
                _sut.AccountId = int.MinValue;
                Assert.That(_sut.AccountId, Is.EqualTo(int.MinValue));
                _sut.AccountId = 0;
                Assert.That(_sut.AccountId, Is.EqualTo(0));
                _sut.AccountId = int.MaxValue;
                Assert.That(_sut.AccountId, Is.EqualTo(int.MaxValue));

                // Test CastId with full range
                _sut.CastId = int.MinValue;
                Assert.That(_sut.CastId, Is.EqualTo(int.MinValue));
                _sut.CastId = 0;
                Assert.That(_sut.CastId, Is.EqualTo(0));
                _sut.CastId = int.MaxValue;
                Assert.That(_sut.CastId, Is.EqualTo(int.MaxValue));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AllDateTimeProperties_ShouldAcceptFullRange()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Test DateCreated with boundary values
                _sut.DateCreated = DateTime.MinValue;
                Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.MinValue));
                _sut.DateCreated = DateTime.MaxValue;
                Assert.That(_sut.DateCreated, Is.EqualTo(DateTime.MaxValue));

                // Test DateModified with boundary values and null
                _sut.DateModified = DateTime.MinValue;
                Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MinValue));
                _sut.DateModified = DateTime.MaxValue;
                Assert.That(_sut.DateModified, Is.EqualTo(DateTime.MaxValue));
                _sut.DateModified = null;
                Assert.That(_sut.DateModified, Is.Null);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AnnonymousUserDTO_ComprehensiveAttributeValidation()
        {
            // Arrange
            var type = typeof(AnnonymousUserDTO);

            // Act & Assert - Verify all expected attributes are present
            Assert.Multiple(() =>
            {
                // Id property attributes
                var idProperty = type.GetProperty(nameof(AnnonymousUserDTO.Id));
                Assert.That(idProperty?.GetCustomAttribute<RequiredAttribute>(), Is.Not.Null);
                Assert.That(idProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>()?.Name, Is.EqualTo("id"));
                var idRangeAttr = idProperty?.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();
                Assert.That(idRangeAttr?.Minimum, Is.EqualTo(0));
                Assert.That(idRangeAttr?.Maximum, Is.EqualTo(int.MaxValue));

                // AccountId property attributes
                var accountIdProperty = type.GetProperty(nameof(AnnonymousUserDTO.AccountId));
                Assert.That(accountIdProperty?.GetCustomAttribute<RequiredAttribute>(), Is.Not.Null);
                Assert.That(accountIdProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>()?.Name, Is.EqualTo("accountId"));
                var accountIdRangeAttr = accountIdProperty?.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();
                Assert.That(accountIdRangeAttr?.Minimum, Is.EqualTo(0));
                Assert.That(accountIdRangeAttr?.Maximum, Is.EqualTo(int.MaxValue));

                // IsCast property attributes
                var isCastProperty = type.GetProperty(nameof(AnnonymousUserDTO.IsCast));
                Assert.That(isCastProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>()?.Name, Is.EqualTo("isCast"));
                Assert.That(isCastProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>()?.Condition, 
                    Is.EqualTo(System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingDefault));

                // CastId property attributes
                var castIdProperty = type.GetProperty(nameof(AnnonymousUserDTO.CastId));
                Assert.That(castIdProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>()?.Name, Is.EqualTo("castId"));
                Assert.That(castIdProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>()?.Condition, 
                    Is.EqualTo(System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull));

                // CastType property attributes
                var castTypeProperty = type.GetProperty(nameof(AnnonymousUserDTO.CastType));
                Assert.That(castTypeProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>()?.Name, Is.EqualTo("castType"));
                Assert.That(castTypeProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>()?.Condition, 
                    Is.EqualTo(System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull));

                // DateCreated property attributes
                var dateCreatedProperty = type.GetProperty(nameof(AnnonymousUserDTO.DateCreated));
                Assert.That(dateCreatedProperty?.GetCustomAttribute<RequiredAttribute>(), Is.Not.Null);
                Assert.That(dateCreatedProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>()?.Name, Is.EqualTo("dateCreated"));

                // DateModified property attributes
                var dateModifiedProperty = type.GetProperty(nameof(AnnonymousUserDTO.DateModified));
                Assert.That(dateModifiedProperty?.GetCustomAttribute<RequiredAttribute>(), Is.Not.Null);
                Assert.That(dateModifiedProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>()?.Name, Is.EqualTo("dateModified"));
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
        #endregion
    }
}
