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
                Assert.That(accountFeature.Id, Is.EqualTo(0)); // Default value
                Assert.That(accountFeature.AccountId, Is.EqualTo(789));
                Assert.That(accountFeature.Account, Is.EqualTo(account));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(456));
                Assert.That(accountFeature.Feature, Is.EqualTo(feature));
                Assert.That(accountFeature.CreatedDate, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(accountFeature.CreatedDate, Is.LessThanOrEqualTo(afterCreation));
                //Assert.That(accountFeature.ModifiedDate, Is.EqualTo(default(DateTime)));
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
        public void FeatureDTOAndAccountDTOConstructor_WithMaxValues_HandlesLargeNumbers()
        {
            // Arrange
            var accountDTO = new AccountDTO
            {
                Id = int.MaxValue,
                AccountName = "Max Value Account",
                AccountNumber = "MAX123",
                License = "MAX456",
                Features = [],
                Accounts = null,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            };

            var featureDTO = new FeatureDTO
            {
                Id = int.MaxValue,
                FeatureName = "Max Value Feature",
                IsEnabled = true,
                CreatedDate = DateTime.Now,
                ModifiedDate = DateTime.Now
            };

            // Act
            var accountFeature = new AccountFeature(accountDTO, featureDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.AccountId, Is.EqualTo(int.MaxValue));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(int.MaxValue));
            });
        }

        [Test, Category("Models")]
        public void AccountDTOConstructor_WithIFeatureDTOInterface_WorksCorrectly()
        {
            // Arrange
            IAccountDTO accountDTO = new AccountDTO
            {
                Id = 777,
                AccountName = "Interface Account",
                AccountNumber = "INT123",
                License = "INT456",
                Features = [],
                Accounts = null,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            };

            var featureDTO = new FeatureDTO
            {
                Id = 666,
                FeatureName = "Interface Feature",
                IsEnabled = false,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            };

            // Act
            var accountFeature = new AccountFeature(accountDTO, featureDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.AccountId, Is.EqualTo(777));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(666));
                Assert.That(accountFeature.Id, Is.EqualTo(0));
                Assert.That(accountFeature.Account, Is.Not.Null); // Now set by constructor
                Assert.That(accountFeature.Account!.Id, Is.EqualTo(777));
                Assert.That(accountFeature.Account!.AccountName, Is.EqualTo("Interface Account"));
                Assert.That(accountFeature.Feature, Is.Not.Null); // Now set by constructor
                Assert.That(accountFeature.Feature!.Id, Is.EqualTo(666));
                Assert.That(accountFeature.Feature.FeatureName, Is.EqualTo("Interface Feature"));
            });
        }

        [Test, Category("Models")]
        public void FeatureDTOConstructor_WithIFeatureDTOInterface_WorksCorrectly()
        {
            // Arrange
            var accountDTO = new AccountDTO
            {
                Id = 777,
                AccountName = "Interface Account",
                AccountNumber = "INT123",
                License = "INT456",
                Features = [],
                Accounts = null,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            };

            IFeatureDTO featureDTO = new FeatureDTO
            {
                Id = 666,
                FeatureName = "Interface Feature",
                IsEnabled = false,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            };

            // Act
            var accountFeature = new AccountFeature(accountDTO, featureDTO);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.AccountId, Is.EqualTo(777));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(666));
                Assert.That(accountFeature.Id, Is.EqualTo(0));
                Assert.That(accountFeature.Account, Is.Not.Null); // Now set by constructor
                Assert.That(accountFeature.Account!.Id, Is.EqualTo(777));
                Assert.That(accountFeature.Account!.AccountName, Is.EqualTo("Interface Account"));
                Assert.That(accountFeature.Feature, Is.Not.Null); // Now set by constructor
                Assert.That(accountFeature.Feature!.Id, Is.EqualTo(666));
                Assert.That(accountFeature.Feature!.FeatureName, Is.EqualTo("Interface Feature"));
            });
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
        public void ToJson_HandlesNullModifiedDate()
        {
            // Arrange
            _sut.Id = 2;
            var account = new Account { Id = 789, AccountName = "Test" };
            var feature = new Feature(101, "Test Feature", true, DateTime.Now, null);
            _sut.Account = account;
            _sut.Feature = feature;
            _sut.ModifiedDate = null;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
          {
              Assert.That(json, Is.Not.Null);
              Assert.That(json, Is.Not.Empty);
              Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
          });

            var jsonDocument = JsonDocument.Parse(json);
            var root = jsonDocument.RootElement;

            Assert.Multiple(() =>
                  {
                      Assert.That(root.TryGetProperty("id", out var idProperty), Is.True);
                      Assert.That(idProperty.GetInt32(), Is.EqualTo(2));

                      Assert.That(root.TryGetProperty("accountId", out var accountIdProperty), Is.True);
                      Assert.That(accountIdProperty.GetInt32(), Is.EqualTo(789));

                      Assert.That(root.TryGetProperty("modifiedDate", out var modifiedDateProperty), Is.True);
                      Assert.That(modifiedDateProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
                  });
        }

        [Test, Category("Models")]
        public void Properties_MaintainConsistencyAfterMultipleChanges()
        {
            // Arrange
            const int id = 999;
            var account = new Account { Id = 888, AccountName = "Test Account" };
            var feature = new Feature(777, "Test Feature", true, DateTime.Now, null);
            var modifiedDate = new DateTime(2023, 12, 25, 15, 30, 0);

            // Act
            _sut.Id = id;
            _sut.Account = account;
            _sut.Feature = feature;
            _sut.ModifiedDate = modifiedDate;

            // Assert
            Assert.Multiple(() =>
             {
                 Assert.That(_sut.Id, Is.EqualTo(id));
                 Assert.That(_sut.AccountId, Is.EqualTo(888));
                 Assert.That(_sut.FeatureId, Is.EqualTo(777));
                 Assert.That(_sut.ModifiedDate, Is.EqualTo(modifiedDate)); // Updated by setters
                 Assert.That(_sut.CreatedDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
             });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithZeroValues_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            var accountFeature = new AccountFeature(
              id: 0,
                    account: null,
         feature: null,
                  createdDate: DateTime.UtcNow,
                  modifiedDate: null);

            // Assert
            Assert.Multiple(() =>
                     {
                         Assert.That(accountFeature.AccountId, Is.EqualTo(0));
                         Assert.That(accountFeature.FeatureId, Is.EqualTo(0));
                         Assert.That(accountFeature.Id, Is.EqualTo(0));
                     });
        }

        [Test, Category("Models")]
        public void Properties_WithMaxValues_WorkCorrectly()
        {
            // Arrange
            var account = new Account { Id = int.MaxValue, AccountName = "Test" };
            var feature = new Feature(int.MaxValue, "Test", true, DateTime.Now, null);

            // Act
            _sut.Id = int.MaxValue;
            _sut.Account = account;
            _sut.Feature = feature;

            // Assert
            Assert.Multiple(() =>
         {
             Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
             Assert.That(_sut.AccountId, Is.EqualTo(int.MaxValue));
             Assert.That(_sut.FeatureId, Is.EqualTo(int.MaxValue));
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
        public void ToJson_WithSerializerOptions_HandlesCircularReferences()
        {
            // Arrange
            _sut.Id = 100;
            var account = new Account { Id = 200, AccountName = "Test" };
            var feature = new Feature(300, "Test", true, DateTime.Now, null);
            _sut.Account = account;
            _sut.Feature = feature;

            // Act
            var json = _sut.ToJson();

            // Assert - Should not throw due to ReferenceHandler.IgnoreCycles
            Assert.Multiple(() =>
             {
                 Assert.That(json, Is.Not.Null);
                 Assert.That(json, Is.Not.Empty);
                 Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
             });
        }

        [Test]
        [Category("Models")]
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

        [Test]
        [Category("Models")]
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

        [Test]
        [Category("Models")]
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

        [Test]
        [Category("Models")]
        public void Properties_SetModifiedDate_WhenChanged()
        {
            // Arrange
            var initialModifiedDate = _sut.ModifiedDate;
            System.Threading.Thread.Sleep(10); // Ensure time difference

            // Act & Assert - Test each property setter
            _sut.Id = 100;
            Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(initialModifiedDate));

            var afterIdChange = _sut.ModifiedDate;
            System.Threading.Thread.Sleep(10);

            var account = new Account { Id = 200, AccountName = "Test" };
            _sut.Account = account;
            Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(afterIdChange));

            var afterAccountChange = _sut.ModifiedDate;
            System.Threading.Thread.Sleep(10);

            var feature = new Feature(300, "Test", true, DateTime.Now, null);
            _sut.Feature = feature;
            Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(afterAccountChange));
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithModifiedDateValue_SetsCorrectly()
        {
            // Arrange
            var testCreatedDate = new DateTime(2025, 1, 1, 10, 0, 0);
            var testModifiedDate = new DateTime(2025, 10, 20, 15, 30, 45);
            var account = new Account { Id = 100, AccountName = "Test" };
            var feature = new Feature(200, "Test", true, DateTime.Now, null);

            // Act
            var accountFeature = new AccountFeature(
           id: 555,
                       account: account,
                       feature: feature,
          createdDate: testCreatedDate,
          modifiedDate: testModifiedDate);

            // Assert
            Assert.Multiple(() =>
        {
            Assert.That(accountFeature.Id, Is.EqualTo(555));
            Assert.That(accountFeature.AccountId, Is.EqualTo(100));
            Assert.That(accountFeature.FeatureId, Is.EqualTo(200));
            Assert.That(accountFeature.CreatedDate, Is.EqualTo(testCreatedDate));
            Assert.That(accountFeature.ModifiedDate, Is.EqualTo(testModifiedDate));
        });
        }

        [Test, Category("Models")]
        public void ToJson_WithComplexData_SerializesCorrectly()
        {
            // Arrange
            _sut.Id = 12345;
            var account = new Account { Id = 67890, AccountName = "Test" };
            var feature = new Feature(11111, "Test", true, DateTime.Now, null);
            _sut.Account = account;
            _sut.Feature = feature;
            _sut.ModifiedDate = new DateTime(2025, 10, 20, 14, 35, 42, 999);

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
          {
              Assert.That(json, Is.Not.Null);
              Assert.That(json, Is.Not.Empty);

              var jsonDocument = JsonDocument.Parse(json);
              var root = jsonDocument.RootElement;

              Assert.That(root.TryGetProperty("id", out var idProperty), Is.True);
              Assert.That(idProperty.GetInt32(), Is.EqualTo(12345));

              Assert.That(root.TryGetProperty("accountId", out var accountIdProperty), Is.True);
              Assert.That(accountIdProperty.GetInt32(), Is.EqualTo(67890));

              Assert.That(root.TryGetProperty("featureId", out var featureIdProperty), Is.True);
              Assert.That(featureIdProperty.GetInt32(), Is.EqualTo(11111));

              Assert.That(root.TryGetProperty("createdDate", out var createdDateProperty), Is.True);
              Assert.That(createdDateProperty.ValueKind, Is.EqualTo(JsonValueKind.String));

              Assert.That(root.TryGetProperty("modifiedDate", out var modifiedDateProperty), Is.True);
              Assert.That(modifiedDateProperty.ValueKind, Is.EqualTo(JsonValueKind.String));
          });
        }

        [Test, Category("Models")]
        public void JsonSerializerOptions_HasIgnoreCyclesReferenceHandler()
        {
            // This test verifies that the serializer options are configured correctly
            // by testing serialization doesn't fail with potential circular references

            // Arrange
            _sut.Id = 999;
            var account = new Account { Id = 888, AccountName = "Test" };
            var feature = new Feature(777, "Test", true, DateTime.Now, null);
            _sut.Account = account;
            _sut.Feature = feature;

            // Act - Should not throw due to ReferenceHandler.IgnoreCycles
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
             {
                 Assert.That(json, Is.Not.Null);
                 Assert.That(json, Is.Not.Empty);
                 Assert.That(() => JsonDocument.Parse(json), Throws.Nothing);
             });
        }

        [Test, Category("Models")]
        public void NegativeIds_AreNotAccepted()
        {
            // Arrange, Act & Assert - Id validation throws exception
            Assert.Throws<ArgumentOutOfRangeException>(() => _sut.Id = -100);
        }

        [Test, Category("Models")]
        public void CompleteIntegration_AllPropertiesAndMethods()
        {
            // Arrange
            var feature = new Feature(1, "Integration Test Feature", true, DateTime.Now, DateTime.Now);
            var account = new Account { Id = 2, AccountName = "Integration Test Account" };
            var createdDate = new DateTime(2025, 1, 1, 12, 0, 0);
            var modifiedDate = new DateTime(2025, 10, 20, 18, 30, 45);

            // Act - Create using JsonConstructor
            var accountFeature = new AccountFeature(
            id: 12345,
                       account: account,
       feature: feature,
                  createdDate: createdDate,
                       modifiedDate: modifiedDate);

            // Assert - Verify all properties
            Assert.Multiple(() =>
                       {
                           Assert.That(accountFeature.Id, Is.EqualTo(12345));
                           Assert.That(accountFeature.AccountId, Is.EqualTo(2));
                           Assert.That(accountFeature.Account, Is.EqualTo(account));
                           Assert.That(accountFeature.FeatureId, Is.EqualTo(1));
                           Assert.That(accountFeature.Feature, Is.EqualTo(feature));
                           Assert.That(accountFeature.CreatedDate, Is.EqualTo(createdDate));
                           Assert.That(accountFeature.ModifiedDate, Is.EqualTo(modifiedDate));
                       });

            // Test Cast method
            var dto = accountFeature.Cast<FeatureDTO>();
            Assert.Multiple(() =>
     {
         Assert.That(dto.Id, Is.EqualTo(12345));
         Assert.That(dto.FeatureName, Is.EqualTo("Integration Test Feature"));
         Assert.That(dto.IsEnabled, Is.True);
     });

            // Test ToJson method
            var json = accountFeature.ToJson();
            Assert.Multiple(() =>
                  {
                      Assert.That(json, Is.Not.Null);
                      Assert.That(json, Contains.Substring("\"id\":12345"));
                      Assert.That(json, Contains.Substring("\"accountId\":2"));
                      Assert.That(json, Contains.Substring("\"featureId\":1"));
                  });
        }

        [Test, Category("Models")]
        public void ToString_ReturnsFormattedString()
        {
            // Arrange
            _sut.Id = 123;
            var account = new Account { Id = 456, AccountName = "Test" };
            var feature = new Feature(789, "Test", true, DateTime.Now, null);
            _sut.Account = account;
            _sut.Feature = feature;

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
                 {
                     Assert.That(result, Is.Not.Null);
                     Assert.That(result, Does.Contain("123"));
                     Assert.That(result, Does.Contain("456"));
                     Assert.That(result, Does.Contain("789"));
                     Assert.That(result, Does.Contain("Id:"));
                     Assert.That(result, Does.Contain("AccountId:"));
                     Assert.That(result, Does.Contain("FeatureId:"));
                 });
        }

        [Test, Category("Models")]
        public void ToString_WithZeroValues_ReturnsCorrectFormat()
        {
            // Arrange - Don't set Account or Feature, so IDs default to 0
            _sut.Id = 0;

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
               {
                   Assert.That(result, Is.Not.Null);
                   Assert.That(result, Does.Contain("0"));
                   Assert.That(result, Does.Contain("Id:0"));
                   Assert.That(result, Does.Contain("AccountId:0"));
                   Assert.That(result, Does.Contain("FeatureId:0"));
               });
        }

        [Test, Category("Models")]
        public void ToString_WithMaxValues_ReturnsCorrectFormat()
        {
            // Arrange
            _sut.Id = int.MaxValue;
            var account = new Account { Id = int.MaxValue, AccountName = "Test" };
            var feature = new Feature(int.MaxValue, "Test", true, DateTime.Now, null);
            _sut.Account = account;
            _sut.Feature = feature;

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
        {
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Does.Contain(int.MaxValue.ToString()));
            Assert.That(result, Does.Contain($"Id:{int.MaxValue}"));
            Assert.That(result, Does.Contain($"AccountId:{int.MaxValue}"));
            Assert.That(result, Does.Contain($"FeatureId:{int.MaxValue}"));
        });
        }

        #region Explicit Interface Implementation Tests

        [Test, Category("Models")]
        public void ExplicitInterface_Account_Get_ReturnsAccount()
        {
            // Arrange
            var account = new Account { Id = 123, AccountName = "Test Account" };
            _sut.Account = account;
            IAccountFeature iAccountFeature = _sut;

            // Act
            var result = iAccountFeature.Account;

            // Assert
            Assert.Multiple(() =>
                {
                    Assert.That(result, Is.Not.Null);
                    Assert.That(result, Is.SameAs(account));
                    Assert.That(result!.Id, Is.EqualTo(123));
                    Assert.That(((Account)result).AccountName, Is.EqualTo("Test Account"));
                });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Account_Set_SetsAccountAndUpdatesModifiedDate()
        {
            // Arrange
            var account = new Account { Id = 456, AccountName = "Interface Test" };
            IAccountFeature iAccountFeature = _sut;
            var beforeModifiedDate = _sut.ModifiedDate;

            // Act
            System.Threading.Thread.Sleep(10); // Ensure time difference
            iAccountFeature.Account = account;

            // Assert
            Assert.Multiple(() =>
              {
                  Assert.That(_sut.Account, Is.Not.Null);
                  Assert.That(_sut.Account, Is.SameAs(account));
                  Assert.That(_sut.AccountId, Is.EqualTo(456));
                  Assert.That(_sut.ModifiedDate, Is.Not.Null);
                  Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(beforeModifiedDate));
              });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Account_Set_WithNull_SetsToNull()
        {
            // Arrange
            var account = new Account { Id = 789, AccountName = "Test" };
            _sut.Account = account;
            IAccountFeature iAccountFeature = _sut;
            var beforeModifiedDate = _sut.ModifiedDate;

            // Act
            System.Threading.Thread.Sleep(10); // Ensure time difference
            iAccountFeature.Account = null;

            // Assert
            Assert.Multiple(() =>
         {
             Assert.That(_sut.Account, Is.Null);
             Assert.That(_sut.AccountId, Is.EqualTo(0));
             Assert.That(_sut.ModifiedDate, Is.Not.Null);
             Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(beforeModifiedDate));
         });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Feature_Get_ReturnsFeature()
        {
            // Arrange
            var feature = new Feature(111, "Test Feature", true, DateTime.Now, null);
            _sut.Feature = feature;
            IAccountFeature iAccountFeature = _sut;

            // Act
            var result = iAccountFeature.Feature;

            // Assert
            Assert.Multiple(() =>
              {
                  Assert.That(result, Is.Not.Null);
                  Assert.That(result, Is.SameAs(feature));
                  Assert.That(result!.Id, Is.EqualTo(111));
                  Assert.That(((Feature)result).FeatureName, Is.EqualTo("Test Feature"));
              });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Feature_Set_SetsFeatureAndUpdatesModifiedDate()
        {
            // Arrange
            var feature = new Feature(222, "Interface Feature", false, DateTime.Now, null);
            IAccountFeature iAccountFeature = _sut;
            var beforeModifiedDate = _sut.ModifiedDate;

            // Act
            System.Threading.Thread.Sleep(10); // Ensure time difference
            iAccountFeature.Feature = feature;

            // Assert
            Assert.Multiple(() =>
               {
                   Assert.That(_sut.Feature, Is.Not.Null);
                   Assert.That(_sut.Feature, Is.SameAs(feature));
                   Assert.That(_sut.FeatureId, Is.EqualTo(222));
                   Assert.That(_sut.ModifiedDate, Is.Not.Null);
                   Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(beforeModifiedDate));
               });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Feature_Set_WithNull_SetsToNull()
        {
            // Arrange
            var feature = new Feature(333, "Test", true, DateTime.Now, null);
            _sut.Feature = feature;
            IAccountFeature iAccountFeature = _sut;
            var beforeModifiedDate = _sut.ModifiedDate;

            // Act
            System.Threading.Thread.Sleep(10); // Ensure time difference
            iAccountFeature.Feature = null;

            // Assert
            Assert.Multiple(() =>
{
    Assert.That(_sut.Feature, Is.Null);
    Assert.That(_sut.FeatureId, Is.EqualTo(0));
    Assert.That(_sut.ModifiedDate, Is.Not.Null);
    Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(beforeModifiedDate));
});
        }

        [Test, Category("Models")]
        public void ExplicitInterface_ImplementsIAccountFeature()
        {
            // Arrange & Act
            var accountFeature = new AccountFeature();

            // Assert
            Assert.That(accountFeature, Is.InstanceOf<IAccountFeature>());
        }

        #endregion

        #region Cast Method Tests

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_ReturnsCorrectDTO()
        {
            // Arrange
            var account = new Account { Id = 100, AccountName = "Test" };
            var feature = new Feature(200, "Cast Test Feature", true, DateTime.Now, null);
            _sut.Id = 300;
            _sut.Account = account;
            _sut.Feature = feature;

            // Act
            var result = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
    {
        Assert.That(result, Is.Not.Null);
        Assert.That(result, Is.TypeOf<FeatureDTO>());
        Assert.That(result.Id, Is.EqualTo(300));
        Assert.That(result.FeatureName, Is.EqualTo("Cast Test Feature"));
        Assert.That(result.IsEnabled, Is.True);
    });
        }

        [Test, Category("Models")]
        public void Cast_ToIFeatureDTO_ReturnsCorrectDTO()
        {
            // Arrange
            var account = new Account { Id = 400, AccountName = "Test" };
            var feature = new Feature(500, "Interface Cast Feature", false, DateTime.Now, null);
            _sut.Id = 600;
            _sut.Account = account;
            _sut.Feature = feature;

            // Act
            var result = _sut.Cast<IFeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.AssignableFrom<FeatureDTO>());
                Assert.That(result.Id, Is.EqualTo(600));
                Assert.That(result.FeatureName, Is.EqualTo("Interface Cast Feature"));
                Assert.That(result.IsEnabled, Is.False);
            });
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedType_ThrowsInvalidCastException()
        {
            // Arrange
            var account = new Account { Id = 700, AccountName = "Test" };
            var feature = new Feature(800, "Test", true, DateTime.Now, null);
            _sut.Id = 900;
            _sut.Account = account;
            _sut.Feature = feature;

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<AccountDTO>());
            Assert.Multiple(() =>
                       {
                           Assert.That(ex, Is.Not.Null);
                           Assert.That(ex!.Message, Does.Contain("Cannot cast Feature to type"));
                           Assert.That(ex.Message, Does.Contain("AccountDTO"));
                       });
        }

        [Test, Category("Models")]
        public void Cast_ToAnotherUnsupportedType_ThrowsInvalidCastException()
        {
            // Arrange
            var account = new Account { Id = 111, AccountName = "Test" };
            var feature = new Feature(222, "Test", true, DateTime.Now, null);
            _sut.Id = 333;
            _sut.Account = account;
            _sut.Feature = feature;

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<SubAccountDTO>());
            Assert.Multiple(() =>
             {
                 Assert.That(ex, Is.Not.Null);
                 Assert.That(ex!.Message, Does.Contain("Cannot cast Feature to type"));
                 Assert.That(ex.Message, Does.Contain("SubAccountDTO"));
             });
        }

        [Test, Category("Models")]
        public void Cast_WithNullFeature_ThrowsNullReferenceException()
        {
            // Arrange
            var account = new Account { Id = 444, AccountName = "Test" };
            _sut.Id = 555;
            _sut.Account = account;
            _sut.Feature = null; // Null feature will cause NullReferenceException

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => _sut.Cast<FeatureDTO>());
        }

        [Test, Category("Models")]
        public void Cast_ExceptionHandling_RethrowsException()
        {
            // Arrange
            var account = new Account { Id = 666, AccountName = "Test" };
            var feature = new Feature(777, "Test", true, DateTime.Now, null);
            _sut.Id = 888;
            _sut.Account = account;
            _sut.Feature = feature;

            // Act & Assert - The catch block in Cast method re-throws exceptions
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<AccountDTO>());
            Assert.That(ex, Is.Not.Null);
        }

        #endregion

        #region Additional Coverage Tests

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
        public void CreatedDate_IsReadOnly()
        {
            // Arrange & Act
            var property = typeof(AccountFeature).GetProperty(nameof(AccountFeature.CreatedDate));

            // Assert
            Assert.Multiple(() =>
               {
                   Assert.That(property, Is.Not.Null);
                   Assert.That(property!.CanRead, Is.True);
                   Assert.That(property.GetSetMethod(), Is.Null); // No public setter
               });
        }

        [Test, Category("Models")]
        public void ModifiedDate_CanBeSetDirectly()
        {
            // Arrange
            var testDate = new DateTime(2025, 10, 20, 12, 30, 45);

            // Act
            _sut.ModifiedDate = testDate;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(testDate));
        }

        [Test, Category("Models")]
        public void Id_WithZeroValue_IsValid()
        {
            // Arrange & Act
            _sut.Id = 0;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void TypeInformation_ShouldBeCorrect()
        {
            // Arrange & Act
            var accountFeature = new AccountFeature();

            // Assert
            Assert.Multiple(() =>
  {
      Assert.That(accountFeature.GetType(), Is.EqualTo(typeof(AccountFeature)));
      Assert.That(accountFeature.GetType().Name, Is.EqualTo("AccountFeature"));
      Assert.That(accountFeature.GetType().Namespace, Is.EqualTo("OrganizerCompanion.Core.Models.Domain"));
  });
        }

        [Test, Category("Models")]
        public void AllProperties_WorkCorrectlyTogether()
        {
            // Arrange
            var account = new Account { Id = 111, AccountName = "Comprehensive Test" };
            var feature = new Feature(222, "Comprehensive Feature", true, DateTime.Now, null);
            var testDate = new DateTime(2025, 1, 1, 10, 0, 0);

            // Act
            _sut.Id = 333;
            _sut.Account = account;
            _sut.Feature = feature;
            _sut.ModifiedDate = testDate;

            // Assert
            Assert.Multiple(() =>
         {
             Assert.That(_sut.Id, Is.EqualTo(333));
             Assert.That(_sut.AccountId, Is.EqualTo(111));
             Assert.That(_sut.FeatureId, Is.EqualTo(222));
             Assert.That(_sut.Account, Is.EqualTo(account));
             Assert.That(_sut.Feature, Is.EqualTo(feature));
             Assert.That(_sut.ModifiedDate, Is.EqualTo(testDate));
             Assert.That(_sut.CreatedDate, Is.LessThanOrEqualTo(DateTime.UtcNow));
         });
        }

        [Test, Category("Models")]
        public void Properties_WithMaxAndZeroValues_AllScenarios()
        {
            // Arrange & Act - Test with max values
            _sut.Id = int.MaxValue;
            var accountMax = new Account { Id = int.MaxValue, AccountName = "Max" };
            var featureMax = new Feature(int.MaxValue, "Max", true, DateTime.Now, null);
            _sut.Account = accountMax;
            _sut.Feature = featureMax;

            // Assert max values
            Assert.Multiple(() =>
        {
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
            Assert.That(_sut.AccountId, Is.EqualTo(int.MaxValue));
            Assert.That(_sut.FeatureId, Is.EqualTo(int.MaxValue));
        });

            // Act - Test with zero values
            _sut.Id = 0;
            _sut.Account = null;
            _sut.Feature = null;

            // Assert zero values
            Assert.Multiple(() =>
         {
             Assert.That(_sut.Id, Is.EqualTo(0));
             Assert.That(_sut.AccountId, Is.EqualTo(0));
             Assert.That(_sut.FeatureId, Is.EqualTo(0));
         });
        }

        [Test, Category("Models")]
        public void ConstructorChaining_AllConstructorsWork()
        {
            // Test default constructor
            var af1 = new AccountFeature();
            Assert.That(af1.Id, Is.EqualTo(0));

            // Test JSON constructor
            var account = new Account { Id = 1, AccountName = "Test" };
            var feature = new Feature(2, "Test", true, DateTime.Now, null);
            var af2 = new AccountFeature(
                id: 3,
                 account: account,
                            feature: feature,
                   createdDate: DateTime.UtcNow,
                   modifiedDate: null);
            Assert.That(af2.Id, Is.EqualTo(3));

            // Test IAccount/IFeature constructor
            var af3 = new AccountFeature(account, feature);
            Assert.Multiple(() =>
            {
                Assert.That(af3.AccountId, Is.EqualTo(1));
                Assert.That(af3.FeatureId, Is.EqualTo(2));
            });

            // Test DTO constructor
            var accountDTO = new AccountDTO
            {
                Id = 4,
                AccountName = "DTO Test",
                AccountNumber = "DTO123",
                License = "LIC456",
                Features = [],
                Accounts = null,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            };
            var featureDTO = new FeatureDTO
            {
                Id = 5,
                FeatureName = "DTO Feature",
                IsEnabled = true,
                CreatedDate = DateTime.Now,
                ModifiedDate = null
            };
            var af4 = new AccountFeature(accountDTO, featureDTO);
            Assert.Multiple(() =>
                    {
                        Assert.That(af4.AccountId, Is.EqualTo(4));
                        Assert.That(af4.FeatureId, Is.EqualTo(5));
                    });
        }

        #endregion
    }
}
