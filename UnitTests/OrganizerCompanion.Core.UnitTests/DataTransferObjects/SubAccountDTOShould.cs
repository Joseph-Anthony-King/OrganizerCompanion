using System.ComponentModel.DataAnnotations;
using System.Reflection;
using System.Text.Json.Serialization;
using NUnit.Framework;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    [TestFixture]
    internal class SubAccountDTOShould
    {
        private SubAccountDTO _sut;
        private AccountDTO _testAccount;
        private MockDomainEntity _testEntity;

        [SetUp]
        public void SetUp()
        {
            _sut = new SubAccountDTO();
            _testAccount = new AccountDTO
            {
                Id = 100,
                AccountName = "Test Account",
                AccountNumber = "TEST-100"
            };
            _testEntity = new MockDomainEntity
            {
                Id = 50,
                DateModified = DateTime.UtcNow
            };
        }

        #region Constructor Tests

        [Test, Category("DataTransferObjects")]
        public void DefaultConstructor_ShouldCreateSubAccountDTOWithDefaultValues()
        {
            // Arrange & Act
            _sut = new SubAccountDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.AccountId, Is.Null);
                Assert.That(_sut.Account, Is.Null);
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(0));
                Assert.That(_sut.LinkedEntityType, Is.Null);
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(DateTime.UtcNow));
                Assert.That(_sut.DateModified, Is.Null);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void JsonConstructor_ShouldCreateSubAccountDTOWithProvidedValues()
        {
            // Arrange
            var testDate = new DateTime(2023, 5, 15, 10, 30, 45);
            var modifiedDate = new DateTime(2023, 6, 20, 14, 15, 30);

            // Act
            _sut = new SubAccountDTO(
                id: 123,
                linkedEntityId: 456,
                linktedEntityType: "MockDomainEntity",
                linkedEntity: _testEntity,
                accountId: 100,
                account: _testAccount,
                dateCreated: testDate,
                dateModified: modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(123));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(456));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(_sut.LinkedEntity, Is.SameAs(_testEntity));
                Assert.That(_sut.AccountId, Is.EqualTo(100));
                Assert.That(_sut.Account, Is.SameAs(_testAccount));
                Assert.That(_sut.DateCreated, Is.EqualTo(testDate));
                Assert.That(_sut.DateModified, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void SimplifiedConstructor_ShouldCreateSubAccountDTOWithCalculatedValues()
        {
            // Arrange
            var testDate = DateTime.UtcNow;
            var modifiedDate = DateTime.UtcNow.AddDays(1);

            // Act
            _sut = new SubAccountDTO(
                id: 789,
                linkedEntity: _testEntity,
                account: _testAccount,
                dateCreated: testDate,
                dateModified: modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(789));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(_testEntity.Id));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(_sut.LinkedEntity, Is.SameAs(_testEntity));
                Assert.That(_sut.AccountId, Is.EqualTo(_testAccount.Id));
                Assert.That(_sut.Account, Is.SameAs(_testAccount));
                Assert.That(_sut.DateCreated, Is.EqualTo(testDate));
                Assert.That(_sut.DateModified, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void SimplifiedConstructor_WithNullLinkedEntity_ShouldHandleNullCorrectly()
        {
            // Arrange
            var testDate = DateTime.UtcNow;

            // Act & Assert
            Assert.Throws<NullReferenceException>(() =>
            {
                _sut = new SubAccountDTO(
                    id: 999,
                    linkedEntity: null,
                    account: _testAccount,
                    dateCreated: testDate,
                    dateModified: null);
            });
        }

        #endregion

        #region Property Tests

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
        public void AccountId_ShouldGetAndSetValue()
        {
            // Arrange
            int? expectedAccountId = 456;

            // Act
            _sut.AccountId = expectedAccountId;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(expectedAccountId));
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.AccountId = null;

            // Assert
            Assert.That(_sut.AccountId, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Account_ShouldGetAndSetValue()
        {
            // Act
            _sut.Account = _testAccount;

            // Assert
            Assert.That(_sut.Account, Is.SameAs(_testAccount));
        }

        [Test, Category("DataTransferObjects")]
        public void Account_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.Account = null;

            // Assert
            Assert.That(_sut.Account, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void LinkedEntityId_ShouldGetAndSetValue()
        {
            // Arrange
            int expectedEntityId = 789;

            // Act
            _sut.LinkedEntityId = expectedEntityId;

            // Assert
            Assert.That(_sut.LinkedEntityId, Is.EqualTo(expectedEntityId));
        }

        [Test, Category("DataTransferObjects")]
        public void LinkedEntityType_ShouldBeReadOnly()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.LinkedEntityType));

            // Act & Assert
            Assert.That(property?.CanWrite, Is.False);
            Assert.That(_sut.LinkedEntityType, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void LinkedEntity_ShouldGetAndSetValue()
        {
            // Act
            _sut.LinkedEntity = _testEntity;

            // Assert
            Assert.That(_sut.LinkedEntity, Is.SameAs(_testEntity));
        }

        [Test, Category("DataTransferObjects")]
        public void LinkedEntity_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.LinkedEntity = null;

            // Assert
            Assert.That(_sut.LinkedEntity, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldReturnFieldValue()
        {
            // Arrange & Act
            var createdDate = _sut.DateCreated;

            // Assert
            Assert.That(createdDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            Assert.That(createdDate, Is.GreaterThan(DateTime.UtcNow.AddMinutes(-1)));
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

        #endregion

        #region Explicit Interface Implementation Tests

        [Test, Category("DataTransferObjects")]
        public void IsCast_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                var isCast = _sut.IsCast;
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                _sut.IsCast = true;
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                var castId = _sut.CastId;
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                _sut.CastId = 123;
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                var castType = _sut.CastType;
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                _sut.CastType = "TestType";
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                _sut.Cast<MockDomainEntity>();
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                _sut.ToJson();
            });
        }

        #endregion

        #region Attribute Tests

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.Id));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.Id));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("id"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveRangeAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.Id));

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
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.AccountId));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.AccountId));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("accountId"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void AccountId_ShouldHaveRangeAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.AccountId));

            // Act
            var rangeAttribute = property?.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(rangeAttribute, Is.Not.Null);
                Assert.That(rangeAttribute?.Minimum, Is.EqualTo(0));
                Assert.That(rangeAttribute?.Maximum, Is.EqualTo(int.MaxValue));
                Assert.That(rangeAttribute?.ErrorMessage, Is.EqualTo("AccountId must be a non-negative number."));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Account_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.Account));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Account_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.Account));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("account"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void LinkedEntityId_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.LinkedEntityId));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void LinkedEntityId_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.LinkedEntityId));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("linkedEntityId"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void LinkedEntityId_ShouldHaveRangeAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.LinkedEntityId));

            // Act
            var rangeAttribute = property?.GetCustomAttribute<System.ComponentModel.DataAnnotations.RangeAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(rangeAttribute, Is.Not.Null);
                Assert.That(rangeAttribute?.Minimum, Is.EqualTo(0));
                Assert.That(rangeAttribute?.Maximum, Is.EqualTo(int.MaxValue));
                Assert.That(rangeAttribute?.ErrorMessage, Is.EqualTo("LinkedEntityId must be a non-negative number."));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void LinkedEntityType_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.LinkedEntityType));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void LinkedEntityType_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.LinkedEntityType));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("linkedEntityType"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void LinkedEntity_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.LinkedEntity));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void LinkedEntity_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.LinkedEntity));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("linkedEntity"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.DateCreated));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.DateCreated));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("dateCreated"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.DateModified));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.DateModified));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("dateModified"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.IsCast));

            // Act
            var jsonIgnoreAttribute = property?.GetCustomAttribute<JsonIgnoreAttribute>();

            // Assert
            Assert.That(jsonIgnoreAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.CastId));

            // Act
            var jsonIgnoreAttribute = property?.GetCustomAttribute<JsonIgnoreAttribute>();

            // Assert
            Assert.That(jsonIgnoreAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.CastType));

            // Act
            var jsonIgnoreAttribute = property?.GetCustomAttribute<JsonIgnoreAttribute>();

            // Assert
            Assert.That(jsonIgnoreAttribute, Is.Not.Null);
        }

        #endregion

        #region Interface Implementation Tests

        [Test, Category("DataTransferObjects")]
        public void SubAccountDTO_ShouldImplementISubAccountDTO()
        {
            // Arrange & Act
            var subAccountDTO = new SubAccountDTO();

            // Assert
            Assert.That(subAccountDTO, Is.InstanceOf<ISubAccountDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void SubAccountDTO_ShouldImplementIDomainEntity()
        {
            // Arrange & Act
            var subAccountDTO = new SubAccountDTO();

            // Assert
            Assert.That(subAccountDTO, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void ISubAccountDTO_Account_Get_ReturnsCorrectValue()
        {
            // Arrange
            _sut.Account = _testAccount;
            var subAccountInterface = (ISubAccountDTO)_sut;

            // Act
            var interfaceAccount = subAccountInterface.Account;

            // Assert
            Assert.That(interfaceAccount, Is.SameAs(_testAccount));
            Assert.That(interfaceAccount, Is.InstanceOf<IAccountDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void ISubAccountDTO_Account_GetNull_ReturnsNull()
        {
            // Arrange
            _sut.Account = null;
            var subAccountInterface = (ISubAccountDTO)_sut;

            // Act
            var interfaceAccount = subAccountInterface.Account;

            // Assert
            Assert.That(interfaceAccount, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void ISubAccountDTO_Account_Set_UpdatesAccount()
        {
            // Arrange
            var subAccountInterface = (ISubAccountDTO)_sut;

            // Act
            subAccountInterface.Account = _testAccount;

            // Assert
            Assert.That(_sut.Account, Is.SameAs(_testAccount));
            Assert.That(_sut.Account, Is.InstanceOf<AccountDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void ISubAccountDTO_Account_SetNull_SetsAccountToNull()
        {
            // Arrange
            var subAccountInterface = (ISubAccountDTO)_sut;
            _sut.Account = _testAccount; // Start with a non-null account

            // Act
            subAccountInterface.Account = null;

            // Assert
            Assert.That(_sut.Account, Is.Null);
        }

        #endregion

        #region Comprehensive Integration Tests

        [Test, Category("DataTransferObjects")]
        public void SubAccountDTO_WithAllProperties_ShouldMaintainConsistency()
        {
            // Arrange
            var testDate = new DateTime(2023, 5, 15, 10, 30, 45);
            var modifiedDate = new DateTime(2023, 6, 20, 14, 15, 30);

            // Act
            _sut.Id = 999;
            _sut.AccountId = 100;
            _sut.Account = _testAccount;
            _sut.LinkedEntityId = 50;
            _sut.LinkedEntity = _testEntity;
            _sut.DateModified = modifiedDate;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(999));
                Assert.That(_sut.AccountId, Is.EqualTo(100));
                Assert.That(_sut.Account, Is.SameAs(_testAccount));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(50));
                Assert.That(_sut.LinkedEntity, Is.SameAs(_testEntity));
                Assert.That(_sut.DateModified, Is.EqualTo(modifiedDate));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(DateTime.UtcNow));
                Assert.That(_sut.LinkedEntityType, Is.Null);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void SubAccountDTO_Properties_ShouldBeSettableInChain()
        {
            // Arrange & Act
            var subAccountDTO = new SubAccountDTO
            {
                Id = 888,
                AccountId = _testAccount.Id,
                Account = _testAccount,
                LinkedEntityId = _testEntity.Id,
                LinkedEntity = _testEntity,
                DateModified = DateTime.UtcNow
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccountDTO.Id, Is.EqualTo(888));
                Assert.That(subAccountDTO.AccountId, Is.EqualTo(_testAccount.Id));
                Assert.That(subAccountDTO.Account, Is.SameAs(_testAccount));
                Assert.That(subAccountDTO.LinkedEntityId, Is.EqualTo(_testEntity.Id));
                Assert.That(subAccountDTO.LinkedEntity, Is.SameAs(_testEntity));
                Assert.That(subAccountDTO.DateModified, Is.Not.Null);
                Assert.That(subAccountDTO.DateCreated, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        #endregion

        #region Mock Classes

        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime DateCreated { get; } = DateTime.UtcNow;
            public DateTime? DateModified { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => throw new NotImplementedException();
        }

        #endregion
    }
}
