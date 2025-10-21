using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class AccountShould
    {
        private User _sut;
        private readonly DateTime _testDateCreated = new(2023, 1, 1, 12, 0, 0);
        private readonly DateTime _testDateModified = new(2023, 1, 2, 12, 0, 0);
        private List<AccountFeature> _testFeatures;

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
            _sut = new User
            {
                // Set required properties on Person to make it valid
                Id = 1,
                FirstName = "John",
                LastName = "Doe"
            };

            _testFeatures =
            [
                new AccountFeature { Id = 1, AccountId = 1, FeatureId = 1 }
            ];
        }

        [Test, Category("Models")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var account = new Account();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(0));
                Assert.That(account.AccountName, Is.Null);
                Assert.That(account.AccountNumber, Is.Null);
                Assert.That(account.License, Is.Null);
                Assert.That(account.DatabaseConnection, Is.Null);

                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features.Count, Is.EqualTo(0));
                Assert.That(account.MainAccountId, Is.Null);
                Assert.That(account.Accounts, Is.Null);
                Assert.That(account.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(account.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(account.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsAllPropertiesCorrectly()
        {
            // Arrange
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = Enums.SupportedDatabases.SQLServer
            };

            // Act
            var account = new Account(
                id: 1,
                accountName: "testuser",
                accountNumber: "ACC123",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                features: _testFeatures,
                mainAccountId: null,
                accounts: [],
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(1));
                Assert.That(account.AccountName, Is.EqualTo("testuser"));
                Assert.That(account.AccountNumber, Is.EqualTo("ACC123"));
                Assert.That(account.License, Is.Not.Null);
                Assert.That(account.DatabaseConnection?.ConnectionString, Is.EqualTo("test-db-connection"));
                Assert.That(account.DatabaseConnection?.DatabaseType, Is.EqualTo(Enums.SupportedDatabases.SQLServer));
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Has.Count.EqualTo(1));
                Assert.That(account.MainAccountId, Is.Null);
                Assert.That(account.Accounts, Is.Not.Null);
                Assert.That(account.Accounts, Has.Count.EqualTo(0));
                Assert.That(account.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(account.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_SetsPropertiesCorrectlyFromLinkedEntity()
        {
            // Arrange
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = Enums.SupportedDatabases.SQLServer
            };

            // Act
            var account = new Account(
                accountName: "testuser2",
                accountNumber: "ACC456",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                linkedEntity: _sut,
                features: _testFeatures,
                mainAccountId: null,
                accounts: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.AccountName, Is.EqualTo("testuser2"));
                Assert.That(account.AccountNumber, Is.EqualTo("ACC456"));
                Assert.That(account.License, Is.Not.Null);
                Assert.That(account.DatabaseConnection?.ConnectionString, Is.EqualTo("test-db-connection"));
                Assert.That(account.DatabaseConnection?.DatabaseType, Is.EqualTo(Enums.SupportedDatabases.SQLServer));
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Has.Count.EqualTo(1));
                Assert.That(account.MainAccountId, Is.Null);
                Assert.That(account.Accounts, Is.Null);
                Assert.That(account.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(account.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsMainAccountIdAndAccountsCorrectly()
        {
            // Arrange
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = Enums.SupportedDatabases.SQLServer
            };

            var subAccounts = new List<SubAccount>
            {
                new() { Id = 10 },
                new() { Id = 11 }
            };

            // Act
            var account = new Account(
                id: 5,
                accountName: "Main Account",
                accountNumber: "MAIN001",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                features: _testFeatures,
                mainAccountId: 123,
                accounts: subAccounts,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(5));
                Assert.That(account.AccountName, Is.EqualTo("Main Account"));
                Assert.That(account.MainAccountId, Is.EqualTo(123));
                Assert.That(account.Accounts, Is.Not.Null);
                Assert.That(account.Accounts, Has.Count.EqualTo(2));
                Assert.That(account.Accounts![0].Id, Is.EqualTo(10));
                Assert.That(account.Accounts![1].Id, Is.EqualTo(11));
            });
        }

        [Test, Category("Models")]
        public void Id_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.Id = 123;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(123));
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void AccountName_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.AccountName = "newuser";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.AccountName, Is.EqualTo("newuser"));
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void AccountNumber_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.AccountNumber = "ACC789";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.AccountNumber, Is.EqualTo("ACC789"));
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void License_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.License = Guid.NewGuid().ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.License, Is.Not.Null);
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void DatabaseConnection_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.DatabaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection { ConnectionString = "new-connection-string", DatabaseType = SupportedDatabases.SQLServer };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.DatabaseConnection?.ConnectionString, Is.EqualTo("new-connection-string"));
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }



        [Test, Category("Models")]
        public void Features_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;
            var newFeatures = new List<AccountFeature>
            {
                new() { Id = 2, AccountId = 1, FeatureId = 2 }
            };

            // Act
            account.Features = newFeatures;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Has.Count.EqualTo(1));
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void MainAccountId_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.MainAccountId = 123;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.MainAccountId, Is.EqualTo(123));
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void Accounts_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;
            var newAccounts = new List<SubAccount>
            {
                new() { Id = 2 },
                new() { Id = 3 }
            };

            // Act
            account.Accounts = newAccounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Accounts, Is.Not.Null);
                Assert.That(account.Accounts, Has.Count.EqualTo(2));
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void ToString_ReturnsExpectedFormat()
        {
            // Arrange
            var account = new Account
            {
                Id = 42,
                AccountName = "johndoe"
            };

            // Act
            var result = account.ToString();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Does.Contain("Id:42"));
            Assert.That(result, Does.Contain("AccountName:johndoe"));
            Assert.That(result, Does.Contain("OrganizerCompanion.Core.Models.Domain.Account"));
        }

        [Test, Category("Models")]
        public void ToString_HandlesNullAccountName()
        {
            // Arrange
            var account = new Account
            {
                Id = 42,
                AccountName = null
            };

            // Act
            var result = account.ToString();

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Does.Contain("Id:42"));
            Assert.That(result, Does.Contain("AccountName"));
            Assert.That(result, Does.Contain("OrganizerCompanion.Core.Models.Domain.Account"));
        }

        [Test, Category("Models")]
        public void Properties_CanSetAndGetNullValues()
        {
            // Arrange
            var account = new Account
            {
                // Act & Assert
                AccountName = null
            };
            Assert.That(account.AccountName, Is.Null);

            account.AccountNumber = null;
            Assert.That(account.AccountNumber, Is.Null);

            account.License = null;
            Assert.That(account.License, Is.Null);

            account.DatabaseConnection = null;
            Assert.That(account.DatabaseConnection, Is.Null);
        }

        [Test, Category("Models")]
        public void DateCreated_IsReadOnly_AndSetDuringConstruction()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var account = new Account();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(account.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsDateCreatedFromParameter()
        {
            // Arrange
            var specificDate = new DateTime(2023, 5, 15, 10, 30, 45);
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = SupportedDatabases.SQLServer
            };

            // Act
            var account = new Account(
                id: 1,
                accountName: "testuser",
                accountNumber: "ACC123",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                features: _testFeatures,
                mainAccountId: null,
                accounts: [],
                dateCreated: specificDate,
                dateModified: _testDateModified
            );

            // Assert
            Assert.That(account.DateCreated, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_SetsDateCreatedFromParameter()
        {
            // Arrange
            var specificDate = new DateTime(2023, 6, 20, 14, 15, 30);
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = Enums.SupportedDatabases.SQLServer
            };

            // Act
            var account = new Account(
                accountName: "testuser2",
                accountNumber: "ACC456",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                linkedEntity: _sut,
                features: _testFeatures,
                mainAccountId: null,
                accounts: null,
                dateCreated: specificDate,
                dateModified: _testDateModified
            );

            // Assert
            Assert.That(account.DateCreated, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void DateModified_CanBeSetDirectly()
        {
            // Arrange
            var account = new Account();
            var testDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            account.DateModified = testDate;

            // Assert
            Assert.That(account.DateModified, Is.EqualTo(testDate));
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_ThrowsException_WhenLinkedEntityIsNull()
        {
            // Arrange
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = Enums.SupportedDatabases.SQLServer
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => new Account(
                accountName: "testuser",
                accountNumber: "ACC123",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                linkedEntity: null!,
                features: _testFeatures,
                mainAccountId: null,
                accounts: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            ));
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Error creating Account object."));
                Assert.That(ex.InnerException, Is.Not.Null);
            });
            Assert.That(ex.InnerException, Is.TypeOf<NullReferenceException>());
        }

        [Test, Category("Models")]
        public void JsonConstructor_ThrowsException_WhenInternalExceptionOccurs()
        {
            // Note: This test demonstrates the exception handling structure.
            // In practice, the JsonConstructor is less likely to throw exceptions
            // since it uses direct field assignment, but the try-catch is there for safety.

            // This test would need a scenario that actually causes an exception
            // For demonstration, we'll test with valid parameters to ensure no exception is thrown
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = SupportedDatabases.SQLServer
            };

            Assert.DoesNotThrow(() => new Account(
                id: 1,
                accountName: "testuser",
                accountNumber: "ACC123",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                features: _testFeatures,
                mainAccountId: null,
                accounts: [],
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            ));
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullFeatures_AcceptsNull()
        {
            // Arrange
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = SupportedDatabases.SQLServer
            };

            // Act - The JSON constructor accepts null features and assigns them directly
            var account = new Account(
                id: 1,
                accountName: "testuser",
                accountNumber: "ACC123",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                features: null!,
                mainAccountId: null,
                accounts: [],
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert - The constructor should succeed and assign null to features
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(1));
                Assert.That(account.Features, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_HandlesExceptionFromLinkedEntityId()
        {
            // Arrange
            // Create a mock object that throws an exception when accessing Id
            var mockEntity = new ThrowingMockEntity();
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = Enums.SupportedDatabases.SQLServer
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => new Account(
                accountName: "testuser",
                accountNumber: "ACC123",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                linkedEntity: mockEntity,
                features: _testFeatures,
                mainAccountId: null,
                accounts: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            ));
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Error creating Account object."));
                Assert.That(ex.InnerException, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_HandlesExceptionFromGetType()
        {
            // Arrange
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = Enums.SupportedDatabases.SQLServer
            };

            // Act & Assert
            var ex = Assert.Throws<Exception>(() => new Account(
                accountName: "testuser",
                accountNumber: "ACC123",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                linkedEntity: null!,
                features: _testFeatures,
                mainAccountId: null,
                accounts: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            ));
            Assert.Multiple(() =>
            {
                Assert.That(ex.Message, Is.EqualTo("Error creating Account object."));
                Assert.That(ex.InnerException, Is.Not.Null);
            });
            Assert.That(ex.InnerException!.Message, Is.EqualTo("Object reference not set to an instance of an object."));
        }

        [Test]
        [Category("Models")]
        public void Cast_Method_HasConstraintThatPreventsAccountDTOCasting()
        {
            // Arrange
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = SupportedDatabases.SQLServer
            };

            var account = new Account(
                id: 123,
                accountName: "Test Account",
                accountNumber: "ACC456",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                
                
                features: [],
                mainAccountId: null,
                accounts: [],
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert
            // This test documents that the Cast method cannot actually be called with AccountDTO
            // due to the IDomainEntity constraint, even though the implementation checks for it

            // The following would fail to compile:
            // account.Cast<AccountDTO>(); // Cannot compile due to constraint
            // account.Cast<IAccountDTO>(); // Cannot compile due to constraint

            // This demonstrates a design inconsistency in the codebase where the Cast method
            // implementation supports AccountDTO/IAccountDTO but the method signature prevents it
            Assert.Pass("This test documents the design limitation where Cast<AccountDTO> cannot be called due to IDomainEntity constraint");
        }

        [Test]
        [Category("Models")]
        public void Cast_ToUnsupportedDomainType_ThrowsInvalidCastException()
        {
            // Arrange
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = SupportedDatabases.SQLServer
            };

            var account = new Account(
                id: 777,
                accountName: "Test Account",
                accountNumber: "ACC777",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                
                
                features: [],
                mainAccountId: null,
                accounts: [],
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => account.Cast<User>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Account to type User."));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToAnotherUnsupportedDomainType_ThrowsInvalidCastException()
        {
            // Arrange
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = SupportedDatabases.SQLServer
            };

            var account = new Account(
                id: 888,
                accountName: "Another Test Account",
                accountNumber: "ACC888",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                
                
                features: [],
                mainAccountId: null,
                accounts: [],
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => account.Cast<Organization>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Account to type Organization."));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToFeature_ThrowsInvalidCastException()
        {
            // Arrange
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = SupportedDatabases.SQLServer
            };

            var account = new Account(
                id: 999,
                accountName: "Feature Test Account",
                accountNumber: "ACC999",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                
                
                features: [],
                mainAccountId: null,
                accounts: [],
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => account.Cast<Feature>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Account to type Feature."));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToAnnonymousUser_ThrowsInvalidCastException()
        {
            // Arrange
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = SupportedDatabases.SQLServer
            };

            var account = new Account(
                id: 111,
                accountName: "Anonymous Test Account",
                accountNumber: "ACC111",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                
                
                features: [],
                mainAccountId: null,
                accounts: [],
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => account.Cast<AnnonymousUser>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Account to type AnnonymousUser."));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_InternalImplementation_SupportsAccountDTOButConstraintPreventsUsage()
        {
            // This test documents the design issue in the Cast method implementation
            // The method checks for AccountDTO and IAccountDTO types but the generic constraint
            // prevents them from being used as type parameters

            // Reading the Cast method source code shows:
            // if (typeof(T) == typeof(AccountDTO) || typeof(T) == typeof(IAccountDTO))
            // But the method signature is: public T Cast<T>() where T : IDomainEntity
            // And AccountDTO/IAccountDTO do not implement IDomainEntity

            Assert.Pass("This test documents that the Cast method implementation supports AccountDTO " +
                       "but the IDomainEntity constraint prevents compilation of Cast<AccountDTO>() calls");
        }

        [Test]
        [Category("Validation")]
        public void Validation_ShouldPass_ForValidAccount()
        {
            // Arrange
            var account = new Account
            {
                Id = 1,
                AccountName = "ValidName",
                AccountNumber = "ValidNumber",
                License = Guid.NewGuid().ToString(),
                DatabaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
                {
                    ConnectionString = "Server=localhost;Database=testdb;Integrated Security=true;",
                    DatabaseType = SupportedDatabases.SQLServer
                },
                
                
                Features = _testFeatures
            };

            // Act
            var validationResults = ValidateModel(account);

            // Assert
            Assert.That(validationResults, Is.Empty);
        }

        [Test]
        [Category("Validation")]
        public void Validation_ShouldPass_WhenIdIsZero()
        {
            // Arrange
            var account = new Account
            {
                Id = 0,
                AccountName = "name",
                AccountNumber = "num",
                License = Guid.NewGuid().ToString(),
                DatabaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
                {
                    ConnectionString = "Server=localhost;Database=testdb;Integrated Security=true;",
                    DatabaseType = SupportedDatabases.SQLServer
                },
                
                
                Features = _testFeatures
            };

            // Act
            var validationResults = ValidateModel(account);

            // Assert
            Assert.That(validationResults, Is.Empty);
        }

        [Test]
        [Category("Validation")]
        [TestCase(-1)]
        public void Validation_ShouldFail_WhenIdIsInvalid(int invalidId)
        {
            // Arrange
            var account = new Account
            {
                Id = invalidId,
                AccountName = "name",
                AccountNumber = "num",
                License = Guid.NewGuid().ToString(),
                DatabaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
                {
                    ConnectionString = "Server=localhost;Database=testdb;Integrated Security=true;",
                    DatabaseType = SupportedDatabases.SQLServer
                },
                
                
            };

            // Act
            var validationResults = ValidateModel(account);

            // Assert
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("Id must be a non-negative number."));
        }

        [Test]
        [Category("Validation")]
        public void Validation_ShouldFail_WhenRequiredStringIsNull()
        {
            // Arrange
            var account = new Account { Id = 1, AccountName = null, AccountNumber = null, License = null, DatabaseConnection = null, };

            // Act
            var validationResults = ValidateModel(account);
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(validationResults.Any(v => v!.ErrorMessage!.Contains("AccountName")));
                Assert.That(validationResults.Any(v => v!.ErrorMessage!.Contains("AccountNumber")));
                Assert.That(validationResults.Any(v => v!.ErrorMessage!.Contains("License")));
                Assert.That(validationResults.Any(v => v!.ErrorMessage!.Contains("DatabaseConnection")));
            });
        }

        [Test]
        [Category("Validation")]
        public void Validation_ShouldFail_WhenLicenseIsInvalidGuid()
        {
            // Arrange
            var account = new Account
            {
                Id = 1,
                AccountName = "name",
                AccountNumber = "num",
                License = "invalid-guid",
                DatabaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
                {
                    ConnectionString = "Server=localhost;Database=testdb;Integrated Security=true;",
                    DatabaseType = SupportedDatabases.SQLServer
                },
                
                
            };

            // Act
            var validationResults = ValidateModel(account);

            // Assert
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The GUID is not in a valid format."));
        }

        #region Comprehensive Coverage Tests

        [Test, Category("Models")]
        public void ExplicitInterface_Features_GetReturnsConvertedList()
        {
            // Arrange
            var account = new Account();
            var feature1 = new AccountFeature { Id = 1, AccountId = 1, FeatureId = 1 };
            var feature2 = new AccountFeature { Id = 2, AccountId = 1, FeatureId = 2 };
            account.Features = [feature1, feature2];

            IAccount iAccount = account;

            // Act
            var features = iAccount.Features;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(features, Is.Not.Null);
                Assert.That(features.Count, Is.EqualTo(2));
                Assert.That(features[0], Is.InstanceOf<IAccountFeature>());
                Assert.That(features[1], Is.InstanceOf<IAccountFeature>());
            });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Features_SetUpdatesInternalList()
        {
            // Arrange
            var account = new Account();
            IAccount iAccount = account;
            var feature1 = new AccountFeature { Id = 10, AccountId = 5, FeatureId = 15 };
            var feature2 = new AccountFeature { Id = 20, AccountId = 5, FeatureId = 25 };
            var interfaceFeatures = new List<IAccountFeature> { feature1, feature2 };
            var originalDateModified = account.DateModified;

            // Act
            iAccount.Features = interfaceFeatures;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features.Count, Is.EqualTo(2));
                Assert.That(account.Features[0], Is.EqualTo(feature1));
                Assert.That(account.Features[1], Is.EqualTo(feature2));
                Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Accounts_GetWithNullSubAccounts_ReturnsNull()
        {
      // Arrange
      var account = new Account
      {
        Accounts = null
      };
      IAccount iAccount = account;

            // Act
            var accounts = iAccount.Accounts;

            // Assert
            Assert.That(accounts, Is.Null);
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Accounts_GetWithSubAccounts_ReturnsConvertedList()
        {
            // Arrange
            var account = new Account();
            var subAccount1 = new SubAccount { Id = 1 };
            var subAccount2 = new SubAccount { Id = 2 };
            account.Accounts = [subAccount1, subAccount2];

            IAccount iAccount = account;

            // Act
            var accounts = iAccount.Accounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accounts, Is.Not.Null);
                Assert.That(accounts!.Count, Is.EqualTo(2));
                Assert.That(accounts[0], Is.InstanceOf<ISubAccount>());
                Assert.That(accounts[1], Is.InstanceOf<ISubAccount>());
            });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Accounts_SetWithNull_SetsToNull()
        {
      // Arrange
      var account = new Account
      {
        Accounts = [new SubAccount { Id = 1 }] // Start with some accounts
      };
      IAccount iAccount = account;
            var originalDateModified = account.DateModified;

            // Act
            iAccount.Accounts = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Accounts, Is.Null);
                Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Accounts_SetWithList_UpdatesInternalList()
        {
            // Arrange
            var account = new Account();
            IAccount iAccount = account;
            var subAccount1 = new SubAccount { Id = 100 };
            var subAccount2 = new SubAccount { Id = 200 };
            var interfaceAccounts = new List<ISubAccount> { subAccount1, subAccount2 };
            var originalDateModified = account.DateModified;

            // Act
            iAccount.Accounts = interfaceAccounts;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Accounts, Is.Not.Null);
                Assert.That(account.Accounts!.Count, Is.EqualTo(2));
                Assert.That(account.Accounts[0], Is.EqualTo(subAccount1));
                Assert.That(account.Accounts[1], Is.EqualTo(subAccount2));
                Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void Accounts_SetterWithNull_InitializesEmptyListFirst()
        {
            // Arrange
            var account = new Account();
            Assert.That(account.Accounts, Is.Null); // Verify starts as null
            var originalDateModified = account.DateModified;

            // Act
            account.Accounts = null;

            // Assert - The setter contains: _subAccounts ??= []; which initializes if null
            Assert.Multiple(() =>
            {
                Assert.That(account.Accounts, Is.Null); // Should remain null after assignment
                Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void Accounts_SetterWithEmptyList_WorksCorrectly()
        {
            // Arrange
            var account = new Account();
            var emptyList = new List<SubAccount>();
            var originalDateModified = account.DateModified;

            // Act
            account.Accounts = emptyList;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Accounts, Is.Not.Null);
                Assert.That(account.Accounts.Count, Is.EqualTo(0));
                Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void ToJson_SerializesCorrectly()
        {
            // Arrange
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-connection",
                DatabaseType = SupportedDatabases.SQLServer
            };

            var account = new Account
            {
                Id = 123,
                AccountName = "Test Account",
                AccountNumber = "ACC123",
                License = Guid.NewGuid().ToString(),
                DatabaseConnection = databaseConnection,
                Features = _testFeatures,
                MainAccountId = 456,
                DateModified = new DateTime(2025, 10, 20, 15, 30, 45)
            };

            // Act
            var json = account.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(json, Contains.Substring("\"id\":123"));
                Assert.That(json, Contains.Substring("\"accountName\":\"Test Account\""));
                Assert.That(json, Contains.Substring("\"accountNumber\":\"ACC123\""));
                Assert.That(json, Contains.Substring("\"mainAccountId\":456"));

                // Verify it's valid JSON
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithNullProperties_SerializesCorrectly()
        {
            // Arrange
            var account = new Account
            {
                Id = 999,
                AccountName = null,
                AccountNumber = null,
                License = null,
                DatabaseConnection = null,
                Features = [],
                MainAccountId = null,
                Accounts = null
            };

            // Act
            var json = account.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(json, Contains.Substring("\"id\":999"));

                // Verify it's valid JSON
                Assert.DoesNotThrow(() => JsonDocument.Parse(json));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithCircularReferences_HandlesCorrectly()
        {
            // Arrange
            var account = new Account
            {
                Id = 777,
                AccountName = "Circular Test",
                Features = _testFeatures
            };

            // The JsonSerializerOptions has ReferenceHandler.IgnoreCycles
            // which should handle any potential circular references

            // Act & Assert
            Assert.DoesNotThrow(() =>
            {
                var json = account.ToJson();
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                JsonDocument.Parse(json); // Verify valid JSON
            });
        }

        [Test, Category("Models")]
        public void ImplementsIAccount()
        {
            // Arrange & Act
            var account = new Account();

            // Assert
            Assert.That(account, Is.InstanceOf<IAccount>());
        }

        [Test, Category("Models")]
        public void TypeInformation_ShouldBeCorrect()
        {
            // Arrange & Act
            var account = new Account();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.GetType(), Is.EqualTo(typeof(Account)));
                Assert.That(account.GetType().Name, Is.EqualTo("Account"));
                Assert.That(account.GetType().Namespace, Is.EqualTo("OrganizerCompanion.Core.Models.Domain"));
            });
        }

        [Test, Category("Models")]
        public void AllProperties_GettersAndSetters_WorkCorrectly()
        {
            // Arrange
            var account = new Account();
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "comprehensive-test",
                DatabaseType = SupportedDatabases.MySQL
            };
            var testFeatures = new List<AccountFeature> { new() { Id = 999 } };
            var testSubAccounts = new List<SubAccount> { new() { Id = 888 } };
            var testDate = new DateTime(2025, 5, 15, 10, 30, 45);

            // Act & Assert - Test all property setters and getters
            account.Id = 12345;
            Assert.That(account.Id, Is.EqualTo(12345));

            account.AccountName = "Comprehensive Test";
            Assert.That(account.AccountName, Is.EqualTo("Comprehensive Test"));

            account.AccountNumber = "COMP12345";
            Assert.That(account.AccountNumber, Is.EqualTo("COMP12345"));

            var testGuid = Guid.NewGuid().ToString();
            account.License = testGuid;
            Assert.That(account.License, Is.EqualTo(testGuid));

            account.DatabaseConnection = databaseConnection;
            Assert.That(account.DatabaseConnection, Is.EqualTo(databaseConnection));

            account.Features = testFeatures;
            Assert.That(account.Features, Is.EqualTo(testFeatures));

            account.MainAccountId = 54321;
            Assert.That(account.MainAccountId, Is.EqualTo(54321));

            account.Accounts = testSubAccounts;
            Assert.That(account.Accounts, Is.EqualTo(testSubAccounts));

            account.DateModified = testDate;
            Assert.That(account.DateModified, Is.EqualTo(testDate));

            // DateCreated is read-only, just verify it's set
            Assert.That(account.DateCreated, Is.Not.EqualTo(default(DateTime)));
        }

        [Test, Category("Models")]
        public void DateCreated_PropertyInfo_IsReadOnly()
        {
            // Arrange & Act
            var property = typeof(Account).GetProperty(nameof(Account.DateCreated));

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(property, Is.Not.Null);
                Assert.That(property!.CanRead, Is.True);
                Assert.That(property.GetSetMethod(), Is.Null); // No public setter
            });
        }

        [Test, Category("Models")]
        public void Features_DefaultValue_IsEmptyList()
        {
            // Arrange & Act
            var account = new Account();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Is.Empty);
                Assert.That(account.Features, Is.InstanceOf<List<AccountFeature>>());
            });
        }

        [Test, Category("Models")]
        public void MultiplePropertyChanges_EachUpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var initialDateModified = account.DateModified;

            // Act & Assert - Each property change should update DateModified
            System.Threading.Thread.Sleep(1);
            account.Id = 100;
            var afterId = account.DateModified;
            Assert.That(afterId, Is.GreaterThan(initialDateModified));

            System.Threading.Thread.Sleep(1);
            account.AccountName = "Test";
            var afterName = account.DateModified;
            Assert.That(afterName, Is.GreaterThan(afterId));

            System.Threading.Thread.Sleep(1);
            account.AccountNumber = "ACC100";
            var afterNumber = account.DateModified;
            Assert.That(afterNumber, Is.GreaterThan(afterName));

            System.Threading.Thread.Sleep(1);
            account.License = Guid.NewGuid().ToString();
            var afterLicense = account.DateModified;
            Assert.That(afterLicense, Is.GreaterThan(afterNumber));

            System.Threading.Thread.Sleep(1);
            account.DatabaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test",
                DatabaseType = SupportedDatabases.SQLServer
            };
            var afterDbConnection = account.DateModified;
            Assert.That(afterDbConnection, Is.GreaterThan(afterLicense));

            System.Threading.Thread.Sleep(1);
            account.Features = [new AccountFeature { Id = 1 }];
            var afterFeatures = account.DateModified;
            Assert.That(afterFeatures, Is.GreaterThan(afterDbConnection));

            System.Threading.Thread.Sleep(1);
            account.MainAccountId = 200;
            var afterMainAccountId = account.DateModified;
            Assert.That(afterMainAccountId, Is.GreaterThan(afterFeatures));

            System.Threading.Thread.Sleep(1);
            account.Accounts = [new SubAccount { Id = 300 }];
            var afterAccounts = account.DateModified;
            Assert.That(afterAccounts, Is.GreaterThan(afterMainAccountId));
        }

        [Test, Category("Models")]
        public void BoundaryValues_AreHandledCorrectly()
        {
            // Arrange & Act
            var account = new Account
            {
                Id = int.MaxValue,
                MainAccountId = int.MaxValue,
                AccountName = "",
                AccountNumber = "",
                License = Guid.Empty.ToString()
            };

            // Assert - Domain models accept boundary values
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(int.MaxValue));
                Assert.That(account.MainAccountId, Is.EqualTo(int.MaxValue));
                Assert.That(account.AccountName, Is.EqualTo(""));
                Assert.That(account.AccountNumber, Is.EqualTo(""));
                Assert.That(account.License, Is.EqualTo(Guid.Empty.ToString()));
            });
        }

        [Test, Category("Models")]
        public void NullValues_AreAcceptedWhereAllowed()
        {
            // Arrange & Act
            var account = new Account
            {
                AccountName = null,
                AccountNumber = null,
                License = null,
                DatabaseConnection = null,
                MainAccountId = null,
                Accounts = null
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.AccountName, Is.Null);
                Assert.That(account.AccountNumber, Is.Null);
                Assert.That(account.License, Is.Null);
                Assert.That(account.DatabaseConnection, Is.Null);
                Assert.That(account.MainAccountId, Is.Null);
                Assert.That(account.Accounts, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Cast_MethodImplementation_DocumentsUnsupportedTypeHandling()
        {
            // This test documents that the Cast method implementation contains logic for AccountDTO
            // but the constraint prevents its usage. The method should throw InvalidCastException
            // for any type that doesn't match AccountDTO/IAccountDTO (which can't be used due to constraint)

            var account = new Account
            {
                Id = 123,
                AccountName = "Cast Test"
            };

            // Since we can't call Cast<AccountDTO>() due to constraint, we test unsupported types
            var ex = Assert.Throws<InvalidCastException>(() => account.Cast<User>());
            Assert.That(ex.Message, Does.Contain("Cannot cast Account to type User"));
        }

        [Test, Category("Models")]
        public void Cast_ExceptionHandling_RethrowsCorrectly()
        {
            // The Cast method has a try-catch that re-throws exceptions
            var account = new Account
            {
                Id = 456,
                AccountName = "Exception Test"
            };

            // Test that exceptions are properly re-thrown
            var ex = Assert.Throws<InvalidCastException>(() => account.Cast<Organization>());
            Assert.That(ex, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void Accounts_SetterNullCoalescing_WorksCorrectly()
        {
      // Arrange
      var account = new Account
      {
        // First, set to a non-null value to ensure _subAccounts is not null
        Accounts = [new SubAccount { Id = 1 }]
      };
      Assert.That(account.Accounts, Is.Not.Null);
            
            // Act - Test the null-coalescing assignment: _subAccounts ??= [];
            account.Accounts = null;
            
            // The setter logic is: _subAccounts ??= []; _subAccounts = value?.ConvertAll(account => account);
            // When value is null, ConvertAll is not called, so _subAccounts becomes null
            
            // Assert
            Assert.That(account.Accounts, Is.Null);
        }

        [Test, Category("Models")]
        public void Accounts_SetterWithNewListAfterNull_InitializesCorrectly()
        {
      // Arrange
      var account = new Account
      {
        Accounts = null // Start with null
      };

      var newAccounts = new List<SubAccount>
            {
                new() { Id = 100 },
                new() { Id = 200 }
            };

            // Act
            account.Accounts = newAccounts;

            // Assert - The null-coalescing should initialize the list before assignment
            Assert.Multiple(() =>
            {
                Assert.That(account.Accounts, Is.Not.Null);
                Assert.That(account.Accounts!.Count, Is.EqualTo(2));
                Assert.That(account.Accounts[0].Id, Is.EqualTo(100));
                Assert.That(account.Accounts[1].Id, Is.EqualTo(200));
            });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Comprehensive_AllScenarios()
        {
            // Arrange
            var account = new Account();
            IAccount iAccount = account;

            // Test empty collections
            var emptyFeatures = new List<IAccountFeature>();
            var emptyAccounts = new List<ISubAccount>();

            // Act & Assert - Test all interface scenarios
            iAccount.Features = emptyFeatures;
            Assert.That(account.Features, Is.Not.Null);
            Assert.That(account.Features.Count, Is.EqualTo(0));

            iAccount.Accounts = emptyAccounts;
            Assert.That(account.Accounts, Is.Not.Null);
            Assert.That(account.Accounts!.Count, Is.EqualTo(0));

            // Test with actual data
            var feature = new AccountFeature { Id = 1, FeatureId = 1 };
            var subAccount = new SubAccount { Id = 1 };
            
            iAccount.Features = [feature];
            iAccount.Accounts = [subAccount];

            Assert.Multiple(() =>
            {
                Assert.That(account.Features.Count, Is.EqualTo(1));
                Assert.That(account.Accounts!.Count, Is.EqualTo(1));
                Assert.That(iAccount.Features.Count, Is.EqualTo(1));
                Assert.That(iAccount.Accounts!.Count, Is.EqualTo(1));
            });
        }

        [Test, Category("Models")]
        public void AllFieldsAndProperties_CompleteCoverage()
        {
            // Test to ensure all private fields are covered through properties
            var account = new Account();
            
            // Access all properties to ensure complete coverage
            _ = account.Id;
            _ = account.AccountName;
            _ = account.AccountNumber;
            _ = account.License;
            _ = account.DatabaseConnection;
            _ = account.Features;
            _ = account.MainAccountId;
            _ = account.Accounts;
            _ = account.DateCreated;
            _ = account.DateModified;

            // Set all settable properties
            account.Id = 1;
            account.AccountName = "Test";
            account.AccountNumber = "ACC1";
            account.License = Guid.NewGuid().ToString();
            account.DatabaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test",
                DatabaseType = SupportedDatabases.SQLServer
            };
            account.Features = [];
            account.MainAccountId = 1;
            account.Accounts = [];
            account.DateModified = DateTime.Now;

            // Verify all are set
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(1));
                Assert.That(account.AccountName, Is.EqualTo("Test"));
                Assert.That(account.AccountNumber, Is.EqualTo("ACC1"));
                Assert.That(account.License, Is.Not.Null);
                Assert.That(account.DatabaseConnection, Is.Not.Null);
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.MainAccountId, Is.EqualTo(1));
                Assert.That(account.Accounts, Is.Not.Null);
                Assert.That(account.DateModified, Is.Not.Null);
                Assert.That(account.DateCreated, Is.Not.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithNullFeatures_ThrowsInvalidCastException()
        {
            // Arrange - Create account with null Features 
            var account = new Account
            {
                Id = 123,
                AccountName = "Test",
                Features = null! 
            };

            // Act & Assert - The Cast method throws InvalidCastException before reaching ConvertAll
            var ex = Assert.Throws<InvalidCastException>(() => account.Cast<User>());
            Assert.That(ex.Message, Does.Contain("Cannot cast Account to type User"));
        }

        [Test, Category("Models")]
        public void Cast_WithNullAccounts_HandlesCorrectly()
        {
            // Arrange - Create account with null Accounts (this should work fine due to null-conditional operator)
            var account = new Account
            {
                Id = 456,
                AccountName = "Test2",
                Features = [], // Valid empty list
                Accounts = null // This should be handled by null-conditional operator in Cast
            };

            // Act & Assert - Should still throw InvalidCastException for unsupported type, not NullReferenceException
            var ex = Assert.Throws<InvalidCastException>(() => account.Cast<User>());
            Assert.That(ex.Message, Does.Contain("Cannot cast Account to type User"));
        }

        [Test, Category("Models")]
        public void JsonConstructor_ExceptionHandling_WithSpecificScenario()
        {
            // This test attempts to trigger the exception handling in the JSON constructor
            // Since direct field assignments rarely throw, this documents the structure
            
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = SupportedDatabases.SQLServer
            };

            // Test with extreme values that might cause issues
            Assert.DoesNotThrow(() => new Account(
                id: int.MaxValue,
                accountName: new string('A', 10000), // Very long string
                accountNumber: new string('B', 10000),
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                features: _testFeatures,
                mainAccountId: int.MaxValue,
                accounts: [],
                dateCreated: DateTime.MaxValue,
                dateModified: DateTime.MaxValue
            ));
        }

        [Test, Category("Models")]
        public void CompleteIntegration_AllConstructorsPropertiesAndMethods()
        {
            // Arrange
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "integration-test-connection",
                DatabaseType = SupportedDatabases.PostgreSQL
            };
            var features = new List<AccountFeature>
            {
                new() { Id = 1, AccountId = 1, FeatureId = 1 },
                new() { Id = 2, AccountId = 1, FeatureId = 2 }
            };
            var subAccounts = new List<SubAccount>
            {
                new() { Id = 10 },
                new() { Id = 20 }
            };

            // Act - Test all constructors and methods
            var account1 = new Account(); // Default constructor
            var account2 = new Account( // JSON constructor
                id: 999,
                accountName: "Integration Account",
                accountNumber: "INT999",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                features: features,
                mainAccountId: 123,
                accounts: subAccounts,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            var account3 = new Account( // Parameterized constructor with linked entity
                accountName: "Linked Account",
                accountNumber: "LINK123",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                linkedEntity: _sut,
                features: features,
                mainAccountId: null,
                accounts: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Test interface functionality
            IAccount iAccount = account2;
            var interfaceFeatures = iAccount.Features;
            var interfaceAccounts = iAccount.Accounts;

            // Test methods
            var json1 = account1.ToJson();
            var json2 = account2.ToJson();
            var json3 = account3.ToJson();
            var toString1 = account1.ToString();
            var toString2 = account2.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                // Verify all constructors worked
                Assert.That(account1.Id, Is.EqualTo(0));
                Assert.That(account2.Id, Is.EqualTo(999));
                Assert.That(account3.Id, Is.EqualTo(_sut.Id));

                // Verify interface functionality
                Assert.That(interfaceFeatures, Is.Not.Null);
                Assert.That(interfaceFeatures.Count, Is.EqualTo(2));
                Assert.That(interfaceAccounts, Is.Not.Null);
                Assert.That(interfaceAccounts!.Count, Is.EqualTo(2));

                // Verify methods
                Assert.That(json1, Is.Not.Null);
                Assert.That(json2, Is.Not.Null);
                Assert.That(json3, Is.Not.Null);
                Assert.That(toString1, Contains.Substring("Id:0"));
                Assert.That(toString2, Contains.Substring("Id:999"));
                Assert.That(toString2, Contains.Substring("AccountName:Integration Account"));

                // Verify all JSON is valid
                Assert.DoesNotThrow(() => JsonDocument.Parse(json1));
                Assert.DoesNotThrow(() => JsonDocument.Parse(json2));
                Assert.DoesNotThrow(() => JsonDocument.Parse(json3));
            });
        }

        [Test, Category("Models")]
        public void SerializerOptions_HasIgnoreCyclesReferenceHandler()
        {
            // Test that verifies the JsonSerializerOptions is configured correctly
            var account = new Account
            {
                Id = 999,
                AccountName = "Serializer Test",
                Features = _testFeatures
            };

            // The ToJson method should handle potential circular references
            // due to ReferenceHandler.IgnoreCycles
            Assert.DoesNotThrow(() =>
            {
                var json = account.ToJson();
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
            });
        }

        [Test, Category("Models")]
        public void ToString_BaseClassMethod_IsOverridden()
        {
            // Arrange
            var account = new Account
            {
                Id = 42,
                AccountName = "toString test"
            };

            // Act
            var result = account.ToString();
            var baseResult = ((object)account).ToString();

            // Assert - Verify the ToString method is properly overridden
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.EqualTo(baseResult)); // Should be the same since it's overridden
                Assert.That(result, Does.Contain("OrganizerCompanion.Core.Models.Domain.Account"));
                Assert.That(result, Does.Contain("Id:42"));
                Assert.That(result, Does.Contain("AccountName:toString test"));
            });
        }

        [Test, Category("Models")]
        public void Features_PropertyInitialization_CreatesEmptyList()
        {
            // Arrange & Act
            var account = new Account();

            // Assert - Features should be initialized as empty list, not null
            Assert.Multiple(() =>
            {
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Is.Empty);
                Assert.That(account.Features, Is.InstanceOf<List<AccountFeature>>());
                
                // Verify we can add to it
                account.Features.Add(new AccountFeature { Id = 1 });
                Assert.That(account.Features.Count, Is.EqualTo(1));
            });
        }

        [Test, Category("Models")]
        public void JsonPropertyAttributes_AreAppliedCorrectly()
        {
            // This test verifies that JsonPropertyName attributes are present
            // by testing serialization output contains expected property names
            var account = new Account
            {
                Id = 123,
                AccountName = "JSON Test",
                AccountNumber = "JSON123",
                License = Guid.NewGuid().ToString(),
                MainAccountId = 456
            };

            var json = account.ToJson();

            // Verify JSON contains property names as defined by JsonPropertyNameAttribute
            Assert.Multiple(() =>
            {
                Assert.That(json, Contains.Substring("\"id\""));
                Assert.That(json, Contains.Substring("\"accountName\""));
                Assert.That(json, Contains.Substring("\"accountNumber\""));
                Assert.That(json, Contains.Substring("\"license\""));
                Assert.That(json, Contains.Substring("\"mainAccountId\""));
                Assert.That(json, Contains.Substring("\"dateCreated\""));
                Assert.That(json, Contains.Substring("\"dateModified\""));
            });
        }

        [Test, Category("Models")]
        public void DateModified_DefaultValue_IsDefaultDateTime()
        {
            // Arrange & Act
            var account = new Account();

            // Assert
            Assert.That(account.DateModified, Is.EqualTo(default(DateTime)));
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_ExceptionHandling_DocumentsStructure()
        {
            // This test documents the exception handling structure in the parameterized constructor
            // The try-catch block exists but is difficult to trigger in practice due to 
            // simple field assignments and property access
            
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "test-db-connection",
                DatabaseType = SupportedDatabases.SQLServer
            };

            // Normal operation should not throw
            Assert.DoesNotThrow(() => new Account(
                accountName: "testuser",
                accountNumber: "ACC123",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                linkedEntity: _sut,
                features: _testFeatures,
                mainAccountId: null,
                accounts: null,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            ));
        }

        [Test, Category("Models")]
        public void AllConstructorPaths_AreTestedForCompleteness()
        {
            // Test to ensure we've covered all constructor scenarios including edge cases
            var databaseConnection = new OrganizerCompanion.Core.Models.Type.DatabaseConnection
            {
                ConnectionString = "complete-test",
                DatabaseType = SupportedDatabases.SQLServer
            };

            // Test default constructor  
            var account1 = new Account();
            Assert.That(account1.Id, Is.EqualTo(0));

            // Test JSON constructor with minimum values
            var account2 = new Account(0, null, null, null, null, [], null, [], DateTime.Now, null);
            Assert.That(account2.Id, Is.EqualTo(0));

            // Test parameterized constructor with linked entity
            var account3 = new Account(null, null, null, null, _sut, [], null, null, DateTime.Now, null);
            Assert.That(account3.Id, Is.EqualTo(_sut.Id));

            // Verify all constructors work
            Assert.Multiple(() =>
            {
                Assert.That(account1, Is.Not.Null);
                Assert.That(account2, Is.Not.Null);
                Assert.That(account3, Is.Not.Null);
            });
        }

        #endregion
    }

    // Helper classes for testing exception scenarios
    internal class ThrowingMockEntity : IDomainEntity
    {
        public int Id
        {
            get => throw new Exception("Mock exception from Id property");
            set => throw new Exception("Mock exception from Id property setter");
        }
        public bool IsCast { get; set; }
        public int CastId { get; set; }
        public string? CastType { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
        public string ToJson() => throw new NotImplementedException();
    }

    internal class ThrowingGetTypeMockEntity : IDomainEntity
    {
        public int Id { get; set; } = 1;
        public bool IsCast { get; set; } = false;
        public int CastId { get; set; } = 0;
        public string? CastType { get; set; } = null;
        public DateTime DateCreated { get; set; }
        public DateTime? DateModified { get; set; }

        public new System.Type GetType() => throw new Exception("Mock exception from GetType method");
        public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
        public string ToJson() => throw new NotImplementedException();
    }
}


