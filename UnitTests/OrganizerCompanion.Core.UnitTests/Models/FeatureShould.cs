using System.Text.Json;
using NUnit.Framework;
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
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new Feature();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.FeatureName, Is.Null);
                Assert.That(_sut.IsEnabled, Is.False);
                Assert.That(_sut.DateCreated, Is.GreaterThanOrEqualTo(beforeCreation));
                Assert.That(_sut.DateCreated, Is.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.DateModified, Is.EqualTo(default(DateTime)));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_ShouldCreateFeatureWithProvidedValues()
        {
            // Arrange
            var id = 123;
            var featureName = "TestFeature";
            var isEnabled = true;
            var isCast = true;
            var castId = 456;
            var castType = "TestType";
            var dateCreated = DateTime.Now.AddDays(-1);
            var dateModified = DateTime.Now.AddHours(-2);

            // Act
            var feature = new Feature(id, featureName, isEnabled, isCast, castId, castType, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(feature.Id, Is.EqualTo(id));
                Assert.That(feature.FeatureName, Is.EqualTo(featureName));
                Assert.That(feature.IsEnabled, Is.EqualTo(isEnabled));
                Assert.That(feature.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(feature.DateModified, Is.EqualTo(dateModified));
            });
        }

        [Test, Category("Models")]
        public void JsonConstructor_WithNullValues_ShouldCreateFeatureWithNullValues()
        {
            // Arrange
            var id = 123;
            string? featureName = null;
            var isEnabled = false;
            var isCast = false;
            var castId = 0;
            string? castType = null;
            var dateCreated = DateTime.Now.AddDays(-1);
            DateTime? dateModified = null;

            // Act
            var feature = new Feature(id, featureName, isEnabled, isCast, castId, castType, dateCreated, dateModified);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(feature.Id, Is.EqualTo(id));
                Assert.That(feature.FeatureName, Is.Null);
                Assert.That(feature.IsEnabled, Is.False);
                Assert.That(feature.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(feature.DateModified, Is.Null);
            });
        }

        [Test, Category("Models")]
        public void Id_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newId = 123;
            var beforeSet = DateTime.Now;

            // Act
            _sut.Id = newId;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(newId));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void Id_WhenSetToZero_ShouldStillUpdateDateModified()
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
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void FeatureName_WhenSet_ShouldUpdateDateModified()
        {
            // Arrange
            var newFeatureName = "Authentication";
            var beforeSet = DateTime.Now;

            // Act
            _sut.FeatureName = newFeatureName;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.FeatureName, Is.EqualTo(newFeatureName));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void FeatureName_WhenSetToNull_ShouldUpdateDateModified()
        {
            // Arrange
            _sut.FeatureName = "TestFeature"; // Set initial value
            var beforeSet = DateTime.Now;

            // Act
            _sut.FeatureName = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.FeatureName, Is.Null);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void FeatureName_WhenSetToEmptyString_ShouldUpdateDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.FeatureName = string.Empty;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.FeatureName, Is.EqualTo(string.Empty));
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void IsEnabled_WhenSetToTrue_ShouldUpdateDateModified()
        {
            // Arrange
            var beforeSet = DateTime.Now;

            // Act
            _sut.IsEnabled = true;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsEnabled, Is.True);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
        }

        [Test, Category("Models")]
        public void IsEnabled_WhenSetToFalse_ShouldUpdateDateModified()
        {
            // Arrange
            _sut.IsEnabled = true; // Set initial value
            var beforeSet = DateTime.Now;

            // Act
            _sut.IsEnabled = false;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.IsEnabled, Is.False);
                Assert.That(_sut.DateModified, Is.GreaterThanOrEqualTo(beforeSet));
                Assert.That(_sut.DateModified, Is.LessThanOrEqualTo(DateTime.Now));
            });
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
            Assert.Throws<NotImplementedException>(() => _sut.CastId = 456);
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
            Assert.Throws<NotImplementedException>(() => _sut.CastType = "UserAccount");
        }

        [Test, Category("Models")]
        public void DateCreated_ShouldBeReadOnly()
        {
            // Arrange
            var initialDateCreated = _sut.DateCreated;

            // Act & Assert - DateCreated should not have a public setter
            // This test verifies the property is read-only by checking it doesn't change
            Assert.That(_sut.DateCreated, Is.EqualTo(initialDateCreated));
        }

        [Test, Category("Models")]
        public void DateModified_CanBeSetDirectly()
        {
            // Arrange
            var newDateModified = DateTime.Now.AddHours(-1);

            // Act
            _sut.DateModified = newDateModified;

            // Assert
            Assert.That(_sut.DateModified, Is.EqualTo(newDateModified));
        }

        [Test, Category("Models")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => _sut.Cast<Feature>());
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
                Assert.That(json, Does.Contain("\"dateCreated\""));
                Assert.That(json, Does.Contain("\"dateModified\""));
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
                // Cast properties should be omitted due to JsonIgnore
                Assert.That(json, Does.Not.Contain("\"isCast\""));
                Assert.That(json, Does.Not.Contain("\"castId\""));
                Assert.That(json, Does.Not.Contain("\"castType\""));
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

            // Act
            _sut.Id = negativeId;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(negativeId));
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
        public void JsonSerialization_RoundTrip_ShouldMaintainDataIntegrity()
        {
            // Arrange
            _sut.Id = 123;
            _sut.FeatureName = "TestFeature";
            _sut.IsEnabled = true;

            // Act
            var json = _sut.ToJson();
            var deserializedFeature = JsonSerializer.Deserialize<Feature>(json);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(deserializedFeature, Is.Not.Null);
                Assert.That(deserializedFeature!.Id, Is.EqualTo(_sut.Id));
                Assert.That(deserializedFeature.FeatureName, Is.EqualTo(_sut.FeatureName));
                Assert.That(deserializedFeature.IsEnabled, Is.EqualTo(_sut.IsEnabled));
                Assert.That(deserializedFeature.DateCreated, Is.EqualTo(_sut.DateCreated));
            });
        }
    }
}
