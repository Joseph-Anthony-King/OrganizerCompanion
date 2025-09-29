using NUnit.Framework;
using OrganizerCompanion.Core.Models.Type;

namespace OrganizerCompanion.Core.UnitTests.Types
{
    [TestFixture]
    internal class CAProvinceShould
    {
        private CAProvince _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new CAProvince();
        }

        [Test, Category("Types")]
        public void DefaultConstructor_ShouldCreateProvinceWithNullValues()
        {
            // Arrange & Act
            _sut = new CAProvince();

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Name, Is.Null);
                Assert.That(_sut.Abbreviation, Is.Null);
            });
        }

        [Test, Category("Types")]
        public void Name_WhenSet_ShouldReturnCorrectValue()
        {
            // Arrange
            var provinceName = "Ontario";

            // Act
            _sut.Name = provinceName;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(provinceName));
        }

        [Test, Category("Types")]
        public void Name_WhenSetToNull_ShouldAcceptNullValue()
        {
            // Arrange
            _sut.Name = "British Columbia";

            // Act
            _sut.Name = null;

            // Assert
            Assert.That(_sut.Name, Is.Null);
        }

        [Test, Category("Types")]
        public void Name_WhenSetToEmptyString_ShouldAcceptEmptyValue()
        {
            // Arrange & Act
            _sut.Name = string.Empty;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(string.Empty));
        }

        [Test, Category("Types")]
        public void Abbreviation_WhenSet_ShouldReturnCorrectValue()
        {
            // Arrange
            var abbreviation = "ON";

            // Act
            _sut.Abbreviation = abbreviation;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviation));
        }

        [Test, Category("Types")]
        public void Abbreviation_WhenSetToNull_ShouldAcceptNullValue()
        {
            // Arrange
            _sut.Abbreviation = "BC";

            // Act
            _sut.Abbreviation = null;

            // Assert
            Assert.That(_sut.Abbreviation, Is.Null);
        }

        [Test, Category("Types")]
        public void Abbreviation_WhenSetToEmptyString_ShouldAcceptEmptyValue()
        {
            // Arrange & Act
            _sut.Abbreviation = string.Empty;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(string.Empty));
        }

        [Test, Category("Types")]
        public void Properties_WhenSetIndependently_ShouldNotAffectEachOther()
        {
            // Arrange
            var provinceName = "Quebec";
            var abbreviation = "QC";

            // Act
            _sut.Name = provinceName;
            _sut.Abbreviation = abbreviation;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Name, Is.EqualTo(provinceName));
                Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviation));
            });
        }

        [Test, Category("Types")]
        public void Name_WithLongProvinceNames_ShouldBeAccepted()
        {
            // Arrange
            var longName = "Prince Edward Island";

            // Act
            _sut.Name = longName;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(longName));
        }

        [Test, Category("Types")]
        public void Name_WithSpecialCharacters_ShouldBeAccepted()
        {
            // Arrange
            var nameWithSpecialChars = "Québec";

            // Act
            _sut.Name = nameWithSpecialChars;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(nameWithSpecialChars));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithLowerCase_ShouldBeAccepted()
        {
            // Arrange
            var lowerCaseAbbreviation = "bc";

            // Act
            _sut.Abbreviation = lowerCaseAbbreviation;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(lowerCaseAbbreviation));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithMixedCase_ShouldBeAccepted()
        {
            // Arrange
            var mixedCaseAbbreviation = "Bc";

            // Act
            _sut.Abbreviation = mixedCaseAbbreviation;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(mixedCaseAbbreviation));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithLongString_ShouldBeAccepted()
        {
            // Arrange
            var longAbbreviation = "LONG_ABBREVIATION";

            // Act
            _sut.Abbreviation = longAbbreviation;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(longAbbreviation));
        }

        [Test, Category("Types")]
        public void Name_WithWhitespace_ShouldBeAccepted()
        {
            // Arrange
            var nameWithWhitespace = " British Columbia ";

            // Act
            _sut.Name = nameWithWhitespace;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(nameWithWhitespace));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithWhitespace_ShouldBeAccepted()
        {
            // Arrange
            var abbreviationWithWhitespace = " BC ";

            // Act
            _sut.Abbreviation = abbreviationWithWhitespace;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviationWithWhitespace));
        }

        [Test, Category("Types")]
        public void Properties_WhenSetMultipleTimes_ShouldRetainLastValue()
        {
            // Arrange
            var firstName = "Alberta";
            var firstAbbreviation = "AB";
            var secondName = "Saskatchewan";
            var secondAbbreviation = "SK";

            // Act
            _sut.Name = firstName;
            _sut.Abbreviation = firstAbbreviation;
            _sut.Name = secondName;
            _sut.Abbreviation = secondAbbreviation;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Name, Is.EqualTo(secondName));
                Assert.That(_sut.Abbreviation, Is.EqualTo(secondAbbreviation));
            });
        }

        [Test, Category("Types")]
        public void ProvinceWithAllCanadianProvinces_ShouldAcceptAllValidNames()
        {
            // Test with all Canadian provinces and territories
            var provinces = new[]
            {
                ("Alberta", "AB"),
                ("British Columbia", "BC"),
                ("Manitoba", "MB"),
                ("New Brunswick", "NB"),
                ("Newfoundland and Labrador", "NL"),
                ("Northwest Territories", "NT"),
                ("Nova Scotia", "NS"),
                ("Nunavut", "NU"),
                ("Ontario", "ON"),
                ("Prince Edward Island", "PE"),
                ("Quebec", "QC"),
                ("Saskatchewan", "SK"),
                ("Yukon", "YT")
            };

            foreach (var (name, abbreviation) in provinces)
            {
                // Arrange & Act
                _sut.Name = name;
                _sut.Abbreviation = abbreviation;

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(_sut.Name, Is.EqualTo(name), $"Failed for province: {name}");
                    Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviation), $"Failed for abbreviation: {abbreviation}");
                });
            }
        }

        [Test, Category("Types")]
        public void Name_WithNumericCharacters_ShouldBeAccepted()
        {
            // Arrange
            var nameWithNumbers = "Province123";

            // Act
            _sut.Name = nameWithNumbers;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(nameWithNumbers));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithNumericCharacters_ShouldBeAccepted()
        {
            // Arrange
            var abbreviationWithNumbers = "P1";

            // Act
            _sut.Abbreviation = abbreviationWithNumbers;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviationWithNumbers));
        }

        [Test, Category("Types")]
        public void Properties_WhenOneIsNullAndOtherIsSet_ShouldMaintainIndependence()
        {
            // Arrange
            var provinceName = "Manitoba";

            // Act
            _sut.Name = provinceName;
            _sut.Abbreviation = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Name, Is.EqualTo(provinceName));
                Assert.That(_sut.Abbreviation, Is.Null);
            });

            // Act - Reverse scenario
            _sut.Name = null;
            _sut.Abbreviation = "MB";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Name, Is.Null);
                Assert.That(_sut.Abbreviation, Is.EqualTo("MB"));
            });
        }
    }
}
