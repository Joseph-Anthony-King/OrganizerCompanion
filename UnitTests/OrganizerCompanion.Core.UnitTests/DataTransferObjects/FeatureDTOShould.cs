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
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            _sut = new FeatureDTO();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(0));
                Assert.That(_sut.FeatureName, Is.Null);
                Assert.That(_sut.IsEnabled, Is.False);
                Assert.That(_sut.CreatedDate, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
                Assert.That(_sut.ModifiedDate, Is.Null);
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
            // Arrange, Act & Assert
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
        public void CreatedDate_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedDate = DateTime.Now.AddDays(-1);

            // Act
            _sut.CreatedDate = expectedDate;

            // Assert
            Assert.That(_sut.CreatedDate, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_DefaultValue_ShouldBeCurrentTime()
        {
            // Arrange
            var beforeCreation = DateTime.Now;

            // Act
            var featureDTO = new FeatureDTO();
            var afterCreation = DateTime.Now;

            // Assert
            Assert.That(featureDTO.CreatedDate, Is.GreaterThanOrEqualTo(beforeCreation).And.LessThanOrEqualTo(afterCreation));
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldGetAndSetValue()
        {
            // Arrange
            var expectedDate = DateTime.Now.AddHours(-2);

            // Act
            _sut.ModifiedDate = expectedDate;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(expectedDate));
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldAcceptNullValue()
        {
            // Arrange & Act
            _sut.ModifiedDate = null;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_DefaultValue_ShouldBeNull()
        {
            // Arrange & Act
            var featureDTO = new FeatureDTO();

            // Assert
            Assert.That(featureDTO.ModifiedDate, Is.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException()
        {
            // Arrange, Act & Assert
            Assert.Throws<NotImplementedException>(() => { _sut.Cast<MockDomainEntity>(); });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldThrowNotImplementedException()
        {
            // Arrange, Act & Assert
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
        public void CreatedDate_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(FeatureDTO).GetProperty(nameof(FeatureDTO.CreatedDate));

            // Act
            var requiredAttribute = property?.GetCustomAttribute<RequiredAttribute>();

            // Assert
            Assert.That(requiredAttribute, Is.Not.Null);
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldHaveRequiredAttribute()
        {
            // Arrange
            var property = typeof(FeatureDTO).GetProperty(nameof(FeatureDTO.ModifiedDate));

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
            // Arrange
            var testDate = DateTime.Now.AddDays(-1);
            var testModifiedDate = DateTime.Now.AddHours(-2);

            // Act
            var featureDTO = new FeatureDTO
            {
                Id = 999,
                FeatureName = "Chained Feature",
                IsEnabled = true,
                CreatedDate = testDate,
                ModifiedDate = testModifiedDate
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(featureDTO.Id, Is.EqualTo(999));
                Assert.That(featureDTO.FeatureName, Is.EqualTo("Chained Feature"));
                Assert.That(featureDTO.IsEnabled, Is.True);
                Assert.That(featureDTO.CreatedDate, Is.EqualTo(testDate));
                Assert.That(featureDTO.ModifiedDate, Is.EqualTo(testModifiedDate));
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
                { nameof(FeatureDTO.IsEnabled), "isEnabled" },
                { nameof(FeatureDTO.CreatedDate), "createdDate" },
                { nameof(FeatureDTO.ModifiedDate), "modifiedDate" }
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
        public void Properties_ShouldAllowMultipleReassignments()
        {
            // Arrange, Act & Assert
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

                // Test CreatedDate reassignments
                var firstDate = DateTime.Now.AddDays(-1);
                var secondDate = DateTime.Now.AddDays(-2);
                _sut.CreatedDate = firstDate;
                Assert.That(_sut.CreatedDate, Is.EqualTo(firstDate));
                _sut.CreatedDate = secondDate;
                Assert.That(_sut.CreatedDate, Is.EqualTo(secondDate));

                // Test ModifiedDate reassignments
                var firstModified = DateTime.Now.AddHours(-1);
                var secondModified = DateTime.Now.AddHours(-2);
                _sut.ModifiedDate = firstModified;
                Assert.That(_sut.ModifiedDate, Is.EqualTo(firstModified));
                _sut.ModifiedDate = secondModified;
                Assert.That(_sut.ModifiedDate, Is.EqualTo(secondModified));
                _sut.ModifiedDate = null;
                Assert.That(_sut.ModifiedDate, Is.Null);
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
            // Arrange, Act & Assert
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

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _sut.CreatedDate = DateTime.MinValue;

            // Assert
            Assert.That(_sut.CreatedDate, Is.EqualTo(DateTime.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _sut.CreatedDate = DateTime.MaxValue;

            // Assert
            Assert.That(_sut.CreatedDate, Is.EqualTo(DateTime.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldAcceptMinValue()
        {
            // Arrange & Act
            _sut.ModifiedDate = DateTime.MinValue;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(DateTime.MinValue));
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldAcceptMaxValue()
        {
            // Arrange & Act
            _sut.ModifiedDate = DateTime.MaxValue;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(DateTime.MaxValue));
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldMaintainPrecision()
        {
            // Arrange
            var preciseDate = new DateTime(2023, 12, 25, 14, 30, 45, 123);

            // Act
            _sut.CreatedDate = preciseDate;

            // Assert
            Assert.That(_sut.CreatedDate, Is.EqualTo(preciseDate));
            Assert.That(_sut.CreatedDate.Millisecond, Is.EqualTo(123));
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldMaintainPrecision()
        {
            // Arrange
            var preciseDate = new DateTime(2023, 11, 15, 9, 45, 30, 456);

            // Act
            _sut.ModifiedDate = preciseDate;

            // Assert
            Assert.That(_sut.ModifiedDate, Is.EqualTo(preciseDate));
            Assert.That(_sut.ModifiedDate?.Millisecond, Is.EqualTo(456));
        }

        [Test, Category("DataTransferObjects")]
        public void IFeatureDTO_InterfaceConsistency_ShouldExposeAllProperties()
        {
            // Arrange
            IFeatureDTO interfaceDto = new FeatureDTO();
            var testModified = DateTime.Now.AddHours(-2);

            // Act
            interfaceDto.Id = 100;
            interfaceDto.FeatureName = "Interface Feature";
            interfaceDto.IsEnabled = true;
            interfaceDto.ModifiedDate = testModified;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(interfaceDto.Id, Is.EqualTo(100));
                Assert.That(interfaceDto.FeatureName, Is.EqualTo("Interface Feature"));
                Assert.That(interfaceDto.IsEnabled, Is.True);
                Assert.That(interfaceDto.CreatedDate, Is.Not.EqualTo(default(DateTime))); // CreatedDate is read-only, check it has a value
                Assert.That(interfaceDto.ModifiedDate, Is.EqualTo(testModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureName_ShouldHandleConsecutiveAssignments()
        {
            // Arrange
            var featureNames = new[] { "First Feature", "Second Feature", null, "Third Feature", "", "Fourth Feature" };

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
        public void Id_ShouldSupportSequentialAssignments()
        {
            // Arrange
            var ids = new[] { 0, 1, -1, int.MaxValue, int.MinValue, 42, 999 };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var id in ids)
                {
                    _sut.Id = id;
                    Assert.That(_sut.Id, Is.EqualTo(id));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void IsEnabled_ShouldHandleConsecutiveToggling()
        {
            // Arrange
            var booleanValues = new[] { true, false, true, false, true };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var value in booleanValues)
                {
                    _sut.IsEnabled = value;
                    Assert.That(_sut.IsEnabled, Is.EqualTo(value));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureName_ShouldAcceptSpecialCharactersAndFormats()
        {
            // Arrange
            var specialFeatureNames = new[]
            {
                "Feature@2023",
                "Feature#1_Advanced",
                "Feature (Version 2.0)",
                "Multi-Line\nFeature",
                "Tab\tSeparated\tFeature",
                "Quote\"Feature\"Test",
                "Apostrophe's Feature",
                "Path\\Like\\Feature",
                "URL-like://feature.com/test"
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var featureName in specialFeatureNames)
                {
                    _sut.FeatureName = featureName;
                    Assert.That(_sut.FeatureName, Is.EqualTo(featureName));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldHandleNullToDateTransitions()
        {
            // Arrange
            var testDates = new DateTime[]
            {
                DateTime.Now,
                DateTime.MinValue,
                DateTime.MaxValue,
                new DateTime(2020, 1, 1),
                new DateTime(2030, 12, 31, 23, 59, 59)
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var date in testDates)
                {
                    // Start with null
                    _sut.ModifiedDate = null;
                    Assert.That(_sut.ModifiedDate, Is.Null);

                    // Assign date
                    _sut.ModifiedDate = date;
                    Assert.That(_sut.ModifiedDate, Is.EqualTo(date));

                    // Back to null
                    _sut.ModifiedDate = null;
                    Assert.That(_sut.ModifiedDate, Is.Null);
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureDTO_ShouldMaintainStateAcrossMultipleOperations()
        {
            // Arrange
            var operations = new[]
            {
                new { Id = 1, Name = (string?)"Feature One", Enabled = true },
                new { Id = 2, Name = (string?)"Feature Two", Enabled = false },
                new { Id = 3, Name = (string?)null, Enabled = true },
                new { Id = 4, Name = (string?)"", Enabled = false }
            };

            // Act & Assert
            Assert.Multiple(() =>
            {
                foreach (var op in operations)
                {
                    _sut.Id = op.Id;
                    _sut.FeatureName = op.Name;
                    _sut.IsEnabled = op.Enabled;

                    Assert.That(_sut.Id, Is.EqualTo(op.Id));
                    Assert.That(_sut.FeatureName, Is.EqualTo(op.Name));
                    Assert.That(_sut.IsEnabled, Is.EqualTo(op.Enabled));
                }
            });
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureDTO_PropertiesShouldBeIndependent()
        {
            // Arrange & Act
            _sut.Id = 999;
            _sut.FeatureName = "Independent Feature";
            _sut.IsEnabled = true;
            var testDate = DateTime.Now.AddDays(-5);
            var testModified = DateTime.Now.AddHours(-3);
            _sut.CreatedDate = testDate;
            _sut.ModifiedDate = testModified;

            // Store original values
            var originalId = _sut.Id;
            var originalName = _sut.FeatureName;
            var originalEnabled = _sut.IsEnabled;
            var originalCreated = _sut.CreatedDate;
            var originalModified = _sut.ModifiedDate;

            // Assert
            Assert.Multiple(() =>
            {
                // Change Id, verify others unchanged
                _sut.Id = 1000;
                Assert.That(_sut.FeatureName, Is.EqualTo(originalName));
                Assert.That(_sut.IsEnabled, Is.EqualTo(originalEnabled));
                Assert.That(_sut.CreatedDate, Is.EqualTo(originalCreated));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(originalModified));

                // Change FeatureName, verify others unchanged
                _sut.FeatureName = "Changed Feature";
                Assert.That(_sut.Id, Is.EqualTo(1000)); // New value
                Assert.That(_sut.IsEnabled, Is.EqualTo(originalEnabled));
                Assert.That(_sut.CreatedDate, Is.EqualTo(originalCreated));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(originalModified));

                // Change IsEnabled, verify others unchanged
                _sut.IsEnabled = false;
                Assert.That(_sut.Id, Is.EqualTo(1000)); // New value
                Assert.That(_sut.FeatureName, Is.EqualTo("Changed Feature")); // New value
                Assert.That(_sut.CreatedDate, Is.EqualTo(originalCreated));
                Assert.That(_sut.ModifiedDate, Is.EqualTo(originalModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureDTO_ShouldSupportObjectInitializerSyntax()
        {
            // Arrange
            var testCreated = DateTime.Now.AddDays(-7);
            var testModified = DateTime.Now.AddHours(-1);

            // Act
            var featureDto = new FeatureDTO
            {
                Id = 555,
                FeatureName = "Initializer Feature",
                IsEnabled = true,
                CreatedDate = testCreated,
                ModifiedDate = testModified
            };

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(featureDto.Id, Is.EqualTo(555));
                Assert.That(featureDto.FeatureName, Is.EqualTo("Initializer Feature"));
                Assert.That(featureDto.IsEnabled, Is.True);
                Assert.That(featureDto.CreatedDate, Is.EqualTo(testCreated));
                Assert.That(featureDto.ModifiedDate, Is.EqualTo(testModified));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void Cast_ShouldThrowNotImplementedException_WithDifferentGenericTypes()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                Assert.Throws<NotImplementedException>(() => _sut.Cast<MockDomainEntity>());
                Assert.Throws<NotImplementedException>(() => _sut.Cast<IFeatureDTO>());
                Assert.Throws<NotImplementedException>(() => _sut.Cast<IDomainEntity>());
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ToJson_ShouldConsistentlyThrowNotImplementedException()
        {
            // Arrange - Multiple calls should all throw
            var exceptions = new List<NotImplementedException>();

            // Act & Assert
            Assert.Multiple(() =>
            {
                for (int i = 0; i < 3; i++)
                {
                    var ex = Assert.Throws<NotImplementedException>(() => _sut.ToJson());
                    Assert.That(ex, Is.Not.Null);
                    if (ex != null)
                    {
                        exceptions.Add(ex);
                    }
                }
                
                // Verify all exceptions are separate instances
                Assert.That(exceptions, Has.Count.EqualTo(3));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureName_ShouldSupportVeryLongStrings()
        {
            // Arrange
            var veryLongFeatureName = new string('F', 10000) + " Feature";

            // Act
            _sut.FeatureName = veryLongFeatureName;

            // Assert
            Assert.That(_sut.FeatureName, Is.EqualTo(veryLongFeatureName));
            Assert.That(_sut.FeatureName?.Length, Is.EqualTo(10008)); // 10000 + " Feature".Length (8)
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureName_ShouldAcceptNumbersAndSymbols()
        {
            // Arrange
            var numericFeatureName = "12345!@#$%^&*()_+-=[]{}|;':\",./<>?";

            // Act
            _sut.FeatureName = numericFeatureName;

            // Assert
            Assert.That(_sut.FeatureName, Is.EqualTo(numericFeatureName));
        }

        [Test, Category("DataTransferObjects")]
        public void IsEnabled_ShouldSupportComparisonOperators()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                _sut.IsEnabled = true;
                Assert.That(_sut.IsEnabled == true, Is.True);
                Assert.That(_sut.IsEnabled != false, Is.True);
                Assert.That(_sut.IsEnabled.Equals(true), Is.True);

                _sut.IsEnabled = false;
                Assert.That(_sut.IsEnabled == false, Is.True);
                Assert.That(_sut.IsEnabled != true, Is.True);
                Assert.That(_sut.IsEnabled.Equals(false), Is.True);
            });
        }

        [Test, Category("DataTransferObjects")]
        public void CreatedDate_ShouldSupportDateTimeOperations()
        {
            // Arrange
            var baseDate = new DateTime(2023, 6, 15, 12, 0, 0);
            _sut.CreatedDate = baseDate;

            // Act & Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.CreatedDate.AddDays(1), Is.EqualTo(new DateTime(2023, 6, 16, 12, 0, 0)));
                Assert.That(_sut.CreatedDate.AddHours(-1), Is.EqualTo(new DateTime(2023, 6, 15, 11, 0, 0)));
                Assert.That(_sut.CreatedDate.Date, Is.EqualTo(new DateTime(2023, 6, 15)));
                Assert.That(_sut.CreatedDate.Year, Is.EqualTo(2023));
                Assert.That(_sut.CreatedDate.Month, Is.EqualTo(6));
                Assert.That(_sut.CreatedDate.Day, Is.EqualTo(15));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void ModifiedDate_ShouldSupportNullableOperations()
        {
            // Arrange, Act & Assert
            Assert.Multiple(() =>
            {
                // Test null state
                _sut.ModifiedDate = null;
                Assert.That(_sut.ModifiedDate.HasValue, Is.False);
                Assert.That(_sut.ModifiedDate.GetValueOrDefault(), Is.EqualTo(default(DateTime)));

                // Test with value
                var testDate = new DateTime(2023, 5, 10, 15, 30, 45);
                _sut.ModifiedDate = testDate;
                Assert.That(_sut.ModifiedDate.HasValue, Is.True);
                Assert.That(_sut.ModifiedDate.Value, Is.EqualTo(testDate));
                Assert.That(_sut.ModifiedDate.GetValueOrDefault(), Is.EqualTo(testDate));
            });
        }

        [Test, Category("DataTransferObjects")]
        public void FeatureDTO_ShouldHandleComplexScenarios()
        {
            // Arrange & Act
            _sut.Id = int.MaxValue;
            _sut.FeatureName = "Complex Feature with 特殊字符 and emojis 🚀✅";
            _sut.IsEnabled = true;
            _sut.CreatedDate = DateTime.MaxValue.AddMilliseconds(-1);
            _sut.ModifiedDate = DateTime.MinValue.AddMilliseconds(1);

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Id, Is.EqualTo(int.MaxValue));
                Assert.That(_sut.FeatureName, Contains.Substring("Complex Feature"));
                Assert.That(_sut.FeatureName, Contains.Substring("特殊字符"));
                Assert.That(_sut.FeatureName, Contains.Substring("🚀✅"));
                Assert.That(_sut.IsEnabled, Is.True);
                Assert.That(_sut.CreatedDate, Is.LessThan(DateTime.MaxValue));
                Assert.That(_sut.ModifiedDate, Is.GreaterThan(DateTime.MinValue));
            });
        }

        #region Mock Classes
        private class MockDomainEntity : IDomainEntity
        {
            public int Id { get; set; }
            public bool IsCast { get; set; }
            public int CastId { get; set; }
            public string? CastType { get; set; }
            public DateTime CreatedDate { get; }
            public DateTime? ModifiedDate { get; set; } = default;
            public T Cast<T>() where T : IDomainEntity => throw new NotImplementedException();
            public string ToJson() => throw new NotImplementedException();
        }
        #endregion
    }
}
