using System.Text.Json;
using NUnit.Framework;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
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
            // Arrange & Act
            var accountFeature = new AccountFeature(accountId: 123, featureId: 456);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.AccountId, Is.EqualTo(123));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(456));
                Assert.That(accountFeature.Id, Is.EqualTo(0)); // Default value
                Assert.That(accountFeature.DateModified, Is.EqualTo(default(DateTime)));
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
        public void IsCast_Getter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.IsCast; });
        }

        [Test, Category("Models")]
        public void IsCast_Setter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.IsCast = true);
        }

        [Test, Category("Models")]
        public void CastId_Getter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastId; });
        }

        [Test, Category("Models")]
        public void CastId_Setter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.CastId = 1);
        }

        [Test, Category("Models")]
        public void CastType_Getter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastType; });
        }

        [Test, Category("Models")]
        public void CastType_Setter_ThrowsNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.CastType = "SomeType");
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
            var accountFeature = new AccountFeature(accountId: 0, featureId: 0);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.AccountId, Is.EqualTo(0));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(0));
                Assert.That(accountFeature.Id, Is.EqualTo(0));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNegativeValues_SetsPropertiesCorrectly()
        {
            // Arrange & Act
            var accountFeature = new AccountFeature(accountId: -1, featureId: -2);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(accountFeature.AccountId, Is.EqualTo(-1));
                Assert.That(accountFeature.FeatureId, Is.EqualTo(-2));
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
            // Arrange & Act
            _sut.Id = int.MinValue;
            _sut.AccountId = int.MinValue;
            _sut.FeatureId = int.MinValue;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(int.MinValue));
                Assert.That(_sut.AccountId, Is.EqualTo(int.MinValue));
                Assert.That(_sut.FeatureId, Is.EqualTo(int.MinValue));
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
            var feature = new Feature(1, "Test Feature", true, false, 0, null, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(123, 456)
            {
                Id = 789,
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
            var feature = new Feature(2, "Another Feature", false, false, 0, null, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(999, 888)
            {
                Id = 777,
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
        public void Cast_ToFeatureDTO_WithNullFeature_ThrowsInvalidCastException()
        {
            // Arrange
            _sut = new AccountFeature(123, 456)
            {
                Id = 789,
                Feature = null
            };

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<FeatureDTO>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Error Feature Email to type FeatureDTO"));
                Assert.That(ex.InnerException, Is.Not.Null);
                Assert.That(ex.InnerException, Is.InstanceOf<NullReferenceException>());
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToIFeatureDTO_WithNullFeature_ThrowsInvalidCastException()
        {
            // Arrange
            _sut = new AccountFeature(100, 200)
            {
                Id = 300,
                Feature = null
            };

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<IFeatureDTO>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Error Feature Email to type IFeatureDTO"));
                Assert.That(ex.InnerException, Is.Not.Null);
                Assert.That(ex.InnerException, Is.InstanceOf<NullReferenceException>());
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
            _sut = new AccountFeature(555, 666)
            {
                Id = 777,
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
            var feature = new Feature(1, "Test", true, false, 0, null, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(123, 456)
            {
                Id = 789,
                Feature = feature
            };

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<Account>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Feature to type Account, casting is not supported for this type"));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ToAnotherUnsupportedType_ThrowsInvalidCastException()
        {
            // Arrange
            var feature = new Feature(1, "Test", true, false, 0, null, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(123, 456)
            {
                Id = 789,
                Feature = feature
            };

            // Act & Assert
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<User>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Feature to type User, casting is not supported for this type"));
            });
        }

        [Test]
        [Category("Models")]
        public void Cast_ReturnsNewInstanceEachTime()
        {
            // Arrange
            var feature = new Feature(1, "Test Feature", true, false, 0, null, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(123, 456)
            {
                Id = 789,
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
            var feature1 = new Feature(10, "Feature One", true, false, 0, null, DateTime.Now, DateTime.Now);
            var feature2 = new Feature(20, "Feature Two", false, false, 0, null, DateTime.Now, DateTime.Now);
            
            var accountFeature1 = new AccountFeature(100, 200)
            {
                Id = 1000,
                Feature = feature1
            };
            
            var accountFeature2 = new AccountFeature(300, 400)
            {
                Id = 2000,
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
            var feature = new Feature(1, "Zero ID Test", false, false, 0, null, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(0, 0)
            {
                Id = 0,
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
            var feature = new Feature(1, "Max ID Test", true, false, 0, null, DateTime.Now, DateTime.Now);
            _sut = new AccountFeature(int.MaxValue, int.MaxValue)
            {
                Id = int.MaxValue,
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
    }
}
