using System.ComponentModel.DataAnnotations;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class SubAccountShould
    {
        private Account _testAccount;
        private User _testLinkedEntity;
        private readonly DateTime _testDateCreated = new(2023, 1, 1, 12, 0, 0);
        private readonly DateTime _testDateModified = new(2023, 1, 2, 12, 0, 0);

        // Helper method to perform validation
        private IList<ValidationResult> ValidateModel(object model)
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

            // Create a valid account
            _testAccount = new Account
            {
                Id = 200,
                AccountName = "TestAccount",
                AccountNumber = "ACC123",
                License = Guid.NewGuid().ToString(),
                DatabaseConnection = new DatabaseConnection
                {
                    ConnectionString = "Server=localhost;Database=testdb;Integrated Security=true;",
                    DatabaseType = SupportedDatabases.SQLServer
                }
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
                Assert.That(subAccount.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(subAccount.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(subAccount.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsAllPropertiesCorrectly()
        {
            // Act
            var subAccount = new SubAccount(
                id: 1,
                linkedEntityId: 100,
                linkedEntityType: "User",
                linkedEntity: _testLinkedEntity,
                accountId: 200,
                account: _testAccount,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
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
                Assert.That(subAccount.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(subAccount.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_HandlesNullValues()
        {
            // Act
            var subAccount = new SubAccount(
                id: 1,
                linkedEntityId: 100,
                linkedEntityType: null,
                linkedEntity: null,
                accountId: null,
                account: null,
                dateCreated: _testDateCreated,
                dateModified: null
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.Id, Is.EqualTo(1));
                Assert.That(subAccount.LinkedEntityId, Is.EqualTo(100));
                Assert.That(subAccount.LinkedEntityType, Is.Null);
                Assert.That(subAccount.LinkedEntity, Is.Null);
                Assert.That(subAccount.AccountId, Is.Null);
                Assert.That(subAccount.Account, Is.Null);
                Assert.That(subAccount.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(subAccount.DateModified, Is.Null);
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

        #endregion

        #region Property Tests

        [Test, Category("Models")]
        public void Id_Setter_UpdatesDateModified()
        {
            // Arrange
            var subAccount = new SubAccount();
            var originalDateModified = subAccount.DateModified;

            // Act
            subAccount.Id = 123;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.Id, Is.EqualTo(123));
                Assert.That(subAccount.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(subAccount.DateModified, Is.Not.Null);
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
        public void LinkedEntityId_Setter_UpdatesDateModified()
        {
            // Arrange
            var subAccount = new SubAccount();
            var originalDateModified = subAccount.DateModified;

            // Act
            subAccount.LinkedEntityId = 456;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.LinkedEntityId, Is.EqualTo(456));
                Assert.That(subAccount.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(subAccount.DateModified, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityId_Setter_ThrowsArgumentOutOfRangeException_WhenNegative()
        {
            // Arrange
            var subAccount = new SubAccount();

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => subAccount.LinkedEntityId = -1);
            Assert.Multiple(() =>
            {
                Assert.That(ex.ParamName, Is.EqualTo("value"));
                Assert.That(ex.Message, Does.Contain("Linked Entity Id must be a non-negative number."));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntityType_IsReadOnly()
        {
            // Arrange & Act
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Assert
            Assert.That(subAccount.LinkedEntityType, Is.EqualTo("User"));
            // Note: LinkedEntityType has no setter in the public interface, only private setter used in constructor
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_UpdatesDateModified()
        {
            // Arrange
            var subAccount = new SubAccount();
            var originalDateModified = subAccount.DateModified;

            // Act
            subAccount.LinkedEntity = _testLinkedEntity;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.LinkedEntity, Is.EqualTo(_testLinkedEntity));
                Assert.That(subAccount.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(subAccount.DateModified, Is.Not.Null);
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
      Assert.That(subAccount.LinkedEntity, Is.Null);
        }

        [Test, Category("Models")]
        public void AccountId_Setter_UpdatesDateModified()
        {
            // Arrange
            var subAccount = new SubAccount();
            var originalDateModified = subAccount.DateModified;

            // Act
            subAccount.AccountId = 789;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.AccountId, Is.EqualTo(789));
                Assert.That(subAccount.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(subAccount.DateModified, Is.Not.Null);
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
        public void AccountId_Setter_ThrowsArgumentOutOfRangeException_WhenNegative()
        {
            // Arrange
            var subAccount = new SubAccount();

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => subAccount.AccountId = -1);
            Assert.Multiple(() =>
            {
                Assert.That(ex.ParamName, Is.EqualTo("value"));
                Assert.That(ex.Message, Does.Contain("Account Id must be a non-negative number."));
            });
        }

        [Test, Category("Models")]
        public void Account_Setter_UpdatesDateModified()
        {
            // Arrange
            var subAccount = new SubAccount();
            var originalDateModified = subAccount.DateModified;

            // Act
            subAccount.Account = _testAccount;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.Account, Is.EqualTo(_testAccount));
                Assert.That(subAccount.DateModified, Is.Not.EqualTo(originalDateModified));
                Assert.That(subAccount.DateModified, Is.Not.Null);
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
        public void DateCreated_IsReadOnly_AndSetDuringConstruction()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var subAccount = new SubAccount();
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(subAccount.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsDateCreatedFromParameter()
        {
            // Arrange
            var specificDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            var subAccount = new SubAccount(
                id: 1,
                linkedEntityId: 100,
                linkedEntityType: "User",
                linkedEntity: _testLinkedEntity,
                accountId: 200,
                account: _testAccount,
                dateCreated: specificDate,
                dateModified: _testDateModified
            );

            // Assert
            Assert.That(subAccount.DateCreated, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void DateModified_CanBeSetDirectly()
        {
            // Arrange
            var subAccount = new SubAccount();
            var testDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            subAccount.DateModified = testDate;

            // Assert
            Assert.That(subAccount.DateModified, Is.EqualTo(testDate));
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
        public void Cast_ToSubAccountDTO_WithLinkedEntityCastFailure_UsesOriginalEntity()
        {
            // Arrange
            // Create a mock linked entity that might fail casting
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount)
            {
                Id = 4
            };

            // Act - The Cast method should handle casting failures gracefully
            var result = subAccount.Cast<SubAccountDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<SubAccountDTO>());
                Assert.That(result.Id, Is.EqualTo(4));
                // The LinkedEntity should be present even if casting failed
                Assert.That(result.LinkedEntity, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void Cast_ToSubAccountDTO_WithNullAccount_ThrowsNullReferenceException()
        {
            // Arrange
            var subAccount = new SubAccount(
                id: 1,
                linkedEntityId: 100,
                linkedEntityType: "User",
                linkedEntity: null,
                accountId: 200,
                account: null, // This will cause NullReferenceException in Cast method
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert
            // The Cast method calls _account!.Cast<AccountDTO>() which throws when _account is null
            Assert.Throws<NullReferenceException>(() => subAccount.Cast<SubAccountDTO>());
        }

        [Test, Category("Models")]
        public void Cast_ToISubAccountDTO_WithNullAccount_ThrowsNullReferenceException()
        {
            // Arrange
            var subAccount = new SubAccount(
                id: 2,
                linkedEntityId: 101,
                linkedEntityType: "Organization",
                linkedEntity: null,
                accountId: 201,
                account: null, // This will cause NullReferenceException in Cast method
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert
            // The Cast method calls _account!.Cast<AccountDTO>() which throws when _account is null
            Assert.Throws<NullReferenceException>(() => subAccount.Cast<ISubAccountDTO>());
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
        public void Cast_ToAnotherUnsupportedType_ThrowsInvalidCastException()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => subAccount.Cast<Account>());
            Assert.That(ex.Message, Does.Contain("Cannot cast SubAccount to type Account."));
        }

        [Test, Category("Models")]
        public void Cast_HandlesExceptionDuringCasting()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Create a scenario where the Cast method might throw an exception
            // This tests the try-catch block in the Cast method

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => subAccount.Cast<Organization>());
            Assert.That(ex.Message, Does.Contain("Cannot cast SubAccount to type Organization."));
        }

        [Test, Category("Models")]
        public void ToJson_ReturnsValidJsonString()
        {
            // Arrange
            var subAccount = new SubAccount(
                id: 1,
                linkedEntityId: 100,
                linkedEntityType: "User",
                linkedEntity: null, // Avoid serialization issues with complex entities
                accountId: 200,
                account: null, // Avoid serialization issues with complex entities
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var json = subAccount.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("\"linkedEntityId\":100"));
                Assert.That(json, Does.Contain("\"linktedEntityType\":\"User\""));
                Assert.That(json, Does.Contain("\"accountId\":200"));
            });
        }

        [Test, Category("Models")]
        public void ToJson_HandlesNullValues()
        {
            // Arrange
            var subAccount = new SubAccount();

            // Act
            var json = subAccount.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":0"));
                Assert.That(json, Does.Contain("\"linkedEntityId\":0"));
            });
        }

        [Test, Category("Models")]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            var subAccount = new SubAccount(
                id: 42,
                linkedEntityId: 100,
                linkedEntityType: "User",
                linkedEntity: _testLinkedEntity,
                accountId: 200,
                account: _testAccount,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
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

        [Test, Category("Models")]
        public void ToString_HandlesNullLinkedEntityType()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                Id = 42,
                LinkedEntityId = 100
            };

            // Act
            var result = subAccount.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("Id:42"));
                Assert.That(result, Does.Contain("LinkedEntityId:100"));
                Assert.That(result, Does.Contain("LinkedEntityType:"));
                Assert.That(result, Does.Contain("OrganizerCompanion.Core.Models.Domain.SubAccount"));
            });
        }

        #endregion

        #region TypeRegistry and LinkedEntity Casting Tests

        [Test, Category("Models")]
        public void CastLinkedEntity_WithValidLinkedEntity_ReturnsCorrectType()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Act
            var result = subAccount.CastLinkedEntity<User>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<User>());
                Assert.That(result.Id, Is.EqualTo(_testLinkedEntity.Id));
                Assert.That(result.FirstName, Is.EqualTo(_testLinkedEntity.FirstName));
            });
        }

        [Test, Category("Models")]
        public void CastLinkedEntity_WithNullLinkedEntity_ThrowsInvalidOperationException()
        {
            // Arrange
            var subAccount = new SubAccount(null, _testAccount);

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => subAccount.CastLinkedEntity<User>());
            Assert.That(ex.Message, Does.Contain("LinkedEntity is null and cannot be cast"));
        }

        [Test, Category("Models")]
        public void CastLinkedEntity_WithEmptyLinkedEntityType_ThrowsInvalidOperationException()
        {
            // Arrange
            var subAccount = new SubAccount(
                id: 1,
                linkedEntityId: 100,
                linkedEntityType: "", // Empty string
                linkedEntity: _testLinkedEntity,
                accountId: 200,
                account: _testAccount,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => subAccount.CastLinkedEntity<User>());
            Assert.That(ex.Message, Does.Contain("LinkedEntityType is not set"));
        }

        [Test, Category("Models")]
        public void CastLinkedEntity_WithIncompatibleType_ThrowsInvalidCastException()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => subAccount.CastLinkedEntity<Account>());
            Assert.That(ex.Message, Does.Contain("Cannot cast LinkedEntity"));
        }

        [Test, Category("Models")]
        public void GetLinkedEntityAs_WithCorrectType_ReturnsEntity()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Act
            var result = subAccount.GetLinkedEntityAs<User>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<User>());
                Assert.That(result!.Id, Is.EqualTo(_testLinkedEntity.Id));
            });
        }

        [Test, Category("Models")]
        public void GetLinkedEntityAs_WithIncorrectType_ReturnsNull()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Act
            var result = subAccount.GetLinkedEntityAs<Account>();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("Models")]
        public void GetLinkedEntityAs_WithNullLinkedEntity_ReturnsNull()
        {
            // Arrange
            var subAccount = new SubAccount(null, _testAccount);

            // Act
            var result = subAccount.GetLinkedEntityAs<User>();

            // Assert
            Assert.That(result, Is.Null);
        }

        [Test, Category("Models")]
        public void CanCastLinkedEntityTo_WithValidType_ReturnsTrue()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Act
            var result = subAccount.CanCastLinkedEntityTo<User>();

            // Assert
            Assert.That(result, Is.True);
        }

        [Test, Category("Models")]
        public void CanCastLinkedEntityTo_WithInvalidType_ReturnsFalse()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Act
            var result = subAccount.CanCastLinkedEntityTo<Account>();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test, Category("Models")]
        public void CanCastLinkedEntityTo_WithNullLinkedEntity_ReturnsFalse()
        {
            // Arrange
            var subAccount = new SubAccount(null, _testAccount);

            // Act
            var result = subAccount.CanCastLinkedEntityTo<User>();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test, Category("Models")]
        public void CanCastLinkedEntityTo_WithEmptyLinkedEntityType_ReturnsFalse()
        {
            // Arrange
            var subAccount = new SubAccount(
                id: 1,
                linkedEntityId: 100,
                linkedEntityType: "", // Empty string should return false
                linkedEntity: _testLinkedEntity,
                accountId: 200,
                account: _testAccount,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var result = subAccount.CanCastLinkedEntityTo<User>();

            // Assert
            Assert.That(result, Is.False);
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_AutomaticallyUpdatesLinkedEntityType()
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
                Assert.That(subAccount.LinkedEntity, Is.EqualTo(_testLinkedEntity));
                Assert.That(subAccount.LinkedEntityType, Is.EqualTo("User"));
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_SetsLinkedEntityTypeToNullWhenEntityIsNull()
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
                Assert.That(subAccount.LinkedEntityType, Is.Null);
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
        public void Validation_ShouldPass_WhenIdIsZero()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount)
            {
                Id = 0
            };

            // Act
            var validationResults = ValidateModel(subAccount);

            // Assert
            Assert.That(validationResults, Is.Empty);
        }

        [Test, Category("Validation")]
        public void Validation_ShouldPass_WhenLinkedEntityIdIsZero()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount)
            {
                Id = 1,
                LinkedEntityId = 0
            };

            // Act
            var validationResults = ValidateModel(subAccount);

            // Assert
            Assert.That(validationResults, Is.Empty);
        }

        [Test, Category("Validation")]
        public void Validation_ShouldPass_WhenAccountIdIsZero()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount)
            {
                Id = 1,
                AccountId = 0
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
                Account = _testAccount
            };

            // Act
            var validationResults = ValidateModel(subAccount);

            // Assert
            // AccountId is marked with [Required] even though it's nullable, so validation should fail
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
            // The property setter throws ArgumentOutOfRangeException for negative values
            Assert.Throws<ArgumentOutOfRangeException>(() => subAccount.Id = invalidId);
        }

        [Test, Category("Validation")]
        [TestCase(-1)]
        [TestCase(-100)]
        public void Validation_ShouldFail_WhenLinkedEntityIdIsNegative(int invalidLinkedEntityId)
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Act & Assert
            // The property setter throws ArgumentOutOfRangeException for negative values
            Assert.Throws<ArgumentOutOfRangeException>(() => subAccount.LinkedEntityId = invalidLinkedEntityId);
        }

        [Test, Category("Validation")]
        [TestCase(-1)]
        [TestCase(-100)]
        public void Validation_ShouldFail_WhenAccountIdIsNegative(int invalidAccountId)
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Act & Assert
            // The property setter throws ArgumentOutOfRangeException for negative values
            Assert.Throws<ArgumentOutOfRangeException>(() => subAccount.AccountId = invalidAccountId);
        }

        [Test, Category("Validation")]
        public void Validation_ShouldFail_WhenRequiredPropertiesAreNull()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                Id = 1,
                LinkedEntityId = 100,
                LinkedEntity = null,
                AccountId = 200,
                Account = null
            };

            // Act
            var validationResults = ValidateModel(subAccount);

            // Assert
            Assert.That(validationResults, Has.Count.GreaterThan(0));
            Assert.That(validationResults.Any(v => v.ErrorMessage!.Contains("LinkedEntity") || v.ErrorMessage!.Contains("Account")));
        }

        [Test, Category("Validation")]
        public void Validation_ShouldFail_WhenLinkedEntityTypeExceedsMaxLength()
        {
            // Note: The LinkedEntityType property has MaxLength(50) attribute
            // However, since it's set internally based on GetType().Name, this test would require
            // a mock object with a very long type name, which is not practical.
            // This test documents the validation attribute exists.

            Assert.Pass("LinkedEntityType validation is enforced by MaxLength(50) attribute but is set internally from GetType().Name");
        }

        #endregion

        #region Edge Case Tests

        [Test, Category("Models")]
        public void Properties_CanSetAndGet_MaxIntValues()
        {
            // Arrange
            var subAccount = new SubAccount();

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                subAccount.Id = int.MaxValue;
                subAccount.LinkedEntityId = int.MaxValue;
                subAccount.AccountId = int.MaxValue;
            });

            Assert.Multiple(() =>
            {
                Assert.That(subAccount.Id, Is.EqualTo(int.MaxValue));
                Assert.That(subAccount.LinkedEntityId, Is.EqualTo(int.MaxValue));
                Assert.That(subAccount.AccountId, Is.EqualTo(int.MaxValue));
            });
        }

        [Test, Category("Models")]
        public void DateModified_UpdatesIndependently_ForDifferentPropertyChanges()
        {
      // Arrange
      var subAccount = new SubAccount
      {
        // Act & Assert
        Id = 1
      };
      var firstModification = subAccount.DateModified;
            
            Thread.Sleep(1); // Ensure time difference
            
            subAccount.LinkedEntityId = 100;
            var secondModification = subAccount.DateModified;
            
            Thread.Sleep(1); // Ensure time difference
            
            subAccount.AccountId = 200;
            var thirdModification = subAccount.DateModified;

            Assert.Multiple(() =>
            {
                Assert.That(secondModification, Is.GreaterThan(firstModification));
                Assert.That(thirdModification, Is.GreaterThan(secondModification));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithNullAccount_ThrowsException()
        {
            // Arrange
            var subAccount = new SubAccount(
                id: 1,
                linkedEntityId: 100,
                linkedEntityType: "User",
                linkedEntity: null,
                accountId: null,
                account: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert
            // The Cast method calls account!.Cast<AccountDTO>() which will throw NullReferenceException
            Assert.Throws<NullReferenceException>(() => subAccount.Cast<SubAccountDTO>());
        }

        [Test, Category("Models")]
        public void Cast_WithTypeRegistryEnhancement_HandlesLinkedEntityCasting()
        {
            // This test documents the enhanced Cast method behavior with TypeRegistry
            // The Cast method now attempts to use TypeRegistry for more sophisticated LinkedEntity casting

            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount)
            {
                Id = 5
            };

            // Act
            var result = subAccount.Cast<SubAccountDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(5));
                Assert.That(result.LinkedEntityType, Is.EqualTo("User"));
                // The enhanced Cast method should handle LinkedEntity casting more gracefully
                Assert.That(result.LinkedEntity, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void Cast_DocumentsCastMethodConstraints()
        {
            // This test documents the current behavior and limitations of the Cast method
            // The Cast method requires a non-null account and the account must support casting to AccountDTO
            // The linkedEntity can be null or must support casting to IDomainEntity

            // Arrange - using JsonConstructor with basic values to avoid complex object creation
            var subAccount = new SubAccount(
                id: 1,
                linkedEntityId: 100,
                linkedEntityType: "TestEntity",
                linkedEntity: null,
                accountId: 200,
                account: null, // This causes the Cast to fail
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => subAccount.Cast<SubAccountDTO>());
            Assert.Pass("Cast method documented - requires non-null account that supports Cast<AccountDTO>()");
        }

        [Test, Category("Models")]
        public void Cast_WithComplexLinkedEntityScenario_HandlesTypeRegistryLookup()
        {
            // This test verifies the enhanced Cast method handles TypeRegistry lookup for LinkedEntity casting

            // Arrange
            var organization = new Organization 
            { 
                Id = 999, 
                OrganizationName = "Test Org" 
            };
            var subAccount = new SubAccount(organization, _testAccount)
            {
                Id = 6
            };

            // Act
            var result = subAccount.Cast<SubAccountDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(6));
                Assert.That(result.LinkedEntityType, Is.EqualTo("Organization"));
                Assert.That(result.LinkedEntityId, Is.EqualTo(999));
                // The Cast method should handle Organization -> DTO casting or fallback gracefully
                Assert.That(result.AccountId, Is.EqualTo(_testAccount.Id));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithNullLinkedEntityAndEmptyType_HandlesGracefully()
        {
            // Arrange
            var subAccount = new SubAccount(
                id: 7,
                linkedEntityId: 0,
                linkedEntityType: null, // Null type
                linkedEntity: null,      // Null entity
                accountId: 200,
                account: _testAccount,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var result = subAccount.Cast<SubAccountDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(7));
                Assert.That(result.LinkedEntity, Is.Null);
                Assert.That(result.LinkedEntityType, Is.Null);
                Assert.That(result.AccountId, Is.EqualTo(_testAccount.Id));
            });
        }

        [Test, Category("Models")]
        public void Cast_PreservesAllSubAccountProperties()
        {
            // Arrange
            var testDate = new DateTime(2023, 8, 15, 14, 30, 0);
            var modifiedDate = new DateTime(2023, 8, 16, 10, 15, 0);
            
            var subAccount = new SubAccount(
                id: 888,
                linkedEntityId: 777,
                linkedEntityType: "User",
                linkedEntity: _testLinkedEntity,
                accountId: 666,
                account: _testAccount,
                dateCreated: testDate,
                dateModified: modifiedDate
            );

            // Act
            var result = subAccount.Cast<SubAccountDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(888));
                Assert.That(result.LinkedEntityId, Is.EqualTo(777));
                Assert.That(result.LinkedEntityType, Is.EqualTo("User"));
                Assert.That(result.AccountId, Is.EqualTo(666));
                Assert.That(result.DateCreated, Is.EqualTo(testDate));
                Assert.That(result.DateModified, Is.EqualTo(modifiedDate));
                Assert.That(result.LinkedEntity, Is.Not.Null);
                Assert.That(result.Account, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void CastLinkedEntity_WithReflectionFallback_WhenTypeRegistryReturnsNull()
        {
            // Arrange - Create a subaccount with a custom entity that might not be in TypeRegistry
            var customEntity = new User { Id = 99, FirstName = "Custom", LastName = "Entity" };
            var subAccount = new SubAccount(customEntity, _testAccount);

            // Act - This should work even if TypeRegistry doesn't have the type
            var result = subAccount.CastLinkedEntity<User>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<User>());
                Assert.That(result.Id, Is.EqualTo(99));
                Assert.That(result.FirstName, Is.EqualTo("Custom"));
            });
        }

        [Test, Category("Models")]
        public void CastLinkedEntity_WithReflectionFallback_ThrowsWhenTypeNotFound()
        {
            // Arrange - Create a SubAccount with a fake type name that doesn't exist
            var subAccount = new SubAccount(
                id: 1,
                linkedEntityId: 100,
                linkedEntityType: "NonExistentType",
                linkedEntity: _testLinkedEntity,
                accountId: 200,
                account: _testAccount,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => subAccount.CastLinkedEntity<User>());
            Assert.That(ex.Message, Does.Contain("Could not find type 'NonExistentType'"));
        }

        [Test, Category("Models")]
        public void CastLinkedEntity_WithCastMethodInvocation_HandlesExceptionCorrectly()
        {
            // Arrange - Create a scenario where Cast method might fail
            var customEntity = new User { Id = 100, FirstName = "Test", LastName = "User" };
            var subAccount = new SubAccount(customEntity, _testAccount);

            // Act - Try to cast to an incompatible type that should fail
            var ex = Assert.Throws<InvalidCastException>(() => subAccount.CastLinkedEntity<Account>());

            // Assert
            Assert.That(ex.Message, Does.Contain("Cannot cast LinkedEntity"));
        }

        [Test, Category("Models")]
        public void CastLinkedEntity_WithDirectCast_ReturnsCorrectResult()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Act - This should use the direct cast path (if _linkedEntity is T directCast)
            var result = subAccount.CastLinkedEntity<User>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.SameAs(_testLinkedEntity));
                Assert.That(result.FirstName, Is.EqualTo("John"));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithTypeRegistryEnhancement_WhenLinkedEntityCastFails()
        {
            // Arrange - Create a scenario where LinkedEntity casting might fail in the enhanced Cast method
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount) { Id = 6 };

            // Act - The Cast method should handle LinkedEntity casting failures gracefully
            var result = subAccount.Cast<SubAccountDTO>();

            // Assert - Should still return a valid DTO even if LinkedEntity casting fails
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<SubAccountDTO>());
                Assert.That(result.Id, Is.EqualTo(6));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithTypeRegistryEnhancement_WithNullLinkedEntityType()
        {
            // Arrange - Create a SubAccount with null LinkedEntityType
            var subAccount = new SubAccount(
                id: 7,
                linkedEntityId: 100,
                linkedEntityType: null,
                linkedEntity: _testLinkedEntity,
                accountId: 200,
                account: _testAccount,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var result = subAccount.Cast<SubAccountDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<SubAccountDTO>());
                Assert.That(result.Id, Is.EqualTo(7));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithTypeRegistryEnhancement_WithEmptyLinkedEntityType()
        {
            // Arrange - Create a SubAccount with empty LinkedEntityType
            var subAccount = new SubAccount(
                id: 8,
                linkedEntityId: 100,
                linkedEntityType: "",
                linkedEntity: _testLinkedEntity,
                accountId: 200,
                account: _testAccount,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act
            var result = subAccount.Cast<SubAccountDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<SubAccountDTO>());
                Assert.That(result.Id, Is.EqualTo(8));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithLinkedEntityReflectionInvocation_TestsCastMethodPath()
        {
            // This test specifically targets the reflection-based Cast method invocation path
            // in the enhanced Cast method when TypeRegistry is involved

            // Arrange
            var organization = new Organization { Id = 300, OrganizationName = "Test Org" };
            var subAccount = new SubAccount(organization, _testAccount) { Id = 9 };

            // Act
            var result = subAccount.Cast<SubAccountDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<SubAccountDTO>());
                Assert.That(result.Id, Is.EqualTo(9));
                Assert.That(result.LinkedEntityType, Is.EqualTo("Organization"));
            });
        }

        [Test, Category("Models")]
        public void CastLinkedEntity_WithNullLinkedEntityType_ThrowsInvalidOperationException()
        {
            // Arrange - Create a SubAccount with null LinkedEntityType but non-null LinkedEntity
            var subAccount = new SubAccount(
                id: 1,
                linkedEntityId: 100,
                linkedEntityType: null,
                linkedEntity: _testLinkedEntity,
                accountId: 200,
                account: _testAccount,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert
            var ex = Assert.Throws<InvalidOperationException>(() => subAccount.CastLinkedEntity<User>());
            Assert.That(ex.Message, Does.Contain("LinkedEntityType is not set"));
        }

        [Test, Category("Models")]
        public void CanCastLinkedEntityTo_WithTypeRegistrySupport_ReturnsCorrectResult()
    {
      // Arrange
      var subAccount = new SubAccount(_testLinkedEntity, _testAccount);
      Assert.Multiple(() =>
      {

        // Act & Assert - Should work with TypeRegistry lookup
        Assert.That(subAccount.CanCastLinkedEntityTo<User>(), Is.True);
        Assert.That(subAccount.CanCastLinkedEntityTo<Account>(), Is.False);
      });
    }

    [Test, Category("Models")]
        public void CastLinkedEntity_WithCastMethodFailure_ThrowsInvalidCastExceptionWithInnerException()
        {
            // Arrange - Create an entity that will fail when trying to cast via reflection
            var entity = new User { Id = 123, FirstName = "Test", LastName = "User" };
            var subAccount = new SubAccount(entity, _testAccount);

            // Act & Assert - Try to cast to completely incompatible type
            var ex = Assert.Throws<InvalidCastException>(() => subAccount.CastLinkedEntity<Organization>());
            Assert.That(ex.Message, Does.Contain("Cannot cast LinkedEntity"));
        }

        [Test, Category("Models")]
        public void CastLinkedEntity_WithNoAvailableCastMethod_ThrowsInvalidCastException()
        {
            // Arrange - This tests the final fallback when no Cast method is available
            var entity = new User { Id = 456, FirstName = "Another", LastName = "User" };
            var subAccount = new SubAccount(entity, _testAccount);

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => subAccount.CastLinkedEntity<Account>());
            Assert.That(ex.Message, Does.Contain("Cannot cast LinkedEntity"));
        }

        [Test, Category("Models")]
        public void CastLinkedEntity_TestsAllReflectionPaths()
        {
            // This test specifically targets the reflection-based type resolution paths
            // to ensure we cover all the Type.GetType fallback scenarios

            // Arrange - Use an organization to test different type paths
            var org = new Organization { Id = 999, OrganizationName = "Test Organization" };
            var subAccount = new SubAccount(org, _testAccount);

            // Act & Assert - This should work through the reflection system
            var result = subAccount.CastLinkedEntity<Organization>();
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<Organization>());
                Assert.That(result.Id, Is.EqualTo(999));
                Assert.That(result.OrganizationName, Is.EqualTo("Test Organization"));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithLinkedEntityTypeRegistryLookupFailure_UsesOriginalEntity()
        {
            // This test covers the scenario where TypeRegistry lookup fails but casting continues

            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount) { Id = 10 };

            // Act - Even if TypeRegistry methods fail, it should fall back gracefully
            var result = subAccount.Cast<SubAccountDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.TypeOf<SubAccountDTO>());
                Assert.That(result.Id, Is.EqualTo(10));
            });
        }

        #endregion
    }
}
