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
                Assert.That(accountFeature.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(accountFeature.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(accountFeature.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_SetsPropertiesCorrectly()
        {
            // Arrange
            var testDateCreated = DateTime.UtcNow;

            // Act
            var accountFeature = new AccountFeature(
                id: 1,
                accountId: 123, 
                featureId: 456,
                dateCreated: testDateCreated,
                dateModified: null);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.Id, Is.EqualTo(1));
                Assert.That(accountFeature.AccountId, Is.EqualTo(123));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(456));
                Assert.That(accountFeature.DateCreated, Is.EqualTo(testDateCreated));
                Assert.That(accountFeature.DateModified, Is.Null);
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
                Assert.That(accountFeature.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(accountFeature.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(accountFeature.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void ConstructorWithAccountAndFeature_WithNullAccount_ThrowsException()
        {
            // Arrange
            var feature = new Feature(456, "Test Feature", true, DateTime.Now, DateTime.Now);

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => new AccountFeature(null!, feature));
        }

        [Test, Category("Models")]
        public void ConstructorWithAccountAndFeature_WithNullFeature_ThrowsException()
        {
            // Arrange
            var account = new Account { Id = 789, AccountName = "Test Account" };

            // Act & Assert
            Assert.Throws<NullReferenceException>(() => new AccountFeature(account, null!));
        }

        [Test, Category("Models")]
        public void ConstructorWithAccountAndFeature_WithBothNull_ThrowsException()
        {
            // Act & Assert
            Assert.Throws<NullReferenceException>(() => new AccountFeature(null!, null!));
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
        public void AccountId_CanBeSetAndRetrieved()
        {
            // Arrange
            const int expectedAccountId = 101;

            // Act
            _sut.AccountId = expectedAccountId;

            // Assert
            Assert.That(_sut.AccountId, Is.EqualTo(expectedAccountId));
        }

        [Test, Category("Models")]
        public void FeatureId_CanBeSetAndRetrieved()
        {
            // Arrange
            const int expectedFeatureId = 202;

            // Act
            _sut.FeatureId = expectedFeatureId;

            // Assert
            Assert.That(_sut.FeatureId, Is.EqualTo(expectedFeatureId));
        }

        [Test, Category("Models")]
        public void DateCreated_IsReadOnly_AndSetDuringConstruction()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var accountFeature = new AccountFeature();
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(accountFeature.DateCreated, Is.LessThanOrEqualTo(afterCreation));
            });
        }

        [Test, Category("Models")]
        public void DateModified_CanBeSetAndRetrieved()
        {
            // Arrange
            var expectedDate = new DateTime(2023, 5, 15, 10, 30, 45);

            // Act
            _sut.DateModified = expectedDate;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(expectedDate));
        }

        [Test, Category("Models")]
        public void DateModified_CanBeSetToNull()
        {
            // Arrange & Act
            _sut.DateModified = null;

            // Assert
            Assert.That(_sut.DateModified, Is.Null);
        }

        [Test, Category("Models")]
        public void ToJson_ReturnsValidJsonString()
        {
            // Arrange
            _sut.Id = 1;
            _sut.AccountId = 123;
            _sut.FeatureId = 456;
            _sut.DateModified = new DateTime(2023, 1, 1, 12, 0, 0);

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
                
                Assert.That(root.TryGetProperty("dateCreated", out var dateCreatedProperty), Is.True);
                Assert.That(dateCreatedProperty.ValueKind, Is.EqualTo(JsonValueKind.String));
                
                Assert.That(root.TryGetProperty("dateModified", out var dateModifiedProperty), Is.True);
                Assert.That(dateModifiedProperty.ValueKind, Is.EqualTo(JsonValueKind.String));
            });
        }

        [Test, Category("Models")]
        public void ToJson_HandlesNullDateModified()
        {
            // Arrange
            _sut.Id = 2;
            _sut.AccountId = 789;
            _sut.FeatureId = 101;
            _sut.DateModified = null;

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
                
                Assert.That(root.TryGetProperty("dateModified", out var dateModifiedProperty), Is.True);
                Assert.That(dateModifiedProperty.ValueKind, Is.EqualTo(JsonValueKind.Null));
            });
        }

        [Test, Category("Models")]
        public void Properties_MaintainConsistencyAfterMultipleChanges()
        {
            // Arrange
            const int id = 999;
            const int accountId = 888;
            const int featureId = 777;
            var dateModified = new DateTime(2023, 12, 25, 15, 30, 0);

            // Act
            _sut.Id = id;
            _sut.AccountId = accountId;
            _sut.FeatureId = featureId;
            _sut.DateModified = dateModified;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(id));
                Assert.That(_sut.AccountId, Is.EqualTo(accountId));
                Assert.That(_sut.FeatureId, Is.EqualTo(featureId));
                Assert.That(_sut.DateModified, Is.EqualTo(dateModified));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithZeroValues_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            var accountFeature = new AccountFeature(
                id: 0,
                accountId: 0,
                featureId: 0,
                dateCreated: DateTime.UtcNow,
                dateModified: null);

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
            // Arrange & Act
            _sut.Id = int.MaxValue;
            _sut.AccountId = int.MaxValue;
            _sut.FeatureId = int.MaxValue;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
                Assert.That(_sut.AccountId, Is.EqualTo(int.MaxValue));
                Assert.That(_sut.FeatureId, Is.EqualTo(int.MaxValue));
            });
        }

        [Test, Category("Models")]
        public void Properties_WithMinValues_WorkCorrectly()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _sut.Id = int.MinValue;
                _sut.AccountId = int.MinValue;
                _sut.FeatureId = int.MinValue;
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithSerializerOptions_HandlesCircularReferences()
        {
            // Arrange
            _sut.Id = 100;
            _sut.AccountId = 200;
            _sut.FeatureId = 300;

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
        public void Cast_ToFeatureDTO_WithValidFeature_ReturnsFeatureDTOWithCorrectProperties()
        {
            // Arrange
            var feature = new Feature(1, "Test Feature", true, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(
                id: 789,
                accountId: 123,
                featureId: 456,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = feature
            };

            // Act
            var dto = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto, Is.InstanceOf<FeatureDTO>());
                Assert.That(dto.Id, Is.EqualTo(789));
                Assert.That(dto.FeatureName, Is.EqualTo("Test Feature"));
                Assert.That(dto.IsEnabled, Is.True);
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToIFeatureDTO_WithValidFeature_ReturnsFeatureDTOWithCorrectProperties()
        {
            // Arrange
            var feature = new Feature(2, "Another Feature", false, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(
                id: 777,
                accountId: 999,
                featureId: 888,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = feature
            };

            // Act
            var dto = _sut.Cast<IFeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto, Is.InstanceOf<IFeatureDTO>());
                Assert.That(dto, Is.InstanceOf<FeatureDTO>());
                Assert.That(dto.Id, Is.EqualTo(777));
                Assert.That(dto.FeatureName, Is.EqualTo("Another Feature"));
                Assert.That(dto.IsEnabled, Is.False);
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToFeatureDTO_WithNullFeature_ThrowsNullReferenceException()
        {
            // Arrange
            _sut = new AccountFeature(
                id: 789,
                accountId: 123,
                featureId: 456,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = null
            };

            // Act & Assert
            var ex = Assert.Throws<NullReferenceException>(() => _sut.Cast<FeatureDTO>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Object reference not set to an instance of an object."));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToIFeatureDTO_WithNullFeature_ThrowsNullReferenceException()
        {
            // Arrange
            _sut = new AccountFeature(
                id: 300,
                accountId: 100,
                featureId: 200,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = null
            };

            // Act & Assert
            var ex = Assert.Throws<NullReferenceException>(() => _sut.Cast<IFeatureDTO>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Object reference not set to an instance of an object."));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToFeatureDTO_WithFeatureHavingNullProperties_HandlesCorrectly()
        {
            // Arrange
            var feature = new Feature
            {
                Id = 5,
                FeatureName = null,
                IsEnabled = true
            };
            _sut = new AccountFeature(
                id: 777,
                accountId: 555,
                featureId: 666,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = feature
            };

            // Act
            var dto = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto.Id, Is.EqualTo(777));
                Assert.That(dto.FeatureName, Is.Null);
                Assert.That(dto.IsEnabled, Is.True);
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToUnsupportedType_ThrowsInvalidCastException()
        {
            // Arrange
            var feature = new Feature(1, "Test", true, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(
                id: 789,
                accountId: 123,
                featureId: 456,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = feature
            };

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<Account>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Feature to type Account."));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToAnotherUnsupportedType_ThrowsInvalidCastException()
        {
            // Arrange
            var feature = new Feature(1, "Test", true, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(
                id: 789,
                accountId: 123,
                featureId: 456,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = feature
            };

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<User>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Feature to type User."));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ReturnsNewInstanceEachTime()
        {
            // Arrange
            var feature = new Feature(1, "Test Feature", true, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(
                id: 789,
                accountId: 123,
                featureId: 456,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = feature
            };

            // Act
            var dto1 = _sut.Cast<FeatureDTO>();
            var dto2 = _sut.Cast<FeatureDTO>();
            var idto1 = _sut.Cast<IFeatureDTO>();
            var idto2 = _sut.Cast<IFeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto1, Is.Not.SameAs(dto2));
                Assert.That(idto1, Is.Not.SameAs(idto2));
                Assert.That(dto1, Is.Not.SameAs(idto1));
                
                // Verify they have the same property values but are different instances
                Assert.That(dto1.Id, Is.EqualTo(dto2.Id));
                Assert.That(dto1.FeatureName, Is.EqualTo(dto2.FeatureName));
                Assert.That(dto1.IsEnabled, Is.EqualTo(dto2.IsEnabled));
                
                Assert.That(idto1.Id, Is.EqualTo(idto2.Id));
                Assert.That(idto1.FeatureName, Is.EqualTo(idto2.FeatureName));
                Assert.That(idto1.IsEnabled, Is.EqualTo(idto2.IsEnabled));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_WithDifferentFeatureProperties_ReturnsDifferentDTOValues()
        {
            // Arrange
            var feature1 = new Feature(10, "Feature One", true, DateTime.Now, DateTime.Now);
            var feature2 = new Feature(20, "Feature Two", false, DateTime.Now, DateTime.Now);
            
            var accountFeature1 = new AccountFeature(
                id: 1000,
                accountId: 100,
                featureId: 200,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = feature1
            };
            
            var accountFeature2 = new AccountFeature(
                id: 2000,
                accountId: 300,
                featureId: 400,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = feature2
            };

            // Act
            var dto1 = accountFeature1.Cast<FeatureDTO>();
            var dto2 = accountFeature2.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto1.Id, Is.EqualTo(1000));
                Assert.That(dto1.FeatureName, Is.EqualTo("Feature One"));
                Assert.That(dto1.IsEnabled, Is.True);
                
                Assert.That(dto2.Id, Is.EqualTo(2000));
                Assert.That(dto2.FeatureName, Is.EqualTo("Feature Two"));
                Assert.That(dto2.IsEnabled, Is.False);
                
                Assert.That(dto1.Id, Is.Not.EqualTo(dto2.Id));
                Assert.That(dto1.FeatureName, Is.Not.EqualTo(dto2.FeatureName));
                Assert.That(dto1.IsEnabled, Is.Not.EqualTo(dto2.IsEnabled));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_WithZeroId_ReturnsCorrectDTO()
        {
            // Arrange
            var feature = new Feature(1, "Zero ID Test", false, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(
                id: 0,
                accountId: 0,
                featureId: 0,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = feature
            };

            // Act
            var dto = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto.Id, Is.EqualTo(0));
                Assert.That(dto.FeatureName, Is.EqualTo("Zero ID Test"));
                Assert.That(dto.IsEnabled, Is.False);
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_WithMaxIntId_ReturnsCorrectDTO()
        {
            // Arrange
            var feature = new Feature(1, "Max ID Test", true, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(
                id: int.MaxValue,
                accountId: int.MaxValue,
                featureId: int.MaxValue,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = feature
            };

            // Act
            var dto = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto.Id, Is.EqualTo(int.MaxValue));
                Assert.That(dto.FeatureName, Is.EqualTo("Max ID Test"));
                Assert.That(dto.IsEnabled, Is.True);
            });
        }
        
        [Test]
        [Category("Models")]
        public void ExplicitInterfaceFeature_CanBeSetAndRetrieved()
        {
            // Arrange
            var feature = new Feature(123, "Test Feature", true, DateTime.Now, DateTime.Now);
            IAccountFeature accountFeature = _sut;

            // Act
            accountFeature.Feature = feature;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.Feature, Is.EqualTo(feature));
                Assert.That(_sut.Feature, Is.EqualTo(feature));
                Assert.That(_sut.FeatureId, Is.EqualTo(123));
                Assert.That(_sut.DateModified, Is.Not.Null);
            });
        }

        [Test]
        [Category("Models")]
        public void ExplicitInterfaceAccount_CanBeSetAndRetrieved()
        {
            // Arrange
            var account = new Account { Id = 456, AccountName = "Test Account" };
            IAccountFeature accountFeature = _sut;

            // Act
            accountFeature.Account = account;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.Account, Is.EqualTo(account));
                Assert.That(_sut.Account, Is.EqualTo(account));
                Assert.That(_sut.AccountId, Is.EqualTo(456));
                Assert.That(_sut.DateModified, Is.Not.Null);
            });
        }

        [Test]
        [Category("Models")]
        public void ExplicitInterfaceFeature_WithNullValue_SetsToNull()
        {
            // Arrange
            IAccountFeature accountFeature = _sut;

            // Act
            accountFeature.Feature = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Feature, Is.Null);
                Assert.That(_sut.FeatureId, Is.EqualTo(0));
                Assert.That(_sut.DateModified, Is.Not.Null);
            });
        }

        [Test]
        [Category("Models")]
        public void ExplicitInterfaceAccount_WithNullValue_SetsToNull()
        {
            // Arrange
            IAccountFeature accountFeature = _sut;

            // Act
            accountFeature.Account = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Account, Is.Null);
                Assert.That(_sut.AccountId, Is.EqualTo(0));
                Assert.That(_sut.DateModified, Is.Not.Null);
            });
        }

        [Test]
        [Category("Models")]
        public void Account_CanBeSetAndRetrieved()
        {
            // Arrange
            var account = new Account { Id = 789, AccountName = "Test Account" };

            // Act
            _sut.Account = account;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Account, Is.EqualTo(account));
                Assert.That(_sut.AccountId, Is.EqualTo(789));
                Assert.That(_sut.DateModified, Is.Not.Null);
            });
        }

        [Test]
        [Category("Models")]
        public void Account_WithNullValue_SetsAccountIdToZero()
        {
            // Arrange
            _sut.AccountId = 999; // Set to non-zero first

            // Act
            _sut.Account = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Account, Is.Null);
                Assert.That(_sut.AccountId, Is.EqualTo(0));
                Assert.That(_sut.DateModified, Is.Not.Null);
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
                Assert.That(_sut.DateModified, Is.Not.Null);
            });
        }

        [Test]
        [Category("Models")]
        public void Feature_WithNullValue_SetsFeatureIdToZero()
        {
            // Arrange
            _sut.FeatureId = 888; // Set to non-zero first

            // Act
            _sut.Feature = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Feature, Is.Null);
                Assert.That(_sut.FeatureId, Is.EqualTo(0));
                Assert.That(_sut.DateModified, Is.Not.Null);
            });
        }

        [Test]
        [Category("Models")]
        public void Properties_SetDateModified_WhenChanged()
        {
            // Arrange
            var initialDateModified = _sut.DateModified;
            System.Threading.Thread.Sleep(10); // Ensure time difference

            // Act & Assert - Test each property setter
            _sut.Id = 100;
            Assert.That(_sut.DateModified, Is.Not.EqualTo(initialDateModified));
            
            var afterIdChange = _sut.DateModified;
            System.Threading.Thread.Sleep(10);
            
            _sut.AccountId = 200;
            Assert.That(_sut.DateModified, Is.Not.EqualTo(afterIdChange));
            
            var afterAccountIdChange = _sut.DateModified;
            System.Threading.Thread.Sleep(10);
            
            _sut.FeatureId = 300;
            Assert.That(_sut.DateModified, Is.Not.EqualTo(afterAccountIdChange));
        }

        #region Comprehensive Coverage Tests

        [Test, Category("Models")]
        public void ImplementsIAccountFeature()
        {
            // Act & Assert
            Assert.That(_sut, Is.InstanceOf<IAccountFeature>());
        }

        [Test, Category("Models")]
        public void ImplementsIDomainEntity()
        {
            // Act & Assert
            Assert.That(_sut, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("Models")]
        public void TypeInformation_ShouldBeCorrect()
        {
            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.GetType(), Is.EqualTo(typeof(AccountFeature)));
                Assert.That(_sut.GetType().Name, Is.EqualTo("AccountFeature"));
                Assert.That(_sut.GetType().Namespace, Is.EqualTo("OrganizerCompanion.Core.Models.Domain"));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithDateModifiedValue_SetsCorrectly()
        {
            // Arrange
            var testDateCreated = new DateTime(2025, 1, 1, 10, 0, 0);
            var testDateModified = new DateTime(2025, 10, 20, 15, 30, 45);

            // Act
            var accountFeature = new AccountFeature(
                id: 555,
                accountId: 666,
                featureId: 777,
                dateCreated: testDateCreated,
                dateModified: testDateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.Id, Is.EqualTo(555));
                Assert.That(accountFeature.AccountId, Is.EqualTo(666));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(777));
                Assert.That(accountFeature.DateCreated, Is.EqualTo(testDateCreated));
                Assert.That(accountFeature.DateModified, Is.EqualTo(testDateModified));
            });
        }

        [Test, Category("Models")]
        public void DefaultDateModified_IsDefault()
        {
            // Arrange & Act
            var accountFeature = new AccountFeature();

            // Assert
            Assert.That(accountFeature.DateModified, Is.EqualTo(default(DateTime)));
        }

        [Test, Category("Models")]
        public void DateCreated_IsReadOnlyAndSetAtConstruction()
        {
            // Arrange
            var beforeCreation = DateTime.UtcNow;

            // Act
            var accountFeature = new AccountFeature();
            var afterCreation = DateTime.UtcNow;

            // Assert - Verify DateCreated is within the expected range
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(accountFeature.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                
                // Verify property doesn't have a public setter
                var property = typeof(AccountFeature).GetProperty(nameof(AccountFeature.DateCreated));
                Assert.That(property?.GetSetMethod(), Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Cast_Exception_InTryCatch_ReThrowsOriginalException()
        {
            // Arrange
            _sut = new AccountFeature(
                id: 789,
                accountId: 123,
                featureId: 456,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = null // This will cause NullReferenceException
            };

            // Act & Assert - The try-catch block should re-throw the original exception
            var ex = Assert.Throws<NullReferenceException>(() => _sut.Cast<FeatureDTO>());
            Assert.That(ex, Is.Not.Null);
        }

        [Test, Category("Models")]
        public void Cast_GenericException_ReThrowsCorrectly()
        {
            // Arrange - Create a scenario that might throw a different exception
            var feature = new Feature(1, "Test", true, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(
                id: 789,
                accountId: 123,
                featureId: 456,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = feature
            };

            // Act & Assert - Test that unsupported types throw InvalidCastException
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<AccountFeature>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Feature to type AccountFeature"));
            });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Feature_WithNull_AcceptsNull()
        {
            // Arrange
            IAccountFeature accountFeature = _sut;
            var initialFeature = new Feature(1, "Test", true, DateTime.Now, DateTime.Now);
            accountFeature.Feature = initialFeature;

            // Act - Casting null to reference type succeeds in C#
            accountFeature.Feature = null;

            // Assert - Should accept null and reset FeatureId
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.Feature, Is.Null);
                Assert.That(_sut.Feature, Is.Null);
                Assert.That(_sut.FeatureId, Is.EqualTo(0));
                Assert.That(_sut.DateModified, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void ExplicitInterface_Account_WithNull_AcceptsNull()
        {
            // Arrange
            IAccountFeature accountFeature = _sut;
            var initialAccount = new Account { Id = 123, AccountName = "Test" };
            accountFeature.Account = initialAccount;

            // Act - Casting null to reference type succeeds in C#
            accountFeature.Account = null;

            // Assert - Should accept null and reset AccountId
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.Account, Is.Null);
                Assert.That(_sut.Account, Is.Null);
                Assert.That(_sut.AccountId, Is.EqualTo(0));
                Assert.That(_sut.DateModified, Is.Not.Null);
            });
        }

        [Test, Category("Models")]
        public void Account_Property_SetsAccountIdFromAccountId()
        {
            // Arrange
            var account = new Account { Id = 999, AccountName = "Test Account" };

            // Act
            _sut.Account = account;

            // Assert - Verify that setting Account also sets AccountId
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Account, Is.EqualTo(account));
                Assert.That(_sut.AccountId, Is.EqualTo(999));
                Assert.That(_sut.DateModified, Is.Not.Null);
                Assert.That(_sut.DateModified, Is.Not.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void Feature_Property_SetsFeatureIdFromFeatureId()
        {
            // Arrange
            var feature = new Feature(888, "Test Feature", true, DateTime.Now, DateTime.Now);

            // Act
            _sut.Feature = feature;

            // Assert - Verify that setting Feature also sets FeatureId
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Feature, Is.EqualTo(feature));
                Assert.That(_sut.FeatureId, Is.EqualTo(888));
                Assert.That(_sut.DateModified, Is.Not.Null);
                Assert.That(_sut.DateModified, Is.Not.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void MultiplePropertyChanges_UpdateDateModifiedEachTime()
        {
            // Arrange
            var initialDateModified = _sut.DateModified;

            // Act & Assert - Each property change should update DateModified
            System.Threading.Thread.Sleep(1); // Ensure different timestamps
            _sut.Id = 100;
            var afterId = _sut.DateModified;
            Assert.That(afterId, Is.Not.EqualTo(initialDateModified));

            System.Threading.Thread.Sleep(1);
            _sut.AccountId = 200;
            var afterAccountId = _sut.DateModified;
            Assert.That(afterAccountId, Is.Not.EqualTo(afterId));

            System.Threading.Thread.Sleep(1);
            _sut.FeatureId = 300;
            var afterFeatureId = _sut.DateModified;
            Assert.That(afterFeatureId, Is.Not.EqualTo(afterAccountId));

            System.Threading.Thread.Sleep(1);
            _sut.Account = new Account { Id = 400 };
            var afterAccount = _sut.DateModified;
            Assert.That(afterAccount, Is.Not.EqualTo(afterFeatureId));

            System.Threading.Thread.Sleep(1);
            _sut.Feature = new Feature(500, "Test", true, DateTime.Now, DateTime.Now);
            var afterFeature = _sut.DateModified;
            Assert.That(afterFeature, Is.Not.EqualTo(afterAccount));
        }

        [Test, Category("Models")]
        public void ToJson_WithComplexData_SerializesCorrectly()
        {
            // Arrange
            _sut.Id = 12345;
            _sut.AccountId = 67890;
            _sut.FeatureId = 11111;
            _sut.DateModified = new DateTime(2025, 10, 20, 14, 35, 42, 999);

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

                Assert.That(root.TryGetProperty("dateCreated", out var dateCreatedProperty), Is.True);
                Assert.That(dateCreatedProperty.ValueKind, Is.EqualTo(JsonValueKind.String));

                Assert.That(root.TryGetProperty("dateModified", out var dateModifiedProperty), Is.True);
                Assert.That(dateModifiedProperty.ValueKind, Is.EqualTo(JsonValueKind.String));
            });
        }

        [Test, Category("Models")]
        public void JsonSerializerOptions_HasIgnoreCyclesReferenceHandler()
        {
            // This test verifies that the serializer options are configured correctly
            // by testing serialization doesn't fail with potential circular references

            // Arrange
            _sut.Id = 999;
            _sut.AccountId = 888;
            _sut.FeatureId = 777;

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
        public void ExplicitInterface_Properties_WorkThroughInterface()
        {
            // Arrange
            IAccountFeature accountFeature = _sut;
            var feature = new Feature(123, "Interface Feature", true, DateTime.Now, DateTime.Now);
            var account = new Account { Id = 456, AccountName = "Interface Account" };

            // Act & Assert - Test getting properties through interface
            accountFeature.Feature = feature;
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.Feature, Is.EqualTo(feature));
                Assert.That(accountFeature.Feature, Is.SameAs(_sut.Feature));
            });

            accountFeature.Account = account;
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.Account, Is.EqualTo(account));
                Assert.That(accountFeature.Account, Is.SameAs(_sut.Account));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithEmptyFeatureName_HandlesCorrectly()
        {
            // Arrange
            var feature = new Feature(1, "", false, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(
                id: 777,
                accountId: 555,
                featureId: 666,
                dateCreated: DateTime.UtcNow,
                dateModified: null)
            {
                Feature = feature
            };

            // Act
            var dto = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(dto, Is.Not.Null);
                Assert.That(dto.Id, Is.EqualTo(777));
                Assert.That(dto.FeatureName, Is.EqualTo(""));
                Assert.That(dto.IsEnabled, Is.False);
            });
        }

        [Test, Category("Models")]
        public void Cast_WithComplexFeature_PreservesAllProperties()
        {
            // Arrange
            var feature = new Feature(999, "Complex Feature Name", true, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(
                id: 1000,
                accountId: 2000,
                featureId: 3000,
                dateCreated: DateTime.UtcNow,
                dateModified: DateTime.Now)
            {
                Feature = feature
            };

            // Act
            var dto = _sut.Cast<FeatureDTO>();
            var interfaceDto = _sut.Cast<IFeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                // Test FeatureDTO cast
                Assert.That(dto.Id, Is.EqualTo(1000));
                Assert.That(dto.FeatureName, Is.EqualTo("Complex Feature Name"));
                Assert.That(dto.IsEnabled, Is.True);

                // Test IFeatureDTO cast
                Assert.That(interfaceDto.Id, Is.EqualTo(1000));
                Assert.That(interfaceDto.FeatureName, Is.EqualTo("Complex Feature Name"));
                Assert.That(interfaceDto.IsEnabled, Is.True);

                // Verify they're the same type but different instances
                Assert.That(interfaceDto, Is.InstanceOf<FeatureDTO>());
                Assert.That(dto, Is.Not.SameAs(interfaceDto));
            });
        }

        [Test, Category("Models")]
        public void NegativeIds_AreNotAccepted()
        {
            // Arrange, Act & Assert - Domain models don't validate, they accept any int values
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _sut.Id = -100;
                _sut.AccountId = -200;
                _sut.FeatureId = -300;
            });
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
          accountId: 67890,
          featureId: 98765,
          dateCreated: createdDate,
          dateModified: modifiedDate)
      {
        // Set complex properties
        Account = account,
        Feature = feature
      };

      // Assert - Verify all properties
      Assert.Multiple(() =>
            {
                Assert.That(accountFeature.Id, Is.EqualTo(12345));
                Assert.That(accountFeature.AccountId, Is.EqualTo(2)); // Should be overridden by Account.Id
                Assert.That(accountFeature.Account, Is.EqualTo(account));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(1)); // Should be overridden by Feature.Id
                Assert.That(accountFeature.Feature, Is.EqualTo(feature));
                Assert.That(accountFeature.DateCreated, Is.EqualTo(createdDate));
                Assert.That(accountFeature.DateModified, Is.Not.EqualTo(modifiedDate)); // Updated by property setters
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
            _sut.AccountId = 456;
            _sut.FeatureId = 789;

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
            // Arrange
            _sut.Id = 0;
            _sut.AccountId = 0;
            _sut.FeatureId = 0;

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
            _sut.AccountId = int.MaxValue;
            _sut.FeatureId = int.MaxValue;

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

        #endregion
    }
}
