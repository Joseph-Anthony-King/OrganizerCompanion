using NUnit.Framework;
using OrganizerCompanion.Core.Models.Type;

namespace OrganizerCompanion.Core.UnitTests.Types
{
    [TestFixture]
    internal class MXStateShould
    {
        private MXState _sut;

        [SetUp]
        public void SetUp()
        {
            _sut = new MXState();
        }

        [Test, Category("Types")]
        public void DefaultConstructor_ShouldCreateStateWithNullValues()
        {
            // Arrange & Act
            _sut = new MXState();

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
            var stateName = "Jalisco";

            // Act
            _sut.Name = stateName;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(stateName));
        }

        [Test, Category("Types")]
        public void Name_WhenSetToNull_ShouldAcceptNullValue()
        {
            // Arrange
            _sut.Name = "Nuevo León";

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
            var abbreviation = "JAL";

            // Act
            _sut.Abbreviation = abbreviation;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviation));
        }

        [Test, Category("Types")]
        public void Abbreviation_WhenSetToNull_ShouldAcceptNullValue()
        {
            // Arrange
            _sut.Abbreviation = "NL";

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
            var stateName = "Ciudad de México";
            var abbreviation = "CDMX";

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
            var longName = "Michoacán de Ocampo";

            // Act
            _sut.Name = longName;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(longName));
        }

        [Test, Category("Types")]
        public void Name_WithSpecialCharacters_ShouldBeAccepted()
        {
            // Arrange
            var nameWithSpecialChars = "Nuevo León";

            // Act
            _sut.Name = nameWithSpecialChars;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(nameWithSpecialChars));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithLowerCase_ShouldBeAccepted()
        {
            // Arrange
            var lowerCaseAbbreviation = "jal";

            // Act
            _sut.Abbreviation = lowerCaseAbbreviation;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(lowerCaseAbbreviation));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithMixedCase_ShouldBeAccepted()
        {
            // Arrange
            var mixedCaseAbbreviation = "Jal";

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
            var nameWithWhitespace = " Veracruz ";

            // Act
            _sut.Name = nameWithWhitespace;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(nameWithWhitespace));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithWhitespace_ShouldBeAccepted()
        {
            // Arrange
            var abbreviationWithWhitespace = " VER ";

            // Act
            _sut.Abbreviation = abbreviationWithWhitespace;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviationWithWhitespace));
        }

        [Test, Category("Types")]
        public void Properties_WhenSetMultipleTimes_ShouldRetainLastValue()
        {
            // Arrange
            var firstName = "Sonora";
            var firstAbbreviation = "SON";
            var secondName = "Sinaloa";
            var secondAbbreviation = "SIN";

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
        public void StateWithAllMexicanStates_ShouldAcceptAllValidNames()
        {
            // Test with all Mexican states
            var states = new[]
            {
                ("Aguascalientes", "AGU"),
                ("Baja California", "BC"),
                ("Baja California Sur", "BCS"),
                ("Campeche", "CAM"),
                ("Chiapas", "CHP"),
                ("Chihuahua", "CHH"),
                ("Ciudad de México", "CDMX"),
                ("Coahuila", "COA"),
                ("Colima", "COL"),
                ("Durango", "DUR"),
                ("Guanajuato", "GTO"),
                ("Guerrero", "GRO"),
                ("Hidalgo", "HID"),
                ("Jalisco", "JAL"),
                ("Estado de México", "MEX"),
                ("Michoacán", "MIC"),
                ("Morelos", "MOR"),
                ("Nayarit", "NAY"),
                ("Nuevo León", "NL"),
                ("Oaxaca", "OAX"),
                ("Puebla", "PUE"),
                ("Querétaro", "QRO"),
                ("Quintana Roo", "QR"),
                ("San Luis Potosí", "SLP"),
                ("Sinaloa", "SIN"),
                ("Sonora", "SON"),
                ("Tabasco", "TAB"),
                ("Tamaulipas", "TAM"),
                ("Tlaxcala", "TLX"),
                ("Veracruz", "VER"),
                ("Yucatán", "YUC"),
                ("Zacatecas", "ZAC")
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
            var nameWithNumbers = "Estado123";

            // Act
            _sut.Name = nameWithNumbers;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(nameWithNumbers));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithNumericCharacters_ShouldBeAccepted()
        {
            // Arrange
            var abbreviationWithNumbers = "E1";

            // Act
            _sut.Abbreviation = abbreviationWithNumbers;

            // Assert
            Assert.That(_sut.Abbreviation, Is.EqualTo(abbreviationWithNumbers));
        }

        [Test, Category("Types")]
        public void Properties_WhenOneIsNullAndOtherIsSet_ShouldMaintainIndependence()
        {
            // Arrange
            var stateName = "Yucatán";

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
            _sut.Abbreviation = "YUC";

            // Assert
            Assert.Multiple(() =>
            {
                Assert.That(_sut.Name, Is.Null);
                Assert.That(_sut.Abbreviation, Is.EqualTo("YUC"));
            });
        }

        [Test, Category("Types")]
        public void Name_WithAccentedCharacters_ShouldBeAccepted()
        {
            // Arrange
            var nameWithAccents = "Michoacán de Ocampo";

            // Act
            _sut.Name = nameWithAccents;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(nameWithAccents));
        }

        [Test, Category("Types")]
        public void Name_WithSpanishCharacters_ShouldBeAccepted()
        {
            // Arrange
            var nameWithSpanishChars = "Querétaro de Arteaga";

            // Act
            _sut.Name = nameWithSpanishChars;

            // Assert
            Assert.That(_sut.Name, Is.EqualTo(nameWithSpanishChars));
        }

        [Test, Category("Types")]
        public void Abbreviation_WithSpecialCharacters_ShouldBeAccepted()
        {
            // Arrange
            var abbreviationWithSpecialChars = "Q-RO";

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
            var nameWithUnicode = "Estado 🇲🇽";
            var abbreviationWithUnicode = "E🌟";

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
    }
}
