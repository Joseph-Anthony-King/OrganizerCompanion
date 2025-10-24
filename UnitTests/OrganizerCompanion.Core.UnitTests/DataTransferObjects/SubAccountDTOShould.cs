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
                ModifiedDate = DateTime.UtcNow
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
                Assert.That(_sut.CreatedDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
                Assert.That(_sut.ModifiedDate, Is.Null);
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
                createdDate: testDate,
                modifiedDate: modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(123));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(456));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(_sut.LinkedEntity, Is.SameAs(_testEntity));
                Assert.That(_sut.AccountId, Is.EqualTo(100));
                Assert.That(_sut.Account, Is.SameAs(_testAccount));
                Assert.That(_sut.CreatedDate, Is.EqualTo(testDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(modifiedDate));
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
                createdDate: testDate,
                modifiedDate: modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(789));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(_testEntity.Id));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(_sut.LinkedEntity, Is.SameAs(_testEntity));
                Assert.That(_sut.AccountId, Is.EqualTo(_testAccount.Id));
                Assert.That(_sut.Account, Is.SameAs(_testAccount));
                Assert.That(_sut.CreatedDate, Is.EqualTo(testDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(modifiedDate));
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
                    createdDate: testDate,
                    modifiedDate: null);
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
      Assert.Multiple(() =>
      {

        // Act & Assert
        Assert.That(property?.CanWrite, Is.False);
        Assert.That(_sut.LinkedEntityType, Is.Null);
      });
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
        public void CreatedDate_ShouldReturnFieldValue()
        {
            // Arrange & Act
            var createdDate = _sut.CreatedDate;

            // Assert
            Assert.That(createdDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            Assert.That(createdDate, Is.GreaterThan(DateTime.UtcNow.AddMinutes(-1)));
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 6, 20, 14, 15, 30);

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

        #endregion

        #region Explicit Interface Implementation Tests

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange, Act & Assert
            Assert.Throws<NotImplementedException>(() =>
            {
                _sut.Cast<MockDomainEntity>();
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldThrowNotImplementedException()
        {
            // Arrange, Act & Assert
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
        public void CreatedDate_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.CreatedDate));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.CreatedDate));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("createdDate"));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.ModifiedDate));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldHaveJsonPropertyNameAttribute()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.ModifiedDate));

            // Act
            var jsonPropertyNameAttribute = property?.GetCustomAttribute<JsonPropertyNameAttribute>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(jsonPropertyNameAttribute, Is.Not.Null);
                Assert.That(jsonPropertyNameAttribute?.Name, Is.EqualTo("modifiedDate"));
            });
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
            _sut.ModifiedDate = modifiedDate;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(999));
                Assert.That(_sut.AccountId, Is.EqualTo(100));
                Assert.That(_sut.Account, Is.SameAs(_testAccount));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(50));
                Assert.That(_sut.LinkedEntity, Is.SameAs(_testEntity));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(modifiedDate));
                Assert.That(_sut.CreatedDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
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
                ModifiedDate = DateTime.UtcNow
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccountDTO.Id, Is.EqualTo(888));
                Assert.That(subAccountDTO.AccountId, Is.EqualTo(_testAccount.Id));
                Assert.That(subAccountDTO.Account, Is.SameAs(_testAccount));
                Assert.That(subAccountDTO.LinkedEntityId, Is.EqualTo(_testEntity.Id));
                Assert.That(subAccountDTO.LinkedEntity, Is.SameAs(_testEntity));
                Assert.That(subAccountDTO.ModifiedDate, Is.Not.Null);
                Assert.That(subAccountDTO.CreatedDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void SimplifiedConstructor_WithValidLinkedEntity_ShouldSetLinkedEntityType()
        {
            // Arrange
            var testDate = DateTime.UtcNow;
            var modifiedDate = DateTime.UtcNow.AddDays(1);

            // Act - This covers the linkedEntity?.GetType().Name logic
            _sut = new SubAccountDTO(
                id: 100,
                linkedEntity: _testEntity,
                account: _testAccount,
                createdDate: testDate,
                modifiedDate: modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.LinkedEntityType, Is.EqualTo("MockDomainEntity"));
                Assert.That(_sut.LinkedEntityId, Is.EqualTo(_testEntity.Id));
                Assert.That(_sut.LinkedEntity, Is.SameAs(_testEntity));
            });
        }

        #endregion

        #region Comprehensive Coverage Tests

        [Test, Category("Boundary")]
        public void HandleBoundaryValues_ShouldWorkCorrectly()
        {
            // Act & Assert for Id boundary values
            _sut.Id = int.MaxValue;
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));

            _sut.Id = int.MinValue;
            Assert.That(_sut.Id, Is.EqualTo(int.MinValue));

            _sut.Id = 0;
            Assert.That(_sut.Id, Is.EqualTo(0));

            // Act & Assert for AccountId boundary values
            _sut.AccountId = int.MaxValue;
            Assert.That(_sut.AccountId, Is.EqualTo(int.MaxValue));

            _sut.AccountId = int.MinValue;
            Assert.That(_sut.AccountId, Is.EqualTo(int.MinValue));

            _sut.AccountId = 0;
            Assert.That(_sut.AccountId, Is.EqualTo(0));

            // Act & Assert for LinkedEntityId boundary values
            _sut.LinkedEntityId = int.MaxValue;
            Assert.That(_sut.LinkedEntityId, Is.EqualTo(int.MaxValue));

            _sut.LinkedEntityId = int.MinValue;
            Assert.That(_sut.LinkedEntityId, Is.EqualTo(int.MinValue));

            _sut.LinkedEntityId = 0;
            Assert.That(_sut.LinkedEntityId, Is.EqualTo(0));
        }

        [Test, Category("DateTime")]
        public void HandleDateTimePrecision_ShouldWorkCorrectly()
        {
            // Arrange
            var preciseDateTime = new DateTime(2025, 10, 20, 14, 35, 42, 999);

            // Act & Assert for ModifiedDate
            _sut.ModifiedDate = preciseDateTime;
            Assert.That(_sut.ModifiedDate, Is.EqualTo(preciseDateTime));

            // Test null assignment
            _sut.ModifiedDate = null;
            Assert.That(_sut.ModifiedDate, Is.Null);
        }

        [Test, Category("Threading")]
        public void CreatedDate_FromMultipleAccesses_ShouldMaintainConsistentValue()
        {
            // Arrange
            var originalDate = _sut.CreatedDate;

            // Act - Access multiple times
            var date1 = _sut.CreatedDate;
            var date2 = _sut.CreatedDate;
            var date3 = _sut.CreatedDate;

            // Assert - Should all be the same
            Assert.Multiple(() =>
            {
                Assert.That(date1, Is.EqualTo(originalDate));
                Assert.That(date2, Is.EqualTo(originalDate));
                Assert.That(date3, Is.EqualTo(originalDate));
                Assert.That(date1, Is.EqualTo(date2));
                Assert.That(date2, Is.EqualTo(date3));
            });
        }

        [Test, Category("Interface")]
        public void ISubAccountDTO_AllProperties_ShouldMatchDirectAccess()
        {
            // Arrange
            var testDate = DateTime.UtcNow;
            _sut.Id = 555;
            _sut.AccountId = 333;
            _sut.Account = _testAccount;
            _sut.LinkedEntityId = 777;
            _sut.LinkedEntity = _testEntity;
            _sut.ModifiedDate = testDate;

            var interfaceView = (ISubAccountDTO)_sut;

            // Act & Assert - All properties should match
            Assert.Multiple(() =>
            {
                Assert.That(interfaceView.Id, Is.EqualTo(_sut.Id));
                Assert.That(interfaceView.AccountId, Is.EqualTo(_sut.AccountId));
                Assert.That(interfaceView.Account, Is.SameAs(_sut.Account));
                Assert.That(interfaceView.LinkedEntityId, Is.EqualTo(_sut.LinkedEntityId));
                Assert.That(interfaceView.LinkedEntityType, Is.EqualTo(_sut.LinkedEntityType));
                Assert.That(interfaceView.LinkedEntity, Is.SameAs(_sut.LinkedEntity));
                Assert.That(interfaceView.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(interfaceView.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Interface")]
        public void IDomainEntity_CastAndToJson_ShouldThrowNotImplementedException()
        {
            // Arrange
            var domainEntity = (IDomainEntity)_sut;

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => domainEntity.Cast<IDomainEntity>());
                Assert.Throws<NotImplementedException>(() => domainEntity.ToJson());
            });
        }

        [Test, Category("Comprehensive")]
        public void JsonConstructor_WithNullParameters_ShouldSetNullsCorrectly()
        {
            // Arrange
            var testDate = DateTime.UtcNow;

            // Act
            var dto = new SubAccountDTO(
                id: 0,
                linkedEntityId: 0,
                linktedEntityType: null,
                linkedEntity: null,
                accountId: null,
                account: null!,
                createdDate: testDate,
                modifiedDate: null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto.Id, Is.EqualTo(0));
                Assert.That(dto.LinkedEntityId, Is.EqualTo(0));
                Assert.That(dto.LinkedEntityType, Is.Null);
                Assert.That(dto.LinkedEntity, Is.Null);
                Assert.That(dto.AccountId, Is.Null);
                Assert.That(dto.Account, Is.Null);
                Assert.That(dto.CreatedDate, Is.EqualTo(testDate));
                Assert.That(dto.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Comprehensive")]
        public void JsonConstructor_WithCompleteValidData_ShouldSetAllPropertiesCorrectly()
        {
            // Arrange
            var id = 9999;
            var linkedEntityId = 8888;
            var linkedEntityType = "TestEntityType";
            var accountId = 7777;
            var createdDate = new DateTime(2025, 1, 1, 12, 0, 0);
            var modifiedDate = new DateTime(2025, 1, 2, 15, 30, 45);

            // Act
            var dto = new SubAccountDTO(
                id: id,
                linkedEntityId: linkedEntityId,
                linktedEntityType: linkedEntityType,
                linkedEntity: _testEntity,
                accountId: accountId,
                account: _testAccount,
                createdDate: createdDate,
                modifiedDate: modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto.Id, Is.EqualTo(id));
                Assert.That(dto.LinkedEntityId, Is.EqualTo(linkedEntityId));
                Assert.That(dto.LinkedEntityType, Is.EqualTo(linkedEntityType));
                Assert.That(dto.LinkedEntity, Is.SameAs(_testEntity));
                Assert.That(dto.AccountId, Is.EqualTo(accountId));
                Assert.That(dto.Account, Is.SameAs(_testAccount));
                Assert.That(dto.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(dto.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("Validation")]
        public void LinkedEntityType_ReadOnlyProperty_ShouldNotHaveSetter()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.LinkedEntityType));

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(property, Is.Not.Null);
                Assert.That(property!.CanRead, Is.True);
                Assert.That(property.CanWrite, Is.False);
                Assert.That(property.GetSetMethod(), Is.Null);
                Assert.That(property.GetSetMethod(true), Is.Null); // Check for private setter too
            });
        }

        [Test, Category("Validation")]
        public void CreatedDate_ReadOnlyProperty_ShouldNotHavePublicSetter()
        {
            // Arrange
            var property = typeof(SubAccountDTO).GetProperty(nameof(SubAccountDTO.CreatedDate));

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(property, Is.Not.Null);
                Assert.That(property!.CanRead, Is.True);
                Assert.That(property.CanWrite, Is.False);
                Assert.That(property.GetSetMethod(), Is.Null);
            });
        }

        [Test, Category("Type")]
        public void TypeInformation_ShouldBeCorrect()
        {
            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.GetType(), Is.EqualTo(typeof(SubAccountDTO)));
                Assert.That(_sut.GetType().Name, Is.EqualTo("SubAccountDTO"));
                Assert.That(_sut.GetType().Namespace, Is.EqualTo("OrganizerCompanion.Core.Models.DataTransferObject"));
                Assert.That(_sut is ISubAccountDTO, Is.True);
                Assert.That(_sut is IDomainEntity, Is.True);
            });
        }

        [Test, Category("Interface")]
        public void InterfaceHierarchy_ShouldBeCorrect()
        {
            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(typeof(ISubAccountDTO).IsAssignableFrom(typeof(SubAccountDTO)), Is.True);
                Assert.That(typeof(IDomainEntity).IsAssignableFrom(typeof(SubAccountDTO)), Is.True);
                Assert.That(typeof(IDomainEntity).IsAssignableFrom(typeof(ISubAccountDTO)), Is.True);
            });
        }

        [Test, Category("Comprehensive")]
        public void DefaultValues_ShouldBeSetCorrectly()
        {
            // Arrange & Act
            var freshDTO = new SubAccountDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(freshDTO.Id, Is.EqualTo(0));
                Assert.That(freshDTO.AccountId, Is.Null);
                Assert.That(freshDTO.Account, Is.Null);
                Assert.That(freshDTO.LinkedEntityId, Is.EqualTo(0));
                Assert.That(freshDTO.LinkedEntityType, Is.Null);
                Assert.That(freshDTO.LinkedEntity, Is.Null);
                Assert.That(freshDTO.CreatedDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
                Assert.That(freshDTO.CreatedDate, Is.GreaterThan(DateTime.UtcNow.AddSeconds(-1)));
                Assert.That(freshDTO.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Edge")]
        public void AllNullableProperties_ShouldAcceptNull()
        {
            // Act & Assert - Test all nullable properties can be set to null
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => _sut.AccountId = null);
                Assert.DoesNotThrow(() => _sut.Account = null);
                Assert.DoesNotThrow(() => _sut.LinkedEntity = null);
                Assert.DoesNotThrow(() => _sut.ModifiedDate = null);

                Assert.That(_sut.AccountId, Is.Null);
                Assert.That(_sut.Account, Is.Null);
                Assert.That(_sut.LinkedEntity, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Edge")]
        public void PropertyAssignments_ShouldNotAffectOtherProperties()
        {
            // Arrange
            var originalCreatedDate = _sut.CreatedDate;
            var originalLinkedEntityType = _sut.LinkedEntityType;

            // Act - Set various properties
            _sut.Id = 999;
            _sut.AccountId = 888;
            _sut.Account = _testAccount;
            _sut.LinkedEntityId = 777;
            _sut.LinkedEntity = _testEntity;
            _sut.ModifiedDate = DateTime.UtcNow;

            // Assert - Read-only properties should remain unchanged
            Assert.Multiple(() =>
            {
                Assert.That(_sut.CreatedDate, Is.EqualTo(originalCreatedDate));
                Assert.That(_sut.LinkedEntityType, Is.EqualTo(originalLinkedEntityType));
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
            public DateTime CreatedDate { get; } = DateTime.UtcNow;
            public DateTime? ModifiedDate { get; set; } = default;
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => throw new NotImplementedException();
        }

        #endregion
    }
}
