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
                Assert.That(_sut.AccountId, Is.EqualTo(0));
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
                Assert.That(rangeAttribute?.Minimum, Is.EqualTo(1));
                Assert.That(rangeAttribute?.Maximum, Is.EqualTo(int.MaxValue));
                Assert.That(rangeAttribute?.ErrorMessage, Is.EqualTo("ID must be a positive number"));
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
                Assert.That(rangeAttribute?.Minimum, Is.EqualTo(1));
                Assert.That(rangeAttribute?.Maximum, Is.EqualTo(int.MaxValue));
                Assert.That(rangeAttribute?.ErrorMessage, Is.EqualTo("ID must be a positive number"));
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
                AccountId = 456,
                DateCreated = testDate,
                DateModified = testDate.AddDays(1)
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(anonymousUserDTO.Id, Is.EqualTo(789));
                Assert.That(anonymousUserDTO.AccountId, Is.EqualTo(456));
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
            var accountIdProperty = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.AccountId));
            var dateCreatedProperty = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.DateCreated));
            var dateModifiedProperty = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.DateModified));

            // Act
            var idJsonAttribute = idProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
            var accountIdJsonAttribute = accountIdProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
            var dateCreatedJsonAttribute = dateCreatedProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
            var dateModifiedJsonAttribute = dateModifiedProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(idJsonAttribute, Is.Not.Null);
                Assert.That(idJsonAttribute?.Name, Is.EqualTo("id"));
                Assert.That(accountIdJsonAttribute, Is.Not.Null);
                Assert.That(accountIdJsonAttribute?.Name, Is.EqualTo("accountId"));
                Assert.That(dateCreatedJsonAttribute, Is.Not.Null);
                Assert.That(dateCreatedJsonAttribute?.Name, Is.EqualTo("dateCreated"));
                Assert.That(dateModifiedJsonAttribute, Is.Not.Null);
                Assert.That(dateModifiedJsonAttribute?.Name, Is.EqualTo("dateModified"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Interface_Properties_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var properties = new[]
            {
                nameof(AnnonymousUserDTO.IsCast),
                nameof(AnnonymousUserDTO.CastId),
                nameof(AnnonymousUserDTO.CastType)
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var propertyName in properties)
                {
                    var property = typeof(AnnonymousUserDTO).GetProperty(propertyName);
                    var jsonIgnoreAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();
                    Assert.That(jsonIgnoreAttribute, Is.Not.Null, $"Property {propertyName} should have JsonIgnore attribute");
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AllProperties_ShouldMaintainConsistency()
        {
            // Arrange
            var testId = 999;
            var testAccountId = 888;
            var testDateCreated = new DateTime(2023, 8, 15, 10, 0, 0);
            var testDateModified = new DateTime(2023, 8, 16, 12, 30, 0);

            // Act
            _sut.Id = testId;
            _sut.AccountId = testAccountId;
            _sut.DateCreated = testDateCreated;
            _sut.DateModified = testDateModified;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(testId));
                Assert.That(_sut.AccountId, Is.EqualTo(testAccountId));
                Assert.That(_sut.DateCreated, Is.EqualTo(testDateCreated));
                Assert.That(_sut.DateModified, Is.EqualTo(testDateModified));
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
