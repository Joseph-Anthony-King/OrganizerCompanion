using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class AccountFeatureShould
    {
        private AccountFeature _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new AccountFeature();
        }

        [Test, Category("Models")]
        public void DefaultConstructor_SetsDefaultValues()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var accountFeature = new AccountFeature();
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.Id, Is.EqualTo(0));
                Assert.That(accountFeature.AccountId, Is.EqualTo(0));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(0));
                Assert.That(accountFeature.CreatedDate, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(accountFeature.CreatedDate, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(accountFeature.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var testCreatedDate = DateTime.UtcNow;
            var account = new Account { Id = 789, AccountName = "Test Account" };
            var feature = new Feature(456, "Test Feature", true, DateTime.Now, null);

            // Act
            var accountFeature = new AccountFeature(
                id: 1,
                account: account,
                feature: feature,
                createdDate: testCreatedDate,
                modifiedDate: null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.Id, Is.EqualTo(1));
                Assert.That(accountFeature.AccountId, Is.EqualTo(789));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(456));
                Assert.That(accountFeature.CreatedDate, Is.EqualTo(testCreatedDate));
                Assert.That(accountFeature.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void ConstructorWithAccountAndFeature_SetsPropertiesCorrectly()
        {
            // Arrange
            var account = new Account { Id = 789, AccountName = "Test Account" };
            var feature = new Feature(456, "Test Feature", true, DateTime.Now, null);
            var beforeCreation = DateTime.UtcNow;

            // Act
            var accountFeature = new AccountFeature(account, feature);
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.Id, Is.EqualTo(0));
                Assert.That(accountFeature.AccountId, Is.EqualTo(789));
                Assert.That(accountFeature.Account, Is.EqualTo(account));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(456));
                Assert.That(accountFeature.Feature, Is.EqualTo(feature));
                Assert.That(accountFeature.CreatedDate, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(accountFeature.CreatedDate, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(accountFeature.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void ConstructorWithAccountAndFeature_WithNullAccount_ThrowsException()
        {
            // Arrange
            var feature = new Feature(456, "Test Feature", true, DateTime.Now, DateTime.Now);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AccountFeature(null!, feature));
        }

        [Test, Category("Models")]
        public void ConstructorWithAccountAndFeature_WithNullFeature_ThrowsException()
        {
            // Arrange
            var account = new Account { Id = 789, AccountName = "Test Account" };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AccountFeature(account, null!));
        }

        [Test, Category("Models")]
        public void ConstructorWithAccountAndFeature_WithBothNull_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AccountFeature((IAccount)null!, (IFeature)null!));
        }

        [Test, Category("Models")]
        public void NegativeIds_AreNotAccepted()
        {
            // Arrange, Act & Assert - Id validation throws exception
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = -100);
        }

        [Test, Category("Models")]
        public void Feature_WithNegativeId_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var featureWithNegativeId = new Feature(-1, "Invalid Feature", true, DateTime.Now, null);

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Feature = featureWithNegativeId);
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.ParamName, Is.EqualTo("Feature"));
                Assert.That(ex.Message, Does.Contain("FeatureId must be a non-negative number"));
            });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Feature_Set_WithNegativeId_ThrowsArgumentOutOfRangeException()
        {
            // Arrange
            var featureWithNegativeId = new Feature(-10, "Negative ID Feature", false, DateTime.Now, null);
            IAccountFeature iAccountFeature = _sut;

            // Act & Assert
            var ex = Assert.Throws<ArgumentOutOfRangeException>(() => iAccountFeature.Feature = featureWithNegativeId);
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex!.ParamName, Is.EqualTo("Feature"));
                Assert.That(ex.Message, Does.Contain("FeatureId must be a non-negative number"));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNegativeFeatureId_DoesNotThrow()
        {
            // Arrange
            var account = new Account { Id = 789, AccountName = "Test Account" };
            var featureWithNegativeId = new Feature(-1, "Negative ID Feature", true, DateTime.Now, null);

            // Act & Assert - JsonConstructor bypasses validation by setting fields directly
            Assert.DoesNotThrow(() => new AccountFeature(
                id: 1,
                account: account,
                feature: featureWithNegativeId,
                createdDate: DateTime.UtcNow,
                modifiedDate: null)
            );
        }

        [Test, Category("Models")]
        public void FeatureDTOConstructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var featureDTO = new FeatureDTO
            {
                Id = 123,
                FeatureName = "Test Feature",
                IsEnabled = true,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };
            var accountDTO = new AccountDTO
            {
                Id = 456,
                AccountName = "Test Account",
                AccountNumber = "ACC123",
                License = "LIC456",
                Features = [featureDTO],
                Accounts = null,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            };

            // Act
            var accountFeature = new AccountFeature(accountDTO, featureDTO);
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.Id, Is.EqualTo(0)); // Default value
                Assert.That(accountFeature.AccountId, Is.EqualTo(456));
                Assert.That(accountFeature.Account, Is.Not.Null); // Now set by constructor
                Assert.That(accountFeature.Account!.Id, Is.EqualTo(456));
                Assert.That(accountFeature.Account!.AccountName, Is.EqualTo("Test Account"));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(123));
                Assert.That(accountFeature.Feature, Is.Not.Null); // Now set by constructor
                Assert.That(accountFeature.Feature!.Id, Is.EqualTo(123));
                Assert.That(accountFeature.Feature.FeatureName, Is.EqualTo("Test Feature"));
                Assert.That(accountFeature.CreatedDate, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(accountFeature.ModifiedDate, Is.Null); // Default value
            });
        }

        [Test, Category("Models")]
        public void FeatureDTOConstructor_WithNullFeatureDTO_ThrowsNullReferenceException()
        {
            // Arrange
            var accountDTO = new AccountDTO
            {
                Id = 456,
                AccountName = "Test Account",
                AccountNumber = "ACC123",
                License = "LIC456",
                Features = [],
                Accounts = null,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            };
            IFeatureDTO? featureDTO = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AccountFeature(accountDTO, featureDTO!));
        }

        [Test, Category("Models")]
        public void FeatureDTOConstructor_WithNullAccountDTO_ThrowsArgumentNullException()
        {
            // Arrange
            AccountDTO? accountDTO = null;

            var featureDTO = new FeatureDTO
            {
                Id = 789,
                FeatureName = "Zero Account Feature",
                IsEnabled = false,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            };

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => new AccountFeature(accountDTO!, featureDTO));
        }

        [Test, Category("Models")]
        public void Id_CanBeSetAndRetrieved()
        {
            // Arrange
            const int expectedId = 789;

            // Act
            _sut.Id = expectedId;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(expectedId));
        }

        [Test, Category("Models")]
        public void AccountId_ReturnsZero_WhenAccountIsNull()
        {
            // Arrange & Act
            _sut.Account = null;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void AccountId_ReturnsAccountId_WhenAccountIsSet()
        {
            // Arrange
            var account = new Account { Id = 101, AccountName = "Test Account" };

            // Act
            _sut.Account = account;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(101));
        }

        [Test, Category("Models")]
        public void FeatureId_ReturnsZero_WhenFeatureIsNull()
        {
            // Arrange & Act
            _sut.Feature = null;

            // Assert
            Assert.That(_sut.FeatureId, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void FeatureId_ReturnsFeatureId_WhenFeatureIsSet()
        {
            // Arrange
            var feature = new Feature(202, "Test Feature", true, DateTime.Now, null);

            // Act
            _sut.Feature = feature;

            // Assert
            Assert.That(_sut.FeatureId, Is.EqualTo(202));
        }

        [Test, Category("Models")]
        public void ToJson_ReturnsValidJsonString()
        {
            // Arrange
            _sut.Id = 1;
            var account = new Account { Id = 123, AccountName = "Test" };
            var feature = new Feature(456, "Test Feature", true, DateTime.Now, null);
            _sut.Account = account;
            _sut.Feature = feature;
            _sut.ModifiedDate = new DateTime(2023, 1, 1, 12, 0, 0);

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
            });

            // Verify JSON contains expected properties
            var jsonDocument = JsonDocument.Parse(json);
            var root = jsonDocument.RootElement;

            Assert.Multiple(() =>
            {
                Assert.That(root.TryGetProperty("id", out var idProperty), Is.True);
                Assert.That(idProperty.GetInt32(), Is.EqualTo(1));

                Assert.That(root.TryGetProperty("accountId", out var accountIdProperty), Is.True);
                Assert.That(accountIdProperty.GetInt32(), Is.EqualTo(123));

                Assert.That(root.TryGetProperty("createdDate", out var createdDateProperty), Is.True);
                Assert.That(createdDateProperty.ValueKind, Is.EqualTo(JsonValueKind.String));

                Assert.That(root.TryGetProperty("modifiedDate", out var modifiedDateProperty), Is.True);
                Assert.That(modifiedDateProperty.ValueKind, Is.EqualTo(JsonValueKind.String));
            });
        }

        [Test, Category("Models")]
        public void Properties_WithMinValues_ThrowsException()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _sut.Id = int.MinValue;
            });
        }

        [Test, Category("Models")]
        public void Account_WithNullValue_SetsAccountIdToZero()
        {
            // Arrange
            var account = new Account { Id = 999, AccountName = "Test" };
            _sut.Account = account;
            Assert.That(_sut.AccountId, Is.EqualTo(999)); // Verify it's set

            // Act
            _sut.Account = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Account, Is.Null);
                Assert.That(_sut.AccountId, Is.EqualTo(0));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void Feature_CanBeSetAndRetrieved()
        {
            // Arrange
            var feature = new Feature(101, "Another Feature", false, DateTime.Now, DateTime.Now);

            // Act
            _sut.Feature = feature;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Feature, Is.EqualTo(feature));
                Assert.That(_sut.FeatureId, Is.EqualTo(101));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void Feature_WithNullValue_SetsFeatureIdToZero()
        {
            // Arrange
            var feature = new Feature(888, "Test", true, DateTime.Now, null);
            _sut.Feature = feature;
            Assert.That(_sut.FeatureId, Is.EqualTo(888)); // Verify it's set

            // Act
            _sut.Feature = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Feature, Is.Null);
                Assert.That(_sut.FeatureId, Is.EqualTo(0));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void Account_CanBeSetAndRetrieved()
        {
            // Arrange
            var account = new Account { Id = 123, AccountName = "Test Account" };

            // Act
            _sut.Account = account;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Account, Is.EqualTo(account));
                Assert.That(_sut.AccountId, Is.EqualTo(123));
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void Id_WithZeroValue_IsValid()
        {
            // Arrange & Act
            _sut.Id = 0;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }
    }
}
