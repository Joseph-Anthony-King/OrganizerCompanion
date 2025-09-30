using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NUnit.Framework;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    [TestFixture]
    internal class AccountDTOShould
    {
        private AccountDTO _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new AccountDTO();
        }

        [Test, Category("DataTransferObjects")]
        public void DefaultConstructor_ShouldCreateAccountDTOWithDefaultValues()
        {
            // Arrange & Act
            _sut = new AccountDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.AccountName, Is.Null);
                Assert.That(_sut.AccountNumber, Is.Null);
                Assert.That(_sut.Features, Is.Not.Null);
                Assert.That(_sut.Features, Is.Empty);
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
        public void AccountName_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedAccountName = "Test Account";

            // Act
            _sut.AccountName = expectedAccountName;

            // Assert
            Assert.That(_sut.AccountName, Is.EqualTo(expectedAccountName));
        }

        [Test, Category("DataTransferObjects")]
        public void AccountName_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.AccountName = null;

            // Assert
            Assert.That(_sut.AccountName, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AccountNumber_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedAccountNumber = "ACC-123456";

            // Act
            _sut.AccountNumber = expectedAccountNumber;

            // Assert
            Assert.That(_sut.AccountNumber, Is.EqualTo(expectedAccountNumber));
        }

        [Test, Category("DataTransferObjects")]
        public void AccountNumber_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.AccountNumber = null;

            // Assert
            Assert.That(_sut.AccountNumber, Is.Null);
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
        public void ExplicitInterfaceFeatures_Get_ShouldConvertFeaturesToIFeatureDTO()
        {
            // Arrange
            var features = new List<FeatureDTO>
            {
                new() { Id = 1, FeatureName = "Test Feature", IsEnabled = true }
            };
            _sut.Features = features;
            var accountInterface = (IAccountDTO)_sut;

            // Act
            var result = accountInterface.Features;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Count, Is.EqualTo(1));
                Assert.That(result[0], Is.TypeOf<FeatureDTO>());
                Assert.That(result[0].Id, Is.EqualTo(1));
                Assert.That(result[0].FeatureName, Is.EqualTo("Test Feature"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ExplicitInterfaceFeatures_Set_ShouldConvertIFeatureDTOToFeatures()
        {
            // Arrange
            var interfaceFeatures = new List<IFeatureDTO>
            {
                new FeatureDTO { Id = 2, FeatureName = "Interface Feature", IsEnabled = false }
            };
            var accountInterface = (IAccountDTO)_sut;

            // Act
            accountInterface.Features = interfaceFeatures;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Features, Is.Not.Null);
                Assert.That(_sut.Features.Count, Is.EqualTo(1));
                Assert.That(_sut.Features[0].Id, Is.EqualTo(2));
                Assert.That(_sut.Features[0].FeatureName, Is.EqualTo("Interface Feature"));
                Assert.That(_sut.Features[0].IsEnabled, Is.False);
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
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.Id));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AccountName_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.AccountName));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AccountNumber_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.AccountNumber));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Features_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(AccountDTO).GetProperty(nameof(AccountDTO.Features));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AccountDTO_ShouldImplementIAccountDTO()
        {
            // Arrange & Act
            var accountDTO = new AccountDTO();

            // Assert
            Assert.That(accountDTO, Is.InstanceOf<IAccountDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void AccountDTO_Properties_ShouldBeSettableInChain()
        {
            // Arrange & Act
            var accountDTO = new AccountDTO
            {
                Id = 999,
                AccountName = "Chained Account",
                AccountNumber = "CHAIN-999",
                Features = new List<FeatureDTO>
                {
                    new() { Id = 1, FeatureName = "Chained Feature", IsEnabled = true }
                }
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountDTO.Id, Is.EqualTo(999));
                Assert.That(accountDTO.AccountName, Is.EqualTo("Chained Account"));
                Assert.That(accountDTO.AccountNumber, Is.EqualTo("CHAIN-999"));
                Assert.That(accountDTO.Features.Count, Is.EqualTo(1));
                Assert.That(accountDTO.Features[0].FeatureName, Is.EqualTo("Chained Feature"));
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
