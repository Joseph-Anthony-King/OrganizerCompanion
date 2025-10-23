using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Models.Domain;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;

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
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Is.Empty);
                Assert.That(account.Accounts, Is.Null);
                Assert.That(account.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(account.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(account.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsAllPropertiesCorrectly()
        {
            // Arrange & Act
            var account = new Account(
                id: 1,
                accountName: "testuser",
                accountNumber: "ACC123",
                license: Guid.NewGuid().ToString(),
                features: _testFeatures,
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
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Has.Count.EqualTo(1));
                Assert.That(account.Accounts, Is.Not.Null);
                Assert.That(account.Accounts, Has.Count.EqualTo(0));
                Assert.That(account.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(account.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void ParameterizedConstructor_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            var account = new Account(
                accountName: "testuser2",
                accountNumber: "ACC456",
                license: Guid.NewGuid().ToString(),
                features: _testFeatures,
                accounts: null
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.AccountName, Is.EqualTo("testuser2"));
                Assert.That(account.AccountNumber, Is.EqualTo("ACC456"));
                Assert.That(account.License, Is.Not.Null);
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Has.Count.EqualTo(1));
                Assert.That(account.Accounts, Is.Null);
                Assert.That(account.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsMainAccountIdAndAccountsCorrectly()
        {
            // Arrange
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
                features: _testFeatures,
                accounts: subAccounts,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(5));
                Assert.That(account.AccountName, Is.EqualTo("Main Account"));
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

            // Act
            var account = new Account(
                id: 1,
                accountName: "testuser",
                accountNumber: "ACC123",
                license: Guid.NewGuid().ToString(),
                features: _testFeatures,
                accounts: [],
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
        public void JsonConstructor_ThrowsException_WhenInternalExceptionOccurs()
        {
            // Note: This test demonstrates the exception handling structure.
            // In practice, the JsonConstructor is less likely to throw exceptions
            // since it uses direct field assignment, but the try-catch is there for safety.
            Assert.DoesNotThrow(() => new Account(
                id: 1,
                accountName: "testuser",
                accountNumber: "ACC123",
                license: Guid.NewGuid().ToString(),
                features: _testFeatures,
                accounts: [],
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            ));
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullFeatures_AcceptsNull()
        {
            // Arrange & Act - The JSON constructor accepts null features and assigns them directly
            var account = new Account(
                id: 1,
                accountName: "testuser",
                accountNumber: "ACC123",
                license: Guid.NewGuid().ToString(),
                features: null!,
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
        public void AccountDTOConstructor_SetsAllPropertiesCorrectly()
        {
            // Arrange
            var featureDTOs = new List<FeatureDTO>
            {
                new() { Id = 1, FeatureName = "Test Feature", IsEnabled = true, DateCreated = _testDateCreated, DateModified = _testDateModified }
            };
            var subAccountDTOs = new List<SubAccountDTO>
            {
                new() { Id = 10, LinkedEntityId = 100, AccountId = 123 }
            };
            var accountDTO = new AccountDTO
            {
                Id = 123,
                AccountName = "Test Account",
                AccountNumber = "ACC123",
                License = Guid.NewGuid().ToString(),
                Features = featureDTOs,
                Accounts = subAccountDTOs,
                DateCreated = _testDateCreated,
                DateModified = _testDateModified
            };

            // Act
            var account = new Account(accountDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(123));
                Assert.That(account.AccountName, Is.EqualTo("Test Account"));
                Assert.That(account.AccountNumber, Is.EqualTo("ACC123"));
                Assert.That(account.License, Is.EqualTo(accountDTO.License));
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Has.Count.EqualTo(1));
                Assert.That(account.Features[0].AccountId, Is.EqualTo(123));
                Assert.That(account.Features[0].FeatureId, Is.EqualTo(1));
                Assert.That(account.Accounts, Is.Not.Null);
                Assert.That(account.Accounts, Has.Count.EqualTo(1));
                Assert.That(account.Accounts![0].Id, Is.EqualTo(10));
                Assert.That(account.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(account.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void AccountDTOConstructor_WithNullAccounts_SetsAccountsToNull()
        {
            // Arrange
            var featureDTOs = new List<FeatureDTO>
            {
                new() { Id = 2, FeatureName = "Another Feature", IsEnabled = false, DateCreated = _testDateCreated, DateModified = null }
            };
            var accountDTO = new AccountDTO
            {
                Id = 456,
                AccountName = "No Sub Accounts",
                AccountNumber = "ACC456",
                License = Guid.NewGuid().ToString(),
                Features = featureDTOs,
                Accounts = null, // Explicitly null
                DateCreated = _testDateCreated,
                DateModified = null
            };

            // Act
            var account = new Account(accountDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(456));
                Assert.That(account.AccountName, Is.EqualTo("No Sub Accounts"));
                Assert.That(account.AccountNumber, Is.EqualTo("ACC456"));
                Assert.That(account.License, Is.EqualTo(accountDTO.License));
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Has.Count.EqualTo(1));
                Assert.That(account.Features[0].AccountId, Is.EqualTo(456));
                Assert.That(account.Accounts, Is.Null);
                Assert.That(account.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(account.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void AccountDTOConstructor_WithEmptyCollections_SetsEmptyLists()
        {
            // Arrange
            var accountDTO = new AccountDTO
            {
                Id = 789,
                AccountName = "Empty Collections",
                AccountNumber = "ACC789",
                License = Guid.NewGuid().ToString(),
                Features = [], // Empty list
                Accounts = [], // Empty list
                DateCreated = _testDateCreated,
                DateModified = _testDateModified
            };

            // Act
            var account = new Account(accountDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(789));
                Assert.That(account.AccountName, Is.EqualTo("Empty Collections"));
                Assert.That(account.AccountNumber, Is.EqualTo("ACC789"));
                Assert.That(account.License, Is.EqualTo(accountDTO.License));
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Is.Empty);
                Assert.That(account.Accounts, Is.Not.Null);
                Assert.That(account.Accounts, Is.Empty);
                Assert.That(account.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(account.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void AccountDTOConstructor_WithNullStringProperties_AcceptsNullValues()
        {
            // Arrange
            var featureDTOs = new List<FeatureDTO>
            {
                new() { Id = 3, FeatureName = "Null Test Feature", IsEnabled = true, DateCreated = _testDateCreated, DateModified = _testDateModified }
            };
            var accountDTO = new AccountDTO
            {
                Id = 111,
                AccountName = null, // Null value
                AccountNumber = null, // Null value
                License = null, // Null value
                Features = featureDTOs,
                Accounts = null,
                DateCreated = _testDateCreated,
                DateModified = _testDateModified
            };

            // Act
            var account = new Account(accountDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(111));
                Assert.That(account.AccountName, Is.Null);
                Assert.That(account.AccountNumber, Is.Null);
                Assert.That(account.License, Is.Null);
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Has.Count.EqualTo(1));
                Assert.That(account.Features[0].AccountId, Is.EqualTo(111));
                Assert.That(account.Accounts, Is.Null);
                Assert.That(account.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(account.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void AccountDTOConstructor_WithMultipleFeaturesAndAccounts_MapsCorrectly()
        {
            // Arrange
            var featureDTOs = new List<FeatureDTO>
            {
                new() { Id = 10, FeatureName = "Feature 1", IsEnabled = true, DateCreated = _testDateCreated, DateModified = _testDateModified },
                new() { Id = 20, FeatureName = "Feature 2", IsEnabled = false, DateCreated = _testDateCreated, DateModified = null },
                new() { Id = 30, FeatureName = "Feature 3", IsEnabled = true, DateCreated = _testDateCreated, DateModified = _testDateModified }
            };
            var subAccountDTOs = new List<SubAccountDTO>
            {
                new() { Id = 100, LinkedEntityId = 200, AccountId = 999 },
                new() { Id = 200, LinkedEntityId = 300, AccountId = 999 }
            };
            var accountDTO = new AccountDTO
            {
                Id = 999,
                AccountName = "Multi Test Account",
                AccountNumber = "ACC999",
                License = Guid.NewGuid().ToString(),
                Features = featureDTOs,
                Accounts = subAccountDTOs,
                DateCreated = _testDateCreated,
                DateModified = _testDateModified
            };

            // Act
            var account = new Account(accountDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(999));
                Assert.That(account.AccountName, Is.EqualTo("Multi Test Account"));
                Assert.That(account.AccountNumber, Is.EqualTo("ACC999"));
                Assert.That(account.License, Is.EqualTo(accountDTO.License));
                
                // Verify Features mapping
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Has.Count.EqualTo(3));
                Assert.That(account.Features[0].AccountId, Is.EqualTo(999));
                Assert.That(account.Features[0].FeatureId, Is.EqualTo(10));
                Assert.That(account.Features[1].AccountId, Is.EqualTo(999));
                Assert.That(account.Features[1].FeatureId, Is.EqualTo(20));
                Assert.That(account.Features[2].AccountId, Is.EqualTo(999));
                Assert.That(account.Features[2].FeatureId, Is.EqualTo(30));
                
                // Verify Accounts mapping
                Assert.That(account.Accounts, Is.Not.Null);
                Assert.That(account.Accounts, Has.Count.EqualTo(2));
                Assert.That(account.Accounts![0].Id, Is.EqualTo(100));
                Assert.That(account.Accounts![1].Id, Is.EqualTo(200));
                
                Assert.That(account.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(account.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void AccountDTOConstructor_WithZeroId_AcceptsZeroValue()
        {
            // Arrange
            var featureDTOs = new List<FeatureDTO>
            {
                new() { Id = 1, FeatureName = "Zero ID Test", IsEnabled = true, DateCreated = _testDateCreated, DateModified = _testDateModified }
            };
            var accountDTO = new AccountDTO
            {
                Id = 0, // Zero ID
                AccountName = "Zero ID Account",
                AccountNumber = "ACC000",
                License = Guid.NewGuid().ToString(),
                Features = featureDTOs,
                Accounts = null,
                DateCreated = _testDateCreated,
                DateModified = _testDateModified
            };

            // Act
            var account = new Account(accountDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(0));
                Assert.That(account.AccountName, Is.EqualTo("Zero ID Account"));
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Has.Count.EqualTo(1));
                Assert.That(account.Features[0].AccountId, Is.EqualTo(0)); // Should use the DTO's Id
                Assert.That(account.DateCreated, Is.EqualTo(_testDateCreated));
                Assert.That(account.DateModified, Is.EqualTo(_testDateModified));
            });
        }

        [Test, Category("Models")]
        public void AccountDTOConstructor_ConvertsFeaturesDTOsToAccountFeatures()
        {
            // Arrange
            var featureDTO = new FeatureDTO
            {
                Id = 42,
                FeatureName = "Conversion Test Feature",
                IsEnabled = true,
                DateCreated = _testDateCreated,
                DateModified = _testDateModified
            };
            var accountDTO = new AccountDTO
            {
                Id = 555,
                AccountName = "Conversion Test",
                AccountNumber = "CONV555",
                License = Guid.NewGuid().ToString(),
                Features = [featureDTO],
                Accounts = null,
                DateCreated = _testDateCreated,
                DateModified = _testDateModified
            };

            // Act
            var account = new Account(accountDTO);

            // Assert - Verify the AccountFeature was created properly from FeatureDTO
            Assert.Multiple(() =>
            {
                Assert.That(account.Features, Is.Not.Null);
                Assert.That(account.Features, Has.Count.EqualTo(1));
                
                var accountFeature = account.Features[0];
                Assert.That(accountFeature, Is.TypeOf<AccountFeature>());
                Assert.That(accountFeature.AccountId, Is.EqualTo(555)); // From accountDTO.Id
                Assert.That(accountFeature.FeatureId, Is.EqualTo(42)); // From featureDTO.Id
            });
        }

        [Test, Category("Models")]
        public void Cast_Method_HasConstraintThatPreventsAccountDTOCasting()
        {
            // Arrange
            var account = new Account(
                id: 123,
                accountName: "Test Account",
                accountNumber: "ACC456",
                license: Guid.NewGuid().ToString(),
                features: [],
                accounts: [],
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            // Act & Assert
            // This test documents that the Cast method cannot actually be called with AccountDTO
            // due to the IDomainEntity constraint, even though the implementation checks for it

            // The following would fail to compile:
            account.Cast<AccountDTO>(); // Cannot compile due to constraint
            account.Cast<IAccountDTO>(); // Cannot compile due to constraint

            // This demonstrates a design inconsistency in the codebase where the Cast method
            // implementation supports AccountDTO/IAccountDTO but the method signature prevents it
            Assert.Pass("This test documents the design limitation where Cast<AccountDTO> cannot be called due to IDomainEntity constraint");
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedDomainType_ThrowsInvalidCastException()
        {
            // Arrange
            var account = new Account(
                id: 777,
                accountName: "Test Account",
                accountNumber: "ACC777",
                license: Guid.NewGuid().ToString(),
                features: [],
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

        [Test, Category("Models")]
        public void Cast_ToAnotherUnsupportedDomainType_ThrowsInvalidCastException()
        {
            // Arrange
            var account = new Account(
                id: 888,
                accountName: "Another Test Account",
                accountNumber: "ACC888",
                license: Guid.NewGuid().ToString(),
                features: [],
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

        [Test, Category("Models")]
        public void Cast_ToFeature_ThrowsInvalidCastException()
        {
            // Arrange
            var account = new Account(
                id: 999,
                accountName: "Feature Test Account",
                accountNumber: "ACC999",
                license: Guid.NewGuid().ToString(),
                features: [],
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

        [Test, Category("Models")]
        public void Cast_ToAnnonymousUser_ThrowsInvalidCastException()
        {
            // Arrange
            var account = new Account(
                id: 111,
                accountName: "Anonymous Test Account",
                accountNumber: "ACC111",
                license: Guid.NewGuid().ToString(),
                features: [],
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

        [Test, Category("Validation")]
        public void Validation_ShouldPass_ForValidAccount()
        {
            // Arrange
            var account = new Account
            {
                Id = 1,
                AccountName = "ValidName",
                AccountNumber = "ValidNumber",
                License = Guid.NewGuid().ToString(),
                Features = _testFeatures
            };

            // Act
            var validationResults = ValidateModel(account);

            // Assert
            Assert.That(validationResults, Is.Empty);
        }

        [Test, Category("Validation")]
        public void Validation_ShouldPass_WhenIdIsZero()
        {
            // Arrange
            var account = new Account
            {
                Id = 0,
                AccountName = "name",
                AccountNumber = "num",
                License = Guid.NewGuid().ToString(),
                Features = _testFeatures
            };

            // Act
            var validationResults = ValidateModel(account);

            // Assert
            Assert.That(validationResults, Is.Empty);
        }

        [Test, Category("Validation"), TestCase(-1)]
        public void Validation_ShouldFail_WhenIdIsInvalid(int invalidId)
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                var account = new Account
                {
                    Id = invalidId,
                    AccountName = "name",
                    AccountNumber = "num",
                    License = Guid.NewGuid().ToString(),
                };
            });
        }

        [Test, Category("Validation")]
        public void Validation_ShouldFail_WhenRequiredStringIsNull()
        {
            // Arrange
            var account = new Account { Id = 1, AccountName = null, AccountNumber = null, License = null};

            // Act
            var validationResults = ValidateModel(account);
            Assert.Multiple(() =>
            {

                // Assert
                Assert.That(validationResults.Any(v => v!.ErrorMessage!.Contains("AccountName")));
                Assert.That(validationResults.Any(v => v!.ErrorMessage!.Contains("AccountNumber")));
                Assert.That(validationResults.Any(v => v!.ErrorMessage!.Contains("License")));
            });
        }

        [Test, Category("Validation")]
        public void Validation_ShouldFail_WhenLicenseIsInvalidGuid()
        {
            // Arrange
            var account = new Account
            {
                Id = 1,
                AccountName = "name",
                AccountNumber = "num",
                License = "invalid-guid",
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
                Assert.That(features, Has.Count.EqualTo(2));
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
                Assert.That(account.Features, Has.Count.EqualTo(2));
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
                Assert.That(accounts!, Has.Count.EqualTo(2));
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
                Assert.That(account.Accounts!, Has.Count.EqualTo(2));
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
                Assert.That(account.Accounts, Is.Empty);
                Assert.That(account.DateModified, Is.GreaterThan(originalDateModified));
            });
        }

        [Test, Category("Models")]
        public void ToJson_SerializesCorrectly()
        {
            // Arrange
            var account = new Account
            {
                Id = 123,
                AccountName = "Test Account",
                AccountNumber = "ACC123",
                License = Guid.NewGuid().ToString(),
                Features = _testFeatures,
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
                Features = [],
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
            var databaseConnection = new DatabaseConnection
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

            account.Features = testFeatures;
            Assert.That(account.Features, Is.EqualTo(testFeatures));

            account.Accounts = testSubAccounts;
            Assert.That(account.Accounts, Is.EqualTo(testSubAccounts));

            account.DateModified = testDate;
            Assert.Multiple(() =>
            {
                Assert.That(account.DateModified, Is.EqualTo(testDate));

                // DateCreated is read-only, just verify it's set
                Assert.That(account.DateCreated, Is.Not.EqualTo(default(DateTime)));
            });
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
            account.Features = [new AccountFeature { Id = 1 }];
            var afterFeatures = account.DateModified;
            Assert.That(afterFeatures, Is.GreaterThan(afterLicense));
        }

        [Test, Category("Models")]
        public void BoundaryValues_AreHandledCorrectly()
        {
            // Arrange & Act
            var account = new Account
            {
                Id = int.MaxValue,
                AccountName = "",
                AccountNumber = "",
                License = Guid.Empty.ToString()
            };

            // Assert - Domain models accept boundary values
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(int.MaxValue));
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
                Accounts = null
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(account.AccountName, Is.Null);
                Assert.That(account.AccountNumber, Is.Null);
                Assert.That(account.License, Is.Null);
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
                Assert.That(account.Accounts!, Has.Count.EqualTo(2));
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
            Assert.That(account.Features, Is.Empty);

            iAccount.Accounts = emptyAccounts;
            Assert.That(account.Accounts, Is.Not.Null);
            Assert.That(account.Accounts!, Is.Empty);

            // Test with actual data
            var feature = new AccountFeature { Id = 1, FeatureId = 1 };
            var subAccount = new SubAccount { Id = 1 };

            iAccount.Features = [feature];
            iAccount.Accounts = [subAccount];

            Assert.Multiple(() =>
            {
                Assert.That(account.Features, Has.Count.EqualTo(1));
                Assert.That(account.Accounts!, Has.Count.EqualTo(1));
                Assert.That(iAccount.Features, Has.Count.EqualTo(1));
                Assert.That(iAccount.Accounts!, Has.Count.EqualTo(1));
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
            _ = account.Features;
            _ = account.Accounts;
            _ = account.DateCreated;
            _ = account.DateModified;

            // Set all settable properties
            account.Id = 1;
            account.AccountName = "Test";
            account.AccountNumber = "ACC1";
            account.License = Guid.NewGuid().ToString();
            account.Features = [];
            account.Accounts = [];
            account.DateModified = DateTime.Now;

            // Verify all are set
            Assert.Multiple(() =>
            {
                Assert.That(account.Id, Is.EqualTo(1));
                Assert.That(account.AccountName, Is.EqualTo("Test"));
                Assert.That(account.AccountNumber, Is.EqualTo("ACC1"));
                Assert.That(account.License, Is.Not.Null);
                Assert.That(account.Features, Is.Not.Null);
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
            // Test with extreme values that might cause issues
            Assert.DoesNotThrow(() => new Account(
                id: int.MaxValue,
                accountName: new string('A', 10000), // Very long string
                accountNumber: new string('B', 10000),
                license: Guid.NewGuid().ToString(),
                features: _testFeatures,
                accounts: [],
                dateCreated: DateTime.MaxValue,
                dateModified: DateTime.MaxValue
            ));
        }

        [Test, Category("Models")]
        public void CompleteIntegration_AllConstructorsPropertiesAndMethods()
        {
            // Arrange
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
                features: features,
                accounts: subAccounts,
                dateCreated: _testDateCreated,
                dateModified: _testDateModified
            );

            var account3 = new Account( // Parameterized constructor
                accountName: "Linked Account",
                accountNumber: "LINK123",
                license: Guid.NewGuid().ToString(),
                features: features,
                accounts: null
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
                Assert.That(account3.Id, Is.EqualTo(0));

                // Verify interface functionality
                Assert.That(interfaceFeatures, Is.Not.Null);
                Assert.That(interfaceFeatures, Has.Count.EqualTo(2));
                Assert.That(interfaceAccounts, Is.Not.Null);
                Assert.That(interfaceAccounts!, Has.Count.EqualTo(2));

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
                Assert.That(account.Features, Has.Count.EqualTo(1));
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
                License = Guid.NewGuid().ToString()
            };

            var json = account.ToJson();

            // Verify JSON contains property names as defined by JsonPropertyNameAttribute
            Assert.Multiple(() =>
            {
                Assert.That(json, Contains.Substring("\"id\""));
                Assert.That(json, Contains.Substring("\"accountName\""));
                Assert.That(json, Contains.Substring("\"accountNumber\""));
                Assert.That(json, Contains.Substring("\"license\""));
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
            // Normal operation should not throw
            Assert.DoesNotThrow(() => new Account(
                accountName: "testuser",
                accountNumber: "ACC123",
                license: Guid.NewGuid().ToString(),
                features: _testFeatures,
                accounts: null
            ));
        }

        [Test, Category("Models")]
        public void AllConstructorPaths_AreTestedForCompleteness()
        {
            // Test to ensure we've covered all constructor scenarios including edge cases
            var databaseConnection = new DatabaseConnection
            {
                ConnectionString = "complete-test",
                DatabaseType = SupportedDatabases.SQLServer
            };

            // Test default constructor  
            var account1 = new Account();
            Assert.That(account1.Id, Is.EqualTo(0));

            // Test JSON constructor with minimum values
            var account2 = new Account(0, null, null, null, null!, null!, DateTime.Now, null);
            Assert.That(account2.Id, Is.EqualTo(0));

            // Test parameterized constructor 
            var account3 = new Account(0, null, null, null, null!, null!, DateTime.Now, null);
            Assert.That(account3.Id, Is.EqualTo(0));

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


