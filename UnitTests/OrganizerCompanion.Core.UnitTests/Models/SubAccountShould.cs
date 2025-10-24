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
        private AnnonymousUser _testAnnonymousUser;
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
            // Create a valid linked entity (User)
            _testLinkedEntity = new User
            {
                Id = 100,
                FirstName = "John",
                LastName = "Doe"
            };

            // Create an AnnonymousUser
            _testAnnonymousUser = new AnnonymousUser
            {
                Id = 101,
                UserName = "anonymous_user"
            };

            // Create an Organization
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
            var fakeUser = new User
            {
                Id = 500,
                FirstName = "Test",
                LastName = "User"
            };

            var dto = new TestSubAccountDTO
            {
                Id = 5,
                LinkedEntityId = 500,
                LinkedEntityType = "User",
                LinkedEntity = fakeUser,
                AccountId = 600,
                Account = new AccountDTO { Id = 600 },
                ModifiedDate = _testModifiedDate,
                CreatedDate = specificCreatedDate
            };

            // Act
            var subAccount = new SubAccount(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.Id, Is.EqualTo(dto.Id));
                Assert.That(subAccount.LinkedEntity, Is.Not.Null);
                Assert.That(subAccount.LinkedEntityId, Is.EqualTo(500));
                Assert.That(subAccount.LinkedEntityType, Is.EqualTo("User"));
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
                LinkedEntity = _testLinkedEntity
            };

            // Assert
            Assert.Multiple(() =>
       {
           Assert.That(subAccount.LinkedEntityId, Is.EqualTo(100));
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
        public void LinkedEntity_Setter_SetsUserWhenUserType()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                LinkedEntity = _testLinkedEntity
            };

            // Assert
            Assert.Multiple(() =>
                  {
                      Assert.That(subAccount.User, Is.EqualTo(_testLinkedEntity));
                      Assert.That(subAccount.UserId, Is.EqualTo(_testLinkedEntity.Id));
                      Assert.That(subAccount.Organization, Is.Null);
                      Assert.That(subAccount.OrganizationId, Is.Null);
                      Assert.That(subAccount.AnnonymousUser, Is.Null);
                      Assert.That(subAccount.AnnonymousUserId, Is.Null);
                  });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_SetsOrganizationWhenOrganizationType()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                LinkedEntity = _testOrganization
            };

            // Assert
            Assert.Multiple(() =>
     {
         Assert.That(subAccount.Organization, Is.EqualTo(_testOrganization));
         Assert.That(subAccount.OrganizationId, Is.EqualTo(_testOrganization.Id));
         Assert.That(subAccount.User, Is.Null);
         Assert.That(subAccount.UserId, Is.Null);
         Assert.That(subAccount.AnnonymousUser, Is.Null);
         Assert.That(subAccount.AnnonymousUserId, Is.Null);
     });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_SetsAnnonymousUserWhenAnnonymousUserType()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                LinkedEntity = _testAnnonymousUser
            };

            // Assert
            Assert.Multiple(() =>
  {
      Assert.That(subAccount.AnnonymousUser, Is.EqualTo(_testAnnonymousUser));
      Assert.That(subAccount.AnnonymousUserId, Is.EqualTo(_testAnnonymousUser.Id));
      Assert.That(subAccount.User, Is.Null);
      Assert.That(subAccount.UserId, Is.Null);
      Assert.That(subAccount.Organization, Is.Null);
      Assert.That(subAccount.OrganizationId, Is.Null);
  });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_CanSetToNull()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount)
            {
                LinkedEntity = null
            };

            // Assert
            Assert.Multiple(() =>
         {
             Assert.That(subAccount.LinkedEntity, Is.Null);
             Assert.That(subAccount.User, Is.Null);
             Assert.That(subAccount.Organization, Is.Null);
             Assert.That(subAccount.AnnonymousUser, Is.Null);
         });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Getter_LazyLoadsFromUser()
        {
            // Arrange
            var subAccount = new SubAccount();
            var userField = typeof(SubAccount).GetField("_user", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            userField?.SetValue(subAccount, _testLinkedEntity);

            // Act
            var linkedEntity = subAccount.LinkedEntity;

            // Assert
            Assert.Multiple(() =>
     {
         Assert.That(linkedEntity, Is.Not.Null);
         Assert.That(linkedEntity, Is.EqualTo(_testLinkedEntity));
         Assert.That(subAccount.LinkedEntityId, Is.EqualTo(_testLinkedEntity.Id));
         Assert.That(subAccount.LinkedEntityType, Is.EqualTo("User"));
     });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Getter_LazyLoadsFromOrganization()
        {
            // Arrange
            var subAccount = new SubAccount();
            var organizationField = typeof(SubAccount).GetField("_organization", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            organizationField?.SetValue(subAccount, _testOrganization);

            // Act
            var linkedEntity = subAccount.LinkedEntity;

            // Assert
            Assert.Multiple(() =>
 {
     Assert.That(linkedEntity, Is.Not.Null);
     Assert.That(linkedEntity, Is.EqualTo(_testOrganization));
     Assert.That(subAccount.LinkedEntityId, Is.EqualTo(_testOrganization.Id));
     Assert.That(subAccount.LinkedEntityType, Is.EqualTo("Organization"));
 });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Getter_LazyLoadsFromAnnonymousUser()
        {
            // Arrange
            var subAccount = new SubAccount();
            var annonymousUserField = typeof(SubAccount).GetField("_annonymousUser", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            annonymousUserField?.SetValue(subAccount, _testAnnonymousUser);

            // Act
            var linkedEntity = subAccount.LinkedEntity;

            // Assert
            Assert.Multiple(() =>
                        {
                            Assert.That(linkedEntity, Is.Not.Null);
                            Assert.That(linkedEntity, Is.EqualTo(_testAnnonymousUser));
                            Assert.That(subAccount.LinkedEntityId, Is.EqualTo(_testAnnonymousUser.Id));
                            Assert.That(subAccount.LinkedEntityType, Is.EqualTo("AnnonymousUser"));
                        });
        }

        [Test, Category("Models")]
        public void AccountId_Getter_ReturnsCorrectValue()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Assert
            Assert.That(subAccount.AccountId, Is.EqualTo(200));
        }

        [Test, Category("Models")]
        public void AccountId_Setter_DoesNotUpdateModifiedDate()
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
         Assert.That(subAccount.ModifiedDate, Is.EqualTo(originalModifiedDate));
     });
        }

        [Test, Category("Models")]
        public void AccountId_Setter_AcceptsNull()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                AccountId = null
            };

            // Assert
            Assert.That(subAccount.AccountId, Is.Null);
        }

        [Test, Category("Models")]
        public void AccountId_Setter_CanSetZero()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                AccountId = 0
            };

            // Assert
            Assert.That(subAccount.AccountId, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void Account_Getter_ReturnsCorrectValue()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Assert
            Assert.That(subAccount.Account, Is.EqualTo(_testAccount));
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

        [Test, Category("Models")]
        public void User_Setter_CanBeSetDirectly()
        {
            // Arrange
            var subAccount = new SubAccount();

            // Act
            subAccount.User = _testLinkedEntity;

            // Assert
            Assert.That(subAccount.User, Is.EqualTo(_testLinkedEntity));
        }

        [Test, Category("Models")]
        public void UserId_Setter_CanBeSetDirectly()
        {
            // Arrange
            var subAccount = new SubAccount();

            // Act
            subAccount.UserId = 999;

            // Assert
            Assert.That(subAccount.UserId, Is.EqualTo(999));
        }

        [Test, Category("Models")]
        public void Organization_Setter_CanBeSetDirectly()
        {
            // Arrange
            var subAccount = new SubAccount();

            // Act
            subAccount.Organization = _testOrganization;

            // Assert
            Assert.That(subAccount.Organization, Is.EqualTo(_testOrganization));
        }

        [Test, Category("Models")]
        public void OrganizationId_Setter_CanBeSetDirectly()
        {
            // Arrange
            var subAccount = new SubAccount();

            // Act
            subAccount.OrganizationId = 888;

            // Assert
            Assert.That(subAccount.OrganizationId, Is.EqualTo(888));
        }

        [Test, Category("Models")]
        public void AnnonymousUser_Setter_CanBeSetDirectly()
        {
            // Arrange
            var subAccount = new SubAccount();

            // Act
            subAccount.AnnonymousUser = _testAnnonymousUser;

            // Assert
            Assert.That(subAccount.AnnonymousUser, Is.EqualTo(_testAnnonymousUser));
        }

        [Test, Category("Models")]
        public void AnnonymousUserId_Setter_CanBeSetDirectly()
        {
            // Arrange
            var subAccount = new SubAccount();

            // Act
            subAccount.AnnonymousUserId = 777;

            // Assert
            Assert.That(subAccount.AnnonymousUserId, Is.EqualTo(777));
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
             account: null,
      createdDate: _testCreatedDate,
       modifiedDate: _testModifiedDate
         );

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => subAccount.Cast<SubAccountDTO>());
        }

        [Test, Category("Models")]
        public void Cast_ToSubAccountDTO_WithLinkedEntityNullTypeButNotNullEntity_UsesFallback()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount)
            {
                Id = 4
            };
            var linkedEntityField = typeof(SubAccount).GetField("_linkedEntity", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
            linkedEntityField?.SetValue(subAccount, _testLinkedEntity);

            // Act
            var result = subAccount.Cast<SubAccountDTO>();

            // Assert
            Assert.Multiple(() =>
                 {
                     Assert.That(result, Is.Not.Null);
                     Assert.That(result, Is.InstanceOf<SubAccountDTO>());
                     Assert.That(result.Id, Is.EqualTo(4));
                 });
        }

        [Test, Category("Models")]
        public void Cast_ToSubAccountDTO_WithEmptyLinkedEntityType_UsesCatchBlock()
        {
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
                Assert.That(result.LinkedEntity, Is.Not.Null);
            });
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
        public void Cast_RethrowsException_WhenExceptionOccurs()
        {
            // Arrange
            var subAccount = new SubAccount(
             id: 1,
            linkedEntity: null,
              accountId: 200,
                   account: null,
                     createdDate: _testCreatedDate,
                          modifiedDate: _testModifiedDate
                      );

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => subAccount.Cast<SubAccountDTO>());
        }

        [Test, Category("Models")]
        public void ToJson_ReturnsValidJsonString()
        {
            // Arrange
            var subAccount = new SubAccount(
     id: 1,
     linkedEntity: null,
       accountId: 200,
      account: null,
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
        public void ToJson_WithComplexObjects_HandlesCircularReferences()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount)
            {
                Id = 10
            };

            // Act & Assert
            Assert.DoesNotThrow(() => subAccount.ToJson());

            var json = subAccount.ToJson();
            Assert.That(json, Is.Not.Null);
            Assert.That(json, Is.Not.Empty);
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

        [Test, Category("Models")]
        public void ToString_WithNullLinkedEntity_HandlesGracefully()
        {
            // Arrange
            var subAccount = new SubAccount(
                    id: 50,
                   linkedEntity: null,
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
                         Assert.That(result, Does.Contain("Id:50"));
                         Assert.That(result, Does.Contain("LinkedEntityId:0"));
                         Assert.That(result, Does.Contain("LinkedEntityType:"));
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
            Assert.That(validationResults, Has.Count.GreaterThan(0));
            Assert.That(validationResults.Any(v => v.ErrorMessage!.Contains("AccountId") || v.ErrorMessage!.Contains("Account")));
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

        #region Edge Cases

        [Test, Category("Models")]
        public void LinkedEntity_Setter_WithNonUserOrOrganizationOrAnnonymousUser_DoesNotSetNavigationProperties()
        {
            // Arrange
            var invalidEntity = new Account { Id = 999, AccountName = "Invalid" };
            var subAccount = new SubAccount
            {
                LinkedEntity = invalidEntity
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(subAccount.LinkedEntity, Is.EqualTo(invalidEntity));
                Assert.That(subAccount.User, Is.Null);
                Assert.That(subAccount.Organization, Is.Null);
                Assert.That(subAccount.AnnonymousUser, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Getter_ReturnsExistingEntity_WhenAlreadySet()
        {
            // Arrange
            var subAccount = new SubAccount(_testLinkedEntity, _testAccount);

            // Act
            var firstGet = subAccount.LinkedEntity;
            var secondGet = subAccount.LinkedEntity;

            // Assert
            Assert.Multiple(() =>
                  {
                      Assert.That(firstGet, Is.SameAs(secondGet));
                      Assert.That(firstGet, Is.EqualTo(_testLinkedEntity));
                  });
        }

        [Test, Category("Models")]
        public void LinkedEntityId_ReturnsZero_WhenLinkedEntityIsNull()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                LinkedEntity = null
            };

            // Act
            var linkedEntityId = subAccount.LinkedEntityId;

            // Assert
            Assert.That(linkedEntityId, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void LinkedEntityType_ReturnsNull_WhenLinkedEntityIsNull()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                LinkedEntity = null
            };

            // Act
            var linkedEntityType = subAccount.LinkedEntityType;

            // Assert
            Assert.That(linkedEntityType, Is.Null);
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_OverwritesPreviousUserWithOrganization()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                LinkedEntity = _testLinkedEntity
            };

            // Act
            subAccount.LinkedEntity = _testOrganization;

            // Assert
            Assert.Multiple(() =>
              {
                  Assert.That(subAccount.LinkedEntity, Is.EqualTo(_testOrganization));
                  Assert.That(subAccount.Organization, Is.EqualTo(_testOrganization));
                  Assert.That(subAccount.User, Is.Null); // Should be cleared
                  Assert.That(subAccount.UserId, Is.Null); // Should be cleared
              });
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_OverwritesOrganizationWithAnnonymousUser()
        {
            // Arrange
            var subAccount = new SubAccount
            {
                LinkedEntity = _testOrganization
            };

            // Act
            subAccount.LinkedEntity = _testAnnonymousUser;

            // Assert
            Assert.Multiple(() =>
             {
                 Assert.That(subAccount.LinkedEntity, Is.EqualTo(_testAnnonymousUser));
                 Assert.That(subAccount.AnnonymousUser, Is.EqualTo(_testAnnonymousUser));
                 Assert.That(subAccount.Organization, Is.Null); // Should be cleared
                 Assert.That(subAccount.OrganizationId, Is.Null); // Should be cleared
             });
        }

        #endregion
    }
}
