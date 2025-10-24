using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class SubAccountShould
    {
        private Account _testAccount;
        private User _testLinkedEntity;
        private Organization _testOrganization;
        private readonly DateTime _testCreatedDate = new(2023, 1, 1, 12, 0, 0);
        private readonly DateTime _testModifiedDate = new(2023, 1, 2, 12, 0, 0);

        // Test implementation of ISubAccountDTO that properly handles null values
        private class TestSubAccountDTO : ISubAccountDTO
        {
            public int Id { get; set; }
            public int? AccountId { get; set; }
            public IAccountDTO? Account { get; set; }
            public int LinkedEntityId { get; set; }
            public string? LinkedEntityType { get; set; }
            public IDomainEntity? LinkedEntity { get; set; }
            public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
            public DateTime? ModifiedDate { get; set; } = default;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => throw new NotImplementedException();
        }

        // Helper method to perform validation
        private static IList<ValidationResult> ValidateModel(object model)
        {
            var validationResults = new List<ValidationResult>();
            var ctx = new ValidationContext(model, null, null);
            Validator.TryValidateObject(model, ctx, validationResults, true);
            return validationResults;
        }

        [SetUp]
        public void SetUp()
        {
            // Create a valid linked entity
            _testLinkedEntity = new User
            {
                Id = 100,
                FirstName = "John",
                LastName = "Doe"
            };

            _testOrganization = new Organization
            {
                Id = 300,
                OrganizationName = "Test Org"
            };

            // Create a valid account
            _testAccount = new Account
            {
                Id = 200,
                AccountName = "TestAccount",
                AccountNumber = "ACC123",
                License = Guid.NewGuid().ToString()
            };
        }

        #region Constructor Tests

        [Test, Category("Models")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var subAccount = new SubAccount();
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.Id, Is.EqualTo(0));
                Assert.That(subAccount.LinkedEntityId, Is.EqualTo(0));
                Assert.That(subAccount.LinkedEntityType, Is.Null);
                Assert.That(subAccount.LinkedEntity, Is.Null);
                Assert.That(subAccount.AccountId, Is.Null);
                Assert.That(subAccount.Account, Is.Null);
                Assert.That(subAccount.CreatedDate, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(subAccount.CreatedDate, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(subAccount.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsAllPropertiesCorrectly()
        {
            // Act
            var subAccount = new SubAccount(
                id: 1,
                linkedEntity: _testLinkedEntity,
                accountId: 200,
                account: _testAccount,
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.Id, Is.EqualTo(1));
                Assert.That(subAccount.LinkedEntityId, Is.EqualTo(100));
                Assert.That(subAccount.LinkedEntityType, Is.EqualTo("User"));
                Assert.That(subAccount.LinkedEntity, Is.EqualTo(_testLinkedEntity));
                Assert.That(subAccount.AccountId, Is.EqualTo(200));
                Assert.That(subAccount.Account, Is.EqualTo(_testAccount));
                Assert.That(subAccount.CreatedDate, Is.EqualTo(_testCreatedDate));
                Assert.That(subAccount.ModifiedDate, Is.EqualTo(_testModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_HandlesNullValues()
        {
            // Act
            var subAccount = new SubAccount(
                id: 1,
                linkedEntity: null,
                accountId: null,
                account: null,
                createdDate: _testCreatedDate,
                modifiedDate: null
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.Id, Is.EqualTo(1));
                Assert.That(subAccount.LinkedEntityId, Is.EqualTo(0));
                Assert.That(subAccount.LinkedEntityType, Is.Null);
                Assert.That(subAccount.LinkedEntity, Is.Null);
                Assert.That(subAccount.AccountId, Is.Null);
                Assert.That(subAccount.Account, Is.Null);
                Assert.That(subAccount.CreatedDate, Is.EqualTo(_testCreatedDate));
                Assert.That(subAccount.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_SetsPropertiesFromLinkedEntityAndAccount()
        {
            // Act
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.LinkedEntityId, Is.EqualTo(_testLinkedEntity.Id));
                Assert.That(subAccount.LinkedEntityType, Is.EqualTo(_testLinkedEntity.GetType().Name));
                Assert.That(subAccount.LinkedEntity, Is.EqualTo(_testLinkedEntity));
                Assert.That(subAccount.AccountId, Is.EqualTo(_testAccount.Id));
                Assert.That(subAccount.Account, Is.EqualTo(_testAccount));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_HandlesNullLinkedEntity()
        {
            // Act
            var subAccount = new SubAccount(null, _testAccount);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.LinkedEntityId, Is.EqualTo(0));
                Assert.That(subAccount.LinkedEntityType, Is.Null);
                Assert.That(subAccount.LinkedEntity, Is.Null);
                Assert.That(subAccount.AccountId, Is.EqualTo(_testAccount.Id));
                Assert.That(subAccount.Account, Is.EqualTo(_testAccount));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_HandlesNullAccount()
        {
            // Act
            var subAccount = new SubAccount(_testLinkedEntity, null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.LinkedEntityId, Is.EqualTo(_testLinkedEntity.Id));
                Assert.That(subAccount.LinkedEntityType, Is.EqualTo(_testLinkedEntity.GetType().Name));
                Assert.That(subAccount.LinkedEntity, Is.EqualTo(_testLinkedEntity));
                Assert.That(subAccount.AccountId, Is.Null);
                Assert.That(subAccount.Account, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_HandlesBothNullParameters()
        {
            // Act
            var subAccount = new SubAccount(null, null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.LinkedEntityId, Is.EqualTo(0));
                Assert.That(subAccount.LinkedEntityType, Is.Null);
                Assert.That(subAccount.LinkedEntity, Is.Null);
                Assert.That(subAccount.AccountId, Is.Null);
                Assert.That(subAccount.Account, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithValidDTO_ShouldCreateSubAccountCorrectly()
        {
            // Arrange
            var dto = new TestSubAccountDTO
            {
                Id = 1,
                LinkedEntityId = 100,
                LinkedEntityType = "User",
                LinkedEntity = _testLinkedEntity,
                AccountId = 200,
                Account = new AccountDTO { Id = 200 },
                ModifiedDate = _testModifiedDate
            };

            // Act
            var subAccount = new SubAccount(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.Id, Is.EqualTo(dto.Id));
                Assert.That(subAccount.LinkedEntity, Is.Not.Null);
                Assert.That(subAccount.LinkedEntity!.Id, Is.EqualTo(dto.LinkedEntity.Id));
                Assert.That(subAccount.AccountId, Is.EqualTo(dto.AccountId));
                Assert.That(subAccount.Account, Is.Not.Null);
                Assert.That(subAccount.Account!.Id, Is.EqualTo(dto.Account.Id));
                Assert.That(subAccount.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(subAccount.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullOptionalProperties_ShouldCreateSubAccountCorrectly()
        {
            // Arrange
            var dto = new TestSubAccountDTO
            {
                Id = 2,
                LinkedEntityId = 150,
                LinkedEntityType = null,
                LinkedEntity = null,
                AccountId = null,
                Account = null,
                ModifiedDate = null
            };

            // Act
            var subAccount = new SubAccount(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.Id, Is.EqualTo(dto.Id));
                Assert.That(subAccount.LinkedEntity, Is.Null);
                Assert.That(subAccount.AccountId, Is.Null);
                Assert.That(subAccount.Account, Is.Null);
                Assert.That(subAccount.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(subAccount.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_PreservesCreatedDateFromDTO_ShouldUseProvidedDate()
        {
            // Arrange
            var specificCreatedDate = new DateTime(2023, 6, 15, 14, 30, 45);
            var dto = new TestSubAccountDTO
            {
                Id = 5,
                LinkedEntityId = 500,
                AccountId = 600,
                ModifiedDate = _testModifiedDate
            };
            
            // Use reflection to set the CreatedDate since it's typically read-only
            var createdDateField = typeof(TestSubAccountDTO).GetField("<CreatedDate>k__BackingField", 
                System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            createdDateField?.SetValue(dto, specificCreatedDate);

            // Act
            var subAccount = new SubAccount(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.Id, Is.EqualTo(dto.Id));
                Assert.That(subAccount.LinkedEntityId, Is.EqualTo(dto.LinkedEntityId));
                Assert.That(subAccount.AccountId, Is.EqualTo(dto.AccountId));
                Assert.That(subAccount.CreatedDate, Is.EqualTo(specificCreatedDate));
                Assert.That(subAccount.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        #endregion

        #region Property Tests

        [Test, Category("Models")]
        public void Id_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var subAccount = new SubAccount();
            var originalModifiedDate = subAccount.ModifiedDate;

            // Act
            subAccount.Id = 123;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.Id, Is.EqualTo(123));
                Assert.That(subAccount.ModifiedDate, Is.Not.EqualTo(originalModifiedDate));
                Assert.That(subAccount.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void Id_Setter_ThrowsArgumentOutOfRangeException_WhenNegative()
        {
            // Arrange
            var subAccount = new SubAccount();

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => subAccount.Id = -1);
            Assert.Multiple(() =>
            {
                Assert.That(ex.ParamName, Is.EqualTo("value"));
                Assert.That(ex.Message, Does.Contain("Id must be a non-negative number."));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityId_IsReadOnlyAndDerivedFromLinkedEntity()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                // Act
                LinkedEntity = _testLinkedEntity
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.LinkedEntityId, Is.EqualTo(100));
                // Verify it's not settable
                var property = typeof(SubAccount).GetProperty("LinkedEntityId");
                Assert.That(property!.CanWrite, Is.False);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityType_IsReadOnlyAndDerivedFromLinkedEntity()
        {
            // Arrange & Act
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.LinkedEntityType, Is.EqualTo("User"));
                var property = typeof(SubAccount).GetProperty("LinkedEntityType");
                Assert.That(property!.CanWrite, Is.False);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var subAccount = new SubAccount();
            var originalModifiedDate = subAccount.ModifiedDate;

            // Act
            subAccount.LinkedEntity = _testLinkedEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.LinkedEntity, Is.EqualTo(_testLinkedEntity));
                Assert.That(subAccount.ModifiedDate, Is.Not.EqualTo(originalModifiedDate));
                Assert.That(subAccount.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_SetsContactAndContactId()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                // Act
                LinkedEntity = _testLinkedEntity
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.ContactId, Is.EqualTo(_testLinkedEntity.Id));
                Assert.That(subAccount.Organization, Is.Null);
                Assert.That(subAccount.OrganizationId, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_SetsOrganizationAndOrganizationId()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                // Act
                LinkedEntity = _testOrganization
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.Organization, Is.EqualTo(_testOrganization));
                Assert.That(subAccount.OrganizationId, Is.EqualTo(_testOrganization.Id));
                Assert.That(subAccount.Contact, Is.Null);
                Assert.That(subAccount.ContactId, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_CanSetToNull()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount)
            {
                // Act
                LinkedEntity = null
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.LinkedEntity, Is.Null);
                Assert.That(subAccount.Contact, Is.Null);
                Assert.That(subAccount.Organization, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void AccountId_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var subAccount = new SubAccount();
            var originalModifiedDate = subAccount.ModifiedDate;

            // Act
            subAccount.AccountId = 789;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.AccountId, Is.EqualTo(789));
                Assert.That(subAccount.ModifiedDate, Is.Not.EqualTo(originalModifiedDate));
                Assert.That(subAccount.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void AccountId_Setter_AcceptsNull()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                // Act
                AccountId = null
            };

            // Assert
            Assert.That(subAccount.AccountId, Is.Null);
        }

        [Test, Category("Models")]
        public void Account_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var subAccount = new SubAccount();
            var originalModifiedDate = subAccount.ModifiedDate;

            // Act
            subAccount.Account = _testAccount;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.Account, Is.EqualTo(_testAccount));
                Assert.That(subAccount.ModifiedDate, Is.Not.EqualTo(originalModifiedDate));
                Assert.That(subAccount.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void Account_Setter_CanSetToNull()
        {
            // Arrange
            var subAccount = new SubAccount(null, _testAccount)
            {
                // Act
                Account = null
            };

            // Assert
            Assert.That(subAccount.Account, Is.Null);
        }

        [Test, Category("Models")]
        public void CreatedDate_IsReadOnly_AndSetDuringConstruction()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var subAccount = new SubAccount();
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.CreatedDate, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(subAccount.CreatedDate, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsCreatedDateFromParameter()
        {
            // Arrange
            var specificDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            var subAccount = new SubAccount(
                id: 1,
                linkedEntity: _testLinkedEntity,
                accountId: 200,
                account: _testAccount,
                createdDate: specificDate,
                modifiedDate: _testModifiedDate
            );

            // Assert
            Assert.That(subAccount.CreatedDate, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void ModifiedDate_CanBeSetDirectly()
        {
            // Arrange
            var subAccount = new SubAccount();
            var testDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            subAccount.ModifiedDate = testDate;

            // Assert
            Assert.That(subAccount.ModifiedDate, Is.EqualTo(testDate));
        }

        #endregion

        #region Method Tests

        [Test, Category("Models")]
        public void Cast_ToSubAccountDTO_WithValidData_ReturnsSubAccountDTO()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount)
            {
                Id = 1
            };

            // Act
            var result = subAccount.Cast<SubAccountDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<SubAccountDTO>());
                Assert.That(result.Id, Is.EqualTo(1));
                Assert.That(result.LinkedEntityId, Is.EqualTo(_testLinkedEntity.Id));
                Assert.That(result.LinkedEntityType, Is.EqualTo("User"));
                Assert.That(result.AccountId, Is.EqualTo(_testAccount.Id));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToISubAccountDTO_WithValidData_ReturnsSubAccountDTO()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount)
            {
                Id = 2
            };

            // Act
            var result = subAccount.Cast<ISubAccountDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<SubAccountDTO>());
                Assert.That(result.Id, Is.EqualTo(2));
                Assert.That(result.LinkedEntityId, Is.EqualTo(_testLinkedEntity.Id));
                Assert.That(result.LinkedEntityType, Is.EqualTo("User"));
                Assert.That(result.AccountId, Is.EqualTo(_testAccount.Id));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToSubAccountDTO_WithNullLinkedEntity_HandlesGracefully()
        {
            // Arrange
            var subAccount = new SubAccount(null, _testAccount)
            {
                Id = 3
            };

            // Act
            var result = subAccount.Cast<SubAccountDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<SubAccountDTO>());
                Assert.That(result.Id, Is.EqualTo(3));
                Assert.That(result.LinkedEntity, Is.Null);
                Assert.That(result.LinkedEntityType, Is.Null);
                Assert.That(result.AccountId, Is.EqualTo(_testAccount.Id));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToSubAccountDTO_WithNullAccount_ThrowsNullReferenceException()
        {
            // Arrange
            var subAccount = new SubAccount(
                id: 1,
                linkedEntity: null,
                accountId: 200,
                account: null, // This will cause NullReferenceException in Cast method
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Act & Assert
            // The Cast method calls _account!.Cast<AccountDTO>() which throws when _account is null
            Assert.Throws<NullReferenceException>(() => subAccount.Cast<SubAccountDTO>());
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedType_ThrowsInvalidCastException()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => subAccount.Cast<User>());
            Assert.That(ex.Message, Does.Contain("Cannot cast SubAccount to type User."));
        }

        [Test, Category("Models")]
        public void ToJson_ReturnsValidJsonString()
        {
            // Arrange
            var subAccount = new SubAccount(
                id: 1,
                linkedEntity: null, // Avoid serialization issues with complex entities
                accountId: 200,
                account: null, // Avoid serialization issues with complex entities
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Act
            var json = subAccount.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("\"linkedEntityId\":0"));
                Assert.That(json, Does.Contain("\"accountId\":200"));
            });
        }

        [Test, Category("Models")]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            var subAccount = new SubAccount(
                id: 42,
                linkedEntity: _testLinkedEntity,
                accountId: 200,
                account: _testAccount,
                createdDate: _testCreatedDate,
                modifiedDate: _testModifiedDate
            );

            // Act
            var result = subAccount.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("Id:42"));
                Assert.That(result, Does.Contain("LinkedEntityId:100"));
                Assert.That(result, Does.Contain("LinkedEntityType:User"));
                Assert.That(result, Does.Contain("OrganizerCompanion.Core.Models.Domain.SubAccount"));
            });
        }

        #endregion

        #region Validation Tests

        [Test, Category("Validation")]
        public void Validation_ShouldPass_ForValidSubAccount()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount)
            {
                Id = 1
            };

            // Act
            var validationResults = ValidateModel(subAccount);

            // Assert
            Assert.That(validationResults, Is.Empty);
        }

        [Test, Category("Validation")]
        public void Validation_ShouldFail_WhenAccountIdIsNull()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, null)
            {
                Id = 1,
                Account = null,
                AccountId = null
            };

            // Act
            var validationResults = ValidateModel(subAccount);

            // Assert
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults[0].ErrorMessage, Does.Contain("AccountId"));
        }

        [Test, Category("Validation")]
        [TestCase(-1)]
        [TestCase(-100)]
        public void Validation_ShouldFail_WhenIdIsNegative(int invalidId)
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => subAccount.Id = invalidId);
        }

        [Test, Category("Validation")]
        [TestCase(-1)]
        [TestCase(-100)]
        public void Validation_ShouldFail_WhenAccountIdIsNegative(int invalidAccountId)
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() => subAccount.AccountId = invalidAccountId);
        }

        [Test, Category("Validation")]
        public void Validation_ShouldFail_WhenRequiredPropertiesAreNull()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                Id = 1,
                LinkedEntity = null,
                AccountId = 200,
                Account = null
            };

            // Act
            var validationResults = ValidateModel(subAccount);

            // Assert
            Assert.That(validationResults, Has.Count.GreaterThan(0));
            Assert.That(validationResults.Any(v => v.ErrorMessage!.Contains("Account")));
        }

        #endregion
    }
}
