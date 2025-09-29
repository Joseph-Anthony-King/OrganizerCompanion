using NUnit.Framework;
using OrganizerCompanion.Core.Models.Type;

namespace OrganizerCompanion.Core.UnitTests.Types
{
    [TestFixture]
    internal class USStateShould
    {
        private USState _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new USState();
        }

        [Test, Category("Types")]
        public void DefaultConstructor_ShouldCreateStateWithNullValues()
        {
            // Arrange & Act
            _sut = new USState();

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
            var stateName = "California";

            // Act
            _sut.Name = stateName;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(stateName));
        }

        [Test, Category("Types")]
        public void Name_WhenSetToNull_ShouldAcceptNullValue()
        {
            // Arrange
            _sut.Name = "Texas";

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
            var abbreviation = "CA";

            // Act
            _sut.Abbreviation = abbreviation;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviation));
        }

        [Test, Category("Types")]
        public void Abbreviation_WhenSetToNull_ShouldAcceptNullValue()
        {
            // Arrange
            _sut.Abbreviation = "TX";

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
            var stateName = "New York";
            var abbreviation = "NY";

            // Act
            _sut.Name = stateName;
            _sut.Abbreviation = abbreviation;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Name, Is.EqualTo(stateName));
                Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviation));
            });
        }

        [Test, Category("Types")]
        public void Name_WithLongStateNames_ShouldBeAccepted()
        {
            // Arrange
            var longName = "North Carolina";

            // Act
            _sut.Name = longName;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(longName));
        }

        [Test, Category("Types")]
        public void Name_WithSpecialCharacters_ShouldBeAccepted()
        {
            // Arrange
            var nameWithSpecialChars = "Hawai'i";

            // Act
            _sut.Name = nameWithSpecialChars;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(nameWithSpecialChars));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithLowerCase_ShouldBeAccepted()
        {
            // Arrange
            var lowerCaseAbbreviation = "ca";

            // Act
            _sut.Abbreviation = lowerCaseAbbreviation;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(lowerCaseAbbreviation));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithMixedCase_ShouldBeAccepted()
        {
            // Arrange
            var mixedCaseAbbreviation = "Ca";

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
            var nameWithWhitespace = " Florida ";

            // Act
            _sut.Name = nameWithWhitespace;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(nameWithWhitespace));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithWhitespace_ShouldBeAccepted()
        {
            // Arrange
            var abbreviationWithWhitespace = " FL ";

            // Act
            _sut.Abbreviation = abbreviationWithWhitespace;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviationWithWhitespace));
        }

        [Test, Category("Types")]
        public void Properties_WhenSetMultipleTimes_ShouldRetainLastValue()
        {
            // Arrange
            var firstName = "Arizona";
            var firstAbbreviation = "AZ";
            var secondName = "Nevada";
            var secondAbbreviation = "NV";

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
        public void StateWithAllUSStates_ShouldAcceptAllValidNames()
        {
            // Test with all US states and territories
            var states = new[]
            {
                ("Alabama", "AL"),
                ("Alaska", "AK"),
                ("Arizona", "AZ"),
                ("Arkansas", "AR"),
                ("California", "CA"),
                ("Colorado", "CO"),
                ("Connecticut", "CT"),
                ("Delaware", "DE"),
                ("Florida", "FL"),
                ("Georgia", "GA"),
                ("Hawaii", "HI"),
                ("Idaho", "ID"),
                ("Illinois", "IL"),
                ("Indiana", "IN"),
                ("Iowa", "IA"),
                ("Kansas", "KS"),
                ("Kentucky", "KY"),
                ("Louisiana", "LA"),
                ("Maine", "ME"),
                ("Maryland", "MD"),
                ("Massachusetts", "MA"),
                ("Michigan", "MI"),
                ("Minnesota", "MN"),
                ("Mississippi", "MS"),
                ("Missouri", "MO"),
                ("Montana", "MT"),
                ("Nebraska", "NE"),
                ("Nevada", "NV"),
                ("New Hampshire", "NH"),
                ("New Jersey", "NJ"),
                ("New Mexico", "NM"),
                ("New York", "NY"),
                ("North Carolina", "NC"),
                ("North Dakota", "ND"),
                ("Ohio", "OH"),
                ("Oklahoma", "OK"),
                ("Oregon", "OR"),
                ("Pennsylvania", "PA"),
                ("Rhode Island", "RI"),
                ("South Carolina", "SC"),
                ("South Dakota", "SD"),
                ("Tennessee", "TN"),
                ("Texas", "TX"),
                ("Utah", "UT"),
                ("Vermont", "VT"),
                ("Virginia", "VA"),
                ("Washington", "WA"),
                ("West Virginia", "WV"),
                ("Wisconsin", "WI"),
                ("Wyoming", "WY"),
                ("District of Columbia", "DC"),
                ("American Samoa", "AS"),
                ("Guam", "GU"),
                ("Northern Mariana Islands", "MP"),
                ("Puerto Rico", "PR"),
                ("U.S. Virgin Islands", "VI")
            };

            foreach (var (name, abbreviation) in states)
            {
                // Arrange & Act
                _sut.Name = name;
                _sut.Abbreviation = abbreviation;

                // Assert
                Assert.Multiple(() =>
                {
                    Assert.That(_sut.Name, Is.EqualTo(name), $"Failed for state: {name}");
                    Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviation), $"Failed for abbreviation: {abbreviation}");
                });
            }
        }

        [Test, Category("Types")]
        public void Name_WithNumericCharacters_ShouldBeAccepted()
        {
            // Arrange
            var nameWithNumbers = "State123";

            // Act
            _sut.Name = nameWithNumbers;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(nameWithNumbers));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithNumericCharacters_ShouldBeAccepted()
        {
            // Arrange
            var abbreviationWithNumbers = "S1";

            // Act
            _sut.Abbreviation = abbreviationWithNumbers;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviationWithNumbers));
        }

        [Test, Category("Types")]
        public void Properties_WhenOneIsNullAndOtherIsSet_ShouldMaintainIndependence()
        {
            // Arrange
            var stateName = "Oregon";

            // Act
            _sut.Name = stateName;
            _sut.Abbreviation = null;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Name, Is.EqualTo(stateName));
                Assert.That(_sut.Abbreviation, Is.Null);
            });

            // Act - Reverse scenario
            _sut.Name = null;
            _sut.Abbreviation = "OR";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Name, Is.Null);
                Assert.That(_sut.Abbreviation, Is.EqualTo("OR"));
            });
        }

        [Test, Category("Types")]
        public void Name_WithApostrophes_ShouldBeAccepted()
        {
            // Arrange
            var nameWithApostrophe = "Hawai'i";

            // Act
            _sut.Name = nameWithApostrophe;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(nameWithApostrophe));
        }

        [Test, Category("Types")]
        public void Name_WithPeriods_ShouldBeAccepted()
        {
            // Arrange
            var nameWithPeriods = "U.S. Virgin Islands";

            // Act
            _sut.Name = nameWithPeriods;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(nameWithPeriods));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithSpecialCharacters_ShouldBeAccepted()
        {
            // Arrange
            var abbreviationWithSpecialChars = "N-Y";

            // Act
            _sut.Abbreviation = abbreviationWithSpecialChars;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviationWithSpecialChars));
        }

        [Test, Category("Types")]
        public void Properties_WithVeryLongStrings_ShouldBeAccepted()
        {
            // Arrange
            var veryLongName = new string('A', 1000);
            var veryLongAbbreviation = new string('B', 100);

            // Act
            _sut.Name = veryLongName;
            _sut.Abbreviation = veryLongAbbreviation;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Name, Is.EqualTo(veryLongName));
                Assert.That(_sut.Abbreviation, Is.EqualTo(veryLongAbbreviation));
            });
        }

        [Test, Category("Types")]
        public void Properties_WithUnicodeCharacters_ShouldBeAccepted()
        {
            // Arrange
            var nameWithUnicode = "State 🇺🇸";
            var abbreviationWithUnicode = "S🌟";

            // Act
            _sut.Name = nameWithUnicode;
            _sut.Abbreviation = abbreviationWithUnicode;

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Name, Is.EqualTo(nameWithUnicode));
                Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviationWithUnicode));
            });
        }

        [Test, Category("Types")]
        public void Name_WithHyphens_ShouldBeAccepted()
        {
            // Arrange
            var nameWithHyphens = "North-South Carolina";

            // Act
            _sut.Name = nameWithHyphens;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(nameWithHyphens));
        }

        [Test, Category("Types")]
        public void Name_WithMultipleWords_ShouldBeAccepted()
        {
            // Arrange
            var multiWordName = "New Hampshire State";

            // Act
            _sut.Name = multiWordName;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(multiWordName));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithThreeCharacters_ShouldBeAccepted()
        {
            // Arrange
            var threeCharAbbreviation = "USA";

            // Act
            _sut.Abbreviation = threeCharAbbreviation;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(threeCharAbbreviation));
        }
    }
}
