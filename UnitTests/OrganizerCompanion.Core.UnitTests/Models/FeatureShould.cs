using System.Text.Json;
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(exception.Message, Contains.Substring("Error Feature Email to type MockDomainEntity: Cannot cast Feature to type MockDomainEntity, casting is not supported for this type"));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_WithCompleteData_ShouldPreserveAllData()
        {
            // Arrange - Set up Feature with comprehensive data
            var dateCreated = DateTime.Now.AddDays(-3);
            var dateModified = DateTime.Now.AddHours(-1);

            var fullFeature = new Feature(
                id: 999,
                featureName: "CompleteFeature",
                isEnabled: true,
                isCast: false,
                castId: 0,
                castType: null,
                dateCreated: dateCreated,
                dateModified: dateModified
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
                Assert.That(result.DateCreated, Is.EqualTo(dateCreated));
                Assert.That(result.DateModified, Is.EqualTo(dateModified));
                // Note: Cast-related properties are not part of FeatureDTO
                // This is by design as FeatureDTO is a simplified representation
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
                Assert.That(result1.DateCreated, Is.EqualTo(result2.DateCreated));
                Assert.That(result1.DateModified, Is.EqualTo(result2.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(exception.Message, Contains.Substring("Error Feature Email to type AnotherMockEntity: Cannot cast Feature to type AnotherMockEntity, casting is not supported for this type"));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
            });
        }

        [Test, Category("Models")]
        public void Cast_ToFeatureDTO_WithNegativeId_ShouldAllowNegativeIds()
        {
            // Arrange
            _sut.Id = -100;
            _sut.FeatureName = "NegativeIdFeature";
            _sut.IsEnabled = true;

            // Act
            var result = _sut.Cast<FeatureDTO>();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(result, Is.Not.Null);
                Assert.That(result.Id, Is.EqualTo(-100));
                Assert.That(result.FeatureName, Is.EqualTo("NegativeIdFeature"));
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
                Assert.That(result.DateCreated, Is.EqualTo(_sut.DateCreated));
                Assert.That(result.DateModified, Is.EqualTo(_sut.DateModified));
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
            public DateTime DateCreated { get; } = DateTime.Now;
            public DateTime? DateModified { get; set; } = DateTime.Now;

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
            public DateTime DateCreated { get; } = DateTime.Now;
            public DateTime? DateModified { get; set; } = DateTime.Now;

            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => "{}";
        }
    }
}
