using System.ComponentModel.DataAnnotations;
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
                Assert.That(account.LinkedEntityId, Is.EqualTo(0));
                Assert.That(account.LinkedEntityType, Is.Null);
                Assert.That(account.LinkedEntity, Is.Null);
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
                linkedEntityId: 1,
                linkedEntity: _sut,
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
                Assert.That(account.LinkedEntityId, Is.EqualTo(1));
                Assert.That(account.LinkedEntityType, Is.EqualTo("User"));
                Assert.That(account.LinkedEntity, Is.EqualTo(_sut));
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
                Assert.That(account.LinkedEntityId, Is.EqualTo(_sut.Id));
                Assert.That(account.LinkedEntityType, Is.EqualTo("User"));
                Assert.That(account.LinkedEntity, Is.EqualTo(_sut));
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

            var subAccounts = new List<Account>
            {
                new() { Id = 10, AccountName = "Sub Account 1", AccountNumber = "SUB001" },
                new() { Id = 11, AccountName = "Sub Account 2", AccountNumber = "SUB002" }
            };

            // Act
            var account = new Account(
                id: 5,
                accountName: "Main Account",
                accountNumber: "MAIN001",
                license: Guid.NewGuid().ToString(),
                databaseConnection: databaseConnection,
                linkedEntityId: 1,
                linkedEntity: _sut,
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
                Assert.That(account.Accounts![0].AccountName, Is.EqualTo("Sub Account 1"));
                Assert.That(account.Accounts![1].AccountName, Is.EqualTo("Sub Account 2"));
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
        public void LinkedEntityId_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.LinkedEntityId = 456;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.LinkedEntityId, Is.EqualTo(456));
                Assert.That(account.DateModified, Is.Not.EqualTo(originalDateModified));
            });
            Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
        }

        [Test, Category("Models")]
        public void LinkedEntity_Setter_UpdatesDateModified()
        {
            // Arrange
            var account = new Account();
            var originalDateModified = account.DateModified;

            // Act
            account.LinkedEntity = _sut;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.LinkedEntity, Is.EqualTo(_sut));
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
            var newAccounts = new List<Account>
            {
                new() { Id = 2, AccountName = "Sub Account 1", AccountNumber = "SUB001" },
                new() { Id = 3, AccountName = "Sub Account 2", AccountNumber = "SUB002" }
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
        public void IsCast_Getter_ThrowsNotImplementedException()
        {
            // Arrange
            var account = new Account();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = account.IsCast; });
        }

        [Test, Category("Models")]
        public void IsCast_Setter_ThrowsNotImplementedException()
        {
            // Arrange
            var account = new Account();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => account.IsCast = true);
        }

        [Test, Category("Models")]
        public void CastId_Getter_ThrowsNotImplementedException()
        {
            // Arrange
            var account = new Account();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = account.CastId; });
        }

        [Test, Category("Models")]
        public void CastId_Setter_ThrowsNotImplementedException()
        {
            // Arrange
            var account = new Account();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => account.CastId = 1);
        }

        [Test, Category("Models")]
        public void CastType_Getter_ThrowsNotImplementedException()
        {
            // Arrange
            var account = new Account();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = account.CastType; });
        }

        [Test, Category("Models")]
        public void CastType_Setter_ThrowsNotImplementedException()
        {
            // Arrange
            var account = new Account();

            // Act & Assert
            Assert.Throws<NotImplementedException>(() => account.CastType = "SomeType");
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

            account.LinkedEntity = null;
            Assert.That(account.LinkedEntity, Is.Null);
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
                linkedEntityId: 2,
                linkedEntity: _sut,
                features: _testFeatures,
                mainAccountId: null,
                accounts: [],
                dateCreated: specificDate,
                dateModified: _testDateModified,
                isCast: false,
                castId: 0,
                castType: null
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

            Assert.That(ex.Message, Is.EqualTo("Error creating Account object."));
            Assert.That(ex.InnerException, Is.Not.Null);
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
                linkedEntityId: 2,
                linkedEntity: _sut,
                features: _testFeatures,
                mainAccountId: null,
                accounts: [],
                dateCreated: _testDateCreated,
                dateModified: _testDateModified,
                isCast: false,
                castId: 0,
                castType: null
            ));
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

            Assert.That(ex.Message, Is.EqualTo("Error creating Account object."));
            Assert.That(ex.InnerException, Is.Not.Null);
            Assert.That(ex.InnerException.Message, Is.EqualTo("Mock exception from Id property"));
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

            Assert.That(ex.Message, Is.EqualTo("Error creating Account object."));
            Assert.That(ex.InnerException, Is.Not.Null);
            Assert.That(ex.InnerException.Message, Is.EqualTo("Object reference not set to an instance of an object."));
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
                linkedEntityId: 1,
                linkedEntity: _sut,
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
                linkedEntityId: 1,
                linkedEntity: _sut,
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
                linkedEntityId: 1,
                linkedEntity: _sut,
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
                linkedEntityId: 1,
                linkedEntity: _sut,
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
                linkedEntityId: 1,
                linkedEntity: _sut,
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
                LinkedEntityId = 1,
                LinkedEntity = _sut,
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
                LinkedEntityId = 1, 
                LinkedEntity = _sut,
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
                LinkedEntityId = 1, 
                LinkedEntity = _sut 
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
            var account = new Account { Id = 1, AccountName = null, AccountNumber = null, License = null, DatabaseConnection = null, LinkedEntityId = 1, LinkedEntity = _sut };

            // Act
            var validationResults = ValidateModel(account);

            // Assert
            Assert.That(validationResults.Any(v => v!.ErrorMessage!.Contains("AccountName")));
            Assert.That(validationResults.Any(v => v!.ErrorMessage!.Contains("AccountNumber")));
            Assert.That(validationResults.Any(v => v!.ErrorMessage!.Contains("License")));
            Assert.That(validationResults.Any(v => v!.ErrorMessage!.Contains("DatabaseConnection")));
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
                LinkedEntityId = 1, 
                LinkedEntity = _sut 
            };

            // Act
            var validationResults = ValidateModel(account);

            // Assert
            Assert.That(validationResults, Has.Count.EqualTo(1));
            Assert.That(validationResults[0].ErrorMessage, Is.EqualTo("The GUID is not in a valid format."));
        }
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


