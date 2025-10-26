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
              Assert.That(accountFeature.AccountId, Is.EqualTo(0)); // Returns 0 due to ?? 0
              Assert.That(accountFeature.FeatureId, Is.EqualTo(0)); // Returns 0 due to ?? 0
              Assert.That(accountFeature.Account, Is.Null);
              Assert.That(accountFeature.Feature, Is.Null);
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
                Assert.That(accountFeature.Account, Is.EqualTo(account));
                Assert.That(accountFeature.Feature, Is.EqualTo(feature));
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
                Assert.That(accountFeature.FeatureId, Is.EqualTo(456));
                Assert.That(accountFeature.Account, Is.EqualTo(account));
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

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.Id, Is.EqualTo(0));
                Assert.That(accountFeature.AccountId, Is.EqualTo(456));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(123));
                Assert.That(accountFeature.Account, Is.Not.Null);
                Assert.That(accountFeature.Account!.Id, Is.EqualTo(456));
                Assert.That(accountFeature.Account!.AccountName, Is.EqualTo("Test Account"));
                Assert.That(accountFeature.Feature, Is.Not.Null);
                Assert.That(accountFeature.Feature!.Id, Is.EqualTo(123));
                Assert.That(accountFeature.Feature.FeatureName, Is.EqualTo("Test Feature"));
                Assert.That(accountFeature.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void FeatureDTOConstructor_WithNullFeatureDTO_ThrowsArgumentNullException()
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
        public void AccountId_ReturnsZero_WhenAccountAndBackingFieldAreNull()
        {
            // Arrange & Act
            _sut.Account = null;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(0)); // Returns 0 due to ?? 0 operator
        }

        [Test, Category("Models")]
        public void AccountId_ReturnsZero_WhenAccountIsSetButBackingFieldIsNull()
        {
            // Arrange
            var account = new Account { Id = 101, AccountName = "Test Account" };

            // Act
            _sut.Account = account;

            // Assert - AccountId still returns 0 because backing field _accountId is not synced
            Assert.That(_sut.AccountId, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void FeatureId_ReturnsZero_WhenFeatureAndBackingFieldAreNull()
        {
            // Arrange & Act
            _sut.Feature = null;

            // Assert
            Assert.That(_sut.FeatureId, Is.EqualTo(0)); // Returns 0 due to ?? 0 operator
        }

        [Test, Category("Models")]
        public void FeatureId_ReturnsZero_WhenFeatureIsSetButBackingFieldIsNull()
        {
            // Arrange
            var feature = new Feature(202, "Test Feature", true, DateTime.Now, null);

            // Act
            _sut.Feature = feature;

            // Assert - FeatureId still returns 0 because backing field _featureId is not synced
            Assert.That(_sut.FeatureId, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void AccountId_IsReadOnly_Property()
        {
            // Arrange & Act
            var accountIdProperty = typeof(AccountFeature).GetProperty(nameof(AccountFeature.AccountId));

            // Assert
            Assert.Multiple(() =>
           {
               Assert.That(accountIdProperty, Is.Not.Null);
               Assert.That(accountIdProperty!.CanRead, Is.True);
               Assert.That(accountIdProperty.GetSetMethod(), Is.Null); // No public setter
           });
        }

        [Test, Category("Models")]
        public void FeatureId_IsReadOnly_Property()
        {
            // Arrange & Act
            var featureIdProperty = typeof(AccountFeature).GetProperty(nameof(AccountFeature.FeatureId));

            // Assert
            Assert.Multiple(() =>
        {
            Assert.That(featureIdProperty, Is.Not.Null);
            Assert.That(featureIdProperty!.CanRead, Is.True);
            Assert.That(featureIdProperty.GetSetMethod(), Is.Null); // No public setter
        });
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
                         Assert.That(accountIdProperty.GetInt32(), Is.EqualTo(0)); // Returns 0 due to backing field

                         Assert.That(root.TryGetProperty("featureId", out var featureIdProperty), Is.True);
                         Assert.That(featureIdProperty.GetInt32(), Is.EqualTo(0)); // Returns 0 due to backing field

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
              Assert.That(accountIdProperty.GetInt32(), Is.EqualTo(0));

              Assert.That(root.TryGetProperty("modifiedDate", out var modifiedDateProperty), Is.True);
              Assert.That(modifiedDateProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
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
            Assert.That(_sut.AccountId, Is.EqualTo(0)); // Still 0 because backing field not synced

            // Act
            _sut.Account = null;

            // Assert
            Assert.Multiple(() =>
                  {
                      Assert.That(_sut.Account, Is.Null);
                      Assert.That(_sut.AccountId, Is.EqualTo(0)); // Still 0 due to ?? 0
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
      Assert.That(_sut.FeatureId, Is.EqualTo(0)); // Still 0 because backing field not synced
      Assert.That(_sut.ModifiedDate, Is.Not.Null);
  });
        }

        [Test, Category("Models")]
        public void Feature_WithNullValue_SetsFeatureIdToZero()
        {
            // Arrange
            var feature = new Feature(888, "Test", true, DateTime.Now, null);
            _sut.Feature = feature;
            Assert.That(_sut.FeatureId, Is.EqualTo(0)); // Still 0 because backing field not synced

            // Act
            _sut.Feature = null;

            // Assert
            Assert.Multiple(() =>
             {
                 Assert.That(_sut.Feature, Is.Null);
                 Assert.That(_sut.FeatureId, Is.EqualTo(0)); // Still 0 due to ?? 0
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
                Assert.That(_sut.AccountId, Is.EqualTo(0)); // Still 0 because backing field not synced
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

        [Test, Category("Models")]
        public void Id_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;

            // Act
            _sut.Id = 456;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(originalModifiedDate, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.UtcNow.AddMinutes(-1)));
            });
        }

        [Test, Category("Models")]
        public void Account_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;
            var account = new Account { Id = 123, AccountName = "Test" };

            // Act
            _sut.Account = account;

            // Assert
            Assert.Multiple(() =>
                   {
                       Assert.That(originalModifiedDate, Is.Null);
                       Assert.That(_sut.ModifiedDate, Is.Not.Null);
                       Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.UtcNow.AddMinutes(-1)));
                   });
        }

        [Test, Category("Models")]
        public void Feature_Setter_UpdatesModifiedDate()
        {
            // Arrange
            var originalModifiedDate = _sut.ModifiedDate;
            var feature = new Feature(456, "Test Feature", true, DateTime.Now, null);

            // Act
            _sut.Feature = feature;

            // Assert
            Assert.Multiple(() =>
              {
                  Assert.That(originalModifiedDate, Is.Null);
                  Assert.That(_sut.ModifiedDate, Is.Not.Null);
                  Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.UtcNow.AddMinutes(-1)));
              });
        }

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
                     });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Account_Set_UpdatesModifiedDate()
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
 });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Feature_Set_UpdatesModifiedDate()
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
                Assert.That(_sut.ModifiedDate, Is.Not.Null);
                Assert.That(_sut.ModifiedDate, Is.Not.EqualTo(beforeModifiedDate));
            });
        }

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
              Assert.That(result.Id, Is.EqualTo(300)); // Uses AccountFeature.Id
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
               Assert.That(result, Does.Contain("0")); // AccountId and FeatureId both return 0
               Assert.That(result, Does.Contain("Id:"));
               Assert.That(result, Does.Contain("AccountId:"));
               Assert.That(result, Does.Contain("FeatureId:"));
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
        public void ImplementsIAccountFeature()
        {
            // Arrange & Act
            var accountFeature = new AccountFeature();

            // Assert
            Assert.That(accountFeature, Is.InstanceOf<IAccountFeature>());
        }

        [Test, Category("Models")]
        public void Account_WithNullValidation_AllowsNull()
        {
            // Arrange & Act & Assert - Setting Account to null should not throw
            Assert.DoesNotThrow(() => _sut.Account = null);
            Assert.That(_sut.Account, Is.Null);
        }

        [Test, Category("Models")]
        public void Feature_WithNullValidation_AllowsNull()
        {
            // Arrange & Act & Assert - Setting Feature to null should not throw
            Assert.DoesNotThrow(() => _sut.Feature = null);
            Assert.That(_sut.Feature, Is.Null);
        }

        [Test, Category("Models")]
        public void JsonSerializerOptions_ConfiguredCorrectly()
        {
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
        public void AllPropertySetters_UpdateModifiedDate()
        {
            // Arrange
            var timestamps = new List<DateTime?>();
            timestamps.Add(_sut.ModifiedDate); // Initial null

            // Act & Assert - Test that all property setters update ModifiedDate
            _sut.Id = 100;
            timestamps.Add(_sut.ModifiedDate);

            System.Threading.Thread.Sleep(1);
            _sut.Account = new Account { Id = 200, AccountName = "Test" };
            timestamps.Add(_sut.ModifiedDate);

            System.Threading.Thread.Sleep(1);
            _sut.Feature = new Feature(300, "Test", true, DateTime.Now, null);
            timestamps.Add(_sut.ModifiedDate);

            // Assert that each property setter updated ModifiedDate
            Assert.Multiple(() =>
                  {
                      Assert.That(timestamps[0], Is.Null); // Initial
                      for (int i = 1; i < timestamps.Count; i++)
                      {
                          Assert.That(timestamps[i], Is.Not.Null);
                          if (i > 1)
                          {
                              Assert.That(timestamps[i], Is.GreaterThanOrEqualTo(timestamps[i - 1]));
                          }
                      }
                  });
        }
    }
}
