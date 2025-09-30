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
            // Arrange & Act
            _sut = new AnnonymousUserDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.AccountId, Is.EqualTo(0));
                Assert.That(_sut, Is.Not.Null);
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
        public void Id_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.Id; });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.Id = 123; });
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
        public void DateCreated_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.DateCreated; });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.DateModified; });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.DateModified = DateTime.Now; });
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
        public void AccountId_PropertyInitializer_ShouldWork()
        {
            // Arrange & Act
            var anonymousUserDTO = new AnnonymousUserDTO
            {
                AccountId = 456
            };

            // Assert
            Assert.That(anonymousUserDTO.AccountId, Is.EqualTo(456));
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
        public void JsonPropertyName_Attributes_ShouldBePresent()
        {
            // Arrange
            var idProperty = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.Id));
            var accountIdProperty = typeof(AnnonymousUserDTO).GetProperty(nameof(AnnonymousUserDTO.AccountId));

            // Act
            var idJsonAttribute = idProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
            var accountIdJsonAttribute = accountIdProperty?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(idJsonAttribute, Is.Not.Null);
                Assert.That(idJsonAttribute?.Name, Is.EqualTo("id"));
                Assert.That(accountIdJsonAttribute, Is.Not.Null);
                Assert.That(accountIdJsonAttribute?.Name, Is.EqualTo("accountId"));
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
                nameof(AnnonymousUserDTO.CastType),
                nameof(AnnonymousUserDTO.DateCreated),
                nameof(AnnonymousUserDTO.DateModified)
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
