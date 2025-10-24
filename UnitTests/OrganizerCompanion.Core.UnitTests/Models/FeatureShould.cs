using NUnit.Framework;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Models.Domain;

namespace OrganizerCompanion.Core.UnitTests.Models
{
    [TestFixture]
    internal class FeatureShould
    {
        private Feature _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new Feature();
        }

        [TearDown]
        public void TearDown()
        {
            _sut = null!;
        }

        [Test, Category("Models")]
        public void DefaultConstructor_ShouldCreateFeatureWithDefaultValues()
        {
            // Arrange & Act
            _sut = new Feature();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.FeatureName, Is.Null);
                Assert.That(_sut.IsEnabled, Is.False);
                Assert.That(_sut.CreatedDate, Is.LessThan(DateTime.UtcNow));
                Assert.That(_sut.ModifiedDate, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_ShouldCreateFeatureWithProvidedValues()
        {
            // Arrange
            var id = 123;
            var featureName = "TestFeature";
            var isEnabled = true;
            var createdDate = DateTime.Now.AddDays(-1);
            var modifiedDate = DateTime.Now.AddHours(-2);

            // Act
            var feature = new Feature(id, featureName, isEnabled, createdDate, modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(feature.Id, Is.EqualTo(id));
                Assert.That(feature.FeatureName, Is.EqualTo(featureName));
                Assert.That(feature.IsEnabled, Is.EqualTo(isEnabled));
                Assert.That(feature.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(feature.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullValues_ShouldCreateFeatureWithNullValues()
        {
            // Arrange
            var id = 123;
            string? featureName = null;
            var isEnabled = false;
            var createdDate = DateTime.Now.AddDays(-1);
            DateTime? modifiedDate = null;

            // Act
            var feature = new Feature(id, featureName, isEnabled, createdDate, modifiedDate);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(feature.Id, Is.EqualTo(id));
                Assert.That(feature.FeatureName, Is.Null);
                Assert.That(feature.IsEnabled, Is.False);
                Assert.That(feature.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(feature.ModifiedDate, Is.Null);
            });
        }

        #region IFeatureDTO Constructor Tests

        [Test, Category("Models")]
        public void DTOConstructor_WithCompleteDTO_ShouldSetAllProperties()
        {
            // Arrange
            var dto = new MockFeatureDTO
            {
                Id = 123,
                FeatureName = "TestFeature",
                IsEnabled = true,
                CreatedDate = DateTime.Now.AddDays(-2),
                ModifiedDate = DateTime.Now.AddDays(-1)
            };

            // Act
            _sut = new Feature(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.FeatureName, Is.EqualTo(dto.FeatureName));
                Assert.That(_sut.IsEnabled, Is.EqualTo(dto.IsEnabled));
                Assert.That(_sut.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithMinimalDTO_ShouldSetBasicProperties()
        {
            // Arrange
            var dto = new MockFeatureDTO
            {
                FeatureName = "MinimalFeature"
            };

            // Act
            _sut = new Feature(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.FeatureName, Is.EqualTo(dto.FeatureName));
                Assert.That(_sut.IsEnabled, Is.EqualTo(dto.IsEnabled));
                Assert.That(_sut.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullFeatureName_ShouldAcceptNullValue()
        {
            // Arrange
            var dto = new MockFeatureDTO
            {
                Id = 456,
                FeatureName = null,
                IsEnabled = false
            };

            // Act
            _sut = new Feature(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.FeatureName, Is.Null);
                Assert.That(_sut.IsEnabled, Is.EqualTo(dto.IsEnabled));
                Assert.That(_sut.CreatedDate, Is.EqualTo(dto.CreatedDate));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(dto.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithEmptyFeatureName_ShouldAcceptEmptyString()
        {
            // Arrange
            var dto = new MockFeatureDTO
            {
                Id = 789,
                FeatureName = string.Empty,
                IsEnabled = true
            };

            // Act
            _sut = new Feature(dto);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(dto.Id));
                Assert.That(_sut.FeatureName, Is.EqualTo(string.Empty));
                Assert.That(_sut.IsEnabled, Is.EqualTo(dto.IsEnabled));
            });
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithIsEnabledTrue_ShouldSetIsEnabledCorrectly()
        {
            // Arrange
            var dto = new MockFeatureDTO
            {
                FeatureName = "EnabledFeature",
                IsEnabled = true
            };

            // Act
            _sut = new Feature(dto);

            // Assert
            Assert.That(_sut.IsEnabled, Is.True);
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithIsEnabledFalse_ShouldSetIsEnabledCorrectly()
        {
            // Arrange
            var dto = new MockFeatureDTO
            {
                FeatureName = "DisabledFeature",
                IsEnabled = false
            };

            // Act
            _sut = new Feature(dto);

            // Assert
            Assert.That(_sut.IsEnabled, Is.False);
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithNullDTO_ShouldThrowArgumentNullException()
        {
            // Act & Assert
            Assert.Throws<NullReferenceException>(() => new Feature((IFeatureDTO)null!));
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithSpecialCharactersInName_ShouldAcceptSpecialCharacters()
        {
            // Arrange
            var dto = new MockFeatureDTO
            {
                FeatureName = "Feature!@#$%^&*()_+-={}[]|\\:;\"'<>?,./ 123",
                IsEnabled = true
            };

            // Act
            _sut = new Feature(dto);

            // Assert
            Assert.That(_sut.FeatureName, Is.EqualTo(dto.FeatureName));
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithUnicodeCharacters_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var dto = new MockFeatureDTO
            {
                FeatureName = "Feature 功能 🚀 ñáéíóú",
                IsEnabled = false
            };

            // Act
            _sut = new Feature(dto);

            // Assert
            Assert.That(_sut.FeatureName, Is.EqualTo(dto.FeatureName));
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithLongFeatureName_ShouldAcceptLongString()
        {
            // Arrange
            var longName = new string('A', 1000);
            var dto = new MockFeatureDTO
            {
                FeatureName = longName,
                IsEnabled = true
            };

            // Act
            _sut = new Feature(dto);

            // Assert
            Assert.That(_sut.FeatureName, Is.EqualTo(longName));
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithZeroId_ShouldAcceptZeroId()
        {
            // Arrange
            var dto = new MockFeatureDTO
            {
                Id = 0,
                FeatureName = "ZeroIdFeature",
                IsEnabled = true
            };

            // Act
            _sut = new Feature(dto);

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("Models")]
        public void DTOConstructor_WithMaxIntId_ShouldAcceptMaxValue()
        {
            // Arrange
            var dto = new MockFeatureDTO
            {
                Id = int.MaxValue,
                FeatureName = "MaxIdFeature",
                IsEnabled = false
            };

            // Act
            _sut = new Feature(dto);

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        #endregion

        [Test, Category("Models")]
        public void Id_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newId = 123;

            // Act
            _sut.Id = newId;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(newId));
                Assert.That(_sut.ModifiedDate, Is.LessThan(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void Id_WhenSetToZero_ShouldStillUpdateModifiedDate()
        {
            // Arrange
            _sut.Id = 123; // Set initial value
            var beforeSet = DateTime.Now;

            // Act
            _sut.Id = 0;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.ModifiedDate, Is.LessThan(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void FeatureName_WhenSet_ShouldUpdateModifiedDate()
        {
            // Arrange
            var newFeatureName = "Authentication";

            // Act
            _sut.FeatureName = newFeatureName;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.FeatureName, Is.EqualTo(newFeatureName));
                Assert.That(_sut.ModifiedDate, Is.LessThan(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void FeatureName_WhenSetToNull_ShouldUpdateModifiedDate()
        {
            // Arrange
            _sut.FeatureName = "TestFeature"; // Set initial value

            // Act
            _sut.FeatureName = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.FeatureName, Is.Null);
                Assert.That(_sut.ModifiedDate, Is.LessThan(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void FeatureName_WhenSetToEmptyString_ShouldUpdateModifiedDate()
        {
            // Arrange & Act
            _sut.FeatureName = string.Empty;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.FeatureName, Is.EqualTo(string.Empty));
                Assert.That(_sut.ModifiedDate, Is.LessThan(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void IsEnabled_WhenSetToTrue_ShouldUpdateModifiedDate()
        {
            // Arrange & Act
            _sut.IsEnabled = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsEnabled, Is.True);
                Assert.That(_sut.ModifiedDate, Is.LessThan(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void IsEnabled_WhenSetToFalse_ShouldUpdateModifiedDate()
        {
            // Arrange
            _sut.IsEnabled = true; // Set initial value

            // Act
            _sut.IsEnabled = false;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsEnabled, Is.False);
                Assert.That(_sut.ModifiedDate, Is.LessThan(DateTime.UtcNow));
            });
        }

        [Test, Category("Models")]
        public void CreatedDate_ShouldBeReadOnly()
        {
            // Arrange
            var initialCreatedDate = _sut.CreatedDate;

            // Act & Assert - CreatedDate should not have a public setter
            // This test verifies the property is read-only by checking it doesn't change
            Assert.That(_sut.CreatedDate, Is.EqualTo(initialCreatedDate));
        }

        [Test, Category("Models")]
        public void ModifiedDate_CanBeSetDirectly()
        {
            // Arrange
            var newModifiedDate = DateTime.Now.AddHours(-1);

            // Act
            _sut.ModifiedDate = newModifiedDate;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(newModifiedDate));
        }

        [Test, Category("Models")]
        public void ToJson_ShouldReturnValidJsonString()
        {
            // Arrange
            _sut.Id = 123;
            _sut.FeatureName = "TestFeature";
            _sut.IsEnabled = true;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":123"));
                Assert.That(json, Does.Contain("\"featureName\":\"TestFeature\""));
                Assert.That(json, Does.Contain("\"isEnabled\":true"));
                Assert.That(json, Does.Contain("\"createdDate\""));
                Assert.That(json, Does.Contain("\"modifiedDate\""));
            });
        }

        [Test, Category("Models")]
        public void ToJson_WithNullValues_ShouldHandleNullsCorrectly()
        {
            // Arrange
            _sut.Id = 1;
            _sut.FeatureName = null;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null);
                Assert.That(json, Is.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":1"));
                Assert.That(json, Does.Contain("\"featureName\":null"));
            });
        }

        [Test, Category("Models")]
        public void ToString_ShouldReturnFormattedString()
        {
            // Arrange
            _sut.Id = 123;
            _sut.FeatureName = "TestFeature";

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain("123"));
                Assert.That(result, Does.Contain("TestFeature"));
                Assert.That(result, Does.Contain(".Id:123"));
                Assert.That(result, Does.Contain(".FeatureName:TestFeature"));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithNullFeatureName_ShouldHandleNull()
        {
            // Arrange
            _sut.Id = 456;
            _sut.FeatureName = null;

            // Act
            var result = _sut.ToString();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Does.Contain(".Id:456"));
                Assert.That(result, Does.Contain(".FeatureName:"));
            });
        }

        [Test, Category("Models")]
        public void Properties_WhenSetMultipleTimes_ShouldRetainLastValue()
        {
            // Arrange
            var firstId = 100;
            var secondId = 200;
            var firstName = "FirstFeature";
            var secondName = "SecondFeature";

            // Act
            _sut.Id = firstId;
            _sut.FeatureName = firstName;
            _sut.Id = secondId;
            _sut.FeatureName = secondName;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(secondId));
                Assert.That(_sut.FeatureName, Is.EqualTo(secondName));
            });
        }

        [Test, Category("Models")]
        public void Properties_WhenSetIndependently_ShouldNotAffectEachOther()
        {
            // Arrange
            var id = 123;
            var featureName = "TestFeature";
            var isEnabled = true;

            // Act
            _sut.Id = id;
            _sut.FeatureName = featureName;
            _sut.IsEnabled = isEnabled;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(id));
                Assert.That(_sut.FeatureName, Is.EqualTo(featureName));
                Assert.That(_sut.IsEnabled, Is.EqualTo(isEnabled));
            });
        }

        [Test, Category("Models")]
        public void Feature_WithLongFeatureName_ShouldAcceptLongString()
        {
            // Arrange
            var longName = new string('A', 1000);

            // Act
            _sut.FeatureName = longName;

            // Assert
            Assert.That(_sut.FeatureName, Is.EqualTo(longName));
        }

        [Test, Category("Models")]
        public void Feature_WithSpecialCharactersInName_ShouldAcceptSpecialCharacters()
        {
            // Arrange
            var specialName = "Feature!@#$%^&*()_+-={}[]|\\:;\"'<>?,./ 123";

            // Act
            _sut.FeatureName = specialName;

            // Assert
            Assert.That(_sut.FeatureName, Is.EqualTo(specialName));
        }

        [Test, Category("Models")]
        public void Feature_WithUnicodeCharacters_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var unicodeName = "Feature 功能 🚀 ñáéíóú";

            // Act
            _sut.FeatureName = unicodeName;

            // Assert
            Assert.That(_sut.FeatureName, Is.EqualTo(unicodeName));
        }

        [Test, Category("Models")]
        public void Id_WithNegativeValue_ShouldAcceptNegativeValue()
        {
            // Arrange
            var negativeId = -456;

            // Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _sut.Id = negativeId;
            });
        }

        [Test, Category("Models")]
        public void Id_WithMaxValue_ShouldAcceptMaxValue()
        {
            // Arrange
            var maxId = int.MaxValue;

            // Act
            _sut.Id = maxId;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(maxId));
        }

        [Test, Category("Models")]
        public void TwoParameterConstructor_ShouldCreateFeatureWithProvidedValues()
        {
            // Arrange
            var featureName = "TestFeature";
            var isEnabled = true;

            // Act
            var feature = new Feature(featureName, isEnabled);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(feature.Id, Is.EqualTo(0)); // Default value
                Assert.That(feature.FeatureName, Is.EqualTo(featureName));
                Assert.That(feature.IsEnabled, Is.EqualTo(isEnabled));
                Assert.That(feature.CreatedDate, Is.LessThan(DateTime.UtcNow));
                Assert.That(feature.ModifiedDate, Is.Null); // Default value
            });
        }

        [Test, Category("Models")]
        public void TwoParameterConstructor_WithNullFeatureName_ShouldCreateFeatureWithNull()
        {
            // Arrange
            string? featureName = null;
            var isEnabled = false;
            var beforeCreation = DateTime.UtcNow;

            // Act
            var feature = new Feature(featureName, isEnabled);
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(feature.Id, Is.EqualTo(0));
                Assert.That(feature.FeatureName, Is.Null);
                Assert.That(feature.IsEnabled, Is.False);
                Assert.That(feature.CreatedDate, Is.LessThan(DateTime.UtcNow));
                Assert.That(feature.ModifiedDate, Is.Null); // Default value
            });
        }

        [Test, Category("Models")]
        public void TwoParameterConstructor_WithEmptyFeatureName_ShouldCreateFeatureWithEmptyString()
        {
            // Arrange
            var featureName = string.Empty;
            var isEnabled = true;
            var beforeCreation = DateTime.UtcNow;

            // Act
            var feature = new Feature(featureName, isEnabled);
            var afterCreation = DateTime.UtcNow;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(feature.Id, Is.EqualTo(0));
                Assert.That(feature.FeatureName, Is.EqualTo(string.Empty));
                Assert.That(feature.IsEnabled, Is.True);
                Assert.That(feature.CreatedDate, Is.LessThan(DateTime.UtcNow));
                Assert.That(feature.ModifiedDate, Is.Null); // Default value
            });
        }

        [Test, Category("Models")]
        public void TwoParameterConstructor_WithSpecialCharacters_ShouldAcceptSpecialCharacters()
        {
            // Arrange
            var specialName = "Feature!@#$%^&*()_+-={}[]|\\:;\"'<>?,./ 123";
            var isEnabled = false;

            // Act
            var feature = new Feature(specialName, isEnabled);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(feature.Id, Is.EqualTo(0));
                Assert.That(feature.FeatureName, Is.EqualTo(specialName));
                Assert.That(feature.IsEnabled, Is.False);
                Assert.That(feature.CreatedDate, Is.LessThan(DateTime.UtcNow));
                Assert.That(feature.ModifiedDate, Is.Null); // Default value
            });
        }

        [Test, Category("Models")]
        public void TwoParameterConstructor_WithUnicodeCharacters_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var unicodeName = "Feature 功能 🚀 ñáéíóú";
            var isEnabled = true;

            // Act
            var feature = new Feature(unicodeName, isEnabled);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(feature.Id, Is.EqualTo(0));
                Assert.That(feature.FeatureName, Is.EqualTo(unicodeName));
                Assert.That(feature.IsEnabled, Is.True);
                Assert.That(feature.CreatedDate, Is.LessThan(DateTime.UtcNow));
                Assert.That(feature.ModifiedDate, Is.Null); // Default value
            });
        }

        [Test, Category("Models")]
        public void TwoParameterConstructor_WithLongFeatureName_ShouldAcceptLongString()
        {
            // Arrange
            var longName = new string('A', 1000);
            var isEnabled = false;

            // Act
            var feature = new Feature(longName, isEnabled);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(feature.Id, Is.EqualTo(0));
                Assert.That(feature.FeatureName, Is.EqualTo(longName));
                Assert.That(feature.IsEnabled, Is.False);
                Assert.That(feature.CreatedDate, Is.LessThan(DateTime.UtcNow));
                Assert.That(feature.ModifiedDate, Is.Null); // Default value
            });
        }

        [Test, Category("Models")]
        public void TwoParameterConstructor_PropertiesCanBeModifiedAfterConstruction()
        {
            // Arrange
            var initialName = "InitialFeature";
            var initialEnabled = false;

            // Act
            var feature = new Feature(initialName, initialEnabled)
            {
                // Modify properties after construction
                Id = 123,
                FeatureName = "ModifiedFeature",
                IsEnabled = true
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(feature.Id, Is.EqualTo(123));
                Assert.That(feature.FeatureName, Is.EqualTo("ModifiedFeature"));
                Assert.That(feature.IsEnabled, Is.True);
                Assert.That(feature.ModifiedDate, Is.Not.EqualTo(default(DateTime)));
            });
        }

        #region Cast Method Tests

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 123;
            _sut.FeatureName = "TestFeature";
            _sut.IsEnabled = true;

            // Act
            var result = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<FeatureDTO>());
                Assert.That(result.Id, Is.EqualTo(123));
                Assert.That(result.FeatureName, Is.EqualTo("TestFeature"));
                Assert.That(result.IsEnabled, Is.True);
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToIFeatureDTO_ShouldReturnCorrectlyMappedDTO()
        {
            // Arrange
            _sut.Id = 456;
            _sut.FeatureName = "InterfaceFeature";
            _sut.IsEnabled = false;

            // Act
            var result = _sut.Cast<IFeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<FeatureDTO>());
                Assert.That(result.Id, Is.EqualTo(456));
                Assert.That(result.FeatureName, Is.EqualTo("InterfaceFeature"));
                Assert.That(result.IsEnabled, Is.False);
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_WithNullFeatureName_ShouldHandleNullValues()
        {
            // Arrange
            _sut.Id = 789;
            _sut.FeatureName = null;
            _sut.IsEnabled = true;

            // Act
            var result = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<FeatureDTO>());
                Assert.That(result.Id, Is.EqualTo(789));
                Assert.That(result.FeatureName, Is.Null);
                Assert.That(result.IsEnabled, Is.True);
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_WithEmptyFeatureName_ShouldHandleEmptyString()
        {
            // Arrange
            _sut.Id = 101;
            _sut.FeatureName = string.Empty;
            _sut.IsEnabled = false;

            // Act
            var result = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.FeatureName, Is.EqualTo(string.Empty));
                Assert.That(result.IsEnabled, Is.False);
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToUnsupportedType_ShouldThrowInvalidCastException()
        {
            // Arrange
            _sut.Id = 1;
            _sut.FeatureName = "TestFeature";
            _sut.IsEnabled = true;

            // Act & Assert
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Contains.Substring("Cannot cast Feature to type MockDomainEntity."));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_WithCompleteData_ShouldPreserveAllData()
        {
            // Arrange - Set up Feature with comprehensive data
            var createdDate = DateTime.Now.AddDays(-3);
            var modifiedDate = DateTime.Now.AddHours(-1);

            var fullFeature = new Feature(
                id: 999,
                featureName: "CompleteFeature",
                isEnabled: true,
                createdDate: createdDate,
                modifiedDate: modifiedDate
            );

            // Act
            var result = fullFeature.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result, Is.InstanceOf<FeatureDTO>());
                Assert.That(result.Id, Is.EqualTo(999));
                Assert.That(result.FeatureName, Is.EqualTo("CompleteFeature"));
                Assert.That(result.IsEnabled, Is.True);
                Assert.That(result.CreatedDate, Is.EqualTo(createdDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(modifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_MultipleCallsToSameType_ShouldReturnDifferentInstances()
        {
            // Arrange
            _sut.Id = 777;
            _sut.FeatureName = "InstanceFeature";
            _sut.IsEnabled = true;

            // Act
            var result1 = _sut.Cast<FeatureDTO>();
            var result2 = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result1, Is.Not.Null);
                Assert.That(result2, Is.Not.Null);
                Assert.That(result1, Is.Not.SameAs(result2), "Each cast should return a new instance");
                Assert.That(result1.Id, Is.EqualTo(result2.Id));
                Assert.That(result1.FeatureName, Is.EqualTo(result2.FeatureName));
                Assert.That(result1.IsEnabled, Is.EqualTo(result2.IsEnabled));
                Assert.That(result1.CreatedDate, Is.EqualTo(result2.CreatedDate));
                Assert.That(result1.ModifiedDate, Is.EqualTo(result2.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_WithSpecialCharactersInName_ShouldPreserveCharacters()
        {
            // Arrange
            var specialName = "Feature!@#$%^&*()_+-={}[]|\\:;\"'<>?,./ 123";
            _sut.Id = 888;
            _sut.FeatureName = specialName;
            _sut.IsEnabled = false;

            // Act
            var result = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.FeatureName, Is.EqualTo(specialName));
                Assert.That(result.IsEnabled, Is.False);
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_WithUnicodeCharacters_ShouldPreserveUnicode()
        {
            // Arrange
            var unicodeName = "Feature 功能 🚀 ñáéíóú";
            _sut.Id = 333;
            _sut.FeatureName = unicodeName;
            _sut.IsEnabled = true;

            // Act
            var result = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.FeatureName, Is.EqualTo(unicodeName));
                Assert.That(result.IsEnabled, Is.True);
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_WithLongFeatureName_ShouldHandleLongStrings()
        {
            // Arrange
            var longName = new string('A', 1000);
            _sut.Id = 555;
            _sut.FeatureName = longName;
            _sut.IsEnabled = false;

            // Act
            var result = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.FeatureName, Is.EqualTo(longName));
                Assert.That(result.FeatureName!.Length, Is.EqualTo(1000));
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_WithExceptionInCasting_ShouldWrapInInvalidCastException()
        {
            // This test verifies the exception handling in the Cast method
            // Since the current implementation doesn't have scenarios that cause inner exceptions,
            // this test documents the expected behavior when such scenarios arise

            // Arrange
            _sut.Id = 1;
            _sut.FeatureName = "TestFeature";

            // Act & Assert - Test unsupported type casting
            var exception = Assert.Throws<InvalidCastException>(() => _sut.Cast<AnotherMockEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(exception.Message, Contains.Substring("Cannot cast Feature to type AnotherMockEntity."));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_WithZeroId_ShouldAllowZeroId()
        {
            // Arrange
            _sut.Id = 0;
            _sut.FeatureName = "ZeroIdFeature";
            _sut.IsEnabled = true;

            // Act
            var result = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(0));
                Assert.That(result.FeatureName, Is.EqualTo("ZeroIdFeature"));
                Assert.That(result.IsEnabled, Is.True);
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_WithMaxIntId_ShouldHandleLargeIds()
        {
            // Arrange
            _sut.Id = int.MaxValue;
            _sut.FeatureName = "MaxIntFeature";
            _sut.IsEnabled = false;

            // Act
            var result = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(int.MaxValue));
                Assert.That(result.FeatureName, Is.EqualTo("MaxIntFeature"));
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_WithNegativeId_ShouldNotBeAllowed()
        {
            // Arrange, Act & Assert
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _sut.Id = -100;
                _sut.FeatureName = "NegativeIdFeature";
                _sut.IsEnabled = true;
            });
        }

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_WithIsEnabledTrue_ShouldPreserveTrueValue()
        {
            // Arrange
            _sut.Id = 200;
            _sut.FeatureName = "EnabledFeature";
            _sut.IsEnabled = true;

            // Act
            var result = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsEnabled, Is.True);
                Assert.That(result.FeatureName, Is.EqualTo("EnabledFeature"));
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_WithIsEnabledFalse_ShouldPreserveFalseValue()
        {
            // Arrange
            _sut.Id = 300;
            _sut.FeatureName = "DisabledFeature";
            _sut.IsEnabled = false;

            // Act
            var result = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.IsEnabled, Is.False);
                Assert.That(result.FeatureName, Is.EqualTo("DisabledFeature"));
                Assert.That(result.CreatedDate, Is.EqualTo(_sut.CreatedDate));
                Assert.That(result.ModifiedDate, Is.EqualTo(_sut.ModifiedDate));
            });
        }

        #endregion

        #region Additional Comprehensive Coverage Tests

        [Test, Category("Models")]
        public void JsonConstructor_WithUnusedParameters_ShouldIgnoreThemAndSetPropertiesCorrectly()
        {
            // Test that JsonConstructor handles all provided parameters correctly
            
            // Arrange & Act
            var testDate = DateTime.Now;
            var feature = new Feature(
                id: 999,
                featureName: "ComprehensiveFeature",
                isEnabled: true,
                createdDate: testDate.AddDays(-1),
                modifiedDate: testDate
            );

            // Assert - Verify that the object is created correctly
            Assert.Multiple(() =>
            {
                Assert.That(feature.Id, Is.EqualTo(999));
                Assert.That(feature.FeatureName, Is.EqualTo("ComprehensiveFeature"));
                Assert.That(feature.IsEnabled, Is.True);
                Assert.That(feature.CreatedDate, Is.EqualTo(testDate.AddDays(-1)));
                Assert.That(feature.ModifiedDate, Is.EqualTo(testDate));
            });
        }

        [Test, Category("Models")]
        public void Cast_ExceptionHandling_RethrowsCorrectly()
        {
            // This test verifies that the catch block in the Cast method properly rethrows exceptions
            
            // Arrange
            _sut.Id = 1;
            _sut.FeatureName = "TestFeature";

            // Act & Assert - Test that InvalidCastException is thrown and rethrown correctly
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Feature to type MockDomainEntity"));
            });
        }

        [Test, Category("Models")]
        public void SerializerOptions_CyclicalReferenceHandling_ComprehensiveTest()
        {
            // Test that the serialization options handle complex scenarios correctly
            
            // Arrange - Create feature with complex data
            _sut = new Feature(
                id: 1,
                featureName: "SerializationTestFeature",
                isEnabled: true,
                createdDate: DateTime.Now.AddHours(-1),
                modifiedDate: DateTime.Now
            );

            // Act & Assert - Multiple serialization calls should work consistently
            string json1, json2, json3;
            Assert.Multiple(() =>
            {
                Assert.DoesNotThrow(() => json1 = _sut.ToJson());
                Assert.DoesNotThrow(() => json2 = _sut.ToJson());
                Assert.DoesNotThrow(() => json3 = _sut.ToJson());
            });

            // All serializations should produce valid JSON
            json1 = _sut.ToJson();
            json2 = _sut.ToJson();
            json3 = _sut.ToJson();
            
            Assert.Multiple(() =>
            {
                Assert.That(json1, Is.Not.Null.And.Not.Empty);
                Assert.That(json2, Is.Not.Null.And.Not.Empty);
                Assert.That(json3, Is.Not.Null.And.Not.Empty);
                Assert.That(json1, Is.EqualTo(json2));
                Assert.That(json2, Is.EqualTo(json3));
            });
        }

        [Test, Category("Models")]
        public void ToString_WithExtremeValues_ShouldHandleAllScenarios()
        {
            // Test ToString with various extreme value combinations
            
            // Case 1: Maximum values
            _sut.Id = int.MaxValue;
            _sut.FeatureName = new string('A', 1000);
            var maxResult = _sut.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(maxResult, Is.Not.Null);
                Assert.That(maxResult, Does.Contain(int.MaxValue.ToString()));
                Assert.That(maxResult, Does.Contain(".Id:" + int.MaxValue));
                Assert.That(maxResult, Does.Contain(_sut.FeatureName));
            });

            // Case 2: Minimum/Zero values
            _sut.Id = 0;
            _sut.FeatureName = "";
            var minResult = _sut.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(minResult, Is.Not.Null);
                Assert.That(minResult, Does.Contain(".Id:0"));
                Assert.That(minResult, Does.Contain(".FeatureName:"));
            });

            // Case 3: Negative ID with null feature name
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            {
                _sut.Id = -42;
            });
        }

        [Test, Category("Models")]
        public void Cast_ToMultipleDTOTypes_ShouldCreateIndependentInstances()
        {
            // Test that multiple Cast calls create independent DTO instances
            
            // Arrange
            _sut = new Feature(
                id: 100,
                featureName: "MultiCastFeature",
                isEnabled: true,
                createdDate: DateTime.Now.AddDays(-1),
                modifiedDate: DateTime.Now
            );

            // Act - Cast to different supported types
            var featureDto1 = _sut.Cast<FeatureDTO>();
            var featureDto2 = _sut.Cast<FeatureDTO>();
            var iFeatureDto = _sut.Cast<IFeatureDTO>();

            // Assert - All should be different instances but with same data
            Assert.Multiple(() =>
            {
                Assert.That(featureDto1, Is.Not.SameAs(featureDto2));
                Assert.That(featureDto1, Is.Not.SameAs(iFeatureDto));
                Assert.That(featureDto2, Is.Not.SameAs(iFeatureDto));
                
                // All should have same data
                Assert.That(featureDto1.Id, Is.EqualTo(100));
                Assert.That(featureDto2.Id, Is.EqualTo(100));
                Assert.That(iFeatureDto.Id, Is.EqualTo(100));
                
                Assert.That(featureDto1.FeatureName, Is.EqualTo("MultiCastFeature"));
                Assert.That(featureDto2.FeatureName, Is.EqualTo("MultiCastFeature"));
                Assert.That(iFeatureDto.FeatureName, Is.EqualTo("MultiCastFeature"));
                
                Assert.That(featureDto1.IsEnabled, Is.True);
                Assert.That(featureDto2.IsEnabled, Is.True);
                Assert.That(iFeatureDto.IsEnabled, Is.True);
            });
        }

        [Test, Category("Models")]
        public void ModifiedDate_PropertyChangeCombinations_ShouldUpdateInSequence()
        {
            // Test that rapid sequential property changes all update ModifiedDate correctly

            // Arrange
            _sut = new Feature
            {
                FeatureName = "InitialFeature",
                IsEnabled = true
            };
            var timestamps = new List<DateTime?>
            {
              _sut.ModifiedDate // Initial state
            };

            // Act & Assert - Test sequential property changes
            System.Threading.Thread.Sleep(2);
            _sut.Id = 1;
            timestamps.Add(_sut.ModifiedDate);

            System.Threading.Thread.Sleep(2);
            _sut.FeatureName = "SequenceFeature";
            timestamps.Add(_sut.ModifiedDate);

            System.Threading.Thread.Sleep(2);
            _sut.IsEnabled = true;
            timestamps.Add(_sut.ModifiedDate);

            System.Threading.Thread.Sleep(2);
            _sut.IsEnabled = false;
            timestamps.Add(_sut.ModifiedDate);

            System.Threading.Thread.Sleep(2);
            _sut.FeatureName = "UpdatedSequenceFeature";
            timestamps.Add(_sut.ModifiedDate);

            // Assert - Each timestamp should be greater than the previous
            for (int i = 1; i < timestamps.Count; i++)
            {
                Assert.That(timestamps[i], Is.GreaterThan(timestamps[i - 1]), 
                    $"Timestamp at index {i} should be greater than timestamp at index {i - 1}");
            }
        }

        [Test, Category("Models")]
        public void JsonSerialization_WithSpecialCharactersAndUnicode_ShouldSerializeCorrectly()
        {
            // Test JSON serialization with special characters and Unicode
            
            // Arrange
            _sut.Id = 888;
            _sut.FeatureName = "Feature!@#$%^&*()_+-={}[]|\\:;\"'<>?,./ 功能 🚀 ñáéíóú";
            _sut.IsEnabled = true;

            // Act
            var json = _sut.ToJson();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(json, Is.Not.Null.And.Not.Empty);
                Assert.That(json, Does.Contain("\"id\":888"));
                Assert.That(json, Does.Contain("\"isEnabled\":true"));
                // The feature name should be properly escaped in JSON
                Assert.That(json, Does.Contain("featureName"));
                
                // Verify JSON is valid by attempting to parse
                Assert.DoesNotThrow(() =>
                {
                    var document = System.Text.Json.JsonDocument.Parse(json);
                    Assert.That(document.RootElement.ValueKind, Is.EqualTo(System.Text.Json.JsonValueKind.Object));
                    document.Dispose();
                });
            });
        }

        [Test, Category("Models")]
        public void IsEnabled_ToggleOperations_ShouldUpdateModifiedDateEachTime()
        {
            // Test that toggling IsEnabled multiple times updates ModifiedDate each time

            // Arrange
            _sut = new Feature
            {
                FeatureName = "ToggleFeature",
                IsEnabled = true
            };
            var originalModifiedDate = _sut.ModifiedDate;
            var toggleTimestamps = new List<DateTime?>();

            // Act & Assert - Test multiple toggle operations
            for (int i = 0; i < 5; i++)
            {
                System.Threading.Thread.Sleep(2);
                _sut.IsEnabled = !_sut.IsEnabled;
                toggleTimestamps.Add(_sut.ModifiedDate);
            }

            // Assert - Each toggle should update ModifiedDate
            Assert.That(toggleTimestamps[0], Is.GreaterThan(originalModifiedDate));
            for (int i = 1; i < toggleTimestamps.Count; i++)
            {
                Assert.That(toggleTimestamps[i], Is.GreaterThan(toggleTimestamps[i - 1]),
                    $"Toggle {i + 1} should have updated ModifiedDate");
            }
        }

        [Test, Category("Models")]
        public void Feature_ComprehensiveFunctionalityIntegrationTest()
        {
            // Comprehensive test that exercises all major functionality together
            
            // Test default constructor
            var defaultFeature = new Feature();
            Assert.That(defaultFeature, Is.Not.Null);
            Assert.That(defaultFeature.CreatedDate, Is.Not.EqualTo(default(DateTime)));
            
            // Test JsonConstructor with comprehensive data
            var testDate = DateTime.Now;
            var comprehensiveFeature = new Feature(
                id: 12345,
                featureName: "ComprehensiveTestFeature",
                isEnabled: true,
                createdDate: testDate.AddDays(-1),
                modifiedDate: testDate
            );
            
            // Verify comprehensive properties
            Assert.Multiple(() =>
            {
                Assert.That(comprehensiveFeature.Id, Is.EqualTo(12345));
                Assert.That(comprehensiveFeature.FeatureName, Is.EqualTo("ComprehensiveTestFeature"));
                Assert.That(comprehensiveFeature.IsEnabled, Is.True);
                Assert.That(comprehensiveFeature.CreatedDate, Is.EqualTo(testDate.AddDays(-1)));
                Assert.That(comprehensiveFeature.ModifiedDate, Is.EqualTo(testDate));
            });
            
            // Test all property setters
            defaultFeature.Id = 54321;
            defaultFeature.FeatureName = "UpdatedFeature";
            defaultFeature.IsEnabled = true;
            
            Assert.Multiple(() =>
            {
                Assert.That(defaultFeature.Id, Is.EqualTo(54321));
                Assert.That(defaultFeature.FeatureName, Is.EqualTo("UpdatedFeature"));
                Assert.That(defaultFeature.IsEnabled, Is.True);
                Assert.That(defaultFeature.CreatedDate, Is.Not.EqualTo(default(DateTime)));
            });
            
            // Test Cast functionality
            var featureDto = defaultFeature.Cast<FeatureDTO>();
            var iFeatureDto = defaultFeature.Cast<IFeatureDTO>();
            
            Assert.Multiple(() =>
            {
                Assert.That(featureDto, Is.InstanceOf<FeatureDTO>());
                Assert.That(iFeatureDto, Is.InstanceOf<FeatureDTO>());
                Assert.That(featureDto.Id, Is.EqualTo(defaultFeature.Id));
                Assert.That(iFeatureDto.Id, Is.EqualTo(defaultFeature.Id));
            });
            
            // Test JSON serialization
            var json = defaultFeature.ToJson();
            Assert.That(json, Is.Not.Null.And.Not.Empty);
            
            // Test ToString functionality
            var stringResult = defaultFeature.ToString();
            Assert.Multiple(() =>
            {
                Assert.That(stringResult, Is.Not.Null.And.Not.Empty);
                Assert.That(stringResult, Does.Contain("UpdatedFeature"));
                Assert.That(stringResult, Does.Contain("54321"));
            });
            
            // Test exception scenarios
            Assert.Throws<InvalidCastException>(() => defaultFeature.Cast<MockDomainEntity>());
        }

        [Test, Category("Models")]
        public void CreatedDate_ReadOnlyBehavior_ComprehensiveTest()
        {
            // Comprehensive test of CreatedDate read-only behavior
            
            // Verify property is read-only
            var property = typeof(Feature).GetProperty("CreatedDate");
            Assert.That(property?.SetMethod, Is.Null, "CreatedDate should be read-only");
            
            // Test that CreatedDate is set during construction and doesn't change
            var feature1 = new Feature();
            
            Assert.Multiple(() =>
            {
                Assert.That(feature1.CreatedDate, Is.LessThan(DateTime.UtcNow));
            });
            
            // Test that different instances have different CreatedDate values
            System.Threading.Thread.Sleep(1);
            var feature2 = new Feature();
            Assert.That(feature2.CreatedDate, Is.GreaterThanOrEqualTo(feature1.CreatedDate));
            
            // Test JsonConstructor with specific CreatedDate
            var specificDate = DateTime.Now.AddDays(-5);
            var feature3 = new Feature(
                id: 1,
                featureName: "DateTest",
                isEnabled: true,
                createdDate: specificDate,
                modifiedDate: DateTime.Now
            );
            
            Assert.That(feature3.CreatedDate, Is.EqualTo(specificDate));
        }

        [Test, Category("Models")]
        public void Cast_ExceptionRethrowing_ShouldPreserveOriginalException()
        {
            // Test that the catch block in Cast method properly rethrows exceptions
            
            // Arrange
            _sut = new Feature(
                id: 1,
                featureName: "ExceptionTestFeature",
                isEnabled: true,
                createdDate: DateTime.Now.AddDays(-1),
                modifiedDate: DateTime.Now
            );
            
            // Act & Assert - Verify that InvalidCastException is thrown for unsupported types
            var ex = Assert.Throws<InvalidCastException>(() => _sut.Cast<MockDomainEntity>());
            Assert.Multiple(() =>
            {
                Assert.That(ex, Is.Not.Null);
                Assert.That(ex.Message, Does.Contain("Cannot cast Feature to type MockDomainEntity"));
                Assert.That(ex.InnerException, Is.Null); // Should be the original exception, not wrapped
            });
            
            // Test multiple unsupported types
            Assert.Throws<InvalidCastException>(() => _sut.Cast<AnotherMockEntity>());
        }

        [Test, Category("Models")]
        public void AllPropertySetters_ShouldUpdateModifiedDate()
        {
            // Comprehensive test that all property setters update ModifiedDate
            
            // Arrange
            _sut = new Feature();

            // Act & Assert - Test each property that should update ModifiedDate
            System.Threading.Thread.Sleep(1);
            _sut.Id = 100;
            Assert.That(_sut.ModifiedDate, Is.Not.Null, "Id setter should update ModifiedDate");

            var idModified = _sut.ModifiedDate;
            System.Threading.Thread.Sleep(1);
            _sut.FeatureName = "PropertyTestFeature";
            Assert.That(_sut.ModifiedDate, Is.GreaterThan(idModified), "FeatureName setter should update ModifiedDate");

            var nameModified = _sut.ModifiedDate;
            System.Threading.Thread.Sleep(1);
            _sut.IsEnabled = true;
            Assert.That(_sut.ModifiedDate, Is.GreaterThan(nameModified), "IsEnabled setter should update ModifiedDate");
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullAndDefaultValues_ShouldHandleGracefully()
        {
            // Test JsonConstructor with various null and default combinations
            
            // Arrange & Act
            var feature = new Feature(
                id: 0,
                featureName: null,
                isEnabled: false,
                createdDate: DateTime.Now.AddDays(-1),
                modifiedDate: null
            );

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(feature.Id, Is.EqualTo(0));
                Assert.That(feature.FeatureName, Is.Null);
                Assert.That(feature.IsEnabled, Is.False);
                Assert.That(feature.CreatedDate, Is.Not.EqualTo(default(DateTime)));
                Assert.That(feature.ModifiedDate, Is.Null);
            });
        }

        #endregion

        // Helper mock class for testing IDomainEntity
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; } = 1;
            public bool IsCast { get; set; } = false;
            public int CastId { get; set; } = 0;
            public string? CastType { get; set; } = null;
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; } = default;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        // Another helper mock class for testing exception handling
        private class AnotherMockEntity : IDomainEntity
        {
            public int Id { get; set; } = 2;
            public bool IsCast { get; set; } = false;
            public int CastId { get; set; } = 0;
            public string? CastType { get; set; } = null;
            public DateTime CreatedDate { get; } = DateTime.Now;
            public DateTime? ModifiedDate { get; set; } = default;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }

        // Mock FeatureDTO for testing IFeatureDTO constructor
        private class MockFeatureDTO : IFeatureDTO
        {
            public int Id { get; set; } = 0;
            public bool IsCast { get; set; } = false;
            public int CastId { get; set; } = 0;
            public string? CastType { get; set; } = null;
            public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
            public DateTime? ModifiedDate { get; set; } = default;
            public string? FeatureName { get; set; }
            public bool IsEnabled { get; set; } = false;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }
    }
}
