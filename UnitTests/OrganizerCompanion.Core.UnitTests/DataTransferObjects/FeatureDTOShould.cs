using System.ComponentModel.DataAnnotations;
using System.Reflection;
using NUnit.Framework;
using OrganizerCompanion.Core.Models.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.DataTransferObject;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.UnitTests.DataTransferObjects
{
    [TestFixture]
    internal class FeatureDTOShould
    {
        private FeatureDTO _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new FeatureDTO();
        }

        [Test, Category("DataTransferObjects")]
        public void DefaultConstructor_ShouldCreateFeatureDTOWithDefaultValues()
        {
            // Arrange & Act
            _sut = new FeatureDTO();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.FeatureName, Is.Null);
                Assert.That(_sut.IsEnabled, Is.False);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldGetAndSetValue()
        {
            // Arrange
            int expectedId = 123;

            // Act
            _sut.Id = expectedId;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(expectedId));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptZeroValue()
        {
            // Arrange & Act
            _sut.Id = 0;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(0));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptPositiveValues()
        {
            // Arrange
            int[] testValues = { 1, 100, 999, int.MaxValue };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (int value in testValues)
                {
                    _sut.Id = value;
                    Assert.That(_sut.Id, Is.EqualTo(value));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldAcceptNegativeValues()
        {
            // Arrange
            int negativeValue = -123;

            // Act
            _sut.Id = negativeValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(negativeValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldSupportMinValue()
        {
            // Arrange & Act
            _sut.Id = int.MinValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldSupportMaxValue()
        {
            // Arrange & Act
            _sut.Id = int.MaxValue;

            // Assert
            Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureName_ShouldGetAndSetValue()
        {
            // Arrange
            string expectedFeatureName = "Advanced Search";

            // Act
            _sut.FeatureName = expectedFeatureName;

            // Assert
            Assert.That(_sut.FeatureName, Is.EqualTo(expectedFeatureName));
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureName_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.FeatureName = null;

            // Assert
            Assert.That(_sut.FeatureName, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureName_ShouldAcceptEmptyString()
        {
            // Arrange & Act
            _sut.FeatureName = "";

            // Assert
            Assert.That(_sut.FeatureName, Is.EqualTo(""));
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureName_ShouldAcceptVariousStringFormats()
        {
            // Arrange
            var featureNames = new[]
            {
                "Simple Feature",
                "Feature-With-Dashes",
                "Feature_With_Underscores",
                "Feature123",
                "UPPERCASE FEATURE",
                "lowercase feature",
                "Feature with special chars !@#$%",
                "Very Long Feature Name That Spans Multiple Words And Contains Numbers 123 And Special Characters !@#"
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var featureName in featureNames)
                {
                    _sut.FeatureName = featureName;
                    Assert.That(_sut.FeatureName, Is.EqualTo(featureName));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureName_ShouldAcceptWhitespace()
        {
            // Arrange
            var whitespaceFeatureName = " Feature Name ";

            // Act
            _sut.FeatureName = whitespaceFeatureName;

            // Assert
            Assert.That(_sut.FeatureName, Is.EqualTo(whitespaceFeatureName));
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureName_ShouldAcceptLongString()
        {
            // Arrange
            var longFeatureName = new string('A', 1000);

            // Act
            _sut.FeatureName = longFeatureName;

            // Assert
            Assert.That(_sut.FeatureName, Is.EqualTo(longFeatureName));
        }

        [Test, Category("DataTransferObjects")]
        public void IsEnabled_ShouldGetAndSetTrueValue()
        {
            // Arrange & Act
            _sut.IsEnabled = true;

            // Assert
            Assert.That(_sut.IsEnabled, Is.True);
        }

        [Test, Category("DataTransferObjects")]
        public void IsEnabled_ShouldGetAndSetFalseValue()
        {
            // Arrange & Act
            _sut.IsEnabled = false;

            // Assert
            Assert.That(_sut.IsEnabled, Is.False);
        }

        [Test, Category("DataTransferObjects")]
        public void IsEnabled_ShouldToggleBetweenValues()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                // Start with false (default)
                Assert.That(_sut.IsEnabled, Is.False);

                // Set to true
                _sut.IsEnabled = true;
                Assert.That(_sut.IsEnabled, Is.True);

                // Set back to false
                _sut.IsEnabled = false;
                Assert.That(_sut.IsEnabled, Is.False);

                // Set to true again
                _sut.IsEnabled = true;
                Assert.That(_sut.IsEnabled, Is.True);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.IsCast; });
        }

        [Test, Category("DataTransferObjects")]
        public void IsCast_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.IsCast = true; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastId; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastId_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.CastId = 123; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.CastType; });
        }

        [Test, Category("DataTransferObjects")]
        public void CastType_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.CastType = "TestType"; });
        }

        [Test, Category("DataTransferObjects")]
        public void DateCreated_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.DateCreated; });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_Get_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { var _ = _sut.DateModified; });
        }

        [Test, Category("DataTransferObjects")]
        public void DateModified_Set_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.DateModified = DateTime.Now; });
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.Cast<MockDomainEntity>(); });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldThrowNotImplementedException()
        {
            // Arrange & Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.ToJson(); });
        }

        [Test, Category("DataTransferObjects")]
        public void Id_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(FeatureDTO).GetProperty(nameof(FeatureDTO.Id));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureName_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(FeatureDTO).GetProperty(nameof(FeatureDTO.FeatureName));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void IsEnabled_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(FeatureDTO).GetProperty(nameof(FeatureDTO.IsEnabled));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureDTO_ShouldImplementIFeatureDTO()
        {
            // Arrange & Act
            var featureDTO = new FeatureDTO();

            // Assert
            Assert.That(featureDTO, Is.InstanceOf<IFeatureDTO>());
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureDTO_ShouldImplementIDomainEntity()
        {
            // Arrange & Act
            var featureDTO = new FeatureDTO();

            // Assert
            Assert.That(featureDTO, Is.InstanceOf<IDomainEntity>());
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureDTO_Properties_ShouldBeSettableInChain()
        {
            // Arrange & Act
            var featureDTO = new FeatureDTO
            {
                Id = 999,
                FeatureName = "Chained Feature",
                IsEnabled = true
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(featureDTO.Id, Is.EqualTo(999));
                Assert.That(featureDTO.FeatureName, Is.EqualTo("Chained Feature"));
                Assert.That(featureDTO.IsEnabled, Is.True);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void JsonPropertyName_Attributes_ShouldBePresent()
        {
            // Arrange
            var properties = new Dictionary<string, string>
            {
                { nameof(FeatureDTO.Id), "id" },
                { nameof(FeatureDTO.FeatureName), "featureName" },
                { nameof(FeatureDTO.IsEnabled), "isEnabled" }
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var kvp in properties)
                {
                    var property = typeof(FeatureDTO).GetProperty(kvp.Key);
                    var jsonAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonPropertyNameAttribute>();
                    Assert.That(jsonAttribute, Is.Not.Null, $"Property {kvp.Key} should have JsonPropertyName attribute");
                    Assert.That(jsonAttribute?.Name, Is.EqualTo(kvp.Value), $"Property {kvp.Key} should have JsonPropertyName '{kvp.Value}'");
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Interface_Properties_ShouldHaveJsonIgnoreAttribute()
        {
            // Arrange
            var properties = new[]
            {
                nameof(FeatureDTO.IsCast),
                nameof(FeatureDTO.CastId),
                nameof(FeatureDTO.CastType),
                nameof(FeatureDTO.DateCreated),
                nameof(FeatureDTO.DateModified)
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var propertyName in properties)
                {
                    var property = typeof(FeatureDTO).GetProperty(propertyName);
                    var jsonIgnoreAttribute = property?.GetCustomAttribute<System.Text.Json.Serialization.JsonIgnoreAttribute>();
                    Assert.That(jsonIgnoreAttribute, Is.Not.Null, $"Property {propertyName} should have JsonIgnore attribute");
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Properties_ShouldAllowMultipleReassignments()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                // Test Id reassignments
                _sut.Id = 1;
                Assert.That(_sut.Id, Is.EqualTo(1));
                _sut.Id = 2;
                Assert.That(_sut.Id, Is.EqualTo(2));

                // Test FeatureName reassignments
                _sut.FeatureName = "First Feature";
                Assert.That(_sut.FeatureName, Is.EqualTo("First Feature"));
                _sut.FeatureName = "Second Feature";
                Assert.That(_sut.FeatureName, Is.EqualTo("Second Feature"));

                // Test IsEnabled reassignments
                _sut.IsEnabled = true;
                Assert.That(_sut.IsEnabled, Is.True);
                _sut.IsEnabled = false;
                Assert.That(_sut.IsEnabled, Is.False);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureName_ShouldAcceptUnicodeCharacters()
        {
            // Arrange
            var unicodeFeatureName = "特殊功能 (Special Feature) - Función Especial - Функция";

            // Act
            _sut.FeatureName = unicodeFeatureName;

            // Assert
            Assert.That(_sut.FeatureName, Is.EqualTo(unicodeFeatureName));
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureName_ShouldAcceptOnlyWhitespaceString()
        {
            // Arrange
            var whitespaceOnlyFeatureName = "   \t   \n   ";

            // Act
            _sut.FeatureName = whitespaceOnlyFeatureName;

            // Assert
            Assert.That(_sut.FeatureName, Is.EqualTo(whitespaceOnlyFeatureName));
        }

        [Test, Category("DataTransferObjects")]
        public void IsEnabled_DefaultValue_ShouldBeFalse()
        {
            // Arrange & Act
            var newFeatureDTO = new FeatureDTO();

            // Assert
            Assert.That(newFeatureDTO.IsEnabled, Is.False);
        }

        [Test, Category("DataTransferObjects")]
        public void AllProperties_ShouldBeIndependentOfEachOther()
        {
            // Arrange & Act
            _sut.Id = 100;
            _sut.FeatureName = "Test Feature";
            _sut.IsEnabled = true;

            // Change one property and verify others remain unchanged
            _sut.Id = 200;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(200));
                Assert.That(_sut.FeatureName, Is.EqualTo("Test Feature"));
                Assert.That(_sut.IsEnabled, Is.True);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void BooleanProperty_ShouldSupportBooleanLogic()
        {
            // Arrange & Act & Assert
            Assert.Multiple(() =>
            {
                // Test logical operations
                _sut.IsEnabled = true;
                Assert.That(_sut.IsEnabled && true, Is.True);
                Assert.That(_sut.IsEnabled || false, Is.True);
                Assert.That(!_sut.IsEnabled, Is.False);

                _sut.IsEnabled = false;
                Assert.That(_sut.IsEnabled && true, Is.False);
                Assert.That(_sut.IsEnabled || false, Is.False);
                Assert.That(!_sut.IsEnabled, Is.True);
            });
        }

        #region Mock Classes
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime DateCreated { get; }
            public DateTime? DateModified { get; set; }
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => throw new NotImplementedException();
        }
        #endregion
    }
}
